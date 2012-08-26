﻿using System;
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
    public class BLAmbitoEncuesta : BusinessLogicBase<AmbitoEncuesta, DAAmbitoEncuesta>
    {
        #region --[Constante]--
        private const string ClassName = "BLAmbitoEncuesta";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLAmbitoEncuesta(DTBase objEscalaMedicion)
        {
            Data = (AmbitoEncuesta)objEscalaMedicion;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLAmbitoEncuesta()
        {
            Data = new AmbitoEncuesta();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAAmbitoEncuesta DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed AmbitoEncuesta Data
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
                int idAmbitoEncuesta = 0;

                if (Data.idAmbitoEncuesta == 0)
                    DataAcces.Create(Data, out idAmbitoEncuesta);
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
                DataAcces = new DAAmbitoEncuesta(objDATransaction);
                if (Data.idAmbitoEncuesta == 0)
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
				DataAcces = new DAAmbitoEncuesta();
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
                DataAcces = new DAAmbitoEncuesta(objDATransaction);
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
        /// Obtiene los ámbitos de encuesta disponibles.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        public List<AmbitoEncuesta> GetAmbitosEncuesta(AmbitoEncuesta objFiltro)
        {
            try
            {
                return DataAcces.GetAmbitosEncuesta(objFiltro);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAmbitosEncuesta", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion
    }
}
