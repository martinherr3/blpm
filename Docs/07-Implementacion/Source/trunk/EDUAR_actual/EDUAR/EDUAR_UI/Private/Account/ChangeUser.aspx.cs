using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI
{
    public partial class ChangeUser : EDUARBasePage
    {
        #region --[Atributos]--
        private BLSeguridad objBLSeguridad;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Propiedad que contiene el objeto seguridad que devuelve la consulta a la Capa de Negocio.
        /// </summary>
        public DTSeguridad propSeguridad
        {
            get
            {
                if (ViewState["propSeguridad"] == null)
                    return null;

                return (DTSeguridad)ViewState["propSeguridad"];
            }
            set { ViewState["propSeguridad"] = value; }
        }

        /// <summary>
        /// Propiedad que contiene el objeto usuario que se edita.
        /// </summary>
        /// <value>
        /// The obj usuario.
        /// </value>
        public DTUsuario propUsuario
        {
            get
            {
                if (ViewState["propUsuario"] == null)
                    return new DTUsuario();

                return (DTUsuario)ViewState["propUsuario"];
            }
            set { ViewState["propUsuario"] = value; }
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
                    propSeguridad = new DTSeguridad();
                    propSeguridad.Usuario.Aprobado = chkHabilitadoBusqueda.Checked;
                    objBLSeguridad = new BLSeguridad(propSeguridad);
                    CargarCamposFiltros();
                    BuscarUsuarios(propSeguridad.Usuario);
                    CargarPresentacion();
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
                    case enumAcciones.Guardar:
                        GuardarUsuario();
                        AccionPagina = enumAcciones.Limpiar;
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
                        LimpiarCampos();
                        udpRoles.Visible = false;
                        propSeguridad = new DTSeguridad();
                        propSeguridad.Usuario.Aprobado = chkHabilitadoBusqueda.Checked;
                        objBLSeguridad = new BLSeguridad(propSeguridad);
                        BuscarUsuarios(propSeguridad.Usuario);
                        CargarPresentacion();
                        break;
                    case enumAcciones.Salir:
                        Response.Redirect("~/Default.aspx", false);
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
                propUsuario = new DTUsuario();
                propUsuario.Nombre = e.CommandArgument.ToString();

                switch (e.CommandName)
                {
                    case "Editar":
                        AccionPagina = enumAcciones.Modificar;
                        CargarUsuario();
                        btnGuardar.Visible = true;
                        btnVolver.Visible = true;
                        btnBuscar.Visible = false;
                        udpRoles.Visible = true;
                        udpFiltrosBusqueda.Visible = false;
                        gvwUsuarios.Visible = false;
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
        /// Handles the PageIndexChanging event of the gvwReporte control.
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
            CargarCombos();
        }


        /// <summary>
        /// Gets or sets the lista de usuarios.
        /// </summary>
        /// <value>
        /// The lista DTUSuario.
        /// </value>
        public List<DTUsuario> listaUsuarios
        {
            get
            {
                if (ViewState["listaUsuarios"] == null)
                {
                    listaUsuarios = propSeguridad.ListaUsuarios;
                }
                return (List<DTUsuario>)ViewState["listaUsuarios"];
            }
            set
            {
                ViewState["listaUsuarios"] = value;
            }
        }

        /// <summary>
        /// Cargars the campos filtros.
        /// </summary>
        private void CargarCamposFiltros()
        {
            objBLSeguridad.GetRoles();
            UIUtilidades.BindCombo<DTRol>(ddlListRolesBusqueda, objBLSeguridad.Data.ListaRoles, "NombreCorto", "Nombre", true);
            UIUtilidades.BindCombo<DTRol>(ddlListRoles, objBLSeguridad.Data.ListaRoles, "NombreCorto", "Nombre", true);
        }

        /// <summary>
        /// Buscars the filtrando.
        /// </summary>
        private void BuscarFiltrando()
        {
            DTUsuario usuario = new DTUsuario();
            foreach (System.Web.UI.WebControls.ListItem item in ddlUser.Items)
            {
                if (item.Selected)
                {
                    usuario.Nombre = item.Text;
                }
            }


            usuario.Aprobado = chkHabilitadoBusqueda.Checked;

            List<DTRol> ListaRoles = new List<DTRol>();
            if (ddlListRolesBusqueda.SelectedValue != "-1" && ddlListRolesBusqueda.SelectedValue != "0")
                ListaRoles.Add(new DTRol() { Nombre = ddlListRolesBusqueda.SelectedValue });
            usuario.ListaRoles = ListaRoles;
            BuscarUsuarios(usuario);
        }

        /// <summary>
        /// Obteners the datos.
        /// </summary>
        private void BuscarUsuarios(DTUsuario objUsuario)
        {
            DTSeguridad seguridad = new DTSeguridad();
            seguridad.Usuario = objUsuario;
            seguridad.ListaRoles = objUsuario.ListaRoles;
            objBLSeguridad = new BLSeguridad(seguridad);
            objBLSeguridad.Data = seguridad;
            objBLSeguridad.GetUsuarios();
            propSeguridad = objBLSeguridad.Data;
            CargarGrilla();
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        private void CargarGrilla()
        {
            gvwUsuarios.DataSource = propSeguridad.ListaUsuarios;
            gvwUsuarios.DataBind();
            gvwUsuarios.SelectedIndex = -1;
            udpRoles.Visible = false;
            udpGrilla.Update();
        }

        /// <summary>
        /// Cargars the usuario.
        /// </summary>
        private void CargarUsuario()
        {
            LimpiarCampos();

            objBLSeguridad = new BLSeguridad();
            objBLSeguridad.Data.Usuario = new DTUsuario();
            objBLSeguridad.Data.Usuario = propUsuario;
            objBLSeguridad.GetUsuario();
            propUsuario = objBLSeguridad.Data.Usuario;
            lblUserName.Text = propUsuario.Nombre;
            chkHabilitado.Checked = propUsuario.Aprobado;

            #region Carga los roles
            if (propUsuario.ListaRoles.Count > 0)
                ddlListRoles.SelectedValue = propUsuario.ListaRoles[0].NombreCorto;
            udpRoles.Visible = true;
            udpRoles.Update();
            #endregion
        }

        /// <summary>
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            lblUserName.Text = string.Empty;
            ddlUser.SelectedIndex = -1;
            ddlListRolesBusqueda.SelectedValue = "-1";
            chkHabilitadoBusqueda.Checked = true;
            propSeguridad.ListaUsuarios.Clear();
            udpFiltrosBusqueda.Update();
        }

        /// <summary>
        /// Guardars the usuario.
        /// </summary>
        private void GuardarUsuario()
        {
            List<DTRol> listaRoles = new List<DTRol>();
            listaRoles.Add(new DTRol() { NombreCorto = ddlListRoles.SelectedValue, Nombre = ddlListRoles.SelectedItem.Text });

            DTSeguridad objDTSeguridad = new DTSeguridad { Usuario = { Nombre = lblUserName.Text.Trim(), ListaRoles = listaRoles, Aprobado = chkHabilitado.Checked } };
            objBLSeguridad = new BLSeguridad();
            objBLSeguridad.Data = objDTSeguridad;
            objBLSeguridad.ActualizarUsuario();

            //BuscarFiltrando();
            LimpiarCampos();

            udpRoles.Visible = false;
            udpRoles.Update();
        }

        /// <summary>
        /// Validars the pagina.
        /// </summary>
        private bool ValidarPagina()
        {
            if (ddlListRoles.SelectedValue == "0" || ddlListRoles.SelectedValue == "-1")
            {
                AccionPagina = enumAcciones.Limpiar;
                Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosRequeridos, enumTipoVentanaInformacion.Advertencia);
                return false;
            }
            return true;
        }

        private void CargarCombos()
        {
            #region --[Motivo Sanción]--

            // Ordena la lista alfabéticamente por nombre de usuario
            listaUsuarios.Sort((p, q) => string.Compare(p.Nombre, q.Nombre));

            if (ddlUser.Items.Count == 0)
            {
                // Carga el combo de Usuarios para filtrar
                foreach (DTUsuario item in listaUsuarios)
                {
                    ddlUser.Items.Add(new System.Web.UI.WebControls.ListItem(item.Nombre, item.ID.ToString()));
                }
            }
            #endregion
        }


    }
        #endregion
}