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
					if (entidad.cursoCicloLectivo.idCursoCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.idCursoCicloLectivo);
					if (entidad.curso.cicloLectivo.idCicloLectivo > 0)
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
					objTutor.idPersona = Convert.ToInt32(reader["idPersona"]);

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

		/// <summary>
		/// Gets the alumnos tutor.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Alumno> GetAlumnosTutor(Tutor entidad, int idCicloLectivo =0, int idCurso = 0)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TutorAlumno_Select");
				if (entidad != null)
				{
					if (!string.IsNullOrEmpty(entidad.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.username);
					if (entidad.activo != null)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
                    if (entidad.idTutor != 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTutor", DbType.Int32, entidad.idTutor);
                    if (idCicloLectivo != 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, idCicloLectivo);
                    if (idCurso != 0 && idCurso !=-1)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, idCurso);

				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Alumno> listaAlumnos = new List<Alumno>();
				Alumno objAlumno;
				while (reader.Read())
				{
					objAlumno = new Alumno();
					objAlumno.idAlumno = Convert.ToInt32(reader["idAlumno"]);

                    objAlumno.nombre = reader["nombre"].ToString();
                    objAlumno.apellido = reader["apellido"].ToString();

					listaAlumnos.Add(objAlumno);
				}
				return listaAlumnos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnosTutor()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnosTutor()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
