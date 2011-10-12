using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using System.Linq;

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
                fechas.ValidarRangoDesdeHasta();
                if (BuscarSanciones())
                {
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
                CargarComboCursos(idCicloLectivo, ddlCurso);
                ddlAlumno.Items.Clear();
                ddlAlumno.Enabled = false;
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
                if (ddlAlumno.SelectedIndex > 0)
                {
                    var listaParcial = listaReporte.FindAll(p => p.alumno == ddlAlumno.SelectedItem.Text);

                    foreach (var item in listaTipoSancion)
                    {
                        var listaPorTipoSancion = listaParcial.FindAll(p => p.tipo == item.nombre);
                        if (listaPorTipoSancion.Count > 0)
                        {
                            serie.Add(new RptSancionesAlumnoPeriodo
                            {
                                tipo = item.nombre,
                                cantidad = listaPorTipoSancion.Count
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

            TablaGrafico.Add("- Cantidad de Alumnos analizados: " + cantAlumnos.Count().ToString());

            var fechaMin =
               from p in listaReporte
               group p by p.alumno into g
               select new { Alumno = g.Key, Fecha = g.Min(p => p.fecha) };

            var fechaMax =
               from p in listaReporte
               group p by p.alumno into g
               select new { Alumno = g.Key, Fecha = g.Max(p => p.fecha) };

            TablaGrafico.Add("- Periodo de notas: " + fechaMin.First().Fecha.ToShortDateString() + " - " + fechaMax.First().Fecha.ToShortDateString());

            var worstAlumnos =
                 (from p in listaReporte
                  group p by p.alumno into g
                  orderby g.Count() descending
                  select new { Alumno = g.Key, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);


            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad de Sanciones:");

            foreach (var item in worstAlumnos)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " - Cantidad de Sanciones: " + item.Sanciones);
            }

            var SancionesPorTipo =
                  (from p in listaReporte
                   group p by p.tipo into g
                   orderby g.Count() descending
                   select new { Tipo = g.Key, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);

            TablaGrafico.Add("- Cantidad de Sanciones según Tipo:");
            foreach (var item in SancionesPorTipo)
            {
                TablaGrafico.Add("Tipo de Sancion: " + item.Tipo + " - Cantidad de Sanciones: " + item.Sanciones);
            }

            var SancionesPorMotivo =
                  (from p in listaReporte
                   group p by p.motivo into g
                   orderby g.Count() descending
                   select new { Motivo = g.Key, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);

            TablaGrafico.Add("- Cantidad de Sanciones según Motivo:");
            foreach (var item in SancionesPorMotivo)
            {
                TablaGrafico.Add("Motivo de Sancion: " + item.Motivo + " - Cantidad de Sanciones: " + item.Sanciones);
            }


            var worstAlumnosByMotivo =
            (from p in listaReporte
             group p by new { p.alumno, p.motivo } into g
             orderby g.Sum(p => p.cantidad) descending
             select new { Alumno = g.Key.alumno, Motivo = g.Key.motivo, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Motivo de Sanciones:");
            foreach (var item in worstAlumnosByMotivo)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " Motivo: " + item.Motivo + " - Cantidad de Sanciones: " + item.Sanciones);
            }


            var worstAlumnosByTipo =
            (from p in listaReporte
             group p by new { p.alumno, p.tipo } into g
             orderby g.Sum(p => p.cantidad) descending
             select new { Alumno = g.Key.alumno, Tipo = g.Key.tipo, Sanciones = g.Sum(p => p.cantidad) }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Tipo de Sanciones:");
            foreach (var item in worstAlumnosByTipo)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " Tipo: " + item.Tipo + " - Cantidad de Sanciones: " + item.Sanciones);
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
                if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlCurso.SelectedValue) > 0)
                {
                    filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);
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
            List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
            BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
            listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);

            List<Curso> listaCurso = new List<Curso>();
            UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
            UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);

            if (ddlCicloLectivo.Items.Count > 0)
            {
                ddlCicloLectivo.SelectedIndex = ddlCicloLectivo.Items.Count - 1;
                CargarComboCursos(Convert.ToInt16(ddlCicloLectivo.SelectedValue), ddlCurso);
                ddlCurso.Enabled = true;
                ddlCurso.SelectedIndex = -1;
            }

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
        #endregion
    }
}