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

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "~/Public/Account/Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
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

                    ObjDTSessionDataUI.ObjDTUsuario = objDTSeguridad.Usuario;
                }
                else
                {
                    e.Authenticated = false;
                    LoginUser.FailureText = UIConstantesGenerales.MensajeLoginFallido;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Master.ManageExceptions(ex);
                }
                catch
                {

                }
            }
        }
    }
}
