using System;
using EDUAR_UI.Shared;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities.Security;

namespace EDUAR_UI
{
    public partial class Default : EDUARBasePage
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
                    propSeguridad = new DTSeguridad();
                    objBLSeguridad = new BLSeguridad(propSeguridad);
                    if (Request.Params["const"] != null)
                    {
                        string user = BLEncriptacion.Decrypt(Request.Params["const"].ToString());
                        ObjDTSessionDataUI.ObjDTUsuario.EsUsuarioInicial = true;
                        ObjDTSessionDataUI.ObjDTUsuario.Nombre = user;
                        propSeguridad.Usuario.Nombre = user;
                        objBLSeguridad = new BLSeguridad(propSeguridad);
                        objBLSeguridad.GetUsuario();
                        //ObjDTSessionDataUI.ObjDTUsuario.Password = objBLSeguridad.Data.Usuario.Password;
                        Response.Redirect("~/Public/Account/ForgotPassword.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}
