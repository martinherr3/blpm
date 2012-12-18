using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_Entities;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI
{
	public partial class VerRespuestas : EDUARBasePage
	{
		#region --[Propiedades]--
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
		/// Gets or sets the respuestas textuales.
		/// </summary>
		/// <value>
		/// The respuestas textuales.
		/// </value>
		public List<Respuesta> listaRespuestasTextuales
		{
			get
			{
				if (Session["listaRespuestasTextuales"] == null)
					listaRespuestasTextuales = new List<Respuesta>();

				return (List<Respuesta>)Session["listaRespuestasTextuales"];
			}
			set { Session["listaRespuestasTextuales"] = value; }
		}

		/// <summary>
		/// Gets or sets the mi pregunta textual.
		/// </summary>
		/// <value>
		/// The mi pregunta textual.
		/// </value>
		public RespuestaPreguntaAnalisis miPreguntaTextual
		{
			get
			{
				if (Session["miPreguntaTextual"] == null)
					miPreguntaTextual = new RespuestaPreguntaAnalisis();

				return (RespuestaPreguntaAnalisis)Session["miPreguntaTextual"];
			}
			set { Session["miPreguntaTextual"] = value; }
		}
		#endregion

		#region --[Eventos]--
		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				btnVolver.Attributes.Add("onClick", "Cerrar();");
				btnVolver2.Attributes.Add("onClick", "Cerrar();");
				if (!Page.IsPostBack)
				{
					List<string> textoPregunta = new List<string>();

					textoPregunta = UIUtilidades.StringWrap(miPreguntaTextual.textoPregunta, 130);

					foreach (string item in textoPregunta)
						lblPregunta.Text += item + "<br />";

					rptConversacion.DataSource = listaRespuestasTextuales;
					rptConversacion.DataBind();
					udpConversacion.Update();
				}
			}
			catch (Exception ex)
			{
				AvisoMostrar = true;
				AvisoExcepcion = ex;
			}
		}
		#endregion
	}
}