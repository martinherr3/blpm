using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
    public partial class reportRendimiento : EDUARBasePage
    {
        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the filtro calificaciones.
        /// </summary>
        /// <value>
        /// The filtro calificaciones.
        /// </value>
        public FilCalificacionesAlumnoPeriodo filtroReporte
        {
            get
            {
                if (ViewState["filtroCalificaciones"] == null)
                    filtroReporte = new FilCalificacionesAlumnoPeriodo();
                return (FilCalificacionesAlumnoPeriodo)ViewState["filtroCalificaciones"];
            }
            set
            {
                ViewState["filtroCalificaciones"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the lista calificaciones.
        /// </summary>
        /// <value>
        /// The lista calificaciones.
        /// </value>
        public List<RptRendimientoHistorico> listaReporteRendimiento
        {
            get
            {
                if (Session["listaReporteRendimiento"] == null)
                    listaReporteRendimiento = new List<RptRendimientoHistorico>();
                return (List<RptRendimientoHistorico>)Session["listaReporteRendimiento"];
            }
            set
            {
                Session["listaReporteRendimiento"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the filtros aplicados.
        /// </summary>
        /// <value>
        /// The filtros aplicados.
        /// </value>
        public string filtrosAplicados
        {
            get
            {
                if (Session["filtrosAplicados"] == null)
                    filtrosAplicados = string.Empty;
                return Session["filtrosAplicados"].ToString();
            }
            set
            {
                Session["filtrosAplicados"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the lista niveles.
        /// </summary>
        /// <value>
        /// The lista niveles.
        /// </value>
        public List<Nivel> listaNiveles
        {
            get
            {
                if (ViewState["listaNiveles"] == null)
                {
                    listaNiveles = new List<Nivel>();
                    BLNivel objBLNivel = new BLNivel();
                    listaNiveles = objBLNivel.GetNiveles();
                }
                return (List<Nivel>)ViewState["listaNiveles"];
            }
            set
            {
                ViewState["listaNiveles"] = value;
            }
        }

        public List<Asignatura> listaAsignatura
        {
            get
            {
                if (ViewState["listaAsignatura"] == null)
                {
                    listaAsignatura = new List<Asignatura>();
                    BLAsignatura objBLAsignatura = new BLAsignatura();
                    listaAsignatura = objBLAsignatura.GetAsignaturas(null);
                }
                return (List<Asignatura>)ViewState["listaAsignatura"];
            }
            set
            {
                ViewState["listaAsignatura"] = value;
            }
        }

        public List<CicloLectivo> listaCicloLectivo
        {
            get
            {
                if (ViewState["listaCicloLectivo"] == null)
                {
                    listaCicloLectivo = new List<CicloLectivo>();
                    BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
                    listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);
                }
                return (List<CicloLectivo>)ViewState["listaCicloLectivo"];
            }
            set
            {
                ViewState["listaCicloLectivo"] = value;
            }
        }
        #endregion

        #region --[Eventos]--
        /// <summary>
        /// Método que se ejecuta al dibujar los controles de la página.
        /// Se utiliza para gestionar las excepciones del método Page_Load().
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (AvisoMostrar)
            {
                AvisoMostrar = false;

                try
                {
                    Master.ManageExceptions(AvisoExcepcion);
                }
                catch (Exception ex) { Master.ManageExceptions(ex); }
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                rptCalificaciones.orderExpression = "asignatura";
                rptCalificaciones.ExportarPDFClick += (ExportarPDF);
                rptCalificaciones.VolverClick += (VolverReporte);
                rptCalificaciones.PaginarGrilla += (PaginarGrilla);
                rptCalificaciones.CerrarGraficoClick += (CerrarGrafico);
                Master.BotonAvisoAceptar += (VentanaAceptar);
                rptCalificaciones.GraficarClick += (btnGraficar);
                rptCalificaciones.OrdenarClick += (OrdenarGrilla);

                if (!Page.IsPostBack)
                {
                    CargarPresentacion();
                    divFiltros.Visible = true;
                    divReporte.Visible = false;
                }
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
            }
        }

        /// <summary>
        /// Ventanas the aceptar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void VentanaAceptar(object sender, EventArgs e)
        {
            AccionPagina = enumAcciones.Limpiar;
            //divFiltros.Visible = true;
            //divReporte.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the btnBuscar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = enumAcciones.Buscar;
                if (BuscarCalificaciones())
                {
                    if (listaReporteRendimiento != null)
                        rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporteRendimiento);
                    AccionPagina = enumAcciones.Limpiar;
                    divFiltros.Visible = false;
                    divReporte.Visible = true;
                }
                else
                { Master.MostrarMensaje("Faltan Datos", UIConstantesGenerales.MensajeDatosRequeridos, EDUAR_Utility.Enumeraciones.enumTipoVentanaInformacion.Advertencia); }
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        /// <summary>
        /// Exportars the PDF.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ExportarPDF(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = enumAcciones.Limpiar;
                string nombreGrafico = string.Empty;
                if (rptCalificaciones.verGrafico)
                    nombreGrafico = nombrePNG;
                ExportPDF.ExportarPDF(Page.Title, rptCalificaciones.dtReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        /// <summary>
        /// Volvers the reporte.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void VolverReporte(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = enumAcciones.Limpiar;
                rptCalificaciones.verGrafico = false;
                divFiltros.Visible = true;
                divReporte.Visible = false;
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        protected void CerrarGrafico(object sender, EventArgs e)
        {
            try
            {
                rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporteRendimiento);
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        /// <summary>
        /// BTNs the graficar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnGraficar(object sender, EventArgs e)
        {
            try
            {
                //GenerarDatosGrafico();
                AccionPagina = enumAcciones.Limpiar;
                double sumaNotas = 0;
                rptCalificaciones.graficoReporte.LimpiarSeries();
                string alumno = string.Empty;

                if (ddlAsignatura.SelectedIndex > 0)
                { // so reporte   distribucion de calificaciones por asignatura 
                    foreach (ListItem itemAsig in ddlAsignatura.Items)
                    {
                        if (itemAsig.Selected)
                        {
                            var serie = new List<RptRendimientoHistorico>();
                            foreach (ListItem itemCiclo in ddlCicloLectivo.Items)
                            {
                                sumaNotas = 0;
                                var listaParcial = listaReporteRendimiento.FindAll(p => p.asignatura == itemAsig.Text && p.ciclolectivo == itemCiclo.Text);

                                if (listaParcial.Count > 0)
                                {
                                    foreach (var nota in listaParcial)
                                    {
                                        sumaNotas += Convert.ToDouble(nota.promedio);
                                    }

                                    serie.Add(new RptRendimientoHistorico
                                    {
                                        promedio = Math.Round(sumaNotas / listaParcial.Count, 2).ToString(CultureInfo.InvariantCulture),
                                        ciclolectivo = itemCiclo.Text
                                    });
                                }
                            }
                            if (serie != null && serie.Count > 0)
                            {
                                DataTable dt = UIUtilidades.BuildDataTable<RptRendimientoHistorico>(serie);
                                rptCalificaciones.graficoReporte.AgregarSerie(itemAsig.Text, dt, "ciclolectivo", "promedio");
                            }
                        }
                        rptCalificaciones.graficoReporte.Titulo = "Promedio de Calificaciones por Asignatura \n";
                    }

                }
                else
                { // promedio de calificaciones por año por asignatura
                    var Ciclos = (from p in listaReporteRendimiento
                                  group p by p.ciclolectivo into g
                                  //orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
                                  select new { CicloLectivo = g.Key }).Distinct();

                    //foreach (ListItem itemCiclo in ddlCicloLectivo.Items)
                    var serie = new List<RptRendimientoHistorico>();

                    foreach (var itemCiclo in Ciclos)
                    {
                        var Promedio =
                       (from p in listaReporteRendimiento
                        where p.ciclolectivo == itemCiclo.CicloLectivo
                        group p by p.ciclolectivo into g
                        select new { CicloLectivo = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct();

                        foreach (var item in Promedio)
                        {
                            serie.Add(new RptRendimientoHistorico
                            {
                                //promedio = Math.Round(sumaNotas / listaParcial.Count, 2).ToString(CultureInfo.InvariantCulture),
                                promedio = Math.Round(item.Promedio, 2).ToString(CultureInfo.InvariantCulture),
                                asignatura = item.CicloLectivo
                            });
                        }
                    }
                    if (serie != null && serie.Count > 0)
                    {
                        DataTable dt = UIUtilidades.BuildDataTable<RptRendimientoHistorico>(serie);
                        rptCalificaciones.graficoReporte.AgregarSerie("Promedios", dt, "asignatura", "promedio");
                    }
                    rptCalificaciones.graficoReporte.Titulo = "Promedio de Calificaciones por Ciclo Lectivo \n";
                    rptCalificaciones.graficoReporte.habilitarTorta = false;
                }
                GenerarDatosGrafico();
                rptCalificaciones.graficoReporte.GraficarLinea();
                rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporteRendimiento);
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        /// <summary>
        /// Paginars the grilla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void PaginarGrilla(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int pagina = e.NewPageIndex;

                if (rptCalificaciones.GrillaReporte.PageCount > pagina)
                {
                    rptCalificaciones.GrillaReporte.PageIndex = pagina;

                    rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporteRendimiento);
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        //<summary>
        //Handles the SelectedIndexChanged event of the ddlNivel control.
        //</summary>
        //<param name="sender">The source of the event.</param>
        //<param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = enumAcciones.Limpiar;

                CargarComboAsignatura();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void OrdenarGrilla(object sender, GridViewSortEventArgs e)
        {
            if (listaReporteRendimiento != null)
                rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporteRendimiento);
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the presentacion.
        /// </summary>
        private void CargarPresentacion()
        {
            CargarCombos();
            btnBuscar.Visible = true;
        }

        /// <summary>
        /// Buscars the calificaciones.
        /// </summary>
        private bool BuscarCalificaciones()
        {
            if (AccionPagina == enumAcciones.Buscar)
            {
                filtroReporte = new FilCalificacionesAlumnoPeriodo();
                StringBuilder filtros = new StringBuilder();

                List<Asignatura> listaAsignatura = new List<Asignatura>();
                foreach (System.Web.UI.WebControls.ListItem item in ddlAsignatura.Items)
                {
                    if (item.Selected)
                    {
                        if (!filtros.ToString().Contains("- Asignatura"))
                            filtros.AppendLine("- Asignatura");
                        filtros.AppendLine(" * " + item.Text);
                        listaAsignatura.Add(new Asignatura() { idAsignatura = Convert.ToInt16(item.Value) });
                    }
                }
                filtroReporte.listaAsignaturas = listaAsignatura;

                //if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0)
                //{
                //    filtroReporte.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
                //    filtros.AppendLine("- Ciclo Lectivo: " + ddlCicloLectivo.SelectedItem.Text);
                //}

                List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
                foreach (System.Web.UI.WebControls.ListItem item in ddlCicloLectivo.Items)
                {
                    if (item.Selected)
                    {
                        if (!filtros.ToString().Contains("- Ciclo Lectivo"))
                            filtros.AppendLine("- Ciclo Lectivo");
                        filtros.AppendLine(" * " + item.Text);
                        listaCicloLectivo.Add(new CicloLectivo() { idCicloLectivo = Convert.ToInt16(item.Value) });
                    }
                }
                filtroReporte.listaCicloLectivo = listaCicloLectivo;

                if (ddlNivel.Items.Count > 0 && Convert.ToInt32(ddlNivel.SelectedValue) > 0)
                {
                    filtroReporte.idNivel = Convert.ToInt32(ddlNivel.SelectedValue);
                    filtros.AppendLine("- Nivel: " + ddlNivel.SelectedItem.Text);
                }

                //if (Context.User.IsInRole(enumRoles.Docente.ToString()))
                //	filtroReporte.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

                BLRptCalificacionesAlumnoPeriodo objBLReporte = new BLRptCalificacionesAlumnoPeriodo();
                listaReporteRendimiento = objBLReporte.GetRptRendimientoHistorico(filtroReporte);
                filtrosAplicados = filtros.ToString();

                rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporteRendimiento);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Cargars the combos.
        /// </summary>
        private void CargarCombos()
        {
            //UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
            CargarComboCicloLectivo();
            CargarNiveles();
            CargarComboAsignatura();
        }

        private void CargarComboCicloLectivo()
        {
            ddlCicloLectivo.Items.Clear();

            BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();

            listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);

            // Ordena la lista alfabéticamente por la descripción
            listaCicloLectivo.Sort((p, q) => string.Compare(p.nombre, q.nombre));

            foreach (CicloLectivo cicloLectivo in listaCicloLectivo)
            {
                ddlCicloLectivo.Items.Add(new System.Web.UI.WebControls.ListItem(cicloLectivo.nombre, cicloLectivo.idCicloLectivo.ToString()));
            }

            if (ddlCicloLectivo.Items.Count > 0)
                ddlCicloLectivo.Disabled = false;
        }



        /// <summary>
        /// Cargars the combo asignatura.
        /// </summary>
        private void CargarComboAsignatura()
        {
            ddlAsignatura.Items.Clear();

            BLAsignatura objBLAsignatura = new BLAsignatura();

            int idNivel = Convert.ToInt32(ddlNivel.SelectedValue);
            //int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
            int idCicloLectivo = 0;

            listaAsignatura = objBLAsignatura.GetAsignaturasNivelCicloLectivo(idCicloLectivo, idNivel);

            // Ordena la lista alfabéticamente por la descripción
            listaAsignatura.Sort((p, q) => string.Compare(p.nombre, q.nombre));

            foreach (Asignatura asignatura in listaAsignatura)
            {
                ddlAsignatura.Items.Add(new System.Web.UI.WebControls.ListItem(asignatura.nombre, asignatura.idAsignatura.ToString()));
            }

            if (ddlAsignatura.Items.Count > 0)
                ddlAsignatura.Disabled = false;

        }

        /// <summary>
        /// Cargars the niveles.
        /// </summary>
        /// <param name="idCurso">The id curso.</param>
        private void CargarNiveles()
        {
            UIUtilidades.BindCombo<Nivel>(ddlNivel, listaNiveles, "idNivel", "nombre", true);

            ddlNivel.Enabled = true;
        }

        ///// <summary>
        ///// Generars the datos grafico.
        ///// </summary>
        private void GenerarDatosGrafico()
        {
            var cantAsignaturas =
                from p in listaReporteRendimiento
                group p by p.asignatura into g
                select new { Asignatura = g.Key, Cantidad = g.Count() };

            //TablaGrafico.Add("- Cantidad de Alumnos analizados: " + cantAlumnos.Count().ToString());

            //TablaGrafico.Add("- Registros Totales: " + listaReporte.Count.ToString());

            TablaGrafico = new List<TablaGrafico>();
            TablaGrafico tabla3 = new TablaGrafico();
            tabla3.listaCuerpo = new List<List<string>>();
            List<string> encabezado3 = new List<string>();
            List<string> fila3 = new List<string>();
            tabla3.titulo = "Periodos Analizados: \n";

            foreach (ListItem item in ddlCicloLectivo.Items)
            {
                if (item.Selected)
                {
                    tabla3.titulo += item.Value + "\n";
                }
            }

            tabla3.listaEncabezados = encabezado3;
            tabla3.listaCuerpo.Add(fila3);
            TablaGrafico.Add(tabla3);

            //if (!string.IsNullOrEmpty(ddlAsignatura.Value) && Convert.ToInt32(ddlAsignatura.Value) > 0)
            //{
            //    #region --[Recorrer Ciclos Lectivos Seleccionados]--
            //    var Ciclos = (from p in listaReporte
            //                  group p by p.ciclolectivo into g
            //                  //orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
            //                  select new { CicloLectivo = g.Key }).Distinct();

            //    foreach (var item in Ciclos)
            //    {
            //        TablaGrafico tabla2 = new TablaGrafico();
            //        tabla2.listaCuerpo = new List<List<string>>();
            //        List<string> encabezado2 = new List<string>();
            //        List<List<string>> filasTabla2 = new List<List<string>>();
            //        List<string> fila2 = new List<string>();

            //        tabla2.titulo = "Top 3 Materias de Mejor Desempeño " + item.CicloLectivo;
            //        encabezado2.Add("Asignatura");
            //        encabezado2.Add("Promedio");
            //        //encabezado2.Add("Ciclo Lectivo");
            //        var topPromedio =
            //           (from p in listaReporte
            //            where p.ciclolectivo == item.CicloLectivo
            //            group p by p.asignatura into g
            //            orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
            //            select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct().Take(3);

            //        //TablaGrafico.Add("- Top 3 Materias con mejor desempeño por Ciclo Lectivo:");
            //        foreach (var materia in topPromedio)
            //        {
            //            //TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString());
            //            fila2 = new List<string>();
            //            fila2.Add(materia.Asignatura);
            //            fila2.Add(materia.Promedio.ToString("#.00"));
            //            //fila2.Add(item.Text);
            //            filasTabla2.Add(fila2);
            //        }
            //        if (filasTabla2.Count > 0)
            //        {
            //            tabla2.listaEncabezados = encabezado2;
            //            tabla2.listaCuerpo = filasTabla2;
            //            TablaPropiaGrafico.Add(tabla2);
            //        }

            //        TablaGrafico tabla4 = new TablaGrafico();
            //        tabla4.listaCuerpo = new List<List<string>>();
            //        List<string> encabezado4 = new List<string>();
            //        List<List<string>> filasTabla4 = new List<List<string>>();
            //        List<string> fila4 = new List<string>();

            //        tabla4.titulo = "Top 3 Materias de Bajo Desempeño " + item.CicloLectivo;
            //        encabezado4.Add("Asignatura");
            //        encabezado4.Add("Promedio");
            //        //encabezado4.Add("Ciclo Lectivo");
            //        var lowPromedio =
            //           (from p in listaReporte
            //            where p.ciclolectivo == item.CicloLectivo
            //            group p by p.asignatura into g
            //            orderby g.Average(p => Convert.ToDouble(p.promedio)) ascending
            //            select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct().Take(3);

            //        //TablaGrafico.Add("- Top 3 Materias con mejor desempeño por Ciclo Lectivo:");
            //        foreach (var materia in lowPromedio)
            //        {
            //            //TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString());
            //            fila4 = new List<string>();
            //            fila4.Add(materia.Asignatura);
            //            fila4.Add(materia.Promedio.ToString("#.00"));
            //            //fila4.Add(item.Text);
            //            filasTabla4.Add(fila4);
            //        }
            //        if (filasTabla4.Count > 0)
            //        {
            //            tabla4.listaEncabezados = encabezado4;
            //            tabla4.listaCuerpo = filasTabla4;
            //            TablaPropiaGrafico.Add(tabla4);
            //        }
            //    }
            //    #endregion
            //}
            //else
            {
                TablaGrafico tabla2 = new TablaGrafico();
                tabla2.listaCuerpo = new List<List<string>>();
                List<string> encabezado2 = new List<string>();
                List<List<string>> filasTabla2 = new List<List<string>>();
                List<string> fila2 = new List<string>();

                var Ciclos =
                   (from p in listaReporteRendimiento
                    group p by p.ciclolectivo into g
                    //orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
                    select new { CicloLectivo = g.Key }).Distinct();

                var Asignaturas =
                   (from p in listaReporteRendimiento
                    group p by p.asignatura into g
                    //orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
                    select new { Asignatura = g.Key }).Distinct();

                tabla2.titulo = "Desempeño Por Ciclo Lectivo ";
                //encabezado2.Add("Promedio");
                encabezado2.Add("Asignatura");

                foreach (var item in Ciclos)
                {
                    encabezado2.Add(item.CicloLectivo);
                }

                foreach (var materia in Asignaturas)
                {
                    fila2 = new List<string>();
                    fila2.Add(materia.Asignatura);
                    foreach (var item in Ciclos)
                    {
                        //encabezado2.Add(item.CicloLectivo);

                        var Promedio =
                           (from p in listaReporteRendimiento
                            where p.ciclolectivo == item.CicloLectivo && p.asignatura == materia.Asignatura
                            group p by p.asignatura into g
                            //orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
                            select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct();

                        foreach (var itemPromedio in Promedio)
                        {
                            fila2.Add(itemPromedio.Promedio.ToString("#.00"));
                            //fila2.Add(item.Text);
                        }
                        if (Promedio.Count() == 0)
                            fila2.Add(string.Empty);
                    }
                    filasTabla2.Add(fila2);
                }
                if (filasTabla2.Count > 0)
                {
                    tabla2.listaEncabezados = encabezado2;
                    tabla2.listaCuerpo = filasTabla2;
                    TablaGrafico.Add(tabla2);
                }
            }
        }
        #endregion
    }
}