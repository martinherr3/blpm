using System;
using System.Web.UI;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Utilidades;
using System.Text;
using System.Web.Security;
using System.Configuration;

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
        public String AvisoTexto
        {
            get { return ViewState["AvisoTexto"] == null ? String.Empty : ViewState["AvisoTexto"].ToString(); }
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
            get { return (enumAcciones)ViewState["AccionPagina"]; }
            set { ViewState["AccionPagina"] = value; }
        }

        /// <summary>
        /// Contiene la Master que se usa en paginas que no tienen una Master Page asociada.
        /// </summary>
        public EDUARMaster MasterBase { get; set; }

        #endregion

        #region --[Eventos]--

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            try
            {
                base.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

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
            loginPage += strDefaultPage;

            if (Session != null)
            {
                int sessionTimeOut = Session.Timeout;
                int redirectTimeOut = (sessionTimeOut * 60000) - 10;

                StringBuilder javascript = new StringBuilder();
                javascript.Append("var redirectTimeout;");
                javascript.Append("clearTimeout(redirectTimeout);");
                javascript.Append(String.Format("setTimeout(\"window.location='{0}'\",{1});", loginPage, redirectTimeOut));
                /// Register JavaScript Code on WebPage        
                page.ClientScript.RegisterStartupScript(page.GetType(), "RegisterRedirectOnSessionEndScript", javascript.ToString(), true);
            }
        }

        /// <summary>
        /// Loguea un mensaje Particular
        /// </summary>
        /// <param name="mensaje"></param>
        public void LogMensaje(String mensaje)
        {
            Boolean oLogActivo = Boolean.Parse(ConfigurationManager.AppSettings["oLogActivo"]);

            if (!oLogActivo)
                return;

            String logPath = ConfigurationManager.AppSettings["oLogPath"];
            String logNombre = ConfigurationManager.AppSettings["oLogNombre"];

            //Crea el directorio.
            if (!System.IO.Directory.Exists(logPath))
                System.IO.Directory.CreateDirectory(logPath);

            String oLogPath = String.Format("{0}\\{1}", logPath, logNombre);

            //Crea el archivo.
            if (!System.IO.File.Exists(oLogPath))
                System.IO.File.CreateText(oLogPath);

            StringBuilder msgLog = new StringBuilder();
            msgLog.Append(String.Format("Message: {0} -  {1}", DateTime.Now, mensaje));
            msgLog.AppendLine();

            EDUARLog objLog = new EDUARLog(oLogPath, true);
            objLog.write(msgLog.ToString());
        }
    }
}
