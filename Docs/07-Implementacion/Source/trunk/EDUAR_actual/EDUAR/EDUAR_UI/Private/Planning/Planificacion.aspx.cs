using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;

namespace EDUAR_UI
{
    public partial class Planificacion : EDUARBasePage
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
        /// Gets or sets the id curso.
        /// </summary>
        /// <value>
        /// The id curso.
        /// </value>
        public int idCurso
        {
            get
            {
                if (ViewState["idCurso"] == null)
                    ViewState["idCurso"] = 0;
                return (int)ViewState["idCurso"];
            }
            set { ViewState["idCurso"] = value; }
        }

        /// <summary>
        /// Gets or sets the id asignatura curso.
        /// </summary>
        /// <value>
        /// The id asignatura curso.
        /// </value>
        public int idAsignaturaCurso
        {
            get
            {
                if (ViewState["idAsignaturaCurso"] == null)
                    ViewState["idAsignaturaCurso"] = 0;
                return (int)ViewState["idAsignaturaCurso"];
            }
            set { ViewState["idAsignaturaCurso"] = value; }
        }

        /// <summary>
        /// Gets or sets the id planificacion anual.
        /// </summary>
        /// <value>
        /// The id planificacion anual.
        /// </value>
        public int idPlanificacionAnual
        {
            get
            {
                if (ViewState["idPlanificacionAnual"] == null)
                    ViewState["idPlanificacionAnual"] = 0;
                return (int)ViewState["idPlanificacionAnual"];
            }
            set { ViewState["idPlanificacionAnual"] = value; }
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
                //Master.BotonAvisoAceptar += (VentanaAceptar);

                if (!Page.IsPostBack)
                {
                    CargarPresentacion();
                }
                chkAprobada.Attributes.Add("onclick", "if(!confirm('¿Desea aprobar la presente planificación?')) {return false};");
                chkSolicitarAprobacion.Attributes.Add("onclick", "if(!confirm('¿Desea solicitar la aprobación de la presente planificación?')) {return false};");
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
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
                LimpiarCampos();
                DesHabilitarCampos(true);
				divAprobacion.Visible = false;
                btnGuardar.Visible = true;
                ddlAsignatura.Enabled = false;
                ddlCurso.Enabled = false;
                btnVolver.Visible = true;
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
        /// Handles the Click event of the btnGuardar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = ValidarPagina();
                if (string.IsNullOrEmpty(mensaje))
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

                    if (idTemaPlanificacion > 0)
                        objTema.idTemaPlanificacion = idTemaPlanificacion;

                    PlanificacionAnual objPlanificacion = new PlanificacionAnual();
                    objPlanificacion.creador.username = (string.IsNullOrEmpty(planificacionEditar.creador.username)) ? User.Identity.Name : planificacionEditar.creador.username;
                    objPlanificacion.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignaturaCurso;
                    objPlanificacion.idPlanificacionAnual = planificacionEditar.idPlanificacionAnual;
                    objPlanificacion.solicitarAprobacion = planificacionEditar.solicitarAprobacion;
                    objPlanificacion.fechaAprobada = planificacionEditar.fechaAprobada;
                    objPlanificacion.listaTemasPlanificacion.Add(objTema);
                    BLPlanificacionAnual objPlanificacionBL = new BLPlanificacionAnual(objPlanificacion);
                    objPlanificacionBL.Save();
                    idTemaPlanificacion = 0;
                    ObtenerPlanificacion(objPlanificacion.asignaturaCicloLectivo.idAsignaturaCicloLectivo);
                    udpBotonera.Update();
                    CargarPresentacion();
                }
                else
                {
                    //AccionPagina = enumAcciones.Error;
                    Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
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
                CargarPresentacion();
                ObtenerPlanificacion(idAsignaturaCurso);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCurso control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCursoCicloLectivo = 0;
                int.TryParse(ddlCurso.SelectedValue, out idCursoCicloLectivo);
                if (idCursoCicloLectivo > 0)
                {
                    idCurso = idCursoCicloLectivo;
                    CargarComboAsignatura(idCursoCicloLectivo);
                }
                else
                {
					ddlAsignatura.Enabled = false;
					ddlAsignatura.Items.Clear();
					ddlAsignatura.Items.Add("[Seleccione Curso]");
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
                btnNuevo.Visible = idAsignatura > 0;
                if (idAsignatura > 0)
                {
                    idTemaPlanificacion = 0;
                    idAsignaturaCurso = idAsignatura;
                    ObtenerPlanificacion(idAsignatura);
                    btnNuevo.Visible = true;
                    divControles.Visible = false;
                    udpDivControles.Update();
                }
                else
                {
                    LimpiarCampos();
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
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the gvwPlanificacion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvwPlanificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

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
                        idTemaPlanificacion = Convert.ToInt32(e.CommandArgument.ToString());
                        DesHabilitarCampos(true);
                        CargarPlanificacion();
                        divAprobacion.Visible = false;
                        divControles.Visible = true;
                        ddlCurso.Enabled = false;
                        ddlAsignatura.Enabled = false;
                        gvwPlanificacion.Visible = false;
                        btnGuardar.Visible = true;
                        btnNuevo.Visible = false;
                        btnVolver.Visible = true;
                        udpBotonera.Update();
                        udpGrilla.Update();
                        udpDivControles.Update();
                        break;
                    case "Eliminar":
                        //AccionPagina = enumAcciones.Eliminar;
                        idTemaPlanificacion = Convert.ToInt32(e.CommandArgument.ToString());
                        EliminarPlanificacion();
                        ObtenerPlanificacion(idAsignaturaCurso);
						idTemaPlanificacion = 0;
                        break;
                    case "Consultar":
                        idTemaPlanificacion = Convert.ToInt32(e.CommandArgument.ToString());
                        CargarPlanificacion();
                        DesHabilitarCampos(false);
						divAprobacion.Visible = false;
                        divControles.Visible = true;
                        ddlCurso.Enabled = false;
                        ddlAsignatura.Enabled = false;
                        gvwPlanificacion.Visible = false;
                        btnGuardar.Visible = false;
                        btnNuevo.Visible = false;
                        btnVolver.Visible = true;
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
                PlanificacionAnual objAprobar = new PlanificacionAnual();
                objAprobar.creador.username = (string.IsNullOrEmpty(planificacionEditar.creador.username)) ? User.Identity.Name : planificacionEditar.creador.username;
                objAprobar.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignaturaCurso;
                objAprobar.idPlanificacionAnual = planificacionEditar.idPlanificacionAnual;
                objAprobar.solicitarAprobacion = planificacionEditar.solicitarAprobacion;
                objAprobar.fechaAprobada = DateTime.Today;
                planificacionEditar.fechaAprobada = DateTime.Today;
                BLPlanificacionAnual objBLAprobar = new BLPlanificacionAnual(objAprobar);
                objBLAprobar.Save();

                ObtenerPlanificacion(idAsignaturaCurso);
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
                PlanificacionAnual objAprobar = new PlanificacionAnual();
                objAprobar.creador.username = (string.IsNullOrEmpty(planificacionEditar.creador.username)) ? User.Identity.Name : planificacionEditar.creador.username;
                objAprobar.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignaturaCurso;
                objAprobar.idPlanificacionAnual = planificacionEditar.idPlanificacionAnual;
                objAprobar.fechaAprobada = planificacionEditar.fechaAprobada;
                objAprobar.solicitarAprobacion = true;
                planificacionEditar.solicitarAprobacion = true;
                BLPlanificacionAnual objBLAprobar = new BLPlanificacionAnual(objAprobar);
                objBLAprobar.Save();

                ObtenerPlanificacion(idAsignaturaCurso);

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
            UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "Nombre", true);
			if (idCurso > 0)
			{
				ddlCurso.SelectedValue = idCurso.ToString();
				CargarComboAsignatura(idCurso);
				if (idAsignaturaCurso > 0)
					ddlAsignatura.SelectedValue = idAsignaturaCurso.ToString();
				ddlAsignatura.Enabled = true;
			}
			else
			{
				ddlAsignatura.Enabled = false;
				ddlAsignatura.Items.Clear();
				ddlAsignatura.Items.Add("[Seleccione Curso]");
			}
            divFiltros.Visible = true;
            divControles.Visible = false;
            btnNuevo.Visible = true;
            btnVolver.Visible = false;
            btnGuardar.Visible = false;
            divFiltros.Visible = true;
            ddlCurso.Enabled = true;
            udpBotonera.Update();
            udpDivControles.Update();
            udpGrilla.Update();
        }

        /// <summary>
        /// Cargars the asignaturas.
        /// </summary>
        private void CargarComboAsignatura(int idCursoCicloLectivo)
        {
            List<Asignatura> listaAsignaturas = new List<Asignatura>();
            BLAsignatura objBLAsignatura = new BLAsignatura();
            CursoCicloLectivo curso = new CursoCicloLectivo();
            Docente docente = null;
            if (User.IsInRole(enumRoles.Docente.ToString()))
                docente.username = User.Identity.Name;
            curso.idCursoCicloLectivo = idCursoCicloLectivo;
            listaAsignaturas = objBLAsignatura.GetAsignaturasCurso(new Asignatura() { cursoCicloLectivo = curso, docente = docente });
            if (listaAsignaturas != null && listaAsignaturas.Count > 0)
                UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, listaAsignaturas, "idAsignatura", "Nombre", true);
        }

