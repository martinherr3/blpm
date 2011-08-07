using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
	public class DAAgendaActividades : DataAccesBase<AgendaActividades>
	{
		#region --[Atributos]--
		private const string ClassName = "DAAgendaActividades";
		#endregion

		#region --[Constructor]--
		public DAAgendaActividades()
		{
		}

		public DAAgendaActividades(DATransaction objDATransaction)
			: base(objDATransaction)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		public List<AgendaActividades> GetAgendaActividadess(AgendaActividades entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AgendaActividades_Select");
				if (entidad != null)
				{
					if (entidad.idAgendaActividad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
					if (entidad.curso.idCurso > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.curso.idCurso);
					if (!string.IsNullOrEmpty(entidad.descripcion))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
					if (ValidarFechaSQL(entidad.fechaCreacion))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<AgendaActividades> listaAgendaActividadess = new List<AgendaActividades>();
				AgendaActividades objAgendaActividades;
				while (reader.Read())
				{
					objAgendaActividades = new AgendaActividades();

					objAgendaActividades.idAgendaActividad = Convert.ToInt32(reader["idAgendaActividad"]);
					objAgendaActividades.curso.idCurso = Convert.ToInt32(reader["idCurso"]);
					objAgendaActividades.descripcion = reader["descripcion"].ToString();
					objAgendaActividades.activo = Convert.ToBoolean(reader["activo"].ToString());
					objAgendaActividades.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"].ToString());
					listaAgendaActividadess.Add(objAgendaActividades);
				}
				return listaAgendaActividadess;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAgendaActividadess()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAgendaActividadess()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion

		#region --[Implementación métodos heredados]--
		public override string FieldID
		{
			get { throw new NotImplementedException(); }
		}

		public override string FieldDescription
		{
			get { throw new NotImplementedException(); }
		}

		public override AgendaActividades GetById(AgendaActividades entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(AgendaActividades entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(AgendaActividades entidad, out int identificador)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AgendaActividadess_Insert");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.curso.idCurso);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

				identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idAgendaActividad"].Value.ToString());

			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		public override void Update(AgendaActividades entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(AgendaActividades entidad)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
