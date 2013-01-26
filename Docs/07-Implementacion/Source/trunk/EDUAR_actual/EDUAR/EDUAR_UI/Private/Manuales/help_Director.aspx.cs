using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDUAR_UI
{
	public partial class help_Director : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void CambiarPanel_Click(object sender, EventArgs e)
		{
			foreach (Control item in udpContenido.ContentTemplateContainer.Controls)
				item.Visible = false;

			Panel panel = (Panel)udpContenido.ContentTemplateContainer.FindControl(((LinkButton)sender).CommandArgument);

			panel.Visible = true;
			udpContenido.Update();
		}
	}
}