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
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.Int32, entidad.nombre);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, entidad.idEscalaMedicion);

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

        public override void Delete(ValorEscalaMedicion entidad)
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

                List<ValorEscalaMedicion> listaEscalas = entidad.valoresEscalas;
                ValorEscalaMedicion objEscala;

                while (reader.Read())
                {
                    objEscala = new ValorEscalaMedicion();

                    objEscala.idValorEscala = Convert.ToInt32(reader["idValorEscalaPonderacion"]);
                    objEscala.nombre = reader["nombre"].ToString();
                    objEscala.descripcion = reader["descripcion"].ToString();
                    //objEscala.idEscalaMedicion = Convert.ToInt32(reader["idEscalaPonderacion"]);

                    listaEscalas.Add(objEscala);
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
