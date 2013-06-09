using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Security;
using EDUAR_BusinessLogic.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Utilidades;

namespace EDUAR_UI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RegisterUser : EDUARBasePage
    {
        #region --[Atributos]--
        private BLSeguridad atrBLSeguridad;
        private BLPersona atrBLPersona;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Propiedad que contiene el objeto seguridad que devuelve la consulta a la Capa de Negocio.
        /// </summary>
        public DTSeguridad propSeguridad
        {
            get
            {
                if (ViewState["objSeguridad"] == null)
                    return null;

                return (DTSeguridad)ViewState["objSeguridad"];
            }
            set { ViewState["objSeguridad"] = value; }
        }

        /// <summary>
        /// Gets or sets the prop persona.
        /// </summary>
        /// <value>
        /// The prop persona.
        /// </value>
        public Persona propPersona
        {
            get
            {
                if (ViewState["propPersona"] == null)
                    return new Persona();

                return (Persona)ViewState["propPersona"];
            }
            set { ViewState["propPersona"] = value; }
        }

        /// <summary>
        /// Gets or sets the prop lista personas.
        /// </summary>
        /// <value>
        /// The prop lista personas.
        /// </value>
        public List<Persona> propListaPersonas
        {
            get
            {
                if (ViewState["propListaPersonas"] == null)
                    return new List<Persona>();

                return (List<Persona>)ViewState["propListaPersonas"];
            }
            set { ViewState["propListaPersonas"] = value; }
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
                Master.BotonAvisoCancelar += (VentanaCancelar);

                if (!Page.IsPostBack)
                {
                    propSeguridad = new DTSeguridad();
                    atrBLSeguridad = new BLSeguridad(propSeguridad);
                    propPersona = new Persona();
                    propPersona.idTipoPersona = 1;
                    CargarPresentacion();
                    CargarCamposFiltros();
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
            }
        }

        /// <summary>
        /// Ejecuta las acciones requeridas por el boton aceptar de la ventana de informacion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VentanaAceptar(object sender, EventArgs e)
        {
            try
            {
                switch (AccionPagina)
                {
                    case enumAcciones.Guardar:
                        GuardarUsuario();
                        AccionPagina = enumAcciones.Limpiar;
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
                        break;
                    case enumAcciones.Salir:
                        Response.Redirect("~/Default.aspx", false);
                        break;
                    case enumAcciones.Limpiar:
                        propSeguridad = new DTSeguridad();
                        atrBLSeguridad = new BLSeguridad(propSeguridad);
                        propPersona = new Persona();
                        propListaPersonas = null;
                        propPersona.idTipoPersona = 1;
                        LimpiarCampos();
                        CargarPresentacion();
                        //CargarCamposFiltros();
                        CargarGrilla();
                        break;
                    default:
                        break;
                }
                udpRoles.Update();
                udpGrilla.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Ejecuta las acciones requeridas por el boton cancelar de la ventana de informacion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VentanaCancelar(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect("~/Default.aspx", false);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnGuardar control.
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
                    if (Page.IsValid)
                    {
                        AccionPagina = enumAcciones.Guardar;
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
                    }
                    else
                    {
                        AccionPagina = enumAcciones.Limpiar;
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosRequeridos, enumTipoVentanaInformacion.Advertencia);
                    }
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
        protected void gvwUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Nuevo":
                        propPersona = new Persona();
                        propPersona.idPersona = Convert.ToInt32(e.CommandArgument.ToString());
                        AccionPagina = enumAcciones.Modificar;
                        CargarPersona();
                        btnGuardar.Visible = true;
                        btnVolver.Visible = true;
                        btnBuscar.Visible = false;
                        udpRoles.Visible = true;
                        udpFiltrosBusqueda.Visible = false;
                        gvwUsuarios.Visible = false;
                        udpRoles.Update();
                        udpFiltros.Update();
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the gvwUsuarios control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvwUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvwUsuarios.PageIndex = e.NewPageIndex;
                CargarGrilla();
            }
            catch (Exception ex) { Master.ManageExceptions(ex); }
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the presentacion.
        /// </summary>
        private void CargarPresentacion()
        {
            btnBuscar.Visible = true;
            btnGuardar.Visible = false;
            btnVolver.Visible = false;
            udpFiltrosBusqueda.Visible = true;
            gvwUsuarios.Visible = true;
            udpRoles.Visible = false;
            udpGrilla.Update();
            udpFiltros.Update();
        }
        /// <summary>
        /// Cargars the campos filtros.
        /// </summary>
        private void CargarCamposFiltros()
        {
            atrBLSeguridad.GetRoles();
            UIUtilidades.BindCombo<DTRol>(ddlListRoles, atrBLSeguridad.Data.ListaRoles, "NombreCorto", "Nombre", true);
            UIUtilidades.BindComboTipoPersona(ddlTipoUsuario);
        }

        /// <summary>
        /// Buscars the filtrando.
        /// </summary>
        private void BuscarFiltrando()
        {
            Persona objPersona = new Persona();
            objPersona.nombre = txtNombreBusqueda.Text;
            objPersona.apellido = txtApellidoBusqueda.Text;
            objPersona.activo = chkActivoBusqueda.Checked;
            objPersona.username = null;
            objPersona.idTipoPersona = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
            BuscarPersona(objPersona);
        }

        /// <summary>
        /// Obteners the datos.
        /// </summary>
        private void BuscarPersona(Persona objPersona)
        {
            atrBLPersona = new BLPersona(objPersona);
            propListaPersonas = atrBLPersona.GetPersonas(objPersona);
            CargarGrilla();
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        private void CargarGrilla()
        {
            gvwUsuarios.DataSource = propListaPersonas;
            gvwUsuarios.DataBind();
            gvwUsuarios.SelectedIndex = -1;
            udpRoles.Visible = false;
            udpGrilla.Update();
        }

        /// <summary>
        /// Cargars the usuario.
        /// </summary>
        private void CargarPersona()
        {
			LimpiarCampos();
            atrBLPersona = new BLPersona();
            //propPersona.username = string.Empty;
            atrBLPersona.Data = propPersona;
            atrBLPersona.GetById();
            Persona objPersona = atrBLPersona.Data;
            lblNombreApellido.Text = objPersona.nombre + " " + objPersona.apellido;
            txtUserName.Text = string.Empty;
            txtEmailUsuario.Text = objPersona.email;
            lblPreguntaUsuario.Text = BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.PreguntaDefault);
            lblRespuestaUsuario.Text = objPersona.numeroDocumento.ToString();

            udpRoles.Visible = true;
            udpRoles.Update();
        }

        /// <summary>
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            txtUserName.Text = string.Empty;
            txtNombreBusqueda.Text = string.Empty;
            txtApellidoBusqueda.Text = string.Empty;
            txtEmailUsuario.Text = string.Empty;
            ddlListRoles.SelectedValue = "-1"; //SELECCIONE
            ddlTipoUsuario.SelectedValue = "0"; //TODOS
        }

        /// <summary>
        /// Guardars the usuario.
        /// </summary>
        private void GuardarUsuario()
        {
            DTUsuario objUsuario = new DTUsuario();
            objUsuario.Nombre = txtUserName.Text;
            objUsuario.Email = txtEmailUsuario.Text;
            objUsuario.Aprobado = chkHabilitado.Checked;
            objUsuario.PaswordPregunta = lblPreguntaUsuario.Text;
            objUsuario.PaswordRespuesta = lblRespuestaUsuario.Text;
            objUsuario.EsUsuarioInicial = true;
            objUsuario.ListaRoles = new List<DTRol>();
            objUsuario.ListaRoles.Add(new DTRol() { NombreCorto = ddlListRoles.SelectedValue, Nombre = ddlListRoles.SelectedItem.Text });

            DTSeguridad objSeguridad = new DTSeguridad();
            objSeguridad.Usuario = objUsuario;
            atrBLSeguridad = new BLSeguridad(objSeguridad);
            atrBLSeguridad.CrearUsuario();

            Persona objPersona = new Persona();
            atrBLPersona = new BLPersona(propPersona);
            atrBLPersona.GetById();
            atrBLPersona.Data.username = objUsuario.Nombre;
            atrBLPersona.Save();

        }

        /// <summary>
        /// Validars the pagina.
        /// </summary>
        private bool ValidarPagina()
        {
            if (txtUserName.Text.Trim().Length == 0)
            {
                AccionPagina = enumAcciones.Limpiar;
                Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosRequeridos, enumTipoVentanaInformacion.Advertencia);
                return false;
            }
            if (!EDUARUtilidades.EsEmailValido(txtEmailUsuario.Text.Trim()))
            {
                AccionPagina = enumAcciones.Limpiar;
                Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosRequeridos, enumTipoVentanaInformacion.Advertencia);
                return false;
            }
            if (ddlListRoles.SelectedValue == "0" || ddlListRoles.SelectedValue == "-1")
            {
                AccionPagina = enumAcciones.Limpiar;
                Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosRequeridos, enumTipoVentanaInformacion.Advertencia);
                return false;
            }
            return true;
        }
        #endregion
    }
}