using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using System.Collections.Generic;

namespace EDUAR_UI
{
    public partial class Welcome : EDUARBasePage
    {
        #region --[Atributos]--

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
                    string divID = "div" + ObjSessionDataUI.ObjDTUsuario.ListaRoles[0].Nombre;
                    foreach (Control item in Page.Form.Controls)
                    {
                        if (item.ID.Equals("MainContent") && (HtmlGenericControl)item.FindControl(divID) != null)
                        {
                            ((HtmlGenericControl)item.FindControl(divID)).Visible = true;
                            break;
                        }
                    }
                    fechas.startDate = cicloLectivoActual.fechaInicio;
                    fechas.endDate = cicloLectivoActual.fechaFin;
                    fechas.setSelectedDate(DateTime.Now, DateTime.Now.AddDays(15));
                    CargarAgenda();
                }
                else
                {
                    fechas.setSelectedDate((DateTime)fechas.ValorFechaDesde, (DateTime)fechas.ValorFechaHasta);
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
                fechas.ValidarRangoDesdeHasta(false);
                CargarAgenda();
                //if (BuscarSanciones())
                //{
                //    divFiltros.Visible = false;
                //    divReporte.Visible = true;
                //}
                //else
                //{ Master.MostrarMensaje("Faltan Datos", UIConstantesGenerales.MensajeDatosRequeridos, EDUAR_Utility.Enumeraciones.enumTipoVentanaInformacion.Advertencia); }
            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
        }
        #endregion

        #region --[Métodos Privados]
        /// <summary>
        /// Cargars the agenda.
        /// </summary>
        private void CargarAgenda()
        {
            CargarValoresEnPantalla();
            propAgenda.listaEventos.Sort((p, q) => DateTime.Compare(p.fechaEvento, q.fechaEvento));
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
            enumRoles obj = (enumRoles)Enum.Parse(typeof(enumRoles), ObjSessionDataUI.ObjDTUsuario.ListaRoles[0].Nombre);
            List<EventoAgenda> listaEventos = new List<EventoAgenda>();
            switch (obj)
            {
                case enumRoles.Alumno:
                    listaEventos = objBLAgenda.GetAgendaActividadesByAlumno(new Alumno() { username = ObjSessionDataUI.ObjDTUsuario.Nombre }, fechaDesde, fechaHasta);
                    break;
                case enumRoles.Docente:
                    listaEventos = objBLAgenda.GetAgendaActividadesByAlumno(new Alumno() { username = ObjSessionDataUI.ObjDTUsuario.Nombre }, fechaDesde, fechaHasta);
                    break;
                default:
                    break;
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
        #endregion
    }
}