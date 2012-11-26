using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using EDUAR_BusinessLogic.Encuestas;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using System.Web.UI;

namespace EDUAR_UI
{
	/// <summary>
	/// 
	/// </summary>
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

		/// <summary>
		/// Gets or sets the lista respuesta.
		/// </summary>
		/// <value>
		/// The lista respuesta.
		/// </value>
		public List<RespuestaPreguntaAnalisis> listaRespuestaNumericas
		{
			get
			{
				if (ViewState["listaRespuestaNumericas"] == null)
					listaRespuestaNumericas = new List<RespuestaPreguntaAnalisis>();

				return (List<RespuestaPreguntaAnalisis>)ViewState["listaRespuestaNumericas"];
			}
			set { ViewState["listaRespuestaNumericas"] = value; }
		}

		public List<RespuestaPreguntaAnalisis> listaRespuestaTextuales
		{
			get
			{
				if (ViewState["listaRespuestaTextuales"] == null)
					listaRespuestaTextuales = new List<RespuestaPreguntaAnalisis>();

				return (List<RespuestaPreguntaAnalisis>)ViewState["listaRespuestaTextuales"];
			}
			set { ViewState["listaRespuestaTextuales"] = value; }
		}

		/// <summary>
		/// Gets or sets the titulo reporte.
		/// </summary>
		/// <value>
		/// The titulo reporte.
		/// </value>
		public string tituloReporte
		{
			get
			{
				if (Session["tituloReporte"] == null)
					Session["tituloReporte"] = string.Empty;
				return Session["tituloReporte"].ToString();
			}
			set { Session["tituloReporte"] = value; }
		}

		/// <summary>
		/// Gets or sets the filtros aplicados.
		/// </summary>
		/// <value>
		/// The filtros aplicados.
		/// </value>
		public string filtrosAplicados
		{
			get
			{
				if (Session["filtrosAplicados"] == null)
					filtrosAplicados = string.Empty;
				return Session["filtrosAplicados"].ToString();
			}
			set
			{
				Session["filtrosAplicados"] = value;
			}
		}
		#endregion

		#region --[Estructura]--
		[Serializable]
		public struct miRespuesta
		{
			public string respuesta { get; set; }
			public decimal cantidad { get; set; }
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
				//if (!Page.IsPostBack)
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

			lblCurso.Text = encuestaSesion.curso.curso.nombre;
			if (!string.IsNullOrEmpty(encuestaSesion.asignatura.asignatura.nombre))
			{
				lblAsignatura.Visible = true;
				lblAsignaturaNombre.Text = encuestaSesion.asignatura.asignatura.nombre;
			}
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
			#region --Preguntas Numéricas--

			listaRespuestaNumericas = new BLRespuesta().GetRespuestaPreguntaAnalisis(encuestaSesion);

			AccordionPane panel;
			Label lblCategoria;
			Table tabla = new Table();

			TableRow fila = new TableRow();
			TableCell celda = new TableCell();
			List<string> textoPregunta = new List<string>();

