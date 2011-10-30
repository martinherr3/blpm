using System;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;

namespace EDUAR_UI
{
	public partial class ChangeEmail : EDUARBasePage
	{
		#region --[Atributos]--
		private BLSeguridad atrBLSeguridad;
		private BLPersona atrBLPersona;
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
		/// Propiedad que contiene el objeto usuario que se edita.
		/// </summary>
		/// <value>
		/// The obj usuario.
		/// </value>
		public DTUsuario propUsuario
		{
			get
			{
				if (ViewState["propUsuario"] == null)
					propUsuario = new DTUsuario();

				return (DTUsuario)ViewState["propUsuario"];
			}
			set { ViewState["propUsuario"] = value; }
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
					lblEmailActual.Text = ObjSessionDataUI.ObjDTUsuario.Email;
					//CargarPresentacion();
					//CargarCamposFiltros();
					//BuscarUsuarios(propSeguridad.Usuario);
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
				switch (AccionPagina)
				{
					case enumAcciones.Guardar:
						GuardarUsuario();
						AccionPagina = enumAcciones.Redirect;
						Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
						//LimpiarCampos();
						//udpRoles.Visible = false;
						break;
					case enumAcciones.Redirect:
						Response.Redirect("~/Private/Account/Welcome.aspx", false);
						break;
					case enumAcciones.Salir:
						Response.Redirect("~/Default.aspx", false);
						break;
					default:
						break;
				}
				//udpRoles.Update();
				//udpGrilla.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnAsignarRol control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				//if (ValidarPagina())
				//{
				if (Page.IsValid)
				{
					AccionPagina = enumAcciones.Guardar;
					Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
				}
				else
				{
					AccionPagina = enumAcciones.Limpiar;
					Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosRequeridos, enumTipoVentanaInformacion.Advertencia);
				}
				//}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Métodos Privados]
		/// <summary>
		/// Guardars the usuario.
		/// </summary>
		private void GuardarUsuario()
		{
			DTSeguridad objSeguridad = new DTSeguridad();
			objSeguridad.Usuario = ObjSessionDataUI.ObjDTUsuario;
			objSeguridad.Usuario.Email = txtEmail.Text;
			atrBLSeguridad = new BLSeguridad(objSeguridad);
			atrBLSeguridad.ActualizarEmail();
		}
		#endregion
	}
}