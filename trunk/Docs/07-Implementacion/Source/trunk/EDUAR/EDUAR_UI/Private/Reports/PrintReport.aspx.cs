using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using System.Data;
using EDUAR_UI.Utilidades;
using System.Text;
using System.IO;

namespace EDUAR_UI
{
    public partial class PrintReport : EDUARBasePage
    {
        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the dt reporte.
        /// </summary>
        /// <value>
        /// The dt reporte.
        /// </value>
        public DataTable dtReporte
        {
            get
            {
                if (Session["dtReporte"] == null)
                    Session["dtReporte"] = new DataTable();
                return (DataTable)Session["dtReporte"];
            }
            set { Session["dtReporte"] = value; }
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
                //if (Session["tituloReporte"] == null)
                //    Session["tituloReporte"] = string.Empty;
                return Session["tituloReporte"].ToString();
            }
            //set { Session["tituloReporte"] = value; }
        }
        #endregion

        #region --[Eventos]--
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                btnImprimir.Attributes.Add("onClick", "Imprimir();");
                btnVolver.Attributes.Add("onClick", "Cerrar();");

                if (!Page.IsPostBack)
                {
                    gvwReporte = UIUtilidades.GenerarGrilla(gvwReporte, dtReporte);
                    lblTitulo.Text = "EDU@R 2.0";
                    lblInforme.Text = tituloReporte;
                    lblFecha.Text = DateTime.Now.ToShortDateString();
                    gvwReporte.DataSource = dtReporte.DefaultView;
                    gvwReporte.DataBind();
                    udpReporte.Update();
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region --[Métodos Privados]--
        
        #endregion
    }
}