using System;
using System.Web.UI;

namespace EDUAR_UI.UserControls
{
	public partial class Editor : UserControl
	{
        public String contenido
        {
			get { return txtEditor.Text; }
            set { txtEditor.Text = value; }
        }
		protected void Page_Load(object sender, EventArgs e)
		{
		}
	}
}