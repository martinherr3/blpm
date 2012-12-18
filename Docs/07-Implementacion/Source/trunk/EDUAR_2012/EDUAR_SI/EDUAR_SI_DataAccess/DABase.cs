using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System;
using System.Data;
using System.Collections.Generic;

namespace EDUAR_SI_DataAccess
{
	/// <summary>
	/// Clase base que contiene la conexion a la base de datos.
	/// </summary>
	public class DABase
	{
		#region --[Atributos]--
		/// <summary>
		/// Conexión a la base de datos.
		/// </summary>
		protected SqlConnection sqlConnectionConfig;

		private const string ClassName = "DABase";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public DABase()
		{
		}

		/// <summary>
		/// Constructor. Inicializa el objeto SQLConnection con la cadena de conexión
		/// pasada por parámetro.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos.</param>
		public DABase(string connectionString)
		{
			sqlConnectionConfig = new SqlConnection(connectionString);
		}
		#endregion

		#region --[Métodos Públicos]
		/// <summary>
		/// Obteners the configuracion.
		/// </summary>
		/// <param name="parametroConfiguracion">The parametro configuracion.</param>
		/// <returns></returns>
		public Configuraciones ObtenerConfiguracion(enumConfiguraciones configuracion)
		{
			Configuraciones objConfiguracion = null;
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					sqlConnectionConfig.Open();

					command.Connection = sqlConnectionConfig;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Configuraciones_Select";
					command.CommandTimeout = 10;

					if (!string.IsNullOrEmpty(configuracion.ToString()))
						command.Parameters.AddWithValue("@nombre", configuracion.ToString());

					SqlDataReader reader = command.ExecuteReader();
					objConfiguracion = new Configuraciones();
					while (reader.Read())
					{
						objConfiguracion.idConfiguracion = (int)reader["idConfiguracion"];
						objConfiguracion.nombre = reader["nombre"].ToString();
						objConfiguracion.descripcion = reader["descripcion"].ToString();
						objConfiguracion.activo = (bool)reader["activo"];
						objConfiguracion.valor = reader["valor"].ToString();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - ObtenerConfiguracion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - ObtenerConfiguracion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
			finally
			{
				if (sqlConnectionConfig.State == ConnectionState.Open)
					sqlConnectionConfig.Close();
			}
			return objConfiguracion;
		}

		/// <summary>
		/// Obteners the configuracion.
		/// </summary>
		/// <returns></returns>
		public List<Configuraciones> ObtenerConfiguraciones()
		{
			Configuraciones objConfiguracion = null;
			var listaConfiguraciones = new List<Configuraciones>();
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					sqlConnectionConfig.Open();

					command.Connection = sqlConnectionConfig;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Configuraciones_Select";
					command.CommandTimeout = 10;

					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						objConfiguracion = new Configuraciones();
						objConfiguracion.idConfiguracion = (int)reader["idConfiguracion"];
						objConfiguracion.nombre = reader["nombre"].ToString();
						objConfiguracion.descripcion = reader["descripcion"].ToString();
						objConfiguracion.activo = (bool)reader["activo"];
						objConfiguracion.valor = reader["valor"].ToString();
						listaConfiguraciones.Add(objConfiguracion);
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - ObtenerConfiguraciones()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - ObtenerConfiguraciones()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
			finally
			{
				if (sqlConnectionConfig.State == ConnectionState.Open)
					sqlConnectionConfig.Close();
			}
			return listaConfiguraciones;
		}

		/// <summary>
		/// Called when [error process].
		/// </summary>
		/// <param name="resultadoProceso">The resultado proceso.</param>
		public void OnErrorProcess(ProcesosEjecutados resultadoProceso)
		{
			SqlTransaction transaccion = null;
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

					command.Connection = sqlConnectionConfig;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "ProcesosEjecutados_Insert";
					command.CommandTimeout = 10;

					command.Parameters.AddWithValue("fechaEjecucion", resultadoProceso.fechaEjecucion);
					command.Parameters.AddWithValue("resultado", false);
					command.Parameters.AddWithValue("descripcionError", resultadoProceso.descripcionError);
					command.Parameters.AddWithValue("idProcesoAutomatico", resultadoProceso.idProcesoAutomatico);
					command.ExecuteNonQuery();
				}
			}
			catch (SqlException ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - OnErrorProcess()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - OnErrorProcess()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Validars the fecha SQL.
		/// </summary>
		/// <param name="fecha">The fecha.</param>
		/// <returns></returns>
		public static bool ValidarFechaSQL(DateTime fecha)
		{
			if (fecha.Year < 1753 || fecha.Year > 9999)
				return false;
			return true;
		}
		#endregion
	}
}