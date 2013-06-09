using System;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System.Data;
using System.Collections.Generic;

namespace EDUAR_SI_DataAccess
{
	public class DANotificarInasistencia : DABase
	{
		#region --[Atributos]--
		private const string ClassName = "DANotificarInasistencia";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor. LLama al constructor de la clase base DABase.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos</param>
		public DANotificarInasistencia(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the informe inasistencias.
		/// </summary>
		/// <param name="idProcesoAutomatico">The id proceso automatico.</param>
		/// <returns></returns>
		public List<Asistencia> GetInformeInasistencias(int idProcesoAutomatico)
		{
			var listaAsistencia = new List<Asistencia>();
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					sqlConnectionConfig.Open();

					command.Connection = sqlConnectionConfig;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "AsistenciaInformeMail_Select";
					command.CommandTimeout = 10;

					command.Parameters.AddWithValue("@idProcesoAutomatico", idProcesoAutomatico);

					SqlDataReader reader = command.ExecuteReader();
					Asistencia objAsistencia = null;
					while (reader.Read())
					{
						objAsistencia = new Asistencia();
						objAsistencia.fecha = Convert.ToDateTime(reader["fecha"]);
						objAsistencia.alumno.alumno.nombre = reader["nombreAlumno"].ToString();
						objAsistencia.alumno.alumno.apellido = reader["apellidoAlumno"].ToString();
						objAsistencia.alumno.alumno.idPersona = Convert.ToInt32(reader["idPersonaAlumno"]);

						objAsistencia.alumno.alumno.listaTutores.Add(new Tutor
						{
							nombre = reader["nombreTutor"].ToString(),
							apellido = reader["nombreTutor"].ToString(),
							email = reader["email"].ToString(),
							idPersona = Convert.ToInt32(reader["idPersonaTutor"])
						});
						objAsistencia.tipoAsistencia.descripcion = reader["descripcion"].ToString();

						listaAsistencia.Add(objAsistencia);
					}
					return listaAsistencia;
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GetInformeInasistencias()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GetInformeInasistencias()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion

	}
}
