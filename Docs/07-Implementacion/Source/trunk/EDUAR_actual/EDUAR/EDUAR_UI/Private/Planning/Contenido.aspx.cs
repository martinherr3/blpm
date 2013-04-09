using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
    public partial class Contenido : EDUARBasePage
    {
        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the lista Niveles.
        /// </summary>
        /// <value>
        /// The lista Niveles.
        /// </value>
        public List<Nivel> listaNiveles
        {
            get
            {
                if (ViewState["listaNiveles"] == null && cicloLectivoActual != null)
                {
                    BLNivel objBLNivel = new BLNivel();
                    listaNiveles = objBLNivel.GetNiveles();
                }
                return (List<Nivel>)ViewState["listaNiveles"];
            }
            set { ViewState["listaNiveles"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista contenido.
        /// </summary>
        /// <value>
        /// The lista contenido.
        /// </value>
        protected List<EDUAR_Entities.Contenido> listaContenido
        {
            get
            {
                if (ViewState["listaContenido"] == null)
                    ViewState["listaContenido"] = new List<EDUAR_Entities.Contenido>();
                return (List<EDUAR_Entities.Contenido>)ViewState["listaContenido"];
            }
            set { ViewState["listaContenido"] = value; }
        }

        /// <summary>
        /// Gets or sets the id Nivel.
        /// </summary>
        /// <value>
        /// The id Nivel.
        /// </value>
        public int idNivel
        {
            get
            {
                if (Session["idNivel"] == null)
                    idNivel = 0;
                return (int)Session["idNivel"];
            }
            set { Session["idNivel"] = value; }
        }

        /// <summary>
        /// Gets or sets the id orientacion.
        /// </summary>
        /// <value>
        /// The id orientacion.
        /// </value>
        public int idOrientacion
        {
            get
            {
                if (Session["idOrientacion"] == null)
                    idOrientacion = 0;
                return (int)Session["idOrientacion"];
            }
            set { Session["idOrientacion"] = value; }
        }

        /// <summary>
        /// Gets or sets the id contenido.
        /// </summary>
        /// <value>
        /// The id contenido.
        /// </value>
        public int idContenido
        {
            get
            {
                if (ViewState["idContenido"] == null)
                    ViewState["idContenido"] = 0;
                return (int)ViewState["idContenido"];
            }
            set { ViewState["idContenido"] = value; }
        }

        /// <summary>
        /// Gets or sets the contenido editar.
        /// </summary>
        /// <value>
        /// The contenido editar.
        /// </value>
        public EDUAR_Entities.Contenido contenidoEditar
        {
            get
            {
                if (Session["contenidoEditar"] == null)
                    Session["contenidoEditar"] = new EDUAR_Entities.Contenido();
                return (EDUAR_Entities.Contenido)Session["contenidoEditar"];
            }
            set { Session["contenidoEditar"] = value; }
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
                    CargarPresentacion();
                    if (Request.UrlReferrer.AbsolutePath.Contains("TemasContenido.aspx"))
                    {
                        ddlNivel.SelectedValue = idNivel.ToString();
                        ddlAsignatura.Enabled = true;
                        CargarComboAsignatura(idNivel);
                        ddlAsignatura.SelectedValue = idAsignatura.ToString();
                        ddlAsignatura.Enabled = idNivel > 0;
                        btnNuevo.Visible = idAsignatura > 0;
                        CargarContenido();
                    }
                }
                //else
                //{
                //    if (Request.Form["__EVENTTARGET"] == "btnGuardar")
                //        //llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Button2
                //        btnGuardar_Click(this, new EventArgs());
                //}
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
        protected void VentanaAceptar(object sender, EventArgs e)
        {
            try
            {
                switch (AccionPagina)
                {
                    case enumAcciones.Buscar:
                        break;
                    case enumAcciones.Nuevo:
                        break;
                    case enumAcciones.Modificar:
                        break;
                    case enumAcciones.Eliminar:
                        EliminarContenido();
                        break;
                    case enumAcciones.Seleccionar:
                        break;
                    case enumAcciones.Limpiar:
                        break;
                    case enumAcciones.Aceptar:
                        break;
                    case enumAcciones.Salir:
                        break;
                    case enumAcciones.Redirect:
                        break;
                    case enumAcciones.Guardar:
                        break;
                    case enumAcciones.Ingresar:
                        break;
                    case enumAcciones.Desbloquear:
                        break;
                    case enumAcciones.Error:
                        AccionPagina = enumAcciones.Limpiar;
                        mpeContenido.Show();
                        break;
                    case enumAcciones.Enviar:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
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

            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
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
                //pnlContenidos.Attributes["display"] = "inherit";
                //pnlContenidos.Visible = true;
                mpeContenido.Show();
                //udpBotonera.Update();
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
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
                if (!string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    EDUAR_Entities.Contenido nuevoContenido = new EDUAR_Entities.Contenido();
                    //nuevoContenido.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignatura;
                    nuevoContenido.descripcion = txtDescripcion.Text;
                    nuevoContenido.idContenido = idContenido;

                    GuardarContenido(nuevoContenido);

                    txtDescripcion.Text = string.Empty;
                    mpeContenido.Hide();
                }
                else
                {
                    AccionPagina = enumAcciones.Error;
                    mpeContenido.Hide();
                    Master.MostrarMensaje("Datos Faltantes", "Por favor, ingrese una descripción válida.", enumTipoVentanaInformacion.Advertencia);
                }
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
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
                txtDescripcion.Text = string.Empty;
                mpeContenido.Hide();
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlNivel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idNivel = 0;
                int.TryParse(ddlNivel.SelectedValue, out idNivel);
                gvwContenido.DataSource = null;
                gvwContenido.DataBind();
                if (idNivel > 0)
                {
                    this.idNivel = idNivel;
                    CargarComboAsignatura(idNivel);
                }
                else
                {
                    ddlAsignatura.SelectedIndex = 0;
                    ddlAsignatura.Items.Clear();
                    ddlAsignatura.Items.Add("[Seleccione Nivel]");
                }
                ddlOrientacion.Items.Clear();
                ddlAsignatura.Enabled = idNivel > 0;
                udpAsignatura.Update();
                VerOrientacion(false);
                udpGrilla.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlAsignatura control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlAsignatura_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idAsignatura = 0;
                int.TryParse(ddlAsignatura.SelectedValue, out idAsignatura);
                if (idAsignatura > 0)
                {
                    this.idAsignatura = idAsignatura;
                    //CargarContenido(idAsignatura);
                    CargarOrientacion();
                }
                else
                    VerOrientacion(false);

                udpBotonera.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlOrientacion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlOrientacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idOrientacion = 0;
                int.TryParse(ddlOrientacion.SelectedValue, out idOrientacion);
                btnNuevo.Visible = idOrientacion > 0;
                if (idOrientacion > 0)
                {
                    this.idOrientacion = idOrientacion;
                    CargarGrilla();
                }
                udpBotonera.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the gvwContenido control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvwContenido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        /// <summary>
        /// Handles the RowCommand event of the gvwContenido control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gvwContenido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        lblTitulo.Text = "Editar Contenido";
                        idContenido = Convert.ToInt32(e.CommandArgument.ToString());
                        var lista = listaContenido.Find(p => p.idContenido == idContenido);
                        txtDescripcion.Text = lista.descripcion;
                        udpBotonera.Update();
                        mpeContenido.Show();
                        break;
                    case "Eliminar":
                        AccionPagina = enumAcciones.Eliminar;
                        idContenido = Convert.ToInt32(e.CommandArgument.ToString());
                        Master.MostrarMensaje("Eliminar Contenido", "¿Desea <b>eliminar</b> el contenido seleccionado y todos sus temas asociados?", enumTipoVentanaInformacion.Confirmación);
                        //EliminarContenido();
                        break;
                    case "Temas":
                        AccionPagina = enumAcciones.Redirect;
                        idContenido = Convert.ToInt32(e.CommandArgument.ToString());
                        contenidoEditar = listaContenido.Find(p => p.idContenido == idContenido);
                        Response.Redirect("TemasContenido.aspx", false);
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
            UIUtilidades.BindCombo<Nivel>(ddlNivel, listaNiveles, "idNivel", "Nombre", true);
            ddlAsignatura.Items.Add("[Seleccione Nivel]");
            pnlContenidos.Attributes["display"] = "none";
            //pnlContenidos.Visible = false;
            VerOrientacion(false);
            udpBotonera.Update();
        }

        /// <summary>
        /// Cargars the asignaturas.
        /// </summary>
        private void CargarComboAsignatura(int idNivel)
        {
            List<Asignatura> listaAsignaturas = new List<Asignatura>();
            BLAsignatura objBLAsignatura = new BLAsignatura();
            Nivel Nivel = new Nivel();
            Docente docente = null;
            if (User.IsInRole(enumRoles.Docente.ToString()))
            {
                docente = new Docente();
                docente.username = User.Identity.Name;
            }
            Nivel.idNivel = idNivel;
            listaAsignaturas = objBLAsignatura.GetAsignaturasNivel(Nivel);
            if (listaAsignaturas != null && listaAsignaturas.Count > 0)
                UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, listaAsignaturas, "idAsignatura", "nombre", true);
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        private void CargarGrilla()
        {
            gvwContenido.DataSource = UIUtilidades.BuildDataTable<EDUAR_Entities.Contenido>(listaContenido).DefaultView;
            gvwContenido.DataBind();
            udpGrilla.Update();
        }

        /// <summary>
        /// Guardars the contenido.
        /// </summary>
        /// <param name="nuevoContenido">The nuevo contenido.</param>
        private void GuardarContenido(EDUAR_Entities.Contenido nuevoContenido)
        {
            Curricula curricula = new Curricula();
            curricula.nivel.idNivel = this.idNivel;
            curricula.asignatura.idAsignatura = this.idAsignatura;
            curricula.orientacion.idOrientacion = this.idOrientacion;
            curricula.personaModificacion.username = User.Identity.Name;
            curricula.personaAlta.username = User.Identity.Name;

            BLCurricula objBLCurricula = new BLCurricula();
            objBLCurricula.GuardarContenidos(curricula, nuevoContenido);

            lblTitulo.Text = "Nuevo Contenido";
            txtDescripcion.Text = string.Empty;
            idContenido = 0;
            pnlContenidos.Attributes["display"] = "none";
            udpBotonera.Update();
            CargarContenido();
        }

        /// <summary>
        /// Eliminars the contenido.
        /// </summary>
        private void EliminarContenido()
        {
            EDUAR_Entities.Contenido objEliminar = new EDUAR_Entities.Contenido();
            objEliminar.idContenido = idContenido;
            objEliminar.usuarioBaja.username = User.Identity.Name;
            BLContenido ojbBLContenido = new BLContenido(objEliminar);
            ojbBLContenido.EliminarContenidos();

            CargarContenido();
        }

        /// <summary>
        /// Cargars the contenido.
        /// </summary>
        /// <param name="idAsignaturaCicloLectivo">The id asignatura ciclo lectivo.</param>
        private void CargarContenido()
        {
            Curricula entidad = new Curricula();
            entidad.nivel.idNivel = this.idNivel;
            entidad.asignatura.idAsignatura = this.idAsignatura;
            entidad.orientacion.idOrientacion = this.idOrientacion;

            BLCurricula objBL = new BLCurricula();
            listaContenido = objBL.GetCurriculaAsignaturaNivel(entidad);
            CargarGrilla();
        }

        /// <summary>
        /// Cargars the orientacion.
        /// </summary>
        /// <param name="idAsignaturaNivel">The id asignatura nivel.</param>
        private void CargarOrientacion()
        {
            AsignaturaNivel objAsignatura = new AsignaturaNivel();
            objAsignatura.asignatura.idAsignatura = this.idAsignatura;
            objAsignatura.nivel.idNivel = this.idNivel;
            BLOrientacion objBLOrientacion = new BLOrientacion();
            List<Orientacion> listaOrientaciones = objBLOrientacion.GetOrientacionesByAsignaturaNivel(objAsignatura);
            if (listaOrientaciones != null && listaOrientaciones.Count > 0)
            {
                UIUtilidades.BindCombo<Orientacion>(ddlOrientacion, listaOrientaciones, "idOrientacion", "nombre", true, false, "Nivel");
                if (listaOrientaciones.Count == 1)
                {
                    ddlOrientacion.SelectedIndex = 1;
                    idOrientacion = listaOrientaciones[0].idOrientacion;
                    CargarContenido();
                }
                btnNuevo.Visible = listaOrientaciones.Count == 1;
                ddlOrientacion.Enabled = !(listaOrientaciones.Count == 1);
            }

            VerOrientacion(listaOrientaciones != null);
            udpOrientacion.Update();
        }

        /// <summary>
        /// Vers the orientacion.
        /// </summary>
        /// <param name="verCampos">if set to <c>true</c> [ver campos].</param>
        private void VerOrientacion(bool verCampos)
        {
            lblOrientacion.Visible = verCampos;
            ddlOrientacion.Visible = verCampos;
            udpOrientacion.Update();
        }
        #endregion
    }
}