			foreach (RespuestaPreguntaAnalisis respuesta in listaRespuestaNumericas)
			{
				tabla = new Table();
				tabla.Width = Unit.Percentage(100);
				fila = new TableRow();
				celda = new TableCell();

				textoPregunta = new List<string>();

				textoPregunta = UIUtilidades.StringWrap(respuesta.textoPregunta, 130);

				lblCategoria = new Label();

				foreach (string item in textoPregunta)
					lblCategoria.Text += item + "<br />";

				//lblCategoria.Text = respuesta.textoPregunta;
				tabla.BorderStyle = BorderStyle.None;
				celda.Controls.Add(lblCategoria);
				celda.Width = Unit.Percentage(90);
				celda.HorizontalAlign = HorizontalAlign.Left;

				fila.Cells.Add(celda);

				celda = new TableCell();
				lblCategoria = new Label();
				lblCategoria.Text = "[Relevancia: " + respuesta.relevancia.ToString().PadLeft(5, 'x') + "%]";
				lblCategoria.Text = lblCategoria.Text.Replace("x", "&nbsp;&nbsp;");

				celda.Controls.Add(lblCategoria);
				celda.HorizontalAlign = HorizontalAlign.Right;

				fila.Cells.Add(celda);
				tabla.Rows.Add(fila);

				panel = new AjaxControlToolkit.AccordionPane();
				panel.ID = "Panel_" + respuesta.idPregunta;

				panel.HeaderContainer.Controls.Add(tabla);
				panel.HeaderContainer.HorizontalAlign = HorizontalAlign.Left;

				ImageButton btnGraficar = new ImageButton();
				btnGraficar.ID = "btnGraficar_" + respuesta.idPregunta.ToString();
				btnGraficar.ToolTip = "Ver Gráfico";
				btnGraficar.ImageUrl = "~/Images/GraficarEncuesta.png";

				btnGraficar.CommandArgument = respuesta.idPregunta.ToString();
				btnGraficar.CommandName = "Graficar";
				btnGraficar.Command += new CommandEventHandler(btnGraficar_Command);

				List<miRespuesta> listaRespuestasPregunta = ObtenerRespuestas(respuesta);

				tabla = new Table();
				tabla.Width = Unit.Percentage(70);

				fila = new TableRow();
				celda = new TableCell();

				GridView grilla = new GridView();
				grilla.CssClass = "DatosLista";
				grilla.SkinID = "gridviewSkinPagerListado";
				grilla.AutoGenerateColumns = true;
				grilla.Width = Unit.Percentage(30);
				grilla.DataSource = listaRespuestasPregunta;
				grilla.DataBind();

				celda.Controls.Add(grilla);
				fila.Cells.Add(celda);

				celda = new TableCell();
				grilla.Width = Unit.Percentage(30);
				celda.VerticalAlign = VerticalAlign.Middle;
				celda.HorizontalAlign = HorizontalAlign.Center;
				celda.Controls.Add(btnGraficar);

				celda.Controls.Add(new LiteralControl(@"<br/><div class='loginDisplay' style='text-align: center'>"));

				celda.Controls.Add(new LiteralControl("[ "));

				LinkButton miLink = new LinkButton();
				miLink.Text = "Ver Gráfico";
				miLink.CommandArgument = respuesta.idPregunta.ToString();
				miLink.CommandName = "Graficar";
				miLink.Command += new CommandEventHandler(btnGraficar_Command);

				celda.Controls.Add(miLink);
				celda.Controls.Add(new LiteralControl(" ]</div>"));

				fila.Cells.Add(celda);

				tabla.Rows.Add(fila);

				panel.ContentContainer.Controls.Add(tabla);

				CuestionarioAccordion.Panes.Add(panel);
			}
			#endregion

			#region --Preguntas Textuales--

			listaRespuestaTextuales = new BLRespuesta().GetRespuestaPreguntaTextual(encuestaSesion);

