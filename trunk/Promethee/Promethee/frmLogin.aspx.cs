using System;
using System.Web.Security;
using DataAccess;


namespace Promethee
{
    public partial class frmLogin : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Processes the login.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ProcessLogin(object sender, EventArgs e)
        {
            if (LoginService.Autenticar(txtUser.Text, txtPassword.Text))
            {
                FormsAuthentication.RedirectFromLoginPage(txtUser.Text, chkPersistLogin.Checked, null);

                Response.Redirect("~/Modelos.aspx");
            }
            else
                ErrorMessage.InnerHtml = "<b>Usuario o contraseña incorrectos...</b> por favor re-ingrese las credenciales...";
        }
    }
}
