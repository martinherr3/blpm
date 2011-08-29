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
        private string _ColumnaNombre;
        private string _ColumnaValor;
        private DataTable _Datos;
        private bool _LegendOutside;
        private bool _IsListado;

        public string ColumnaNombre
        {
            get
            {
                if (_ColumnaNombre == null)
                    if (ViewState["_ColumnaNombre"] != null)
                        _ColumnaNombre = ViewState["_ColumnaNombre"].ToString();
                return _ColumnaNombre;
            }
            set { ViewState["_ColumnaNombre"] = value; _ColumnaNombre = value; }
        }

        public string ColumnaValor
        {
            get
            {
                if (_ColumnaValor == null)
                    if (ViewState["_ColumnaValor"] != null)
                        _ColumnaValor = ViewState["_ColumnaValor"].ToString();
                return _ColumnaValor;
            }
            set { ViewState["_ColumnaValor"] = value; _ColumnaValor = value; }
        }

        public DataTable Datos
        {
            get
            {
                if (_Datos == null)
                    if (ViewState["_Datos"] != null)
                        _Datos = (DataTable)ViewState["_Datos"];
                return _Datos;
            }
            set { ViewState["_Datos"] = value; _Datos = value; }
        }

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

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (_Datos != null)
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
            //Chart1.Series["Default"].Points.DataBind(Datos.DefaultView, ColumnaNombre, ColumnaValor, "");
            Chart1.Titles.Add(Titulo);
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
            //Chart1.Series[serie].Points.DataBind(Datos.DefaultView, ColumnaNombre, ColumnaValor, "");
        }

        /// <summary>
        /// Grafica de barras.
        /// </summary>
        public void GraficarBarra()
        {
            foreach (MiSerie item in ListaSeries)
            {
                Chart1.Series.Add(item.NombreSerie);
                Chart1.Series[item.NombreSerie]["DrawingStyle"] = "Emboss";
                Chart1.Series[item.NombreSerie].Color = System.Drawing.Color.Blue;
                Chart1.Series[item.NombreSerie].BackGradientStyle = GradientStyle.DiagonalLeft;
                Chart1.Series[item.NombreSerie].ShadowOffset = 2;
                Chart1.Series[item.NombreSerie].ToolTip = "#VALX : #VALY";
            }

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
                Chart1.Legends.Clear();
                Chart1.Series[item.NombreSerie].ChartType = SeriesChartType.Line;
                Chart1.ChartAreas[item.NombreSerie].Area3DStyle.Enable3D = false;
                Chart1.Series[item.NombreSerie].Color = System.Drawing.Color.Blue;
                Chart1.Series[item.NombreSerie].BorderWidth = 4;
                Chart1.Series[item.NombreSerie].ShadowOffset = 2;
            }
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

                Graficar();

                Chart1.Series[item.NombreSerie].LegendText = "#VALX : #PERCENT";
                if (Chart1.Legends.Count > 0)
                    Chart1.Legends[0].Alignment = StringAlignment.Center;
            }
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
                Chart1.Series[item.NombreSerie].Color = System.Drawing.Color.Blue;
                Chart1.Series[item.NombreSerie].ToolTip = "#VALX : #PERCENT";

                Graficar();

                // Set radar chart style (Area, Line or Marker)
                Chart1.Series[item.NombreSerie]["RadarDrawingStyle"] = "Area";

                // Set circular area drawing style (Circle or Polygon)
                Chart1.Series[item.NombreSerie]["AreaDrawingStyle"] = "Polygon";

                // Set labels style (Auto, Horizontal, Circular or Radial)
                Chart1.Series[item.NombreSerie]["CircularLabelsStyle"] = "Horizontal";

                Chart1.Legends.Clear();
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.Legends.Clear();
            }
        }

        /// <summary>
        /// Muestra un listado de la tabla cargada.
        /// </summary>
        public void Listar()
        {
            _IsListado = true;
        }

        /// <summary>
        /// Carga los datos de un store procedure.
        /// </summary>
        /// <param name="NombreSP"></param>
        //public void CargarDatos<T>(List<T> lista)
        //{
        //    Datos = UIUtilidades.BuildDataTable<T>(lista);
        //}

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
    }
}