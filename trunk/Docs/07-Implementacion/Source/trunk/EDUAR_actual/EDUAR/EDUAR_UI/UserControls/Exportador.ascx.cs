using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace EDUAR_UI.UserControls
{
    public partial class Exportador : UserControl
    {
        private string _Titulo;
        private string _SP;
        private string _Observaciones;
        private DataTable _Datos;

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

        public string Observaciones
        {
            get
            {
                if (String.IsNullOrEmpty(_Observaciones))
                    if (ViewState["_Observaciones"] != null)
                        _Observaciones = ViewState["_Observaciones"].ToString();
                return _Observaciones;
            }
            set { ViewState["_Observaciones"] = value; _Observaciones = value; }
        }

        public string Titulo
        {
            get
            {
                if (String.IsNullOrEmpty(_Titulo))
                    if (ViewState["_Titulo"] != null)
                        _Titulo = ViewState["_Titulo"].ToString();
                return _Titulo;
            }
            set
            {
                ViewState["_Titulo"] = value;
                _Titulo = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(SP))
            {
                //Ejecutar SP
                CargarDatosFromSP();
            }

            if (Datos != null && !String.IsNullOrEmpty(Titulo))
            {
                lblTitulo.Text = Titulo;
                lblFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
                lblObservaciones.Text = Observaciones;

                string html = getHTML();

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/msword";
                //application/vnd.ms-excel   ---- pdf
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Titulo + ".doc");

                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.Default;

                Response.Write(html);
                Response.End();


                //PDF _pdf = new PDF();
                //Response.BinaryWrite(_pdf.HTMLtoPDFParser(html));
                //Response.End();
            }
        }

        /// <summary>
        /// Carga los datos de un store procedure.
        /// </summary>
        /// <param name="NombreSP"></param>
        private void CargarDatosFromSP()
        {
            //Informes i = new Informes();
            //DataSet ds = i.getDatos(SP);
            DataSet ds = new DataSet();
            if (ds.Tables.Count > 0)
                Datos = ds.Tables[0];
        }

        /// <summary>
        /// Genera el HTML a exportar
        /// </summary>
        /// <param name="NombreSP"></param>
        private string getHTML()
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            divHTML.Visible = true;
            divHTML.RenderControl(hw);

            StringBuilder html = new StringBuilder();
            //Encabezado HTML
            html.Append("<html><head></head><body>");
            //Encabezado del Titulo
            html.Append(tw.ToString());

            //Tabla
            html.Replace("§", GenerarTabla());

            //Pie HTML            
            html.Append("</body></html>");

            divHTML.Visible = false;
            return html.ToString();
        }

        private string GenerarTabla()
        {
            StringBuilder html = new StringBuilder();

            #region Genero Tabla HTML

            //Tabla
            html.Append("<table  width='100%' border='0' cellpadding='0' cellspacing='0'>");
            //Cabecera tabla
            html.Append(" <tr> ");
            foreach (DataColumn col in Datos.Columns)
            {
                html.Append("<td style='border: solid 1px #341010; border-color: #341010; color: #FFFFFF; padding: 4px 5px 4px 10px;text-align: center; vertical-align: bottom; font-size: 14px; font-weight: bold;background-color: #341010;'>" + col.ColumnName + "</td>");
            }
            html.Append("</tr>");

            //Cuerpo de la tabla
            if (Datos.Rows.Count > 0)
            {
                //string bordeSuperior = "border-right-style:solid; border-left-style:solid; border-top-style:solid; border-right-width:1px; border-left-width:1px; border-top-width:1px; border-right-color:Black; border-left-color:Black; border-top-color:Black;";
                //string bordeCostados = "border-right-style:solid; border-left-style:solid; border-right-width:1px; border-left-width:1px; border-right-color:Black; border-left-color:Black;";                
                string backColor = "";
                string estilo = "border: solid 1px #341010;padding:3px;font-size: 14px;";

                for (int i = 0; i < Datos.Rows.Count; i++)
                {
                    if ((i % 2) == 0 && backColor == "")
                        backColor = "background-color: #E3E3D9;";
                    else
                        backColor = "background-color: #F2F2F2;";
                    html.Append(" <tr align='left'> ");
                    for (int j = 0; j < Datos.Columns.Count; j++)
                    {
                        string valorColumna = Datos.Rows[i][j].ToString();
                        html.Append("<td style='" + estilo + backColor + "'>" + valorColumna + "</td>");

                    }
                    html.Append("</tr>");
                }
            }
            //linea de cierre final
            html.Append("<tr><td colspan='" + Datos.Columns.Count.ToString() + "' style='border-top-style:solid; border-top-width:1px; border-top-color:#341010;'></td></tr>");
            html.Append("</table>");
            #endregion

            return html.ToString();
        }

        public void Clean()
        {
            Titulo = "";
            SP = "";
            Observaciones = "";
            Datos = null;
        }
    }
}