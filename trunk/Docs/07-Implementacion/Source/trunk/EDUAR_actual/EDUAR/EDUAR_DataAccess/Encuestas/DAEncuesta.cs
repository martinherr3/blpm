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

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, 0);
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

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, 0);
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
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DateTime.Now);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombreEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@objetivo", DbType.String, entidad.objetivo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

                foreach (Pregunta pregunta in entidad.preguntas)
                {
                    if(pregunta.idPregunta == 0)
                        AgregarPregunta(entidad.idEncuesta, pregunta);
                    else ActualizarPregunta(entidad.idEncuesta, pregunta);
                }

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
            try
            {
                if(entidad.preguntas.Count == 1)
                {
                    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Pregunta_Delete");

                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, entidad.preguntas[0].idPregunta);

                    if (Transaction.Transaction != null)
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                    else
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
                }
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

                    objEncuesta.preguntas = GetPreguntasEncuesta(entidad,null);

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
        public List<Pregunta> GetPreguntasEncuesta(Encuesta entidad, Pregunta pregunta)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Pregunta_Select");
                
                if (entidad != null)
                {
                    if (entidad.idEncuesta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
                    if (pregunta != null)
                    {
                        if (pregunta.categoria.idCategoriaPregunta > 0) Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, pregunta.categoria.idCategoriaPregunta);
                        if (pregunta.escala.idEscala > 0) Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscala", DbType.Int32, pregunta.escala.idEscala);
                    }
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
                    objPregunta.ponderacion = Convert.ToDouble(reader["peso"]);

                    objPregunta.categoria = new CategoriaPregunta();
                    {
                        objPregunta.categoria.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                        objPregunta.categoria.nombre = reader["categoria"].ToString();
                        //objPregunta.categoria.descripcion = reader["descripcionCategoria"].ToString();
                    }

                    objPregunta.escala = new EscalaMedicion();
                    {
                        objPregunta.escala.idEscala = Convert.ToInt32(reader["idEscalaPonderacion"]);
                        objPregunta.escala.nombre = reader["escala"].ToString();
                        objPregunta.escala.descripcion = reader["descripcionEscala"].ToString();
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

        /// <summary>
        /// Crea una pregunta, la cual queda asociada a una encuesta dada.
        /// </summary>
        /// <param name="encuesta">The encuesta.</param>
        /// <param name="pregunta">The pregunta.</param>
        public void AgregarPregunta(int idEncuesta, Pregunta pregunta)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Pregunta_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Double, idEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, pregunta.categoria.idCategoriaPregunta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, pregunta.escala.idEscala);

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@textoPregunta", DbType.String, pregunta.textoPregunta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@objetivo", DbType.String, pregunta.objetivoPregunta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@peso", DbType.Double, pregunta.ponderacion);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - AgregarPregunta()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - AgregarPregunta()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }

        }

        /// <summary>
        /// Actualiza el contenido de la pregunta asociada a la encuesta.
        /// </summary>
        /// <param name="encuesta">The encuesta.</param>
        /// <param name="pregunta">The pregunta.</param>
        public void ActualizarPregunta(int idEncuesta, Pregunta pregunta)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Pregunta_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, pregunta.idPregunta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Double, idEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, pregunta.categoria.idCategoriaPregunta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, pregunta.escala.idEscala);

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@textoPregunta", DbType.String, pregunta.textoPregunta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@objetivo", DbType.String, pregunta.objetivoPregunta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@peso", DbType.Double, pregunta.ponderacion);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - AgregarPregunta()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - AgregarPregunta()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public List<CategoriaPregunta> GetCategoriasPorEncuesta(Encuesta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CategoriasEncuesta_Select");

                if (entidad != null)
                {
                    if (entidad.idEncuesta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<CategoriaPregunta> listaCategoriasPreguntas = new List<CategoriaPregunta>();
                CategoriaPregunta objCategoriaPregunta;

                while (reader.Read())
                {
                    objCategoriaPregunta = new CategoriaPregunta();

                    objCategoriaPregunta.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                    objCategoriaPregunta.nombre = reader["nombre"].ToString();
                    objCategoriaPregunta.descripcion = reader["descripcion"].ToString();
                    
                    objCategoriaPregunta.ambito = new AmbitoEncuesta();
                    {
                        objCategoriaPregunta.ambito.idAmbitoEncuesta = Convert.ToInt32(reader["idAmbito"]);
                        objCategoriaPregunta.ambito.nombre = reader["nombreAmbito"].ToString();
                        objCategoriaPregunta.ambito.descripcion = reader["descripcionAmbito"].ToString();
                    }

                    listaCategoriasPreguntas.Add(objCategoriaPregunta);
                }
                return listaCategoriasPreguntas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCategoriasPorEncuesta()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCategoriasPorEncuesta()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}