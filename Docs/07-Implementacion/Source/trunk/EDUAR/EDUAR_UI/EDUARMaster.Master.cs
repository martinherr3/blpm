using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_Entities.Shared;
using EDUAR_UI.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI
{
    public partial class EDUARMaster : MasterPage, ICallbackEventHandler
    {
        #region --[Propiedades]--
        public Boolean EsExepcion
        {
            get { return (Boolean)ViewState["esExepcion"]; }
            set { ViewState["esExepcion"] = value; }
        }

        private string _callbackResult;

        /// <summary>
        /// Mantiene los datos del usuario logueado.
        /// </summary>
        public DTSessionDataUI ObjSessionDataUI
        {
            get
            {
                if (Session["ObjSessionDataUI"] == null)
                    Session["ObjSessionDataUI"] = new DTSessionDataUI();

                return (DTSessionDataUI)Session["ObjSessionDataUI"];
            }
            set { Session["ObjSessionDataUI"] = value; }
        }
        #endregion

        #region --[Eventos]--
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Llama a la funcionalidad que redirecciona a la pagina de Login cuando finaliza el tiempo de session
                ((EDUARBasePage)Page).DireccionamientoOnSessionEndScript();

                if (ObjSessionDataUI.ObjDTUsuario.Nombre == null && HttpContext.Current.User.Identity.Name != string.Empty)
                    HttpContext.Current.User = null;

                if (HttpContext.Current.User == null)
                    NavigationMenu.DataSource = SiteMapAnonymusEDUAR;
                else
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        NavigationMenu.DataSource = SiteMapEDUAR;

                        StringBuilder s = new StringBuilder();
                        string er;
                        //  configura los llamados a RaiseCallbackEvent y GetCallbackResult
                        er = Page.ClientScript.GetCallbackEventReference(this, "clientTime('')", "putCallbackResult", "null", "clientErrorCallback", true);

                        //  funcion que llama a RaiseCallbackEvent
                        s.Append(" function callServerTask() { ");
                        s.Append((er + ";"));
                        s.Append(" } ");

                        //  inserta el script en la pgina
                        Page.ClientScript.RegisterClientScriptBlock(
                             this.GetType(), "callServerTask", s.ToString(), true);

                        //  NOTA:
                        //  La función callServerTask() es llamada desde la function timerEvent()
                    }
                    else
                    {
                        NavigationMenu.DataSource = SiteMapAnonymusEDUAR;
                    }
                NavigationMenu.DataBind();

                // Ocultar la ventana de información
                ventanaInfoMaster.Visible = false;

                //Suscribe los eventos de la ventana emergente. 
                ventanaInfoMaster.VentanaAceptarClick += (Aceptar);
                ventanaInfoMaster.VentanaCancelarClick += (Cancelar);

                if (!Page.IsPostBack)
                    CargarMenu();
            }
            catch (Exception ex)
            {
                ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Método que cierra la sesión del usuario logueado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HeadLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            try
            {
                UIUtilidades.EliminarArchivosSession(Session.SessionID);
                LoginStatus control = ((LoginStatus)Page.Master.FindControl("HeadLoginView").FindControl("HeadLoginStatus"));
                control.LogoutPageUrl = "~/Public/Account/Login.aspx";
                control.LogoutAction = LogoutAction.RedirectToLoginPage;
                Session.Clear();
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the PreRender event of the siteMapPathEDUAR control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void siteMapPathEDUAR_PreRender(object sender, EventArgs e)
        {
            //SiteMapNodeItem sepItem = new SiteMapNodeItem(-1, SiteMapNodeItemType.PathSeparator);
            //ITemplate sepTemplate = siteMapPathEDUAR.PathSeparatorTemplate;
            //if (sepTemplate == null)
            //{
            //    Literal separator = new Literal { Text = siteMapPathEDUAR.PathSeparator };
            //    sepItem.Controls.Add(separator);
            //}
            //else
            //    sepTemplate.InstantiateIn(sepItem);

            //sepItem.ApplyStyle(siteMapPathEDUAR.PathSeparatorStyle);
        }

        /// <summary>
        /// Valida si el nodo se debe mostrar. 
        /// Puede tener el atributo visible=false o puede que el perfil del usuario lo permita.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected Boolean ValidarNodo(SiteMapNode node)
        {
            //Si el nodo está marcado como visible False es porque solo se utiliza para que sea visible 
            //en el menu superior y no se debe mostrar en el menu lateral
            Boolean isVisible;
            if (bool.TryParse(node["visible"], out isVisible) && !isVisible)
                return false;

            foreach (DTRol rolUsuario in ObjSessionDataUI.ObjDTUsuario.ListaRoles)
            {
                if (node.Roles.Contains(rolUsuario.Nombre))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Click en botón aceptar de ventana de información / confirmación / error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Aceptar(object sender, EventArgs e)
        {
            if (!EsExepcion)
                OnBotonClickAviso(BotonAvisoAceptar, e);
        }

        /// <summary>
        /// Click en botón Cancelar de ventana de información / confirmación / error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Cancelar(object sender, EventArgs e)
        {
            if (!EsExepcion)
                OnBotonClickAviso(BotonAvisoCancelar, e);
        }

        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Método que permite tratar las excepciones de forma standard. 
        /// </summary>
        /// <param name="ex">Excepción a tratar</param>
        public void ManageExceptions(Exception ex)
        {
            try
            {
                string exceptionName = ex.GetType().FullName;
                string Titulo = string.Empty;
                string Detalle = string.Empty;
                enumTipoVentanaInformacion tipoVentana = enumTipoVentanaInformacion.Error;
                Detalle = ex.Message;

                if (exceptionName.Contains("CustomizedException"))
                {
                    switch (((CustomizedException)ex).ExceptionType)
                    {
                        case enuExceptionType.BusinessLogicException:
                            Titulo = "Error en Negocio";
                            Detalle = "Se ha producido un error al realizar una acción en el negocio.";
                            break;
                        case enuExceptionType.SqlException:
                        case enuExceptionType.MySQLException:
                        case enuExceptionType.DataAccesException:
                            Titulo = "Error en Base de Datos";
                            Detalle = "Se ha producido un error al realizar una acción en la Base de Datos.";
                            break;
                        case enuExceptionType.ServicesException:
                            Titulo = "Error en Servicio";
                            Detalle = "Se ha producido un error al realizar la consulta al Servicio.";
                            break;
                        case enuExceptionType.IntegrityDataException:
                            Titulo = "Error de Integridad de Datos";
                            break;
                        case enuExceptionType.ConcurrencyException:
                            Titulo = "Error de Concurrencia";
                            break;
                        case enuExceptionType.ValidationException:
                            //Esta es una excepcion de tipo validacion que viene de UI.
                            Titulo = "Error de Validación";
                            tipoVentana = enumTipoVentanaInformacion.Advertencia;
                            //MostrarMensaje("Error de Validación", ex.Message, enumTipoVentanaInformacion.Advertencia);
                            break;
                        case enuExceptionType.SecurityException:
                            Titulo = "Error de seguridad";
                            tipoVentana = enumTipoVentanaInformacion.Advertencia;
                            break;
                        case enuExceptionType.WorkFlowException:
                            break;
                        case enuExceptionType.Exception:
                            Titulo = "Error en la Aplicación";
                            Detalle = "Se ha producido un error interno en la aplicación.";
                            break;
                        default:
                            break;
                    }
                    if (Detalle != ex.Message) Detalle += " " + ex.Message;
                    MostrarMensaje(Titulo, Detalle, tipoVentana);
                    if (tipoVentana != enumTipoVentanaInformacion.Advertencia)
                        ManageExceptionsLog(ex);
                }
                //Esta es una excepcion de tipo validacion que viene de BL.
                else if ((exceptionName.Contains("GenericException")))
                {
                    ///GenericException genericEx = ((GenericException)ex).Detail;
                    if (((CustomizedException)ex).ExceptionType == enuExceptionType.ValidationException)
                        MostrarMensaje("Error de Validación", ex.Message, enumTipoVentanaInformacion.Advertencia);
                    else
                        ventanaInfoMaster.GestionExcepciones(ex);
                }
                else
                    ventanaInfoMaster.GestionExcepciones(ex);

                // Refrescar updatepanel
                updVentaneMensajes.Update();
                EsExepcion = true;
            }
            catch (Exception exNew)
            {
                ventanaInfoMaster.GestionExcepciones(exNew);
            }
        }

        /// <summary>
        /// Método que guardar un log
        /// </summary>
        /// <param name="exepcionControlada">Excepcion que se va a guardar</param>
        public void ManageExceptionsLog(Exception exepcionControlada)
        {
            try
            {
                ventanaInfoMaster.GestionExcepcionesLog(exepcionControlada);
            }
            catch (Exception ex)
            {
                ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Metodo que se encarga de mostrar mensajes en la aplicacion.
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="detalle"></param>
        /// <param name="tipoventana"></param>
        public void MostrarMensaje(string titulo, string detalle, enumTipoVentanaInformacion tipoventana)
        {
            try
            {
                EsExepcion = false;
                ventanaInfoMaster.TipoVentana = tipoventana;
                ventanaInfoMaster.Titulo = titulo;
                ventanaInfoMaster.Detalle = detalle;
                ventanaInfoMaster.MostrarMensaje();

                // Refrescar updatepanel
                updVentaneMensajes.Update();

            }
            catch (Exception ex)
            {
                ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Metodo que se encarga de ocultar los mensajes en la aplicacion.
        /// </summary>
        public void OcultarMensaje()
        {
            try
            {
                ventanaInfoMaster.OcultarMensaje();
                // Refrescar updatepanel
                updVentaneMensajes.Update();

            }
            catch (Exception ex)
            {
                ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Procesa un evento de devolución de llamada que tiene como destino un control.
        /// </summary>
        /// <param name="eventArgument">Cadena que representa un argumento del evento que se pasará al controlador de eventos.</param>
        public void RaiseCallbackEvent(string eventArgument)
        {
            StringWriter sr = new StringWriter();
            HtmlTextWriter htm = new HtmlTextWriter(sr);

            if (HttpContext.Current.User != null)
            {
                BLMensaje objBLMensaje = new BLMensaje();
                List<Mensaje> objMensajes = new List<Mensaje>();

                objMensajes = objBLMensaje.GetMensajes(new Mensaje() { destinatario = new Persona() { username = ObjSessionDataUI.ObjDTUsuario.Nombre }, activo = true });
                objMensajes = objMensajes.FindAll(p => p.leido == false);
                btnMail.Visible = true;
                if (objMensajes.Count > 0)
                {
                    btnMail.ImageUrl = "/EDUAR_UI/Images/mail-unread.png";
                    btnMail.AlternateText = "Nuevo Mensaje!";
                    btnMail.ToolTip = "Nuevo Mensaje!";
                }
                else
                {
                    btnMail.ImageUrl = "/EDUAR_UI/Images/mail-inbox.png";
                    btnMail.AlternateText = "Mensajes";
                    btnMail.ToolTip = "Mensaje";
                }
                lnkMensajes.HRef = "Private/Mensajes/MsjeEntrada.aspx";
                lnkMensajes.RenderControl(htm);
                htm.Flush();
            }
            else
            {
                btnMail.ImageUrl = "";
                btnMail.Visible = false;
                btnMail.RenderControl(htm);
                htm.Flush();
            }
            _callbackResult = sr.ToString();
        }

        /// <summary>
        /// Devuelve los resultados de un evento de devolución de llamada que tiene como destino un control.
        /// </summary>
        /// <returns>
        /// Resultado de la devolución de llamada.
        /// </returns>
        public string GetCallbackResult()
        {
            //  ésta variable es pasada al argumento de putCallbackResult
            return _callbackResult;
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the menu.
        /// </summary>
        private void CargarMenu()
        {
            if (SiteMapEDUAR.Provider.RootNode != null)
            {
                foreach (SiteMapNode node in SiteMapEDUAR.Provider.RootNode.ChildNodes)
                {
                    if (!ValidarNodo(node))
                        continue;
                    trvMenu.Visible = true;
                    TreeNode objTreeNode = new TreeNode(node.Title);
                    if (node.Url != string.Empty)
                        objTreeNode.NavigateUrl = node.Url;

                    objTreeNode.SelectAction = TreeNodeSelectAction.Expand;
                    //Recorre los nodos hijos
                    foreach (SiteMapNode nodeChild in node.ChildNodes)
                    {
                        if (!ValidarNodo(nodeChild))
                            continue;

                        TreeNode objTreeNodeChild = new TreeNode(nodeChild.Title) { NavigateUrl = nodeChild.Url };
                        objTreeNode.ChildNodes.Add(objTreeNodeChild);
                    }
                    trvMenu.Nodes.Add(objTreeNode);
                }
                trvMenu.ExpandAll();
            }
        }

        #endregion

        #region --[Delegados]--
        /// <summary>
        /// Delegado para capturar el evento de click sobre aceptar / cancelar en ventana de aviso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MasterPageAvisoClickHandler(object sender, EventArgs e);

        /// <summary>
        /// Evento click sobre botón aceptar de ventana de aviso
        /// </summary>
        public event MasterPageAvisoClickHandler BotonAvisoAceptar;

        /// <summary>
        /// Evento click sobre botón cancelar de ventana de aviso
        /// </summary>
        public event MasterPageAvisoClickHandler BotonAvisoCancelar;

        /// <summary>
        /// Invoca los delegados al evento de click al botón, para que los eventos de
        /// aceptar / cancelar puedan ser controlados desde las páginas "hijas" al masterpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Argumentos del evento</param>
        protected virtual void OnBotonClickAviso(MasterPageAvisoClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca los delegados
                sender(this, e);
            }
        }

        #endregion
    }
}
