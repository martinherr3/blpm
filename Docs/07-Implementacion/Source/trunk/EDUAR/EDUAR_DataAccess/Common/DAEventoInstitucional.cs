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
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "id_evento_institucional", DbType.Int32, entidad.idEventoInstitucional);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                EventoInstitucional objEvento = new EventoInstitucional();
                while (reader.Read())
                {
                    objEvento.idEventoInstitucional = Convert.ToInt32(reader["id_evento_institucional"]);
                    //objEvento.tipoEventoInstitucional = Convert.ToInt32(reader["tipo_ev_institucional"]);
                    //objEvento.organizador = reader["organizador"].ToString();
                    objEvento.fecha = Convert.ToDateTime(reader["fecha"].ToString());
                    objEvento.lugar = reader["lugar"].ToString();
                    if (!string.IsNullOrEmpty(reader["tipo_ev_institucional"].ToString()))
                        objEvento.tipoEventoInstitucional = new TipoEventoInstitucional() { idTipoEventoInstitucional = Convert.ToInt32(reader["tipo_ev_institucional"]) };
                    //if (!string.IsNullOrEmpty(reader["organizador"].ToString()))
                    //    objEvento.organizador = new Personal() { IdPersonal = Convert.ToInt32(reader["organizador"]) };
                    objEvento.descripcionBreve = reader["descripcionBreve"].ToString();
                    objEvento.detalle = reader["detalle"].ToString();
                    objEvento.activo = Convert.ToBoolean(reader["activo"]);
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

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@id_evento_institucional", DbType.Int32, 0);
                //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@tipo_ev_institucional", DbType.String, entidad.tipoEventoInstitucional);
                //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@organizador", DbType.String, entidad.organizador);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.DateTime, entidad.fecha);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@lugar", DbType.String, entidad.lugar);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcionBreve", DbType.String, entidad.descripcionBreve);
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

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@id_evento_institucional", DbType.Int32, 0);
                //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@tipo_ev_institucional", DbType.String, entidad.tipoEventoInstitucional);
                //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@organizador", DbType.String, entidad.organizador);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.DateTime, entidad.fecha);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@lugar", DbType.String, entidad.lugar);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcionBreve", DbType.String, entidad.descripcionBreve);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@id_evento_institucional"].Value.ToString());

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

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@id_evento_institucional", DbType.Int32, 0);
                //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@tipo_ev_institucional", DbType.String, entidad.tipoEventoInstitucional);
                //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@organizador", DbType.String, entidad.organizador);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.DateTime, entidad.fecha);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@lugar", DbType.String, entidad.lugar);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcionBreve", DbType.String, entidad.descripcionBreve);
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
                           entidad.descripcionBreve), ex, enuExceptionType.ConcurrencyException);

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
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoInstitucional_Delete");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@id_evento_institucional", DbType.Int32, 0);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

            }
            catch (SqlException ex)
            {
                if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
                    throw new CustomizedException(string.Format(
                           "No se puede eliminar el evento {0}, debido a que otro usuario lo ha modificado.",
                           entidad.descripcionBreve), ex, enuExceptionType.ConcurrencyException);
                
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
        public List<EventoInstitucional> GetEventoInstitucional(EventoInstitucional entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoInstitucional_Select");

                if (entidad.idEventoInstitucional > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@id_evento_institucional", DbType.Int32, entidad.idEventoInstitucional);
                //if (entidad.tipoEventoInstitucional.idTipoEventoInstitucional > 0)
                //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@tipo_ev_institucional", DbType.Int32, entidad.tipoEventoInstitucional.idTipoEventoInstitucional);
                //if (entidad.organizador.Equals(null))
                //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@organizador", DbType.String, entidad.organizador.);
                //if (entidad.fecha.Year>0)
                //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
                if (!string.IsNullOrEmpty(entidad.lugar))
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@lugar", DbType.String, entidad.lugar);
                if (!string.IsNullOrEmpty(entidad.descripcionBreve))
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcionBreve", DbType.String, entidad.descripcionBreve);
                if (!string.IsNullOrEmpty(entidad.detalle))
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalle);
                if (entidad.activo)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<EventoInstitucional> listEventoInstitucional = new List<EventoInstitucional>();
                EventoInstitucional objEventoInstitucional;
               
                while (reader.Read())
                {
                    objEventoInstitucional = new EventoInstitucional();

                    objEventoInstitucional.idEventoInstitucional = Convert.ToInt32(reader["id_evento_institucional"]);
                    TipoEventoInstitucional tipoEv = new TipoEventoInstitucional();
                    //tipoEv.idTipoEventoInstitucional = Convert.ToInt32(reader["tipo_ev_institucional"]);
                    objEventoInstitucional.tipoEventoInstitucional = tipoEv;
                    objEventoInstitucional.fecha = Convert.ToDateTime(reader["fecha"].ToString());
                    objEventoInstitucional.lugar = Convert.ToString(reader["lugar"]);
                    objEventoInstitucional.descripcionBreve = Convert.ToString(reader["descripcionBreve"]);
                    objEventoInstitucional.detalle = Convert.ToString(reader["detalle"]);

                    bool activo = Convert.ToBoolean(reader["activo"]);
                    
                    objEventoInstitucional.activo = activo;
                    
                    //Provisoriamente lo manejo de este modo
                    //objEventoInstitucional.organizador = null;

                    if(activo.Equals(true)) listEventoInstitucional.Add(objEventoInstitucional);
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
