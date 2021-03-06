﻿using System;
using System.Collections.Generic;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Reports;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_BusinessLogic.Reports
{
    public class BLRptAccesos : BusinessLogicBase<RptAccesos, DARptAccesos>
    {
        #region --[Constante]--
        private const string ClassName = "BLRptAcceso";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLRptAccesos(DTBase objRptAcceso)
        {
            Data = (RptAccesos)objRptAcceso;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLRptAccesos()
        {
            Data = new RptAccesos();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DARptAccesos DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed RptAccesos Data
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
        public List<RptAccesos> GetRptAccesos(FilAccesos entidad)
        {
            try
            {
                return DataAcces.GetRptAccesos(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptAccesos", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion
    }
}
