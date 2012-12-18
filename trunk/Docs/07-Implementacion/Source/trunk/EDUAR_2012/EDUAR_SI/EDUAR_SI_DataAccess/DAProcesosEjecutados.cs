using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities;
using System.Data.SqlClient;
using System.Data;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_SI_DataAccess
{
	public class DAProcesosEjecutados : DABase
	{
		#region --[Atributos]--
		private const string ClassName = "DAProcesosEjecutados";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor. LLama al constructor de la clase base DABase.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos</param>
		public DAProcesosEjecutados(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Creates the specified resultado proceso.
		/// </summary>
		/// <param name="resultadoProceso">The resultado proceso.</param>
		public void Create(ProcesosEjecutados resultadoProceso)
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

					transaccion = sqlConnectionConfig.BeginTransaction();
					command.Transaction = transaccion;

					command.Parameters.AddWithValue("fechaEjecucion", resultadoProceso.fechaEjecucion);
					command.Parameters.AddWithValue("resultado", resultadoProceso.resultado);
					command.Parameters.AddWithValue("descripcionError", resultadoProceso.descripcionError);
					command.Parameters.AddWithValue("idProcesoAutomatico", resultadoProceso.idProcesoAutomatico);
					command.ExecuteNonQuery();
					transaccion.Commit();
				}
			}
			catch (SqlException ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
