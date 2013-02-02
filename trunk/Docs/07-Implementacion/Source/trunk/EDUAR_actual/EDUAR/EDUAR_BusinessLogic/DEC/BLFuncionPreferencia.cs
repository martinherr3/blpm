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
    public class BLFuncionPreferencia : BusinessLogicBase<FuncionPreferencia, DAFuncionPreferencia>
    {
        #region --[Constante]--
        private const string ClassName = "BLFuncionPreferencia";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLFuncionPreferencia(DTBase objFuncionPreferencia)
        {
            Data = (FuncionPreferencia)objFuncionPreferencia;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLFuncionPreferencia()
        {
            Data = new FuncionPreferencia();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAFuncionPreferencia DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed FuncionPreferencia Data
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
                int idFuncionPreferencia = 0;

                if (Data.idFuncionPreferencia == 0)
                    DataAcces.Create(Data, out idFuncionPreferencia);
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
                DataAcces = new DAFuncionPreferencia(objDATransaction);
                if (Data.idFuncionPreferencia == 0)
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
                DataAcces = new DAFuncionPreferencia(objDATransaction);
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
		//public List<FuncionPreferencia> GetFuncionPreferencias(FuncionPreferencia entidad)
		//{
		//    try
		//    {
		//        return DataAcces.GetFuncionPreferencias(entidad);
		//    }
		//    catch (CustomizedException ex)
		//    {
		//        throw ex;
		//    }
		//    catch (Exception ex)
		//    {
		//        throw new CustomizedException(string.Format("Fallo en {0} - GetFuncionPreferencias", ClassName), ex,
		//                                      enuExceptionType.BusinessLogicException);
		//    }
		//}
        #endregion
    }
}