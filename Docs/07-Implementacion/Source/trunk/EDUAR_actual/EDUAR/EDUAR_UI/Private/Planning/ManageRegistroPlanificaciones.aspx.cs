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
        public PlanificacionAnual propFiltroEvento
        {
            get
            {
                if (ViewState["propFiltroEvento"] == null)
                    propFiltroEvento = new PlanificacionAnual();

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
                this.txtDescripcionEdit.Attributes.Add("onkeyup", " ValidarCaracteres(this, 4000);");
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
                ddlTipoRegistroClase.SelectedValue = enumTipoRegistroClases.ClaseNormal.GetHashCode().ToString();
                esNuevo = true;
                btnBuscar.Visible = false;
                btnVolver.Visible = true;
                gvwReporte.Visible = false;
                litEditar.Visible = false;
                litNuevo.Visible = true;
                udpEdit.Visible = true;
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
                {
                    Response.Redirect("ManageRegistroClases.aspx", false);

                }
                else
                {
                    Response.Redirect("ManageAgendaActividades.aspx", false);
                }

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
                        //propEvento.idEventoAgenda = Convert.ToInt32(e.CommandArgument.ToString());
                        //CargaAgenda();
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlMeses control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int mes = 0;
                int.TryParse(ddlMeses.SelectedValue, out mes);
                if (mes > 0)
                {
                    ddlDia.Enabled = true;
                    //BindComboModulos(mes);
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlAsignaturaEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlAsignaturaEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idAsignatura = 0;
                int.TryParse(ddlAsignaturaEdit.SelectedValue, out idAsignatura);
                if (idAsignatura > 0)
                {
                    ddlMeses.Enabled = true;
                    if (DateTime.Now.Month >= 3)
                    {
                        ddlMeses.SelectedValue = DateTime.Now.Month.ToString();
                    }
                    else
                    {

                        ddlMeses.SelectedValue = "3";
                    }
                    ddlDia.Enabled = true;
                    //BindComboModulos(DateTime.Now.Month);
                }
                else
                {
                    ddlMeses.Enabled = false;
                    ddlDia.Enabled = false;
                }
                udpBotonera.Update();
                udpEdit.Update();
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
                //Master.ManageExceptions(ex);
            }
        }

        ///// <summary>
        ///// Handles the PageIndexChanged event of the gvwContenidos control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void gvwReporte_PageIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //ProductsSelectionManager.RestoreSelection(gvwContenidos, "listaSeleccion");
        //    }
        //    catch (Exception ex)
        //    {
        //        Master.ManageExceptions(ex);
        //    }
        //}


        #endregion


        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the presentacion.
        /// </summary>
        private void CargarPresentacion()
        {
            LimpiarCampos();
            CargarCombos();
            udpEdit.Visible = false;
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
            chkActivoEdit.Checked = false;
            if (ddlMeses.Items.Count > 0) ddlMeses.SelectedIndex = 0;
            if (ddlDia.Items.Count > 0) ddlDia.SelectedIndex = 0;
            calfechas.LimpiarControles();
            if (ddlAsignatura.Items.Count > 0) ddlAsignatura.SelectedIndex = 0;
            if (ddlAsignaturaEdit.Items.Count > 0) ddlAsignaturaEdit.SelectedIndex = 0;
            //if (listaContenido != null && listaContenido.Count > 0) listaContenido.Clear();
            txtDescripcionEdit.Text = string.Empty;
            //listaSeleccionGuardar.Clear();
        }

        /// <summary>
        /// Cargars the combos.
        /// </summary>
        private void CargarCombos()
        {
            //BLAsignatura objBLAsignatura = new BLAsignatura();
            //Asignatura objAsignatura = new Asignatura();
            //objAsignatura.cursoCicloLectivo.idCursoCicloLectivo = propAgenda.cursoCicloLectivo.idCursoCicloLectivo;
            //objAsignatura.cursoCicloLectivo.idCicloLectivo = propAgenda.cursoCicloLectivo.idCicloLectivo;
            //if (User.IsInRole(enumRoles.Docente.ToString()))
            //    objAsignatura.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

            //ddlAsignatura.Items.Clear();
            //ddlAsignaturaEdit.Items.Clear();
            //ddlMeses.Items.Clear();
            //UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, objBLAsignatura.GetAsignaturasCurso(objAsignatura), "idAsignatura", "nombre", false, true);
            //UIUtilidades.BindComboMeses(ddlMeses, false, cicloLectivoActual.fechaInicio.Month);
            //ddlMeses.Enabled = false;

            //BLTipoRegistroClases objBLTipoRegistroClase = new BLTipoRegistroClases();
            //List<TipoRegistroClases> listaRegistros = new List<TipoRegistroClases>();
            //listaRegistros = objBLTipoRegistroClase.GetTipoRegistroClases(new TipoRegistroClases());
            //UIUtilidades.BindCombo<TipoRegistroClases>(ddlTipoRegistroClase, listaRegistros, "idTipoRegistroClases", "nombre", true, false);
        }


        /// Cargars the grilla.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">The lista.</param>
        private void CargarGrilla()
        {
            gvwReporte.DataSource = UIUtilidades.BuildDataTable<PlanificacionAnual>(listaPlanificaciones).DefaultView;
            gvwReporte.DataBind();
            udpEdit.Visible = false;
            udpGrilla.Update();
        }

        /// <summary>
        /// Buscars the filtrando.
        /// </summary>
        private void BuscarFiltrando()
        {
            calfechas.ValidarRangoDesdeHasta(false);
            PlanificacionAnual entidad = new PlanificacionAnual();
            //entidad.asignatura.idAsignatura = Convert.ToInt32(ddlAsignatura.SelectedValue);
            //entidad.fechaEventoDesde = Convert.ToDateTime(calfechas.ValorFechaDesde);
            //entidad.fechaEventoHasta = Convert.ToDateTime(calfechas.ValorFechaHasta);
            //entidad.activo = chkActivo.Checked;
            propFiltroEvento = entidad;
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
            if (txtDescripcionEdit.Text.Trim().Length == 0)
                mensaje = "- Descripcion<br />";
            if (string.IsNullOrEmpty(ddlAsignaturaEdit.SelectedValue) || !(Convert.ToInt32(ddlAsignaturaEdit.SelectedValue) > 0))
                mensaje += "- Asignatura<br />";
            if (string.IsNullOrEmpty(ddlMeses.SelectedValue)
                || !(Convert.ToInt32(ddlMeses.SelectedValue) > 0)
                || string.IsNullOrEmpty(ddlDia.SelectedValue)
                || !(Convert.ToInt32(ddlDia.SelectedValue) > 0))
                mensaje += "- Fecha de Registro<br />";
            int idTipoClase = 0;
            int.TryParse(ddlTipoRegistroClase.SelectedValue, out idTipoClase);
            if (idTipoClase <= 0)
                mensaje += "- Tipo de Registro de Clase<br />";
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

        #endregion
    }
}