using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;

namespace EDUAR_UI.WebServices
{
	/// <summary>
	/// Descripción breve de SrvDestinatario
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
	[System.Web.Script.Services.ScriptService]
	public class SrvDestinatario : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			BLPersona objBLPersona = new BLPersona();

			return "hola";
		}

		[WebMethod]
        public List<Persona> obtenerDestinatarios(string prefixText, int limite)
		{
			return new BLPersona().GetPersonas(new Persona() { apellido = prefixText });
		}
	}
}
