using System;
using System.Collections.Generic;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Reports;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.Reports;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System.Data;

namespace EDUAR_BusinessLogic.Reports
{
    public class BLInformeIndicador : BusinessLogicBase<InformeIndicador, DAInformeIndicador>
    {
        #region --[Constante]--
        private const string ClassName = "BLInformeIndicador";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLInformeIndicador(DTBase objIndicador)
        {
            Data = (InformeIndicador)objIndicador;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLInformeIndicador()
        {
            Data = new InformeIndicador();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAInformeIndicador DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed InformeIndicador Data
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
                int idIndicador = 0;

                if (Data.idIndicador == 0)
                    DataAcces.Create(Data, out idIndicador);
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
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Delete(DATransaction objDATransaction)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos publicos]--
        public DataTable GetInformeIndicador(string nombreSP, int idCursoCicloLectivo, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return DataAcces.GetInformeIndicador(nombreSP, idCursoCicloLectivo, fechaDesde, fechaHasta);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetInformeIndicador", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion

    }
}
