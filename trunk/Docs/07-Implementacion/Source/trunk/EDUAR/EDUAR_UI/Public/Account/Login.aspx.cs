using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_Utility.Constantes;

namespace EDUAR_UI
{
    public partial class Login : EDUARBasePage
    {
        /// <summary>
        /// Método que se ejecuta al dibujar los controles de la página.
        /// Se utiliza para gestionar las excepciones del método Page_Load().
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (AvisoMostrar)
            {
                AvisoMostrar = false;

                try
                {
                    Master.ManageExceptions(AvisoExcepcion);
                }
                catch (Exception ex) { Master.ManageExceptions(ex); }
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RegisterHyperLink.NavigateUrl = "~/Public/Account/Validate.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);

                ForgotPasswordHyperLink.NavigateUrl = "~/Public/Account/ForgotPassword.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);

				if (!Page.IsPostBack)
				{
					DTSeguridad propSeguridad = new DTSeguridad();
					BLSeguridad objBLSeguridad = new BLSeguridad(propSeguridad);
					if (Request.Params["const"] != null)
					{
						string user = BLEncriptacion.Decrypt(Request.Params["const"].ToString());
						ObjSessionDataUI.ObjDTUsuario.EsUsuarioInicial = true;
						ObjSessionDataUI.ObjDTUsuario.Nombre = user;
						propSeguridad.Usuario.Nombre = user;
						objBLSeguridad = new BLSeguridad(propSeguridad);
						objBLSeguridad.GetUsuario();
						//ObjDTSessionDataUI.ObjDTUsuario.Password = objBLSeguridad.Data.Usuario.Password;
						Response.Redirect("~/Public/Account/ForgotPassword.aspx", false);
					}
				}
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginUsuario_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                DTSeguridad objDTSeguridad = new DTSeguridad
                {
                    Usuario =
                    {
                        Nombre = LoginUser.UserName.Trim(),
                        Password = LoginUser.Password.Trim()
                    }
                };

                BLSeguridad objSeguridadBL = new BLSeguridad(objDTSeguridad);
                objSeguridadBL.ValidarUsuario();

                if (objDTSeguridad.Usuario.UsuarioValido)
                {
                    e.Authenticated = true;
                    FormsAuthentication.SignOut();
                    FormsAuthentication.Initialize();
					FormsAuthentication.SetAuthCookie(LoginUser.UserName.Trim(), true);
                    ObjSessionDataUI.ObjDTUsuario = objDTSeguridad.Usuario;
                }
                else
                {
                    e.Authenticated = false;
                    LoginUser.FailureText = UIConstantesGenerales.MensajeLoginFallido;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }
    }
}
