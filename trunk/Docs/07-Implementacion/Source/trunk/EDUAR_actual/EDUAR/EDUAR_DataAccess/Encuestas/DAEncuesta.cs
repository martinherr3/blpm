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
    public class DAEncuesta : DataAccesBase<Encuesta>
    {
        #region --[Atributos]--
        private const string ClassName = "DAEncuesta";
        #endregion

        #region --[Constructor]--
        public DAEncuesta()
        {
        }

        public DAEncuesta(DATransaction objDATransaction)
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

        public override Encuesta GetById(Encuesta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Encuesta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, DBNull.Value);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombreEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
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

        public override void Create(Encuesta entidad, out int identificador)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, DBNull.Value);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombreEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idEncuesta"].Value.ToString());
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

        public override void Update(Encuesta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DateTime.Now);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombreEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Delete(Encuesta entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the encuestas.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Encuesta> GetEncuestas(Encuesta entidad)
        {
            try
            {   
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_Select");
                if (entidad != null)
                {
                    if (entidad.idEncuesta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
                    if (!entidad.usuario.Equals(null))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@responsable", DbType.String, entidad.usuario.idPersona);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Encuesta> listaEncuestas = new List<Encuesta>();
                Encuesta objEncuesta;
                while (reader.Read())
                {
                    objEncuesta = new Encuesta();

                    objEncuesta.idEncuesta = Convert.ToInt32(reader["idEncuesta"]);
                    objEncuesta.nombreEncuesta = reader["nombre"].ToString();
                    objEncuesta.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"].ToString());
                    objEncuesta.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());
                    
                    objEncuesta.usuario = new Persona();
                    {
                        objEncuesta.usuario.idPersona = Convert.ToInt32(reader["idPersona"]);
                        objEncuesta.usuario.nombre = reader["nombreOrganizador"].ToString();
                        objEncuesta.usuario.apellido = reader["apellidoOrganizador"].ToString();
                    }
                    
                    objEncuesta.activo = Convert.ToBoolean(reader["activo"].ToString());

                    //TODO: (PABLO) Incluir la lista de preguntas de la misma, para eso llamar a GetPreguntasEncuesta en la clase correspondiente
                    objEncuesta.preguntas = GetPreguntasEncuesta(entidad);

                    listaEncuestas.Add(objEncuesta);
                }
                return listaEncuestas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestas()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestas()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the preguntas de una encuesta dada.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Pregunta> GetPreguntasEncuesta(Encuesta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PreguntasEncuestas_Select");
                
                if (entidad != null)
                {
                    if (entidad.idEncuesta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
                }
                
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Pregunta> listaPreguntasEncuesta = new List<Pregunta>();
                Pregunta objPregunta;

                while (reader.Read())
                {
                    objPregunta = new Pregunta();

                    objPregunta.idPregunta = Convert.ToInt32(reader["idPregunta"]);
                    objPregunta.textoPregunta = reader["textoPregunta"].ToString();
                    objPregunta.objetivoPregunta = reader["objetivo"].ToString();

                    objPregunta.categoria = new CategoriaPregunta();
                    {
                        objPregunta.categoria.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                        objPregunta.categoria.nombre = reader["nombre"].ToString();
                        objPregunta.categoria.descripcion = reader["descripcion"].ToString();
                    }

                    objPregunta.escala = new EscalaMedicion();
                    {
                        objPregunta.escala.idEscala = Convert.ToInt32(reader["idEscalaMedicion"]);
                        objPregunta.escala.nombre = reader["nombre"].ToString();
                    }

                    listaPreguntasEncuesta.Add(objPregunta);
                }
                return listaPreguntasEncuesta;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasEncuesta()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasEncuesta()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}