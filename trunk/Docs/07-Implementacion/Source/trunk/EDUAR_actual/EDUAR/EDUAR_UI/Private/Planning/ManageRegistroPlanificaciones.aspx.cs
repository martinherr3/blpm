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

                    if (User.IsInRole(enumRoles.Docente.ToString()))
                    {
                        AsignaturaCicloLectivo asignatura = new AsignaturaCicloLectivo();
                        asignatura.docente.username = User.Identity.Name;
                        asignatura.cursoCicloLectivo.cicloLectivo = cicloLectivoActual;
                        listaNiveles = objBLNivel.GetNiveles(asignatura);
                    }
                    else
                        listaNiveles = objBLNivel.GetNiveles();
                }
                return (List<Nivel>)ViewState["listaNiveles"];
            }
            set { ViewState["listaNiveles"] = value; }
        }

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
        /// Gets or sets the lista cursos.
        /// </summary>
        /// <value>
        /// The lista cursos.
        /// </value>
        public List<CursoCicloLectivo> listaCursos
        {
            get
            {
                if (Session["listaCursos"] == null && cicloLectivoActual != null)
                {
                    BLCurso objBLCurso = new BLCurso();
                    Nivel objFiltro = new Nivel();
                    objFiltro.idNivel = idNivel;
                    listaCursos = objBLCurso.GetCursosCicloLectivo(objFiltro, cicloLectivoActual);
                }
                return (List<CursoCicloLectivo>)Session["listaCursos"];
            }
            set { Session["listaCursos"] = value; }
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
                    BuscarFiltrando();
                }
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
                if (idNivel > 0)
                {
                    this.idNivel = idNivel;
                    CargarComboAsignatura();
                }
                else
                {
                    CargarPresentacion();
                }
                BuscarFiltrando();
                listaCursos.Clear();
                listaCursos = null;
                ddlAsignatura.Enabled = idNivel > 0;
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
                }
                else
                {
                    CargarPresentacion();
                    udpGrilla.Update();
                }
                BuscarFiltrando();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
                idAsignatura = -1;
            }
        }

        #endregion


        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the presentacion.
        /// </summary>
        private void CargarPresentacion()
        {
            udpFiltrosBusqueda.Visible = true;
            gvwReporte.Visible = true;
            udpFiltros.Update();
            udpGrilla.Update();
            UIUtilidades.BindCombo<Nivel>(ddlNivel, listaNiveles, "idNivel", "Nombre", true);
            ddlAsignatura.Items.Clear();
            ddlAsignatura.Items.Add("[Seleccione Nivel]");
            ddlAsignatura.Enabled = false;
            this.idAsignatura = 0;
            this.idNivel = 0;
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
            PlanificacionAnual entidad = new PlanificacionAnual();
            if (this.idNivel > 0)
            {
                entidad.curricula.nivel.idNivel = this.idNivel;
            }
            if (this.idAsignatura > 0)
            {
                entidad.curricula.asignatura.idAsignatura = this.idAsignatura;
            }
            entidad.cicloLectivo = cicloLectivoActual;
            listaPlanificaciones = objBLPlanificacion.GetPlanificacion(entidad);
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
            ddlAsignatura.Items.Clear();
            List<Asignatura> listaAsignaturas = new List<Asignatura>();
            BLAsignatura objBLAsignatura = new BLAsignatura();
            AsignaturaCicloLectivo asignatura = new AsignaturaCicloLectivo();

            if (User.IsInRole(enumRoles.Docente.ToString()))
                asignatura.docente.username = User.Identity.Name;
            asignatura.cursoCicloLectivo.curso.nivel.idNivel = idNivel;
            asignatura.cursoCicloLectivo.cicloLectivo = cicloLectivoActual;

            listaAsignaturas = objBLAsignatura.GetAsignaturasNivel(asignatura);
            if (listaAsignaturas != null && listaAsignaturas.Count > 0)
            {
                UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, listaAsignaturas, "idAsignatura", "nombre", true);
                if (listaAsignaturas.Count == 1)
                {
                    ddlAsignatura.SelectedIndex = 1;
                    idAsignatura = listaAsignaturas[0].idAsignatura;
                }
            }
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