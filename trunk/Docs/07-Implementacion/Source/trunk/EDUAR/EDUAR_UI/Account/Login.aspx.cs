using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_Entities.Security;
using EDUAR_BusinessLogic.Security;
using System.Web.Security;

namespace EDUAR_UI
{
    public partial class Login : EDUARBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
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
                    LoginUser.FailureText = "Error de login";//UIConstantesGenerales.MensajeLoginFallido;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    //ventanaInfoLogin.GestionExcepciones(ex);
                    //updVentaneMensajes.Update();
                }
                catch
                {

                }
            }
        }
    }
}
