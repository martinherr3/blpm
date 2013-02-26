using System;
using System.Collections.Generic;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Common;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.DEC;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using EDUAR_Entities;

namespace EDUAR_BusinessLogic.Common
{
	public class BLIndicador : BusinessLogicBase<Indicador, DAIndicador>
	{
		#region --[Constante]--
		private const string ClassName = "BLIndicador";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLIndicador(DTBase objIndicador)
		{
			Data = (Indicador)objIndicador;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLIndicador()
		{
			Data = new Indicador();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DAIndicador DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed Indicador Data
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

				BLConfigFuncionPreferencia objBLConfig;
				foreach (ConfigFuncionPreferencia item in Data.listaConfig)
				{
					objBLConfig = new BLConfigFuncionPreferencia(item);
					objBLConfig.Save();
				}

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
				DataAcces = new DAIndicador(objDATransaction);
				if (Data.idIndicador == 0)
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
				DataAcces = new DAIndicador(objDATransaction);
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
		/// Gets the indicadores.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Indicador> GetIndicadores(Indicador entidad)
		{
			try
			{
				List<Indicador> listaIndicadores = DataAcces.GetIndicadores(entidad);

				BLConfigFuncionPreferencia objBLConfiguracion = new BLConfigFuncionPreferencia();
				ConfigFuncionPreferencia config = null;

				foreach (Indicador item in listaIndicadores)
				{
					config = new ConfigFuncionPreferencia();
					config.idIndicador = item.idIndicador;
					item.listaConfig = objBLConfiguracion.GetConfigFuncionPreferencias(config);
				}
				return listaIndicadores;
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadores", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the indicadores.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public Indicador GetIndicador(Indicador entidad)
		{
			try
			{
				Indicador miIndicador = DataAcces.GetIndicador(entidad);
				ConfigFuncionPreferencia config = null;
				BLConfigFuncionPreferencia objBLConfiguracion = new BLConfigFuncionPreferencia();

				config = new ConfigFuncionPreferencia();
				config.idIndicador = miIndicador.idIndicador;

				miIndicador.listaConfig = objBLConfiguracion.GetConfigFuncionPreferencias(config);

				return miIndicador;
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadores", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion
	}
}