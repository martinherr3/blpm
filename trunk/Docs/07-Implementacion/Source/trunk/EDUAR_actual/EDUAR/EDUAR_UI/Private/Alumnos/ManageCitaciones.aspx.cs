using System;
using System.Collections.Generic;
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
    public partial class ManageCitaciones : EDUARBasePage
    {
        #region --[Atributos]--
        private BLCitacion objBLCitacion;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the prop citacion.
        /// </summary>
        /// <value>
        /// The prop citacion.
        /// </value>
        public Citacion propCitacion
        {
            get
            {
                if (ViewState["propCitacion"] == null)
                    propCitacion = new Citacion();
                return (Citacion)ViewState["propCitacion"];
            }
            set
            {
                ViewState["propCitacion"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the prop filtro citación.
        /// </summary>
        /// <value>
        /// The prop filtro citacion.
        /// </value>
        public Citacion propFiltroCitacion
        {
            get
            {
                if (ViewState["propFiltroCitacion"] == null)
                    propFiltroCitacion = new Citacion();

                return (Citacion)ViewState["propFiltroCitacion"];
            }
            set { ViewState["propFiltroCitacion"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista agenda.
        /// </summary>
        /// <value>
        /// The lista agenda.
        /// </value>
        public List<Citacion> listaCitaciones
        {
            get
            {
                if (ViewState["listaCitaciones"] == null)
                    listaCitaciones = new List<Citacion>();

                return (List<Citacion>)ViewState["listaCitaciones"];
            }
            set { ViewState["listaCitaciones"] = value; }
        }

        /// <summary>
        /// Gets or sets the prop ciclo lectivo.
        /// </summary>
        /// <value>
        /// The prop ciclo lectivo.
        /// </value>
        public CicloLectivo propCicloLectivo
        {
            get
            {
                if (ViewState["propCicloLectivo"] == null)
                {
                    propCicloLectivo = new CicloLectivo();
                    BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
                    List<CicloLectivo> objCicloLectivo = new List<CicloLectivo>();
                    objCicloLectivo = objBLCicloLectivo.GetCicloLectivos(new CicloLectivo() { activo = true });
                    if (objCicloLectivo.Count > 0)
                        propCicloLectivo = objCicloLectivo[0];
                }
                return (CicloLectivo)ViewState["propCicloLectivo"];
            }
            set { ViewState["propCicloLectivo"] = value; }
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
                    BuscarCitacion(null);
                    //Siempre que se acceda a la página debiera existir una agenda
                    //propEvento.idAgendaActividad = propAgenda.idAgendaActividad;
                    //if (propEvento.idAgendaActividad > 0)
                    //{
                    //    BuscarAgenda(propEvento);
                    //}
                    //else
                    //    BuscarAgenda(null);
                }
                this.txtDescripcionEdit.Attributes.Add("onkeyup", " ValidarCaracteres(this, 4000);");
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
                        BuscarFiltrando();
                        break;
                    case enumAcciones.Guardar:
                        AccionPagina = enumAcciones.Limpiar;
                        GuardarEntidad(ObtenerValoresDePantalla());
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
                LimpiarCampos();
                propCitacion = new Citacion();
                CargarCombosEdicion();
                esNuevo = true;
                btnGuardar.Visible = true;
                btnBuscar.Visible = false;
                btnVolver.Visible = true;
                btnNuevo.Visible = false;
                gvwReporte.Visible = false;
                litEditar.Visible = false;
                litNuevo.Visible = true;
                udpEdit.Visible = true;
                udpFiltrosBusqueda.Visible = false;
                udpFiltros.Update();
                udpGrilla.Update();
                txtAlumno.Visible = true;
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
                        AccionPagina = enumAcciones.Guardar;
                        Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
                    }
                }
                else
                {
                    AccionPagina = enumAcciones.Error;
                    Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
                }
                //}
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
                ViewState.Clear();
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
        protected void gvwReporte_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        propCitacion.idCitacion = Convert.ToInt32(e.CommandArgument.ToString());
                        CargaCitacion();
                        break;
                }
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
        protected void gvwReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvwReporte.PageIndex = e.NewPageIndex;
                CargarGrilla();
            }
            catch (Exception ex) { Master.ManageExceptions(ex); }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCicloLectivo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlCicloLectivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
                if (idCicloLectivo <= 0)
                    ddlCurso.Items.Clear();
                ddlTutores.Items.Clear();
                ddlTutores.Enabled = false;
                CargarComboCursos(idCicloLectivo, ddlCurso);
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
                int idCurso = Convert.ToInt32(ddlCurso.SelectedValue);
                int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
                CargarComboTutor(idCurso, idCicloLectivo, ddlTutores);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCursoEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlCursoEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCursoCicloLectivo = Convert.ToInt32(ddlCursoEdit.SelectedValue);
                CargarComboTutor(idCursoCicloLectivo, propCicloLectivo.idCicloLectivo, ddlTutorEdit);
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
            //lblTitulo.Text = propAgenda.cursoCicloLectivo.curso.nombre + " - " + propAgenda.cursoCicloLectivo.cicloLectivo.nombre;
            LimpiarCampos();
            CargarCombos();
            udpEdit.Visible = false;
            btnVolver.Visible = false;
            btnGuardar.Visible = false;
            udpFiltrosBusqueda.Visible = true;
            btnNuevo.Visible = true;
            btnBuscar.Visible = true;
            gvwReporte.Visible = true;
            udpFiltros.Update();
            udpGrilla.Update();
        }

        /// <summary>
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            chkActivo.Checked = true;
            chkActivoEdit.Checked = false;
            //if (ddlMeses.Items.Count > 0) ddlMeses.SelectedIndex = 0;
            //if (ddlDia.Items.Count > 0) ddlDia.SelectedIndex = 0;
            calfechas.LimpiarControles();
            //ddlAsignatura.SelectedIndex = 0;
            //ddlAsignaturaEdit.SelectedIndex = 0;
            ddlTutorEdit.Items.Clear();
            txtDescripcionEdit.Text = string.Empty;
        }

        /// <summary>
        /// Cargars the combos.
        /// </summary>
        private void CargarCombos()
        {
            List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
            BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
            listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(new CicloLectivo() { activo = true });

            UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);

            List<MotivoCitacion> listaMotivos = new List<MotivoCitacion>();
            BLMotivoCitacion objBLMotivos = new BLMotivoCitacion();
            listaMotivos = objBLMotivos.GetMotivos(new MotivoCitacion());
            UIUtilidades.BindCombo<MotivoCitacion>(ddlMotivoCitacion, listaMotivos, "idMotivoCitacion", "nombre", false, true);

            ddlCurso.Enabled = false;
            ddlTutores.Enabled = false;
        }

        /// <summary>
        /// Cargars the combos edicion.
        /// </summary>
        private void CargarCombosEdicion()
        {
            CargarComboCursos(propCicloLectivo.idCicloLectivo, ddlCursoEdit);

            List<MotivoCitacion> listaMotivos = new List<MotivoCitacion>();
            BLMotivoCitacion objBLMotivos = new BLMotivoCitacion();
            listaMotivos = objBLMotivos.GetMotivos(new MotivoCitacion());
            UIUtilidades.BindCombo<MotivoCitacion>(ddlMotivoEdit, listaMotivos, "idMotivoCitacion", "nombre", true);
        }

        /// <summary>
        /// Cargars the combo cursos.
        /// </summary>
        /// <param name="idCicloLectivo">The id ciclo lectivo.</param>
        /// <param name="ddlCurso">The DDL curso.</param>
        private void CargarComboCursos(int idCicloLectivo, DropDownList ddlCurso)
        {
            if (idCicloLectivo > 0)
            {
                BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
                List<Curso> listaCursos = new List<Curso>();
                if (User.IsInRole(enumRoles.Docente.ToString()))
                {
                    Asignatura objFiltro = new Asignatura();
                    objFiltro.curso.cicloLectivo = cicloLectivoActual;
                    //nombre del usuario logueado
                    objFiltro.docente.username = User.Identity.Name;
                    listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
                }
                else
                {
                    Curso objCurso = new Curso();
                    listaCursos = objBLCicloLectivo.GetCursosByCicloLectivo(idCicloLectivo);
                }
                UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "nombre", true);
                ddlCurso.Enabled = true;
            }
            else
            {
                ddlCurso.Enabled = false;
            }
        }

        /// <summary>
        /// Cargars the combo tutor.
        /// </summary>
        /// <param name="idCurso">The id curso.</param>
        /// <param name="ddlTutor">The DDL tutor.</param>
        private void CargarComboTutor(int idCursoCicloLectivo, int idCicloLectivo, DropDownList ddlTutor)
        {
            if (idCursoCicloLectivo > 0)
            {
                List<Tutor> listaTutores = new List<Tutor>();
                BLTutor objBLTutor = new BLTutor();
                AlumnoCurso objFiltro = new AlumnoCurso();
                objFiltro.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(idCursoCicloLectivo);
                objFiltro.curso.cicloLectivo.idCicloLectivo = Convert.ToInt32(idCicloLectivo);
                listaTutores = objBLTutor.GetTutoresPorCurso(objFiltro);
                UIUtilidades.BindCombo<Tutor>(ddlTutor, listaTutores, "idTutor", "apellido", "nombre", true);
                ddlTutor.Enabled = true;
            }
            else
            { ddlTutor.Enabled = false; }
        }

        /// Cargars the grilla.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">The lista.</param>
        private void CargarGrilla()
        {
            gvwReporte.DataSource = UIUtilidades.BuildDataTable<Citacion>(listaCitaciones).DefaultView;
            gvwReporte.DataBind();
            udpEdit.Visible = false;
            udpGrilla.Update();
        }

        /// <summary>
        /// Buscars the filtrando.
        /// </summary>
        private void BuscarFiltrando()
        {
            string mensaje = ValidarPaginaBuscando();
            if (mensaje == string.Empty)
            {
                calfechas.ValidarRangoDesdeHasta(false);
                Citacion entidad = new Citacion();
                entidad.fechaEventoDesde = Convert.ToDateTime(calfechas.ValorFechaDesde);
                entidad.fechaEventoHasta = Convert.ToDateTime(calfechas.ValorFechaHasta);
                entidad.motivoCitacion.idMotivoCitacion = Convert.ToInt32(ddlMotivoCitacion.SelectedValue);
                entidad.tutor.idTutor = (!string.IsNullOrEmpty(ddlTutores.SelectedValue)) ? Convert.ToInt32(ddlTutores.SelectedValue) : 0;
                entidad.activo = chkActivo.Checked;
                propFiltroCitacion = entidad;
                BuscarCitacion(entidad);
            }
            else
            {
                AccionPagina = enumAcciones.Error;
                Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
            }

        }

        /// <summary>
        /// Buscars the entidads.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void BuscarCitacion(Citacion entidad)
        {
            CargarLista(entidad);
            CargarGrilla();
        }

        /// <summary>
        /// Cargars the lista.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void CargarLista(Citacion entidad)
        {
            objBLCitacion = new BLCitacion();
            listaCitaciones = objBLCitacion.GetCitaciones(entidad);
        }

        /// <summary>
        /// Obteners the valores pantalla.
        /// </summary>
        /// <returns></returns>
        private Citacion ObtenerValoresDePantalla()
        {
            Citacion entidad = new Citacion();
            entidad = propCitacion;
            if (!esNuevo)
            {
                entidad.idCitacion = propCitacion.idCitacion;
            }
            entidad.tutor.idTutor = Convert.ToInt32(ddlTutorEdit.SelectedValue);
            entidad.motivoCitacion.idMotivoCitacion = Convert.ToInt32(ddlMotivoEdit.SelectedValue);
            entidad.detalles = txtDescripcionEdit.Text;
            entidad.fecha = Convert.ToDateTime(calFechaEvento.ValorFecha);
            entidad.hora = new DateTime(entidad.fecha.Year, entidad.fecha.Month, entidad.fecha.Day, Convert.ToDateTime(txtHoraEdit.Text).Hour, Convert.ToDateTime(txtHoraEdit.Text).Minute, 0);
            entidad.organizador.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
            entidad.activo = chkActivoEdit.Checked;
            return entidad;
        }

        /// <summary>
        /// Guardars the agenda.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void GuardarEntidad(Citacion entidad)
        {
            BLCitacion objBLCitacion = new BLCitacion();
            objBLCitacion.VerificarDisponibilidad(entidad);

            objBLCitacion = new BLCitacion(entidad);
            objBLCitacion.Save();
        }

        /// <summary>
        /// Cargars the entidad.
        /// </summary>
        private void CargarValoresEnPantalla(int idCitacion)
        {
            Citacion entidad = listaCitaciones.Find(c => c.idCitacion == idCitacion);
            propCitacion = entidad;
            if (entidad != null)
            {
                txtDescripcionEdit.Text = entidad.detalles;
                CargarCombosEdicion();
                ddlCursoEdit.Enabled = false;
                calFechaEvento.Fecha.Text = entidad.fecha.ToShortDateString();
                txtHoraEdit.Text = entidad.hora.Hour.ToString().PadLeft(2, '0') + ":" + entidad.hora.Minute.ToString().PadLeft(2, '0');
                ddlMotivoEdit.SelectedValue = entidad.motivoCitacion.idMotivoCitacion.ToString();
                ddlTutorEdit.Items.Add(new ListItem(entidad.tutor.apellido + " " + entidad.tutor.nombre, entidad.tutor.idTutor.ToString()));
                ddlTutorEdit.SelectedValue = entidad.tutor.idTutor.ToString();
                ddlTutorEdit.Enabled = false;
                chkActivoEdit.Checked = entidad.activo;
            }
        }

        /// <summary>
        /// Validars the pagina.
        /// </summary>
        /// <returns></returns>
        private string ValidarPagina()
        {
            string mensaje = string.Empty;
            calFechaEvento.ValidarRangoDesde(false, true);

            BLFeriadosYFechasEspeciales objBLFeriado = new BLFeriadosYFechasEspeciales();
            objBLFeriado.ValidarFecha(Convert.ToDateTime(calFechaEvento.ValorFecha));


            String aux = txtHoraEdit.Text;
            String []aux2 = aux.Split(':');
            
            
            objBLFeriado.ValidarHora(new DateTime(System.DateTime.Now.Year,System.DateTime.Now.Month,System.DateTime.Now.Day,int.Parse(aux2[0]),int.Parse(aux2[1]), 0));




            if (txtDescripcionEdit.Text.Trim().Length == 0)
                mensaje = "- Descripcion<br />";
            if (calFechaEvento.Fecha.Text.Trim().Length == 0)
                mensaje += "- Fecha<br />";
            if (txtHoraEdit.Text.Trim().Length == 0)
                mensaje += "- Hora<br />";
            if (!string.IsNullOrEmpty(ddlTutorEdit.SelectedValue) && !(Convert.ToInt32(ddlTutorEdit.SelectedValue) > 0))
                mensaje += "- Tutor";
            if (!string.IsNullOrEmpty(ddlMotivoEdit.SelectedValue) && !(Convert.ToInt32(ddlMotivoEdit.SelectedValue) > 0))
                mensaje += "- Motivo de Citación";

            return mensaje;
        }
        //
        private string ValidarPaginaBuscando()
        {
            string mensaje = string.Empty;
            calfechas.ValidarRangoDesde(false, false);

            //if (txtDescripcionEdit.Text.Trim().Length == 0)
            //    mensaje = "- Descripcion<br />";
            //if (calFechaEvento.Fecha.Text.Trim().Length == 0)
            //    mensaje += "- Fecha<br />";
            //if (txtHoraEdit.Text.Trim().Length == 0)
            //    mensaje += "- Hora<br />";
            //if (!(Convert.ToInt32(ddlTutorEdit.SelectedValue) > 0))
            //    mensaje += "- Tutor";
            //if (!(Convert.ToInt32(ddlMotivoEdit.SelectedValue) > 0))
            //    mensaje += "- Motivo de Citación";

            if (!string.IsNullOrEmpty(ddlCurso.SelectedValue) && Convert.ToInt32(ddlCurso.SelectedValue) > 0 
                &&
                !string.IsNullOrEmpty(ddlTutores.SelectedValue) && Convert.ToInt32(ddlTutores.SelectedValue) <= 0)
                    mensaje += "- Tutor<br />";

            return mensaje;
        }

        /// <summary>
        /// Cargas the agenda.
        /// </summary>
        private void CargaCitacion()
        {
            AccionPagina = enumAcciones.Modificar;
            esNuevo = false;
            CargarValoresEnPantalla(propCitacion.idCitacion);
            litEditar.Visible = true;
            litNuevo.Visible = false;
            btnBuscar.Visible = false;
            btnNuevo.Visible = false;
            btnVolver.Visible = true;
            btnGuardar.Visible = true;
            gvwReporte.Visible = false;
            udpFiltrosBusqueda.Visible = false;
            udpEdit.Visible = true;
            udpFiltros.Update();
            udpEdit.Update();
        }

        protected void ddlTutores_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLTutor unBLTutor = new BLTutor();
            Tutor unTutor = new Tutor();
            unTutor.idTutor = int.Parse(ddlTutores.SelectedValue);

            List<Alumno> AlumnosTutor = new List<Alumno>();
            AlumnosTutor = unBLTutor.GetAlumnosDeTutor(unTutor);

            txtAlumno.Text = AlumnosTutor[0].nombre + " " + AlumnosTutor[0].apellido;

        }

        #endregion

        protected void ddlTutorEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLTutor unBLTutor = new BLTutor();
            Tutor unTutor = new Tutor();
            unTutor.idTutor = int.Parse(ddlTutorEdit.SelectedValue);

            List<Alumno> AlumnosTutor = new List<Alumno>();
            AlumnosTutor = unBLTutor.GetAlumnosDeTutor(unTutor, propCicloLectivo.idCicloLectivo,Convert.ToInt32(ddlCursoEdit.SelectedValue));

            txtAlumno.Text = AlumnosTutor[0].nombre + " " + AlumnosTutor[0].apellido;


        }

    }
}