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
using AjaxControlToolkit;
using EDUAR_Utility.Enumeraciones;

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
		/// <summary>
		/// Cargars the encabezado.
		/// </summary>
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

		/// <summary>
		/// Buscars the preguntas.
		/// </summary>
		private void BuscarPreguntas()
		{
			List<RespuestaPreguntaAnalisis> listaRespuesta = new BLRespuesta().GetRespuestaPreguntaAnalisis(encuestaSesion);
			//gvwPreguntas.DataSource = listaRespuesta;
			//gvwPreguntas.DataBind();
			//udpPreguntas.Update();

			AccordionPane panel;
			Label lblCategoria;
			foreach (RespuestaPreguntaAnalisis respuesta in listaRespuesta)
			{
				lblCategoria = new Label();

				lblCategoria.Text = respuesta.textoPregunta;

				panel = new AjaxControlToolkit.AccordionPane();
				panel.ID = "Panel_" + respuesta.idPregunta;

				panel.HeaderContainer.Controls.Add(lblCategoria);

				Table tabla = new Table();
				TableRow fila = new TableRow();
				TableCell celda = new TableCell();
				if (respuesta.idEscalaPonderacion == 1)
				{
					enumRespCualitativa cant1 = (enumRespCualitativa)1;
					celda.Text = cant1.ToString().Replace("_", " ");
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant1.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
					enumRespCualitativa cant2 = (enumRespCualitativa)2;
					fila = new TableRow();
					celda = new TableCell();
					celda.Text = cant2.ToString();
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant2.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
					enumRespCualitativa cant3 = (enumRespCualitativa)3;
					fila = new TableRow();
					celda = new TableCell();
					celda.Text = cant3.ToString();
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant3.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
					enumRespCualitativa cant4 = (enumRespCualitativa)4;
					fila = new TableRow();
					celda = new TableCell();
					celda.Text = cant4.ToString();
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant4.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
					enumRespCualitativa cant5 = (enumRespCualitativa)5;
					fila = new TableRow();
					celda = new TableCell();
					celda.Text = cant5.ToString().Replace("_", " ");
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant5.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
				}
				else
				{
					enumRespCuantitativa cant1 = (enumRespCuantitativa)1;
					celda.Text = cant1.ToString();
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant1.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
					enumRespCuantitativa cant2 = (enumRespCuantitativa)2;
					fila = new TableRow();
					celda = new TableCell();
					celda.Text = cant2.ToString();
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant2.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
					enumRespCuantitativa cant3 = (enumRespCuantitativa)3;
					fila = new TableRow();
					celda = new TableCell();
					celda.Text = cant3.ToString();
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant3.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
					enumRespCuantitativa cant4 = (enumRespCuantitativa)4;
					fila = new TableRow();
					celda = new TableCell();
					celda.Text = cant4.ToString();
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant4.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
					enumRespCuantitativa cant5 = (enumRespCuantitativa)5;
					fila = new TableRow();
					celda = new TableCell();
					celda.Text = cant5.ToString();
					fila.Cells.Add(celda);
					celda = new TableCell();
					celda.Text = respuesta.cant5.ToString();
					fila.Cells.Add(celda);
					tabla.Rows.Add(fila);
				}

				panel.ContentContainer.Controls.Add(tabla);
				//panel.ContentContainer.Controls.Add(new LiteralControl("<br/>"));
				CuestionarioAccordion.Panes.Add(panel);

			}
		}
		#endregion
	}
}