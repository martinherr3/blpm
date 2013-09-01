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

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public String Text
		{
			get { return txtHora.Text; }
			set { txtHora.Text = value; }
		}

		/// <summary>
		/// Gets or sets the validation summary.
		/// </summary>
		/// <value>
		/// The validation summary.
		/// </value>
		public string validationSummary
		{
			get { return RegularExpressionValidator1.ValidationGroup; }
			set { RegularExpressionValidator1.ValidationGroup = validationSummary; }
		}
 
		#endregion

		#region --[Eventos]--
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		#endregion
	}
}