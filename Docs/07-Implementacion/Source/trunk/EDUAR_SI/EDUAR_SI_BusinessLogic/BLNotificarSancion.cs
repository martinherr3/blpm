using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_SI_DataAccess;
using EDUAR_Utility.Enumeraciones;
using System.Text;
using System.Linq;

namespace EDUAR_SI_BusinessLogic
{
	public class BLNotificarSancion : BLProcesoBase
	{
		#region --[Atributos]--
		/// <summary>
		/// Se utiliza para acceder a la capa de datos
		/// </summary>
		DANotificarSancion objDANotificar;
		#endregion

		#region --[Propiedades]--
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor. LLama al constructor de la clase base BLProcesoBase.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos.</param>
		public BLNotificarSancion(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Procedimientoes the importar datos.
		/// </summary>
		public void ProcedimientoNotificarSancion()
		{
			try
			{
				objDANotificar = new DANotificarSancion(ConnectionString);

				var listaSanciones = objDANotificar.GetInformeSanciones(enumProcesosAutomaticos.InformeSanciones.GetHashCode());

				if (listaSanciones.Count > 0) EnviarEmailsSanciones(listaSanciones);
				ProcesosEjecutadosCreate(enumProcesosAutomaticos.InformeSanciones.GetHashCode(), true);
			}
			catch (Exception ex)
			{
				OnErrorProcess(enumProcesosAutomaticos.InformeSanciones.GetHashCode(), ex);
				throw ex;
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Importars the datos.
		/// </summary>
		private void EnviarEmailsSanciones(List<Sancion> listaSanciones)
		{
			try
			{
				int idTutorAnterior = 0;
				StringBuilder sbMail = null;
				listaSanciones.Sort((p, q) => DateTime.Compare(p.fecha, q.fecha));
				DateTime fechaMinima = listaSanciones[0].fecha;
				foreach (var item in listaSanciones)
				{
					if (idTutorAnterior != item.alumno.alumno.listaTutores[0].idPersona)
					{
						if (sbMail != null)
						{
							// cambio el tutor... primero envio el mail al anterior? 
							sbMail.Append("<br />");
							sbMail.AppendLine("EDU@R 2.0 - Educación Argentina del Nuevo Milenio");
							Email.EnviarMail("Sanciones " + fechaMinima.ToShortDateString() + " al " + DateTime.Now.ToShortDateString(), sbMail.ToString().Replace("\n", "<br />"), true);
						}
						// empiezo a armar un nuevo email
						sbMail = new StringBuilder();
						sbMail.AppendLine("Gracias por utilizar <b>EDU@R 2.0</b>");
						sbMail.Append("<br />");
						sbMail.AppendLine("Buenos días " + item.alumno.alumno.listaTutores[0].nombre + ",");
						sbMail.Append("<br />");
						sbMail.Append("A continuación se encuentra el detalle de sanciones disciplinarios aplicadas sobre sus alumnos ");
						sbMail.Append(" en el periodo ");
						sbMail.Append(fechaMinima.ToShortDateString() + " al " + DateTime.Now.ToShortDateString());
						sbMail.Append("<br /><br />");

						sbMail.Append(("<b>" + item.alumno.alumno.nombre + " " + item.alumno.alumno.apellido + "</b> ").PadRight(25, ' '));
						sbMail.AppendLine("Fecha: " + item.fecha.ToShortDateString() + " - <u>Motivo</u>: " + item.tipoSancion.nombre + " por " + item.motivoSancion.descripcion + " - <u>Cantidad</u>: " + item.cantidad.ToString());

						Email.AgregarDestinatario(item.alumno.alumno.listaTutores[0].email);
					}
					else
					{
						sbMail.Append(("<b>" + item.alumno.alumno.nombre + " " + item.alumno.alumno.apellido + "</b> ").PadRight(25, ' '));
						sbMail.AppendLine("Fecha: " + item.fecha.ToShortDateString() + " - <u>Motivo</u>: " + item.tipoSancion.nombre + " por " + item.motivoSancion.descripcion + " - <u>Cantidad</u>: " + item.cantidad.ToString());
					}
					// Para controlar si sigue siendo el mismo tutor que antes
					idTutorAnterior = item.alumno.alumno.listaTutores[0].idPersona;
				}
				// envio el último mail despues que sale del for
				if (sbMail != null)
				{
					// cambio el tutor... primero envio el mail al anterior? 
					sbMail.Append("<br />");
					sbMail.AppendLine("EDU@R 2.0 - Educación Argentina del Nuevo Milenio");
					Email.EnviarMail("Sanciones " + fechaMinima.ToShortDateString() + " al " + DateTime.Now.ToShortDateString(), sbMail.ToString().Replace("\n", "<br />"), true);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public class Students
		{
			public string Nombre { get; set; }
			public string Apellido { get; set; }
			public int Edad { get; set; }
		}
		#endregion
	}
}
