using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Data;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Encuestas
{
    public class DARespuesta : DataAccesBase<Respuesta>
    {
        #region --[Atributos]--
        private const string ClassName = "DARespuesta";
        #endregion

        #region --[Constructor]--
        public DARespuesta()
        {
        }

        public DARespuesta(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Implementación métodos heredados]--
        public override string FieldID
        {
            get { throw new NotImplementedException(); }
        }

        public override string FieldDescription
        {
            get { throw new NotImplementedException(); }
        }

        public override Respuesta GetById(Respuesta entidad)
        {
            throw new NotImplementedException();
        }

		/// <summary>
		/// Creates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
        public override void Create(Respuesta entidad)
        {
			try
			{
				int identificador = 0;
				this.Create(entidad, out identificador);
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
        }

		/// <summary>
		/// Creates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="identificador">The identificador.</param>
        public override void Create(Respuesta entidad, out int identificador)
        {
            try
            {
                using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RespuestaPregunta_Insert"))
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idRespuesta", DbType.Int32, 0);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, entidad.pregunta.idPregunta);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.encuestaDisponible.usuario.username);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.encuestaDisponible.encuesta.idEncuesta);
                    if (entidad.respuestaSeleccion == 0) Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@valorRespuestaTextual", DbType.String, entidad.respuestaTextual);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@valorRespuestaSeleccion", DbType.Int32, entidad.respuestaSeleccion);
                    //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@respondida", DbType.Boolean, true);
                    //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaRespuesta", DbType.DateTime, DateTime.Now);

                    if (Transaction.Transaction != null)
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                    else
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idRespuesta"].Value.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Update(Respuesta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Respuesta entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
		/// <summary>
		/// Gets the respuesta pregunta analisis.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<RespuestaPreguntaAnalisis> GetRespuestaPreguntaAnalisis(Encuesta entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_EncuestaAnalisisSumarizado");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<RespuestaPreguntaAnalisis> listaRepuesta = new List<RespuestaPreguntaAnalisis>();
				RespuestaPreguntaAnalisis objEntidad;

				while (reader.Read())
				{
					objEntidad = new RespuestaPreguntaAnalisis();

					objEntidad.idPregunta = Convert.ToInt32(reader["idPregunta"]);
					objEntidad.textoPregunta = reader["textoPregunta"].ToString();
					objEntidad.idEscalaPonderacion = Convert.ToInt32(reader["idEscalaPonderacion"]);
                    objEntidad.relevancia = Convert.ToDecimal(reader["relevancia"]);
					objEntidad.cant1 = Convert.ToInt32(reader["cant1"]);
					objEntidad.cant2 = Convert.ToInt32(reader["cant2"]);
					objEntidad.cant3 = Convert.ToInt32(reader["cant3"]);
					objEntidad.cant4 = Convert.ToInt32(reader["cant4"]);
					objEntidad.cant5 = Convert.ToInt32(reader["cant5"]);

					listaRepuesta.Add(objEntidad);
				}
				return listaRepuesta;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRespuestaPreguntaAnalisis()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestasDisponibles()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}


		/// <summary>
		/// Gets the respuesta pregunta textual.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<RespuestaPreguntaAnalisis> GetRespuestaPreguntaTextual(Encuesta entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_EncuestaAnalisisTextuales");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<RespuestaPreguntaAnalisis> listaRepuesta = new List<RespuestaPreguntaAnalisis>();
				RespuestaPreguntaAnalisis objEntidad;

				while (reader.Read())
				{
					objEntidad = new RespuestaPreguntaAnalisis();

					objEntidad.idPregunta = Convert.ToInt32(reader["idPregunta"]);
					objEntidad.textoPregunta = reader["textoPregunta"].ToString();
					objEntidad.idEscalaPonderacion = 3;
					objEntidad.respuestasEsperadas = Convert.ToInt32(reader["respuestasEsperadas"]);
					objEntidad.respuestasObtenidas = Convert.ToInt32(reader["respuestasObtenidas"]);
					objEntidad.porcentaje = Convert.ToDecimal(reader["porcentaje"]);
					objEntidad.relevancia = Convert.ToDecimal(reader["relevancia"]);

					listaRepuesta.Add(objEntidad);
				}
				return listaRepuesta;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRespuestaPreguntaTextual()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRespuestaPreguntaTextual()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

        /// <summary>
        /// Gets the respuesta textuales.
        /// </summary>
        /// <param name="encuesta">The encuesta.</param>
        /// <param name="pregunta">The pregunta.</param>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Respuesta> GetRespuestaTextuales(int encuesta, int pregunta)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RespuestasTextuales_Select");

                if(encuesta>0) Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, encuesta);
                if(pregunta>0) Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, pregunta);
                
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Respuesta> listaRepuesta = new List<Respuesta>();
                Respuesta objEntidad;

                while (reader.Read())
                {
                    objEntidad = new Respuesta();

                    objEntidad.encuestaDisponible.encuesta.idEncuesta = Convert.ToInt32(reader["idEncuesta"]);
                    objEntidad.pregunta.idPregunta = Convert.ToInt32(reader["idPregunta"]);
                    objEntidad.respuestaTextual = reader["valorRespuestaTextual"].ToString();        

                    listaRepuesta.Add(objEntidad);
                }
                return listaRepuesta;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRespuestaTextuales()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRespuestaTextuales()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
		#endregion

	}
}
