using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;

namespace EDUAR_UI
{
	public partial class MsjeRedactar : EDUARBasePage
	{
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
					BLPersona objpersona = new BLPersona();
					List<Persona> lista = objpersona.GetPersonas(new Persona() { activo = true });
					lista.Sort((p, q) => string.Compare(p.apellido + " " + p.nombre, q.apellido + " " + q.nombre));

					foreach (Persona item in lista)
					{
						string[] nombre;
						nombre = item.nombre.Trim().Split(' ');
						string nuevoNombre = string.Empty;
						for (int i = 0; i < nombre.Length; i++)
						{
							nuevoNombre += " " + nombre[i].ToUpper().Substring(0, 1) + nombre[i].ToLower().Substring(1, nombre[i].Length - 1);
						}
						nuevoNombre = nuevoNombre.Trim();
						ddlDestino.Items.Add(new System.Web.UI.WebControls.ListItem(item.apellido.ToUpper() + ", " + nuevoNombre, item.idPersona.ToString()));
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
		/// Ventanas the aceptar.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void VentanaAceptar(object sender, EventArgs e)
		{
			try
			{
				switch (AccionPagina)
				{
					case enumAcciones.Salir:
						Response.Redirect("MsjeEntrada.aspx", false);
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnEnviar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnEnviar_Click(object sender, EventArgs e)
		{
			try
			{
				Mensaje objMensaje = new Mensaje();

				objMensaje.asuntoMensaje = txtAsunto.Value;
				objMensaje.textoMensaje = textoMensaje.contenido;
				objMensaje.remitente.username = ObjDTSessionDataUI.ObjDTUsuario.Nombre;
				objMensaje.fechaEnvio = DateTime.Now;
				objMensaje.horaEnvio = Convert.ToDateTime(DateTime.Now.Hour + ":" + DateTime.Now.Minute);

				Persona destinatario;
				int cantidad = 0;
				foreach (ListItem item in ddlDestino.Items)
				{
					if (item.Selected)
					{
						destinatario = new Persona();
						destinatario.idPersona = Convert.ToInt32(item.Value);
						objMensaje.ListaDestinatarios.Add(destinatario);
						cantidad++;
					}
				}
				BLMensaje objBLMensaje = new BLMensaje(objMensaje);
				objBLMensaje.Save();
				AccionPagina = enumAcciones.Salir;

				if (cantidad == 1)
					Master.MostrarMensaje("Mensaje Enviado", UIConstantesGenerales.MensajeUnicoDestino, enumTipoVentanaInformacion.Satisfactorio);
				else
					Master.MostrarMensaje("Mensaje Enviado", UIConstantesGenerales.MensajeMultiDestino, enumTipoVentanaInformacion.Satisfactorio);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnVolver control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnVolver_Click(object sender, EventArgs e)
		{
			try
			{
				Response.Redirect("MsjeEntrada.aspx", false);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion
	}
}