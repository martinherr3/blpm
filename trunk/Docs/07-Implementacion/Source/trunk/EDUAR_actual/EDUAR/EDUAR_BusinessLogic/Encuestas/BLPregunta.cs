using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_DataAccess.Encuestas;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Shared;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using EDUAR_DataAccess.Shared;

namespace EDUAR_BusinessLogic.Encuestas
{
    public class BLPregunta : BusinessLogicBase<Pregunta, DAPregunta>
    {
        #region --[Constante]--
        private const string ClassName = "BLPregunta";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLPregunta(DTBase objPregunta)
        {
            Data = (Pregunta)objPregunta;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLPregunta()
        {
            Data = new Pregunta();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAPregunta DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed Pregunta Data
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
                int idPregunta = 0;

                if (Data.idPregunta == 0)
                    DataAcces.Create(Data, out idPregunta);
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
                DataAcces = new DAPregunta(objDATransaction);
                if (Data.idPregunta == 0)
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
			try
			{
				DataAcces = new DAPregunta();
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

        public override void Delete(DATransaction objDATransaction)
        {
            try
            {
                DataAcces = new DAPregunta(objDATransaction);
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
        /// Obtiene todas las preguntas.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        public List<Pregunta> GetPreguntas(Pregunta objFiltro)
        {
            try
            {
                return DataAcces.GetPreguntas(objFiltro);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntas", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Obtiene todas las preguntas que se ajustan a una categoría dada.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        public List<Pregunta> GetPreguntasPorCategoria(CategoriaPregunta objFiltro)
        {
            try
            {
                return DataAcces.GetPreguntasPorCategoria(objFiltro);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasPorCategoria", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

		/// <summary>
		/// Gets the preguntas por categoria.
		/// </summary>
		/// <param name="objFiltro">The obj filtro.</param>
		/// <param name="miEncuesta">The mi encuesta.</param>
		/// <returns></returns>
		public List<Pregunta> GetPreguntasPorCategoria(CategoriaPregunta objFiltro, Encuesta miEncuesta)
		{
			try
			{
				return DataAcces.GetPreguntasPorCategoria(objFiltro, miEncuesta);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasPorCategoria", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

        #endregion
    }
}
