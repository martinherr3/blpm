using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
    public class DATipoDocumento : DataAccesBase<TipoDocumento>
    {
        #region --[Atributos]--
        private const string ClassName = "DATipoDocumento";
        #endregion

        #region --[Constructor]--
        public DATipoDocumento()
        {
        }

        public DATipoDocumento(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--
        public List<TipoDocumento> GetTipoDocumento(TipoDocumento entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoDocumento_Select");

                if (entidad.idTipoDocumento > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoDocumento", DbType.Int32, entidad.idTipoDocumento);
                if (!string.IsNullOrEmpty(entidad.nombre))
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                if (!string.IsNullOrEmpty(entidad.descripcion))
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                if (entidad.activo != null)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TipoDocumento> listTipoDocumento = new List<TipoDocumento>();
                TipoDocumento objTipoDocumento;
                while (reader.Read())
                {
                    objTipoDocumento = new  TipoDocumento();

                    objTipoDocumento.idTipoDocumento = Convert.ToInt32(reader["idTipoDocumento"]);
                    objTipoDocumento.nombre = reader["nombre"].ToString();
                    objTipoDocumento.descripcion = reader["descripcion"].ToString();
                    objTipoDocumento.activo = Convert.ToBoolean(reader["activo"]);

                    listTipoDocumento.Add(objTipoDocumento);
                }
                return listTipoDocumento;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTipoDocumento()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTipoDocumento()", ClassName),
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

        public override TipoDocumento GetById(TipoDocumento entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoDocumento_Select");

                if (entidad.idTipoDocumento > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersona", DbType.Int32, entidad.idTipoDocumento);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                TipoDocumento tipoDocumento = new TipoDocumento();
                while (reader.Read())
                {
                    tipoDocumento.idTipoDocumento = (int)reader["idTipoDocumento"];
                    tipoDocumento.nombre = reader["nombre"].ToString();
                    tipoDocumento.descripcion = reader["descripcion"].ToString();
                    tipoDocumento.activo = (bool)reader["activo"];
                }
                return tipoDocumento;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetById()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetById()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Create(TipoDocumento entidad)
        {
            throw new NotImplementedException();

        }

        public override void Create(TipoDocumento entidad, out int identificador)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoDocumento_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoDocumento", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoDocumentoTransaccional", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion)", DbType.String, entidad.descripcion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idTipoDocumento"].Value.ToString());


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

        public override void Update(TipoDocumento entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoDocumento_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoDocumento", DbType.Int32, entidad.idTipoDocumento);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoDocumentoTransaccional", DbType.Int32, entidad.idTipoDocumentoTransaccional);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descricion", DbType.String, entidad.descripcion);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
                    throw new CustomizedException(string.Format(
                           "No se puede modificar el Tipo de Documento {0}, debido a que otro usuario lo ha modificado.",
                           entidad.nombre), ex, enuExceptionType.ConcurrencyException);

                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                                      ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                                      ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Delete(TipoDocumento entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoDocumento_Delete");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoDocumento", DbType.Int32, entidad.idTipoDocumento);


                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

            }
            catch (SqlException ex)
            {
                if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
                    throw new CustomizedException(string.Format(
                           "No se puede eliminar el Tipo de Documento {0}, debido a que otro usuario lo ha modificado.",
                           entidad.nombre), ex, enuExceptionType.ConcurrencyException);
                if (ex.Number == BLConstantesGenerales.IntegrityErrorNumber)
                    throw new CustomizedException(string.Format("No se puede eliminar el Tipo de Documento {0}, debido a que tiene registros asociados.",
                                       entidad.nombre), ex, enuExceptionType.IntegrityDataException);


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


    }
}
