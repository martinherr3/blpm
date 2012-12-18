using System;
using System.IO;
using EDUAR_UI.Utilidades;
using System.Web;

namespace EDUAR_UI
{
	public class Global : System.Web.HttpApplication
	{

		void Application_Start(object sender, EventArgs e)
		{
			// Código que se ejecuta al iniciarse la aplicación

		}

		void Application_End(object sender, EventArgs e)
		{
			//  Código que se ejecuta cuando se cierra la aplicación

		}

		void Application_Error(object sender, EventArgs e)
		{
			// Código que se ejecuta al producirse un error no controlado
			//string error = "Se produjo un error: \n" +
			//if (Server.GetLastError() != null)
			//{
			//    Application["CurrentError"] = Server.GetLastError().Message.ToString();
			//    Application["CurrentErrorDetalle"] = Server.GetLastError().ToString();
			//}
			////Response.Redirect("~/Error.aspx", false);
			//Server.Transfer("~/Error.aspx");
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			switch (Request.Url.Scheme)
			{
				case "https":
					if (Request.Url.PathAndQuery.Contains("Public") || Request.Url.PathAndQuery.Contains("Default") ||
						Request.Url.PathAndQuery.Contains("About") || Request.Url.PathAndQuery.Contains("Error"))
					{
						var path = "http://" + Request.Url.Host + Request.Url.PathAndQuery;
						Response.Status = "301 Moved Permanently";
						Response.AddHeader("Location", path);
					}
					else
						Response.AddHeader("Strict-Transport-Security", "max-age=300");
					break;
				case "http":
					if (Request.Url.PathAndQuery.Contains("Login") || Request.Url.PathAndQuery.Contains("Private"))
					{
						var path = "https://" + Request.Url.Host + Request.Url.PathAndQuery;
						Response.Status = "301 Moved Permanently";
						Response.AddHeader("Location", path);
					}
					break;
			}
		}

		void Session_Start(object sender, EventArgs e)
		{
			// Código que se ejecuta cuando se inicia una nueva sesión

		}

		void Session_End(object sender, EventArgs e)
		{
			// Código que se ejecuta cuando finaliza una sesión.
			UIUtilidades.EliminarArchivosSession(Session.SessionID);

			// Nota: el evento Session_End se desencadena sólo cuando el modo sessionstate
			// se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
			// o SQLServer, el evento no se genera.
			Session.Abandon();
		}
	}
}
