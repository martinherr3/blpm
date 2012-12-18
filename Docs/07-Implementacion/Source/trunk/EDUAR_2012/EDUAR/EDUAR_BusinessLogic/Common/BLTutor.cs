using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_DataAccess.Common;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Shared;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using EDUAR_DataAccess.Shared;

namespace EDUAR_BusinessLogic.Common
{
	public class BLTutor : BusinessLogicBase<Tutor, DATutor>
	{
		#region --[Constante]--
		private const string ClassName = "BLTutor";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLTutor(DTBase objTutor)
		{
			Data = (Tutor)objTutor;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLTutor()
		{
			Data = new Tutor();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DATutor DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed Tutor Data
		{
			get { return data; }
			set { data = value; }
		}

		public override string FieldId
		{
			get { return DataAcces.FieldID; }
		}

		public override string FieldDescription
		{
			get { return DataAcces.FieldDescription; }
		}

		/// <summary>
		/// Gets the by id.
		/// </summary>
		public override void GetById()
		{
			try
			{
				Data = DataAcces.GetById(Data);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetById", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Método que guarda el registro actualmente cargado en memoria. No importa si se trata de una alta o modificación.
		/// </summary>
		public override void Save()
		{
			try
			{
				//Abre la transaccion que se va a utilizar
				DataAcces.Transaction.OpenTransaction();
				int idTutor = 0;

				if (Data.idTutor == 0)
					DataAcces.Create(Data, out idTutor);
				else
					DataAcces.Update(Data);

				//Se da el OK para la transaccion.
				DataAcces.Transaction.CommitTransaction();
			}
			catch (CustomizedException ex)
			{
				if (DataAcces != null && DataAcces.Transaction != null)
					DataAcces.Transaction.RollbackTransaction();
				throw ex;
			}
			catch (Exception ex)
			{
				if (DataAcces != null && DataAcces.Transaction != null)
					DataAcces.Transaction.RollbackTransaction();
				throw new CustomizedException(string.Format("Fallo en {0} - Save()", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Método que guarda el registro actualmente cargado en memoria. No importa si se trata de una alta o modificación.
		/// </summary>
		public override void Save(DATransaction objDATransaction)
		{
			try
			{
				//Si no viene el Id es porque se esta creando la entidad
				DataAcces = new DATutor(objDATransaction);
				if (Data.idTutor == 0)
					DataAcces.Create(Data);
				else
				{
					DataAcces.Update(Data);
				}
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Save()", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		public override void Delete()
		{
			throw new NotImplementedException();
		}

		public override void Delete(DATransaction objDATransaction)
		{
			try
			{
				DataAcces = new DATutor(objDATransaction);
				DataAcces.Delete(Data);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion

		#region --[Métodos publicos]--
		/// <summary>
		/// Gets the tutores.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Tutor> GetTutores(Tutor entidad)
		{
			try
			{
				return DataAcces.GetTutores(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTutores", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the tutores por curso.
		/// </summary>
		/// <param name="tutor">The tutor.</param>
		/// <returns></returns>
		public List<Tutor> GetTutoresPorCurso(AlumnoCurso entidad)
		{
			try
			{
				return DataAcces.GetTutoresPorCurso(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTutoresPorCurso", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the docentes alumnos.
		/// </summary>
		/// <returns></returns>
		public List<Persona> GetDocentesAlumnos(CicloLectivo cicloLectivoActual)
		{
			try
			{
				List<Alumno> listaAlumnos = DataAcces.GetAlumnosTutor(Data);
				BLAlumno objBLAlumno = null;
				List<Persona> listaPersonas = new List<Persona>();
				List<Persona> listaPersonasSec = null;

				foreach (Alumno item in listaAlumnos)
				{
					objBLAlumno = new BLAlumno(item);
					listaPersonasSec = new List<Persona>();
					listaPersonasSec = objBLAlumno.GetDocentesAlumno(cicloLectivoActual);
					foreach (Persona itemPersona in listaPersonasSec)
					{
						if (!listaPersonas.Exists(p => p.idPersona == itemPersona.idPersona))
							listaPersonas.Add(itemPersona);
					}
				}

				return listaPersonas;
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetDocentesAlumnos", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the alumnos tutor.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<AlumnoCursoCicloLectivo> GetAlumnosTutor(Tutor entidad)
		{
			try
			{
				List<Alumno> listaAlumnos = DataAcces.GetAlumnosTutor(entidad);
				BLAlumno objBLAlumno = new BLAlumno();
				List<AlumnoCursoCicloLectivo> listaAlumnoCurso = new List<AlumnoCursoCicloLectivo>();
				foreach (var item in listaAlumnos)
				{
					objBLAlumno.Data = item;
					listaAlumnoCurso.Add(objBLAlumno.GetCursoAlumno());
				}
				return listaAlumnoCurso;
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnosTutor", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

        /// <summary>
        /// Gets the alumnos tutor.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Alumno> GetAlumnosDeTutor(Tutor entidad)
        {
            try
            {
				return DataAcces.GetAlumnosTutor(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
				throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnosDeTutor", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the alumnos tutor.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Alumno> GetAlumnosDeTutor(Tutor entidad, int idCicloLectivo, int idCurso)
        {
            try
            {
                return DataAcces.GetAlumnosTutor(entidad, idCicloLectivo, idCurso);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnosDeTutor", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
		#endregion
	}
}
