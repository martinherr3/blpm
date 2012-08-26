using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_BusinessLogic.Encuestas;

namespace EDUAR_UI
{
    public partial class ManageContenidoEncuestas : EDUARBasePage
    {
        #region --[Atributos]--
        private BLEncuesta objBLEncuesta;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Mantiene la encuesta seleccionada en la grilla.
        /// </summary>
        /// <value>
        /// The prop encuesta.
        /// </value>
        public Encuesta propEncuesta
        {
            get
            {
                if (Session["propEncuesta"] == null)
                    propEncuesta = new Encuesta();

                return (Encuesta)Session["propEncuesta"];
            }
            set { Session["propEncuesta"] = value; }
        }

        /// <summary>
        /// Gets or sets the prop filtro encuesta.
        /// </summary>
        /// <value>
        /// The prop filtro encuesta.
        /// </value>
        public Encuesta propFiltroEncuesta
        {
            get
            {
                if (ViewState["propFiltroEncuesta"] == null)
                    propFiltroEncuesta = new Encuesta();

                return (Encuesta)ViewState["propFiltroEncuesta"];
            }
            set { ViewState["propFiltroEncuesta"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista encuesta.
        /// </summary>
        /// <value>
        /// The lista agenda.
        /// </value>
        public List<Encuesta> listaEncuesta
        {
            get
            {
                if (ViewState["listaEncuesta"] == null)
                    listaEncuesta = new List<Encuesta>();

                return (List<Encuesta>)ViewState["listaEncuesta"];
            }
            set { ViewState["listaEncuesta"] = value; }
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
                    if (propEncuesta.idEncuesta > 0)
                    {
                        CargarLista(propEncuesta);
                        CargaEncuesta();
                    }
                    else
                        BuscarEncuesta(propEncuesta);
                }
                //this.txtDescripcionEdit.Attributes.Add("onkeyup", " ValidarCaracteres(this, 4000);");
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
                        BuscarEncuesta(null);
                        break;
                    case enumAcciones.Guardar:
                        AccionPagina = enumAcciones.Limpiar;
                        GuardarEncuesta(ObtenerValoresDePantalla());
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
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
        /// Handles the Click event of the btnPreguntasEncuesta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnPreguntasEncuesta_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect("ManagePreguntasEncuesta.aspx", false);
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
                CargarCombos(ddlAmbitoEdit);
                btnGuardar.Visible = true;
                btnBuscar.Visible = false;
                btnVolver.Visible = true;
                btnNuevo.Visible = false;
                gvwEncuestas.Visible = false;
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
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
                        //TODO: Aquí hay que llamar a la validación de disponibilidad de agenda
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
                CargarPresentacion();
                BuscarEncuesta(propFiltroEncuesta);
                propEncuesta = new Encuesta();
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
        protected void gvwEncuestas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        propEncuesta.idEncuesta = Convert.ToInt32(e.CommandArgument.ToString());
                        CargaEncuesta();
                        lblTitulo.Text = "Encuesta: " + propEncuesta.nombreEncuesta;
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
        protected void gvwEncuesta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvwEncuestas.PageIndex = e.NewPageIndex;
                CargarGrilla();
                btnBuscar.Visible = false;
                btnVolver.Visible = true;
                gvwEncuestas.Visible = false;
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
            gvwEncuestas.DataSource = UIUtilidades.BuildDataTable<Encuesta>(listaEncuesta).DefaultView;
            gvwEncuestas.DataBind();
            udpEdit.Visible = false;
            udpGrilla.Update();
        }

        /// <summary>
        /// Cargars the presentacion.
        /// </summary>
        private void CargarPresentacion()
        {
            LimpiarCampos();
            lblTitulo.Text = "Encuesta";
            CargarCombos(ddlAmbito);
            udpEdit.Visible = false;
            btnVolver.Visible = false;
            btnGuardar.Visible = false;
            udpFiltrosBusqueda.Visible = true;
            btnBuscar.Visible = true;
            gvwEncuestas.Visible = true;
            udpFiltros.Update();
            udpGrilla.Update();
        }

        /// <summary>
        /// Habilitars the botones detalle.
        /// </summary>
        /// <param name="habilitar">if set to <c>true</c> [habilitar].</param>
        private void HabilitarBotonesDetalle(bool habilitar)
        {
            //btnPreguntasEncuesta.Visible = habilitar; 
            //TODO (Pablo): Agregar este noble botón
        }

        /// <summary>
        /// Buscars the entidads.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void BuscarEncuesta(Encuesta entidad)
        {
            CargarLista(entidad);
            CargarGrilla();
        }

        /// <summary>
        /// Cargars the lista.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void CargarLista(Encuesta entidad)
        {
            entidad.activo = true;
            if (User.IsInRole(enumRoles.Administrador.ToString()))
                entidad.usuario.nombre = ObjSessionDataUI.ObjDTUsuario.Nombre;
            objBLEncuesta = new BLEncuesta(entidad);
            listaEncuesta = objBLEncuesta.GetEncuestas(entidad);
        }

        /// <summary>
        /// Cargars the combos.
        /// </summary>
        private void CargarCombos(DropDownList ddlAmbito)
        {
            UIUtilidades.BindCombo<AmbitoEncuesta>(ddlAmbito, listaAmbitos, "idAmbitoEncuesta", "nombre", true);
        }

        /// <summary>
        /// Obteners the valores pantalla.
        /// </summary>
        /// <returns></returns>
        private Encuesta ObtenerValoresDePantalla()
        {
            Encuesta entidad = new Encuesta();
            entidad = propEncuesta;
            if (!esNuevo)
            {
                entidad.idEncuesta = propEncuesta.idEncuesta;
                entidad.nombreEncuesta = propEncuesta.nombreEncuesta;
                //entidad.preguntas = propEncuesta.preguntas;
                entidad.fechaCreacion = propEncuesta.fechaCreacion;
                entidad.fechaModificacion = propEncuesta.fechaModificacion;
                entidad.activo = propEncuesta.activo;
            }
            return entidad;
        }

        /// <summary>
        /// Guardars the encuesta.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void GuardarEncuesta(Encuesta entidad)
        {
            objBLEncuesta = new BLEncuesta(entidad);
            objBLEncuesta.Save();
        }

        /// <summary>
        /// Cargars the entidad.
        /// </summary>
        private void CargarValoresEnPantalla(int idEntidad)
        {
            BLEncuesta objBLEncuesta = new BLEncuesta(new Encuesta() { idEncuesta = idEntidad });
            objBLEncuesta.GetById();
            propEncuesta = objBLEncuesta.Data;
        }

        /// <summary>
        /// Validars the pagina.
        /// </summary>
        /// <returns></returns>
        private string ValidarPagina()
        {
            string mensaje = string.Empty;
            return mensaje;
        }

        /// <summary>
        /// Cargas the agenda.
        /// </summary>
        private void CargaEncuesta()
        {
            AccionPagina = enumAcciones.Modificar;
            esNuevo = false;
            CargarValoresEnPantalla(propEncuesta.idEncuesta);
            //CargarGrillaEncuesta();
            HabilitarBotonesDetalle(propEncuesta.activo);
            btnBuscar.Visible = false;
            btnVolver.Visible = true;
            gvwEncuestas.Visible = false;
            udpFiltrosBusqueda.Visible = false;
            udpEdit.Visible = true;
            udpEdit.Update();
        }

        /// <summary>
        /// Buscars the filtrando.
        /// </summary>
        private void BuscarFiltrando()
        {
            lblTitulo.Text = "Encuestas";
            Encuesta entidad = new Encuesta();
            entidad.activo = chkActivo.Checked;
            int idAmbito = Convert.ToInt32(ddlAmbito.SelectedValue);
            propFiltroEncuesta = entidad;
            BuscarEncuesta(entidad);
        }

        /// <summary>
        /// Limpiar the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            //TODO (Pablo): Incluir estos componentes de filtrado
            if (ddlAmbito.Items.Count > 0) ddlAmbito.SelectedIndex = 0;
            HabilitarBotonesDetalle(false);
            chkActivo.Checked = true;
        }
        #endregion
    }
}