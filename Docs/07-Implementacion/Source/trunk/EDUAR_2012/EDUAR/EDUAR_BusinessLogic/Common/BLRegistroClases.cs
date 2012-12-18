using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Common;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_BusinessLogic.Common
{
    public class BLRegistroClases : BusinessLogicBase<RegistroClases, DARegistroClases>
    {
        #region --[Constante]--
        private const string ClassName = "BLRegistroClases";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLRegistroClases(DTBase objRegistroClases)
        {
            Data = (RegistroClases)objRegistroClases;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLRegistroClases()
        {
            Data = new RegistroClases();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DARegistroClases DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed RegistroClases Data
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
                int idRegistroClases = 0;

                if (Data.idRegistroClases == 0)
                    DataAcces.Create(Data, out idRegistroClases);
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
                DataAcces = new DARegistroClases(objDATransaction);
				int idRegistroClases = 0;

                if (Data.idRegistroClases == 0)
					DataAcces.Create(Data, out idRegistroClases);
                else
                    DataAcces.Update(Data);

				if (Data.idRegistroClases > 0) DataAcces.DeleteContenidosDictados(Data.idRegistroClases);
				if (Data.listaDetalleRegistro.Count > 0)
					foreach (DetalleRegistroClases item in Data.listaDetalleRegistro)
						DataAcces.SaveDetalleRegistroClases(idRegistroClases > 0 ? idRegistroClases : Data.idRegistroClases, item.temaContenido.idTemaContenido);
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
                DataAcces = new DARegistroClases(objDATransaction);
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
		/// Obteners the contenidos.
		/// </summary>
		/// <returns></returns>
		public List<TemaContenido> ObtenerContenidos()
		{
			try
			{
				return DataAcces.GetContenidos(Data);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ObtenerContenidos", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion

	}
}
