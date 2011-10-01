using System;
using System.Collections.Generic;
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

                if (!Page.IsPostBack)
                {
                    CargarPresentacion();
                    BLRptSancionesAlumnoPeriodo objBLRptSanciones = new BLRptSancionesAlumnoPeriodo();
                    objBLRptSanciones.GetRptSancionesAlumnoPeriodo(null);
                    divFiltros.Visible = true;
                    divReporte.Visible = false;
                }
                //BuscarSanciones();
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
        protected void ddlCurso_SelectedIndexChanged1(object sender, EventArgs e)
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

        private void CargarAlumnos(int idCurso)
        {
            BLAlumno objBLAlumno = new BLAlumno();
            UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", true);
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
        /// Buscars the sanciones.
        /// </summary>
        private bool BuscarSanciones()
        {
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
                
               // if (ddlAlumno.SelectedIndex > 1)
                if (ddlAlumno.SelectedIndex > 0)
                    filtroReporte.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);
                
                BLRptSancionesAlumnoPeriodo objBLReporte = new BLRptSancionesAlumnoPeriodo();
                listaReporte = objBLReporte.GetRptSancionesAlumnoPeriodo(filtroReporte);
                filtrosAplicados = filtros.ToString();

                rptSanciones.CargarReporte<RptSancionesAlumnoPeriodo>(listaReporte);
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
            List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
            BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
            listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);

            List<Curso> listaCurso = new List<Curso>();
            List<Alumno> listaAlumno = new List<Alumno>();
            UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
            UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);
            //UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "nombre", true);

            ddlCurso.Enabled = false;
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
                Curso objCurso = new Curso();

                listaCurso = objBLCicloLectivo.GetCursosByCicloLectivo(idCicloLectivo);
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