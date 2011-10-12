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
	public class BLAgendaActividades : BusinessLogicBase<AgendaActividades, DAAgendaActividades>
	{
		#region --[Constante]--
		private const string ClassName = "BLAgendaActividades";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLAgendaActividades(DTBase objAgendaActividades)
		{
			Data = (AgendaActividades)objAgendaActividades;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLAgendaActividades()
		{
			Data = new AgendaActividades();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DAAgendaActividades DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed AgendaActividades Data
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
				Data.listaEventos = DataAcces.GetEventosAgenda(Data.idAgendaActividad);
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
				int idAgendaActividad = 0;

                if (Data.idAgendaActividad == 0)
					DataAcces.Create(Data, out idAgendaActividad);
				else
					DataAcces.Update(Data);

                BLEvaluacion objBLEvaluacion;
                BLExcursion objBLExcursion;
                BLReunion objBLReunion;

                foreach (Reunion item in Data.listaReuniones)
                {
                    objBLReunion = new BLReunion(item);
                    if (GetReunionesAgenda(item).Count > 0)
                        throw new CustomizedException("Ya existe una reunión para el mismo día.", null, enuExceptionType.ValidationException);
                    objBLReunion.Save(DataAcces.Transaction);
                }

                foreach (Evaluacion item in Data.listaEvaluaciones)
                {
                    objBLEvaluacion = new BLEvaluacion(item);
                    if (GetEvaluacionesAgenda(item).Count > 0)
                        throw new CustomizedException("Ya existe una evaluación para el mismo día y materia.", null, enuExceptionType.ValidationException);
                    objBLEvaluacion.Save(DataAcces.Transaction);
                }

                foreach (Excursion item in Data.listaExcursiones)
                {
                    objBLExcursion = new BLExcursion(item);
                    if (GetExcursionesAgenda(item).Count > 0)
                        throw new CustomizedException("Ya existe una excursión para el mismo día.", null, enuExceptionType.ValidationException);
                    objBLExcursion.Save(DataAcces.Transaction);
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
				DataAcces = new DAAgendaActividades(objDATransaction);
				if (Data.idAgendaActividad == 0)
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
				DataAcces = new DAAgendaActividades(objDATransaction);
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
		/// Gets the agenda actividadess.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<AgendaActividades> GetAgendaActividades(AgendaActividades entidad)
		{
			try
			{
				return DataAcces.GetAgendaActividades(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAgendaActividadess", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the eventos agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Evaluacion> GetEvaluacionesAgenda(Evaluacion entidad)
		{
			try
			{
				return DataAcces.GetEvaluacionesAgenda(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEventosAgenda", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

        /// <summary>
        /// Gets the eventos agenda.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Reunion> GetReunionesAgenda(Reunion entidad)
        {
            try
            {
                return DataAcces.GetReunionesAgenda(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEventosAgenda", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the eventos agenda.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Excursion> GetExcursionesAgenda(Excursion entidad)
        {
            try
            {
                return DataAcces.GetExcursionesAgenda(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetExcursionesAgenda", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        public bool VerificarAgendaEvaluacion(EventoAgenda entidad, int idAsignatura)
        {
            try
            {
                if (!DataAcces.VerificarAgendaEvaluacion(entidad,idAsignatura))
                    throw new CustomizedException("Ya existen eventos agendados para la presente fecha", null,
                                              enuExceptionType.ValidationException);
                return true;
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgenda", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        public bool VerificarAgendaExcursion(EventoAgenda entidad)
        {
            try
            {
                if (!DataAcces.VerificarAgendaExcursiones(entidad))
                    throw new CustomizedException("Ya existen eventos agendados para la presente fecha", null,
                                              enuExceptionType.ValidationException);
                return true;
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgenda", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        public bool VerificarAgendaReuniones(EventoAgenda entidad)
        {
            try
            {
                if (!DataAcces.VerificarAgendaReuniones(entidad))
                    throw new CustomizedException("Ya existen reuniones agendadas para la presente fecha", null,
                                              enuExceptionType.ValidationException);
                return true;
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgendaReuniones", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion
    }
	
}
