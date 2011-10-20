using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using EDUAR_UI.Utilidades;
using EDUAR_Entities.Shared;
using iTextSharp.text.pdf;

namespace EDUAR_UI.UserControls
{
	public partial class Grafico : UserControl
	{
		#region --[Atributos]--
		private bool _LegendOutside;
		private bool _IsListado;
		private int numeroAleatorio;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Para mostrar la leyenda fuera de del grafico de torta.
		/// </summary>
		public bool LegendOutside
		{
			get
			{
				if (ViewState["_LegendOutside"] != null)
					_LegendOutside = (bool)ViewState["_LegendOutside"];
				return _LegendOutside;
			}
			set { ViewState["_LegendOutside"] = value; _LegendOutside = value; }
		}

		/// <summary>
		/// Gets or sets the titulo.
		/// </summary>
		/// <value>
		/// The titulo.
		/// </value>
		public string Titulo
		{
			get
			{
				if (Chart1.Titles.Count > 0)
					return Chart1.Titles[0].Text.ToString();
				else
				{
					if (ViewState["_Titulo"] != null)
					{
						Chart1.Titles.Clear();
						Title t = new Title(ViewState["_Titulo"].ToString(), Docking.Top, new System.Drawing.Font("Helvetica Neue", 16, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black);
						Chart1.Titles.Add(t);
					}
				}
				return "";
			}
			set
			{
				ViewState["_Titulo"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista series.
		/// </summary>
		/// <value>
		/// The lista series.
		/// </value>
		public List<MiSerie> ListaSeries
		{
			get
			{
				if (ViewState["_Series"] == null)
					ListaSeries = new List<MiSerie>();
				return (List<MiSerie>)ViewState["_Series"];
			}
			set { ViewState["_Series"] = value; }
		}

		/// <summary>
		/// Utilizo la clase para manejar las series del gráfico
		/// </summary>
		[Serializable]
		public class MiSerie
		{
			public DataTable Datos;
			public string ColumnaValor;
			public string ColumnaNombre;
			public string NombreSerie;

			public MiSerie()
			{ }
		}

		/// <summary>
		/// Nombre del gráfico que se genera en el servidor para la session
		/// </summary>
		public string nombrePNG
		{
			get
			{
				if (Session["nombrePNG"] == null)
					Session["nombrePNG"] = string.Empty;
				return Session["nombrePNG"].ToString();
			}

			set { Session["nombrePNG"] = value; }
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
				return Session["filtrosAplicados"].ToString();
			}
		}

		/// <summary>
		/// Mantiene los datos del usuario logueado.
		/// </summary>
		public DTSessionDataUI ObjSessionDataUI
		{
			get
			{
				return (DTSessionDataUI)Session["ObjSessionDataUI"];
			}
		}

		/// <summary>
		/// Gets or sets the titulo reporte.
		/// </summary>
		/// <value>
		/// The titulo reporte.
		/// </value>
		public string tituloReporte
		{
			get
			{
				return Session["tituloReporte"].ToString();
			}
		}

		/// <summary>
		/// Contiene información detallada sobre el gráfico que puede generarse
		/// </summary>
		public List<string> TablaGrafico
		{
			get
			{
				if (Session["TablaGrafico1"] == null)
					TablaGrafico = new List<string>();
				return (List<string>)Session["TablaGrafico1"];
			}
			set
			{
				Session["TablaGrafico1"] = value;
			}
		}

		public List<TablaGrafico> TablaPropiaGrafico
		{
			get
			{
				if (Session["TablaGrafico"] == null)
					TablaPropiaGrafico = new List<TablaGrafico>();
				return (List<TablaGrafico>)Session["TablaGrafico"];
			}
			set
			{
				Session["TablaGrafico"] = value;
			}
		}

        public bool habilitarTorta
        {
            get
            {
                if (Session["habilitarTorta"] == null)
                    habilitarTorta = true;
                return (bool)Session["habilitarTorta"];
            }
            set
            {
                Session["habilitarTorta"] = value;
                btnTorta.Visible = value;
            }
        }
		#endregion

		#region --[Eventos]--
		/// <summary>
		/// Genera el evento <see cref="E:System.Web.UI.Control.PreRender"/>.
		/// </summary>
		/// <param name="e">Objeto <see cref="T:System.EventArgs"/> que contiene los datos del evento.</param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (ListaSeries != null)
			{
				if (_IsListado)
				{
					divGrafico.Visible = false;
				}
				else
				{
					divGrafico.Visible = true;
				}
				//divNoHayDatos.Visible = false;
			}
			else
			{
				divGrafico.Visible = false;
				//divNoHayDatos.Visible = true;
			}
		}

		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			string TmpPath = System.Configuration.ConfigurationManager.AppSettings["oTmpPath"];
			//Crea el directorio.
			if (!System.IO.Directory.Exists(TmpPath))
				System.IO.Directory.CreateDirectory(TmpPath);
			numeroAleatorio = DateTime.Now.Millisecond;
		}

		/// <summary>
		/// Handles the Click event of the btnLineal control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnLineal_Click(object sender, EventArgs e)
		{
			//divGrafico.Attributes.Add("class", "divGraficoOcultar");
			//TDGrafico.Attributes.Add("class", "divGraficoOcultar");
			Chart1.Series.Clear();
			GraficarLinea();
		}

		/// <summary>
		/// Handles the Click event of the btnBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnBar_Click(object sender, EventArgs e)
		{
			//divGrafico.Attributes.Add("class", "divGraficoOcultar");
			//TDGrafico.Attributes.Add("class", "divGraficoOcultar");
			Chart1.Series.Clear();
			GraficarBarra();
		}

		/// <summary>
		/// Handles the Click event of the btnTorta control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnTorta_Click(object sender, EventArgs e)
		{
			//divGrafico.Attributes.Add("class", "divGraficoOcultar");
			//TDGrafico.Attributes.Add("class", "divGraficoOcultar");
			Chart1.Series.Clear();
			GraficarTorta3D();
		}

		//protected void btnRadar_Click(object sender, EventArgs e)
		//{
		//    divGrafico.Attributes.Add("class", "divGraficoOcultar");
		//    TDGrafico.Attributes.Add("class", "divGraficoOcultar");
		//    Chart1.Series.Clear();
		//    GraficarRadar();
		//}

		/// <summary>
		/// Handles the Click event of the btnCerrar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnCerrar_Click(object sender, EventArgs e)
		{
			divGrafico.Attributes.Add("class", "divGraficoOcultar");
			TDGrafico.Attributes.Add("class", "divGraficoOcultar");
			TDBotonera.Attributes.Add("class", "BotoneraGraficoOcultar");
			Chart1.Series.Clear();
		}

		/// <summary>
		/// Handles the Click event of the btnExportar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnExportar_Click(object sender, EventArgs e)
		{
			if (TablaPropiaGrafico.Count > 0)
				ExportPDF.ExportarGraficoPDF(tituloReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados, nombrePNG, TablaPropiaGrafico);
			else
				ExportPDF.ExportarGraficoPDF(tituloReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados, nombrePNG, TablaGrafico);
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Agregars the serie.
		/// </summary>
		/// <param name="serie">The serie.</param>
		/// <param name="dt">The dt.</param>
		/// <param name="ColumnaNombre">The columna nombre.</param>
		/// <param name="ColumnaValor">The columna valor.</param>
		public void AgregarSerie(string serie, DataTable dt, string ColumnaNombre, string ColumnaValor)
		{
			MiSerie serieNueva = new MiSerie();
			serieNueva.NombreSerie = serie;
			serieNueva.Datos = dt;
			serieNueva.ColumnaNombre = ColumnaNombre;
			serieNueva.ColumnaValor = ColumnaValor;
			ListaSeries.Add(serieNueva);
		}

		/// <summary>
		/// Grafica de barras.
		/// </summary>
		public void GraficarBarra()
		{
			foreach (MiSerie item in ListaSeries)
			{
				Chart1.Series.Add(item.NombreSerie);
				Chart1.Series[item.NombreSerie].Color = GetRandomColor();
				Chart1.Series[item.NombreSerie].BackGradientStyle = GradientStyle.DiagonalLeft;
				Chart1.Series[item.NombreSerie].ShadowOffset = 2;
				Chart1.Series[item.NombreSerie].ToolTip = "#VALX: #VALY";

				// Set series chart type
				Chart1.Series[item.NombreSerie].ChartType = SeriesChartType.Column;

				// Set series point width
				Chart1.Series[item.NombreSerie]["PointWidth"] = "1.0";

				// Show data points labels
				//Chart1.Series[item.NombreSerie].IsValueShownAsLabel = false;

				Chart1.Series[item.NombreSerie].IsValueShownAsLabel = true;
				Chart1.Series[item.NombreSerie].IsVisibleInLegend = true;

				// Set data points label style
				Chart1.Series[item.NombreSerie]["BarLabelStyle"] = "Center";

				// Draw as 3D Cylinder
				Chart1.Series[item.NombreSerie]["DrawingStyle"] = "Emboss";
			}

			// Disable X axis margin
			//Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
			Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

			Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
			Graficar();
		}

		/// <summary>
		/// Grafica lineal.
		/// </summary>
		public void GraficarLinea()
		{
			foreach (MiSerie item in ListaSeries)
			{
				Chart1.Series.Add(item.NombreSerie);
				Chart1.Series[item.NombreSerie].Color = GetRandomColor();
				Chart1.Series[item.NombreSerie].BorderWidth = 4;
				Chart1.Series[item.NombreSerie].ShadowOffset = 2;
				Chart1.Series[item.NombreSerie].IsVisibleInLegend = true;
				Chart1.Series[item.NombreSerie].ToolTip = "#VALX : #VALY";

				// Set series chart type
				Chart1.Series[item.NombreSerie].ChartType = SeriesChartType.Line;

				// Set point labels
				Chart1.Series[item.NombreSerie].IsValueShownAsLabel = false;

				// Enable 3D, and show data point marker lines
				Chart1.Series[item.NombreSerie]["ShowMarkerLines"] = "True";
			}
			Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
			// Enable X axis margin
			Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

			Graficar();
		}

		/// <summary>
		/// Grafica una torta 3D.
		/// </summary>
		public void GraficarTorta3D()
		{
			foreach (MiSerie item in ListaSeries)
			{
				Chart1.Series.Add(item.NombreSerie);
				Chart1.Series[item.NombreSerie].ChartType = SeriesChartType.Pie;
				Chart1.Series[item.NombreSerie].ShadowOffset = 2;
				Chart1.Series[item.NombreSerie].ToolTip = "#VALX : #PERCENT";

				if (LegendOutside)
				{
					Chart1.Series[item.NombreSerie]["PieLabelStyle"] = "Outside";
					Chart1.Series[item.NombreSerie].Label = "#VALX";
				}
				else
					Chart1.Series[item.NombreSerie].Label = " ";
				Chart1.Series[item.NombreSerie].LegendText = "#VALX : #PERCENT";

				// Set the threshold type to be in percentage
				// When set to false, this property uses the actual value to determine the collected threshold
				//Chart1.Series[item.NombreSerie]["CollectedThresholdUsePercent"] = "true";

				// Set the label of the collected pie slice
				Chart1.Series[item.NombreSerie]["CollectedLabel"] = "Otros";

				// Set the legend text of the collected pie slice
				Chart1.Series[item.NombreSerie]["CollectedLegendText"] = "Otros";

				// Set the collected pie slice to be exploded
				Chart1.Series[item.NombreSerie]["CollectedSliceExploded"] = "true";

				// Set the color of the collected pie slice
				Chart1.Series[item.NombreSerie]["CollectedColor"] = "Grey";

				// Set the tooltip of the collected pie slice
				Chart1.Series[item.NombreSerie]["CollectedToolTip"] = "Otros";

			}
			Graficar();

			if (Chart1.Legends.Count > 0)
				Chart1.Legends[0].Alignment = StringAlignment.Center;

		}

		/// <summary>
		/// Grafica una torta 3D.
		/// </summary>
		public void GraficarRadar()
		{
			foreach (MiSerie item in ListaSeries)
			{
				Chart1.Series.Add(item.NombreSerie);
				Chart1.Series[item.NombreSerie].ChartType = SeriesChartType.Radar;
				Chart1.Series[item.NombreSerie].Color = GetRandomColor();
				Chart1.Series[item.NombreSerie].ToolTip = "#VALX : #PERCENT";

				// Set radar chart style (Area, Line or Marker)
				Chart1.Series[item.NombreSerie]["RadarDrawingStyle"] = "Area";

				// Set circular area drawing style (Circle or Polygon)
				Chart1.Series[item.NombreSerie]["AreaDrawingStyle"] = "Polygon";

				// Set labels style (Auto, Horizontal, Circular or Radial)
				Chart1.Series[item.NombreSerie]["CircularLabelsStyle"] = "Horizontal";

				Chart1.Series[item.NombreSerie].IsValueShownAsLabel = false;

				Chart1.Series[item.NombreSerie].IsVisibleInLegend = true;
			}
			Graficar();

			Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
		}

		/// <summary>
		/// Muestra un listado de la tabla cargada.
		/// </summary>
		public void Listar()
		{
			_IsListado = true;
		}

		/// <summary>
		/// Limpiars the series.
		/// </summary>
		public void LimpiarSeries()
		{
			Chart1.Series.Clear();
			ListaSeries.Clear();
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Graficars this instance.
		/// </summary>
		private void Graficar()
		{
			foreach (MiSerie item in ListaSeries)
			{
				Chart1.Series[item.NombreSerie].Points.DataBind(item.Datos.DefaultView, item.ColumnaNombre, item.ColumnaValor, "");
			}
			Chart1.Titles.Add(Titulo);
			btnTorta.Visible = habilitarTorta;
			if (ListaSeries.Count > 1) btnTorta.Visible = false;

			_IsListado = false;
			//divGrafico.Visible = true;
			divGrafico.Attributes.Add("class", "divGraficoMostrar");
			TDGrafico.Attributes.Add("class", "divGraficoMostrar");
			TDGrafico.Visible = true;
			TDBotonera.Attributes.Add("class", "BotoneraGraficoMostrar");

			string TmpPath = System.Configuration.ConfigurationManager.AppSettings["oTmpPath"];
			nombrePNG = TmpPath + "\\Grafico_" + Session.SessionID + ".png";
			Chart1.SaveImage(nombrePNG, ChartImageFormat.Png);
		}

		/// <summary>
		/// Gets the random color.
		/// </summary>
		/// <returns></returns>
		private Color GetRandomColor()
		{
			Random aleatorioRojo = new Random(DateTime.Now.Millisecond + 2 * numeroAleatorio);
			numeroAleatorio = aleatorioRojo.Next(0, 255) * numeroAleatorio + DateTime.Now.Millisecond + DateTime.Now.Year;
			Random aleatorioVerde = new Random((DateTime.Now.Millisecond + 5) * numeroAleatorio * DateTime.Now.Second);
			Random aleatorioAzul = new Random((DateTime.Now.Millisecond + 3) * DateTime.Now.Year * DateTime.Now.Millisecond);
			return Color.FromArgb(aleatorioRojo.Next(0, 255), aleatorioVerde.Next(0, 255), aleatorioAzul.Next(0, 255));
		}
		#endregion
	}
}