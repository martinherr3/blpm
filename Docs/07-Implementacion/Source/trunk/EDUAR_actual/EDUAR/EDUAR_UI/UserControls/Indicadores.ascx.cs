using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using System.Data;

namespace EDUAR_UI.UserControls
{
	public partial class Indicadores : UserControl
	{
		public string nombreIndicador;

		#region --[Propiedades]--
		//protected string nombreIndicador
		//{
		//    get
		//    {
		//        if (ViewState["nombreIndicador"] == null)
		//            ViewState["nombreIndicador"] = string.Empty;
		//        return ViewState["nombreIndicador"].ToString();
		//    }
		//    set { ViewState["nombreIndicador"] = value; }
		//}

		protected string idColumna
		{
			get
			{
				if (ViewState["idColumna"] == null)
					ViewState["idColumna"] = string.Empty;
				return ViewState["idColumna"].ToString();
			}
			set { ViewState["idColumna"] = value; }
		}

		/// <summary>
		/// Gets or sets the dt reporte.
		/// </summary>
		/// <value>
		/// The dt reporte.
		/// </value>
		public DataTable dtDatosIndicador
		{
			get
			{
				if (ViewState["dtDatosIndicador_" + nombreIndicador] == null)
					ViewState["dtDatosIndicador_" + nombreIndicador] = new DataTable();
				return (DataTable)ViewState["dtDatosIndicador_" + nombreIndicador];
			}
			set { ViewState["dtDatosIndicador_" + nombreIndicador] = value; }
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			pnlContenidos.ID = nombreIndicador;

			BLContenido objBLContenidos = new BLContenido();
			EDUAR_Entities.Contenido objFiltro = new EDUAR_Entities.Contenido();
			objFiltro.asignaturaCicloLectivo = new AsignaturaCicloLectivo();
			objFiltro.asignaturaCicloLectivo.idAsignaturaCicloLectivo = 2817;
			List<EDUAR_Entities.Contenido> listaContenidos = objBLContenidos.GetByAsignaturaCicloLectivo(objFiltro);

			// Since the reader implements IEnumerable, pass the reader directly into
			//   the DataBind method with the name of the Column selected in the query    
			Chart1.Series["Sales"].Points.DataBindXY(listaContenidos, "descripcion", listaContenidos, "idContenido");

			UpdateAttrib();
			if (!Page.IsPostBack)
			{

			}
			else
			{
				if (Request.Form["__EVENTTARGET"] == "btnPanel")
				{
					//llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Button2
					//EventArgs evento = new EventArgs();
					idColumna = Request.Form["__EVENTARGUMENT"].ToString();
					btnPanel_Click(this, new EventArgs());
				}
			}
		}

		public void UpdateAttrib()
		{

			// Set series tooltips
			foreach (Series series in Chart1.Series)
			{
				for (int pointIndex = 0; pointIndex < series.Points.Count; pointIndex++)
				{
					//string toolTip = "";

					//toolTip = "<img src=RegionChart.aspx?region=" + series.Points[pointIndex].AxisLabel + " />";
					//toolTip = "<img src=RegionChart.aspx />";
					//series.Points[pointIndex].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip + "');\" onmouseout=\"DisplayTooltip('');\"";
					//series.Points[pointIndex].Url = "DetailedRegionChart.aspx?region=" + series.Points[pointIndex].AxisLabel;


					//TODO: setear algún valor en algún campo oculto x javascript y luego click del boton?
					series.Points[pointIndex].MapAreaAttributes = "onclick=\"panelGraficoAuxiliar('" + series.Points[pointIndex].YValues[0].ToString() + "');\" style=\"cursor: pointer\"";
					series.Points[pointIndex].ToolTip = "Click para más información";
				}
			}

		}
		protected void btnPanel_Click(object sender, EventArgs e)
		{
			string local = idColumna;
			BLContenido objBLContenidos = new BLContenido();
			EDUAR_Entities.Contenido objFiltro = new EDUAR_Entities.Contenido();
			objFiltro.asignaturaCicloLectivo = new AsignaturaCicloLectivo();
			objFiltro.asignaturaCicloLectivo.idAsignaturaCicloLectivo = 2817;
			List<EDUAR_Entities.Contenido> listaContenidos = objBLContenidos.GetByAsignaturaCicloLectivo(objFiltro);

			// Since the reader implements IEnumerable, pass the reader directly into
			//   the DataBind method with the name of the Column selected in the query    
			Chart2.Series["Sales"].Points.DataBindXY(listaContenidos, "descripcion", listaContenidos, "idContenido");

			this.mpeContenido.Show();
		}

		/// <summary>
		/// Handles the Click event of the btnVolverPopUp control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnVolverPopUp_Click(object sender, EventArgs e)
		{
			this.mpeContenido.Hide();
		}
	}
}