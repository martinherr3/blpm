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
    public class DAValorEscala : DataAccesBase<ValorEscalaMedicion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAValorEscala";
        #endregion

        #region --[Constructor]--
        public DAValorEscala()
        {
        }

        public DAValorEscala(DATransaction objDATransaction)
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

        public override ValorEscalaMedicion GetById(ValorEscalaMedicion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(ValorEscalaMedicion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(ValorEscalaMedicion entidad, out int identificador)
        {
            try
            {
                using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ValorEscalaPonderacion_Insert"))
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idValorEscalaPonderacion", DbType.Int32, 0);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, entidad.idEscalaMedicion);

                    if (Transaction.Transaction != null)
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                    else
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idValorEscalaPonderacion"].Value.ToString());
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

        public override void Update(ValorEscalaMedicion entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ValorEscalaPonderacion_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idValorEscalaPonderacion", DbType.Int32, entidad.idValorEscala);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, entidad.idEscalaMedicion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@orden", DbType.Int32, entidad.orden);

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
        public override void Delete(ValorEscalaMedicion entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ValorEscalaPonderacion_Delete");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idValorEscalaPonderacion", DbType.Int32, entidad.idValorEscala);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, entidad.idEscalaMedicion);

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
        /// Gets the escalas de ponderacion.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<ValorEscalaMedicion> GetValoresEscalasMedicion(EscalaMedicion entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ValorEscalaPonderacion_Select");
                if (entidad != null)
                {
                    if (entidad.idEscala > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, entidad.idEscala);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<ValorEscalaMedicion> listaEscalas = new List<ValorEscalaMedicion>();
                ValorEscalaMedicion objValorEscala;

                while (reader.Read())
                {
                    objValorEscala = new ValorEscalaMedicion();

                    objValorEscala.idValorEscala = Convert.ToInt32(reader["idValorEscalaPonderacion"]);
                    objValorEscala.nombre = reader["nombre"].ToString();
                    objValorEscala.descripcion = reader["descripcion"].ToString();
                    objValorEscala.idEscalaMedicion = Convert.ToInt32(reader["idEscalaPonderacion"]);
                    objValorEscala.orden = Convert.ToInt32(reader["orden"]);

                    objValorEscala.eliminable = Convert.ToInt32(reader["cantidadEncuestas"]) == 0;

                    listaEscalas.Add(objValorEscala);
                }
                return listaEscalas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetValoresEscalasMedicion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetValoresEscalasMedicion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
