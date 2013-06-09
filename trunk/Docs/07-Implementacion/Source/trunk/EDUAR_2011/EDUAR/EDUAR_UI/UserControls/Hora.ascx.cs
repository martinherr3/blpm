using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDUAR_UI.UserControls
{
	public partial class Hora : UserControl
	{
		#region --[Propiedades]--
		/// <summary>
		/// textbox que contiene la hora.
		/// </summary>
		public TextBox hora
		{
			get { return txtHora; }
			set { txtHora = value; }
		}

		public String Text
		{
			get { return txtHora.Text; }
			set { txtHora.Text = value; }
		}
		#endregion

		#region --[Eventos]--
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		#endregion
	}
}