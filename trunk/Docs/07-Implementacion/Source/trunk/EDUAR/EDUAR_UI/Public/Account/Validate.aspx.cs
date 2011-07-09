using System;
using System.Collections.Generic;
using System.Web.UI;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
    public partial class Validate : EDUARBasePage
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
                    //if (ObjDTSessionDataUI.ObjDTUsuario.EsUsuarioInicial == true)
                    //{
                    //    ObjDTSessionDataUI.ObjDTUsuario.EsUsuarioInicial = false;
                    //    CargarDatosUsuario();
                    //    CargarPresentacionRecover();
                    //}
                    //else
                    //{
                    //    udpEmail.Visible = true;
                    //    udpRecover.Visible = false;
                    //}
                    //udpForgotPassword.Update();
                    CargarPresentacionValidacion();
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
                Response.Redirect("~/Default.aspx", false);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnValidar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                Persona persona = new Persona();
                persona.idTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                persona.numeroDocumento = Convert.ToInt32(txtNroDocumento.Text);
                persona.fechaNacimiento = Convert.ToDateTime(calFechaNacimiento.Fecha.Text);
                BLPersona objBLPersona = new BLPersona(persona);
                objBLPersona.GetPersonaByEntidad();
                if (!(objBLPersona.Data == null))
                {
                    int id = objBLPersona.Data.idPersona;
                }

                else
                {
                    string error = "El {0} número {1} no se encuentra registrado. <br />Por favor, póngase en contacto con el administrador del sistema.";
                    Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), string.Format(error, ddlTipoDocumento.SelectedItem.Text, txtNroDocumento.Text), enumTipoVentanaInformacion.Advertencia);
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the presentacion recover.
        /// </summary>
        private void CargarPresentacionValidacion()
        {
            CargarComboTipoDocumento();
            udpDatosValidar.Visible = true;
            udpValidate.Update();
            //udpEmail.Visible = false;
            //udpRecover.Visible = true;
            //udpNewPassword.Visible = false;
            //udpRecover.Update();
        }

        /// <summary>
        /// Cargars the presentacion nueva password.
        /// </summary>
        private void CargarPresentacionNuevaPassword()
        {
            //udpEmail.Visible = false;
            //udpRecover.Visible = false;
            //udpNewPassword.Visible = true;
            //udpForgotPassword.Update();
        }

        /// <summary>
        /// Cargars the datos usuario.
        /// </summary>
        private void CargarDatosUsuario()
        {
            //propSeguridad.Usuario.Email = txtEmail.Text.Trim();

            //propSeguridad.Usuario = ObjDTSessionDataUI.ObjDTUsuario;
            //objBLSeguridad = new BLSeguridad(propSeguridad);
            //objBLSeguridad.GetUsuario();
            //ObjDTSessionDataUI.ObjDTUsuario.PaswordPregunta = objBLSeguridad.Data.Usuario.PaswordPregunta;
            //lblPregunta.Text = ObjDTSessionDataUI.ObjDTUsuario.PaswordPregunta;
        }

        /// <summary>
        /// Cargars the combo tipo documento.
        /// </summary>
        private void CargarComboTipoDocumento()
        {
            BLTipoDocumento objBLTipoDocumento = new BLTipoDocumento();
            List<TipoDocumento> lista = new List<TipoDocumento>();
            lista = objBLTipoDocumento.GetTipoDocumento(new TipoDocumento());
            UIUtilidades.BindCombo<TipoDocumento>(ddlTipoDocumento, lista, "idTipoDocumento", "descripcion", false);
        }

        #endregion
    }
}