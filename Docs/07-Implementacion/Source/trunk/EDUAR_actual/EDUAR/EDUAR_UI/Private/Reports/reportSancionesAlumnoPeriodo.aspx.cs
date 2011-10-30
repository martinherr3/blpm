using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_BusinessLogic.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
    public partial class ReportSancionesAlumnoPeriodo : EDUARBasePage
    {
        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the filtro sanciones.
        /// </summary>
        /// <value>
        /// The filtro sanciones.
        /// </value>
        public FilSancionesAlumnoPeriodo filtroReporte
        {
            get
            {
                if (ViewState["filtroSanciones"] == null)
                    filtroReporte = new FilSancionesAlumnoPeriodo();
                return (FilSancionesAlumnoPeriodo)ViewState["filtroSanciones"];
            }
            set
            {
                ViewState["filtroSanciones"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the lista sanciones.
        /// </summary>
        /// <value>
        /// The lista sanciones.
        /// </value>
        public List<RptSancionesAlumnoPeriodo> listaReporte
        {
            get
            {
                if (Session["listaSanciones"] == null)
                    listaReporte = new List<RptSancionesAlumnoPeriodo>();
                return (List<RptSancionesAlumnoPeriodo>)Session["listaSanciones"];
            }
            set
            {
                Session["listaSanciones"] = value;
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
        /// Gets or sets the lista tipo sancion.
        /// </summary>
        /// <value>
        /// The lista tipo sancion.
        /// </value>
        public List<TipoSancion> listaTipoSancion
        {
            get
            {
                if (ViewState["listaTipoSancion"] == null)
                {
                    listaTipoSancion = new List<TipoSancion>();
                    BLTipoSancion objBLTipoSancion = new BLTipoSancion();
                    listaTipoSancion = objBLTipoSancion.GetTipoSancion(null);
                }
                return (List<TipoSancion>)ViewState["listaTipoSancion"];
            }
            set
            {
                ViewState["listaTipoSancion"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the lista motivo sancion.
        /// </summary>
        /// <value>
        /// The lista motivo sancion.
        /// </value>
        public List<MotivoSancion> listaMotivoSancion
        {
            get
            {
                if (ViewState["listaMotivoSancion"] == null)
                {
                    listaMotivoSancion = new List<MotivoSancion>();
                    BLMotivoSancion objBLMotivoSancion = new BLMotivoSancion();
                    listaMotivoSancion = objBLMotivoSancion.GetMotivoSanciones(null);
                }
                return (List<MotivoSancion>)ViewState["listaMotivoSancion"];
            }
            set
            {
                ViewState["listaMotivoSancion"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the lista ciclo lectivo.
        /// </summary>
        /// <value>
        /// The lista ciclo lectivo.
        /// </value>
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
                rptSanciones.ExportarPDFClick += (ExportarPDF);
                rptSanciones.VolverClick += (VolverReporte);
                rptSanciones.PaginarGrilla += (PaginarGrilla);
                Master.BotonAvisoAceptar += (VentanaAceptar);
                rptSanciones.GraficarClick += (btnGraficar);

                if (!Page.IsPostBack)
                {
                    TablaGrafico = null;
                    CargarPresentacion();
                    BLRptSancionesAlumnoPeriodo objBLRptSanciones = new BLRptSancionesAlumnoPeriodo();
                    //objBLRptSanciones.GetRptSancionesAlumnoPeriodo(null);
                    divFiltros.Visible = true;
                    divReporte.Visible = false;
                }
                if (listaReporte != null)
                    rptSanciones.CargarReporte<RptSancionesAlumnoPeriodo>(listaReporte);
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
                fechas.ValidarRangoDesdeHasta(false);
                string mensaje = ValidarPagina();
                if (mensaje == string.Empty)
                {
                    if (BuscarSanciones())
                    {
                        divFiltros.Visible = false;
                        divReporte.Visible = true;
                    }
                    else
                    { Master.MostrarMensaje("Faltan Datos", UIConstantesGenerales.MensajeDatosRequeridos, EDUAR_Utility.Enumeraciones.enumTipoVentanaInformacion.Advertencia); }
                }
                else
                { Master.MostrarMensaje("Faltan Datos", UIConstantesGenerales.MensajeDatosFaltantes + mensaje, EDUAR_Utility.Enumeraciones.enumTipoVentanaInformacion.Advertencia); }
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
                ExportPDF.ExportarPDF(Page.Title, rptSanciones.dtReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
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
                divFiltros.Visible = true;
                divReporte.Visible = false;
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

                if (rptSanciones.GrillaReporte.PageCount > pagina)
                {
                    rptSanciones.GrillaReporte.PageIndex = pagina;

                    rptSanciones.CargarReporte<RptSancionesAlumnoPeriodo>(listaReporte);
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCicloLectivo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlCicloLectivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
                if (idCicloLectivo > 0)
                {
                    fechas.startDate = listaCicloLectivo.Find(p => p.idCicloLectivo == idCicloLectivo).fechaInicio;
                    fechas.endDate = listaCicloLectivo.Find(p => p.idCicloLectivo == idCicloLectivo).fechaFin;
                    CargarComboCursos(idCicloLectivo, ddlCurso);
                    ddlAlumno.Items.Clear();
                    ddlAlumno.Enabled = false;
                }
                else
                {
                    if (ddlCurso.Items.Count > 0)
                    {
                        ddlCurso.Items.Clear();
                        ddlCurso.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCurso control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
                CargarAlumnos(Convert.ToInt32(ddlCurso.SelectedValue));
                ddlAlumno.Enabled = true;
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
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
                GenerarDatosGraficoSanciones();
                rptSanciones.graficoReporte.LimpiarSeries();
                var serie = new List<RptSancionesAlumnoPeriodo>();
                int cantidad = 0;
                if (ddlAlumno.SelectedIndex > 0)
                {
                    var listaParcial = listaReporte.FindAll(p => p.alumno == ddlAlumno.SelectedItem.Text);

                    foreach (var item in listaTipoSancion)
                    {
                        cantidad = 0;
                        var listaPorTipoSancion = listaParcial.FindAll(p => p.tipo == item.nombre);
                        if (listaPorTipoSancion.Count > 0)
                        {
                            
                            foreach (var sancion in listaPorTipoSancion)
                            {
                                cantidad += sancion.cantidad;
                            }
                            serie.Add(new RptSancionesAlumnoPeriodo
                            {
                                tipo = item.nombre,
                                cantidad = cantidad
                            });
                        }
                    }
                    if (serie != null)
                    {
                        DataTable dt = UIUtilidades.BuildDataTable<RptSancionesAlumnoPeriodo>(serie);
                        // En cantidad envio la cantidad de sanciones y en tipo la sancion
                        rptSanciones.graficoReporte.AgregarSerie(ddlAlumno.SelectedItem.Text, dt, "tipo", "cantidad");
                        rptSanciones.graficoReporte.Titulo = "Sanciones " + ddlAlumno.SelectedItem.Text;
                    }

                    string escala = BLConfiguracionGlobal.ObtenerConfiguracion(EDUAR_Utility.Enumeraciones.enumConfiguraciones.LimiteInasistencias);

                    List<ValoresEscalaCalificacion> listaEscala = new List<ValoresEscalaCalificacion>();
                    foreach (RptSancionesAlumnoPeriodo item in serie)
                    {
                        listaEscala.Add(new ValoresEscalaCalificacion
                        {
                            valor = escala,
                            nombre = item.tipo
                        });
                    }
                    DataTable dtEscala = UIUtilidades.BuildDataTable<ValoresEscalaCalificacion>(listaEscala);
                    rptSanciones.graficoReporte.AgregarSerie("Nivel de Expulsión (" + escala + ")", dtEscala, "nombre", "valor", true);
                }
                else
                {
                    foreach (var item in listaTipoSancion)
                    {
                        var listaPorTipoSancion = listaReporte.FindAll(p => p.tipo == item.nombre);

                        if (listaPorTipoSancion.Count > 0)
                        {
                            RptSancionesAlumnoPeriodo oneSerie = new RptSancionesAlumnoPeriodo();
                            oneSerie.cantidad = 0;
                            foreach (var item2 in listaPorTipoSancion)
                            {
                                oneSerie.tipo = item.nombre;
                                oneSerie.cantidad += item2.cantidad;
                            }

                            serie.Add(oneSerie);

                        }
                    }
                    DataTable dt = UIUtilidades.BuildDataTable<RptSancionesAlumnoPeriodo>(serie);
                    rptSanciones.graficoReporte.AgregarSerie("Sanciones", dt, "tipo", "cantidad");

                    rptSanciones.graficoReporte.Titulo = "Sanciones";

                    if (fechas.ValorFechaDesde != null
                        && fechas.ValorFechaHasta != null)
                        rptSanciones.graficoReporte.Titulo = @"Sanciones " + ((DateTime)fechas.ValorFechaDesde).ToShortDateString() +
                             " - " + ((DateTime)fechas.ValorFechaHasta).ToShortDateString();
                    else
                        if (fechas.ValorFechaDesde != null
                            && fechas.ValorFechaHasta == null)
                            rptSanciones.graficoReporte.Titulo = @"Sanciones desde el " + ((DateTime)fechas.ValorFechaDesde).ToShortDateString();
                        else
                            if (fechas.ValorFechaDesde == null
                            && fechas.ValorFechaHasta != null)
                                rptSanciones.graficoReporte.Titulo = @"Sanciones hasta el " + ((DateTime)fechas.ValorFechaHasta).ToShortDateString();
                }
                rptSanciones.graficoReporte.GraficarBarra();
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }


        }

        /// <summary>
        /// Generars the datos grafico.
        /// </summary>
        private void GenerarDatosGraficoSanciones()
        {
            var cantAlumnos =
                from p in listaReporte
                group p by p.alumno into g
                select new { Alumno = g.Key, Cantidad = g.Count() };

            var fechaMin =
               from p in listaReporte
               group p by p.alumno into g
               select new { Alumno = g.Key, Fecha = g.Min(p => p.fecha) };

            var fechaMax =
               from p in listaReporte
               group p by p.alumno into g
               select new { Alumno = g.Key, Fecha = g.Max(p => p.fecha) };

            TablaGrafico = new List<TablaGrafico>();
            TablaGrafico tabla3 = new TablaGrafico();
            tabla3.listaCuerpo = new List<List<string>>();
            List<string> encabezado3 = new List<string>();
            List<string> fila3 = new List<string>();

            tabla3.titulo = "Periodo Analizado: " + fechaMin.First().Fecha.ToShortDateString() + " - " + fechaMax.First().Fecha.ToShortDateString();
            encabezado3.Add("Cantidad de Alumnos");
            fila3.Add(cantAlumnos.Count().ToString());
            tabla3.listaEncabezados = encabezado3;
            tabla3.listaCuerpo.Add(fila3);
            TablaGrafico.Add(tabla3);

            // Calcular Promedio y Desviacion Standard por tipo de Sanciones
			//TablaGrafico.Add("- Desviacion Estandar por tipo de Sanciones: ");
            TablaGrafico tabla2 = new TablaGrafico();
            tabla2.listaCuerpo = new List<List<string>>();
            List<string> encabezado2 = new List<string>();
            List<List<string>> filasTabla2 = new List<List<string>>();
            List<string> fila2 = new List<string>();

            tabla2.titulo = "Desviacion Estandar por Tipo de Sanción";
            encabezado2.Add("Alumno");
            encabezado2.Add("Promedio de Sanciones por Ocurrencia");
            encabezado2.Add("Desviación");

            double sumaSanciones, promedio, desvStd, dif, cociente, sumaDifCuad = 0;

            var serie = new List<RptCalificacionesAlumnoPeriodo>();
            foreach (var item in listaTipoSancion)
            {
                promedio = 0;
                cociente = 0;
                desvStd = 0;
                sumaSanciones = 0;
                dif = 0;
                sumaDifCuad = 0;

                var listaParcial = listaReporte.FindAll(p => p.tipo == item.nombre);
                if (listaParcial.Count > 0)
                {
                    foreach (var sancion in listaParcial)
                    {
                        sumaSanciones += Convert.ToInt32(sancion.cantidad);
                    }
                    promedio = sumaSanciones / listaParcial.Count;
                    foreach (var sancion in listaParcial)
                    {
                        dif = (Convert.ToInt32(sancion.cantidad) - promedio);
                        sumaDifCuad += Math.Pow(dif, 2);
                    }
                    // cociente = (sumaDifCuad / (listaParcial.Count - 1));
                    cociente = (sumaDifCuad / (listaParcial.Count));

                    desvStd = Math.Sqrt(cociente);
                    //TablaGrafico.Add(item.nombre + " Promedio: " + promedio.ToString("#.##") + " , Desviacion Standard: " + desvStd.ToString("#.##"));
                    fila2 = new List<string>();
                    fila2.Add(item.nombre);
                    fila2.Add(promedio.ToString("#.##"));
                    fila2.Add(desvStd.ToString("#.##"));
                    filasTabla2.Add(fila2);
                }
            }
            tabla2.listaEncabezados = encabezado2;
            tabla2.listaCuerpo = filasTabla2;
            TablaGrafico.Add(tabla2);

            var worstAlumnos =
                 (from p in listaReporte
                  group p by p.alumno into g
                  orderby g.Count() descending
                  select new { Alumno = g.Key, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);

            if (worstAlumnos.Count() > 1)
            {
                TablaGrafico tabla4 = new TablaGrafico();
                tabla4.listaCuerpo = new List<List<string>>();
                List<string> encabezado4 = new List<string>();
                List<List<string>> filasTabla4 = new List<List<string>>();
                List<string> fila4 = new List<string>();
                //TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad de Sanciones:");
                tabla4.titulo = "Top Alumnos a observar por Cantidad de Sanciones";
                encabezado4.Add("Alumno");
                encabezado4.Add("Sanciones");

                foreach (var item in worstAlumnos)
                {
                    //TablaGrafico.Add("Alumno: " + item.Alumno + " - Cantidad de Sanciones: " + item.Sanciones);
                    fila4 = new List<string>();
                    fila4.Add(item.Alumno);
                    fila4.Add(item.Sanciones.ToString());
                    filasTabla4.Add(fila4);
                }
                tabla4.listaEncabezados = encabezado4;
                tabla4.listaCuerpo = filasTabla4;
                TablaGrafico.Add(tabla4);
            }
            var SancionesPorTipo =
                  (from p in listaReporte
                   group p by p.tipo into g
                   orderby g.Count() descending
                   select new { Tipo = g.Key, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);

            //TablaGrafico.Add("- Cantidad de Sanciones según Tipo:");
            TablaGrafico tabla5 = new TablaGrafico();
            tabla5.listaCuerpo = new List<List<string>>();
            List<string> encabezado5 = new List<string>();
            List<List<string>> filasTabla5 = new List<List<string>>();
            List<string> fila5 = new List<string>();

            tabla5.titulo = "Cantidad de Sanciones según Tipo";
            encabezado5.Add("Motivo");
            encabezado5.Add("Cantidad");

            foreach (var item in SancionesPorTipo)
            {
                //TablaGrafico.Add("Tipo de Sancion: " + item.Tipo + " - Cantidad de Sanciones: " + item.Sanciones);
                fila5 = new List<string>();
                fila5.Add(item.Tipo);
                fila5.Add(item.Sanciones.ToString());
                filasTabla5.Add(fila5);
            }
            tabla5.listaEncabezados = encabezado5;
            tabla5.listaCuerpo = filasTabla5;
            TablaGrafico.Add(tabla5);

            var SancionesPorMotivo =
                  (from p in listaReporte
                   group p by p.motivo into g
                   orderby g.Count() descending
                   select new { Motivo = g.Key, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);

            TablaGrafico tabla6 = new TablaGrafico();
            tabla6.listaCuerpo = new List<List<string>>();
            List<string> encabezado6 = new List<string>();
            List<List<string>> filasTabla6 = new List<List<string>>();
            List<string> fila6 = new List<string>();

            tabla6.titulo = "Cantidad de Sanciones según Motivo";
            encabezado6.Add("Motivo");
            encabezado6.Add("Cantidad");
            //TablaGrafico.Add("- Cantidad de Sanciones según Motivo:");
            foreach (var item in SancionesPorMotivo)
            {
                //TablaGrafico.Add("Motivo de Sancion: " + item.Motivo + " - Cantidad de Sanciones: " + item.Sanciones);
                fila6 = new List<string>();
                fila6.Add(item.Motivo);
                fila6.Add(item.Sanciones.ToString());
                filasTabla6.Add(fila6);
            }
            tabla6.listaEncabezados = encabezado6;
            tabla6.listaCuerpo = filasTabla6;
            TablaGrafico.Add(tabla6);

            var worstAlumnosByMotivo = (from p in listaReporte
                                        group p by new { p.alumno, p.motivo } into g
                                        orderby g.Sum(p => p.cantidad) descending
                                        select new { Alumno = g.Key.alumno, Motivo = g.Key.motivo, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);

            if (worstAlumnosByMotivo.Count() > 1)
            {
                //TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Motivo de Sanciones:");
                TablaGrafico tabla7 = new TablaGrafico();
                tabla7.listaCuerpo = new List<List<string>>();
                List<string> encabezado7 = new List<string>();
                List<List<string>> filasTabla7 = new List<List<string>>();
                List<string> fila7 = new List<string>();

                tabla7.titulo = "Top Alumnos a observar por Cantidad y Motivo de Sanciones";
                encabezado7.Add("Alumno");
                encabezado7.Add("Motivo");
                encabezado7.Add("Cantidad");
                foreach (var item in worstAlumnosByMotivo)
                {
                    //TablaGrafico.Add("Alumno: " + item.Alumno + " Motivo: " + item.Motivo + " - Cantidad de Sanciones: " + item.Sanciones);
                    fila7 = new List<string>();
                    fila7.Add(item.Alumno);
                    fila7.Add(item.Motivo);
                    fila7.Add(item.Sanciones.ToString());
                    filasTabla7.Add(fila7);
                }
                tabla7.listaEncabezados = encabezado7;
                tabla7.listaCuerpo = filasTabla7;
                TablaGrafico.Add(tabla7);
            }

            var worstAlumnosByTipo = (from p in listaReporte
                                      group p by new { p.alumno, p.tipo } into g
                                      orderby g.Sum(p => p.cantidad) descending
                                      select new { Alumno = g.Key.alumno, Tipo = g.Key.tipo, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);

            if (worstAlumnosByTipo.Count() > 1)
            {
                //TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Tipo de Sanciones:");
                TablaGrafico tabla8 = new TablaGrafico();
                tabla8.listaCuerpo = new List<List<string>>();
                List<string> encabezado8 = new List<string>();
                List<List<string>> filasTabla8 = new List<List<string>>();
                List<string> fila8 = new List<string>();

                tabla8.titulo = "Top Alumnos a observar por Cantidad y Tipo de Sanciones";
                encabezado8.Add("Alumno");
                encabezado8.Add("Motivo");
                encabezado8.Add("Cantidad");
                foreach (var item in worstAlumnosByTipo)
                {
                    //TablaGrafico.Add("Alumno: " + item.Alumno + " Tipo: " + item.Tipo + " - Cantidad de Sanciones: " + item.Sanciones);
                    fila8 = new List<string>();
                    fila8.Add(item.Alumno);
                    fila8.Add(item.Tipo);
                    fila8.Add(item.Sanciones.ToString());
                    filasTabla8.Add(fila8);
                }
                tabla8.listaEncabezados = encabezado8;
                tabla8.listaCuerpo = filasTabla8;
                TablaGrafico.Add(tabla8);
            }
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
        /// Cargars the alumnos.
        /// </summary>
        /// <param name="idCurso">The id curso.</param>
        private void CargarAlumnos(int idCurso)
        {
            BLAlumno objBLAlumno = new BLAlumno();
            UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", "nombre", true);
            ddlAlumno.Enabled = true;
        }

        /// <summary>
        /// Buscars the sanciones.
        /// </summary>
        private bool BuscarSanciones()
        {
            if (Page.IsPostBack)
            {
                filtroReporte = new FilSancionesAlumnoPeriodo();
                StringBuilder filtros = new StringBuilder();
                if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 /*&& Convert.ToInt32(ddlCurso.SelectedValue) > 0*/)
                {
                    filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text);

                    if (Convert.ToInt32(ddlCurso.SelectedValue) > 0)
                        filtros.AppendLine(" - Curso: " + ddlCurso.SelectedItem.Text);

                    if (fechas.ValorFechaDesde != null)
                    {
                        filtros.AppendLine("- Fecha Desde: " + ((DateTime)fechas.ValorFechaDesde).ToShortDateString());
                        filtroReporte.fechaDesde = (DateTime)fechas.ValorFechaDesde;
                    }

                    if (fechas.ValorFechaHasta != null)
                    {
                        filtros.AppendLine("- Fecha Hasta: " + ((DateTime)fechas.ValorFechaHasta).ToShortDateString());
                        filtroReporte.fechaHasta = (DateTime)fechas.ValorFechaHasta;
                    }

                    filtroReporte.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);

                    filtroReporte.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

                    if (ddlAlumno.SelectedIndex > 0)
                    {
                        filtroReporte.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);
                        filtros.AppendLine("- Alumno: " + ddlAlumno.SelectedItem.Text);
                    }

                    #region --[Tipo Sanción]--
                    List<TipoSancion> listaTipoSancionSelect = new List<TipoSancion>();
                    foreach (System.Web.UI.WebControls.ListItem item in ddlTipoSancion.Items)
                    {
                        if (item.Selected)
                        {
                            if (!filtros.ToString().Contains("- Tipo de Sanción"))
                                filtros.AppendLine("- Tipo de Sanción");
                            filtros.AppendLine(" * " + item.Text);
                            listaTipoSancionSelect.Add(new TipoSancion() { idTipoSancion = Convert.ToInt16(item.Value) });
                        }
                    }
                    filtroReporte.listaTipoSancion = listaTipoSancionSelect;
                    #endregion

                    #region --[Motivo Sanción]--
                    List<MotivoSancion> listaMotivoSancionSelect = new List<MotivoSancion>();
                    foreach (System.Web.UI.WebControls.ListItem item in ddlMotivoSancion.Items)
                    {
                        if (item.Selected)
                        {
                            if (!filtros.ToString().Contains("- Motivo de Sanción"))
                                filtros.AppendLine("- Motivo de Sanción");
                            filtros.AppendLine(" * " + item.Text);
                            listaMotivoSancionSelect.Add(new MotivoSancion() { idMotivoSancion = Convert.ToInt16(item.Value) });
                        }
                    }
                    filtroReporte.listaMotivoSancion = listaMotivoSancionSelect;
                    #endregion

                    if (Context.User.IsInRole(enumRoles.Docente.ToString()))
                        filtroReporte.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

                    BLRptSancionesAlumnoPeriodo objBLReporte = new BLRptSancionesAlumnoPeriodo();
                    listaReporte = objBLReporte.GetRptSancionesAlumnoPeriodo(filtroReporte);
                    filtrosAplicados = filtros.ToString();

                    rptSanciones.CargarReporte<RptSancionesAlumnoPeriodo>(listaReporte);
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Cargars the combos.
        /// </summary>
        private void CargarCombos()
        {
            List<Curso> listaCurso = new List<Curso>();
            UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
            UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);

            if (ddlCicloLectivo.Items.Count > 0)
            {
                ddlCicloLectivo.SelectedIndex = ddlCicloLectivo.Items.Count - 1;
                fechas.startDate = listaCicloLectivo.Find(p => p.idCicloLectivo == Convert.ToInt16(ddlCicloLectivo.SelectedValue)).fechaInicio;
                fechas.endDate = listaCicloLectivo.Find(p => p.idCicloLectivo == Convert.ToInt16(ddlCicloLectivo.SelectedValue)).fechaFin;
                CargarComboCursos(Convert.ToInt16(ddlCicloLectivo.SelectedValue), ddlCurso);
                ddlCurso.Enabled = true;
                ddlCurso.SelectedIndex = -1;
            }

            #region --[Tipo Sanción]--
            // Ordena la lista alfabéticamente por la descripción
            listaTipoSancion.Sort((p, q) => string.Compare(p.nombre, q.nombre));

            // Carga el combo de tipo de asistencia para filtrar
            foreach (TipoSancion item in listaTipoSancion)
            {
                ddlTipoSancion.Items.Add(new System.Web.UI.WebControls.ListItem(item.nombre, item.idTipoSancion.ToString()));
            }
            #endregion

            #region --[Motivo Sanción]--
            // Ordena la lista alfabéticamente por la descripción
            listaMotivoSancion.Sort((p, q) => string.Compare(p.descripcion, q.descripcion));

            // Carga el combo de tipo de asistencia para filtrar
            foreach (MotivoSancion item in listaMotivoSancion)
            {
                ddlMotivoSancion.Items.Add(new System.Web.UI.WebControls.ListItem(item.descripcion, item.idMotivoSancion.ToString()));
            }
            #endregion

            ddlAlumno.Enabled = false;
        }

        /// <summary>
        /// Cargars the combo cursos.
        /// </summary>
        /// <param name="idCicloLectivo">The id ciclo lectivo.</param>
        /// <param name="ddlCurso">The DDL curso.</param>
        private void CargarComboCursos(int idCicloLectivo, DropDownList ddlCurso)
        {
            if (idCicloLectivo > 0)
            {
                List<Curso> listaCurso = new List<Curso>();
                BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
                Asignatura objAsignatura = new Asignatura();
                objAsignatura.curso.cicloLectivo.idCicloLectivo = idCicloLectivo;

                if (User.IsInRole(enumRoles.Docente.ToString()))
                    objAsignatura.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

                listaCurso = objBLCicloLectivo.GetCursosByAsignatura(objAsignatura);
                UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "nombre", true);
                ddlCurso.Enabled = true;
            }
            else
            {
                ddlCurso.Enabled = false;
            }
        }

        /// <summary>
        /// Validars the pagina.
        /// </summary>
        /// <returns></returns>
        private string ValidarPagina()
        {
            string mensaje = string.Empty;

            if (string.IsNullOrEmpty(ddlCicloLectivo.SelectedValue) || Convert.ToInt32(ddlCicloLectivo.SelectedValue) <= 0)
                mensaje = "- Ciclo Lectivo<br />";
            //if (string.IsNullOrEmpty(ddlCurso.SelectedValue) || Convert.ToInt32(ddlCurso.SelectedValue) <= 0)
            //    mensaje += "- Curso<br />";
            return mensaje;
        }
        #endregion
    }
}