using System;
using EDUAR_UI.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Entities.Security;
using EDUAR_BusinessLogic.Security;

namespace EDUAR_UI
{
	public partial class ChangePassword : EDUARBasePage
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
				Master.BotonAvisoAceptar += (VentanaAceptar);
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
		/// Handles the Click event of the CancelPushButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void CancelPushButton_Click(object sender, EventArgs e)
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

		protected void ChangePasswordPushButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				DTSeguridad objDTSeguridad = new DTSeguridad
				{
					Usuario =
					{
						Nombre = ObjSessionDataUI.ObjDTUsuario.Nombre,
						Password = BLEncriptacion.Encrypt(ChangeUserPassword.CurrentPassword.Trim()),
						PasswordNuevo = BLEncriptacion.Encrypt(ChangeUserPassword.NewPassword.Trim())
					}
				};

				BLSeguridad objSeguridadBL = new BLSeguridad(objDTSeguridad);
				objSeguridadBL.CambiarPassword();
				Response.Redirect("~/Private/Account/ChangePasswordSuccess.aspx", false);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
	}
}