			foreach (RespuestaPreguntaAnalisis respuesta in listaRespuestaTextuales)
			{
				tabla = new Table();
				tabla.Width = Unit.Percentage(100);
				fila = new TableRow();
				celda = new TableCell();

				textoPregunta = new List<string>();

				textoPregunta = UIUtilidades.StringWrap(respuesta.textoPregunta, 130);

				lblCategoria = new Label();

				foreach (string item in textoPregunta)
					lblCategoria.Text += item + "<br />";

				//lblCategoria.Text = respuesta.textoPregunta;
				tabla.BorderStyle = BorderStyle.None;
				celda.Controls.Add(lblCategoria);
				celda.Width = Unit.Percentage(90);
				celda.HorizontalAlign = HorizontalAlign.Left;

				fila.Cells.Add(celda);

				celda = new TableCell();
				lblCategoria = new Label();
				lblCategoria.Text = "[Relevancia: " + respuesta.relevancia.ToString().PadLeft(5, 'x') + "%]";
				lblCategoria.Text = lblCategoria.Text.Replace("x", "&nbsp;&nbsp;");

				celda.Controls.Add(lblCategoria);
				celda.HorizontalAlign = HorizontalAlign.Right;

				fila.Cells.Add(celda);
				tabla.Rows.Add(fila);

				panel = new AjaxControlToolkit.AccordionPane();
				panel.ID = "Panel_" + respuesta.idPregunta;

				panel.HeaderContainer.Controls.Add(tabla);
				panel.HeaderContainer.HorizontalAlign = HorizontalAlign.Left;

				ImageButton btnGraficar = new ImageButton();
				btnGraficar.ID = "btnGraficar_" + respuesta.idPregunta.ToString();
				btnGraficar.ToolTip = "Ver Gráfico";
				btnGraficar.ImageUrl = "~/Images/GraficarEncuesta.png";

				btnGraficar.CommandArgument = respuesta.idPregunta.ToString();
				btnGraficar.CommandName = "Graficar";
				btnGraficar.Command += new CommandEventHandler(btnGraficar_Command);

				List<miRespuesta> listaRespuestasPregunta = ObtenerRespuestas(respuesta);

				tabla = new Table();
				tabla.Width = Unit.Percentage(70);

				fila = new TableRow();
				celda = new TableCell();

				GridView grilla = new GridView();
				grilla.CssClass = "DatosLista";
				grilla.SkinID = "gridviewSkinPagerListado";
				grilla.AutoGenerateColumns = true;
				grilla.Width = Unit.Percentage(30);
				grilla.DataSource = listaRespuestasPregunta;
				grilla.DataBind();

				celda.Controls.Add(grilla);
				fila.Cells.Add(celda);

				celda = new TableCell();
				grilla.Width = Unit.Percentage(30);
				celda.VerticalAlign = VerticalAlign.Middle;
				celda.HorizontalAlign = HorizontalAlign.Center;
				celda.Controls.Add(btnGraficar);

				celda.Controls.Add(new LiteralControl(@"<br/><div class='loginDisplay' style='text-align: center'>"));

				celda.Controls.Add(new LiteralControl("[ "));

				LinkButton miLink = new LinkButton();
				miLink.Text = "Ver Gráfico";
				miLink.CommandArgument = respuesta.idPregunta.ToString();
				miLink.CommandName = "Graficar";
				miLink.Command += new CommandEventHandler(btnGraficar_Command);

				celda.Controls.Add(miLink);
				celda.Controls.Add(new LiteralControl(" ]</div>"));

				fila.Cells.Add(celda);

				tabla.Rows.Add(fila);

				panel.ContentContainer.Controls.Add(tabla);

				CuestionarioAccordion.Panes.Add(panel);
			}
			#endregion
		}

