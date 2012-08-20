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
    public class DAEscala : DataAccesBase<EscalaMedicion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAEscala";
        #endregion

        #region --[Constructor]--
        public DAEscala()
        {
        }

        public DAEscala(DATransaction objDATransaction)
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

        public override EscalaMedicion GetById(EscalaMedicion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(EscalaMedicion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(EscalaMedicion entidad, out int identificador)
        {
            try
            {
                using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EscalaPonderacion_Insert"))
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, 0);

                    if (Transaction.Transaction != null)
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                    else
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idEscalaPonderacion"].Value.ToString());
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

        public override void Update(EscalaMedicion entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EscalaPonderacion_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, entidad.idEscala);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.Int32, entidad.nombre);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);

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

        public override void Delete(EscalaMedicion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the escalas de ponderacion.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<EscalaMedicion> GetEscalasMedicion(EscalaMedicion entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EscalaMedicion_Select");
                if (entidad != null)
                {
                    if (entidad.idEscala > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscala", DbType.Int32, entidad.idEscala);
                    if (!string.IsNullOrEmpty(entidad.nombre))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.Int32, entidad.nombre);
                    if (!string.IsNullOrEmpty(entidad.descripcion))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.Int32, entidad.descripcion);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<EscalaMedicion> listaEscalas = new List<EscalaMedicion>();
                EscalaMedicion objEscala;

                while (reader.Read())
                {
                    objEscala = new EscalaMedicion();

                    objEscala.idEscala = Convert.ToInt32(reader["idEscala"]);
                    objEscala.nombre = reader["nombre"].ToString();
                    objEscala.nombre = reader["descripcion"].ToString();

                    listaEscalas.Add(objEscala);
                }
                return listaEscalas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEscalasMedicion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetEscalasMedicion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
