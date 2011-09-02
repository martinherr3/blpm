using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDUAR_UI.UserControls
{
	public partial class Editor : UserControl
	{
        public String contenido
        {
            get { return inputEditor.InnerText; }
            set { inputEditor.InnerText = value; }
        }
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}
	}
}