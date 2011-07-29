using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using EDUAR_Utility.Utilidades;
using System.Web.UI.WebControls;

namespace EDUAR_UI
{
    public partial class reportAccesos : EDUARBasePage
    {
        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the filtro acceso.
        /// </summary>
        /// <value>
        /// The filtro acceso.
        /// </value>
        public FilAccesos filtroAcceso
        {
            get
            {
                if (ViewState["filtroAcceso"] == null)
                    ViewState["filtroAcceso"] = new FilAccesos();
                return (FilAccesos)ViewState["filtroAcceso"];
            }
            set
            {
                ViewState["filtroAcceso"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the lista acceso.
        /// </summary>
        /// <value>
        /// The lista acceso.
        /// </value>
        public List<RptAccesos> listaAcceso
        {
            get
            {
                if (ViewState["listaAcceso"] == null)
                    ViewState["listaAcceso"] = new List<RptAccesos>();
                return (List<RptAccesos>)ViewState["listaAcceso"];
            }
            set
            {
                ViewState["listaAcceso"] = value;
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
                rptAccesos.ExportarPDFClick += (ExportarPDF);
                rptAccesos.VolverClick += (VolverReporte);
                rptAccesos.PaginarGrilla += (PaginarGrilla);

                if (!Page.IsPostBack)
                {
                    CargarPresentacion();
                    BLRptAccesos objBLAcceso = new BLRptAccesos();
                    objBLAcceso.GetRptAccesos(null);
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
        /// Handles the Click event of the btnBuscar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                fechas.ValidarRangoDesdeHasta();
                BuscarAccesos();
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
                EDUARExportPDF.ExportarPDF(Page.Title, rptAccesos.dtReporte);
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

                if (rptAccesos.GrillaReporte.PageCount > pagina)
                {
                    rptAccesos.GrillaReporte.PageIndex = pagina;

                    rptAccesos.CargarReporte<RptAccesos>(listaAcceso);
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
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
            filtroAcceso.idPagina = Convert.ToInt32(ddlPagina.SelectedValue);
            if (fechas.ValorFechaDesde != null)
                filtroAcceso.fechaDesde = (DateTime)fechas.ValorFechaDesde;
            if (fechas.ValorFechaHasta != null)
                filtroAcceso.fechaHasta = (DateTime)fechas.ValorFechaHasta;

            List<DTRol> ListaRoles = new List<DTRol>();
            foreach (System.Web.UI.WebControls.ListItem item in chkListRolesBusqueda.Items)
            {
                if (item.Selected)
                {
                    ListaRoles.Add(new DTRol() { Nombre = item.Value });
                }
            }
            filtroAcceso.listaRoles = ListaRoles;

            BLRptAccesos objBLReporte = new BLRptAccesos();
            listaAcceso = objBLReporte.GetRptAccesos(filtroAcceso);

            rptAccesos.CargarReporte<RptAccesos>(listaAcceso);
        }

        /// <summary>
        /// Cargars the presentacion.
        /// </summary>
        private void CargarPresentacion()
        {
            CargarComboPagina();
            CargarListRoles();
        }

        /// <summary>
        /// Cargars the list roles.
        /// </summary>
        private void CargarListRoles()
        {
            DTSeguridad objSeguridad = new DTSeguridad();
            BLSeguridad objBLSeguridad = new BLSeguridad(objSeguridad);

            objBLSeguridad.GetRoles();
            foreach (DTRol rol in objBLSeguridad.Data.ListaRoles)
            {
                chkListRolesBusqueda.Items.Add(new System.Web.UI.WebControls.ListItem(rol.Nombre, rol.NombreCorto));
            }
        }
        #endregion
    }
}