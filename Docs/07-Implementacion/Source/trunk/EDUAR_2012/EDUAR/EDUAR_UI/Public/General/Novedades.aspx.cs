﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;

namespace EDUAR_UI
{
    public partial class Novedades : EDUARBasePage
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
            try
            {
                Master.BotonAvisoAceptar += (VentanaAceptar);

                if (!Page.IsPostBack)
                {
                    //propSeguridad = new DTSeguridad();

                    CargarNovedades();
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
			try
			{

			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
        #endregion

        #region --[Métodos Privados]--
		/// <summary>
		/// Cargars the novedades.
		/// </summary>
        private void CargarNovedades()
        {
            BLEventoInstitucional objBLEventoInstitucional = new BLEventoInstitucional();
            EventoInstitucional example = new EventoInstitucional();
            example.activo = true;
            List<EventoInstitucional> listEventoInstitucional = new List<EventoInstitucional>();
            listEventoInstitucional = objBLEventoInstitucional.GetEventoInstitucional(example);

            foreach (EventoInstitucional evento in listEventoInstitucional)
            {
                udpNovedades.ContentTemplateContainer.Controls.Add(new LiteralControl("<h3>" + evento.titulo.ToUpper() + " - " + evento.fecha.ToShortDateString() + "</h3>"));
                //udpNovedades.ContentTemplateContainer.Controls.Add(new LiteralControl());
                udpNovedades.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + evento.lugar + "</p>"));
                udpNovedades.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + evento.detalle + "</p><hr />"));

                //Label lblTitulo = new Label();
                //lblTitulo.ID = "evento_" + evento.idEventoInstitucional;
                //lblTitulo.Text = evento.descripcionBreve;

                //udpNovedades.ContentTemplateContainer.Controls.Add(lblTitulo);
            }
            udpNovedades.Update();
        }

        #endregion
    }

}