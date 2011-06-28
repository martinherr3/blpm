using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;

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
                        AccionPagina = enumAcciones.Salir;
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
                        break;
                    case enumAcciones.Salir:
                        Response.Redirect("~/Default.aspx", false);
                        break;
                    default:
                        break;
                }
                LimpiarCampos();
                udpRoles.Visible = false;
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
                AvisoMostrar = true;
                AvisoExcepcion = ex;
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
                if (Page.IsValid)
                {
                    AccionPagina = enumAcciones.Guardar;
                    Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
                }
                else
                {
                    AccionPagina = enumAcciones.Salir;
                    Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosRequeridos, enumTipoVentanaInformacion.Advertencia);
                }
                //Master.MostrarMensaje("Errores", "email inválido", enumTipoVentanaInformacion.Error);
                //Master.MostrarMensaje("Errores", "email inválido", enumTipoVentanaInformacion.Advertencia);
                //Master.MostrarMensaje("Errores", "email inválido", enumTipoVentanaInformacion.Confirmación);
                //Master.MostrarMensaje("Errores", "email inválido", enumTipoVentanaInformacion.Satisfactorio);
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
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
                    case "Editar":
                        propPersona = new Persona();
                        propPersona.idPersona = Convert.ToInt32(e.CommandArgument.ToString());
                        AccionPagina = enumAcciones.Modificar;
                        CargarPersona();
                        break;
                }
                udpRoles.Visible = true;
                udpRoles.Update();
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
        /// Cargars the campos filtros.
        /// </summary>
        private void CargarCamposFiltros()
        {
            atrBLSeguridad.GetRoles();
            foreach (DTRol rol in atrBLSeguridad.Data.ListaRoles)
            {
                chkListRoles.Items.Add(new ListItem(rol.Nombre, rol.NombreCorto));
            }
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
            atrBLPersona = new BLPersona();
            atrBLPersona.Data = propPersona;
            atrBLPersona.GetById();
            Persona objPersona = atrBLPersona.Data;
            txtUserName.Text = string.Empty;
            lblEmailUsuario.Text = objPersona.email;

            LimpiarCampos();

            udpRoles.Visible = true;
            udpRoles.Update();
        }

        /// <summary>
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            txtUserName.Text = string.Empty;
            foreach (ListItem item in chkListRoles.Items)
            { item.Selected = false; }
        }

        /// <summary>
        /// Guardars the usuario.
        /// </summary>
        private void GuardarUsuario()
        {
            DTUsuario objUsuario = new DTUsuario();
            objUsuario.Nombre = txtUserName.Text;
            objUsuario.Email = lblEmailUsuario.Text;
            objUsuario.Aprobado = chkHabilitado.Checked;

            objUsuario.ListaRoles = new List<DTRol>();
            foreach (ListItem item in chkListRoles.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    DTRol objDTRol = new DTRol { Nombre = item.Value };
                    objUsuario.ListaRoles.Add(objDTRol);
                }
            }

            DTSeguridad objSeguridad = new DTSeguridad();
            objSeguridad.Usuario = objUsuario;
            atrBLSeguridad = new BLSeguridad(objSeguridad);
            atrBLSeguridad.CrearUsuario();
        }
        #endregion
    }
}