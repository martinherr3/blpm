using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Common;


namespace EDUAR_UI.UserControls
{
    public partial class InformeIndicador : UserControl
    {
        private string _SP;

        #region --[Propiedades]--
        public int idCursoCicloLectivo
        {
            get { return (int)Session["idCursoCicloLectivo"]; }
        }

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
                if (Session["Fecha_Desde_Indicador"] != null)
                    return Convert.ToDateTime(Session["Fecha_Desde_Indicador"].ToString());
                return DateTime.MinValue;
            }
            set { Session["Fecha_Desde_Indicador"] = value; }
        }

        public DateTime Hasta
        {
            get
            {
                if (Session["Fecha_Hasta_Indicador"] != null)
                    return Convert.ToDateTime(Session["Fecha_Hasta_Indicador"].ToString());
                return DateTime.MinValue;
            }
            set { Session["Fecha_Hasta_Indicador"] = value; }
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

        /// <summary>
        /// Gets or sets the ciclo lectivo actual.
        /// </summary>
        /// <value>
        /// The ciclo lectivo actual.
        /// </value>
        public CicloLectivo cicloLectivoActual
        {
            get
            {
                if (ViewState["cicloLectivoActual"] == null)
                {
                    BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
                    cicloLectivoActual = objBLCicloLectivo.GetCicloLectivoActual();
                }
                return (CicloLectivo)ViewState["cicloLectivoActual"];
            }
            set { ViewState["cicloLectivoActual"] = value; }
        }

                /// <summary>
        /// Gets or sets the id curso ciclo lectivo.
        /// </summary>
        /// <value>
        /// The id curso ciclo lectivo.
        /// </value>
        public int idCursoIndicador
        {
            get
            {
                if (ViewState["idCursoIndicador_" + this.UniqueID] == null)
                {
                    if (idCursoCicloLectivo > 0)
                        idCursoIndicador = idCursoCicloLectivo;
                    else
                        idCursoIndicador = 0;
                }
                return (int)ViewState["idCursoIndicador_" + this.UniqueID];
            }
            set { ViewState["idCursoIndicador_" + this.UniqueID] = value; }
        }
        #endregion

        #region --[Métodos Públicos]--
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSalir.Click += (InformeIndicadorSalir);
            calFechaDesde.startDate = cicloLectivoActual.fechaInicio;
            calFechaHasta.startDate = cicloLectivoActual.fechaInicio;

            calFechaDesde.endDate = DateTime.Today;
            calFechaHasta.endDate = DateTime.Today;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Hide();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        void InformeIndicadorSalir(object sender, EventArgs e)
        {
            OnSalirClick(SalirClick, e);
        }

        #region --[Sorting y Paginado de la grilla]--

        protected void grvInforme_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvInforme.PageIndex = e.NewPageIndex;
            BLInformeIndicador objBLInforme = new BLInformeIndicador();
            DateTime desde = Convert.ToDateTime(calFechaDesde.ValorFecha);
            DateTime hasta = Convert.ToDateTime(calFechaHasta.ValorFecha);
            DataTable dt = objBLInforme.GetInformeIndicador(SP, idCursoCicloLectivo, desde, hasta);
            grvInforme.DataSource = dt;
            grvInforme.DataBind();
        }

        protected void grvInforme_Sorting(object sender, GridViewSortEventArgs e)
        {
            BLInformeIndicador objBLInforme = new BLInformeIndicador();
            DateTime desde = Convert.ToDateTime(calFechaDesde.ValorFecha);
            DateTime hasta = Convert.ToDateTime(calFechaHasta.ValorFecha);
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

        #endregion

        #region --[Delegados]--

        public delegate void VentanaBotonClickHandler(object sender, EventArgs e);

        public event VentanaBotonClickHandler SalirClick;

        /// <summary>
        /// Called when [exportar PDF click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public virtual void OnSalirClick(VentanaBotonClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }
        #endregion

        #region --[Métodos Públicos]--
        public void Show()
        {
            divInforme.Visible = true;
            lblTitulo.Text = Titulo;
            calFechaDesde.Fecha.Text = Desde != DateTime.MinValue ? Desde.ToString("dd/MM/yyyy") : "";
            calFechaHasta.Fecha.Text = Hasta != DateTime.MinValue ? Hasta.ToString("dd/MM/yyyy") : "";
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
            Desde = cicloLectivoActual.fechaInicio;
            Hasta = DateTime.Today;
            Titulo = "";
            grvInforme.DataSource = new DataTable();
            grvInforme.DataBind();

            exp.Clean();

            upInforme.Update();
        }

        private void Buscar()
        {
            BLInformeIndicador objBLInforme = new BLInformeIndicador();
            DateTime desde = Convert.ToDateTime(calFechaDesde.ValorFecha);
            if (desde.Subtract(cicloLectivoActual.fechaInicio).Days < 0)
            {
                desde = cicloLectivoActual.fechaInicio;
                calFechaDesde.Fecha.Text = desde.ToShortDateString();
            }
            DateTime hasta = Convert.ToDateTime(calFechaHasta.ValorFecha);
            DataTable dt = objBLInforme.GetInformeIndicador(SP, idCursoIndicador, desde, hasta);
            grvInforme.DataSource = dt;
            grvInforme.DataBind();

            //Exportador
            exp.Datos = dt;
            exp.Titulo = Titulo;
            upInforme.Update();
        }
        #endregion

    }
}