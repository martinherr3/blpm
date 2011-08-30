using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI.UserControls
{
    public partial class Grafico : UserControl
    {
        private bool _LegendOutside;
        private bool _IsListado;
        private int numeroAleatorio;

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

        [Serializable]
        public class MiSerie
        {
            public DataTable Datos;
            public string ColumnaValor;
            public string ColumnaNombre;
            public string NombreSerie;

            public MiSerie()
            { }

            ~MiSerie()
            { }
        }

        public Chart ChartTesis
        {
            get { return Chart1; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            numeroAleatorio = DateTime.Now.Millisecond;
        }

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

        private void Graficar()
        {
            foreach (MiSerie item in ListaSeries)
            {
                Chart1.Series[item.NombreSerie].Points.DataBind(item.Datos.DefaultView, item.ColumnaNombre, item.ColumnaValor, "");
            }
            Chart1.Titles.Add(Titulo);
            btnTorta.Visible = true;
            if (ListaSeries.Count > 1) btnTorta.Visible = false;

            _IsListado = false;
            //divGrafico.Visible = true;
            divGrafico.Attributes.Add("class", "divGraficoMostrar");
            TDGrafico.Attributes.Add("class", "divGraficoMostrar");
            TDBotonera.Attributes.Add("class", "BotoneraGraficoMostrar");
        }

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
                Chart1.Series[item.NombreSerie].ToolTip = "#VALX : #VALY";

                // Set series chart type
                Chart1.Series[item.NombreSerie].ChartType = SeriesChartType.Column;

                // Set series point width
                Chart1.Series[item.NombreSerie]["PointWidth"] = "1.0";

                // Show data points labels
                Chart1.Series[item.NombreSerie].IsValueShownAsLabel = false;

                // Set data points label style
                Chart1.Series[item.NombreSerie]["BarLabelStyle"] = "Center";

                // Draw as 3D Cylinder
                Chart1.Series[item.NombreSerie]["DrawingStyle"] = "Emboss";
            }

            // Disable X axis margin
            Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
            // Show as 3D
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
                Chart1.Series[item.NombreSerie].ChartType = SeriesChartType.Spline;

                // Set point labels
                Chart1.Series[item.NombreSerie].IsValueShownAsLabel = false ;

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

        public void LimpiarSeries()
        {
            Chart1.Series.Clear();
            ListaSeries.Clear();
        }

        protected void btnLineal_Click(object sender, EventArgs e)
        {
            divGrafico.Attributes.Add("class", "divGraficoOcultar");
            TDGrafico.Attributes.Add("class", "divGraficoOcultar");
            Chart1.Series.Clear();
            GraficarLinea();
        }

        protected void btnBar_Click(object sender, EventArgs e)
        {
            divGrafico.Attributes.Add("class", "divGraficoOcultar");
            TDGrafico.Attributes.Add("class", "divGraficoOcultar");
            Chart1.Series.Clear();
            GraficarBarra();
        }

        protected void btnTorta_Click(object sender, EventArgs e)
        {
            divGrafico.Attributes.Add("class", "divGraficoOcultar");
            TDGrafico.Attributes.Add("class", "divGraficoOcultar");
            Chart1.Series.Clear();
            GraficarTorta3D();
        }

        protected void btnRadar_Click(object sender, EventArgs e)
        {
            divGrafico.Attributes.Add("class", "divGraficoOcultar");
            TDGrafico.Attributes.Add("class", "divGraficoOcultar");
            Chart1.Series.Clear();
            GraficarRadar();
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            divGrafico.Attributes.Add("class", "divGraficoOcultar");
            TDGrafico.Attributes.Add("class", "divGraficoOcultar");
            TDBotonera.Attributes.Add("class", "BotoneraGraficoOcultar");
            Chart1.Series.Clear();
        }

        private Color GetRandomColor()
        {
            Random aleatorioRojo = new Random(DateTime.Now.Millisecond + 2 * numeroAleatorio);
            numeroAleatorio = aleatorioRojo.Next(0, 255) * numeroAleatorio + DateTime.Now.Millisecond + DateTime.Now.Year;
            Random aleatorioVerde = new Random((DateTime.Now.Millisecond + 5) * numeroAleatorio * DateTime.Now.Second);
            Random aleatorioAzul = new Random((DateTime.Now.Millisecond + 3) * DateTime.Now.Year * DateTime.Now.Millisecond);
            return Color.FromArgb(aleatorioRojo.Next(0, 255), aleatorioVerde.Next(0, 255), aleatorioAzul.Next(0, 255));
        }
    }
}