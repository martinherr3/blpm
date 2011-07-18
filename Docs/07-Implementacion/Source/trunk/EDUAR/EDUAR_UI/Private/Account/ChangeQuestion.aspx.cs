using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities.Security;
using EDUAR_Entities;

namespace EDUAR_UI
{
    public partial class ChangeQuestion : EDUARBasePage
    {
        #region --[Atributos]--
        private BLSeguridad objBLSeguridad;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Propiedad que contiene el objeto seguridad que devuelve la consulta a la Capa de Negocio.
        /// </summary>
        public DTSeguridad propSeguridad
        {
            get
            {
                if (ViewState["propSeguridad"] == null)
                    return null;

                return (DTSeguridad)ViewState["propSeguridad"];
            }
            set { ViewState["propSeguridad"] = value; }
        }

        /// <summary>
        /// Propiedad que contiene el objeto seguridad que devuelve la consulta a la Capa de Negocio.
        /// </summary>
        public Persona propPersona
        {
            get
            {
                if (Session["propPersona"] == null)
                    return null;

                return (Persona)Session["propPersona"];
            }
            set { Session["propPersona"] = value; }
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

            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void btnChangeQuestion_Click(object sender, EventArgs e)
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
    }
}