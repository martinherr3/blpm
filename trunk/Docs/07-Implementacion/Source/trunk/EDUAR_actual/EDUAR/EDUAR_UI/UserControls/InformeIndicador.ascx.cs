using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EDUAR_BusinessLogic.Reports;


namespace EDUAR_UI.UserControls
{
    public partial class InformeIndicador : UserControl
    {
        #region [Propiedades]
        public int idCursoCicloLectivo
        {
            get { return (int)Session["idCursoCicloLectivo"]; }
        }

        private string _SP;

        /// <summary>
        /// Nombre del Store Procedure. Debe recibir la fecha desde y hasta como parametro.
        /// </summary>
        public string SP
        {
            get
            {
                if (String.IsNullOrEmpty(_SP))
                    if (ViewState["_SP"] != null)
                        _SP = ViewState["_SP"].ToString();
                return _SP;
            }
            set { ViewState["_SP"] = value; _SP = value; }
        }

        public DateTime Desde
        {
            get
            {
                if (ViewState["Desde"] != null)
                    return Convert.ToDateTime(ViewState["Desde"].ToString());
                return DateTime.MinValue;
            }
            set { ViewState["Desde"] = value; }
        }

        public DateTime Hasta
        {
            get
            {
                if (ViewState["Hasta"] != null)
                    return Convert.ToDateTime(ViewState["Hasta"].ToString());
                return DateTime.MinValue;
            }
            set { ViewState["Hasta"] = value; }
        }

        public string Filtro1
        {
            get
            {
                if (ViewState["Filtro1"] != null)
                    return ViewState["Filtro1"].ToString();
                return "";
            }
            set { ViewState["Filtro1"] = value; }
        }

        public string Filtro2
        {
            get
            {
                if (ViewState["Filtro2"] != null)
                    return ViewState["Filtro2"].ToString();
                return "";
            }
            set { ViewState["Filtro2"] = value; }
        }

        public string Filtro3
        {
            get
            {
                if (ViewState["Filtro3"] != null)
                    return ViewState["Filtro3"].ToString();
                return "";
            }
            set { ViewState["Filtro3"] = value; }
        }

        public string Filtro4
        {
            get
            {
                if (ViewState["Filtro4"] != null)
                    return ViewState["Filtro4"].ToString();
                return "";
            }
            set { ViewState["Filtro4"] = value; }
        }

        public string Filtro5
        {
            get
            {
                if (ViewState["Filtro5"] != null)
                    return ViewState["Filtro5"].ToString();
                return "";
            }
            set { ViewState["Filtro5"] = value; }
        }

        public string Filtro6
        {
            get
            {
                if (ViewState["Filtro6"] != null)
                    return ViewState["Filtro6"].ToString();
                return "";
            }
            set { ViewState["Filtro6"] = value; }
        }

        public string Titulo
        {
            get
            {
                if (ViewState["Titulo"] != null)
                    return ViewState["Titulo"].ToString();
                return "";
            }
            set { ViewState["Titulo"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region [Sorting y Paginado de la grilla]

        protected void grvInforme_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvInforme.PageIndex = e.NewPageIndex;
			BLInformeIndicador objBLInforme = new BLInformeIndicador();
			DateTime desde = Convert.ToDateTime(txtDesde.Text);
			DateTime hasta = Convert.ToDateTime(txtHasta.Text);
			DataTable dt = objBLInforme.GetInformeIndicador(SP, idCursoCicloLectivo, desde, hasta);
			grvInforme.DataSource = dt;
            grvInforme.DataBind();
        }

        protected void grvInforme_Sorting(object sender, GridViewSortEventArgs e)
        {
			BLInformeIndicador objBLInforme = new BLInformeIndicador();
			DateTime desde = Convert.ToDateTime(txtDesde.Text);
			DateTime hasta = Convert.ToDateTime(txtHasta.Text);
			DataTable dt = objBLInforme.GetInformeIndicador(SP, idCursoCicloLectivo, desde, hasta);

            if (dt != null)
            {
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                grvInforme.DataSource = dataView;
                grvInforme.DataBind();
            }
        }

        private string SortExpressionInf
        {
            get { return (ViewState["SortExpressionInf"] == null ? string.Empty : ViewState["SortExpressionInf"].ToString()); }
            set { ViewState["SortExpressionInf"] = value; }
        }

        private string SortDirectionInf
        {
            get { return (ViewState["SortDirectionInf"] == null ? string.Empty : ViewState["SortDirectionInf"].ToString()); }
            set { ViewState["SortDirectionInf"] = value; }
        }

        private string GetSortDirection(string sortExpression)
        {
            if (SortExpressionInf == sortExpression)
            {
                if (SortDirectionInf == "ASC")
                    SortDirectionInf = "DESC";
                else if (SortDirectionInf == "DESC")
                    SortDirectionInf = "ASC";
                return SortDirectionInf;
            }
            else
            {
                SortExpressionInf = sortExpression;
                SortDirectionInf = "ASC";
                return SortDirectionInf;
            }
        }

        #endregion

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Hide();
        }

        public void Show()
        {
            divInforme.Visible = true;
            lblTitulo.Text = Titulo;
            txtDesde.Text = Desde != DateTime.MinValue ? Desde.ToString("dd/MM/yyyy") : "";
            txtHasta.Text = Hasta != DateTime.MinValue ? Hasta.ToString("dd/MM/yyyy") : "";
            Buscar();

            upInforme.Update();
        }

        public void Hide()
        {
            divInforme.Visible = false;
            //Vacio el viewstate
            SortExpressionInf = null;
            SortDirectionInf = null;
            SP = null;
            Filtro1 = "";
            Filtro2 = "";
            Filtro3 = "";
            Filtro4 = "";
            Filtro5 = "";
            Filtro6 = "";
            Desde = DateTime.MinValue;
            Hasta = DateTime.MinValue;
            Titulo = "";
            grvInforme.DataSource = new DataTable();
            grvInforme.DataBind();

            exp.Clean();

            upInforme.Update();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            BLInformeIndicador objBLInforme = new BLInformeIndicador();
            DateTime desde = Convert.ToDateTime(txtDesde.Text);
            DateTime hasta = Convert.ToDateTime(txtHasta.Text);
            DataTable dt = objBLInforme.GetInformeIndicador(SP, idCursoCicloLectivo, desde, hasta);
            grvInforme.DataSource = dt;
            grvInforme.DataBind();

            //Exportador
            exp.Datos = dt;
            exp.Titulo = Titulo;
        }

    }
}