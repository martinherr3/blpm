using System;
using System.Web.UI;
using EDUAR_UI.Shared;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Common;
using EDUAR_DataAccess.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;
using System.Collections.Generic;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI
{
    public partial class RegisterEventoInstitucional : EDUARBasePage
    {
        #region --[Atributos]--
        private BLEventoInstitucional objBLEvento;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the lista evento.
        /// </summary>
        /// <value>
        /// The lista evento.
        /// </value>
        public List<EventoInstitucional> listaEvento
        {
            get
            {
                if (ViewState["listaEvento"] == null)
                    return new List<EventoInstitucional>();

                return (List<EventoInstitucional>)ViewState["listaEvento"];
            }
            set { ViewState["listaEvento"] = value; }
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
                    //Hora.Text = DateTime.Now.ToString("HH:mm");

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
        void VentanaAceptar(object sender, EventArgs e)
        {
            EventoInstitucional evento;
            try
            {
                if (AccionPagina == enumAcciones.Guardar)
                {
                    AccionPagina = enumAcciones.Limpiar;
                    evento = new EventoInstitucional();

                    evento.tipoEventoInstitucional = null;
                    evento.organizador = null;

                    registrarEvento(evento);
                    Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
                    AccionPagina = enumAcciones.Salir;
                }
                else
                    if (AccionPagina == enumAcciones.Salir)
                        Response.Redirect("~/Default.aspx", false);
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
                calfecha.ValidarRangoDesde();
                EventoInstitucional evento = new EventoInstitucional();
                evento.lugar = txtLugar.Text.Trim();
                evento.descripcionBreve = txtTitulo.Text.Trim();
                evento.fecha = Convert.ToDateTime(calfecha.ValorFecha);
                objBLEvento = new BLEventoInstitucional(evento);
                listaEvento = objBLEvento.GetEventoInstitucional(evento);

                //gvwReporte = UIUtilidades.GenerarGrilla(listaEvento, gvwReporte);

                CargarGrilla();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRegisterEvent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = EDUAR_Utility.Enumeraciones.enumAcciones.Guardar;
                Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">The lista.</param>
        private void CargarGrilla()
        {
            gvwReporte.DataSource = UIUtilidades.BuildDataTable<EventoInstitucional>(listaEvento).DefaultView;
            gvwReporte.DataBind();
            udpReporte.Update();
        }
        /// <summary>
        /// Registrar el evento.
        /// </summary>
        private void registrarEvento(EventoInstitucional evento)
        {
            objBLEvento = new BLEventoInstitucional(evento);

            objBLEvento.Save();
        }

        #endregion
    }
}