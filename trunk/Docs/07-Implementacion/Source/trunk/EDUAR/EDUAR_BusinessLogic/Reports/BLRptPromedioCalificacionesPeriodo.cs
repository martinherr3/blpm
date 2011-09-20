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
    public class BLRptPromedioCalificacionesPeriodo : BusinessLogicBase<RptPromedioCalificacionesPeriodo, DARptPromedioCalificacionesPeriodo>
    {
        #region --[Constante]--
        private const string ClassName = "BLRptPromedioCalificaciones";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLRptPromedioCalificacionesPeriodo(DTBase objRptPromedioCalificaciones)
        {
            Data = (RptPromedioCalificacionesPeriodo) objRptPromedioCalificaciones;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLRptPromedioCalificacionesPeriodo()
        {
            Data = new RptPromedioCalificacionesPeriodo();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DARptPromedioCalificacionesPeriodo DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed RptPromedioCalificacionesPeriodo Data
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
		/// <summary>
		/// Gets the RPT calificaciones alumno periodo.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
        public List<RptPromedioCalificacionesPeriodo> GetRptPromedioCalificaciones(FilPromedioCalificacionesPeriodo entidad)
        {
            try
            {
                return DataAcces.GetRptPromedioCalificaciones(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptPromedioCalificaciones", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        public List<RptPromedioCalificacionesAsignaturaCursoPeriodo> GetRptPromedioCalificacionesAsignaturaCurso(FilPromedioCalificacionesPeriodo entidad)
        {
            try
            {
                return DataAcces.GetRptPromedioCalificacionesAsignaturaCurso(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptPromedioCalificacionesAsignaturaCurso", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        
        #endregion
    }
}
