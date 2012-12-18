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
    public class BLPersona : BusinessLogicBase<Persona, DAPersona>
    {
        #region --[Constante]--
        private const string ClassName = "BLPersona";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLPersona(DTBase objPersona)
        {
            Data = (Persona)objPersona;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLPersona()
        {
            Data = new Persona();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAPersona DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed Persona Data
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
                Int32 idPersona = 0;

                if (Data.idPersona == 0)
                    DataAcces.Create(Data, out idPersona);
                else
                    DataAcces.Update(Data);

                //Se da el OK para la transaccion.
                DataAcces.Transaction.CommitTransaction();
            }
            catch (CustomizedException ex)
            {
                DataAcces.Transaction.RollbackTransaction();
                throw ex;
            }
            catch (Exception ex)
            {
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
                DataAcces = new DAPersona(objDATransaction);
                if (Data.idPersona == 0)
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
                DataAcces = new DAPersona(objDATransaction);
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
        /// Gets the personas.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Persona> GetPersonas(Persona entidad)
        {
            try
            {
                return DataAcces.GetPersonas(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPersonas", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

		/// <summary>
		/// Gets the personas.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Persona> GetPersonas(Persona entidad, bool bNoRegistrado)
		{
			try
			{
				return DataAcces.GetPersonas(entidad, bNoRegistrado);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPersonas", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

        /// <summary>
        /// Gets the by id.
        /// </summary>
        public void GetPersonaByEntidad()
        {
            try
            {
                Data = DataAcces.GetPersonaByEntidad(Data);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPersonaByEntidad", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }


        /// <summary>
        /// Validars the registro persona.
        /// </summary>
        public void ValidarRegistroPersona()
        {
            try
            {
                Persona objPersona = new Persona();
                objPersona = DataAcces.GetPersonaByEntidad(Data);
                if (objPersona == null)
                {
                    throw new CustomizedException("Los datos ingresados no han podido ser validados. <br />Por favor, póngase en contacto con el administrador del sistema.", null,
                                                enuExceptionType.SecurityException);
                }
                //return objPersona;
                Data = objPersona;
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - ValidarRegistroPersona", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion
    }
}
