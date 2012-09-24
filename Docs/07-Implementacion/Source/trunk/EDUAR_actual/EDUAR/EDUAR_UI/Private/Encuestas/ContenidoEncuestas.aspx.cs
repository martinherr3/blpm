using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Encuestas;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
    public partial class ContenidoEncuestas : EDUARBasePage
    {
        #region --[Atributos]--
        private BLEncuesta objBLEncuesta;
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
        /// Gets or sets the id escala medicion.
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

        /// <summary>
        /// Gets or sets the pregunta editar.
        /// </summary>
        /// <value>
        /// The pregunta editar.
        /// </value>
        public Pregunta preguntaEditar
        {
            get
            {
                if (Session["preguntaEditar"] == null)
                    Session["preguntaEditar"] = new Pregunta();
                return (Pregunta)Session["preguntaEditar"];
            }
            set { Session["preguntaEditar"] = value; }
        }

        /// <summary>
        /// Mantiene la pregunta seleccionada en la grilla.
        /// </summary>
        /// <value>
        /// The prop encuesta.
        /// </value>
        public Pregunta propPregunta
        {
            get
            {
                if (ViewState["propPregunta"] == null)
                    propPregunta = new Pregunta();

                return (Pregunta)ViewState["propPregunta"];
            }
            set { ViewState["propPregunta"] = value; }
        }

        /// <summary>
        /// Gets or sets the prop filtro pregunta.
        /// </summary>
        /// <value>
        /// The prop filtro encuesta.
        /// </value>
        public Pregunta propFiltroPregunta
        {
            get
            {
                if (ViewState["propFiltroPregunta"] == null)
                    propFiltroPregunta = new Pregunta();

                return (Pregunta)ViewState["propFiltroPregunta"];
            }
            set { ViewState["propFiltroPregunta"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista encuesta.
        /// </summary>
        /// <value>
        /// The lista agenda.
        /// </value>
        public List<Pregunta> listaPreguntas
        {
            get
            {
                if (ViewState["listaPreguntas"] == null) listaPreguntas = new List<Pregunta>();

                return (List<Pregunta>)ViewState["listaPreguntas"];
            }
            set { ViewState["listaPreguntas"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista de ambitos.
        /// </summary>
        /// <value>
        /// The lista ambitos.
        /// </value>
        public List<AmbitoEncuesta> listaAmbitos
        {
            get
            {
                if (ViewState["listaAmbitos"] == null)
                {
                    BLAmbitoEncuesta objBLAmbitoEncuesta = new BLAmbitoEncuesta();

                    listaAmbitos = objBLAmbitoEncuesta.GetAmbitosEncuesta(null);
                }
                return (List<AmbitoEncuesta>)ViewState["listaAmbitos"];
            }
            set { ViewState["listaAmbitos"] = value; }
        }

        /// <summary>
        /// Devuelve la lista de categorias disponibles
        /// </summary>
        /// <value>
        /// The lista categorias.
        /// </value>
        public List<CategoriaPregunta> listaCategorias
        {
            get
            {
                if (ViewState["listaCategorias"] == null)
                {
                    BLCategoriaPregunta objBLCategoriasPregunta = new BLCategoriaPregunta();

                    //Necesito que las categorias sean solamente de el ambito en cuestion, no en general

                    CategoriaPregunta categoriaGenerica = new CategoriaPregunta();
                    categoriaGenerica.ambito.idAmbitoEncuesta = encuestaSesion.ambito.idAmbitoEncuesta;

                    listaCategorias = objBLCategoriasPregunta.GetCategoriasPregunta(categoriaGenerica);
                }
                return (List<CategoriaPregunta>)ViewState["listaCategorias"];
            }
            set { ViewState["listaCategorias"] = value; }
        }

        /// <summary>
        /// Devuelve la lista de escalas disponibles
        /// </summary>
        /// <value>
        /// The lista escalas.
        /// </value>
        public List<EscalaMedicion> listaEscalas
        {
            get
            {
                if (ViewState["listaEscalas"] == null)
                {
                    BLEscala objBLEscalasMedicion = new BLEscala();

                    EscalaMedicion escalaGenerica = new EscalaMedicion();
                    escalaGenerica.idEscala = idEscalaMedicion;

                    listaEscalas = objBLEscalasMedicion.GetEscalasMedicion(escalaGenerica);
                }
                return (List<EscalaMedicion>)ViewState["listaEscalas"];
            }
            set { ViewState["listaEscalas"] = value; }
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
                Master.BotonAvisoAceptar += (VentanaAceptar);

                if (!Page.IsPostBack)
                {
                    CargarPresentacion();
                    BuscarPregunta(encuestaSesion,null);
                }
                else
                {
                    if (Request.Form["__EVENTTARGET"] == "btnGuardar")
                        //llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Guardar
                        btnGuardar_Click(this, new EventArgs());
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
                switch (AccionPagina)
                {
                    case enumAcciones.Limpiar:
                        CargarPresentacion();
                        BuscarPregunta(encuestaSesion, null);
                        break;
                    case enumAcciones.Guardar:
                        AccionPagina = enumAcciones.Limpiar;
                        GuardarPregunta(ObtenerValoresDePantalla());
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
                        break;
					case enumAcciones.Eliminar:
						AccionPagina = enumAcciones.Limpiar;
						EliminarPregunta();
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
        /// Handles the Click event of the btnNuevo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = enumAcciones.Nuevo;
                LimpiarCampos();
                esNuevo = true;
                CargarCombos();
                btnGuardar.Visible = true;
                btnBuscar.Visible = false;
                btnVolver.Visible = true;
                btnNuevo.Visible = false;
                gvwPreguntas.Visible = false;
                litEditar.Visible = false;
                litNuevo.Visible = true;
                udpEdit.Visible = true;
                udpFiltrosBusqueda.Visible = false;
                udpFiltros.Update();
                udpGrilla.Update();
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
        /// <param name="e">The <see cref="System.EventArgs"/> instancia que contiene los datos de la encuesta.</param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                BuscarFiltrando();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void btnDesign_Click(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = enumAcciones.Redirect;

                Response.Redirect("Cuestionario.aspx", false);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAsignarRol control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = ValidarPagina();
                if (mensaje == string.Empty)
                {
                    if (Page.IsValid)
                    {
                        AccionPagina = enumAcciones.Guardar;
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
                    }
                }
                else
                {
                    Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
                }
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
                Response.Redirect("~/Private/Encuestas/ManageContenidoEncuestas.aspx", true);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Método que se llama al hacer click sobre las acciones de la grilla
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gvwPreguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        propPregunta.idPregunta = Convert.ToInt32(e.CommandArgument.ToString());
                        CargaPregunta();
                        break;
                    case "Eliminar":
                        AccionPagina = enumAcciones.Eliminar;
                        propPregunta.idPregunta = Convert.ToInt32(e.CommandArgument.ToString());
						Master.MostrarMensaje("Eliminar Pregunta", "¿Desea <b>eliminar</b> la pregunta seleccionada?", enumTipoVentanaInformacion.Confirmación);
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the gvwEncuesta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvwPreguntas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvwPreguntas.PageIndex = e.NewPageIndex;
                CargarGrilla();
                btnBuscar.Visible = false;
                btnVolver.Visible = true;
                gvwPreguntas.Visible = false;
                udpFiltrosBusqueda.Visible = false;
                udpEdit.Visible = true;
                udpEdit.Update();
            }
            catch (Exception ex) { Master.ManageExceptions(ex); }
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">The lista.</param>
        private void CargarGrilla()
        {
            gvwPreguntas.DataSource = UIUtilidades.BuildDataTable<Pregunta>(listaPreguntas).DefaultView;
            gvwPreguntas.DataBind();
            udpEdit.Visible = false;
            udpGrilla.Update();
        }

        /// <summary>
        /// Carga el contenido de la grilla de encuestas.
        /// </summary>
        private void CargarPresentacion()
        {
            LimpiarCampos();
            lblTitulo.Text = "Pregunta";
            CargarCombos();
            udpEdit.Visible = false;
            btnVolver.Visible = true;
            btnNuevo.Visible = true;
            btnGuardar.Visible = false;
            udpFiltrosBusqueda.Visible = true;
            btnBuscar.Visible = true;
            gvwPreguntas.Visible = true;
            udpFiltros.Update();
            udpGrilla.Update();
        }

        /// <summary>
        /// Buscars the entidads.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void BuscarPregunta(Encuesta encuesta, Pregunta entidad)
        {
            CargarLista(encuesta,entidad);
            CargarGrilla();
        }

        /// <summary>
        /// Cargars the lista.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void CargarLista(Encuesta encuesta, Pregunta entidad)
        {
            objBLEncuesta = new BLEncuesta(encuesta);
            listaPreguntas = objBLEncuesta.GetPreguntasEncuesta(encuestaSesion,entidad);
        }

        /// <summary>
        /// Cargars the combos.
        /// </summary>
        private void CargarCombos()
        {
            UIUtilidades.BindCombo<CategoriaPregunta>(ddlCategoria, listaCategorias, "idCategoriaPregunta", "nombre", true);
            UIUtilidades.BindCombo<EscalaMedicion>(ddlEscalaPonderacion, listaEscalas, "idEscala", "nombre", true);

            UIUtilidades.BindCombo<CategoriaPregunta>(ddlCategoriaEdit, listaCategorias, "idCategoriaPregunta", "nombre", true);
            UIUtilidades.BindCombo<EscalaMedicion>(ddlEscalaPonderacionEdit, listaEscalas, "idEscala", "nombre", true);
        }

        /// <summary>
        /// Obteners the valores pantalla.
        /// </summary>
        /// <returns></returns>
        private Pregunta ObtenerValoresDePantalla()
        {
            Pregunta entidad = new Pregunta();
            entidad = propPregunta;

            if (!esNuevo)
            {
                entidad.idPregunta = propPregunta.idPregunta;
            }

            if (Convert.ToInt32(ddlCategoriaEdit.SelectedValue) > 0 && Convert.ToInt32(ddlEscalaPonderacionEdit.SelectedValue) > 0)
            {
                entidad.categoria.idCategoriaPregunta = Convert.ToInt32(ddlCategoriaEdit.SelectedValue);
                entidad.escala.idEscala = Convert.ToInt32(ddlEscalaPonderacionEdit.SelectedValue);
                entidad.textoPregunta = txtTextoPreguntaEdit.Text.Trim();
                entidad.objetivoPregunta = txtObjetivoPreguntaEdit.Text.Trim();
                entidad.ponderacion = Convert.ToDouble(txtPesoPreguntaEdit.Text.Trim());
            }

            return entidad;
        }

        /// <summary>
        /// Eliminar la pregunta.
        /// </summary>
        private void EliminarPregunta()
        {
            Pregunta objEliminar = new Pregunta();
            objEliminar.idPregunta =  propPregunta.idPregunta;

            encuestaSesion.preguntas.Clear();
            encuestaSesion.preguntas.Add(objEliminar);

            objBLEncuesta = new BLEncuesta(encuestaSesion);
            objBLEncuesta.Delete();

            CargarPresentacion();
            BuscarPregunta(encuestaSesion, null);
        }

        /// <summary>
        /// Guardars the encuesta.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void GuardarPregunta(Pregunta entidad)
        {
            objBLEncuesta = new BLEncuesta(encuestaSesion);
            Pregunta preguntaExistente = new Pregunta();

            encuestaSesion.preguntas = objBLEncuesta.GetPreguntasEncuesta(encuestaSesion,null);
            encuestaSesion.preguntas.Add(entidad);
            
            objBLEncuesta.Save(); //Como la encuesta ya existe, en realidad se va a actualizar la misma
        }

        /// <summary>
        /// Cargars the entidad.
        /// </summary>
        private void CargarValoresEnPantalla(int idEntidad)
        {
            Pregunta pregunta = listaPreguntas.Find(c => c.idPregunta == idEntidad);

            ddlCategoriaEdit.SelectedValue = pregunta.categoria.idCategoriaPregunta.ToString();
            ddlEscalaPonderacionEdit.SelectedValue = pregunta.escala.idEscala.ToString();
            txtTextoPreguntaEdit.Text = pregunta.textoPregunta;
            txtObjetivoPreguntaEdit.Text = pregunta.objetivoPregunta;
            txtPesoPreguntaEdit.Text = pregunta.ponderacion.ToString();
        }

        /// <summary>
        /// Validars the pagina.
        /// </summary>
        /// <returns></returns>
        private string ValidarPagina()
        {
            StringBuilder message = new StringBuilder();

            if(ddlCategoriaEdit.SelectedIndex <= 0)
                message.Append("- Categoría<br/>");
            if(ddlEscalaPonderacionEdit.SelectedIndex <= 0)
                message.Append("- Escala de ponderación<br/>");
            if(txtTextoPreguntaEdit.Text.Trim().Length == 0)
                message.Append("- Texto Pregunta<br/>");
            if(txtObjetivoPreguntaEdit.Text.Trim().Length == 0)
                message.Append("- Objetivo Pregunta<br/>");
            if(txtPesoPreguntaEdit.Text.Trim().Length == 0)
                message.Append("- Peso<br/>");

            return message.ToString();
        }

        /// <summary>
        /// Cargas the agenda.
        /// </summary>
        private void CargaPregunta()
        {
            AccionPagina = enumAcciones.Modificar;
            esNuevo = false;

            CargarValoresEnPantalla(propPregunta.idPregunta);

            litEditar.Visible = true;
            litNuevo.Visible = false;
            btnBuscar.Visible = false;
            btnNuevo.Visible = false;
            btnVolver.Visible = true;
            btnGuardar.Visible = true;
            gvwPreguntas.Visible = false;
            udpFiltrosBusqueda.Visible = false;
            udpEdit.Visible = true;
            udpFiltros.Update();
            udpEdit.Update();
        }

        /// <summary>
        /// Buscars the filtrando.
        /// </summary>
        private void BuscarFiltrando()
        {
            lblTitulo.Text = "Preguntas";
            Pregunta entidad = new Pregunta();

            CategoriaPregunta categoria = new CategoriaPregunta();
            categoria.idCategoriaPregunta = Convert.ToInt32(ddlCategoria.SelectedValue);

            EscalaMedicion escala = new EscalaMedicion();
            escala.idEscala = Convert.ToInt32(ddlEscalaPonderacion.SelectedValue);

            entidad.categoria = categoria;
            entidad.escala = escala;

            propFiltroPregunta = entidad;
            BuscarPregunta(encuestaSesion, propFiltroPregunta);
        }

        /// <summary>
        /// Limpiar the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            if (ddlCategoria.Items.Count > 0) ddlCategoria.SelectedIndex = 0;
            if (ddlEscalaPonderacion.Items.Count > 0) ddlEscalaPonderacion.SelectedIndex = 0;
            if (ddlCategoriaEdit.Items.Count > 0) ddlCategoria.SelectedIndex = 0;
            if (ddlEscalaPonderacionEdit.Items.Count > 0) ddlEscalaPonderacion.SelectedIndex = 0;
            txtObjetivoPreguntaEdit.Text = string.Empty;
            txtPesoPreguntaEdit.Text = string.Empty;
            txtTextoPreguntaEdit.Text = string.Empty;
            
            //reseteo este valor, el cual se actualizará cuando sea necesario
            propPregunta = null;
        }
        #endregion
    }
}