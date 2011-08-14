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
    public class DAEventoAgenda : DataAccesBase<EventoAgenda>
    {
        #region --[Atributos]--
        private const string ClassName = "DAEventoAgenda";
        #endregion

        #region --[Constructor]--
        public DAEventoAgenda()
        {
        }

        public DAEventoAgenda(DATransaction objDATransaction)
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

        public override EventoAgenda GetById(EventoAgenda entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoAgenda_Select");

                if (entidad.idEventoAgenda > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idEventoAgenda", DbType.Int32, entidad.idEventoAgenda);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                EventoAgenda objEvento = new EventoAgenda();
                while (reader.Read())
                {
                    objEvento.idEventoAgenda = Convert.ToInt32(reader["idEventoAgenda"]);
                    objEvento.idAgendaActividad = Convert.ToInt32(reader["idAgendaActividades"]);
                    objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"].ToString());
                    objEvento.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());
                    objEvento.descripcion = reader["descripcion"].ToString();
                    objEvento.tipoEventoAgenda = new TipoEventoAgenda() { idTipoEventoAgenda = Convert.ToInt32((reader["idTipoEvento"])), descripcion = reader["tipoEvento"].ToString() };
                    objEvento.usuario = new Persona() { idPersona = Convert.ToInt32(reader["idPersona"]), nombre = reader["username"].ToString(), apellido = reader["apellidoOrganizador"].ToString() };
                    objEvento.activo = (bool)(reader["activo"]);
                }
                return objEvento;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEventoAgenda()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEventoAgenda()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Create(EventoAgenda entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoAgenda_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, entidad.tipoEventoAgenda.idTipoEventoAgenda);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuario", DbType.Int32, entidad.usuario.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, entidad.fechaModificacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
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

        public override void Create(EventoAgenda entidad, out int identificador)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoAgenda_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, entidad.tipoEventoAgenda.idTipoEventoAgenda);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuario", DbType.Int32, entidad.usuario.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, entidad.fechaModificacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idEventoAgenda"].Value.ToString());

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

        public override void Update(EventoAgenda entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(EventoAgenda entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        public List<EventoAgenda> GetEventoAgenda(EventoAgenda entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoAgenda_Select");
                if (entidad != null)
                {
                    if (entidad.idEventoAgenda > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, entidad.idEventoAgenda);
                    if (entidad.tipoEventoAgenda.idTipoEventoAgenda > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, entidad.tipoEventoAgenda.idTipoEventoAgenda);
                    if (entidad.usuario.Equals(null))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
                    if (ValidarFechaSQL(entidad.fechaEvento))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento.ToShortDateString());
                    if (!string.IsNullOrEmpty(entidad.descripcion))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                    if (entidad.activo)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<EventoAgenda> listEventoAgenda = new List<EventoAgenda>();
                EventoAgenda objEvento;

                while (reader.Read())
                {
                    objEvento = new EventoAgenda();

                    objEvento.idEventoAgenda = Convert.ToInt32(reader["idEventoAgenda"]);
                    objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"].ToString());
                    objEvento.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());
                    objEvento.descripcion = reader["descripcion"].ToString();
                    objEvento.activo = (bool)(reader["activo"]);

                    objEvento.tipoEventoAgenda = new TipoEventoAgenda();
                    {
                        objEvento.tipoEventoAgenda.idTipoEventoAgenda = Convert.ToInt32(reader["idTipoEvento"]);
                        objEvento.tipoEventoAgenda.descripcion = reader["tipoEvento"].ToString();
                    }
                    
                    objEvento.usuario  = new Persona();
                    {
                        objEvento.usuario.idPersona = Convert.ToInt32(reader["idPersona"]);
                        objEvento.usuario.nombre = reader["nombreOrganizador"].ToString();
                        objEvento.usuario.apellido = reader["apellidoOrganizador"].ToString();
                    }
                    
					//switch (objEvento.tipoEventoAgenda.idTipoEventoAgenda)
					//{
					//    case (int)enumEventoAgendaType.Evaluacion:
					//        Evaluacion eventoEvaluacion = (Evaluacion) objEvento.evento;

					//        //eventoEvaluacion.asignatura = null;
					//        //eventoEvaluacion.idEvaluacion = (int)reader["idEvaluacion"];

					//        break;
					//    case (int)enumEventoAgendaType.Reunion:
					//        Reunion eventoReunion = (Reunion)objEvento.evento;

					//        eventoReunion.idReunion = Convert.ToInt32(reader["idEvento"]);
					//        eventoReunion.horario = Convert.ToDateTime(reader["hora"].ToString());
                            
					//        break;
					//    case (int)enumEventoAgendaType.Excursion:
					//        Excursion eventoExcursion = (Excursion)objEvento.evento;

					//        //eventoExcursion.horaDesde = Convert.ToDateTime(reader["horaDesde"].ToString());
					//        //eventoExcursion.horaHasta = Convert.ToDateTime(reader["horaHasta"].ToString());
					//        //eventoExcursion.destino = reader["destino"].ToString();
					//        //eventoExcursion.idExcursion = (int)reader["idExcursion"];

					//        break;
					//    default:
					//        objEvento.evento = null;
					//        break;
					//}

                    listEventoAgenda.Add(objEvento);
                }
                return listEventoAgenda;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEventoAgenda()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEventoAgenda()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        #endregion
    }
}
