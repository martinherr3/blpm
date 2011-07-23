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
                if (!Page.IsPostBack)
                {
                    propSeguridad = new DTSeguridad();
                    propSeguridad.Usuario.Aprobado = chkHabilitadoBusqueda.Checked;
                    objBLSeguridad = new BLSeguridad(propSeguridad);
                    CargarCamposFiltros();
                    BuscarUsuarios(propSeguridad.Usuario);
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
                AccionPagina = enumAcciones.Guardar;
                Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(),
                    UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
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
                    //case "Eliminar":
                    //    AccionPagina = enumAcciones.Eliminar;
                    //    Master.MostrarMensaje(UIConstantesGenerales.NotuAdmu0000, UIConstantesGenerales.NotuAdmu0003.Replace("[Identificador]", UserName), enumTipoVentanaInformacion.Confirmación);
                    //    break;
                    case "Editar":
                        AccionPagina = enumAcciones.Modificar;
                        CargarUsuario();
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


        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the campos filtros.
        /// </summary>
        private void CargarCamposFiltros()
        {
            objBLSeguridad.GetRoles();
            foreach (DTRol rol in objBLSeguridad.Data.ListaRoles)
            {
                chkListRolesBusqueda.Items.Add(new ListItem(rol.Nombre, rol.NombreCorto));
                chkListRoles.Items.Add(new ListItem(rol.Nombre, rol.NombreCorto));
            }
        }

        private void BuscarFiltrando()
        {
            DTUsuario usuario = new DTUsuario();
            usuario.Nombre = txtUsernameBusqueda.Text;
            usuario.Aprobado = chkHabilitadoBusqueda.Checked;

            List<DTRol> ListaRoles = new List<DTRol>();
            foreach (ListItem item in chkListRolesBusqueda.Items)
            {
                if (item.Selected)
                {
                    ListaRoles.Add(new DTRol() { Nombre = item.Value });
                }
            }
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
            foreach (DTRol rol in propUsuario.ListaRoles)
            {
                foreach (ListItem item in chkListRoles.Items)
                {
                    if (item.Value == rol.NombreCorto)
                    {
                        item.Selected = true;
                    }
                }
            }
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
            foreach (ListItem item in chkListRoles.Items)
            { item.Selected = false; }
        }


        /// <summary>
        /// Guardars the usuario.
        /// </summary>
        private void GuardarUsuario()
        {
            List<DTRol> listaRoles = new List<DTRol>();
            foreach (ListItem item in chkListRoles.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    DTRol objDTRol = new DTRol { Nombre = item.Value };
                    listaRoles.Add(objDTRol);
                }
            }

            DTSeguridad objDTSeguridad = new DTSeguridad { Usuario = { Nombre = lblUserName.Text.Trim(), ListaRoles = listaRoles, Aprobado = chkHabilitado.Checked } };
            objBLSeguridad = new BLSeguridad();
            objBLSeguridad.Data = objDTSeguridad;
            objBLSeguridad.ActualizarUsuario();

            BuscarFiltrando();
            LimpiarCampos();

            udpRoles.Visible = false;
            udpRoles.Update();
        }
        #endregion
    }
}