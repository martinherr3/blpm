using System;
using EDUAR_UI.Shared;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace EDUAR_UI
{
    public partial class MsjeRedactar : EDUARBasePage
    {
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
            BLPersona objpersona = new BLPersona();
            List<Persona> lista = objpersona.GetPersonas(new Persona() { activo = true });
            foreach (Persona item in lista)
            {
                ddlDestino.Items.Add(new System.Web.UI.WebControls.ListItem(item.apellido + " " + item.nombre, item.idPersona.ToString()));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Mensaje objMensaje = new Mensaje();

            objMensaje.textoMensaje = textoMensaje.contenido;
            objMensaje.remitente = ObjDTSessionDataUI.ObjDTUsuario;
            objMensaje.fechaEnvio = DateTime.Now;
            objMensaje.horaEnvio = Convert.ToDateTime(DateTime.Now.Hour + ":" + DateTime.Now.Minute);

            Persona destinatario;
            foreach (ListItem item in ddlDestino.Items)
            {
                if (item.Selected)
                {
                    destinatario = new Persona();
                    destinatario.idPersona = Convert.ToInt32(item.Value);
                    objMensaje.ListaDestinatarios.Add(destinatario);
                }
            }
            BLMensaje objBLMensaje = new BLMensaje(objMensaje);
            objBLMensaje.Save();
            
        }
        #endregion
    }
}