        /// <summary>
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
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
            //			if (object.ReferenceEquals(objGrid, DBNull.Value))
            if (object.ReferenceEquals(objGrid, null))
            {
                return " - ";
            }
            else
            {
                return Convert.ToDateTime(objGrid).ToShortDateString();
                //return objGrid.ToString();
            }
        }

        /// <summary>
        /// Checks the aprobada.
        /// </summary>
        /// <param name="objGrid">The obj grid.</param>
        /// <param name="editar">if set to <c>true</c> [editar].</param>
        /// <returns></returns>
        protected bool CheckAprobada(object objGrid, bool editar)
        {
            //			if (object.ReferenceEquals(objGrid, DBNull.Value))
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
            string mensaje = string.Empty;
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
                mensaje += "- Instrumntos de Evaluación<br />";
            else
                hayContenido = true;

            calFechaDesde.ValidarRangoDesde();
            calFechaFin.ValidarRangoDesde();

            if (hayContenido) return string.Empty;
            return mensaje;
        }

        /// <summary>
        /// Obteners the planificacion.
        /// </summary>
        /// <param name="idAsignatura">The id asignatura.</param>
        private void ObtenerPlanificacion(int idAsignatura)
        {
            BLPlanificacionAnual objBLPlanificacion = new BLPlanificacionAnual();
            planificacionEditar = objBLPlanificacion.GetPlanificacionByAsignatura(idAsignatura);
            gvwPlanificacion.DataSource = planificacionEditar.listaTemasPlanificacion;
            gvwPlanificacion.DataBind();
            gvwPlanificacion.Visible = true;
            ValidarAprobaciones();
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
                    if ((User.IsInRole(enumRoles.Director.ToString())
                    || User.IsInRole(enumRoles.Administrador.ToString()))
                    && planificacionEditar.solicitarAprobacion
                    )
                    {
                        chkAprobada.Enabled = true;
                        chkSolicitarAprobacion.Enabled = false;
                        chkSolicitarAprobacion.Checked = planificacionEditar.solicitarAprobacion;
                    }
                    else if ((User.IsInRole(enumRoles.Docente.ToString()) || User.IsInRole(enumRoles.Administrador.ToString()))
                        && !planificacionEditar.solicitarAprobacion
                        && !planificacionEditar.fechaAprobada.HasValue
                        )
                        chkSolicitarAprobacion.Enabled = true;
                }
            }
            else
                divAprobacion.Visible = false;
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
        }

        /// <summary>
        /// Cargars the planificacion.
        /// </summary>
        private void CargarPlanificacion()
        {
            var lista = planificacionEditar.listaTemasPlanificacion.Find(p => p.idTemaPlanificacion == idTemaPlanificacion);
            txtCActitudinales.Text = lista.contenidosActitudinales;
            txtCConceptuales.Text = lista.contenidosConceptuales;
            txtCProcedimentales.Text = lista.contenidosProcedimentales;
            txtCriteriosEvaluacion.Text = lista.criteriosEvaluacion;
            txtEstrategias.Text = lista.estrategiasAprendizaje;
            txtInstrumentosEvaluacion.Text = lista.instrumentosEvaluacion;
            calFechaDesde.Fecha.Text = lista.fechaInicioEstimada.ToString();
            calFechaFin.Fecha.Text = lista.fechaFinEstimada.ToString();
            chkAprobada.Enabled = false;
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
        #endregion

    }
}