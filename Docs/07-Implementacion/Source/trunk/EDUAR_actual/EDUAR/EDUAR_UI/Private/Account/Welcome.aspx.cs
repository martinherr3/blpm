﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_BusinessLogic.Encuestas;

namespace EDUAR_UI
{
    public partial class Welcome : EDUARBasePage
    {
        #region --[Atributos]--
        private BLSeguridad objBLSeguridad;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Mantiene la agenda seleccionada en la grilla.
        /// Se utiliza para el manejo de eventos de agenda (evaluación, excursión, reunión).
        /// </summary>
        /// <value>
        /// The prop agenda.
        /// </value>
        public AgendaActividades propAgenda
        {
            get
            {
                if (ViewState["propAgenda"] == null)
                    propAgenda = new AgendaActividades();

                return (AgendaActividades)ViewState["propAgenda"];
            }
            set { ViewState["propAgenda"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista cursos.
        /// </summary>
        /// <value>
        /// The lista cursos.
        /// </value>
        public List<Curso> listaCursos
        {
            //get
            //{
            //if (ViewState["listaCursos"] == null && cicloLectivoActual != null)
            //{
            //    BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();

            //    Asignatura objFiltro = new Asignatura();
            //    objFiltro.curso.cicloLectivo = cicloLectivoActual;
            //    //nombre del usuario logueado
            //    if (User.IsInRole(enumRoles.Docente.ToString()))
            //    {
            //        objFiltro.docente.username = User.Identity.Name;
            //        listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
            //    }
            //    if (User.IsInRole(enumRoles.Preceptor.ToString()))
            //    {
            //        Curso miCurso = new Curso();
            //        miCurso.cicloLectivo = cicloLectivoActual;
            //        miCurso.preceptor.username = User.Identity.Name;
            //        listaCursos = objBLCicloLectivo.GetCursosByCicloLectivo(miCurso);
            //    }
            //}
            //return (List<Curso>)ViewState["listaCursos"];
            //}
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
        /// Gets or sets the lista alumnos.
        /// </summary>
        /// <value>
        /// The lista alumnos.
        /// </value>
        public List<AlumnoCursoCicloLectivo> listaAlumnos
        {
            get
            {
                if (ViewState["listaAlumnos"] == null)
                {
                    List<Tutor> lista = new List<Tutor>();
                    lista.Add(new Tutor() { username = User.Identity.Name });
                    BLAlumno objBLAlumno = new BLAlumno(new Alumno() { listaTutores = lista });
                    listaAlumnos = objBLAlumno.GetAlumnosTutor(cicloLectivoActual);
                }
                return (List<AlumnoCursoCicloLectivo>)ViewState["listaAlumnos"];
            }
            set { ViewState["listaAlumnos"] = value; }
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
                    //Cargo en sesión los datos del usuario logueado
                    DTSeguridad propSeguridad = new DTSeguridad();
                    propSeguridad.Usuario.Nombre = User.Identity.Name;
                    objBLSeguridad = new BLSeguridad(propSeguridad);
                    objBLSeguridad.GetUsuario();
                    ObjSessionDataUI.ObjDTUsuario = objBLSeguridad.Data.Usuario;
                    divEncuesta.Visible = false;
                    if (User.IsInRole(enumRoles.Alumno.ToString()))
                    {
                        habilitarAlumno(false);
                        habilitarCurso(false);
                        BuscarEncuestas();
                        divAgenda.Visible = true;
                        lblCurso.Visible = false;
                        ddlCurso.Visible = false;
                    }
                    if (User.IsInRole(enumRoles.Docente.ToString()) || User.IsInRole(enumRoles.Preceptor.ToString()))
                    {
                        divAgenda.Visible = true;
                        habilitarAlumno(false);
                        UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "Nombre", true);
                    }
                    if (User.IsInRole(enumRoles.Tutor.ToString()))
                    {
                        divAgenda.Visible = true;
                        habilitarCurso(false);
                        BuscarEncuestas();
                        ddlAlumnos.Items.Clear();
                        ddlAlumnos.DataSource = null;
                        foreach (AlumnoCursoCicloLectivo item in listaAlumnos)
                        {
                            ddlAlumnos.Items.Insert(ddlAlumnos.Items.Count, new ListItem(item.alumno.apellido + " " + item.alumno.nombre, item.alumno.idAlumno.ToString()));
                        }
                        UIUtilidades.SortByText(ddlAlumnos);
                        ddlAlumnos.Items.Insert(0, new ListItem("[Seleccione]", "-1"));
                        ddlAlumnos.SelectedValue = "-1";
                    }

                    fechas.startDate = cicloLectivoActual.fechaInicio;
                    fechas.endDate = cicloLectivoActual.fechaFin;
                    fechas.setSelectedDate(DateTime.Now, DateTime.Now.AddDays(15));

                    BLMensaje objBLMensaje = new BLMensaje();
                    List<Mensaje> objMensajes = new List<Mensaje>();
                    objMensajes = objBLMensaje.GetMensajes(new Mensaje() { destinatario = new Persona() { username = ObjSessionDataUI.ObjDTUsuario.Nombre }, activo = true });
                    objMensajes = objMensajes.FindAll(p => p.leido == false);
                    if (objMensajes.Count > 0)
                    {
                        string mensaje = objMensajes.Count == 1 ? "mensaje" : "mensajes";

                        lblMensajes.Text = lblMensajes.Text.Replace("<MENSAJES>", objMensajes.Count.ToString());
                        lblMensajes.Text = lblMensajes.Text.Replace("<MSJ_STRING>", mensaje);
                    }
                    else
                        divMensajes.Visible = false;
                    CargarAgenda();
                }
                else
                {
                    fechas.setSelectedDate(
                            (fechas.ValorFechaDesde != null) ? (DateTime)fechas.ValorFechaDesde : DateTime.Now,
                            (fechas.ValorFechaHasta != null) ? (DateTime)fechas.ValorFechaHasta : DateTime.Now.AddDays(15));
                }
                //this.txtDescripcionEdit.Attributes.Add("onkeyup", " ValidarCaracteres(this, 4000);");

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
                //switch (AccionPagina)
                //{
                //    case enumAcciones.Limpiar:
                //        CargarPresentacion();
                //        BuscarAgenda(null);
                //        break;
                //    case enumAcciones.Guardar:
                //        AccionPagina = enumAcciones.Limpiar;
                //        GuardarAgenda(ObtenerValoresDePantalla());
                //        Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
                //        break;
                //    default:
                //        break;
                //}
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the gvwAgenda control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvwAgenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvwAgenda.PageIndex = e.NewPageIndex;
                CargarGrillaAgenda();
            }
            catch (Exception ex) { Master.ManageExceptions(ex); }
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
                string mensaje = ValidarPagina();
                if (mensaje == string.Empty)
                {
                    fechas.ValidarRangoDesdeHasta(false);
                    CargarAgenda();
                }
                else
                { Master.MostrarMensaje("Faltan Datos", UIConstantesGenerales.MensajeDatosFaltantes + mensaje, EDUAR_Utility.Enumeraciones.enumTipoVentanaInformacion.Advertencia); }
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }

        /// <summary>
        /// Handles the Click event of the btnMensaje control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnMensaje_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Private/Mensajes/MsjeEntrada.aspx", false);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEncuesta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnEncuesta_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Private/Encuestas/Cuestionario.aspx", false);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        #region --[Panel]--
        /// <summary>
        /// Handles the Click event of the btnCerrarPopup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCerrarPopup_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((ImageButton)sender).CommandArgument)
                {
                    case "Cursos":
                        mpeCursos.Hide();
                        break;
                    case "Encuestas":
                        mpeEncuestas.Hide();
                        break;
                    case "Reportes":
                        mpeReportes.Hide();
                        break;
                    case "Contenidos":
                        mpeContenidos.Hide();
                        break;
                    case "Comunicacion":
                        mpeComunicacion.Hide();
                        break;
                    case "Administracion":
                        mpeAdministracion.Hide();
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
        /// Handles the Click event of the btnCursos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnPopUP_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((ImageButton)sender).CommandArgument)
                {
                    case "Cursos":
                        dtlCursos.DataSource = listaCursos;
                        dtlCursos.DataBind();
                        udpCursos.Update();
                        mpeCursos.Show();
                        break;
                    case "Encuestas":
                        mpeEncuestas.Show();
                        break;
                    case "Reportes":
                        mpeReportes.Show();
                        break;
                    case "Contenidos":
                        mpeContenidos.Show();
                        break;
                    case "Comunicacion":
                        mpeComunicacion.Show();
                        break;
                    case "Administracion":
                        mpeAdministracion.Show();
                        break;
                    default:
                        break;
                }
                //Response.Redirect("~/Private/Encuestas/Cuestionario.aspx", false);

            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRedireccion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnRedireccion_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((ImageButton)sender).CommandArgument)
                {
                    case "Categorias":
                        Response.Redirect("~/Private/Encuestas/ManageCategoriasPregunta.aspx", false);
                        break;
                    case "Encuestas":
                        Response.Redirect("~/Private/Encuestas/ManageContenidoEncuestas.aspx", false);
                        break;
                    case "Escalas":
                        Response.Redirect("~/Private/Encuestas/ManageEscalasPonderacion.aspx", false);
                        break;
                    case "Calificaciones":
                        Response.Redirect("~/Private/Reports/reportCalificacionesAlumnoPeriodo.aspx", false);
                        break;
                    case "Inasistencias":
                        Response.Redirect("~/Private/Reports/reportInasistenciasAlumnoPeriodo.asp", false);
                        break;
                    case "Sanciones":
                        Response.Redirect("~/Private/Reports/reportSancionesAlumnoPeriodo.aspx", false);
                        break;
                    case "Consolidado":
                        Response.Redirect("~/Private/Reports/reportPromedios.aspx", false);
                        break;
                    case "Indicadores":
                        Response.Redirect("~/Private/Monitoreo/IndicadoresGenerales.aspx", false);
                        break;
                    case "Historico":
                        Response.Redirect("~/Private/Reports/Historicos/reportRendimiento.aspx", false);
                        break;
                    case "Contenidos":
                        Response.Redirect("~/Private/Planning/Contenido.aspx", false);
                        break;
                    case "Planificacion":
                        Response.Redirect("~/Private/Planning/PlanificacionAnual.aspx", false);
                        break;
                    case "Aprobar":
                        Response.Redirect("~/Private/Planning/ManageRegistroPlanificaciones.aspx", false);
                        break;
                    case "Mensajes":
                        Response.Redirect("~/Private/Mensajes/MsjeEntrada.aspx", false);
                        break;
                    case "Citaciones":
                        Response.Redirect("~/Private/Alumnos/ManageCitaciones.aspx", false);
                        break;
                    case "Novedades":
                        Response.Redirect("~/Private/Novedades/RegNovedadInstitucion.aspx", false);
                        break;
                    case "Usuarios":
                        Response.Redirect("~/Private/Account/ChangeUser.aspx", false);
                        break;
                    case "ConfigIndicadores":
                        Response.Redirect("~/Private/Monitoreo/ConfigIndicadores.aspx", false);
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

        #endregion
        #endregion

        #region --[Métodos Privados]
        /// <summary>
        /// Cargars the agenda.
        /// </summary>
        private void CargarAgenda()
        {
            CargarValoresEnPantalla();
            if (propAgenda.listaEventos.Count > 0) propAgenda.listaEventos.Sort((p, q) => DateTime.Compare(p.fechaEvento, q.fechaEvento));
            CargarGrillaAgenda();
            udpGrilla.Update();
        }

        /// <summary>
        /// Cargars the entidad.
        /// </summary>
        private void CargarValoresEnPantalla()
        {
            BLAgendaActividades objBLAgenda = new BLAgendaActividades();
            DateTime fechaDesde = DateTime.Now;
            DateTime fechaHasta = DateTime.Now.AddDays(15);
            if (fechas.ValorFechaDesde != null)
                fechaDesde = (DateTime)fechas.ValorFechaDesde;
            if (fechas.ValorFechaHasta != null)
                fechaHasta = (DateTime)fechas.ValorFechaHasta;

            CursoCicloLectivo objCurso = new CursoCicloLectivo();
            List<EventoAgenda> listaEventos = new List<EventoAgenda>();

            if (HttpContext.Current.User.IsInRole(enumRoles.Alumno.ToString()))
            {

                objCurso.cicloLectivo = cicloLectivoActual;
                listaEventos = objBLAgenda.GetAgendaActividadesByRol(new Alumno() { username = ObjSessionDataUI.ObjDTUsuario.Nombre }, null, objCurso, fechaDesde, fechaHasta);
            }
            else if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()) || HttpContext.Current.User.IsInRole(enumRoles.Preceptor.ToString()))
            {
                if (Convert.ToInt16(ddlCurso.SelectedValue) > 0)
                {
                    objCurso.idCursoCicloLectivo = Convert.ToInt16(ddlCurso.SelectedValue);
                    listaEventos = objBLAgenda.GetAgendaActividadesByRol(null, null, objCurso, fechaDesde, fechaHasta);
                }

            }
            else if (HttpContext.Current.User.IsInRole(enumRoles.Tutor.ToString()))
            {
                if (Convert.ToInt16(ddlAlumnos.SelectedValue) > 0)
                {
                    objCurso.idCursoCicloLectivo = (listaAlumnos.Find(p => p.alumno.idAlumno == Convert.ToInt16(ddlAlumnos.SelectedValue))).cursoCicloLectivo.idCursoCicloLectivo;
                    listaEventos = objBLAgenda.GetAgendaActividadesByRol(null, null, objCurso, fechaDesde, fechaHasta);
                }
            }
            propAgenda.listaEventos = listaEventos;
        }

