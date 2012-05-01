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
using EDUAR_Utility.Constantes;

namespace EDUAR_UI
{
    public partial class Validate : EDUARBasePage
    {
        #region --[Atributos]--
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
                Master.BotonAvisoAceptar += (VentanaAceptar);

                if (!Page.IsPostBack)
                {
                    propSeguridad = new DTSeguridad();
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
                switch (AccionPagina)
                {

                    case enumAcciones.Redirect:
                        Response.Redirect("~/Public/Account/ForgotPassword.aspx", false);
                        break;
                    default:
                        Response.Redirect("~/Default.aspx", false);
                        break;
                }
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
                objBLPersona.ValidarRegistroPersona();
                if (objBLPersona.Data != null)
                {
                    propPersona = new Persona();
                    propPersona = objBLPersona.Data;
                    // Ya tiene usuario registrado
                    if (objBLPersona.Data.username != string.Empty)
                    {
                        AccionPagina = enumAcciones.Redirect;
                        Master.MostrarMensaje("Ya posee usuario", UIConstantesGenerales.MensajeUsuarioExiste, enumTipoVentanaInformacion.Advertencia);
                    }
                    else if (objBLPersona.Data.activo == false)
                    {
                        AccionPagina = enumAcciones.Salir;
                        Master.MostrarMensaje("Usuario no activo", UIConstantesGenerales.MensajeLoginUsuarioNoActivo, enumTipoVentanaInformacion.Advertencia);
                    }
                    else
                        Response.Redirect("~/Public/Account/Register.aspx", false);
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