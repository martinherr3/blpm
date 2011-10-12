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
    public partial class ReportInasistenciasAlumnoPeriodo : EDUARBasePage
    {
        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the filtro inasistencias.
        /// </summary>
        /// <value>
        /// The filtro inasistencias.
        /// </value>
        public FilInasistenciasAlumnoPeriodo filtroReporte
        {
            get
            {
                if (ViewState["filtroInasistencias"] == null)
                    filtroReporte = new FilInasistenciasAlumnoPeriodo();
                return (FilInasistenciasAlumnoPeriodo)ViewState["filtroInasistencias"];
            }
            set
            {
                ViewState["filtroInasistencias"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the lista inasistencias.
        /// </summary>
        /// <value>
        /// The lista inasistencias.
        /// </value>
        public List<RptInasistenciasAlumnoPeriodo> listaReporte
        {
            get
            {
                if (Session["listaInasistencias"] == null)
                    listaReporte = new List<RptInasistenciasAlumnoPeriodo>();
                return (List<RptInasistenciasAlumnoPeriodo>)Session["listaInasistencias"];
            }
            set
            {
                Session["listaInasistencias"] = value;
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
        /// Gets or sets the lista tipo asistencia.
        /// </summary>
        /// <value>
        /// The lista tipo asistencia.
        /// </value>
        public List<TipoAsistencia> listaTipoAsistencia
        {
            get
            {
                if (ViewState["listaTipoAsistencia"] == null)
                {
                    listaTipoAsistencia = new List<TipoAsistencia>();
                    BLTipoAsistencia objBLTipoAsistencia = new BLTipoAsistencia();
                    listaTipoAsistencia = objBLTipoAsistencia.GetTipoAsistencia(null);
                }
                return (List<TipoAsistencia>)ViewState["listaTipoAsistencia"];
            }
            set
            {
                ViewState["listaTipoAsistencia"] = value;
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
                rptInasistencias.ExportarPDFClick += (ExportarPDF);
                rptInasistencias.VolverClick += (VolverReporte);
                rptInasistencias.PaginarGrilla += (PaginarGrilla);
                Master.BotonAvisoAceptar += (VentanaAceptar);
                rptInasistencias.GraficarClick += (btnGraficar);

                if (!Page.IsPostBack)
                {
                    CargarPresentacion();
                    //BLRptInasistenciasAlumnoPeriodo objBLRptInasistencias = new BLRptInasistenciasAlumnoPeriodo();
                    //objBLRptInasistencias.GetRptInasistenciasAlumnoPeriodo(null);
                    divFiltros.Visible = true;
                    divReporte.Visible = false;
                }
                //BuscarInasistencias();
                if (listaReporte != null)
                    rptInasistencias.CargarReporte<RptInasistenciasAlumnoPeriodo>(listaReporte);
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
                if (BuscarInasistencias())
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
                ExportPDF.ExportarPDF(Page.Title, rptInasistencias.dtReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
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
        /// BTNs the graficar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnGraficar(object sender, EventArgs e)
        {
            try
            {
                GenerarDatosGraficoInasistencias();
                rptInasistencias.graficoReporte.LimpiarSeries();
                var serie = new List<RptInasistenciasAlumnoPeriodo>();
                if (ddlAlumno.SelectedIndex > 0)
                {
                    var listaParcial = listaReporte.FindAll(p => p.alumno == ddlAlumno.SelectedItem.Text);

                    foreach (var item in listaTipoAsistencia)
                    {
                        var listaPorTipoAsistencia = listaParcial.FindAll(p => p.motivo == item.descripcion);
                        if (listaPorTipoAsistencia.Count > 0)
                        {
                            serie.Add(new RptInasistenciasAlumnoPeriodo
                            {
                                motivo = item.descripcion,
                                alumno = listaPorTipoAsistencia.Count.ToString()
                            });
                        }
                    }
                    if (serie != null)
                    {
                        DataTable dt = UIUtilidades.BuildDataTable<RptInasistenciasAlumnoPeriodo>(serie);
                        // En alumno envio la nota y en calificación la cantidad de esa nota que se produjo
                        rptInasistencias.graficoReporte.AgregarSerie(ddlAlumno.SelectedItem.Text, dt, "motivo", "alumno");
                        rptInasistencias.graficoReporte.Titulo = "Inasistencias " + ddlAlumno.SelectedItem.Text;
                    }
                }
                else
                {
                    foreach (var item in listaTipoAsistencia)
                    {
                        var listaPorTipoAsistencia = listaReporte.FindAll(p => p.motivo == item.descripcion);
                        if (listaPorTipoAsistencia.Count > 0)
                        {
                            serie.Add(new RptInasistenciasAlumnoPeriodo
                            {
                                motivo = item.descripcion,
                                alumno = listaPorTipoAsistencia.Count.ToString()
                            });
                        }
                    }
                    DataTable dt = UIUtilidades.BuildDataTable<RptInasistenciasAlumnoPeriodo>(serie);
                    rptInasistencias.graficoReporte.AgregarSerie("Inasistencias", dt, "motivo", "alumno");

                    if (fechas.ValorFechaDesde != null
                        && fechas.ValorFechaHasta != null)
                        rptInasistencias.graficoReporte.Titulo = @"Inasistencias " + ((DateTime)fechas.ValorFechaDesde).ToShortDateString() +
                             " - " + ((DateTime)fechas.ValorFechaHasta).ToShortDateString();
                    else
                        rptInasistencias.graficoReporte.Titulo = "Inasistencias";
                }
                rptInasistencias.graficoReporte.GraficarBarra();
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

                if (rptInasistencias.GrillaReporte.PageCount > pagina)
                {
                    rptInasistencias.GrillaReporte.PageIndex = pagina;

                    rptInasistencias.CargarReporte<RptInasistenciasAlumnoPeriodo>(listaReporte);
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
        /// Cargars the alumnos.
        /// </summary>
        /// <param name="idCurso">The id curso.</param>
        private void CargarAlumnos(int idCurso)
        {
            BLAlumno objBLAlumno = new BLAlumno();
            UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", "nombre", true);
            ddlAlumno.Enabled = true;
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
        /// Buscars the inasistencias.
        /// </summary>
        private bool BuscarInasistencias()
        {
            if (Page.IsPostBack)
            {
                filtroReporte = new FilInasistenciasAlumnoPeriodo();
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

                    //Sólo si selecciona el ciclo lectivo
                    if (ddlCicloLectivo.SelectedIndex > 0)
                    {
                        filtroReporte.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
                    }

                    //Sólo si selecciona el curso
                    if (ddlCurso.SelectedIndex > 0)
                    {
                        filtroReporte.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);
                    }

                    //Sólo si selecciona un alumno
                    if (ddlAlumno.SelectedIndex > 0)
                    {
                        filtroReporte.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);
                        filtros.AppendLine("- Alumno: " + ddlAlumno.SelectedItem.Text);
                    }

                    if (Context.User.IsInRole(enumRoles.Docente.ToString()))
                        filtroReporte.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

                    BLRptInasistenciasAlumnoPeriodo objBLReporte = new BLRptInasistenciasAlumnoPeriodo();
                    listaReporte = objBLReporte.GetRptInasistenciasAlumnoPeriodo(filtroReporte);
                    filtrosAplicados = filtros.ToString();

                    rptInasistencias.CargarReporte<RptInasistenciasAlumnoPeriodo>(listaReporte);
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

        /// <summary>
        /// Generars the datos grafico.
        /// </summary>
        private void GenerarDatosGraficoInasistencias()
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
                  select new { Alumno = g.Key, Faltas = g.Count() }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad de Inasistencias:");
            foreach (var item in worstAlumnos)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " - Cantidad de Inasistencias: " + item.Faltas);
            }

            var FaltasPorMotivo =
                  (from p in listaReporte
                   group p by p.motivo into g
                   orderby g.Count() descending
                   select new { Motivo = g.Key, Faltas = g.Count() }).Distinct().Take(3);

            TablaGrafico.Add("- Cantidad de Inasistencias según Motivo:");
            foreach (var item in FaltasPorMotivo)
            {
                TablaGrafico.Add("Motivo de Ausencia: " + item.Motivo + " - Cantidad de Ocurrencias: " + item.Faltas);
            }

            var worstAlumnosByMotivo =
            (from p in listaReporte
             group p by new { p.alumno, p.motivo } into g
             orderby g.Count() descending
             select new { Alumno = g.Key.alumno, Motivo = g.Key.motivo, Faltas = g.Count() }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Motivo de Inasistencias:");
            foreach (var item in worstAlumnosByMotivo)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " Motivo: " + item.Motivo + " - Cantidad de Inasistencias: " + item.Faltas);
            }
        }

        #endregion
    }
}