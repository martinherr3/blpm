﻿using System;
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
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);

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
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@objetivo", DbType.String, entidad.objetivo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);

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
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@objetivo", DbType.String, entidad.objetivo);
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
        /// <param name="idAmbito">El ámbito de la encuesta.</param>
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
                    if (entidad.usuario.idPersona > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@responsable", DbType.Int32, entidad.usuario.idPersona);
                    if (entidad.ambito.idAmbitoEncuesta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
                    if (entidad.activo)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activa", DbType.Boolean, entidad.activo);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Encuesta> listaEncuestas = new List<Encuesta>();
                Encuesta objEncuesta;
                while (reader.Read())
                {
                    objEncuesta = new Encuesta();

                    objEncuesta.idEncuesta = Convert.ToInt32(reader["idEncuesta"]);
                    objEncuesta.nombreEncuesta = reader["nombreEncuesta"].ToString();
                    objEncuesta.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"].ToString());
                    objEncuesta.objetivo = reader["objetivo"].ToString();

                    string fechaModificacion = reader["fechaModificacion"].ToString();

                    if (!fechaModificacion.Equals(string.Empty)) objEncuesta.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());

                    objEncuesta.usuario = new Persona();
                    {
                        objEncuesta.usuario.idPersona = Convert.ToInt32(reader["responsable"]);
                        objEncuesta.usuario.username = reader["username"].ToString();
                        objEncuesta.usuario.nombre = reader["nombreOrganizador"].ToString();
                        objEncuesta.usuario.apellido = reader["apellidoOrganizador"].ToString();
                    }

                    objEncuesta.ambito = new AmbitoEncuesta();
                    {
                        objEncuesta.ambito.idAmbitoEncuesta = Convert.ToInt32(reader["idAmbito"]);
                        objEncuesta.ambito.nombre = reader["nombreAmbito"].ToString();
                        //objEncuesta.ambito.descripcion = reader["descripcionAmbito"].ToString();
                    }

                    objEncuesta.activo = Convert.ToBoolean(reader["activa"].ToString());

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
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PreguntasEncuesta_Select");
                
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
                    objPregunta.textoPregunta = reader["pregunta"].ToString();
                    objPregunta.objetivoPregunta = reader["objetivoPregunta"].ToString();
                    objPregunta.ponderacion = Convert.ToDouble(reader["pesoPregunta"]);

                    objPregunta.categoria = new CategoriaPregunta();
                    {
                        objPregunta.categoria.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                        objPregunta.categoria.nombre = reader["categoria"].ToString();
                        //objPregunta.categoria.descripcion = reader["descripcion"].ToString();
                    }

                    objPregunta.escala = new EscalaMedicion();
                    {
                        objPregunta.escala.idEscala = Convert.ToInt32(reader["idEscalaPonderacion"]);
                        objPregunta.escala.nombre = reader["escala"].ToString();
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