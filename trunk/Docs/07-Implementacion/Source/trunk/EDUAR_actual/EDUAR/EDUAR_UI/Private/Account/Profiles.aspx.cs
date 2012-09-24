using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
    public partial class Profiles : EDUARBasePage
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
        /// Propiedad que contiene el objeto rol que se edita/crea/elimina.
        /// </summary>
        /// <value>
        /// The obj propRol.
        /// </value>
        public DTRol propRol
        {
            get
            {
                if (ViewState["propRol"] == null)
                    return new DTRol();

                return (DTRol)ViewState["propRol"];
            }
            set { ViewState["propRol"] = value; }
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
                    objBLSeguridad = new BLSeguridad(propSeguridad);
                    CargarPresentacion();
                    CargarCamposFiltros();
                    BuscarRoles(propSeguridad);
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
                DTRol objRol;
                switch (AccionPagina)
                {
                    case enumAcciones.Eliminar:
                        AccionPagina = enumAcciones.Limpiar;
                        objRol = new DTRol();
                        objRol.Nombre = propRol.Nombre;
                        EliminarRol(objRol);
                        break;
                    case enumAcciones.Guardar:
                        AccionPagina = enumAcciones.Limpiar;
                        objRol = new DTRol();
                        if (esNuevo)
                        {
                            objRol.Descripcion = txtNuevaDescripcion.Text;
                            objRol.Nombre = txtNuevoRol.Text;
                            objRol.NombreCorto = objRol.Nombre.ToLower();
                            objRol.RoleId = null;
                        }
                        else
                        {
                            objRol.Descripcion = txtDescripcion.Text;
                            objRol.RoleId = propRol.RoleId;
                            objRol.Nombre = propRol.Nombre;
                        }
                        GuardarRol(objRol);
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
                        break;
                    case enumAcciones.Nuevo:
                        break;

                }
                CargarPresentacion();
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
        /// Handles the Click event of the btnNuevo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = enumAcciones.Nuevo;
                esNuevo = true;
                btnGuardar.Visible = true;
                btnBuscar.Visible = false;
                btnVolver.Visible = true;
                btnNuevo.Visible = false;
                gvwPerfiles.Visible = false;
                udpEditRoles.Visible = false;
                udpControlesBusqueda.Visible = false;
                udpNewRol.Visible = true;
                udpFiltros.Update();
                udpGrilla.Update();
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
        protected void gvwPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Eliminar":
                        AccionPagina = enumAcciones.Eliminar;
                        propRol = new DTRol();
                        propRol.Nombre = e.CommandArgument.ToString();
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeEliminar, enumTipoVentanaInformacion.Confirmación);
                        break;
                    case "Editar":
                        AccionPagina = enumAcciones.Modificar;
                        esNuevo = false;
                        propRol = new DTRol();
                        propRol.RoleId = e.CommandArgument.ToString();
                        propRol.Nombre = lblNombreRol.Text;
                        btnBuscar.Visible = false;
                        btnNuevo.Visible = false;
                        btnVolver.Visible = true;
                        btnGuardar.Visible = true;
                        CargarRol();
                        udpEditRoles.Visible = true;
                        udpEditRoles.Update();
                        break;
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
        /// Cargars the presentacion.
        /// </summary>
        private void CargarPresentacion()
        {
            LimpiarCampos();
            udpEditRoles.Visible = false;
            udpNewRol.Visible = false;
            btnVolver.Visible = true;
            btnGuardar.Visible = false;
            udpControlesBusqueda.Visible = true;
            btnNuevo.Visible = true;
            btnBuscar.Visible = true;
            gvwPerfiles.Visible = true;
            udpFiltros.Update();
            udpGrilla.Update();
            BuscarFiltrando();
        }

        /// <summary>
        /// Cargars the campos filtros.
        /// </summary>
        private void CargarCamposFiltros()
        {
            objBLSeguridad.GetRoles();
            foreach (DTRol rol in objBLSeguridad.Data.ListaRoles)
            {
                chkListRolesBusqueda.Items.Add(new ListItem(rol.Nombre, rol.NombreCorto));
            }
        }

        private void BuscarFiltrando()
        {
            List<DTRol> ListaRoles = new List<DTRol>();
            foreach (ListItem item in chkListRolesBusqueda.Items)
            {
                if (item.Selected)
                {
                    ListaRoles.Add(new DTRol() { Nombre = item.Value });
                }
            }
            DTSeguridad objSeguridad = new DTSeguridad();
            objSeguridad.ListaRoles = ListaRoles;
            BuscarRoles(objSeguridad);
        }

        /// <summary>
        /// Obteners the datos.
        /// </summary>
        private void BuscarRoles(DTSeguridad objSeguridad)
        {
            objBLSeguridad = new BLSeguridad(objSeguridad);
            objBLSeguridad.Data = objSeguridad;
            objBLSeguridad.GetRoles();
            propSeguridad = objBLSeguridad.Data;
            CargarGrilla();
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        private void CargarGrilla()
        {
            gvwPerfiles.DataSource = propSeguridad.ListaRoles;
            gvwPerfiles.DataBind();
            gvwPerfiles.SelectedIndex = -1;
            udpEditRoles.Visible = false;
            udpGrilla.Update();
        }

        /// <summary>
        /// Cargars the usuario.
        /// </summary>
        private void CargarRol()
        {
            LimpiarCampos();

            objBLSeguridad = new BLSeguridad();
            objBLSeguridad.Data.Rol = new DTRol();
            objBLSeguridad.Data.Rol = propRol;
            objBLSeguridad.GetRol();
            propRol = objBLSeguridad.Data.Rol;
            lblNombreRol.Text = propRol.Nombre;
            txtDescripcion.Text = propRol.Descripcion;

            udpControlesBusqueda.Visible = false;
            udpControlesBusqueda.Update();

            gvwPerfiles.Visible = false;
            udpEditRoles.Visible = true;
            udpEditRoles.Update();
        }

        /// <summary>
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            lblNombreRol.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            foreach (ListItem item in chkListRolesBusqueda.Items)
            { item.Selected = false; }
        }


        /// <summary>
        /// Guardars the rol.
        /// </summary>
        private void GuardarRol(DTRol objRol)
        {
            DTSeguridad objDTSeguridad = new DTSeguridad { Rol = objRol };
            objBLSeguridad = new BLSeguridad();
            objBLSeguridad.Data = objDTSeguridad;
            objBLSeguridad.GuardarRol();

            BuscarRoles(propSeguridad);
            LimpiarCampos();

        }

		/// <summary>
		/// Eliminars the rol.
		/// </summary>
		/// <param name="objRol">The obj rol.</param>
        private void EliminarRol(DTRol objRol)
        {
            DTSeguridad objDTSeguridad = new DTSeguridad { Rol = objRol };
            objBLSeguridad = new BLSeguridad();
            objBLSeguridad.Data = objDTSeguridad;
            objBLSeguridad.EliminarRol();

            BuscarRoles(propSeguridad);
            LimpiarCampos();
        }
        #endregion
    }
}