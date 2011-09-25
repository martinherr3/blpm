using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_SI_DataAccess;
using EDUAR_Utility.Enumeraciones;
using System.Text;

namespace EDUAR_SI_BusinessLogic
{
	public class BLNotificarInasistencia : BLProcesoBase
	{
		#region --[Atributos]--
		/// <summary>
		/// Se utiliza para acceder a la capa de datos
		/// </summary>
		DANotificarInasistencia objDANotificar;
		#endregion

		#region --[Propiedades]--
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor. LLama al constructor de la clase base BLProcesoBase.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos.</param>
		public BLNotificarInasistencia(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Procedimientoes the importar datos.
		/// </summary>
		public void ProcedimientoNotificarInasistencia()
		{
			try
			{
				objDANotificar = new DANotificarInasistencia(ConnectionString);

				var listaInasistencias = objDANotificar.GetInformeInasistencias(enumProcesosAutomaticos.InformeInasistencias.GetHashCode());

				if (listaInasistencias.Count > 0) EnviarEmailsInasistencia(listaInasistencias);
					ProcesosEjecutadosCreate(enumProcesosAutomaticos.InformeInasistencias.GetHashCode(), true);
			}
			catch (Exception ex)
			{
				OnErrorProcess(enumProcesosAutomaticos.InformeInasistencias.GetHashCode(), ex);
				throw ex;
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Importars the datos.
		/// </summary>
		private void EnviarEmailsInasistencia(List<Asistencia> listaAsistencia)
		{
			try
			{
				int idTutorAnterior = 0;
				StringBuilder sbMail = null;
				listaAsistencia.Sort((p, q) => DateTime.Compare(p.fecha, q.fecha));
				DateTime fechaMinima = listaAsistencia[0].fecha;
				foreach (var item in listaAsistencia)
				{
					if (idTutorAnterior != item.unAlumno.listaTutores[0].idPersona)
					{
						if (sbMail != null)
						{
							// cambio el tutor... primero envio el mail al anterior? 
							sbMail.Append("<br />");
							sbMail.AppendLine("EDU@R 2.0 - Educación Argentina del Nuevo Milenio");
							Email.EnviarMail("Inasistencias " + fechaMinima.ToShortDateString() + " al " + DateTime.Now.ToShortDateString(), sbMail.ToString().Replace("\n", "<br />"), true);
						}
						// empiezo a armar un nuevo email
						sbMail = new StringBuilder();
						sbMail.AppendLine("Gracias por utilizar <b>EDU@R 2.0</b>");
						sbMail.Append("<br />");
						sbMail.AppendLine("Buenos días " + item.unAlumno.listaTutores[0].nombre + ",");
						sbMail.Append("<br />");
						sbMail.Append("A continuación se encuentra el detalle de inasistencias y llegadas tarde para sus alumnos ");
						sbMail.Append(" en el periodo ");
						sbMail.Append(fechaMinima.ToShortDateString() + " al " + DateTime.Now.ToShortDateString());
						sbMail.Append("<br /><br />");

						sbMail.AppendLine(("<b>" + item.unAlumno.nombre + " " + item.unAlumno.apellido + "</b> ").PadRight(25, ' '));
						var listaInasistencia = listaAsistencia.FindAll(p => p.unAlumno.idPersona == item.unAlumno.idPersona);
						foreach (var asistencia in listaInasistencia)
						{
							sbMail.AppendLine("Fecha: " + asistencia.fecha.ToShortDateString() + " - Tipo de Inasistencia: " + asistencia.tipoAsistencia.descripcion);
						}

						Email.AgregarDestinatario(item.unAlumno.listaTutores[0].email);
						//Email.AgregarDestinatario("");
					}
					else
					{
						sbMail.AppendLine(("<b>" + item.unAlumno.nombre + " " + item.unAlumno.apellido + "</b> ").PadRight(25, ' '));
						var listaInasistencia = listaAsistencia.FindAll(p => p.unAlumno.idPersona == item.unAlumno.idPersona);
						foreach (var asistencia in listaInasistencia)
						{
							sbMail.AppendLine("Fecha: " + asistencia.fecha.ToShortDateString() + " - Tipo de Inasistencia: " + asistencia.tipoAsistencia.descripcion);
						}
					}
					// Para controlar si sigue siendo el mismo tutor que antes
					idTutorAnterior = item.unAlumno.listaTutores[0].idPersona;
				}
				// envio el último mail despues que sale del for
				if (sbMail != null)
				{
					// cambio el tutor... primero envio el mail al anterior? 
					sbMail.Append("<br />");
					sbMail.AppendLine("EDU@R 2.0 - Educación Argentina del Nuevo Milenio");
					Email.EnviarMail("Inasistencias " + fechaMinima.ToShortDateString() + " al " + DateTime.Now.ToShortDateString(), sbMail.ToString().Replace("\n", "<br />"), true);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}
