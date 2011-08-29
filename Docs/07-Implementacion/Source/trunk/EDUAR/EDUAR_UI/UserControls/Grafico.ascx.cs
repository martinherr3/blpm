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
						Title t = new Title(ViewState["_Titulo"].ToString(), Docking.Top, new System.Drawing.Font("Tahoma", 16, System.Drawing.FontStyle.Underline), System.Drawing.Color.FromArgb(52, 16, 16));
						Chart1.Titles.Add(t);
                    }
                }
                return "";
            }
            set
            {
                Chart1.Titles.Clear();
                Title t = new Title(value, Docking.Top, new System.Drawing.Font("Tahoma", 16, System.Drawing.FontStyle.Underline), System.Drawing.Color.FromArgb(52, 16, 16));
                Chart1.Titles.Add(t);
                ViewState["_Titulo"] = value;
            }
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
            Chart1.Series["Default"].Points.DataBind(Datos.DefaultView, ColumnaNombre, ColumnaValor, "");
			Chart1.Titles.Add(Titulo);
            _IsListado = false;
			//divGrafico.Visible = true;
			divGrafico.Attributes.Add("class", "divGraficoMostrar");
			TDGrafico.Attributes.Add("class", "divGraficoMostrar");
			TDBotonera.Attributes.Add("class", "BotoneraGraficoMostrar");
        }

        /// <summary>
        /// Grafica de barras.
        /// </summary>
        public void GraficarBarra()
        {
            Chart1.Series.Add("Default");
            Chart1.Legends.Clear();
            Chart1.Series["Default"]["DrawingStyle"] = "Emboss";
            Chart1.Series["Default"].Color = System.Drawing.Color.Brown;
            Chart1.Series["Default"].BackGradientStyle = GradientStyle.DiagonalLeft;
            Chart1.Series["Default"].ShadowOffset = 2;
            Chart1.Series["Default"].ToolTip = "#VALX : #VALY";
           
			Graficar();
        }

        /// <summary>
        /// Grafica lineal.
        /// </summary>
        public void GraficarLinea()
        {
            Chart1.Series.Add("Default");
            Chart1.Legends.Clear();
            Chart1.Series["Default"].ChartType = SeriesChartType.Line;
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            Chart1.Series["Default"].Color = System.Drawing.Color.Brown;
            Chart1.Series["Default"].BorderWidth = 4;
            Chart1.Series["Default"].ShadowOffset = 2;

            Graficar();
        }

        /// <summary>
        /// Grafica una torta 3D.
        /// </summary>
        public void GraficarTorta3D()
        {
			Chart1.Series.Add("Default");
            Chart1.Series["Default"].ChartType = SeriesChartType.Pie;
            Chart1.Series["Default"].ShadowOffset = 2;
            Chart1.Series["Default"].ToolTip = "#VALX : #PERCENT";

            if (LegendOutside)
            {
                Chart1.Series["Default"]["PieLabelStyle"] = "Outside";
                Chart1.Series["Default"].Label = "#VALX";
            }
            else
                Chart1.Series["Default"].Label = " ";

            Graficar();

            Chart1.Series["Default"].LegendText = "#VALX : #PERCENT";
            if (Chart1.Legends.Count > 0)
                Chart1.Legends[0].Alignment = StringAlignment.Center;
        }

        /// <summary>
        /// Grafica una torta 3D.
        /// </summary>
        public void GraficarRadar()
        {
			Chart1.Series.Add("Default");
            Chart1.Series["Default"].ChartType = SeriesChartType.Radar;
            Chart1.Series["Default"].Color = System.Drawing.Color.Brown;
            Chart1.Series["Default"].ToolTip = "#VALX : #PERCENT";

            Graficar();

            // Set radar chart style (Area, Line or Marker)
            Chart1.Series["Default"]["RadarDrawingStyle"] = "Area";

            // Set circular area drawing style (Circle or Polygon)
            Chart1.Series["Default"]["AreaDrawingStyle"] = "Polygon";

            // Set labels style (Auto, Horizontal, Circular or Radial)
            Chart1.Series["Default"]["CircularLabelsStyle"] = "Horizontal";

            Chart1.Legends.Clear();
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Legends.Clear();
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
        public void CargarDatos<T>(List<T> lista)
        {
			Datos = UIUtilidades.BuildDataTable<T>(lista);
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