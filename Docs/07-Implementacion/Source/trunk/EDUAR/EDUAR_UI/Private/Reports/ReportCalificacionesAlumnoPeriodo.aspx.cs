using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities.Reports;
using EDUAR_BusinessLogic.Reports;
using Microsoft.Reporting.WebForms;

namespace EDUAR_UI
{
    public partial class ReportCalificacionesAlumnoPeriodo : EDUARBasePage
    {
        #region --[Propiedades]--
        public string rutaReporte { get; set; }
        public List<RptCalificacionesAlumnoPeriodo> listaReporte { get; set; }
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
                if (!Page.IsPostBack)
                {
                    CargarPresentacion();
                }
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

                //udpReporte.Update();
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
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
            RptCalificacionesAlumnoPeriodo filtroReporte = new RptCalificacionesAlumnoPeriodo();
            filtroReporte.idAlumno = Convert.ToInt32(ddlAsignatura.SelectedValue);
            if (fechas.ValorFechaDesde != null)
                filtroReporte.fechaDesde = (DateTime)fechas.ValorFechaDesde;
            if (fechas.ValorFechaHasta != null)
                filtroReporte.fechaHasta = (DateTime)fechas.ValorFechaHasta;

            BLRptCalificacionesAlumnoPeriodo objBLReporte = new BLRptCalificacionesAlumnoPeriodo();
            Reportes("RptCalificacionesAlumnoPeriodo.rdlc", objBLReporte.GetRptCalificacionesAlumnoPeriodo(filtroReporte));

            this.rptReporte.ProcessingMode = ProcessingMode.Local;
            this.rptReporte.LocalReport.ReportPath = rutaReporte;

            ReportDataSource datos = new ReportDataSource();
            datos.Name = "dsCalificaciones";
            datos.Value = this.listaReporte;

            this.rptReporte.LocalReport.DataSources.Clear();
            this.rptReporte.LocalReport.DataSources.Add(datos);
            this.rptReporte.LocalReport.Refresh();
        }

        /// <summary>
        /// Reporteses the specified reporte.
        /// </summary>
        /// <param name="reporte">The reporte.</param>
        /// <param name="lista">The lista.</param>
        private void Reportes(string reporte, List<RptCalificacionesAlumnoPeriodo> lista)
        {
            rutaReporte = Server.MapPath("~/Private/Reports/" + reporte);
            listaReporte = lista;
        }
        #endregion
    }
}