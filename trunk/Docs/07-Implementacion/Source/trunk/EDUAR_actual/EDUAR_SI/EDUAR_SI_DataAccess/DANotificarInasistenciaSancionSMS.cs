using System;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System.Data;
using System.Collections.Generic;

namespace EDUAR_SI_DataAccess
{
	public class DANotificarInasistenciaSancionSMS : DABase
	{
		#region --[Atributos]--
		private const string ClassName = "DANotificarInasistenciaSancionSMS";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor. LLama al constructor de la clase base DABase.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos</param>
		public DANotificarInasistenciaSancionSMS(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the informe InasistenciasSancionSMS.
		/// </summary>
		/// <param name="idProcesoAutomatico">The id proceso automatico.</param>
		/// <returns></returns>
		public List<Asistencia> GetInformeInasistenciasSMS(int idProcesoAutomatico)
		{
			var listaAsistencia = new List<Asistencia>();
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					sqlConnectionConfig.Open();

					command.Connection = sqlConnectionConfig;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "AsistenciaInformeSMS_Select";
                    //command.CommandText = "AsistenciaInformeMail_Select"; // Como no hay cargadas inasistencias en la ultima semana 
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
							apellido = reader["apellidoTutor"].ToString(),
                            telefonoCelular = reader["telefonoCelular"].ToString(),
							idPersona = Convert.ToInt32(reader["idPersonaTutor"])
						});
						objAsistencia.tipoAsistencia.descripcion = reader["descripcion"].ToString();

						listaAsistencia.Add(objAsistencia);
					}
                    sqlConnectionConfig.Close();
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


        /// <summary>
        /// Gets the informe sanciones. Retorna las sanciones de la ultima semana.
        /// </summary>
        /// <param name="idProcesoAutomatico">The id proceso automatico.</param>
        /// <returns></returns>
        public List<Sancion> GetInformeSancionesSMS(int idProcesoAutomatico)
        {
            var listaSanciones = new List<Sancion>();
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "SancionInformeSMS_Select"; // Como no hay sanciones cargadas en la ultima semana, usamos el del mail para probar
                   // command.CommandText = "SancionInformeMail_Select";
                    command.CommandTimeout = 10;

                    command.Parameters.AddWithValue("@idProcesoAutomatico", idProcesoAutomatico);

                    SqlDataReader reader = command.ExecuteReader();
                    Sancion objSancion = null;
                    while (reader.Read())
                    {
                        objSancion = new Sancion();
                        objSancion.fecha = Convert.ToDateTime(reader["fecha"]);
                        objSancion.alumno.alumno.nombre = reader["nombreAlumno"].ToString();
                        objSancion.alumno.alumno.apellido = reader["apellidoAlumno"].ToString();
                        objSancion.alumno.alumno.idPersona = Convert.ToInt32(reader["idPersonaAlumno"]);

                        objSancion.alumno.alumno.listaTutores.Add(new Tutor
                        {
                            nombre = reader["nombreTutor"].ToString(),
                            apellido = reader["apellidoTutor"].ToString(),
                            telefonoCelular = reader["email"].ToString(),
                            idPersona = Convert.ToInt32(reader["idPersonaTutor"])
                        });
                        objSancion.motivoSancion.descripcion = reader["motivoSancion"].ToString();
                        objSancion.tipoSancion.nombre = reader["tipoSancion"].ToString();
                        objSancion.cantidad = Convert.ToInt32(reader["cantidad"]);

                        listaSanciones.Add(objSancion);
                    }
                    sqlConnectionConfig.Close();
                    return listaSanciones;
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetInformeSanciones()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetInformeSanciones()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

		#endregion

	}
}
