using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
    public class DAAcceso : DataAccesBase<Acceso>
    {
        #region --[Atributos]--
        private const string ClassName = "DAAcceso";
        #endregion

        #region --[Constructor]--
        public DAAcceso()
        {
        }

        public DAAcceso(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--
        public List<Acceso> GetAccesos(Acceso entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Accesos_Select");
                if (entidad != null)
                {
                    if (entidad.idAcceso > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAcceso", DbType.Int32, entidad.idAcceso);
                    if (entidad.pagina.idPagina > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, entidad.pagina.idPagina);
                    if (!string.IsNullOrEmpty(entidad.pagina.titulo))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.pagina.titulo);
                    if (ValidarFechaSQL(entidad.fecha))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
                    if (ValidarFechaSQL(entidad.hora))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Date, entidad.hora);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Acceso> listaAccesos = new List<Acceso>();
                Acceso objAcceso;
                while (reader.Read())
                {
                    objAcceso = new Acceso();

                    objAcceso.idAcceso = Convert.ToInt32(reader["idAcceso"]);
                    objAcceso.pagina.idPagina = Convert.ToInt32(reader["idPagina"]);
                    objAcceso.pagina.titulo = reader["titulo"].ToString();
                    objAcceso.pagina.url = reader["url"].ToString();
                    objAcceso.fecha = Convert.ToDateTime(reader["fecha"].ToString());
                    objAcceso.hora = Convert.ToDateTime(reader["hora"].ToString());

                    listaAccesos.Add(objAcceso);
                }
                return listaAccesos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAccesos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAccesos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
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

        public override Acceso GetById(Acceso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Acceso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Acceso entidad, out int identificador)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Accesos_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAcceso", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha.Date.ToShortDateString());
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, entidad.hora.ToShortTimeString());
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@url", DbType.String, entidad.pagina.url);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.pagina.titulo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idAcceso"].Value.ToString());

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

        public override void Update(Acceso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Acceso entidad)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
