using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess.Entity;
using Promethee.Utility;
using System.Data;

namespace DataAccess
{
	public class ModelosDA
	{
		private static string ClassName = "ModelosDA";

		/// <summary>
		/// Selects the specified usuario.
		/// </summary>
		/// <param name="usuario">The usuario.</param>
		/// <returns></returns>
		public static List<ModeloEntity> Select(UsuarioEntity usuario)
		{
			try
			{
				List<ModeloEntity> lista = new List<ModeloEntity>();
				using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
				{
					SqlCommand command = new SqlCommand();
					command.Connection = conn;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Modelos_Select";
					command.Parameters.AddWithValue("@username", usuario.username);
					command.Parameters.AddWithValue("@idModelo", DBNull.Value);
					command.Parameters.AddWithValue("@fechaCreacion", DBNull.Value);
					command.Parameters.AddWithValue("@nombre", DBNull.Value);

					command.CommandTimeout = 10;

					conn.Open();

					SqlDataReader reader = command.ExecuteReader();
					ModeloEntity objEntidad = null;
					while (reader.Read())
					{
						objEntidad = new ModeloEntity();
						objEntidad.idModelo = Convert.ToInt32(reader["idModelo"]);
						objEntidad.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"]);
						objEntidad.username = reader["username"].ToString();
						objEntidad.nombre = reader["nombre"].ToString();
						objEntidad.alternativas = Convert.ToInt32(reader["alternativas"]);
						objEntidad.criterios = Convert.ToInt32(reader["criterios"]);
						lista.Add(objEntidad);
					}
					conn.Close();
					return lista;
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - Select()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - Select()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Creates the specified nuevo modelo.
		/// </summary>
		/// <param name="nuevoModelo">The nuevo modelo.</param>
		public static void Save(ModeloEntity nuevoModelo)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
				{
					SqlCommand command = new SqlCommand();
					command.Connection = conn;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Modelos_Update";
					if (nuevoModelo.idModelo == 0)
						command.CommandText = "Modelos_Insert";
					command.Parameters.AddWithValue("@idModelo", nuevoModelo.idModelo);
					command.Parameters.AddWithValue("@username", nuevoModelo.username);
					command.Parameters.AddWithValue("@fechaCreacion", nuevoModelo.fechaCreacion);
					command.Parameters.AddWithValue("@nombre", nuevoModelo.nombre);

					command.CommandTimeout = 10;

					conn.Open();

					int idModelo = Convert.ToInt32(command.ExecuteScalar());

					conn.Close();
				}
			}
			catch (SqlException ex)
			{
                throw new CustomizedException(String.Format("Fallo en {0} - Save()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
                throw new CustomizedException(String.Format("Fallo en {0} - Save()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
	}
}
