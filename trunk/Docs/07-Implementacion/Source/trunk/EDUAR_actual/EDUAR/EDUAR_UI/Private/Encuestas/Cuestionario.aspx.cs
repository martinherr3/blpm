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
        BLEncuestaDisponible objBLEncuestaDisponible;
        BLEscala objBLEscala;
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

                    if (Int32.TryParse(ddlEncuesta.SelectedValue, out idEncuestaSeleccionada))
                    {
                        if (AccionPagina == enumAcciones.Buscar
                            ||
                            AccionPagina == enumAcciones.Responder
                            ||
                            AccionPagina == enumAcciones.Error)
                            CargarEncuesta(idEncuestaSeleccionada);
                        else
                            if (Request.Params["__EVENTTARGET"] == "GuardarRespuesta")
                                text_Changed(this, new EventArgs());
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
        void VentanaAceptar(object sender, EventArgs e)
        {
            try
            {
                int idEncuestaSeleccionada;
                switch (AccionPagina)
                {
                    case enumAcciones.Buscar:
                    case enumAcciones.Responder:
                        if (Int32.TryParse(ddlEncuesta.SelectedValue, out idEncuestaSeleccionada))
                            CargarEncuesta(idEncuestaSeleccionada);
                        break;
                    case enumAcciones.Guardar:
                        AccionPagina = enumAcciones.Limpiar;
                        CargarCombos();
                        LimpiarPantalla();
                        udpFormulario.Update();
                        udpSeleccionEncuesta.Update();
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
                if (ValidarPagina())
                {
                    GuardarRespuestas();
                    AccionPagina = enumAcciones.Guardar;
                    Master.MostrarMensaje("Gracias", "Muchas Gracias por contestar nuestra encuesta.", enumTipoVentanaInformacion.Satisfactorio);
                }
                else
                {
                    AccionPagina = enumAcciones.Error;
                    Master.MostrarMensaje("Error de Validación", "Existen preguntas sin responder.", enumTipoVentanaInformacion.Advertencia);
                }
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
        private bool ValidarPagina()
        {
            int contRespuestas = 0;
            foreach (Respuesta item in ListaRespuestas)
            {
                if (item.respuestaSeleccion > 0) contRespuestas++;
            }
            return contRespuestas == cantRespuestasMinimas;
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

                respuestaPuntual.pregunta.textoPregunta = ((Panel)sender).ID;

                string[] aux = ((Panel)sender).ID.Split('_');
                int auxIdPregunta = 0;
                if (aux.Length == 2)
                    int.TryParse(aux[1], out auxIdPregunta);

                Respuesta miRespuesta = ListaRespuestas.Find(p => p.pregunta.textoPregunta == ((Panel)sender).ID);
                if (miRespuesta != null)
                {
                    ListaRespuestas.Find(p => p.pregunta.textoPregunta == ((Panel)sender).ID).respuestaSeleccion = Convert.ToInt16(e.Value);
                }
                else
                {
                    respuestaPuntual = respuestaSkeleton;

                    respuestaPuntual.pregunta.idPregunta = auxIdPregunta;
                    respuestaPuntual.pregunta.textoPregunta = ((Panel)sender).ID;
                    respuestaPuntual.respuestaSeleccion = Convert.ToInt16(e.Value);
                    ListaRespuestas.Add(respuestaPuntual);
                }
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
                string valor = ((TextBox)sender).Text.Trim();
                if (!string.IsNullOrEmpty(valor))
                {
                    AccionPagina = enumAcciones.Responder;

                    Respuesta respuestaPuntual = new Respuesta();
                    respuestaPuntual.pregunta.textoPregunta = ((TextBox)sender).ID;

                    string[] aux = ((TextBox)sender).ID.Split('_');
                    int auxIdPregunta = 0;
                    if (aux.Length == 2)
                        int.TryParse(aux[1], out auxIdPregunta);

                    Respuesta miRespuesta = ListaRespuestas.Find(p => p.pregunta.textoPregunta == ((TextBox)sender).ID);
                    if (miRespuesta != null)
                    {
                        ListaRespuestas.Find(p => p.pregunta.textoPregunta == ((TextBox)sender).ID).respuestaTextual = valor;
                    }
                    else
                    {
                        respuestaPuntual = respuestaSkeleton;

                        respuestaPuntual.pregunta.idPregunta = auxIdPregunta;
                        respuestaPuntual.pregunta.textoPregunta = ((TextBox)sender).ID;
                        respuestaPuntual.respuestaTextual = valor;
                        ListaRespuestas.Add(respuestaPuntual);
                    }
                }
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

            List<Encuesta> listaEncuesta = objBLEncuestaDisponible.GetEncuestasDisponibles(encuestaSkeleton);
            if (listaEncuesta.Count > 0)
                UIUtilidades.BindCombo<Encuesta>(ddlEncuesta, listaEncuesta, "idEncuesta", "nombreEncuesta", true);
            else
                Response.Redirect("~/Private/Account/Welcome.aspx", true);
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
            ListaRespuestas = new List<Respuesta>();
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
            List<string> textoPregunta = new List<string>();
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

                        textoPregunta = new List<string>();

                        textoPregunta = UIUtilidades.StringWrap(pregunta.textoPregunta, 130);

                        foreach (string item in textoPregunta)
                            lblPregunta.Text += item + "<br />";

                        lblPregunta.Font.Bold = true;
                        lblPregunta.Font.Size = 11;

                        panelRespuesta.Controls.Add(lblPregunta);

                        respuestaSkeleton.pregunta = pregunta;

                        Respuesta miRespuesta = null;
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

                            if (ListaRespuestas != null)
                            {
                                miRespuesta = ListaRespuestas.Find(p => p.pregunta.textoPregunta == txtRespuesta.ID);
                                if (miRespuesta != null) txtRespuesta.Text = miRespuesta.respuestaTextual;
                            }
                            txtRespuesta.TextChanged += new EventHandler(this.text_Changed);
                            txtRespuesta.Attributes.Add("onBlur", "GuardarRespuesta(this);");

                            panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
                            panelRespuesta.Controls.Add(txtRespuesta);
                            panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
                        }
                        else
                        {
                            List<ValorEscalaMedicion> lista = new List<ValorEscalaMedicion>();
                            BLValorEscala objBL = new BLValorEscala();
                            EscalaMedicion miEscala = new EscalaMedicion();
                            lista = objBL.GetValoresEscalasMedicion(pregunta.escala);

                            RadioButtonList botonera = new RadioButtonList();
                            botonera.AutoPostBack = true;
                            botonera.ID = "respuesta_" + pregunta.idPregunta.ToString();
                            botonera.SelectedIndexChanged += new EventHandler(botonera_SelectedIndexChanged);
                            foreach (ValorEscalaMedicion item in lista)
                                botonera.Items.Add(new ListItem(item.nombre, item.idValorEscala.ToString()));

                            //AjaxControlToolkit.Rating rating = new AjaxControlToolkit.Rating();

                            //rating.ID = "respuesta_" + pregunta.idPregunta.ToString();

                            //objBLEscala = new BLEscala();

                            //int quantity = objBLEscala.GetCantidadValores(pregunta.escala).Count;
                            //string firstValue = objBLEscala.GetCantidadValores(pregunta.escala)[0].nombre;
                            //string lastValue = objBLEscala.GetCantidadValores(pregunta.escala)[quantity - 1].nombre;

                            //rating.MaxRating = quantity;

                            //rating.StarCssClass = "ratingStar";
                            //rating.WaitingStarCssClass = "savedRatingStar";
                            //rating.FilledStarCssClass = "filledRatingStar";
                            //rating.EmptyStarCssClass = "emptyRatingStar";
                            //rating.AutoPostBack = false;

                            //if (ListaRespuestas != null)
                            //{
                            //    miRespuesta = ListaRespuestas.Find(p => p.pregunta.textoPregunta == rating.ID);
                            //    if (miRespuesta != null) rating.CurrentRating = miRespuesta.respuestaSeleccion;
                            //}
                            //rating.Changed += new AjaxControlToolkit.RatingEventHandler(rating_Changed);

                            //panelRespuesta.Controls.Add(new LiteralControl("<br/>"));
                            //panelRespuesta.Controls.Add(new LiteralControl("<table border=0><tr><td><i>" + firstValue + "</i></td><td>"));
                            //panelRespuesta.Controls.Add(rating);
                            //panelRespuesta.Controls.Add(new LiteralControl("</td><td><i>" + lastValue + "</i></td></tr></table>"));

                            panelRespuesta.Controls.Add(botonera);
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

        void botonera_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList boton = (RadioButtonList)sender;
            AccionPagina = enumAcciones.Responder;
            Respuesta respuestaPuntual = new Respuesta();

            //respuestaPuntual.pregunta.textoPregunta = ((Panel)sender).ID;

            string[] aux = boton.ID.Split('_');
            int auxIdPregunta = 0;
            if (aux.Length == 2)
                int.TryParse(aux[1], out auxIdPregunta);

            Respuesta miRespuesta = ListaRespuestas.Find(p => p.pregunta.idPregunta == auxIdPregunta);
            if (miRespuesta != null)
            {
                ListaRespuestas.Find(p => p.pregunta.idPregunta == auxIdPregunta).respuestaSeleccion = Convert.ToInt16(boton.SelectedValue);
            }
            else
            {
                respuestaPuntual = respuestaSkeleton;

                respuestaPuntual.pregunta.idPregunta = auxIdPregunta;
                //respuestaPuntual.pregunta.textoPregunta = ((Panel)sender).ID;
                respuestaPuntual.respuestaSeleccion = Convert.ToInt16(boton.SelectedValue);
                ListaRespuestas.Add(respuestaPuntual);
            }
        }

        protected void ddlEncuesta_SelectedIndexChanged(object sender, EventArgs e)
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