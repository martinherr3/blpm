using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EDUAR_UI.UserControls
{
    public partial class Reporte : UserControl
    {
        #region --[Propiedades]--
        /// <summary>
        /// textbox que contiene una fecha
        /// </summary>
        public GridView GrillaReporte
        {
            get { return gvwReporte; }
            set { gvwReporte = value; }
        }

        public DataTable dtReporte
        {
            get
            {
                if (ViewState["dtReporte"] == null)
                    ViewState["dtReporte"] = new DataTable();
                return (DataTable)ViewState["dtReporte"];
            }
            set { ViewState["dtReporte"] = value; }
        }
        #endregion

        #region --[Eventos]--
        protected void Page_Load(object sender, EventArgs e)
        {
            btnPDF.Click += (ExportarPDF);
            btnVolver.Click += (Volver);
            gvwReporte.PageIndexChanging += (PaginandoGrilla);

            if (!Page.IsPostBack)
            {
                btnPDF.Visible = false;
                btnVolver.Visible = false;
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter salida = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(salida);
            Page pag = new Page();
            HtmlForm form = new HtmlForm();
            this.GrillaReporte.EnableViewState = false;
            pag.EnableEventValidation = false;
            pag.DesignerInitialize();
            pag.Controls.Add(form);
            form.Controls.Add(this.GrillaReporte);
            pag.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/text/HTML";
            Response.AddHeader("Content-Type", "text/html");
            Response.Charset = "MS-Windows";
            Response.ContentEncoding = Encoding.Default;
            Response.Write("<script language='JavaScript'>window.open('Print.aspx')</script>" + sb.ToString());
            Response.End();
        }
        #endregion

        #region --[Métodos Públicos]--
        public void CargarReporte<T>(List<T> lista)
        {
            try
            {
                if (lista.Count != 0)
                {
                    GrillaReporte = UIUtilidades.GenerarGrilla(lista, GrillaReporte);
                    btnVolver.Visible = true;
                    btnPDF.Visible = true;
                    gvwReporte.Visible = true;
                    CargarGrilla(lista);
                    udpReporte.Update();
                }
                else
                {
                    udpReporte.ContentTemplateContainer.Controls.Add(new LiteralControl("<h3>" + UIConstantesGenerales.MensajeSinResultados + "</h3>"));
                    gvwReporte.Visible = false;
                    btnVolver.Visible = true;
                    udpReporte.Update();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region --[Métodos Privados]--
        void ExportarPDF(object sender, EventArgs e)
        {

            OnExportarPDFClick(ExportarPDFClick, e);
            udpReporte.Update();
        }

        void Volver(object sender, EventArgs e)
        {
            OnVolverClick(VolverClick, e);
            udpReporte.Update();
        }

        void PaginandoGrilla(object sender, GridViewPageEventArgs e)
        {
            onPaginandoGrilla(PaginarGrilla, e);
            udpReporte.Update();
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">The lista.</param>
        private void CargarGrilla<T>(List<T> lista)
        {
            dtReporte = UIUtilidades.BuildDataTable<T>(lista);
            GrillaReporte.DataSource = dtReporte.DefaultView;
            GrillaReporte.DataBind();
            udpReporte.Update();
        }

        #endregion

        #region --[Delegados ]--

        public delegate void VentanaBotonClickHandler(object sender, EventArgs e);
        public delegate void PaginarGrillaHandler(object sender, GridViewPageEventArgs e);

        public event VentanaBotonClickHandler ExportarPDFClick;
        public event VentanaBotonClickHandler VolverClick;
        public event PaginarGrillaHandler PaginarGrilla;

        public virtual void OnExportarPDFClick(VentanaBotonClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }

        public virtual void OnVolverClick(VentanaBotonClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }

        public virtual void onPaginandoGrilla(PaginarGrillaHandler sender, GridViewPageEventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }
        #endregion
    }
}
