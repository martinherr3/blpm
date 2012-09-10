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
            //Informes inf = new Informes();
            BLInformeIndicador objBLInforme = new BLInformeIndicador();
            DateTime desde = Convert.ToDateTime(txtDesde.Text);
            DateTime hasta = Convert.ToDateTime(txtHasta.Text);
            //grvInforme.DataSource = inf.getDatosIndicadores(SP, desde, hasta).Tables[0];
            grvInforme.DataSource = objBLInforme.GetInformeIndicador(SP, idCursoCicloLectivo, desde, hasta);
            grvInforme.DataBind();
        }

        protected void grvInforme_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Informes inf = new Informes();
            DateTime desde = Convert.ToDateTime(txtDesde.Text);
            DateTime hasta = Convert.ToDateTime(txtHasta.Text);
            //DataTable dt = inf.getDatosIndicadores(SP, desde, hasta).Tables[0];
            DataTable dt = new DataTable();
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
            DesactivarFiltros();
            ActivarFiltros();
            lblTitulo.Text = Titulo;
            MyAccordion.SelectedIndex = -1;
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

        private void DesactivarFiltros()
        {
            lblFiltro1.Visible = false;
            txtFiltro1.Visible = false;
            lblFiltro2.Visible = false;
            txtFiltro2.Visible = false;
            lblFiltro3.Visible = false;
            txtFiltro3.Visible = false;
            lblFiltro4.Visible = false;
            txtFiltro4.Visible = false;
            lblFiltro5.Visible = false;
            txtFiltro5.Visible = false;
            lblFiltro6.Visible = false;
            txtFiltro6.Visible = false;
        }

        private void ActivarFiltros()
        {
            if (Filtro1 != "")
            {
                lblFiltro1.Visible = true;
                lblFiltro1.Text = Filtro1;
                txtFiltro1.Visible = true;
                txtFiltro1.Text = "";
            }
            if (Filtro2 != "")
            {
                lblFiltro2.Visible = true;
                lblFiltro2.Text = Filtro2;
                txtFiltro2.Visible = true;
                txtFiltro2.Text = "";
            }
            if (Filtro3 != "")
            {
                lblFiltro3.Visible = true;
                lblFiltro3.Text = Filtro3;
                txtFiltro3.Visible = true;
                txtFiltro3.Text = "";
            }
            if (Filtro4 != "")
            {
                lblFiltro4.Visible = true;
                lblFiltro4.Text = Filtro4;
                txtFiltro4.Visible = true;
                txtFiltro4.Text = "";
            }
            if (Filtro5 != "")
            {
                lblFiltro5.Visible = true;
                lblFiltro5.Text = Filtro5;
                txtFiltro5.Visible = true;
                txtFiltro5.Text = "";
            }
            if (Filtro6 != "")
            {
                lblFiltro6.Visible = true;
                lblFiltro6.Text = Filtro6;
                txtFiltro6.Visible = true;
                txtFiltro6.Text = "";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void Filtrar()
        {
            //Informes inf = new Informes();
            DateTime desde = Convert.ToDateTime(txtDesde.Text);
            DateTime hasta = Convert.ToDateTime(txtHasta.Text);
            //DataTable dt = inf.getDatosIndicadores(SP, desde, hasta).Tables[0];
            DataTable dt = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dataView = new DataView(dt);
                string filtro = "";

                #region [Filtros]

                if (Filtro1 != "" && txtFiltro1.Text != "")
                {
                    if (filtro == "")
                        filtro += "[" + Filtro1 + "] like '%" + txtFiltro1.Text + "%' ";
                    else
                        filtro += "and [" + Filtro1 + "] like '%" + txtFiltro1.Text + "%' ";
                }
                if (Filtro2 != "" && txtFiltro2.Text != "")
                {
                    if (filtro == "")
                        filtro += "[" + Filtro2 + "] like '%" + txtFiltro2.Text + "%' ";
                    else
                        filtro += "and [" + Filtro2 + "] like '%" + txtFiltro2.Text + "%' ";
                }
                if (Filtro3 != "" && txtFiltro3.Text != "")
                {
                    if (filtro == "")
                        filtro += "[" + Filtro3 + "] like '%" + txtFiltro3.Text + "%' ";
                    else
                        filtro += "and [" + Filtro3 + "] like '%" + txtFiltro3.Text + "%' ";
                }
                if (Filtro4 != "" && txtFiltro4.Text != "")
                {
                    if (filtro == "")
                        filtro += "[" + Filtro4 + "] like '%" + txtFiltro4.Text + "%' ";
                    else
                        filtro += "and [" + Filtro4 + "] like '%" + txtFiltro4.Text + "%' ";
                }
                if (Filtro5 != "" && txtFiltro5.Text != "")
                {
                    if (filtro == "")
                        filtro += "[" + Filtro5 + "] like '%" + txtFiltro5.Text + "%' ";
                    else
                        filtro += "and [" + Filtro5 + "] like '%" + txtFiltro5.Text + "%' ";
                }
                if (Filtro6 != "" && txtFiltro6.Text != "")
                {
                    if (filtro == "")
                        filtro += "[" + Filtro6 + "] like '%" + txtFiltro6.Text + "%' ";
                    else
                        filtro += "and [" + Filtro6 + "] like '%" + txtFiltro6.Text + "%' ";
                }

                #endregion

                dataView.RowFilter = filtro;
                grvInforme.DataSource = dataView;
                grvInforme.DataBind();
            }
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