using System;
using System.Web.UI;
using EDUAR_UI.Shared;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Common;
using EDUAR_DataAccess.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;

namespace EDUAR_UI
{
    public partial class RegisterEventoInstitucional : EDUARBasePage
    {
        #region --[Atributos]--
        private BLEventoInstitucional objBLEvento;
        #endregion

        #region --[Propiedades]--
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
        
        protected void Page_Load(object sender, EventArgs e)
        {
                       
            try
            {
                //if (!Page.IsPostBack)
                //{
                    //Hora.Text = DateTime.Now.ToString("HH:mm");
                    Master.BotonAvisoAceptar += (VentanaAceptar);
                //}
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
            }
        }

        void VentanaAceptar(object sender, EventArgs e)
        {
            EventoInstitucional evento;
            try
            {
                if (AccionPagina == enumAcciones.Guardar)
                {
                    AccionPagina = enumAcciones.Limpiar;
                    evento = new EventoInstitucional();

                    //evento.lugar = Lugar.Text.Trim();
                    //evento.descripcionBreve = Titulo.Text.Trim();
                    //evento.detalle = Detalle.Text.Trim();
                    //evento.fecha = Convert.ToDateTime(Fecha.Text + " " + Hora.Text);
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
        /// Registrar el evento.
        /// </summary>
        private void registrarEvento(EventoInstitucional evento)
        {
            String nombre = ObjDTSessionDataUI.ObjDTUsuario.Nombre;

            objBLEvento = new BLEventoInstitucional(evento);
                        
            objBLEvento.Save();
        }

        #endregion
    }
}