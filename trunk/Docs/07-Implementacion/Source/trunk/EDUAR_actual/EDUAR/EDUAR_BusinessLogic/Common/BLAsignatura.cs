using System;
using System.Collections.Generic;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Common;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_BusinessLogic.Common
{
    public class BLAsignatura : BusinessLogicBase<Asignatura, DAAsignatura>
    {
        #region --[Constante]--
        private const string ClassName = "BLAsignatura";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLAsignatura(DTBase objAsignatura)
        {
            Data = (Asignatura)objAsignatura;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLAsignatura()
        {
            Data = new Asignatura();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAAsignatura DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed Asignatura Data
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
                int idAsignatura = 0;

                if (Data.idAsignatura == 0)
                    DataAcces.Create(Data, out idAsignatura);
                else
                    DataAcces.Update(Data);

                //Se da el OK para la transaccion.
                DataAcces.Transaction.CommitTransaction();
            }
            catch (CustomizedException ex)
            {
                DataAcces.Transaction.RollbackTransaction();
                throw ex;
            }
            catch (Exception ex)
            {
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
                DataAcces = new DAAsignatura(objDATransaction);
                if (Data.idAsignatura == 0)
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
                DataAcces = new DAAsignatura(objDATransaction);
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
		/// Gets the asignaturas.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
        public List<Asignatura> GetAsignaturas(Asignatura entidad)
        {
            try
            {
                return DataAcces.GetAsignaturas(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturas", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

		/// <summary>
		/// Gets the asignaturas curso.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Asignatura> GetAsignaturasCurso(Asignatura entidad)
		{
			try
			{
				return DataAcces.GetAsignaturasCurso(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturas", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

        /// <summary>
        /// Gets the asignaturas curso.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Asignatura> GetAsignaturasNivelCicloLectivo(int idCicloLectivo, int idNivel)
        {
            try
            {
                return DataAcces.GetAsignaturasNivelCicloLectivo(idCicloLectivo, idNivel);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivelCicloLectivo", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the asignaturas curso.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Asignatura> GetAsignaturasNivelesCiclosLectivos(List<CicloLectivo> cicloLectivo, List<Nivel> nivel)
        {
            try
            {
                return DataAcces.GetAsignaturasNivelesCiclosLectivos(cicloLectivo, nivel);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivelesCiclosLectivos", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the asignaturas nivel.
        /// </summary>
        /// <param name="asignaturaNivel">The asignatura nivel.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException"></exception>
        public List<Asignatura> GetAsignaturasNivel(Nivel nivel)
        {
            try
            {
                return DataAcces.GetAsignaturasNivel(nivel);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivel", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion

    }
}
