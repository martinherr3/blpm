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
    public class BLEncuestaDisponible : BusinessLogicBase<EncuestaDisponible, DAEncuestaDisponible>
    {
        #region --[Constante]--
        private const string ClassName = "BLEncuestaDisponible";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLEncuestaDisponible(DTBase objEncuesta)
        {
            Data = (EncuestaDisponible)objEncuesta;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLEncuestaDisponible()
        {
            Data = new EncuestaDisponible();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAEncuestaDisponible DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed EncuestaDisponible Data
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
                int idEncuesta = 0;

                if (Data.encuesta.idEncuesta == 0)
                    DataAcces.Create(Data, out idEncuesta);
                else
                    DataAcces.Update(Data);

				BLRespuesta objBLRespuesta;
				
				// GUARDAR LAS RESPUESTAS
				foreach (Respuesta respuesta in Data.listaRespuestas)
				{
					objBLRespuesta = new BLRespuesta(respuesta);
					objBLRespuesta.Save(DataAcces.Transaction);
				}

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
                DataAcces = new DAEncuestaDisponible(objDATransaction);
                if (Data.encuesta.idEncuesta == 0)
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
				DataAcces = new DAEncuestaDisponible();
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
                DataAcces = new DAEncuestaDisponible(objDATransaction);
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
		/// Gets the encuestas disponibles.
		/// </summary>
		/// <param name="objFiltro">The obj filtro.</param>
		/// <returns></returns>
        public List<Encuesta> GetEncuestasDisponibles(EncuestaDisponible objFiltro)
        {
            try
            {
                return DataAcces.GetEncuestasDisponibles(objFiltro);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestasDisponibles", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        public List<EncuestaDisponible> GetEncuestasRespondidas(EncuestaDisponible objFiltro)
        {
            try
            {
                return DataAcces.GetEncuestasRespondidas(objFiltro);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestasRespondidas", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        public List<Respuesta> GetRespuestasSumarizadas(EncuestaDisponible objFiltro)
        {
            try
            {
				//return DataAcces.GetRespuestasSumarizadas(objFiltro);
				return new List<Respuesta>();
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRespuestasSumarizadas", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion
    }
}
