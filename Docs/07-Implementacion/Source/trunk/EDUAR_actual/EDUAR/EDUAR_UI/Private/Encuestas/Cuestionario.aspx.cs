using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_BusinessLogic.Encuestas;
using AjaxControlToolkit;
using EDUAR_UI.UserControls;
using EDUAR_UI.Utilidades;

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
        /// Gets or sets el tipo de escala a utilizar
        /// Recordar que puede ser cualitativa o cuantitativa, y de ello depende las opciones a desplegar
        /// </summary>
        /// <value>
        /// El tipo de escala.
        /// </value>
        //public int tipoEscala
        //{
        //    get
        //    {
        //        if (Session["tipoEscala"] == null)
        //            tipoEscala = 0;
        //        return (int)Session["tipoEscala"];
        //    }
        //    set { Session["tipoEscala"] = value; }
        //}

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

        public List<Respuesta> respuestas
        {
            get
            {
                if (Session["respuestas"] == null)
                    Session["respuestas"] = new List<Respuesta>();

                return (List<Respuesta>)Session["respuestas"];
            }
            set { Session["respuestas"] = value; }
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
                cargarEncabezado();
            }
            else
            {
                int idEncuestaSeleccionada;

                if (Int32.TryParse(ddlEncuesta.SelectedValue, out idEncuestaSeleccionada) 
                    && AccionPagina == EDUAR_Utility.Enumeraciones.enumAcciones.Buscar
                    )
                    CargarEncuesta();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // REGISTRAR QUE LA ENCUESTA DISPONIBLE HA SIDO RESPONDIDA
                encuestaSeleccionada.respondida = true;
                encuestaSeleccionada.fechaRespuesta = DateTime.Now;

                objBLEncuestaDisponible = new BLEncuestaDisponible(encuestaSeleccionada);
                objBLEncuestaDisponible.Save();

                // GUARDAR LAS RESPUESTAS
                //foreach (respuesta respuesta in respuestas)
                //{
                //    objblrespuesta = new blrespuesta(respuesta);
                //    objblrespuesta.save();
                //}
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
                foreach (Pregunta preguntaPuntual in encuestaSeleccionada.encuesta.preguntas)
                {
                    Control myControl = FindControlRecursive(udpFormulario, "respuesta_" + preguntaPuntual.idPregunta);

                    if (myControl.GetType() == typeof(TextBox))
                    {
                        ((TextBox)myControl).Text = string.Empty;
                    }
                    //if (myControl.GetType() == typeof(Rating))
                    //{
                    //    int respuesta2 = ((Rating)myControl).CurrentRating;
                    //}
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        private void CargarCombos()
        {
            objBLEncuestaDisponible = new BLEncuestaDisponible();

            EncuestaDisponible encuestaSkeleton = new EncuestaDisponible();
            encuestaSkeleton.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

            UIUtilidades.BindCombo<Encuesta>(ddlEncuesta, objBLEncuestaDisponible.GetEncuestasDisponibles(encuestaSkeleton), "idEncuesta", "nombreEncuesta", true);
        }

        protected void rating_Changed(object sender, RatingEventArgs e)
        {
            try
            {
                Respuesta respuestaPuntual = new Respuesta();
                respuestaPuntual = respuestaSkeleton;

                respuestaPuntual.respuestaSeleccion = Convert.ToInt16(e.Value);
                respuestas.Add(respuestaPuntual);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void text_Changed(object sender, EventArgs e)
        {
            try
            {
                string valor = ((TextBox)sender).Text;

                Respuesta respuestaPuntual = new Respuesta();
                respuestaPuntual = respuestaSkeleton;

                respuestaPuntual.respuestaTextual = valor;
                respuestas.Add(respuestaPuntual);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = EDUAR_Utility.Enumeraciones.enumAcciones.Buscar;
                CargarEncuesta();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        private void CargarEncuesta()
        {
            int idEncuestaSeleccionada = Convert.ToInt32(ddlEncuesta.SelectedValue);

            objBLEncuesta = new BLEncuesta();
            objBLEncuestaDisponible = new BLEncuestaDisponible();

            //OBTENGO LA ENCUESTA PUNTUAL

            Encuesta encuestaPuntual = objBLEncuestaDisponible.GetEncuestasDisponibles(encuestaSeleccionada).Find(c => c.idEncuesta == idEncuestaSeleccionada);

            encuestaSeleccionada.encuesta = encuestaPuntual;
            encuestaSeleccionada.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

            //PRECARGA DE ATRIBUTOS DE LA RESPUESTA SKELETON
            respuestaSkeleton.encuestaDisponible.encuesta = encuestaPuntual;
            respuestaSkeleton.encuestaDisponible.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

            generarEsqueleto(encuestaPuntual);
        }
        #endregion

        #region --[Métodos Privados]--
        public void cargarEncabezado()
        {
            CargarCombos();
        }

        public void generarEsqueleto(Encuesta entidad)
        {
            objBLEncuesta = new BLEncuesta();

            //objBLEncuestaDisponible = new BLEncuestaDisponible();

            objBLPregunta = new BLPregunta();

            //List<CategoriaPregunta> listaCategorias = objBLEncuesta.GetCategoriasPorEncuesta(encuestaSeleccionada.encuesta);
            List<CategoriaPregunta> listaCategorias = objBLEncuesta.GetCategoriasPorEncuesta(entidad);

            //lblNombreEncuesta.Text = encuestaSeleccionada.encuesta.nombreEncuesta;
            lblNombreEncuesta.Text = entidad.nombreEncuesta;

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
                        }

                        pn.ContentContainer.Controls.Add(panelRespuesta);
                        pn.ContentContainer.Controls.Add(new LiteralControl("<br/>"));
                    }
                }
                CuestionarioAccordion.Panes.Add(pn);
                ++i;
            }
        }

        public static Control FindControlRecursive(Control container, string name)
        {
            if ((container.ID != null) && (container.ID.Equals(name)))
                return container;

            foreach (Control ctrl in container.Controls)
            {
                Control foundCtrl = FindControlRecursive(ctrl, name);
                if (foundCtrl != null)
                    return foundCtrl;
            }
            return null;
        }
        #endregion
    }
}