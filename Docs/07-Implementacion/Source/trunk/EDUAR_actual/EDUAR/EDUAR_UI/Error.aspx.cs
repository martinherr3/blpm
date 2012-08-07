using System;
using System.Web;
using System.Web.UI;

namespace EDUAR_UI
{
	public partial class Error : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Application["CurrentError"] != null || HttpContext.Current.IsDebuggingEnabled)
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

		/// <summary>
		/// Handles the Click event of the btnVolver control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnVolver_Click(object sender, EventArgs e)
		{
			try
			{
				Response.Redirect("~/Default.aspx",false);
			}
			catch (Exception)
			{
			}
		}
	}
}