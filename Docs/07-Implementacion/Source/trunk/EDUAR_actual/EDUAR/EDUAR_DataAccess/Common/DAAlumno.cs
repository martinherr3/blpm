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
	public class DAAlumno : DataAccesBase<Alumno>
	{
		#region --[Atributos]--
		private const string ClassName = "DAAlumno";
		#endregion

		#region --[Constructor]--
		public DAAlumno()
		{
		}

		public DAAlumno(DATransaction objDATransaction)
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

		public override Alumno GetById(Alumno entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Alumno entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Alumno entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(Alumno entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Alumno entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the alumnos.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Alumno> GetAlumnos(AlumnoCurso entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AlumnosPorCurso_Select");
				if (entidad != null)
				{
					if (entidad.alumno.idAlumno > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.alumno.idAlumno);
					if (entidad.curso.idCurso > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.curso.idCurso);
					if (!string.IsNullOrEmpty(entidad.alumno.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.alumno.username);
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
					if (!string.IsNullOrEmpty(reader["fechaAlta"].ToString()))
						objAlumno.fechaAlta = (DateTime)reader["fechaAlta"];
					if (!string.IsNullOrEmpty(reader["fechaBaja"].ToString()))
						objAlumno.fechaBaja = (DateTime)reader["fechaBaja"];
					objAlumno.activo = Convert.ToBoolean(reader["activo"]);
					objAlumno.idPersona = Convert.ToInt32(reader["idPersona"]);
					//TODO: Completar los miembros que faltan de alumno

					listaAlumnos.Add(objAlumno);
				}
				return listaAlumnos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnosPorCurso()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetalumnosPorCurso()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the curso alumno.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public AlumnoCursoCicloLectivo GetCursoAlumno(Alumno entidad)
		{
			try
			{
				return GetCursoAlumno(entidad, 0);
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursoAlumno()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursoAlumno()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the curso alumno.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public AlumnoCursoCicloLectivo GetCursoAlumno(Alumno entidad, int idCicloLectivo)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AlumnosPorCurso_Select");
				if (entidad != null)
				{
					if (!string.IsNullOrEmpty(entidad.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.username);
					if (entidad.idAlumno > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.idAlumno);
					if (idCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, idCicloLectivo);
					if (entidad.listaTutores.Count > 0 && !string.IsNullOrEmpty(entidad.listaTutores[0].username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usernameTutor", DbType.String, entidad.listaTutores[0].username);
				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				AlumnoCursoCicloLectivo objAlumnoCurso = null;
				while (reader.Read())
				{
					objAlumnoCurso = new AlumnoCursoCicloLectivo();
					objAlumnoCurso.alumno.idAlumno = Convert.ToInt32(reader["idAlumno"]);
					objAlumnoCurso.alumno.nombre = reader["nombre"].ToString();
					objAlumnoCurso.alumno.apellido = reader["apellido"].ToString();
					if (!string.IsNullOrEmpty(reader["fechaAlta"].ToString()))
						objAlumnoCurso.alumno.fechaAlta = (DateTime)reader["fechaAlta"];
					if (!string.IsNullOrEmpty(reader["fechaBaja"].ToString()))
						objAlumnoCurso.alumno.fechaBaja = (DateTime)reader["fechaBaja"];
					objAlumnoCurso.alumno.activo = Convert.ToBoolean(reader["activo"]);
					objAlumnoCurso.alumno.idPersona = Convert.ToInt32(reader["idPersona"]);
					objAlumnoCurso.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(reader["idCursoCicloLectivo"]);
					objAlumnoCurso.cursoCicloLectivo.curso.nombre = reader["curso"].ToString();
					objAlumnoCurso.cursoCicloLectivo.curso.nivel.nombre = reader["nivel"].ToString();
					return objAlumnoCurso;
				}
				return null;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursoAlumno()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursoAlumno()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the curso alumno.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<AlumnoCursoCicloLectivo> GetAlumnosTutor(Alumno entidad, int idCicloLectivo)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TutorAlumnosPorCurso_Select");
				if (entidad != null)
				{
					if (!string.IsNullOrEmpty(entidad.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.username);
					if (entidad.idAlumno > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.idAlumno);
					if (idCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, idCicloLectivo);
					if (entidad.listaTutores.Count > 0 && !string.IsNullOrEmpty(entidad.listaTutores[0].username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usernameTutor", DbType.String, entidad.listaTutores[0].username);
				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);
				List<AlumnoCursoCicloLectivo> listaAlumnos = new List<AlumnoCursoCicloLectivo>();
				AlumnoCursoCicloLectivo objAlumnoCurso = null;
				while (reader.Read())
				{
					objAlumnoCurso = new AlumnoCursoCicloLectivo();
					objAlumnoCurso.alumno.idAlumno = Convert.ToInt32(reader["idAlumno"]);
					objAlumnoCurso.alumno.nombre = reader["nombre"].ToString();
					objAlumnoCurso.alumno.apellido = reader["apellido"].ToString();
					if (!string.IsNullOrEmpty(reader["fechaAlta"].ToString()))
						objAlumnoCurso.alumno.fechaAlta = (DateTime)reader["fechaAlta"];
					if (!string.IsNullOrEmpty(reader["fechaBaja"].ToString()))
						objAlumnoCurso.alumno.fechaBaja = (DateTime)reader["fechaBaja"];
					objAlumnoCurso.alumno.activo = Convert.ToBoolean(reader["activo"]);
					objAlumnoCurso.alumno.idPersona = Convert.ToInt32(reader["idPersona"]);
					objAlumnoCurso.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(reader["idCursoCicloLectivo"]);
					objAlumnoCurso.cursoCicloLectivo.curso.nombre = reader["curso"].ToString();
					objAlumnoCurso.cursoCicloLectivo.curso.nivel.nombre = reader["nivel"].ToString();
					listaAlumnos.Add(objAlumnoCurso);
				}
				return listaAlumnos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursoAlumno()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursoAlumno()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

        /// <summary>
        /// Gets the alumnos por niveles y ciclos lectivos.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Alumno> GetAlumnosNivelCicloLectivo(List<CicloLectivo> cicloLectivo, List<Nivel> nivel)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AlumnosPorNivelCicloLectivo_select");
                if (cicloLectivo != null && nivel != null)
                {
                    string ciclosLectivosParam = string.Empty;
                    if (cicloLectivo.Count > 0)
                    {
                        foreach (CicloLectivo unCicloLectivo in cicloLectivo)
                            ciclosLectivosParam += string.Format("{0},", unCicloLectivo.idCicloLectivo);

                        ciclosLectivosParam = ciclosLectivosParam.Substring(0, ciclosLectivosParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaCicloLectivo", DbType.String, ciclosLectivosParam);
                    }

                    string nivelesParam = string.Empty;
                    if (nivel.Count > 0)
                    {
                        foreach (Nivel unNivel in nivel)
                            nivelesParam += string.Format("{0},", unNivel.idNivel);

                        nivelesParam = nivelesParam.Substring(0, nivelesParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaNivel", DbType.String, nivelesParam);
                    }
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);
                List<Alumno> listaAlumnos = new List<Alumno>();
                Alumno objAlumno = null;
                while (reader.Read())
                {
                    objAlumno = new Alumno();
                    objAlumno.idAlumno = Convert.ToInt32(reader["idAlumno"]);
                    objAlumno.nombre = reader["Nombre"].ToString();
                    
                    listaAlumnos.Add(objAlumno);
                }
                return listaAlumnos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnosNivelCicloLectivo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnosNivelCicloLectivo()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
		#endregion
	}
}
