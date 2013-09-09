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
    public partial class TemasContenido : EDUARBasePage
    {
        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the lista cursos.
        /// </summary>
        /// <value>
        /// The lista cursos.
        /// </value>
        public List<Curso> listaCursos
        {
            get
            {
                if (ViewState["listaCursos"] == null && cicloLectivoActual != null)
                {
                    BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();

                    Asignatura objFiltro = new Asignatura();
                    objFiltro.curso.cicloLectivo = cicloLectivoActual;
                    if (User.IsInRole(enumRoles.Docente.ToString()))
                        //nombre del usuario logueado
                        objFiltro.docente.username = User.Identity.Name;
                    listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
                }
                return (List<Curso>)ViewState["listaCursos"];
            }
            set { ViewState["listaCursos"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista contenido.
        /// </summary>
        /// <value>
        /// The lista contenido.
        /// </value>
        protected List<TemaContenido> listaTemaContenido
        {
            get
            {
                if (ViewState["listaTemaContenido"] == null)
                    ViewState["listaTemaContenido"] = new List<TemaContenido>();
                return (List<TemaContenido>)ViewState["listaTemaContenido"];
            }
            set { ViewState["listaTemaContenido"] = value; }
        }

        /// <summary>
        /// Gets or sets the id tema contenido.
        /// </summary>
        /// <value>
        /// The id tema contenido.
        /// </value>
        public int idTemaContenido
        {
            get
            {
                if (ViewState["idTemaContenido"] == null)
                    ViewState["idTemaContenido"] = 0;
                return (int)ViewState["idTemaContenido"];
            }
            set { ViewState["idTemaContenido"] = value; }
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
                btnNuevo.Visible = contenidoEditar.activo;
                if (!(contenidoEditar.idContenido > 0))
                    Response.Redirect("~/Private/Planning/Contenido.aspx", true);
                if (!Page.IsPostBack)
                {
                    CargarPresentacion();
                }
                else
                {
                    if (Request.Form["__EVENTTARGET"] == "btnGuardar")
                        //llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Button2
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
        protected void VentanaAceptar(object sender, EventArgs e)
        {
            try
            {
                switch (AccionPagina)
                {
                    case enumAcciones.Eliminar:
                        EliminarContenido();
                        break;
                    default:
                        break;
                }
                AccionPagina = enumAcciones.Limpiar;
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
                pnlNuevoContenido.Attributes["display"] = "inherit";
                udpBotonera.Update();
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        /// <summary>
        /// Handles the Click event of the btnVolver control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnVolverContenido_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Private/Planning/Contenido.aspx", true);
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
                if (!string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    TemaContenido nuevoContenido = new TemaContenido();
                    nuevoContenido.idTemaContenido = idTemaContenido;
                    nuevoContenido.detalle = txtDescripcion.Text;
                    nuevoContenido.idContenido = contenidoEditar.idContenido;
                    nuevoContenido.obligatorio = chkObligatorio.Checked;
                    nuevoContenido.titulo = txtTitulo.Text;

                    GuardarContenido(nuevoContenido);
                }
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
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
                        lblTitulo.Text = "Editar Tema";
                        idTemaContenido = Convert.ToInt32(e.CommandArgument.ToString());
                        var lista = listaTemaContenido.Find(p => p.idTemaContenido == idTemaContenido);
                        txtDescripcion.Text = lista.detalle;
                        lblContenido.Text = contenidoEditar.descripcion;
                        txtTitulo.Text = lista.titulo;
                        chkObligatorio.Checked = lista.obligatorio;
                        udpBotonera.Update();
                        mpeContenido.Show();
                        break;
                    case "Eliminar":
                        AccionPagina = enumAcciones.Eliminar;
                        idTemaContenido = Convert.ToInt32(e.CommandArgument.ToString());
                        Master.MostrarMensaje("Eliminar Tema", "¿Desea <b>eliminar</b> el tema seleccionado?", enumTipoVentanaInformacion.Confirmación);
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
            pnlNuevoContenido.Attributes["display"] = "none";
            lblTemas.Text = "Temas - " + contenidoEditar.descripcion;
            CargarContenido(contenidoEditar.idContenido);
            udpBotonera.Update();
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        private void CargarGrilla()
        {
            gvwContenido.DataSource = UIUtilidades.BuildDataTable<TemaContenido>(listaTemaContenido).DefaultView;
            gvwContenido.DataBind();
            udpGrilla.Update();
        }

        /// <summary>
        /// Guardars the contenido.
        /// </summary>
        /// <param name="nuevoContenido">The nuevo contenido.</param>
        private void GuardarContenido(TemaContenido nuevoContenido)
        {
            BLTemaContenido objBLTemaContenido = new BLTemaContenido(nuevoContenido);
            objBLTemaContenido.Save();

            lblTitulo.Text = "Nuevo Tema";
            txtDescripcion.Text = string.Empty;
            txtTitulo.Text = string.Empty;
            chkObligatorio.Checked = true;
            idTemaContenido = 0;
            pnlNuevoContenido.Attributes["display"] = "none";
            udpBotonera.Update();
            CargarContenido(contenidoEditar.idContenido);
        }

        /// <summary>
        /// Eliminars the contenido.
        /// </summary>
        private void EliminarContenido()
        {
            TemaContenido objEliminar = new TemaContenido();
            objEliminar.idTemaContenido = idTemaContenido;
            objEliminar.usuarioBaja.username = User.Identity.Name;
            BLTemaContenido ojbBLContenido = new BLTemaContenido(objEliminar);
            ojbBLContenido.Delete();

            CargarContenido(contenidoEditar.idContenido);
        }

        /// <summary>
        /// Cargars the contenido.
        /// </summary>
        /// <param name="idAsignaturaCicloLectivo">The id asignatura ciclo lectivo.</param>
        private void CargarContenido(int idContenido)
        {
            EDUAR_Entities.Contenido unidad = new EDUAR_Entities.Contenido();
            unidad.idContenido = idContenido;
            
            BLTemaContenido objBL = new BLTemaContenido();
            unidad = objBL.GetTemasByContenido(idContenido);
            listaTemaContenido = unidad.listaContenidos;
            CargarGrilla();
        }
        #endregion
    }
}