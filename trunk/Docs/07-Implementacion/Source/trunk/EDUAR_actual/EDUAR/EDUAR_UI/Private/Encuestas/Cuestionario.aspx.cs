using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using EDUAR_BusinessLogic.Encuestas;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
	public partial class Cuestionario : EDUARBasePage
	{
		#region --[Atributos]--
		BLEncuesta objBLEncuesta;
		BLPregunta objBLPregunta;
		BLRespuesta objBLRespuesta;
		BLEncuestaDisponible objBLEncuestaDisponible;
		#endregion

		#region --[Propiedades]--

		/// <summary>
		/// Gets or sets the encuesta en sesion.
		/// </summary>
		/// <value>
		/// The encuesta en sesion.
		/// </value>
		public EncuestaDisponible encuestaSeleccionada
		{
			get
			{
				if (ViewState["encuestaSeleccionada"] == null)
					encuestaSeleccionada = new EncuestaDisponible();

				return (EncuestaDisponible)ViewState["encuestaSeleccionada"];
			}
			set { ViewState["encuestaSeleccionada"] = value; }
		}

		/// <summary>
		/// Gets or sets the id pregunta con la finalidad de mantener el track de la respuesta.
		/// </summary>
		/// <value>
		/// The id pregunta.
		/// </value>
		public Respuesta respuestaSkeleton
		{
			get
			{
				if (ViewState["respuestaSkeleton"] == null)
					ViewState["respuestaSkeleton"] = new Respuesta();
				return (Respuesta)ViewState["respuestaSkeleton"];
			}
			set { ViewState["respuestaSkeleton"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista respuestas.
		/// </summary>
		/// <value>
		/// The lista respuestas.
		/// </value>
		public List<Respuesta> ListaRespuestas
		{
			get
			{
				if (Session["respuestas"] == null)
					Session["respuestas"] = new List<Respuesta>();

				return (List<Respuesta>)Session["respuestas"];
			}
			set { Session["respuestas"] = value; }
		}

		/// <summary>
		/// Gets or sets the cant respuestas minimas.
		/// </summary>
		/// <value>
		/// The cant respuestas minimas.
		/// </value>
		public int cantRespuestasMinimas
		{
			get
			{
				if (ViewState["cantRespuestasMinimas"] == null)
					ViewState["cantRespuestasMinimas"] = 0;

				return (int)ViewState["cantRespuestasMinimas"];
			}
			set { ViewState["cantRespuestasMinimas"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista categorias.
		/// </summary>
		/// <value>
		/// The lista categorias.
		/// </value>
		public List<CategoriaPregunta> listaCategorias
		{
			get
			{
				if (ViewState["listaCategorias"] == null)
					ViewState["listaCategorias"] = new List<CategoriaPregunta>();

				return (List<CategoriaPregunta>)ViewState["listaCategorias"];
			}
			set { ViewState["listaCategorias"] = value; }
		}

		/// <summary>
		/// Gets or sets the encuesta puntual.
		/// </summary>
		/// <value>
		/// The encuesta puntual.
		/// </value>
		public Encuesta encuestaPuntual
		{
			get
			{
				if (ViewState["encuestaPuntual"] == null)
					ViewState["encuestaPuntual"] = new Encuesta();
				return (Encuesta)ViewState["encuestaPuntual"];
			}
			set { ViewState["encuestaPuntual"] = value; }
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
					cargarEncabezado();
					LimpiarPantalla();
				}
				else
				{
					int idEncuestaSeleccionada;

					if (Int32.TryParse(ddlEncuesta.SelectedValue, out idEncuestaSeleccionada)
						&&
						(AccionPagina == enumAcciones.Buscar
						||
						AccionPagina == enumAcciones.Responder)
						)
						CargarEncuesta(idEncuestaSeleccionada);
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
				int idEncuestaSeleccionada;

				if (Int32.TryParse(ddlEncuesta.SelectedValue, out idEncuestaSeleccionada)
					&&
					(AccionPagina == enumAcciones.Buscar
					||
					AccionPagina == enumAcciones.Responder)
					)
					CargarEncuesta(idEncuestaSeleccionada);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnBuscar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			try
			{
				LimpiarPantalla();
				int idEncuesta = 0;
				if (int.TryParse(ddlEncuesta.SelectedValue, out idEncuesta) && idEncuesta > 0)
				{
					CargarEncuesta(idEncuesta);
					AccionPagina = enumAcciones.Buscar;
					udpFormulario.Visible = true;
				}
				udpFormulario.Update();
			}
			catch (Exception ex)
			{
				AccionPagina = enumAcciones.Limpiar;
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnGuardar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				if (ValidarPagina() == string.Empty)
					GuardarRespuestas();
				else
					Master.MostrarMensaje("Error de Validación", "Existen preguntas sin responder", enumTipoVentanaInformacion.Advertencia);
			}
			catch (Exception ex)
			{
				AccionPagina = enumAcciones.Limpiar;
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Guardars the respuestas.
		/// </summary>
		private void GuardarRespuestas()
		{
			// REGISTRAR QUE LA ENCUESTA DISPONIBLE HA SIDO RESPONDIDA
			encuestaSeleccionada.respondida = true;
			encuestaSeleccionada.fechaRespuesta = DateTime.Now;

			encuestaSeleccionada.listaRespuestas = ListaRespuestas;

			objBLEncuestaDisponible = new BLEncuestaDisponible(encuestaSeleccionada);
			objBLEncuestaDisponible.Save();

		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			int contRespuestas = 0;
			foreach (Respuesta item in ListaRespuestas)
			{
				if (item.respuestaSeleccion > 0) contRespuestas++;
			}
			if (contRespuestas == cantRespuestasMinimas)
				return string.Empty;
			else
				return "FaltanDatos";
		}

		/// <summary>
		/// Handles the Click event of the btnCancelar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnCancelar_Click(object sender, EventArgs e)
		{
			try
			{
				ddlEncuesta.SelectedIndex = 0;
				LimpiarPantalla();
				udpFormulario.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Changed event of the rating control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="AjaxControlToolkit.RatingEventArgs"/> instance containing the event data.</param>
		protected void rating_Changed(object sender, RatingEventArgs e)
		{
			try
			{
				AccionPagina = enumAcciones.Responder;
				Respuesta respuestaPuntual = new Respuesta();
				respuestaPuntual = respuestaSkeleton;

				respuestaPuntual.respuestaSeleccion = Convert.ToInt16(e.Value);
				ListaRespuestas.Add(respuestaPuntual);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Changed event of the text control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void text_Changed(object sender, EventArgs e)
		{
			try
			{
				AccionPagina = enumAcciones.Responder;

				string valor = ((TextBox)sender).Text;

				Respuesta respuestaPuntual = new Respuesta();
				respuestaPuntual = respuestaSkeleton;

				respuestaPuntual.respuestaTextual = valor;
				ListaRespuestas.Add(respuestaPuntual);
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
		public void cargarEncabezado()
		{
			CargarCombos();
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		private void CargarCombos()
		{
			objBLEncuestaDisponible = new BLEncuestaDisponible();

			EncuestaDisponible encuestaSkeleton = new EncuestaDisponible();
			encuestaSkeleton.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

			UIUtilidades.BindCombo<Encuesta>(ddlEncuesta, objBLEncuestaDisponible.GetEncuestasDisponibles(encuestaSkeleton), "idEncuesta", "nombreEncuesta", true);
		}

		/// <summary>
		/// Limpiars the pantalla.
		/// </summary>
		private void LimpiarPantalla()
		{
			//ddlEncuesta.SelectedIndex = 0;
			cantRespuestasMinimas = 0;
			encuestaPuntual = new Encuesta();
			listaCategorias = new List<CategoriaPregunta>();
			AccionPagina = enumAcciones.Limpiar;
			CuestionarioAccordion.Panes.Clear();
			lblNombreEncuesta.Text = string.Empty;
			udpFormulario.Visible = false;
		}

		/// <summary>
		/// Cargars the encuesta.
		/// </summary>
		/// <param name="idEncuestaSeleccionada">The id encuesta seleccionada.</param>
		private void CargarEncuesta(int idEncuestaSeleccionada)
		{
			cantRespuestasMinimas = 0;
			objBLEncuesta = new BLEncuesta();
			objBLEncuestaDisponible = new BLEncuestaDisponible();

			//OBTENGO LA ENCUESTA PUNTUAL
			if (encuestaPuntual.idEncuesta == 0)
				encuestaPuntual = objBLEncuestaDisponible.GetEncuestasDisponibles(encuestaSeleccionada).Find(c => c.idEncuesta == idEncuestaSeleccionada);

			encuestaSeleccionada.encuesta = encuestaPuntual;
			encuestaSeleccionada.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

			//PRECARGA DE ATRIBUTOS DE LA RESPUESTA SKELETON
			respuestaSkeleton.encuestaDisponible.encuesta = encuestaPuntual;
			respuestaSkeleton.encuestaDisponible.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

			generarEsqueleto(encuestaPuntual);
		}

		/// <summary>
		/// Generars the esqueleto.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void generarEsqueleto(Encuesta entidad)
		{
			objBLEncuesta = new BLEncuesta();

			//objBLEncuestaDisponible = new BLEncuestaDisponible();

			objBLPregunta = new BLPregunta();

			//List<CategoriaPregunta> listaCategorias = objBLEncuesta.GetCategoriasPorEncuesta(encuestaSeleccionada.encuesta);

			if (listaCategorias.Count == 0)
				listaCategorias = objBLEncuesta.GetCategoriasPorEncuesta(entidad);

			//lblNombreEncuesta.Text = encuestaSeleccionada.encuesta.nombreEncuesta;
			lblNombreEncuesta.Text = entidad.nombreEncuesta;

			Label lblCategoria;

			AjaxControlToolkit.AccordionPane pn;
			int i = 0;
			int contador = 0;

			foreach (CategoriaPregunta categoria in listaCategorias)
			{
				List<Pregunta> preguntasPorCategoria = objBLPregunta.GetPreguntasPorCategoria(categoria, entidad);

				lblCategoria = new Label();

				lblCategoria.Text = categoria.nombre;

				pn = new AjaxControlToolkit.AccordionPane();
				pn.ID = "Panel_" + i;

				pn.HeaderContainer.Controls.Add(lblCategoria);

				if (preguntasPorCategoria.Count > 0)
				{
					Label lblPregunta;

					foreach (Pregunta pregunta in preguntasPorCategoria)
					{
						contador++;
						Panel panelRespuesta = new Panel();
						panelRespuesta.ID = "pregunta_" + contador.ToString();

						//PREGUNTA
						lblPregunta = new Label();

						lblPregunta.Text = pregunta.textoPregunta;
						lblPregunta.Font.Bold = true;
						lblPregunta.Font.Size = 11;
						lblPregunta.BorderWidth = 1;
						lblPregunta.Width = 990;

						panelRespuesta.Controls.Add(lblPregunta);
						panelRespuesta.Controls.Add(new LiteralControl("<br/>"));

						respuestaSkeleton.pregunta = pregunta;

						//RESPUESTA
						if (pregunta.escala.nombre.Equals("Conceptual literal"))
						{
							TextBox txtRespuesta = new TextBox();
							txtRespuesta.ID = "respuesta_" + pregunta.idPregunta.ToString();
							txtRespuesta.Rows = 5;
							txtRespuesta.Columns = 75;
							txtRespuesta.AutoPostBack = false;
							txtRespuesta.CssClass = "txtMultilinea99";
							txtRespuesta.TextMode = TextBoxMode.MultiLine;
							txtRespuesta.Wrap = false;
							txtRespuesta.MaxLength = 4000;

							txtRespuesta.TextChanged += new EventHandler(this.text_Changed);

							panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
							panelRespuesta.Controls.Add(txtRespuesta);
							panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
						}
						else
						{
							AjaxControlToolkit.Rating rating = new AjaxControlToolkit.Rating();

							rating.ID = "respuesta_" + pregunta.idPregunta.ToString();
							rating.MaxRating = 5;

							rating.StarCssClass = "ratingStar";
							rating.WaitingStarCssClass = "savedRatingStar";
							rating.FilledStarCssClass = "filledRatingStar";
							rating.EmptyStarCssClass = "emptyRatingStar";
							rating.AutoPostBack = false;

							rating.Changed += new AjaxControlToolkit.RatingEventHandler(rating_Changed);

							panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
							panelRespuesta.Controls.Add(rating);
							panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
							cantRespuestasMinimas++;
						}

						pn.ContentContainer.Controls.Add(panelRespuesta);
						pn.ContentContainer.Controls.Add(new LiteralControl("<br/>"));
					}
				}
				CuestionarioAccordion.Panes.Add(pn);
				++i;
			}
		}

		/// <summary>
		/// Finds the control recursive.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		//private static Control FindControlRecursive(Control container, string name)
		//{
		//    if ((container.ID != null) && (container.ID.Equals(name)))
		//        return container;

		//    foreach (Control ctrl in container.Controls)
		//    {
		//        Control foundCtrl = FindControlRecursive(ctrl, name);
		//        if (foundCtrl != null)
		//            return foundCtrl;
		//    }
		//    return null;
		//}
		#endregion
	}
}