        /// <summary>
        /// Cargars the grilla agenda.
        /// </summary>
        private void CargarGrillaAgenda()
        {
            gvwAgenda.DataSource = UIUtilidades.BuildDataTable<EventoAgenda>(propAgenda.listaEventos).DefaultView;
            gvwAgenda.DataBind();
        }

        /// <summary>
        /// Habilitars the curso.
        /// </summary>
        /// <param name="mostrar">if set to <c>true</c> [mostrar].</param>
        private void habilitarCurso(bool mostrar)
        {
            lblCurso.Visible = mostrar;
            ddlCurso.Visible = mostrar;
        }

        /// <summary>
        /// Habilitars the alumno.
        /// </summary>
        /// <param name="mostrar">if set to <c>true</c> [mostrar].</param>
        private void habilitarAlumno(bool mostrar)
        {
            lblAlumnos.Visible = mostrar;
            ddlAlumnos.Visible = mostrar;
        }

        /// <summary>
        /// Validars the pagina.
        /// </summary>
        /// <returns></returns>
        private string ValidarPagina()
        {
            string mensaje = string.Empty;

            if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()) || HttpContext.Current.User.IsInRole(enumRoles.Preceptor.ToString()))
            {
                int idCurso = 0;
                int.TryParse(ddlCurso.SelectedValue, out idCurso);
                if (!(idCurso > 0))
                    mensaje = "- Curso<br />";
            }
            else if (HttpContext.Current.User.IsInRole(enumRoles.Tutor.ToString()))
            {
                int idAlumno = 0;
                int.TryParse(ddlAlumnos.SelectedValue, out idAlumno);
                if (!(idAlumno > 0))
                    mensaje = "- Alumno<br />";
            }
            return mensaje;
        }

        /// <summary>
        /// Buscars the encuestas.
        /// </summary>
        private void BuscarEncuestas()
        {
            BLEncuestaDisponible objBLEncuestaDisponible = new BLEncuestaDisponible();

            EncuestaDisponible encuestaSkeleton = new EncuestaDisponible();
            encuestaSkeleton.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
            int cantidad = objBLEncuestaDisponible.GetEncuestasDisponibles(encuestaSkeleton).Count;
            if (cantidad > 0)
            {
                lblEncuestas.Text = lblEncuestas.Text.Replace("<ENCUESTAS>", cantidad.ToString());
                if (cantidad == 1)
                    lblEncuestas.Text = lblEncuestas.Text.Replace("Encuestas", "Encuesta");
            }
            divEncuesta.Visible = cantidad > 0;
        }
        #endregion
    }
}