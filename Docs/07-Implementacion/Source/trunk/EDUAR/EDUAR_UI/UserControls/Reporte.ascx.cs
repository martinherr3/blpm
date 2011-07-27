using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web;

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

        private DataTable dtReporte
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
            //btnPDF.Click += (ExportarPDF);
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
                    CargarGrilla(lista);
                }
                else
                {
                    udpReporte.ContentTemplateContainer.Controls.Add(new LiteralControl("<h3>" + UIConstantesGenerales.MensajeSinResultados + "</h3>"));
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
        //void ExportarPDF(object sender, EventArgs e)
        //{
        //    OnExportarPDFClick(ExportarPDFClick, e);
        //}

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

        //public delegate void VentanaBotonClickHandler(object sender, EventArgs e);

        //public event VentanaBotonClickHandler ExportarPDFClick;

        //public virtual void OnExportarPDFClick(VentanaBotonClickHandler sender, EventArgs e)
        //{
        //    if (sender != null)
        //    {
        //        //Invoca el delegados
        //        sender(this, e);
        //    }
        //}
        #endregion


        protected void BindGridView()
        {
            GrillaReporte.DataSource = dtReporte;
            GrillaReporte.DataBind();
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dtReporte;
            GridView1.DataBind();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=DataTable.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }

        private void ShowPdf(string strS)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strS);
            Response.TransmitFile(strS);
            Response.End();
            //Response.WriteFile(strS);
            Response.Flush();
            Response.Clear();

        }
    }
}
