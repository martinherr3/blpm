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
		}

		/// <summary>
		/// Contiene información detallada sobre el gráfico que puede generarse
		/// </summary>
		public List<string> TablaGrafico
		{
			get
			{
				if (Session["TablaGrafico"] == null)
					TablaGrafico = new List<string>();
				return (List<string>)Session["TablaGrafico"];
			}
			set
			{
				Session["TablaGrafico"] = value;
			}
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
                    nuevoAcceso.pagina.url = Page.Request.Path;
                    nuevoAcceso.pagina.titulo = Page.Title;
                    nuevoAcceso.fecha = DateTime.Now.Date;
                    nuevoAcceso.hora = DateTime.Now;
                    nuevoAcceso.usuario = "Anonimo";
                    if (string.IsNullOrEmpty(Page.Title))
                    {
                        string[] path = Page.Request.Path.Split('/');
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
                Response.Redirect("~/Error/Error.htm", false);
            }
            catch (Exception) { }
        }
        #endregion
    }
}