		/// <summary>
		/// Handles the Command event of the btnGraficar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
		void btnGraficar_Command(object sender, CommandEventArgs e)
		{
			if (e.CommandName == "Graficar")
			{
				RespuestaPreguntaAnalisis miPregunta = listaRespuestaNumericas.Find(p => p.idPregunta == Convert.ToInt32(e.CommandArgument));

				if (miPregunta == null)
					miPregunta = listaRespuestaTextuales.Find(p => p.idPregunta == Convert.ToInt32(e.CommandArgument));

				List<miRespuesta> listaRespuestaLocal = ObtenerRespuestas(miPregunta);
				DataTable dt = UIUtilidades.BuildDataTable<miRespuesta>(listaRespuestaLocal);
				grafico.Titulo = miPregunta.textoPregunta;
				tituloReporte = encuestaSesion.nombreEncuesta;
				filtrosAplicados = miPregunta.textoPregunta;
				grafico.LimpiarSeries();
				grafico.AgregarSerie("Respuestas", dt, "respuesta", "cantidad");
				grafico.GraficarBarra();
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Obteners the respuestas.
		/// </summary>
		/// <param name="respuesta">The respuesta.</param>
		/// <returns></returns>
		private List<miRespuesta> ObtenerRespuestas(RespuestaPreguntaAnalisis respuesta)
		{
			List<miRespuesta> listaRespuestasLocal = new List<miRespuesta>();
			miRespuesta laRespuesta = new miRespuesta();
			if (respuesta.idEscalaPonderacion == 1)
			{
				enumRespCualitativa cant1 = (enumRespCualitativa)1;
				laRespuesta.respuesta = cant1.ToString().Replace("_", " ");
				laRespuesta.cantidad = respuesta.cant1;
				listaRespuestasLocal.Add(laRespuesta);

				laRespuesta = new miRespuesta();
				enumRespCualitativa cant2 = (enumRespCualitativa)2;
				laRespuesta.respuesta = cant2.ToString().Replace("_", " ");
				laRespuesta.cantidad = respuesta.cant2;
				listaRespuestasLocal.Add(laRespuesta);

				laRespuesta = new miRespuesta();
				enumRespCualitativa cant3 = (enumRespCualitativa)3;
				laRespuesta.respuesta = cant3.ToString().Replace("_", " ");
				laRespuesta.cantidad = respuesta.cant3;
				listaRespuestasLocal.Add(laRespuesta);

				laRespuesta = new miRespuesta();
				enumRespCualitativa cant4 = (enumRespCualitativa)4;
				laRespuesta.respuesta = cant4.ToString().Replace("_", " ");
				laRespuesta.cantidad = respuesta.cant4;
				listaRespuestasLocal.Add(laRespuesta);

				laRespuesta = new miRespuesta();
				enumRespCualitativa cant5 = (enumRespCualitativa)5;
				laRespuesta.respuesta = cant5.ToString().Replace("_", " ");
				laRespuesta.cantidad = respuesta.cant5;
				listaRespuestasLocal.Add(laRespuesta);
			}
			else
			{
				if (respuesta.idEscalaPonderacion == 2)
				{
					enumRespCuantitativa cant1 = (enumRespCuantitativa)1;
					laRespuesta.respuesta = cant1.ToString().Replace("_", " ");
					laRespuesta.cantidad = respuesta.cant1;
					listaRespuestasLocal.Add(laRespuesta);

					laRespuesta = new miRespuesta();
					enumRespCuantitativa cant2 = (enumRespCuantitativa)2;
					laRespuesta.respuesta = cant2.ToString().Replace("_", " ");
					laRespuesta.cantidad = respuesta.cant2;
					listaRespuestasLocal.Add(laRespuesta);

					laRespuesta = new miRespuesta();
					enumRespCuantitativa cant3 = (enumRespCuantitativa)3;
					laRespuesta.respuesta = cant3.ToString().Replace("_", " ");
					laRespuesta.cantidad = respuesta.cant3;
					listaRespuestasLocal.Add(laRespuesta);

					laRespuesta = new miRespuesta();
					enumRespCuantitativa cant4 = (enumRespCuantitativa)4;
					laRespuesta.respuesta = cant4.ToString().Replace("_", " ");
					laRespuesta.cantidad = respuesta.cant4;
					listaRespuestasLocal.Add(laRespuesta);

					laRespuesta = new miRespuesta();
					enumRespCuantitativa cant5 = (enumRespCuantitativa)5;
					laRespuesta.respuesta = cant5.ToString().Replace("_", " ");
					laRespuesta.cantidad = respuesta.cant5;
					listaRespuestasLocal.Add(laRespuesta);
				}
				else
				{
					for (int i = 0; i < 3; i++)
					{
						laRespuesta = new miRespuesta();
						switch (i)
						{
							case 0:
								laRespuesta.respuesta = "Respuestas Esperadas";
								laRespuesta.cantidad = respuesta.respuestasEsperadas;
								break;
							case 1:
								laRespuesta.respuesta = "Respuestas Obtenidas";
								laRespuesta.cantidad = respuesta.respuestasObtenidas;
								break;
							case 2:
								laRespuesta.respuesta = "Porcentaje de Respuestas";
								laRespuesta.cantidad = respuesta.porcentaje;
								break;
							default:
								break;
						}
						listaRespuestasLocal.Add(laRespuesta);
					}
				}
			}
			return listaRespuestasLocal;
		}
		#endregion
	}
}