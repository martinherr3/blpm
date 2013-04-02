using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
    public partial class Planificacion : EDUARBasePage
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
        /// Lista de TODOS los contenidos registrados
        /// </summary>
        /// <value>
        /// The lista contenido.
        /// </value>
        protected List<TemaContenido> listaContenido
        {
            get
            {
                if (ViewState["listaContenido"] == null
                    || ((List<TemaContenido>)ViewState["listaContenido"]).Count == 0)
                {
                    ViewState["listaContenido"] = new List<TemaContenido>();
                    BLCurricula objBLCurricula = new BLCurricula();
                    Curricula objFiltro = new Curricula();
                    objFiltro.asignatura.idAsignatura = idAsignatura;
                    objFiltro.nivel.idNivel = idNivel;
                    objFiltro.orientacion.idOrientacion = idOrientacion;

                    //devuelve todos los temas activos
                    listaContenido = objBLCurricula.GetTemasContenidoByCurricula(objFiltro);
                }
                return (List<TemaContenido>)ViewState["listaContenido"];
            }
            set { ViewState["listaContenido"] = value; }
        }


        /// <summary>
        /// Lista de contenidos SELECCIONADOS
        /// </summary>
        /// <value>
        /// The lista seleccion.
        /// </value>
        protected List<int> listaSeleccion
        {
            get
            {
                if (Session["listaSeleccion"] == null)
                    Session["listaSeleccion"] = new List<int>();
                return (List<int>)Session["listaSeleccion"];
            }
            set { Session["listaSeleccion"] = value; }
        }

        /// <summary>
        /// Lista de Contenidos seleccionados en el momento que presiona GUARDAR
        /// </summary>
        /// <value>
        /// The lista seleccion guardar.
        /// </value>
        protected List<int> listaSeleccionGuardar
        {
            get
            {
                if (Session["listaSeleccionGuardar"] == null)
                    Session["listaSeleccionGuardar"] = new List<int>();
                return (List<int>)Session["listaSeleccionGuardar"];
            }
            set { Session["listaSeleccionGuardar"] = value; }
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
        /// Gets or sets the id asignatura.
        /// </summary>
        /// <value>
        /// The id asignatura.
        /// </value>
        public int idAsignatura
        {
            get
            {
                if (Session["idAsignatura"] == null)
                    idAsignatura = 0;
                return (int)Session["idAsignatura"];
            }
            set { Session["idAsignatura"] = value; }
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
        /// Gets or sets the planificacion editar.
        /// </summary>
        /// <value>
        /// The planificacion editar.
        /// </value>
        public PlanificacionAnual planificacionEditar
        {
            get
            {
                if (ViewState["planificacionEditar"] == null)
                    ViewState["planificacionEditar"] = new PlanificacionAnual();
                return (PlanificacionAnual)ViewState["planificacionEditar"];
            }
            set { ViewState["planificacionEditar"] = value; }
        }

        /// <summary>
        /// Gets or sets the id tema planificacion.
        /// </summary>
        /// <value>
        /// The id tema planificacion.
        /// </value>
        public int idTemaPlanificacion
        {
            get
            {
                if (ViewState["idTemaPlanificacion"] == null)
                    ViewState["idTemaPlanificacion"] = 0;
                return (int)ViewState["idTemaPlanificacion"];
            }
            set { ViewState["idTemaPlanificacion"] = value; }
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
                    CargarPresentacion();
                    CargarCurso();
                }
                //chkAprobada.Attributes.Add("onclick", "if(!jConfirm('¿Desea aprobar la presente planificación?','Confirmación')) {return false};");
                //chkSolicitarAprobacion.Attributes.Add("onclick", "if(!jConfirm('¿Desea solicitar la aprobación de la presente planificación?''Confirmación')) {return false};");
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
                        EliminarPlanificacion();
                        AsignaturaNivel objFiltro = new AsignaturaNivel();
                        objFiltro.asignatura.idAsignatura = idAsignatura;
                        objFiltro.nivel.idNivel = idNivel;
                        objFiltro.orientacion.idOrientacion = idOrientacion;
                        ObtenerPlanificacion(objFiltro);
                        idTemaPlanificacion = 0;
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
                        break;
                    case enumAcciones.Enviar:
                        break;
                    case enumAcciones.AprobarPlanificacion:
                        AprobarPlanificacion();
                        udpAprobacion.Update();
                        break;
                    case enumAcciones.SolicitarAprobacion:
                        SolicitarAprobacion();
                        udpAprobacion.Update();
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
        /// Ventanas the cancelar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void VentanaCancelar(object sender, EventArgs e)
        {
            try
            {
                switch (AccionPagina)
                {
                    case enumAcciones.AprobarPlanificacion:
                        chkAprobada.Checked = false;
                        break;
                    case enumAcciones.SolicitarAprobacion:
                        chkSolicitarAprobacion.Checked = false;
                        break;
                    default:
                        break;
                }
                AccionPagina = enumAcciones.Limpiar;
                udpAprobacion.Update();
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
                LimpiarCampos();
                DesHabilitarCampos(true);
                btnContenidosPopUp.Visible = true;
                divAprobacion.Visible = false;
                btnGuardar.Visible = true;
                ddlAsignatura.Enabled = false;
                ddlNivel.Enabled = false;
                ddlOrientacion.Enabled = false;
                btnPDF.Visible = false;
                btnVolver.Visible = true;
                btnVolverAnterior.Visible = false;
                btnNuevo.Visible = false;
                gvwPlanificacion.Visible = false;
                divControles.Visible = true;
                udpGrilla.Update();
                udpDivControles.Update();
                udpBotonera.Update();
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        /// <summary>
        /// Handles the Click event of the btnPDF control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void btnPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ExportPDF.ExportarPDFPlanificacion("Planificacion Anual", planificacionEditar);
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
                string mensaje = ValidarPagina();
                if (string.IsNullOrEmpty(mensaje) || mensaje.Equals(UIConstantesGenerales.MensajeDatosFaltantes))
                {
                    GuardarPlanificacion();
                    btnPDF.Visible = planificacionEditar.listaTemasPlanificacion.Count > 0;
                    btnNuevo.Visible = true;
                    btnContenidosPopUp.Visible = false;
                    ddlAsignatura.Enabled = true;
                    ddlNivel.Enabled = true;
                    LimpiarCampos();
                    btnPDF.Visible = true;
                    divAprobacion.Visible = false;
                    chkAprobada.Enabled = false;
                    chkSolicitarAprobacion.Enabled = false;
                    chkAprobada.Checked = false;
                    chkSolicitarAprobacion.Checked = false;

                    CargarPlanificacion();
                    divFiltros.Visible = true;
                    divControles.Visible = false;
                    btnVolver.Visible = false;
                    btnVolverAnterior.Visible = false;
                    btnGuardar.Visible = false;
                    udpBotonera.Update();
                    udpDivControles.Update();
                    udpGrilla.Update();
                }
                else
                {
                    //AccionPagina = enumAcciones.Error;
                    Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), mensaje, enumTipoVentanaInformacion.Advertencia);
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
                idTemaPlanificacion = 0;
                btnNuevo.Visible = true;
                btnContenidosPopUp.Visible = false;
                btnPDF.Visible = true;
                CargarPresentacion();
                //ObtenerPlanificacion(idAsignaturaCurso);
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
        protected void btnVolverAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                base.idCursoCicloLectivo = 0;
                base.cursoActual = new CursoCicloLectivo();
                Response.Redirect("~/Private/AccesoCursos.aspx", false);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlNivel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idNivel = 0;
                int.TryParse(ddlNivel.SelectedValue, out idNivel);
                //gvwContenido.DataSource = null;
                //gvwContenido.DataBind();
                if (idNivel > 0)
                {
                    this.idNivel = idNivel;
                    CargarComboAsignatura(idNivel);
                }
                else
                {
                    CargarPresentacion();
                    btnNuevo.Visible = false;
                    btnVolverAnterior.Visible = false;
                    btnVolver.Visible = false;
                }
                ddlOrientacion.Items.Clear();
                ddlAsignatura.Enabled = idNivel > 0;
                udpAsignatura.Update();
                VerOrientacion(false);
                udpGrilla.Update();
                udpBotonera.Update();
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
                    CargarOrientacion();
                    //idTemaPlanificacion = 0;
                    //ObtenerPlanificacion(idAsignatura);
                    //if (planificacionEditar.fechaAprobada.HasValue) btnNuevo.Visible = false;
                    //else btnNuevo.Visible = true;
                    //btnPDF.Visible = planificacionEditar.listaTemasPlanificacion.Count > 0;
                    //divControles.Visible = false;
                    //udpDivControles.Update();
                }
                else
                {
                    LimpiarCampos();
                    btnPDF.Visible = false;
                    divAprobacion.Visible = false;
                    chkAprobada.Enabled = false;
                    chkSolicitarAprobacion.Enabled = false;
                    chkAprobada.Checked = false;
                    chkSolicitarAprobacion.Checked = false;
                    gvwPlanificacion.DataSource = null;
                    gvwPlanificacion.DataBind();
                    udpGrilla.Update();
                }
                udpBotonera.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
                CargarPresentacion();
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
                    CargarPlanificacion();
                }
                udpBotonera.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gvwPlanificacion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gvwPlanificacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        listaSeleccionGuardar.Clear();
                        listaSeleccion.Clear();
                        idTemaPlanificacion = Convert.ToInt32(e.CommandArgument.ToString());
                        DesHabilitarCampos(true);
                        CargarPlanificacion();
                        btnContenidosPopUp.Visible = true;
                        divAprobacion.Visible = false;
                        divControles.Visible = true;
                        //ddlCurso.Enabled = false;
                        ddlAsignatura.Enabled = false;
                        gvwPlanificacion.Visible = false;
                        btnGuardar.Visible = true;
                        btnNuevo.Visible = false;
                        btnVolver.Visible = true;
                        btnVolverAnterior.Visible = false;
                        btnPDF.Visible = false;
                        udpBotonera.Update();
                        udpGrilla.Update();
                        udpDivControles.Update();
                        break;
                    case "Eliminar":
                        AccionPagina = enumAcciones.Eliminar;
                        idTemaPlanificacion = Convert.ToInt32(e.CommandArgument.ToString());
                        Master.MostrarMensaje("Eliminar Planificación", "¿Desea <b>eliminar</b> la planificación seleccionada?", enumTipoVentanaInformacion.Confirmación);
                        //EliminarPlanificacion();
                        //ObtenerPlanificacion(idAsignaturaCurso);
                        //idTemaPlanificacion = 0;
                        break;
                    case "Consultar":
                        idTemaPlanificacion = Convert.ToInt32(e.CommandArgument.ToString());
                        CargarPlanificacion();
                        DesHabilitarCampos(false);
                        divAprobacion.Visible = false;
                        divControles.Visible = true;
                        //ddlCurso.Enabled = false;
                        ddlAsignatura.Enabled = false;
                        gvwPlanificacion.Visible = false;
                        btnGuardar.Visible = false;
                        btnNuevo.Visible = false;
                        btnVolver.Visible = true;
                        btnVolverAnterior.Visible = false;
                        btnPDF.Visible = false;
                        udpBotonera.Update();
                        udpGrilla.Update();
                        udpDivControles.Update();
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkAprobada control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void chkAprobada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = enumAcciones.AprobarPlanificacion;
                Master.MostrarMensaje("Aprobar Planificación", "¿Desea aprobar la presente planificación?", enumTipoVentanaInformacion.Confirmación);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkSolicitarAprobacion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void chkSolicitarAprobacion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSolicitarAprobacion.Checked)
                {
                    AccionPagina = enumAcciones.SolicitarAprobacion;
                    Master.MostrarMensaje("Solicitar Aprobación", "¿Desea solicitar la aprobación de la presente planificación?", enumTipoVentanaInformacion.Confirmación);
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnContenidosPopUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnContenidosPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                CargarContenidos();
                listaSeleccion = listaSeleccionGuardar;
                ProductsSelectionManager.RestoreSelection(gvwContenidos, "listaSeleccion");
                btnGuardarPopUp.Visible = !planificacionEditar.fechaAprobada.HasValue;
                mpeContenido.Show();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnVolverPopUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnVolverPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                mpeContenido.Hide();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnGuardarPopUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnGuardarPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                ProductsSelectionManager.KeepSelection(gvwContenidos, "listaSeleccion");
                listaSeleccionGuardar = listaSeleccion;
                mpeContenido.Hide();
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
        protected void gvwContenidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ProductsSelectionManager.KeepSelection(gvwContenidos, "listaSeleccion");

                gvwContenidos.PageIndex = e.NewPageIndex;
                CargarContenidos();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the PageIndexChanged event of the gvwContenidos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void gvwContenidos_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ProductsSelectionManager.RestoreSelection(gvwContenidos, "listaSeleccion");
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
            ddlAsignatura.Items.Clear();
            ddlAsignatura.Items.Add("[Seleccione Nivel]");
            ddlAsignatura.Enabled = false;
            pnlContenidos.Attributes["display"] = "none";

            LimpiarCampos();
            chkAprobada.Enabled = false;
            chkSolicitarAprobacion.Enabled = false;
            chkAprobada.Checked = false;
            chkSolicitarAprobacion.Checked = false;
            listaContenido = null;

            gvwPlanificacion.DataSource = null;
            gvwPlanificacion.DataBind();

            VerOrientacion(false);

            divFiltros.Visible = true;
            divControles.Visible = false;
            divFiltros.Visible = true;
            divAprobacion.Visible = false;

            btnVolver.Visible = false;
            btnVolverAnterior.Visible = false;
            btnGuardar.Visible = false;
            btnPDF.Visible = false;
            btnNuevo.Visible = false;

            udpBotonera.Update();
            udpDivControles.Update();
            udpGrilla.Update();
        }

        /// <summary>
        /// Cargars the asignaturas.
        /// </summary>
        private void CargarComboAsignatura(int idCursoCicloLectivo)
        {
            //Docente docente = null;
            //if (User.IsInRole(enumRoles.Docente.ToString()))
            //{
            //    docente = new Docente();
            //    docente.username = User.Identity.Name;
            //}
            //curso.idCursoCicloLectivo = idCursoCicloLectivo;
            //listaAsignaturas = objBLAsignatura.GetAsignaturasCurso(new Asignatura() { cursoCicloLectivo = curso, docente = docente });
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
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            listaSeleccion.Clear();
            listaSeleccionGuardar.Clear();
            txtCActitudinales.Text = string.Empty;
            txtCConceptuales.Text = string.Empty;
            txtCProcedimentales.Text = string.Empty;
            txtCriteriosEvaluacion.Text = string.Empty;
            txtEstrategias.Text = string.Empty;
            txtInstrumentosEvaluacion.Text = string.Empty;
            calFechaDesde.LimpiarControles();
            calFechaFin.LimpiarControles();
            udpDivControles.Update();
        }

        /// <summary>
        /// Checks the null.
        /// </summary>
        /// <param name="objGrid">The obj grid.</param>
        /// <returns></returns>
        protected string CheckNull(object objGrid)
        {
            if (object.ReferenceEquals(objGrid, null))
                return " - ";
            else
                return Convert.ToDateTime(objGrid).ToShortDateString();
        }

        /// <summary>
        /// Checks the aprobada.
        /// </summary>
        /// <param name="objGrid">The obj grid.</param>
        /// <param name="editar">if set to <c>true</c> [editar].</param>
        /// <returns></returns>
        protected bool CheckAprobada(object objGrid, bool editar)
        {
            bool aux;
            if (object.ReferenceEquals(objGrid, null))
                aux = false;
            else
                aux = true;
            if (aux && editar)
                return false;
            else
            {
                if (aux && !editar)
                    return true;
                else if (!aux && editar)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Validars the pagina.
        /// </summary>
        /// <returns></returns>
        private string ValidarPagina()
        {
            string mensaje = UIConstantesGenerales.MensajeDatosFaltantes;
            bool hayContenido = false;
            if (txtCConceptuales.Text.Trim().Length == 0)
                mensaje += "- Contenidos Conceptuales<br />";
            else
                hayContenido = true;
            if (txtCProcedimentales.Text.Trim().Length == 0)
                mensaje += "- Contenidos Procedimentales<br />";
            else
                hayContenido = true;
            if (txtCActitudinales.Text.Trim().Length == 0)
                mensaje += "- Contenidos Actitudinales<br />";
            else
                hayContenido = true;
            if (txtEstrategias.Text.Trim().Length == 0)
                mensaje += "- Estrategias<br />";
            else
                hayContenido = true;
            if (txtCriteriosEvaluacion.Text.Trim().Length == 0)
                mensaje += "- Criterios de Evaluación<br />";
            else
                hayContenido = true;
            if (txtInstrumentosEvaluacion.Text.Trim().Length == 0)
                mensaje += "- Instrumentos de Evaluación<br />";
            else
                hayContenido = true;

            calFechaDesde.EtiquetaDesde = "Fecha Inicio:";
            calFechaDesde.ValidarRangoDesde(false);
            calFechaDesde.EtiquetaDesde = string.Empty;

            calFechaFin.EtiquetaDesde = "Fecha Finalización:";
            calFechaFin.ValidarRangoDesde(false);
            calFechaDesde.EtiquetaDesde = string.Empty;
            if ((Convert.ToDateTime(calFechaFin.ValorFecha).Subtract(Convert.ToDateTime(calFechaDesde.ValorFecha))).TotalDays < 0)
            {
                hayContenido = false;
                mensaje = "La Fecha de Inicio no puede ser superior a la Fecha de Finalización";
            }
            foreach (TemaPlanificacionAnual unTema in planificacionEditar.listaTemasPlanificacion)
            {
                if (hayContenido)
                {
                    if (((calFechaDesde.ValorFecha >= unTema.fechaInicioEstimada && calFechaDesde.ValorFecha <= unTema.fechaFinEstimada) ||
                         (calFechaFin.ValorFecha >= unTema.fechaInicioEstimada && calFechaFin.ValorFecha <= unTema.fechaFinEstimada))
                         && unTema.idTemaPlanificacion != idTemaPlanificacion)
                    {
                        hayContenido = false;
                        mensaje = "Existe otro tema planificado para el periodo " + unTema.fechaInicioEstimada.Value.ToShortDateString() + " - " + unTema.fechaFinEstimada.Value.ToShortDateString();
                    }
                }
                else
                    break;
            }

            if (hayContenido) return string.Empty;
            return mensaje;
        }

        /// <summary>
        /// Obteners the planificacion.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        private void ObtenerPlanificacion(AsignaturaNivel objFiltro)
        {
            BLPlanificacionAnual objBLPlanificacion = new BLPlanificacionAnual();
            planificacionEditar = objBLPlanificacion.GetPlanificacionByAsignatura(objFiltro);
            gvwPlanificacion.DataSource = planificacionEditar.listaTemasPlanificacion;
            gvwPlanificacion.DataBind();
            gvwPlanificacion.Visible = true;
            ValidarAprobaciones();
            btnPDF.Visible = planificacionEditar.listaTemasPlanificacion.Count > 0;
            udpGrilla.Update();
        }

        /// <summary>
        /// Validars the aprobaciones.
        /// </summary>
        private void ValidarAprobaciones()
        {
            chkAprobada.Checked = false;
            chkSolicitarAprobacion.Checked = false;
            chkAprobada.Enabled = false;
            chkSolicitarAprobacion.Enabled = false;
            lblFecha.Text = string.Empty;
            if (planificacionEditar.listaTemasPlanificacion.Count > 0)
            {
                divAprobacion.Visible = true;
                if (planificacionEditar.fechaAprobada.HasValue)
                {
                    chkAprobada.Enabled = false;
                    chkSolicitarAprobacion.Enabled = false;
                    chkAprobada.Checked = true;
                    chkSolicitarAprobacion.Checked = true;
                    lblFecha.Text = "Fecha de Aprobación: " + Convert.ToDateTime(planificacionEditar.fechaAprobada).ToShortDateString();
                }
                else
                {
                    if ((User.IsInRole(enumRoles.Director.ToString()) || User.IsInRole(enumRoles.Administrador.ToString()))
                        && planificacionEditar.solicitarAprobacion)
                    {
                        chkAprobada.Enabled = true;
                        chkSolicitarAprobacion.Enabled = false;
                        chkSolicitarAprobacion.Checked = planificacionEditar.solicitarAprobacion;
                    }
                    else
                    {
                        if ((User.IsInRole(enumRoles.Docente.ToString()) || User.IsInRole(enumRoles.Administrador.ToString()))
                            && !planificacionEditar.solicitarAprobacion
                            && !planificacionEditar.fechaAprobada.HasValue
                            )
                            chkSolicitarAprobacion.Enabled = true;
                        else
                            chkSolicitarAprobacion.Checked = true;
                    }
                }
            }
            else
                divAprobacion.Visible = false;
            udpAprobacion.Update();
        }

        /// <summary>
        /// DESs the habilitar campos.
        /// </summary>
        /// <param name="habilitar">if set to <c>true</c> [habilitar].</param>
        private void DesHabilitarCampos(bool habilitar)
        {
            txtCActitudinales.Enabled = habilitar;
            txtCConceptuales.Enabled = habilitar;
            txtCProcedimentales.Enabled = habilitar;
            txtCriteriosEvaluacion.Enabled = habilitar;
            txtEstrategias.Enabled = habilitar;
            txtInstrumentosEvaluacion.Enabled = habilitar;
            calFechaDesde.Habilitado = habilitar;
            calFechaFin.Habilitado = habilitar;
            chkSolicitarAprobacion.Enabled = habilitar;

            calFechaDesde.startDate = cicloLectivoActual.fechaInicio;
            calFechaDesde.endDate = cicloLectivoActual.fechaFin;
            calFechaFin.startDate = cicloLectivoActual.fechaInicio;
            calFechaFin.endDate = cicloLectivoActual.fechaFin;
        }

        /// <summary>
        /// Cargars the planificacion.
        /// </summary>
        private void CargarPlanificacion()
        {
            AsignaturaNivel objFiltro = new AsignaturaNivel();
            objFiltro.asignatura.idAsignatura = idAsignatura;
            objFiltro.nivel.idNivel = idNivel;
            objFiltro.orientacion.idOrientacion = idOrientacion;
            ObtenerPlanificacion(objFiltro);
            if (planificacionEditar.fechaAprobada.HasValue) btnNuevo.Visible = false;
            else btnNuevo.Visible = true;
            btnPDF.Visible = planificacionEditar.listaTemasPlanificacion.Count > 0;
            divControles.Visible = false;
            udpDivControles.Update();

            var temaPlanificacionEditar = planificacionEditar.listaTemasPlanificacion.Find(p => p.idTemaPlanificacion == idTemaPlanificacion);
            if (temaPlanificacionEditar != null)
            {
                txtCActitudinales.Text = temaPlanificacionEditar.contenidosActitudinales;
                txtCConceptuales.Text = temaPlanificacionEditar.contenidosConceptuales;
                txtCProcedimentales.Text = temaPlanificacionEditar.contenidosProcedimentales;
                txtCriteriosEvaluacion.Text = temaPlanificacionEditar.criteriosEvaluacion;
                txtEstrategias.Text = temaPlanificacionEditar.estrategiasAprendizaje;
                txtInstrumentosEvaluacion.Text = temaPlanificacionEditar.instrumentosEvaluacion;
                calFechaDesde.Fecha.Text = temaPlanificacionEditar.fechaInicioEstimada.ToString();
                calFechaFin.Fecha.Text = temaPlanificacionEditar.fechaFinEstimada.ToString();
                chkAprobada.Enabled = !temaPlanificacionEditar.fechaAprobada.HasValue;
                BLTemaPlanificacionAnual objBLTemaPlanificacion = new BLTemaPlanificacionAnual(temaPlanificacionEditar);
                List<TemaContenido> listaTemporal = objBLTemaPlanificacion.ObtenerContenidos();
                listaSeleccionGuardar.Clear();
                foreach (TemaContenido item in listaTemporal)
                    listaSeleccionGuardar.Add(item.idTemaContenido);
                //listaSeleccion = objBLTemaPlanificacion.ObtenerContenidos();
            }
        }

        /// <summary>
        /// Eliminars the planificacion.
        /// </summary>
        private void EliminarPlanificacion()
        {
            TemaPlanificacionAnual objEliminar = new TemaPlanificacionAnual();
            objEliminar.idTemaPlanificacion = idTemaPlanificacion;
            BLTemaPlanificacionAnual ojbBLTemaPlanificacion = new BLTemaPlanificacionAnual(objEliminar);
            ojbBLTemaPlanificacion.Delete();
        }


        /// <summary>
        /// Lista de TODOS los contenidos registrados
        /// </summary>
        /// <value>
        /// The lista contenido.
        /// </value>
        protected List<TemaContenido> getContenidosPlanificados()
        {
            List<TemaContenido> listaContenidosPlanificados = new List<TemaContenido>();
            TemaPlanificacionAnual objFiltro = new TemaPlanificacionAnual();
            objFiltro.idTemaPlanificacion = idTemaPlanificacion;
            BLTemaPlanificacionAnual objBLTemas = new BLTemaPlanificacionAnual(objFiltro);
            listaContenidosPlanificados = objBLTemas.ObtenerContenidos();

            //List<TemaContenido> listaAuxiliar = new List<TemaContenido>();
            //listaAuxiliar = objBLTemas.ObtenerContenidosDesactivados();

            //foreach (TemaContenido item in listaAuxiliar)
            //    listaContenido.Add(item);

            return (listaContenidosPlanificados);
        }


        /// <summary>
        /// Cargars the contenidos.
        /// </summary>
        private void CargarContenidos()
        {
            //listaContenido // tiene todos los contenidos
            //listaSeleccionGuardar // tiene los contenidos asociados al item planificacion en curso
            //listaPlanificacionContenido // tiene todos los contenidos qu estan asociados a una planificacion
            List<TemaContenido> listaContenidosPlanificados = getContenidosPlanificados();

            bool sacarContenido = false;

            List<bool> seleccionContenidos = new List<bool>();

            foreach (TemaContenido contenido in listaContenido)
            {
                foreach (TemaContenido contenidoPlanificado in listaContenidosPlanificados)
                    if (contenido.idTemaContenido == contenidoPlanificado.idTemaContenido)
                        sacarContenido = true;

                foreach (int contenidoActualPlanificacion in listaSeleccionGuardar)
                    if (contenido.idTemaContenido == contenidoActualPlanificacion)
                        sacarContenido = false;

                seleccionContenidos.Add(sacarContenido);
                sacarContenido = false;
            }

            for (int i = seleccionContenidos.Count - 1; i > -1; i--)
                if (seleccionContenidos[i])
                    listaContenido.RemoveAt(i);
            listaContenidosPlanificados.Clear();

            gvwContenidos.DataSource = listaContenido;
            gvwContenidos.DataBind();
        }

        /// <summary>
        /// Guardars the planificacion.
        /// </summary>
        private void GuardarPlanificacion()
        {

            TemaPlanificacionAnual objTema = new TemaPlanificacionAnual();
            objTema.contenidosActitudinales = txtCActitudinales.Text.Trim();
            objTema.contenidosConceptuales = txtCConceptuales.Text.Trim();
            objTema.contenidosProcedimentales = txtCProcedimentales.Text.Trim();
            objTema.criteriosEvaluacion = txtCriteriosEvaluacion.Text.Trim();
            objTema.estrategiasAprendizaje = txtEstrategias.Text.Trim();
            objTema.fechaFinEstimada = calFechaFin.ValorFecha;
            objTema.fechaInicioEstimada = calFechaDesde.ValorFecha;
            objTema.instrumentosEvaluacion = txtInstrumentosEvaluacion.Text.Trim();
            //objTema.listaContenidos = listaSeleccionGuardar;
            List<TemaContenido> listaTemporal = new List<TemaContenido>();
            foreach (int item in listaSeleccionGuardar)
                listaTemporal.Add(new TemaContenido() { idTemaContenido = item });
            objTema.listaContenidos = listaTemporal;

            if (idTemaPlanificacion > 0)
                objTema.idTemaPlanificacion = idTemaPlanificacion;

            PlanificacionAnual objPlanificacion = new PlanificacionAnual();

            objPlanificacion.curricula.asignatura.idAsignatura = idAsignatura;
            objPlanificacion.curricula.nivel.idNivel = idNivel;
            objPlanificacion.curricula.orientacion.idOrientacion = idOrientacion;
            objPlanificacion.curricula.personaAlta.username = User.Identity.Name;
            objPlanificacion.curricula.personaModificacion.username = User.Identity.Name;

            objPlanificacion.creador.username = (string.IsNullOrEmpty(planificacionEditar.creador.username)) ? User.Identity.Name : planificacionEditar.creador.username;
            //objPlanificacion.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignaturaCurso;
            objPlanificacion.idPlanificacionAnual = planificacionEditar.idPlanificacionAnual;
            objPlanificacion.solicitarAprobacion = planificacionEditar.solicitarAprobacion;
            objPlanificacion.fechaAprobada = planificacionEditar.fechaAprobada;
            objPlanificacion.listaTemasPlanificacion.Add(objTema);
            BLPlanificacionAnual objPlanificacionBL = new BLPlanificacionAnual(objPlanificacion);
            objPlanificacionBL.GrabarPlanificacion();
            idTemaPlanificacion = 0;
            //ObtenerPlanificacion(objPlanificacion.asignaturaCicloLectivo.idAsignaturaCicloLectivo);
            ObtenerPlanificacion(new AsignaturaNivel());
        }

        /// <summary>
        /// Aprobars the planificacion.
        /// </summary>
        private void AprobarPlanificacion()
        {
            PlanificacionAnual objAprobar = new PlanificacionAnual();
            objAprobar.creador.username = (string.IsNullOrEmpty(planificacionEditar.creador.username)) ? User.Identity.Name : planificacionEditar.creador.username;
            //objAprobar.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignaturaCurso;
            objAprobar.idPlanificacionAnual = planificacionEditar.idPlanificacionAnual;
            objAprobar.solicitarAprobacion = planificacionEditar.solicitarAprobacion;
            objAprobar.fechaAprobada = DateTime.Today;
            planificacionEditar.fechaAprobada = DateTime.Today;
            BLPlanificacionAnual objBLAprobar = new BLPlanificacionAnual(objAprobar);
            objBLAprobar.Save();

            //ObtenerPlanificacion(idAsignaturaCurso);
            btnNuevo.Visible = false;
        }

        /// <summary>
        /// Solicitars the aprobacion.
        /// </summary>
        private void SolicitarAprobacion()
        {
            PlanificacionAnual objAprobar = new PlanificacionAnual();
            objAprobar.creador.username = (string.IsNullOrEmpty(planificacionEditar.creador.username)) ? User.Identity.Name : planificacionEditar.creador.username;
            //objAprobar.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignaturaCurso;
            objAprobar.idPlanificacionAnual = planificacionEditar.idPlanificacionAnual;
            objAprobar.fechaAprobada = planificacionEditar.fechaAprobada;
            objAprobar.solicitarAprobacion = true;
            planificacionEditar.solicitarAprobacion = true;
            BLPlanificacionAnual objBLAprobar = new BLPlanificacionAnual(objAprobar);
            objBLAprobar.Save();

            //ObtenerPlanificacion(idAsignaturaCurso);
        }

        /// <summary>
        /// Cargars the curso.
        /// </summary>
        private void CargarCurso()
        {
            if (base.idCursoCicloLectivo > 0)
            {
                CargarComboAsignatura(base.idCursoCicloLectivo);
                lblTituloPrincipal.Text = "Planificación de Contenidos - " + base.cursoActual.curso.nombre;
            }

            divAprobacion.Visible = false;
            gvwPlanificacion.DataSource = null;
            gvwPlanificacion.DataBind();

            ddlAsignatura.Enabled = idCursoCicloLectivo > 0;
            btnGuardar.Visible = false;
            divControles.Visible = false;
            udpAsignatura.Update();
            udpBotonera.Update();
            udpDivControles.Update();
            udpGrilla.Update();
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
                    CargarPlanificacion();
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