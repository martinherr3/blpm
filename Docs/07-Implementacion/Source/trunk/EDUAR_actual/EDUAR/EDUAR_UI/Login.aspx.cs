using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using System.Web;

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
                RegisterHyperLink.NavigateUrl = "~/Private/Account/Validate.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);

                ForgotPasswordHyperLink.NavigateUrl = "~/Private/Account/ForgotPassword.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);

                HttpContext.Current.User = null;

                if (!Page.IsPostBack)
                {
                    DTSeguridad propSeguridad = new DTSeguridad();
                    BLSeguridad objBLSeguridad = new BLSeguridad(propSeguridad);
                    if (Request.Params["const"] != null)
                    {
                        string user = BLEncriptacion.Decrypt(Request.Params["const"].ToString().Replace(' ', '+')).Trim().ToLower();
                        ObjSessionDataUI.ObjDTUsuario.Nombre = user;
                        // Provisoriamnente lo guardo en una variale de sesion userName porque el Page_Load de la Master me borra si HTTP.context es igual a null
                        Session["userName"] = user;
                        propSeguridad.Usuario.Nombre = user;
                        objBLSeguridad = new BLSeguridad(propSeguridad);
                        objBLSeguridad.GetUsuario();
                        ObjSessionDataUI.ObjDTUsuario = objBLSeguridad.Data.Usuario;
                        ObjSessionDataUI.ObjDTUsuario.EsUsuarioInicial = true;
                        //ObjDTSessionDataUI.ObjDTUsuario.Password = objBLSeguridad.Data.Usuario.Password;
                        Response.Redirect("~/Private/Account/ForgotPassword.aspx", false);
                    }
                }
                //ventanaInfoLogin.Visible = false;
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
                Session.Abandon();
                LoginUser.UserName = LoginUser.UserName.Trim().ToLower();
                DTSeguridad objDTSeguridad = new DTSeguridad
                {
                    Usuario =
                    {
                        Nombre = LoginUser.UserName.Trim().ToLower(),
                        Password = BLEncriptacion.Encrypt(LoginUser.Password.Trim())
                    }
                };

                BLSeguridad objSeguridadBL = new BLSeguridad(objDTSeguridad);
                objSeguridadBL.ValidarUsuario();

                if (objDTSeguridad.Usuario.UsuarioValido)
                {
                    e.Authenticated = true;
                    FormsAuthentication.SignOut();
                    FormsAuthentication.Initialize();
                    FormsAuthentication.SetAuthCookie(LoginUser.UserName.Trim().ToLower(), false);
                    ObjSessionDataUI.ObjDTUsuario = objDTSeguridad.Usuario;
                    UIUtilidades.EliminarArchivosSession(Session.SessionID);

                    if (ObjSessionDataUI.ObjDTUsuario.EsUsuarioInicial)
                        Response.Redirect("~/Private/Account/ChangePassword.aspx", false);
                    else
                        //if (Request.Params["ReturnUrl"] != null)
                        FormsAuthentication.RedirectFromLoginPage(LoginUser.UserName.Trim(), false);
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