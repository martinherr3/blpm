using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using EDUAR_Utility.Enumeraciones;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
    public class DAExcursion : DataAccesBase<Excursion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAExcursion";
        #endregion

        #region --[Constructor]--
        public DAExcursion()
        {
        }

        public DAExcursion(DATransaction objDATransaction)
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

        public override Excursion GetById(Excursion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Excursion entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Excursion_Insert");

                // Propios del evento de agenda
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, (int)enumEventoAgendaType.Excursion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);

                //Propios de la reunión
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idExcursion", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaDesde", DbType.Time, entidad.horaDesde);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaHasta", DbType.Time, entidad.horaHasta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@destino", DbType.String, entidad.destino);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, 0);

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

        public override void Create(Excursion entidad, out int identificador)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Excursion_Insert");

                // Propios del evento de agenda
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, (int)enumEventoAgendaType.Excursion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);

                //Propios de la reunión
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idExcursion", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaDesde", DbType.Time, entidad.horaDesde);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaHasta", DbType.Time, entidad.horaHasta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@destino", DbType.String, entidad.destino);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, 0);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idExcursion"].Value.ToString());
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

        public override void Update(Excursion entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Excursion_Update");

                // Propios del evento de agenda
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, (int)enumEventoAgendaType.Reunion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaAlta", DbType.Date, entidad.fechaAlta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DateTime.Now);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);

                //Propios de la excursión
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idExcursion", DbType.Int32, entidad.idExcursion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaDesde", DbType.Time, entidad.horaDesde);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaHasta", DbType.Time, entidad.horaHasta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@destino", DbType.String, entidad.destino);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, entidad.idEventoAgenda);

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

        public override void Delete(Excursion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        public List<Excursion> GetExcursiones(Excursion entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Excursion_Select");
                if (entidad != null)
                {
                    if (entidad.idExcursion > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idExcursion", DbType.Int32, entidad.idExcursion);
                    if (entidad.idEventoAgenda > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEvento", DbType.Int32, entidad.idEventoAgenda);
                    if (entidad.horaDesde != null)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaDesde", DbType.Time, Convert.ToDateTime(entidad.horaDesde).ToShortTimeString());
                    if (entidad.horaDesde != null)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaHasta", DbType.Time, Convert.ToDateTime(entidad.horaHasta).ToShortTimeString());

                    if (entidad.activo)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
                    if (ValidarFechaSQL(entidad.fechaEventoDesde))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaEventoDesde);
                    if (ValidarFechaSQL(entidad.fechaEventoHasta))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaEventoHasta);
                    if (entidad.idAgendaActividad > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Excursion> listExcursiones = new List<Excursion>();

                Excursion objEvento;

                while (reader.Read())
                {
                    objEvento = new Excursion();

                    objEvento.idExcursion = Convert.ToInt32(reader["idExcursion"]);
                    if (!string.IsNullOrEmpty(reader["horaDesde"].ToString()))
                        objEvento.horaDesde = Convert.ToDateTime(reader["horaDesde"].ToString());
                    if (!string.IsNullOrEmpty(reader["horaHasta"].ToString()))
                        objEvento.horaHasta = Convert.ToDateTime(reader["horaHasta"].ToString());
                    objEvento.idEventoAgenda = Convert.ToInt32(reader["idEvento"]);
                    objEvento.destino = reader["destino"].ToString();


                    objEvento.activo = Convert.ToBoolean(reader["activo"]);
                    objEvento.descripcion = reader["descripcion"].ToString();
                    objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"]);
                    objEvento.fechaAlta = Convert.ToDateTime(reader["fechaAlta"]);
                    objEvento.tipoEventoAgenda = new TipoEventoAgenda { idTipoEventoAgenda = Convert.ToInt32(reader["idTipoEvento"]), descripcion = reader["tipoEvento"].ToString() };
                    objEvento.usuario = new Persona() { idPersona = Convert.ToInt32(reader["idOrganizador"]), nombre = reader["nombreOrganizador"].ToString(), apellido = reader["apellidoOrganizador"].ToString() };

                    objEvento.idAgendaActividad = Convert.ToInt32(reader["idAgendaActividades"]);

                    listExcursiones.Add(objEvento);
                }
                return listExcursiones;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetExcursiones()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetExcursiones()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
