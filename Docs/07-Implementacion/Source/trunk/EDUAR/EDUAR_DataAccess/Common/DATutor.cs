using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Data.SqlClient;
using System.Data;

namespace EDUAR_DataAccess.Common
{
	public class DATutor : DataAccesBase<Tutor>
	{
		#region --[Atributos]--
		private const string ClassName = "DATutor";
		#endregion

		#region --[Constructor]--
		public DATutor()
		{
		}

		public DATutor(DATransaction objDATransaction)
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

		public override Tutor GetById(Tutor entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Tutor entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Tutor entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(Tutor entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Tutor entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the tutores.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Tutor> GetTutores(Tutor entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Tutor_Select");
				if (entidad != null)
				{
					if (entidad.activo != null)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Tutor> listaTutores = new List<Tutor>();
				Tutor objTutor;
				while (reader.Read())
				{
					objTutor = new Tutor();

					objTutor.idTutor = Convert.ToInt32(reader["idTutor"]);
					objTutor.nombre = reader["nombre"].ToString();
					objTutor.apellido = reader["apellido"].ToString();
					objTutor.activo = Convert.ToBoolean(reader["activo"]);
					//if (!string.IsNullOrEmpty(reader["fechaNacimiento"].ToString()))
					//    objTutor.fechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"].ToString());
					listaTutores.Add(objTutor);
				}
				return listaTutores;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTutores()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTutores()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the tutores por curso.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Tutor> GetTutoresPorCurso(AlumnoCurso entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TutoresPorCurso_Select");
				if (entidad != null)
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.curso.idCurso);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.curso.cicloLectivo.idCicloLectivo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Tutor> listaTutores = new List<Tutor>();
				Tutor objTutor;
				while (reader.Read())
				{
					objTutor = new Tutor();

					objTutor.idTutor = Convert.ToInt32(reader["idTutor"]);
					objTutor.nombre = reader["nombre"].ToString();
					objTutor.apellido = reader["apellido"].ToString();
					//objTutor.activo = Convert.ToBoolean(reader["activo"]);
					//if (!string.IsNullOrEmpty(reader["fechaNacimiento"].ToString()))
					//    objTutor.fechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"].ToString());
					listaTutores.Add(objTutor);
				}
				return listaTutores;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTutoresPorCurso()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTutoresPorCurso()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
