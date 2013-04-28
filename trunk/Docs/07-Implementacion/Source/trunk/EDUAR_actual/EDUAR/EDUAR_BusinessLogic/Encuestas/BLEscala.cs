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
    public class BLEscala : BusinessLogicBase<EscalaMedicion, DAEscala>
    {
        #region --[Constante]--
        private const string ClassName = "BLEscala";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLEscala(DTBase objEscalaMedicion)
        {
            Data = (EscalaMedicion)objEscalaMedicion;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLEscala()
        {
            Data = new EscalaMedicion();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAEscala DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed EscalaMedicion Data
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
                int idEscala = 0;

                if (Data.idEscala == 0)
                    DataAcces.Create(Data, out idEscala);
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
                DataAcces = new DAEscala(objDATransaction);
                if (Data.idEscala == 0)
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
				DataAcces = new DAEscala();
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
                DataAcces = new DAEscala(objDATransaction);
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
        /// Obtiene las escalas de medición disponibles.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        public List<EscalaMedicion> GetEscalasMedicion(EscalaMedicion objFiltro)
        {
            try
            {
                return DataAcces.GetEscalasMedicion(objFiltro);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEscalasMedicion", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Cantidad de valores de la escala de ponderacion.
        /// </summary>
        /// <param name="entidad">The objFiltro.</param>
        /// <returns></returns>
        public List<ValorEscalaMedicion> GetCantidadValores(EscalaMedicion objFiltro)
        {
            try
            {
                return DataAcces.GetCantidadValores(objFiltro);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCantidadValores", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion
    }
}
