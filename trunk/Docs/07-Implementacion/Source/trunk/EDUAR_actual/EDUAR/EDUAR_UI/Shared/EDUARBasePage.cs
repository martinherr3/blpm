using System;
using System.Configuration;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using EDUAR_Entities;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Utilidades;
using EDUAR_BusinessLogic.Common;
using System.Collections.Generic;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI.Shared
{
    public class EDUARBasePage : Page
    {
        #region --[Miembros]--

        #endregion

        #region --[Propiedades]--

        /// <summary>
        /// Indica si se mostrará o no el mensaje emergente de aviso.
        /// Se utiliza para gestionar los mensajes en el preRender
        /// </summary>
        public Boolean AvisoMostrar
        {
            get { return ViewState["AvisoMostrar"] != null && Boolean.Parse(ViewState["AvisoMostrar"].ToString()); }
            set { ViewState["AvisoMostrar"] = value; }
        }

        /// <summary>
        /// Indica si se mostrará o no el mensaje emergente de aviso.
        /// Se utiliza para gestionar los mensajes en el preRender
        /// </summary>
        public Boolean MensajeMostrar
        {
            get { return ViewState["MensajeMostrar"] != null && Boolean.Parse(ViewState["MensajeMostrar"].ToString()); }
            set { ViewState["MensajeMostrar"] = value; }
        }

        /// <summary>
        /// Indica el texto que se mostrara.
        /// Se utiliza para gestionar los mensajes en el preRender.
        /// </summary>
        public string AvisoTexto
        {
            get { return ViewState["AvisoTexto"] == null ? string.Empty : ViewState["AvisoTexto"].ToString(); }
            set { ViewState["AvisoTexto"] = value; }
        }

        /// <summary>
        /// Indica el texto que se mostrara.
        /// Se utiliza para gestionar los mensajes en el preRender.
        /// </summary>
        public Exception AvisoExcepcion
        {
            get { return ViewState["ExcepcionLoad"] == null ? null : (Exception)ViewState["ExcepcionLoad"]; }
            set { ViewState["ExcepcionLoad"] = value; }
        }

        /// <summary>
        /// Indica si se mostrará o no el mensaje emergente de aviso.
        /// Se utiliza para gestionar los mensajes en el preRender
        /// </summary>
        public enumAcciones AccionPagina
        {
            get
            {
                if (ViewState["AccionPagina"] == null)
                    AccionPagina = enumAcciones.Ingresar;
                return (enumAcciones)ViewState["AccionPagina"];
            }
            set { ViewState["AccionPagina"] = value; }
        }

        /// <summary>
        /// Indica si un registro se está generando o modificando.
        /// </summary>
        public bool esNuevo
        {
            get
            {
                if (ViewState["esNuevo"] == null)
                    esNuevo = true;
                return (bool)ViewState["esNuevo"];
            }
            set { ViewState["esNuevo"] = value; }
        }

        /// <summary>
        /// Contiene la Master que se usa en paginas que no tienen una Master Page asociada.
        /// </summary>
        public EDUARMaster MasterBase { get; set; }


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

        /// <summary>
        /// Nombre del gráfico que se genera en el servidor para la session
        /// </summary>
        public string nombrePNG
        {
            get
            {
                return Session["nombrePNG"].ToString();
            }
            set
            {
                Session["nombrePNG"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the ciclo lectivo actual.
        /// </summary>
        /// <value>
        /// The ciclo lectivo actual.
        /// </value>
        public CicloLectivo cicloLectivoActual
        {
            get
            {
                if (ViewState["cicloLectivoActual"] == null)
                {
                    BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
                    cicloLectivoActual = objBLCicloLectivo.GetCicloLectivoActual();
                }
                return (CicloLectivo)ViewState["cicloLectivoActual"];
            }
            set { ViewState["cicloLectivoActual"] = value; }
        }

        /// <summary>
        /// Gets or sets the tabla propia grafico.
        /// </summary>
        /// <value>
        /// The tabla propia grafico.
        /// </value>
        public List<TablaGrafico> TablaGrafico
        {
            get
            {
                if (Session["TablaGrafico"] == null)
                    TablaGrafico = new List<TablaGrafico>();
                return (List<TablaGrafico>)Session["TablaGrafico"];
            }
            set
            {
                Session["TablaGrafico"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the id novedad consulta.
        /// </summary>
        /// <value>
        /// The id novedad consulta.
        /// </value>
        public int idNovedadConsulta
        {
            get
            {
                if (Session["idNovedadConsulta"] == null)
                    Session["idNovedadConsulta"] = 0;
                return (int)Session["idNovedadConsulta"];
            }
            set
            {
                Session["idNovedadConsulta"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the id id Curso Ciclo Lectivo.
        /// </summary>
        /// <value>
        /// The id curso Ciclo Lectivo.
        /// </value>
        public int idCursoCicloLectivo
        {
            get
            {
                if (Session["idCursoCicloLectivo"] == null)
                    Session["idCursoCicloLectivo"] = 0;
                return (int)Session["idCursoCicloLectivo"];
            }
            set
            {
                if (value > 0)
                {
                    BLCurso objBLCurso = new BLCurso();
                    cursoActual.idCursoCicloLectivo = value;
                    cursoActual = objBLCurso.GetCursoCicloLectivo(cursoActual);
                }
                Session["idCursoCicloLectivo"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the curso actual.
        /// </summary>
        /// <value>
        /// The curso actual.
        /// </value>
        public CursoCicloLectivo cursoActual
        {
            get
            {
                if (Session["cursoActual"] == null)
                    Session["cursoActual"] = new CursoCicloLectivo();
                return (CursoCicloLectivo)Session["cursoActual"];
            }
            set { Session["cursoActual"] = value; }
        }

        /// <summary>
        /// Gets or sets the id asignatura.
        /// </summary>
        /// <value>
        /// The id asignatura.
        /// </value>
        public int idAsignatura
        {
            get
            {
                if (Session["idAsignatura"] == null)
                    idAsignatura = 0;
                return (int)Session["idAsignatura"];
            }
            set { Session["idAsignatura"] = value; }
        }
        #endregion

        #region --[Eventos]--
        /// <summary>
        /// Provoca el evento <see cref="E:System.Web.UI.Page.LoadComplete"/> al final de la fase de carga de la página.
        /// </summary>
        /// <param name="e"><see cref="T:System.EventArgs"/> que contiene los datos del evento.</param>
        protected override void OnLoadComplete(EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    base.OnLoadComplete(e);
                    Acceso nuevoAcceso = new Acceso();
                    string ruta = Page.Request.Path;
                    if (ruta.EndsWith("/"))
                        ruta = ruta.Substring(0, ruta.Length - 1);
                    nuevoAcceso.pagina.url = ruta;
                    nuevoAcceso.pagina.titulo = Page.Title;
                    nuevoAcceso.fecha = DateTime.Now.Date;
                    nuevoAcceso.hora = DateTime.Now;
                    nuevoAcceso.usuario = "Anonimo";
                    if (string.IsNullOrEmpty(Page.Title))
                    {
                        string[] path = ruta.Split('/');
                        string[] file = path[path.Length - 1].Split('.');
                        string title = file[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(ObjSessionDataUI.ObjDTUsuario.Nombre))
                    {
                        nuevoAcceso.usuario = ObjSessionDataUI.ObjDTUsuario.Nombre;
                    }
                    BLAcceso objBLAcceso = new BLAcceso(nuevoAcceso);
                    objBLAcceso.Save();
                }
            }
            catch (Exception ex)
            {
                //throw;
                LogMensaje(ex);
            }
        }

        /// <summary>
        /// Habilita un control de servidor para que realice la limpieza final antes de que se libere de la memoria.
        /// </summary>
        public override void Dispose()
        {
            try
            {
                base.Dispose();
            }
            catch (Exception ex)
            {
                //throw;
                LogMensaje(ex);
            }
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Método que redirecciona a la pagina de loguin
        /// </summary>
        public void DireccionamientoOnSessionEndScript()
        {
            Page page = this;

            string strDefaultPage = FormsAuthentication.DefaultUrl;
            if (strDefaultPage.StartsWith("~"))
                strDefaultPage.Remove(0, 1);

            string strScheme = Request.Url.Scheme;
            string strPort = Request.Url.Port.ToString();

            string loginPage = "";
            if (strScheme != "")
                loginPage += strScheme + "://";
            loginPage += Request.Url.Host;
            if (strPort != "")
                loginPage += ":" + strPort;
            //loginPage += strDefaultPage;
            loginPage += "/EDUAR_UI/Login.aspx";

            ObjSessionDataUI.urlDefault = new Uri(loginPage);

            if (Session != null)
            {
                int sessionTimeOut = Session.Timeout;
                int redirectTimeOut = (sessionTimeOut * 60000) - 10;

                StringBuilder javascript = new StringBuilder();
                javascript.Append("var redirectTimeout;");
                javascript.Append("clearTimeout(redirectTimeout);");
                javascript.Append(string.Format("setTimeout(\"window.location='{0}'\",{1});", loginPage, redirectTimeOut));
                /// Register JavaScript Code on WebPage        
                page.ClientScript.RegisterStartupScript(page.GetType(), "RegisterRedirectOnSessionEndScript", javascript.ToString(), true);
            }
        }

        /// <summary>
        /// Loguea un mensaje Particular
        /// </summary>
        /// <param name="mensaje">The mensaje.</param>
        public void LogMensaje(Exception excepcionNoControlada)
        {
            try
            {
                #region Manejo de Directorio de LOG

                Boolean oLogActivo = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["oLogActivo"]);

                if (!oLogActivo)
                    return;

                string logPath = System.Configuration.ConfigurationManager.AppSettings["oLogPath"];
                string logNombre = System.Configuration.ConfigurationManager.AppSettings["oLogNombre"];

                //Crea el directorio.
                if (!System.IO.Directory.Exists(logPath))
                    System.IO.Directory.CreateDirectory(logPath);

                string oLogPath = string.Format("{0}\\{1}", logPath, logNombre);

                //Crea el archivo.
                if (!System.IO.File.Exists(oLogPath))
                    System.IO.File.CreateText(oLogPath);

                #endregion

                //Log
                StringBuilder msgLog = new StringBuilder();
                msgLog.AppendLine("*********************************************************************************");
                msgLog.AppendFormat("{0} - {1}", DateTime.Now, enuExceptionType.Exception);
                //Message
                msgLog.Append(string.Format("Message: {0}", excepcionNoControlada.Message));
                msgLog.AppendLine();
                //Source
                msgLog.Append(string.Format("Source: {0}", excepcionNoControlada.Source));
                msgLog.AppendLine();
                //StackTrace
                msgLog.Append(string.Format("StackTrace: {0}", excepcionNoControlada.StackTrace));
                msgLog.AppendLine();

                msgLog.AppendLine();
                msgLog.AppendLine("*********************************************************************************");

                EDUARLog objLog = new EDUARLog(oLogPath, true);
                objLog.write(msgLog.ToString());
                //Response.Redirect("~/Error.aspx", false);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Truncates the string.
        /// </summary>
        /// <param name="strText">The STR text.</param>
        /// <returns></returns>
        protected string TruncateString(string strText)
        {
            if (strText.Length > 60)
                return strText.Substring(0, 60) + " ...";
            return strText;
        }

        /// <summary>
        /// Mensajeses the enviados.
        /// </summary>
        public void MensajesEnviados()
        {
            Response.Redirect("~/Private/Mensajes/MsjeEnviado.aspx", false);
        }

        /// <summary>
        /// Mensajeses the recibidos.
        /// </summary>
        public void MensajesRecibidos()
        {
            Response.Redirect("~/Private/Mensajes/MsjeEntrada.aspx", false);
        }

        /// <summary>
        /// Mensajeses the nuevo.
        /// </summary>
        public void MensajesNuevo()
        {
            Response.Redirect("~/Private/Mensajes/MsjeRedactar.aspx", false);
        }

        #region --[Ordenamiento Grillas]--
        protected string SortExpressionInf
        {
            get { return (ViewState["SortExpressionInf"] == null ? string.Empty : ViewState["SortExpressionInf"].ToString()); }
            set { ViewState["SortExpressionInf"] = value; }
        }

        protected string SortDirectionInf
        {
            get { return (ViewState["SortDirectionInf"] == null ? string.Empty : ViewState["SortDirectionInf"].ToString()); }
            set { ViewState["SortDirectionInf"] = value; }
        }

        protected string GetSortDirection(string sortExpression)
        {
            if (SortExpressionInf == sortExpression)
            {
                if (SortDirectionInf == "ASC")
                    SortDirectionInf = "DESC";
                else if (SortDirectionInf == "DESC")
                    SortDirectionInf = "ASC";
                return SortDirectionInf;
            }
            else
            {
                SortExpressionInf = sortExpression;
                SortDirectionInf = "ASC";
                return SortDirectionInf;
            }
        }
        #endregion
        #endregion

        internal void LimpiarVariablesSession()
        {
            AccionPagina = enumAcciones.Limpiar;
            nombrePNG = string.Empty;
            TablaGrafico = new List<TablaGrafico>();
            idNovedadConsulta = 0;
            idCursoCicloLectivo = 0;
            cursoActual = new CursoCicloLectivo();
            idAsignatura = 0;
        }
    }
}
