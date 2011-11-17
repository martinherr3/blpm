using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_DataAccess.Common;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Shared;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using EDUAR_DataAccess.Shared;

namespace EDUAR_BusinessLogic.Common
{
    public class BLCitacion : BusinessLogicBase<Citacion, DACitacion>
    {
        #region --[Constante]--
        private const string ClassName = "BLCitacion";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLCitacion(DTBase objCitacion)
        {
            Data = (Citacion)objCitacion;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLCitacion()
        {
            Data = new Citacion();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DACitacion DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed Citacion Data
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
                int idCitacion = 0;

                if (Data.idCitacion == 0)
                    DataAcces.Create(Data, out idCitacion);
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
                DataAcces = new DACitacion(objDATransaction);
                if (Data.idCitacion == 0)
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
                DataAcces = new DACitacion(objDATransaction);
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
		/// Gets the citaciones.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Citacion> GetCitaciones(Citacion entidad)
		{
			try
			{
				return DataAcces.GetCitaciones(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCitaciones", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Verificars the disponibilidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public bool VerificarDisponibilidad(Citacion entidad)
		{
			try
			{
				if(!DataAcces.VerificarDisponibilidad(entidad))
					throw new CustomizedException("Las personas involucradas no se encuentran disponibles en el horario y fecha seleccioandas", null,
											  enuExceptionType.ValidationException);
				return true;
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarDisponibilidad", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
        #endregion
	}
}
