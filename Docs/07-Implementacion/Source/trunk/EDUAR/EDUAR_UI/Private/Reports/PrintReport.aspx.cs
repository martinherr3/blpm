using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using System.Data;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI
{
    public partial class PrintReport : EDUARBasePage
    {
        #region --[Propiedades]--
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                grdReporte.DataSource = dtReporte;
                grdReporte.DataBind();
                udpReporte.Update();
            }
        }
    }
}