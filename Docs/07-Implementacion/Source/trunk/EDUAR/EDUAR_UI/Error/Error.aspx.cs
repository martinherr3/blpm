using System;
using System.Web;
using System.Web.UI;

namespace EDUAR_UI.Error
{
	public partial class Error : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Application["CurrentError"] != null && HttpContext.Current.IsDebuggingEnabled)
			{
				divDetalle.Visible = true;
				lblError.Text = (string)Application["CurrentError"];
				lblDetalle.Text = (string)Application["CurrentErrorDetalle"];
				Application["CurrentError"] = null;
				Application["CurrentErrorDetalle"] = null;
			}
			else
				divDetalle.Visible = false;
		}
	}
}