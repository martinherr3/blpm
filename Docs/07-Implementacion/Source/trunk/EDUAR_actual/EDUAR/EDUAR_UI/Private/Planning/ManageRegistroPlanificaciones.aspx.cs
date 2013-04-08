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
    public partial class ManageRegistroPlanificaciones : EDUARBasePage
    {

        #region --[Atributos]--
        private BLPlanificacionAnual objBLPlanificacion;
        #endregion

        #region --[Propiedades]--

        /// <summary>
        /// Gets or sets the prop filtro evento.
        /// </summary>
        /// <value>
        /// The prop filtro evento.
        /// </value>
        public PlanificacionAnual propFiltroPlanificacion
        {
            get
            {
                if (ViewState["propFiltroEvento"] == null)
                    propFiltroPlanificacion = new PlanificacionAnual();

                return (PlanificacionAnual)ViewState["propFiltroEvento"];
            }
            set { ViewState["propFiltroEvento"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista agenda.
        /// </summary>
        /// <value>
        /// The lista agenda.
        /// </value>
        public List<PlanificacionAnual> listaPlanificaciones
        {
            get
            {
                if (ViewState["listaPlanificaciones"] == null)
                    listaPlanificaciones = new List<PlanificacionAnual>();

                return (List<PlanificacionAnual>)ViewState["listaPlanificaciones"];
            }
            set { ViewState["listaPlanificaciones"] = value; }
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
                    BuscarPlanificacion();
                }
                calfechas.startDate = cicloLectivoActual.fechaInicio;
                calfechas.endDate = cicloLectivoActual.fechaFin;
                //this.txtDescripcionEdit.Attributes.Add("onkeyup", " ValidarCaracteres(this, 4000);");
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
            }
        }

        ///// <summary>
        ///// Ventanas the aceptar.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void VentanaAceptar(object sender, EventArgs e)
        {
            try
            {
                switch (AccionPagina)
                {
                    case enumAcciones.Limpiar:
                        CargarPresentacion();
                        BuscarFiltrando();
                        //BuscarAgenda(propEvento);
                        break;
                    case enumAcciones.Error:
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
        /// Handles the RowCommand event of the gvwReporte control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gvwReporte_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        propFiltroPlanificacion.idPlanificacionAnual = Convert.ToInt32(e.CommandArgument.ToString());
                        editarPlanificacion();
                        break;
                    case "Aprobar":
                        propFiltroPlanificacion.idPlanificacionAnual = Convert.ToInt32(e.CommandArgument.ToString());
                        AprobarPlanificacion();
                        break;
                    case "Default":
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the gvwReporte control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void gvwReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
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
                esNuevo = true;
                btnBuscar.Visible = false;
                btnVolver.Visible = true;
                gvwReporte.Visible = false;
                udpFiltrosBusqueda.Visible = false;
                udpFiltros.Update();
                udpGrilla.Update();
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
                if (AccionPagina == enumAcciones.Nuevo || AccionPagina == enumAcciones.Modificar)
                    Response.Redirect("ManageRegistroClases.aspx", false);
                else
                    Response.Redirect("ManageAgendaActividades.aspx", false);
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
            btnVolver.Visible = true;
            udpFiltrosBusqueda.Visible = true;
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
            calfechas.LimpiarControles();
        }

        /// Cargars the grilla.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">The lista.</param>
        private void CargarGrilla()
        {
            gvwReporte.DataSource = UIUtilidades.BuildDataTable<PlanificacionAnual>(listaPlanificaciones).DefaultView;
            gvwReporte.DataBind();
            udpGrilla.Update();
        }

        /// <summary>
        /// Buscars the filtrando.
        /// </summary>
        private void BuscarFiltrando()
        {
            calfechas.ValidarRangoDesdeHasta(false);
            PlanificacionAnual entidad = new PlanificacionAnual();
            propFiltroPlanificacion = entidad;
            BuscarPlanificacion();
        }

        /// <summary>
        /// Buscars the entidads.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void BuscarPlanificacion(/*RegistroClases entidad*/)
        {
            CargarLista();
            CargarGrilla();
        }

        /// <summary>
        /// Cargars the lista.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void CargarLista()
        {
            objBLPlanificacion = new BLPlanificacionAnual();
            listaPlanificaciones = objBLPlanificacion.GetPlanificacion(cicloLectivoActual);
            calcularCobertura();
        }

        private void calcularCobertura()
        {
            objBLPlanificacion = new BLPlanificacionAnual();
            objBLPlanificacion.calcularCobertura(listaPlanificaciones);
        }

        /// <summary>
        /// Validars the pagina.
        /// </summary>
        /// <returns></returns>
        private string ValidarPagina()
        {
            string mensaje = string.Empty;
            return mensaje;
        }

        /// <summary>
        /// Cargars the combo asignatura.
        /// </summary>
        private void CargarComboAsignatura()
        {
            //BLAsignatura objBLAsignatura = new BLAsignatura();
            //Asignatura objAsignatura = new Asignatura();
            //objAsignatura.cursoCicloLectivo.idCursoCicloLectivo = propAgenda.cursoCicloLectivo.idCursoCicloLectivo;
            //objAsignatura.curso.cicloLectivo.idCicloLectivo = propAgenda.cursoCicloLectivo.idCicloLectivo;
            //if (User.IsInRole(enumRoles.Docente.ToString()))
            //    objAsignatura.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

            //UIUtilidades.BindCombo<Asignatura>(ddlAsignaturaEdit, objBLAsignatura.GetAsignaturasCurso(objAsignatura), "idAsignatura", "nombre", true);
        }

        /// <summary>
        /// Aprobars the planificacion.
        /// </summary>
        private void AprobarPlanificacion()
        {

            foreach (PlanificacionAnual unaPlanificacion in listaPlanificaciones)
            {
                if (propFiltroPlanificacion.idPlanificacionAnual == unaPlanificacion.idPlanificacionAnual)
                {
                    propFiltroPlanificacion.creador = unaPlanificacion.creador;
                    propFiltroPlanificacion.curricula = unaPlanificacion.curricula;
                    propFiltroPlanificacion.fechaAprobada = unaPlanificacion.fechaAprobada;
                    propFiltroPlanificacion.fechaCreacion = unaPlanificacion.fechaCreacion;
                    propFiltroPlanificacion.listaCursos = unaPlanificacion.listaCursos;
                    propFiltroPlanificacion.listaTemasPlanificacion = unaPlanificacion.listaTemasPlanificacion;
                    propFiltroPlanificacion.observaciones = unaPlanificacion.observaciones;
                    propFiltroPlanificacion.porcentajeCobertura = unaPlanificacion.porcentajeCobertura;
                    propFiltroPlanificacion.solicitarAprobacion = unaPlanificacion.solicitarAprobacion;
                }
            }

            PlanificacionAnual objAprobar = new PlanificacionAnual();
            objAprobar.creador.username = (string.IsNullOrEmpty(propFiltroPlanificacion.creador.username)) ? User.Identity.Name : propFiltroPlanificacion.creador.username;
            objAprobar.idPlanificacionAnual = propFiltroPlanificacion.idPlanificacionAnual;
            objAprobar.solicitarAprobacion = propFiltroPlanificacion.solicitarAprobacion;
            objAprobar.fechaAprobada = DateTime.Today;
            propFiltroPlanificacion.fechaAprobada = DateTime.Today;
            BLPlanificacionAnual objBLAprobar = new BLPlanificacionAnual(objAprobar);
            objBLAprobar.Save();

        }

        private void editarPlanificacion()
        {
            foreach (PlanificacionAnual unaPlanificacion in listaPlanificaciones)
            {
                if (propFiltroPlanificacion.idPlanificacionAnual == unaPlanificacion.idPlanificacionAnual)
                {
                    propFiltroPlanificacion.creador = unaPlanificacion.creador;
                    propFiltroPlanificacion.curricula = unaPlanificacion.curricula;
                    propFiltroPlanificacion.fechaAprobada = unaPlanificacion.fechaAprobada;
                    propFiltroPlanificacion.fechaCreacion = unaPlanificacion.fechaCreacion;
                    propFiltroPlanificacion.listaCursos = unaPlanificacion.listaCursos;
                    propFiltroPlanificacion.listaTemasPlanificacion = unaPlanificacion.listaTemasPlanificacion;
                    propFiltroPlanificacion.observaciones = unaPlanificacion.observaciones;
                    propFiltroPlanificacion.porcentajeCobertura = unaPlanificacion.porcentajeCobertura;
                    propFiltroPlanificacion.solicitarAprobacion = unaPlanificacion.solicitarAprobacion;
                }
            }

            cursoActual.curso.nivel.idNivel = propFiltroPlanificacion.curricula.nivel.idNivel ;
            cursoActual.curso.orientacion.idOrientacion = propFiltroPlanificacion.curricula.orientacion.idOrientacion;
            idAsignatura = propFiltroPlanificacion.curricula.asignatura.idAsignatura;

            Response.Redirect("~/Private/Planning/PlanificacionAnual.aspx", false);


 
        }
        #endregion
    }
}