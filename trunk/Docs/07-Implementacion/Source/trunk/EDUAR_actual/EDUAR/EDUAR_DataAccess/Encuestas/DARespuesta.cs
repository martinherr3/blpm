using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

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
        public List<RespuestaPreguntaAnalisis> GetRespuestaPreguntaAnalisis(Encuesta entidad, List<DTRol> listaRoles)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_EncuestaAnalisisSumarizado_BIS");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);

                string rolesParam = string.Empty;
                if (listaRoles != null && listaRoles.Count > 0)
                {
                    foreach (DTRol rol in listaRoles)
                        rolesParam += string.Format("{0},", rol.Nombre);

                    rolesParam = rolesParam.Substring(0, rolesParam.Length - 1);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@rolesParam", DbType.String, rolesParam);
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<RespuestaPreguntaAnalisis> listaRepuesta = new List<RespuestaPreguntaAnalisis>();
                RespuestaPreguntaAnalisis objEntidad;

                char[] delimitadorEscalas = { '[', ']' };
                char[] delimitadorValores = { '-' };

                while (reader.Read())
                {
                    objEntidad = new RespuestaPreguntaAnalisis();

                    objEntidad.idPregunta = Convert.ToInt32(reader["idPregunta"]);
                    objEntidad.textoPregunta = reader["textoPregunta"].ToString();
                    objEntidad.idEscalaPonderacion = Convert.ToInt32(reader["idEscalaPonderacion"]);
                    objEntidad.relevancia = Convert.ToDecimal(reader["relevancia"]);
                    objEntidad.cadenaSeleccion = reader["cadenaValores"].ToString();

                    string[] quantities = objEntidad.cadenaSeleccion.Split(delimitadorEscalas);
                    string[] aux;
                    ValoresSeleccionados values;
                    foreach (string valor in quantities)
                    {
                        if (!string.IsNullOrEmpty(valor))
                        {
                            values = new ValoresSeleccionados();

                            aux = valor.Split(delimitadorValores);
                            values.idValorEscala = Convert.ToInt32(aux[0]);
                            values.cantidad = Convert.ToInt32(aux[1]);

                            objEntidad.valoresSeleccionados.Add(values);
                        }
                    } 

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
                throw new CustomizedException(string.Format("Fallo en {0} - GetRespuestaPreguntaAnalisis()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }


        /// <summary>
        /// Gets the respuesta pregunta textual.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<RespuestaPreguntaAnalisis> GetRespuestaPreguntaTextual(Encuesta entidad, List<DTRol> listaRoles)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_EncuestaAnalisisTextuales");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);

                string rolesParam = string.Empty;
                if (listaRoles != null && listaRoles.Count > 0)
                {
                    foreach (DTRol rol in listaRoles)
                        rolesParam += string.Format("{0},", rol.Nombre);

                    rolesParam = rolesParam.Substring(0, rolesParam.Length - 1);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@rolesParam", DbType.String, rolesParam);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<RespuestaPreguntaAnalisis> listaRepuesta = new List<RespuestaPreguntaAnalisis>();
                RespuestaPreguntaAnalisis objEntidad;
                int aux = 0;
                decimal auxDec = 0;
                while (reader.Read())
                {
                    objEntidad = new RespuestaPreguntaAnalisis();

                    objEntidad.idPregunta = Convert.ToInt32(reader["idPregunta"]);
                    objEntidad.textoPregunta = reader["textoPregunta"].ToString();
                    objEntidad.idEscalaPonderacion = 3;
                    int.TryParse(reader["respuestasEsperadas"].ToString(), out aux);
                    objEntidad.respuestasEsperadas = aux;
                    int.TryParse(reader["respuestasObtenidas"].ToString(), out aux);
                    objEntidad.respuestasObtenidas = aux;
                    decimal.TryParse(reader["porcentaje"].ToString(), out auxDec);
                    objEntidad.porcentaje = auxDec;
                    decimal.TryParse(reader["relevancia"].ToString(), out auxDec);
                    objEntidad.relevancia = auxDec;

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
        public List<Respuesta> GetRespuestaTextuales(int encuesta, int pregunta, List<DTRol> listaRoles)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RespuestasTextuales_Select");

                if (encuesta > 0) Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, encuesta);
                if (pregunta > 0) Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, pregunta);

                string rolesParam = string.Empty;
                if (listaRoles != null && listaRoles.Count > 0)
                {
                    foreach (DTRol rol in listaRoles)
                        rolesParam += string.Format("{0},", rol.Nombre);

                    rolesParam = rolesParam.Substring(0, rolesParam.Length - 1);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@rolesParam", DbType.String, rolesParam);
                }

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
