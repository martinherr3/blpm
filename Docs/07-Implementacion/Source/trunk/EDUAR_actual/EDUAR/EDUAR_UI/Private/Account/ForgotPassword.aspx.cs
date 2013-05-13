using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Utilidades;

namespace EDUAR_UI
{
    public partial class ForgotPassword : EDUARBasePage
    {
        #region --[Atributos]--
        private BLSeguridad objBLSeguridad;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Propiedad que contiene el objeto seguridad que devuelve la consulta a la Capa de Negocio.
        /// </summary>
        public DTSeguridad propSeguridad
        {
            get
            {
                if (ViewState["propSeguridad"] == null)
                    return null;

                return (DTSeguridad)ViewState["propSeguridad"];
            }
            set { ViewState["propSeguridad"] = value; }
        }
        #endregion

        #region --[Eventos]--
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
                Master.BotonAvisoAceptar += (VentanaAceptar);
				
                if (!Page.IsPostBack)
                {
					Response.Cookies.Clear();
					HttpContext.Current.User = null;
					propSeguridad = new DTSeguridad();
                    if (ObjSessionDataUI.ObjDTUsuario.EsUsuarioInicial == true)
                    {
                        ObjSessionDataUI.ObjDTUsuario.EsUsuarioInicial = false;
                        CargarDatosUsuario();
                        CargarPresentacionRecover();
                    }
                    else
                    {
                        udpEmail.Visible = true;
                        udpRecover.Visible = false;
                    }
                    udpForgotPassword.Update();

                }
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
            }
        }

		/// <summary>
		/// Ventanas the aceptar.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void VentanaAceptar(object sender, EventArgs e)
        {
            try
            {
				Response.Redirect("~/Private/Account/Welcome.aspx", false);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEnviarMail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                if (EDUARUtilidades.EsEmailValido(txtEmail.Text.Trim()))
                {
                    propSeguridad.Usuario.Email = txtEmail.Text.Trim();
                    objBLSeguridad = new BLSeguridad(propSeguridad);
                    objBLSeguridad.GetUsuarioByEmail();
                    objBLSeguridad.RecuperarPassword(ObjSessionDataUI.urlDefault);

                    txtEmail.Text = string.Empty;

                    AccionPagina = enumAcciones.Buscar;
                    Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeCheckearCorreo, enumTipoVentanaInformacion.Satisfactorio);
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRecoverPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnRecoverPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRespuesta.Text.Trim().Length > 0)
                {
                    propSeguridad.Usuario.PaswordRespuesta = txtRespuesta.Text.Trim();
                    propSeguridad.Usuario.Nombre = ObjSessionDataUI.ObjDTUsuario.Nombre;
                    objBLSeguridad = new BLSeguridad(propSeguridad);
                    objBLSeguridad.ValidarRespuesta();

                    if (!string.IsNullOrEmpty(objBLSeguridad.Data.Usuario.PaswordRespuesta))
                    {
                        ObjSessionDataUI.ObjDTUsuario.PaswordRespuesta = txtRespuesta.Text.Trim();
                        CargarPresentacionNuevaPassword();

                        txtEmail.Text = string.Empty;
                    }
                    else
                    {
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeErrorPreguntaSeguridad, enumTipoVentanaInformacion.Advertencia);
                    }
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnConfirmarPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnConfirmarPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    propSeguridad.Usuario.PaswordPregunta = ObjSessionDataUI.ObjDTUsuario.PaswordPregunta;
                    propSeguridad.Usuario.PaswordRespuesta = ObjSessionDataUI.ObjDTUsuario.PaswordRespuesta;
					propSeguridad.Usuario.PasswordNuevo = BLEncriptacion.Encrypt(txtPassword.Text.Trim());
                    // Ya que el nombre de usuario no lo tengo guardado (porque en el Page_Load de la Master me lo borro porque el 
                    // HTTP.context era igual a null), provisoriamente lo guarda en una variable de sesion, para tenerlo disponible aqui
                    propSeguridad.Usuario.Nombre = (Session["userName"]).ToString();
                    objBLSeguridad = new BLSeguridad(propSeguridad);
                    objBLSeguridad.CambiarPassword();
                    HttpContext.Current.SkipAuthorization = true;
                    AccionPagina = enumAcciones.Salir;
                    FormsAuthentication.SetAuthCookie(objBLSeguridad.Data.Usuario.Nombre.Trim().ToLower(), false);

                    Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeNuevoPassword, enumTipoVentanaInformacion.Satisfactorio);
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the presentacion recover.
        /// </summary>
        private void CargarPresentacionRecover()
        {
            udpEmail.Visible = false;
            udpRecover.Visible = true;
            udpNewPassword.Visible = false;
            udpRecover.Update();
        }

        /// <summary>
        /// Cargars the presentacion nueva password.
        /// </summary>
        private void CargarPresentacionNuevaPassword()
        {
            udpEmail.Visible = false;
            udpRecover.Visible = false;
            udpNewPassword.Visible = true;
            udpForgotPassword.Update();
        }

        /// <summary>
        /// Cargars the datos usuario.
        /// </summary>
        private void CargarDatosUsuario()
        {
            propSeguridad.Usuario.Email = txtEmail.Text.Trim();

            propSeguridad.Usuario = ObjSessionDataUI.ObjDTUsuario;
            objBLSeguridad = new BLSeguridad(propSeguridad);
            objBLSeguridad.GetUsuario();
            ObjSessionDataUI.ObjDTUsuario.PaswordPregunta = objBLSeguridad.Data.Usuario.PaswordPregunta;
            lblPregunta.Text = ObjSessionDataUI.ObjDTUsuario.PaswordPregunta;
        }
        #endregion
    }
}