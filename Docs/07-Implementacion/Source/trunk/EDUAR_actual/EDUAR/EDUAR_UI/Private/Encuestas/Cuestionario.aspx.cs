using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_BusinessLogic.Encuestas;
using System.Drawing;
using EDUAR_UI.UserControls;
using System.Text;
using System.IO;
using EDUAR_UI.Utilidades;
using AjaxControlToolkit;

namespace EDUAR_UI
{
    public partial class Cuestionario : EDUARBasePage
    {
        #region --[Atributos]--
        BLEncuesta objBLEncuesta;
        BLPregunta objBLPregunta;
        #endregion

        #region --[Propiedades]--

        /// <summary>
        /// Gets or sets the encuesta en sesion.
        /// </summary>
        /// <value>
        /// The encuesta en sesion.
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
        /// Gets or sets el tipo de escala a utilizar
        /// Recordar que puede ser cualitativa o cuantitativa, y de ello depende las opciones a desplegar
        /// </summary>
        /// <value>
        /// El tipo de escala.
        /// </value>
        public int tipoEscala
        {
            get
            {
                if (Session["tipoEscala"] == null)
                    tipoEscala = 0;
                return (int)Session["tipoEscala"];
            }
            set { Session["tipoEscala"] = value; }
        }


        /// <summary>
        /// Gets or sets the id pregunta con la finalidad de mantener el track de la respuesta.
        /// </summary>
        /// <value>
        /// The id pregunta.
        /// </value>
        public int idPregunta
        {
            get
            {
                if (ViewState["idPregunta"] == null)
                    ViewState["idPregunta"] = 0;
                return (int)ViewState["idPregunta"];
            }
            set { ViewState["idPregunta"] = value; }
        }

        /// <summary>
        /// Gets or sets the id escala medicion con la finalidad de mantener el track de la respuesta y como valorarla.
        /// </summary>
        /// <value>
        /// The id escala medicion.
        /// </value>
        public int idEscalaMedicion
        {
            get
            {
                if (ViewState["idEscalaMedicion"] == null)
                    ViewState["idEscalaMedicion"] = 0;
                return (int)ViewState["idEscalaMedicion"];
            }
            set { ViewState["idEscalaMedicion"] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                generarEsqueleto();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void rating_Changed(object sender, RatingEventArgs e)
        {
            try
            {
                e.CallbackResult = e.Value;
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }
        #endregion

        #region --[Métodos Privados]--
        public void generarEsqueleto()
        {
            objBLEncuesta = new BLEncuesta();
            objBLPregunta = new BLPregunta();

            List<CategoriaPregunta> listaCategorias = objBLEncuesta.GetCategoriasPorEncuesta(encuestaSesion);

            lblNombreEncuesta.Text = encuestaSesion.nombreEncuesta;

            Label lblCategoria;

            AjaxControlToolkit.AccordionPane pn;
            int i = 0;
            int contador = 0;
            
            foreach (CategoriaPregunta categoria in listaCategorias)
            {
                List<Pregunta> preguntasPorCategoria = objBLPregunta.GetPreguntasPorCategoria(categoria);

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
                        panelRespuesta.ID = "respuesta_" + contador.ToString();

                        //PREGUNTA
                        lblPregunta = new Label();

                        lblPregunta.Text = pregunta.textoPregunta;
                        lblPregunta.Font.Bold = true;
                        lblPregunta.Font.Size = 11;
                        lblPregunta.BorderWidth = 1;
                        lblPregunta.Width = 990;

                        panelRespuesta.Controls.Add(lblPregunta);
                        panelRespuesta.Controls.Add(new LiteralControl("<br/>"));

                        //RESPUESTA

                        if (pregunta.escala.nombre.Equals("Conceptual literal"))
                        {
                            TextBox txtRespuesta = new TextBox();
                            txtRespuesta.ID = pregunta.idPregunta.ToString();
                            txtRespuesta.Rows = 15;
                            txtRespuesta.Width = 950;
                            txtRespuesta.BorderStyle = BorderStyle.Groove;

                            panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
                            panelRespuesta.Controls.Add(txtRespuesta);
                            panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
                        }
                        else
                        {
                            AjaxControlToolkit.Rating rating = new AjaxControlToolkit.Rating();

                            rating.ID = pregunta.idPregunta.ToString();
                            rating.MaxRating = 5;
                            
                            rating.StarCssClass = "ratingStar";
                            rating.WaitingStarCssClass = "savedRatingStar";
                            rating.FilledStarCssClass = "filledRatingStar";
                            rating.EmptyStarCssClass = "emptyRatingStar";

                            rating.CurrentRating = Convert.ToInt32(rating.GetCallbackResult());

                            panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
                            panelRespuesta.Controls.Add(rating);
                            panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
                            
                        }

                        //Agrego un salto de línea, para mantener cierta distancia entre las preguntas
                        pn.ContentContainer.Controls.Add(panelRespuesta);
                        pn.ContentContainer.Controls.Add(new LiteralControl("<br/>"));
                    }
                }
                CuestionarioAccordion.Panes.Add(pn);
                ++i;
            }
        }


    }
        #endregion


}