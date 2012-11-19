using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_Entities;
using System.Globalization;
using EDUAR_BusinessLogic.Encuestas;

namespace EDUAR_UI
{
	public partial class ResultadosEncuestas : EDUARBasePage
	{
		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the encuesta sesion.
		/// </summary>
		/// <value>
		/// The encuesta sesion.
		/// </value>
		public Encuesta encuestaSesion
		{
			get
			{
				if (Session["encuestaSesion"] == null)
					encuestaSesion = new Encuesta();

				return (Encuesta)Session["encuestaSesion"];
			}
			set { Session["encuestaSesion"] = value; }
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
					lblTitulo.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(encuestaSesion.nombreEncuesta);
					CargarEncabezado();
					BuscarPreguntas();
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
				Response.Redirect("ManageContenidoEncuestas.aspx", false);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Métodos Privados]--
		private void CargarEncabezado()
		{
			lblFechaLanzamiento.Text = Convert.ToDateTime(encuestaSesion.fechaLanzamiento).ToShortDateString();
			if (encuestaSesion.fechaVencimiento.HasValue)
				lblFechaExpiracion.Text = Convert.ToDateTime(encuestaSesion.fechaVencimiento).ToShortDateString();

			BLEncuesta objBLEncuesta = new BLEncuesta();
			EncuestaAnalisis miAnalisis = objBLEncuesta.GetEncuestaAnalisis(encuestaSesion);
			if (miAnalisis != null)
			{
				lblEnviadas.Text = miAnalisis.nroLanzadas.ToString();
				lblRespondidas.Text = miAnalisis.nroRespondidas.ToString();
				if (Convert.ToDateTime(encuestaSesion.fechaVencimiento).Subtract(DateTime.Today).Days > 0)
					lblPendientes.Text = miAnalisis.nroPendientes.ToString();
				else
				{
					lblEncuestasPendientes.Text = "Encuestas Expiradas: ";
					lblPendientes.Text = miAnalisis.nroExpiradas.ToString();
				}
			}
		}

		private void BuscarPreguntas()
		{

		}
		#endregion
	}
}