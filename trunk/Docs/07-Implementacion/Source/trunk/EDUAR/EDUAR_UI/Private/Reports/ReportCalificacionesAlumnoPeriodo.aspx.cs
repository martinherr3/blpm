using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Utilidades;

namespace EDUAR_UI
{
    public partial class ReportCalificacionesAlumnoPeriodo : EDUARBasePage
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
                    ViewState["filtroCalificaciones"] = new FilCalificacionesAlumnoPeriodo();
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
        public List<RptCalificacionesAlumnoPeriodo> listaReporte
        {
            get
            {
                if (ViewState["listaCalificaciones"] == null)
                    ViewState["listaCalificaciones"] = new List<RptCalificacionesAlumnoPeriodo>();
                return (List<RptCalificacionesAlumnoPeriodo>)ViewState["listaCalificaciones"];
            }
            set
            {
                ViewState["listaCalificaciones"] = value;
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
                rptCalificaciones.ExportarPDFClick += (ExportarPDF);
                rptCalificaciones.VolverClick += (VolverReporte);
                rptCalificaciones.PaginarGrilla += (PaginarGrilla);

                if (!Page.IsPostBack)
                {
                    CargarPresentacion();
                    BLRptCalificacionesAlumnoPeriodo objBLRptCalificaciones = new BLRptCalificacionesAlumnoPeriodo();
                    objBLRptCalificaciones.GetRptCalificacionesAlumnoPeriodo(null);
                    divFiltros.Visible = true;
                    divReporte.Visible = false;
                }
                BuscarCalificaciones();
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
            }
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
                BuscarCalificaciones();
                divFiltros.Visible = false;
                divReporte.Visible = true;
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
                ExportPDF.ExportarPDF(Page.Title, rptCalificaciones.dtReporte);
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

                if (rptCalificaciones.GrillaReporte.PageCount > pagina)
                {
                    rptCalificaciones.GrillaReporte.PageIndex = pagina;

                    rptCalificaciones.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        #endregion

        #region --[Métodos Privados]--
        private void CargarPresentacion()
        {
            BLAsignatura objBLAsignatura = new BLAsignatura();

            UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, objBLAsignatura.GetAsignaturas(null), "idAsignatura", "nombre", true);
        }

        private void BuscarCalificaciones()
        {
            filtroReporte.idAsignatura = Convert.ToInt32(ddlAsignatura.SelectedValue);
            if (fechas.ValorFechaDesde != null)
                filtroReporte.fechaDesde = (DateTime)fechas.ValorFechaDesde;
            if (fechas.ValorFechaHasta != null)
                filtroReporte.fechaHasta = (DateTime)fechas.ValorFechaHasta;

            BLRptCalificacionesAlumnoPeriodo objBLReporte = new BLRptCalificacionesAlumnoPeriodo();
            listaReporte = objBLReporte.GetRptCalificacionesAlumnoPeriodo(filtroReporte);

            rptCalificaciones.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);
        }
        #endregion
    }
}