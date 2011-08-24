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
		public List<RptCalificacionesAlumnoPeriodo> listaReporte
		{
			get
			{
				if (ViewState["listaCalificaciones"] == null)
                    listaReporte = new List<RptCalificacionesAlumnoPeriodo>();
				return (List<RptCalificacionesAlumnoPeriodo>)ViewState["listaCalificaciones"];
			}
			set
			{
				ViewState["listaCalificaciones"] = value;
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
                if (ViewState["filtrosCalificacion"] == null)
                    filtrosAplicados = string.Empty;
                return ViewState["filtrosCalificacion"].ToString();
            }
            set
            {
                ViewState["filtrosCalificacion"] = value;
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
                //BuscarCalificaciones();
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
                ExportPDF.ExportarPDF(Page.Title, rptCalificaciones.dtReporte, ObjDTSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
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
            StringBuilder filtros = new StringBuilder();
			filtroReporte.idAsignatura = Convert.ToInt32(ddlAsignatura.SelectedValue);
            if (filtroReporte.idAsignatura > 0) filtros.AppendLine("- Asignatura: " + ddlAsignatura.SelectedItem.Text);
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

			BLRptCalificacionesAlumnoPeriodo objBLReporte = new BLRptCalificacionesAlumnoPeriodo();
			listaReporte = objBLReporte.GetRptCalificacionesAlumnoPeriodo(filtroReporte);
            filtrosAplicados = filtros.ToString();

			rptCalificaciones.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);
		}
		#endregion
	}
}