using System;
using System.Web.Security;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
	public partial class Register : EDUARBasePage
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

		/// <summary>
		/// Propiedad que contiene el objeto seguridad que devuelve la consulta a la Capa de Negocio.
		/// </summary>
		public Persona propPersona
		{
			get
			{
				if (Session["propPersona"] == null)
					return null;

				return (Persona)Session["propPersona"];
			}
			set { Session["propPersona"] = value; }
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
				RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
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

			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the CreatedUser event of the RegisterUser control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void RegisterUser_CreatedUser(object sender, EventArgs e)
		{
			propSeguridad = new DTSeguridad();
			propSeguridad.Usuario.Nombre = RegisterUser.UserName;
			//Personal = 1,
			//Alumno = 2,
			//Tutor = 3
			DTRol rol = new DTRol();

			switch (propPersona.idTipoPersona)
			{
				case 1:
					rol.Nombre = enumRoles.Administrativo.ToString();
					break;
				case 2:
					rol.Nombre = enumRoles.Alumno.ToString();
					break;
				case 3:
					rol.Nombre = enumRoles.Tutor.ToString();
					break;
			}
			//asigna un rol por defecto, en función de la persona
			propSeguridad.Usuario.ListaRoles.Add(rol);
			propSeguridad.Usuario.Aprobado = true;
			propSeguridad.Usuario.EsUsuarioInicial = false;
			objBLSeguridad = new BLSeguridad(propSeguridad);
			objBLSeguridad.ActualizarUsuario();

			//actualiza el nombre de usuario en la persona
			propPersona.username = propSeguridad.Usuario.Nombre;
			BLPersona objBLPersona = new BLPersona(propPersona);
			objBLPersona.Save();

			//loquea al usuario y lo redirecciona a la pagina de inicio de usuarios logueados
			FormsAuthentication.SignOut();
			FormsAuthentication.Initialize();
			FormsAuthentication.SetAuthCookie(RegisterUser.UserName, true /* createPersistentCookie */);
			ObjSessionDataUI.ObjDTUsuario = propSeguridad.Usuario;

			string continueUrl = RegisterUser.ContinueDestinationPageUrl;
			if (string.IsNullOrEmpty(continueUrl))
			{
				continueUrl = "~/Private/Account/Welcome.aspx";
			}
			Response.Redirect(continueUrl, false);
		}
		#endregion
	}
}
