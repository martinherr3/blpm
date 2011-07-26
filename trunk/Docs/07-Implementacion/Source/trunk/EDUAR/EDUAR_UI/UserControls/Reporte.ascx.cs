using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI.UserControls
{
    public partial class Reporte : UserControl
    {
        /// <summary>
        /// textbox que contiene una fecha
        /// </summary>
        public GridView GrillaReporte
        {
            get { return gvwReporte; }
            set { gvwReporte = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarGrilla<T>(List<T> lista)
        {
            GrillaReporte.DataSource = UIUtilidades.BuildDataTable<T>(lista).DefaultView;
            GrillaReporte.DataBind();
            udpReporte.Update();
        }
    }
}