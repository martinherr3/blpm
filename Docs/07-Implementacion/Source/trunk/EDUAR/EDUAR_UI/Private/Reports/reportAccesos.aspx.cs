using System;
using System.Web.UI;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using EDUAR_Entities.Reports;
using EDUAR_BusinessLogic.Reports;

namespace EDUAR_UI
{
    public partial class reportAccesos : EDUARBasePage
    {
        #region --[Propiedades]--
        public string rutaReporte { get; set; }
        public List<RptAccesos> listaReporte { get; set; }
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
                    CargarComboPagina();

                    BLRptAccesos objBLAcceso = new BLRptAccesos();
                    objBLAcceso.GetRptAccesos(null);
                }
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
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
                BuscarAccesos();
                udpReporte.Update();
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        protected void rptAccesos_OnDrillthrough(object sender, DrillthroughEventArgs e)
        {
            udpReporte.Update();
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the combo pagina.
        /// </summary>
        private void CargarComboPagina()
        {
            BLPagina objBLPagina = new BLPagina();
            UIUtilidades.BindCombo<Pagina>(ddlPagina, objBLPagina.GetPaginas(null), "idPagina", "titulo", true);
        }

        /// <summary>
        /// Buscars the accesos.
        /// </summary>
        private void BuscarAccesos()
        {
            Acceso filtroAcceso = new Acceso();
            filtroAcceso.pagina.idPagina = Convert.ToInt32(ddlPagina.SelectedValue);
            BLRptAccesos objBLAcceso = new BLRptAccesos();
            Reportes("rptAccesos.rdlc", objBLAcceso.GetRptAccesos(filtroAcceso));

            this.rptAccesos.ProcessingMode = ProcessingMode.Local;
            this.rptAccesos.LocalReport.ReportPath = rutaReporte;

            ReportDataSource datos = new ReportDataSource();
            datos.Name = "dsAccesos";
            datos.Value = this.listaReporte;

            this.rptAccesos.LocalReport.DataSources.Clear();
            this.rptAccesos.LocalReport.DataSources.Add(datos);
            this.rptAccesos.LocalReport.Refresh();
        }
        #endregion

        #region --[Métodos Privados]--
        private void Reportes(string reporte, List<RptAccesos> lista)
        {
            rutaReporte = Server.MapPath("~/Private/Reports/" + reporte);
            listaReporte = lista;
        }
        #endregion
    }
}