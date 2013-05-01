using System;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;
using Promethee.Utility;
using Promethee.Scripts;

namespace Promethee
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (Menu1.SelectedValue == "Salir")
            {
                UIUtility.EliminarArchivosSession(Session.SessionID);
                Session.Abandon();
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        #region --[Métodos Públicos]--
        /// <summary>
        /// Método que permite tratar las excepciones de forma standard. 
        /// </summary>
        /// <param name="ex">Excepción a tratar</param>
        public void ManageExceptions(Exception ex)
        {
            try
            {
                ManageExceptionsLog(ex);
            }
            catch (Exception exNew)
            {
                ManageExceptionsLog(exNew);
            }
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Guarda un log con la informacion recibida por parametro.
        /// </summary>
        /// <param name="exepcionControlada"></param>
        private void ManageExceptionsLog(Exception exepcionControlada)
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
                msgLog.Append(string.Format("Message: {0}", exepcionControlada.Message));
                msgLog.AppendLine();
                if (exepcionControlada.InnerException != null)
                {
                    //InnerException.Message
                    msgLog.Append(string.Format("Message: {0}", exepcionControlada.InnerException.Message));
                    msgLog.AppendLine();
                }
                //Source
                msgLog.Append(string.Format("Source: {0}", exepcionControlada.Source));
                msgLog.AppendLine();
                //StackTrace
                msgLog.Append(string.Format("StackTrace: {0}", exepcionControlada.StackTrace));
                msgLog.AppendLine();

                msgLog.AppendLine();
                msgLog.AppendLine("*********************************************************************************");

                PrometheeLog objLog = new PrometheeLog(oLogPath, true);
                objLog.write(msgLog.ToString());

            }
            catch (Exception) { }
        }
        #endregion
    }
}