using System;
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
using EDUAR_Entities.Security;

namespace EDUAR_BusinessLogic.Encuestas
{
	public class BLEncuesta : BusinessLogicBase<Encuesta, DAEncuesta>
	{
		#region --[Constante]--
		private const string ClassName = "BLEncuesta";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLEncuesta(DTBase objEncuesta)
		{
			Data = (Encuesta)objEncuesta;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLEncuesta()
		{
			Data = new Encuesta();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DAEncuesta DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed Encuesta Data
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
				int idEncuesta = 0;

				if (Data.idEncuesta == 0)
				{
					DataAcces.Create(Data, out idEncuesta);
					foreach (DTRol item in Data.listaRoles)
						DataAcces.SaveRol(item.Nombre, idEncuesta);
				}
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
				DataAcces = new DAEncuesta(objDATransaction);
				if (Data.idEncuesta == 0)
					DataAcces.Create(Data);
				else
					DataAcces.Update(Data);
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

		/// <summary>
		/// Deletes this instance.
		/// </summary>
		public override void Delete()
		{
			try
			{
				DataAcces = new DAEncuesta();
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
				DataAcces = new DAEncuesta(objDATransaction);
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
		/// Obtiene las encuestas disponibles.
		/// </summary>
		/// <param name="objFiltro">The obj filtro.</param>
		/// <returns></returns>
		public List<Encuesta> GetEncuestas(Encuesta objFiltro)
		{
			try
			{
				return DataAcces.GetEncuestas(objFiltro);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestas", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Obtiene las preguntas incluidas en una encuesta dada.
		/// </summary>
		/// <param name="objFiltro">The obj filtro.</param>
		/// <returns></returns>
		public List<Pregunta> GetPreguntasEncuesta(Encuesta objFiltroEncuesta, Pregunta objFiltroPregunta)
		{
			try
			{
				return DataAcces.GetPreguntasEncuesta(objFiltroEncuesta, objFiltroPregunta);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasEncuesta", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Obtiene las categorias relacionadas a una encuesta dada.
		/// </summary>
		/// <param name="objFiltro">The obj filtro.</param>
		/// <returns></returns>
		public List<CategoriaPregunta> GetCategoriasPorEncuesta(Encuesta objFiltroEncuesta)
		{
			try
			{
				return DataAcces.GetCategoriasPorEncuesta(objFiltroEncuesta);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCategoriasPorEncuesta", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the roles ambito.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<DTRol> GetRolesAmbito(AmbitoEncuesta entidad)
		{
			try
			{
				return DataAcces.GetRolesAmbito(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRolesAmbito", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Lanzars the encuesta.
		/// </summary>
		/// <param name="encuesta">The encuesta.</param>
		public void LanzarEncuesta(Encuesta encuesta)
		{
			try
			{
				DataAcces.LanzarEncuesta(encuesta);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - LanzarEncuesta", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Validars the lanzamiento.
		/// </summary>
		public void ValidarLanzamiento()
		{
			try
			{
				if (!Data.activo)
					throw new CustomizedException("No se pudo lanzar la encuesta ya que no se ha activado la misma.", null, enuExceptionType.ValidationException);
				if (!Data.fechaVencimiento.HasValue)
					throw new CustomizedException("No se pudo lanzar la encuesta ya que no se ha cargado la fecha de cierre de la misma.", null, enuExceptionType.ValidationException);
				else
					if (!(Convert.ToDateTime(Data.fechaVencimiento).Subtract(DateTime.Now).Days > 0))
						throw new CustomizedException("No se pudo lanzar la encuesta ya que la fecha de cierre es anterior a la fecha actual.", null, enuExceptionType.ValidationException);
				this.ValidarPreguntas();
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ValidarPreguntas", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Validars the preguntas.
		/// </summary>
		public void ValidarPreguntas()
		{
			try
			{
				if (!DataAcces.ValidarPreguntas(Data))
					throw new CustomizedException("No se pudo lanzar la encuesta ya que no posee preguntas.", null, enuExceptionType.ValidationException);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ValidarPreguntas", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the encuesta analisis.
		/// </summary>
		/// <param name="encuestaSesion">The encuesta sesion.</param>
		/// <returns></returns>
		public EncuestaAnalisis GetEncuestaAnalisis(Encuesta entidad)
		{
			try
			{
				return DataAcces.GetEncuestaAnalisis(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestaAnalisis", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion
	}
}
