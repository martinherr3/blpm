using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data;
using EDUAR_Utility.Enumeraciones;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
	public class DADiasHorarios : DataAccesBase<DiasHorarios>
	{
		#region --[Atributos]--
		private const string ClassName = "DADiasHorarios";
		#endregion

		#region --[Constructor]--
		public DADiasHorarios()
		{
		}

		public DADiasHorarios(DATransaction objDATransaction)
			: base(objDATransaction)
		{

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

		public override DiasHorarios GetById(DiasHorarios entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(DiasHorarios entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(DiasHorarios entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(DiasHorarios entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(DiasHorarios entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		public List<DiasHorarios> GetDiasHorarios(DiasHorarios entidad, CursoCicloLectivo cursoCicloLectivo)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("DiaHorario_Select");
				if (entidad != null)
				{
					if (entidad.idAsignaturaCurso > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCurso", DbType.Int32, entidad.idAsignaturaCurso);
					if (cursoCicloLectivo.idCursoCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, cursoCicloLectivo.idCursoCicloLectivo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<DiasHorarios> listaHorario = new List<DiasHorarios>();
				DiasHorarios objDiaHorario;
				while (reader.Read())
				{
					objDiaHorario = new DiasHorarios();
					//Se asigna el idAsignaturaCurso de la tabla - SOLO CUANDO MANEJO ASIGNATURA - CURSO
					objDiaHorario.idAsignaturaCurso = Convert.ToInt32(reader["idAsignaturaCicloLectivo"]);
					objDiaHorario.idDiaHorario = Convert.ToInt32(reader["idDiaHorario"]);
					objDiaHorario.unDia = (enumDiasSemana)Enum.Parse(typeof(enumDiasSemana), Convert.ToInt32(reader["idDiaSemana"]).ToString());
					listaHorario.Add(objDiaHorario);
				}
				return listaHorario;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetDiasHorarios()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetDiasHorarios()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
