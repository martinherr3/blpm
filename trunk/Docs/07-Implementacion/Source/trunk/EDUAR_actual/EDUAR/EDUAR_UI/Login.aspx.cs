using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_UI
{
	public partial class Login : EDUARBasePage
	{
		/// <summary>
		/// Método que se ejecuta al dibujar los controles de la página.
		/// Se utiliza para gestionar las excepciones del método Page_Load().
		/// </summary>
		/// <param name="e"></param>
		//protected override void OnPreRender(EventArgs e)
		//{
		//    base.OnPreRender(e);
		//    if (AvisoMostrar)
		//    {
		//        AvisoMostrar = false;

		//        try
		//        {
		//            Master.ManageExceptions(AvisoExcepcion);
		//        }
		//        catch (Exception ex) { Master.ManageExceptions(ex); }
		//    }
		//}

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

				HttpContext.Current.User = null;
				NavigationMenu.DataSource = SiteMapAnonymusEDUAR;
				NavigationMenu.MenuItemDataBound += (NavigationMenu_OnItemBound);
				NavigationMenu.DataBind();

				//NavigationMenu.MenuItemDataBound += (NavigationMenu_OnItemBound);

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
						ObjSessionDataUI.ObjDTUsuario = objBLSeguridad.Data.Usuario;
						//ObjDTSessionDataUI.ObjDTUsuario.Password = objBLSeguridad.Data.Usuario.Password;
						Response.Redirect("~/Public/Account/ForgotPassword.aspx", false);
					}
				}
				ventanaInfoLogin.Visible = false;
			}
			catch (Exception)
			{
				//AvisoMostrar = true;
				//AvisoExcepcion = ex;
			}
		}

		/// <summary>
		/// Handles the OnItemBound event of the NavigationMenu control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="System.Web.UI.WebControls.MenuEventArgs"/> instance containing the event data.</param>
		protected void NavigationMenu_OnItemBound(object sender, MenuEventArgs args)
		{
			args.Item.ImageUrl = ((SiteMapNode)args.Item.DataItem)["ImageUrl"];
		}

		/// <summary>
		/// Valida si el nodo se debe mostrar. 
		/// Puede tener el atributo visible=false o puede que el perfil del usuario lo permita.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		protected Boolean ValidarNodo(SiteMapNode node)
		{
			//Si el nodo está marcado como visible False es porque solo se utiliza para que sea visible 
			//en el menu superior y no se debe mostrar en el menu lateral
			Boolean isVisible;
			if (bool.TryParse(node["visible"], out isVisible) && !isVisible)
				return false;

			foreach (DTRol rolUsuario in ObjSessionDataUI.ObjDTUsuario.ListaRoles)
			{
				if (node.Roles.Contains(rolUsuario.Nombre))
					return true;
			}
			return false;
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
					FormsAuthentication.SetAuthCookie(LoginUser.UserName.Trim(), false);
					ObjSessionDataUI.ObjDTUsuario = objDTSeguridad.Usuario;
					if (ObjSessionDataUI.ObjDTUsuario.EsUsuarioInicial)
						Response.Redirect("~/Private/Account/ChangePassword.aspx", true);
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
					ManageExceptions(ex);
					updVentaneMensajes.Update();
				}
				catch
				{

				}
			}
		}

		/// <summary>
		/// Método que permite tratar las excepciones de forma standard. 
		/// </summary>
		/// <param name="ex">Excepción a tratar</param>
		public void ManageExceptions(Exception ex)
		{
			try
			{
				string exceptionName = ex.GetType().FullName;
				string Titulo = string.Empty;
				string Detalle = string.Empty;
				enumTipoVentanaInformacion tipoVentana = enumTipoVentanaInformacion.Error;
				Detalle = ex.Message;

				if (exceptionName.Contains("CustomizedException"))
				{
					switch (((CustomizedException)ex).ExceptionType)
					{
						case enuExceptionType.BusinessLogicException:
							Titulo = "Error en Negocio";
							Detalle = "Se ha producido un error al realizar una acción en el negocio.";
							break;
						case enuExceptionType.SqlException:
						case enuExceptionType.MySQLException:
						case enuExceptionType.DataAccesException:
							Titulo = "Error en Base de Datos";
							Detalle = "Se ha producido un error al realizar una acción en la Base de Datos.";
							break;
						case enuExceptionType.ServicesException:
							Titulo = "Error en Servicio";
							Detalle = "Se ha producido un error al realizar la consulta al Servicio.";
							break;
						case enuExceptionType.IntegrityDataException:
							Titulo = "Error de Integridad de Datos";
							break;
						case enuExceptionType.ConcurrencyException:
							Titulo = "Error de Concurrencia";
							break;
						case enuExceptionType.ValidationException:
							//Esta es una excepcion de tipo validacion que viene de UI.
							Titulo = "Error de Validación";
							tipoVentana = enumTipoVentanaInformacion.Advertencia;
							//MostrarMensaje("Error de Validación", ex.Message, enumTipoVentanaInformacion.Advertencia);
							break;
						case enuExceptionType.SecurityException:
							Titulo = "Error de seguridad";
							tipoVentana = enumTipoVentanaInformacion.Advertencia;
							break;
						case enuExceptionType.WorkFlowException:
							break;
						case enuExceptionType.Exception:
							Titulo = "Error en la Aplicación";
							Detalle = "Se ha producido un error interno en la aplicación.";
							break;
						default:
							break;
					}
					if (Detalle != ex.Message) Detalle += " " + ex.Message;
					//Detalle += " " + ex.Message;
					MostrarMensaje(Titulo, Detalle, tipoVentana);
					if (tipoVentana != enumTipoVentanaInformacion.Advertencia)
						ventanaInfoLogin.GestionExcepcionesLog(ex);
				}
				//Esta es una excepcion de tipo validacion que viene de BL.
				else if ((exceptionName.Contains("GenericException")))
				{
					///GenericException genericEx = ((GenericException)ex).Detail;
					if (((CustomizedException)ex).ExceptionType == enuExceptionType.ValidationException)
						MostrarMensaje("Error de Validación", ex.Message, enumTipoVentanaInformacion.Advertencia);
					else
						ventanaInfoLogin.GestionExcepciones(ex);
				}
				else
					ventanaInfoLogin.GestionExcepciones(ex);

				// Refrescar updatepanel
				updVentaneMensajes.Update();
			}
			catch (Exception exNew)
			{
				ventanaInfoLogin.GestionExcepciones(exNew);
			}
		}

		/// <summary>
		/// Metodo que se encarga de mostrar mensajes en la aplicacion.
		/// </summary>
		/// <param name="titulo"></param>
		/// <param name="detalle"></param>
		/// <param name="tipoventana"></param>
		public void MostrarMensaje(string titulo, string detalle, enumTipoVentanaInformacion tipoventana)
		{
			try
			{
				ventanaInfoLogin.TipoVentana = tipoventana;
				ventanaInfoLogin.Titulo = titulo;
				ventanaInfoLogin.Detalle = detalle;
				ventanaInfoLogin.MostrarMensaje();

				// Refrescar updatepanel
				updVentaneMensajes.Update();

			}
			catch (Exception ex)
			{
				ManageExceptions(ex);
			}
		}
	}
}
