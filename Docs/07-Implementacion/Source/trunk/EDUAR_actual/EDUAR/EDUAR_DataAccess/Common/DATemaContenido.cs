using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Data;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
    public class DATemaContenido : DataAccesBase<TemaContenido>
    {
        #region --[Atributos]--
        private const string ClassName = "DATemaContenido";
        #endregion

        #region --[Constructor]--
        public DATemaContenido()
        {
        }

        public DATemaContenido(DATransaction objDATransaction)
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

        public override TemaContenido GetById(TemaContenido entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TemaContenido entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <param name="identificador">The identificador.</param>
        public override void Create(TemaContenido entidad, out int identificador)
        {
            try
            {
                using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaContenido_Insert"))
                {

                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@obligatorio", DbType.Boolean, entidad.obligatorio);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo.Trim());
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idContenido", DbType.Int32, entidad.idContenido);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, 0);

                    if (!string.IsNullOrEmpty(entidad.detalle))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle.Trim());
                    else
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, DBNull.Value);

                    if (Transaction.Transaction != null)
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                    else
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idTemaContenido"].Value.ToString());
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

        /// <summary>
        /// Updates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        public override void Update(TemaContenido entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaContenido_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@obligatorio", DbType.Boolean, entidad.obligatorio);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo.Trim());
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idContenido", DbType.Int32, entidad.idContenido);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, entidad.idTemaContenido);
                if (!string.IsNullOrEmpty(entidad.detalle))
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle.Trim());
                else
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, DBNull.Value);

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

        /// <summary>
        /// Deletes the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        public override void Delete(TemaContenido entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaContenido_Delete");

                if (entidad.idContenido > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idContenido", DbType.Int32, entidad.idContenido);
                if (entidad.idTemaContenido > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, entidad.idTemaContenido);

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usernameBaja", DbType.String, entidad.usuarioBaja.username);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the tema contenidos.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<TemaContenido> GetTemaContenidos(TemaContenido entidad)
        {
            try
            {
                return GetTemaContenidos(entidad, new Curricula());
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemaContenidos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemaContenidos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the tema contenidos by Tema Planificacion.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<TemaContenido> GetTemaContenidosAtrasados(TemaPlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaContenidoByTemaPlanificacionAtrasado_Select");
                if (entidad != null && entidad.idTemaPlanificacion > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, entidad.idTemaPlanificacion);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TemaContenido> listaContenidos = new List<TemaContenido>();
                TemaContenido objContenido;
                while (reader.Read())
                {
                    objContenido = new TemaContenido();
                    objContenido.idContenido = Convert.ToInt32(reader["idContenido"]);
                    objContenido.idTemaContenido = Convert.ToInt32(reader["idTemaContenido"]);
                    objContenido.detalle = reader["detalle"].ToString();
                    objContenido.titulo = reader["titulo"].ToString();
                    objContenido.obligatorio = Convert.ToBoolean(reader["obligatorio"]);
                    listaContenidos.Add(objContenido);
                }
                return listaContenidos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemaContenidos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemaContenidos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the tema contenidos.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        ///         /// <param name="objCurricula">The Curricula.</param>
        /// <returns></returns>
        public List<TemaContenido> GetTemaContenidos(TemaContenido entidad, Curricula objCurricula)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaContenido_Select");
                if (entidad != null)
                {
                    if (!string.IsNullOrEmpty(entidad.titulo))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo);
                    if (entidad.idTemaContenido > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, entidad.idTemaContenido);
                    if (!string.IsNullOrEmpty(entidad.detalle))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle);
                    if (entidad.idContenido > 0)
                    {
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idContenido", DbType.Int32, entidad.idContenido);
                    }
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
                }
                if (objCurricula != null)
                {
                    if (objCurricula.idCurricula > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, objCurricula.idCurricula);
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TemaContenido> listaContenidos = new List<TemaContenido>();
                TemaContenido objContenido;
                while (reader.Read())
                {
                    objContenido = new TemaContenido();
                    objContenido.idContenido = Convert.ToInt32(reader["idContenido"]);
                    objContenido.idTemaContenido = Convert.ToInt32(reader["idTemaContenido"]);
                    objContenido.detalle = reader["detalle"].ToString();
                    objContenido.titulo = reader["contenido"].ToString() + " - " + reader["titulo"].ToString();
                    objContenido.activo = Convert.ToBoolean(reader["activo"]);
                    objContenido.obligatorio = Convert.ToBoolean(reader["obligatorio"]);
                    listaContenidos.Add(objContenido);
                }
                return listaContenidos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemaContenidos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemaContenidos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the tema contenidos.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<TemaContenido> GetTemaContenidos(TemaContenido entidad,TemaPlanificacionAnual objTemaPlanificacionAnual)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaContenidoPorTemaPlanificacion_Select");
                if (entidad != null)
                {
                    if (!string.IsNullOrEmpty(entidad.titulo))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo);
                    if (entidad.idTemaContenido > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, entidad.idTemaContenido);
                    if (!string.IsNullOrEmpty(entidad.detalle))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle);
                    if (entidad.idContenido > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idContenido", DbType.Int32, entidad.idContenido);
                    if (entidad.activo)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

                }

                if (objTemaPlanificacionAnual != null && objTemaPlanificacionAnual.idTemaPlanificacion > 0)
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, objTemaPlanificacionAnual.idTemaPlanificacion);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TemaContenido> listaContenidos = new List<TemaContenido>();
                TemaContenido objContenido;
                while (reader.Read())
                {
                    objContenido = new TemaContenido();
                    objContenido.idContenido = Convert.ToInt32(reader["idContenido"]);
                    objContenido.idTemaContenido = Convert.ToInt32(reader["idTemaContenido"]);
                    objContenido.detalle = reader["detalle"].ToString();
                    objContenido.titulo = reader["contenido"].ToString() + " - " + reader["titulo"].ToString();
                    objContenido.activo = Convert.ToBoolean(reader["activo"]);
                    objContenido.obligatorio = Convert.ToBoolean(reader["obligatorio"]);
                    listaContenidos.Add(objContenido);
                }
                return listaContenidos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemaContenidos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemaContenidos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the contenidos planificados.
        /// </summary>
        /// <param name="objAsignatura">The obj asignatura.</param>
        /// <returns></returns>
        public List<TemaContenido> GetContenidosPlanificados(AsignaturaCicloLectivo entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ContenidosPlanificados_Select");

                if (entidad != null)
                    if (entidad.idAsignaturaCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.idAsignaturaCicloLectivo);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TemaContenido> listaContenidos = new List<TemaContenido>();
                TemaContenido objContenido;
                while (reader.Read())
                {
                    objContenido = new TemaContenido();
                    objContenido.idContenido = Convert.ToInt32(reader["idContenido"]);
                    objContenido.idTemaContenido = Convert.ToInt32(reader["idTemaContenido"]);
                    objContenido.detalle = reader["detalle"].ToString();
                    objContenido.titulo = reader["titulo"].ToString();
                    objContenido.obligatorio = Convert.ToBoolean(reader["obligatorio"]);
                    listaContenidos.Add(objContenido);
                }
                return listaContenidos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidosPlanificados()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidosPlanificados()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
