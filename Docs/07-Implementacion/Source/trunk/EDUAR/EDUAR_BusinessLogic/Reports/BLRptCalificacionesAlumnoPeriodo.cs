using System;
using System.Collections.Generic;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Reports;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.Reports;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_BusinessLogic.Reports
{
    public class BLRptCalificacionesAlumnoPeriodo : BusinessLogicBase<RptCalificacionesAlumnoPeriodo, DARptCalificacionesAlumnoPeriodo>
    {
        #region --[Constante]--
        private const string ClassName = "BLRptCalificacionesAlumnoPeriodo";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLRptCalificacionesAlumnoPeriodo(DTBase objRptCalificacionesAlumnoPeriodo)
        {
            Data = (RptCalificacionesAlumnoPeriodo)objRptCalificacionesAlumnoPeriodo;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLRptCalificacionesAlumnoPeriodo()
        {
            Data = new RptCalificacionesAlumnoPeriodo();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DARptCalificacionesAlumnoPeriodo DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed RptCalificacionesAlumnoPeriodo Data
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que guarda el registro actualmente cargado en memoria. No importa si se trata de una alta o modificación.
        /// </summary>
        public override void Save()
        {
            throw new NotImplementedException();
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
        public List<RptCalificacionesAlumnoPeriodo> GetRptCalificacionesAlumnoPeriodo(RptCalificacionesAlumnoPeriodo entidad)
        {
            try
            {
                return DataAcces.GetRptCalificacionesAlumnoPeriodo(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptCalificacionesAlumnoPeriodo", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion
    }
}
