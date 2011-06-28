using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_Entities.Security;
using EDUAR_Entities.Shared;
using EDUAR_UI.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_UI
{
    public partial class EDUARMaster : MasterPage
    {
        #region --[Propiedades]--
        public Boolean EsExepcion
        {
            get { return (Boolean)ViewState["esExepcion"]; }
            set { ViewState["esExepcion"] = value; }
        }

        /// <summary>
        /// Mantiene los datos del usuario logueado.
        /// </summary>
        public DTSessionDataUI ObjDTSessionDataUI
        {
            get
            {
                if (Session["SessionObjDTODataPage"] == null)
                    Session["SessionObjDTODataPage"] = new DTSessionDataUI();

                return (DTSessionDataUI)Session["SessionObjDTODataPage"];
            }
            set { Session["SessionObjDTODataPage"] = value; }
        }
        #endregion

        #region --[Eventos]--
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                //Llama a la funcionalidad que redirecciona a la pagina de Login cuando finaliza el tiempo de session
                ((EDUARBasePage)Page).DireccionamientoOnSessionEndScript();

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
                LoginStatus control = ((LoginStatus)Page.Master.FindControl("HeadLoginView").FindControl("HeadLoginStatus"));
                control.LogoutPageUrl = "~/Account/Login.aspx";
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void siteMapPathEDUAR_PreRender(object sender, EventArgs e)
        {
            SiteMapNodeItem sepItem = new SiteMapNodeItem(-1, SiteMapNodeItemType.PathSeparator);
            ITemplate sepTemplate = siteMapPathEDUAR.PathSeparatorTemplate;
            if (sepTemplate == null)
            {
                Literal separator = new Literal { Text = siteMapPathEDUAR.PathSeparator };
                sepItem.Controls.Add(separator);
            }
            else
                sepTemplate.InstantiateIn(sepItem);

            sepItem.ApplyStyle(siteMapPathEDUAR.PathSeparatorStyle);
        }

        /// <summary>
        /// 
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
                    if (node.Url != String.Empty)
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

            foreach (DTRol rolUsuario in ObjDTSessionDataUI.ObjDTUsuario.ListaRoles)
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

                //Esta es una excepcion de tipo validacion que viene de UI.
                if (exceptionName.Contains("CustomizedException") && (((CustomizedException)ex).ExceptionType == enuExceptionType.ValidationException))
                    MostrarMensaje("Error de Validación", ex.Message, enumTipoVentanaInformacion.Advertencia);
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
        public void MostrarMensaje(String titulo, String detalle, enumTipoVentanaInformacion tipoventana)
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
