using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities;
using EDUAR_DataAccess.Shared;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;

namespace EDUAR_DataAccess.Common
{
    public class DAEventoInstitucional : DataAccesBase<EventoInstitucional>
    {
        #region --[Atributos]--
        private const string ClassName = "DAEventoInstitucional";
        #endregion

        #region --[Constructor]--
        public DAEventoInstitucional()
        {
        }

        public DAEventoInstitucional(DATransaction objDATransaction)
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

        public override EventoInstitucional GetById(EventoInstitucional entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoInstitucional_Select");

                if (entidad.idEventoInstitucional > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idEventoInstitucional", DbType.Int32, entidad.idEventoInstitucional);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                EventoInstitucional objEvento = new EventoInstitucional();
                while (reader.Read())
                {
                    objEvento.idEventoInstitucional = Convert.ToInt32(reader["idEventoInstitucional"]);
                    objEvento.fecha = Convert.ToDateTime(reader["fecha"].ToString());
                    objEvento.hora = Convert.ToDateTime(reader["hora"].ToString());
                    objEvento.lugar = reader["lugar"].ToString();
                    objEvento.tipoEventoInstitucional = new TipoEventoInstitucional() { idTipoEventoInstitucional = Convert.ToInt32((reader["idTipoEvento"])), descripcion = reader["tipoEvento"].ToString() };
                    objEvento.organizador = new Persona() { idPersona =  Convert.ToInt32(reader["idPersona"]), nombre = reader["nombreOrganizador"].ToString(), apellido = reader["apellidoOrganizador"].ToString() };
                    objEvento.titulo = reader["titulo"].ToString();
                    objEvento.detalle = reader["detalle"].ToString();
                    objEvento.activo = (bool)(reader["activo"]);
                }
                return objEvento;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEventoInstitucional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEventoInstitucional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Create(EventoInstitucional entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoInstitucional_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoInstitucional", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, entidad.tipoEventoInstitucional.idTipoEventoInstitucional);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersona", DbType.Int32, entidad.organizador.idPersona);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, Convert.ToDateTime(entidad.hora).ToShortTimeString());
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@lugar", DbType.String, entidad.lugar);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);

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

        public override void Create(EventoInstitucional entidad, out int identificador)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoInstitucional_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoInstitucional", DbType.Int32,0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, entidad.tipoEventoInstitucional.idTipoEventoInstitucional);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.organizador.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, Convert.ToDateTime(entidad.hora).ToShortTimeString());
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@lugar", DbType.String, entidad.lugar);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idEventoInstitucional"].Value.ToString());

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

        public override void Update(EventoInstitucional entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoInstitucional_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoInstitucional", DbType.Int32, entidad.idEventoInstitucional);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, entidad.tipoEventoInstitucional.idTipoEventoInstitucional);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.organizador.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, Convert.ToDateTime(entidad.hora).ToShortTimeString());
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@lugar", DbType.String, entidad.lugar);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
                    throw new CustomizedException(string.Format(
                           "No se puede modificar el Evento {0}, debido a que otro usuario lo ha modificado.",
                           entidad.titulo), ex, enuExceptionType.ConcurrencyException);

                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                                      ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                                      ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Delete(EventoInstitucional entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        public List<EventoInstitucional> GetEventoInstitucional(EventoInstitucional entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoInstitucional_Select");
                if (entidad != null)
                {
                    if (entidad.idEventoInstitucional > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoInstitucional", DbType.Int32, entidad.idEventoInstitucional);
                    if (entidad.tipoEventoInstitucional.idTipoEventoInstitucional > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, entidad.tipoEventoInstitucional.idTipoEventoInstitucional);
                    if (entidad.organizador.Equals(null))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersona", DbType.String, entidad.organizador.idPersona);
                    if (ValidarFechaSQL(entidad.fecha))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha.ToShortDateString());
                    if (ValidarFechaSQL(entidad.fechaDesde))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaDesde.ToShortDateString());
                    if (ValidarFechaSQL(entidad.fechaHasta))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaHasta.ToShortDateString());
                    if (entidad.hora != null)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, Convert.ToDateTime(String.Format("{0:HH:mm}",entidad.hora)).ToShortTimeString());
                    if (!string.IsNullOrEmpty(entidad.lugar))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@lugar", DbType.String, entidad.lugar);
                    if (!string.IsNullOrEmpty(entidad.titulo))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo);
                    if (!string.IsNullOrEmpty(entidad.detalle))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle);
                    if (entidad.activo)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<EventoInstitucional> listEventoInstitucional = new List<EventoInstitucional>();
                EventoInstitucional objEvento;

                while (reader.Read())
                {
                    objEvento = new EventoInstitucional();

                    objEvento.idEventoInstitucional = Convert.ToInt32(reader["idEventoInstitucional"]);
                    objEvento.fecha = Convert.ToDateTime(reader["fecha"].ToString());
                    if (!string.IsNullOrEmpty(reader["hora"].ToString()))
                        objEvento.hora = Convert.ToDateTime(reader["hora"].ToString());
                    objEvento.lugar = reader["lugar"].ToString();
                    objEvento.tipoEventoInstitucional = new TipoEventoInstitucional();
                    {
                        objEvento.tipoEventoInstitucional.idTipoEventoInstitucional = Convert.ToInt32(reader["idTipoEvento"]);
                        objEvento.tipoEventoInstitucional.descripcion = reader["tipoEvento"].ToString();
                    }
                    objEvento.organizador = new Persona();
                    {
                        objEvento.organizador.idPersona = Convert.ToInt32(reader["idPersona"]);
                        objEvento.organizador.nombre = reader["nombreOrganizador"].ToString();
                        objEvento.organizador.apellido = reader["apellidoOrganizador"].ToString();
                    }
                    objEvento.titulo = reader["titulo"].ToString();
                    objEvento.detalle = reader["detalle"].ToString();
                    objEvento.activo = (bool)(reader["activo"]);
                    listEventoInstitucional.Add(objEvento);
                }
                return listEventoInstitucional;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEventoInstitucional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEventoInstitucional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        #endregion
    }
}
