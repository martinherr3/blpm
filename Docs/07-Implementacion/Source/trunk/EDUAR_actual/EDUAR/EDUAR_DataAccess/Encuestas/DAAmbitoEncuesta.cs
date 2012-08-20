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

namespace EDUAR_DataAccess.Encuestas
{
    public class DAAmbitoEncuesta : DataAccesBase<AmbitoEncuesta>
    {
        #region --[Atributos]--
        private const string ClassName = "DAAmbitoEncuesta";
        #endregion

        #region --[Constructor]--
        public DAAmbitoEncuesta()
        {
        }

        public DAAmbitoEncuesta(DATransaction objDATransaction)
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

        public override AmbitoEncuesta GetById(AmbitoEncuesta entidad)
        {
            try
            {
                if (entidad != null)
                {
                    //El identificador es mandatorio
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbitoEncuesta", DbType.Int32, entidad.idAmbitoEncuesta);
                    
                    if (!string.IsNullOrEmpty(entidad.nombre))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                    if (!string.IsNullOrEmpty(entidad.descripcion))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                AmbitoEncuesta objAmbitoEncuesta = null;
                if (reader.Read())
                {
                    objAmbitoEncuesta = new AmbitoEncuesta();
                    objAmbitoEncuesta.idAmbitoEncuesta = Convert.ToInt32(reader["idAmbito"]);
                    objAmbitoEncuesta.nombre = reader["nombre"].ToString();
                    objAmbitoEncuesta.descripcion = reader["descripcion"].ToString();
                }
                return objAmbitoEncuesta;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Select()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Select()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Create(AmbitoEncuesta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(AmbitoEncuesta entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(AmbitoEncuesta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(AmbitoEncuesta entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the ambitos de encuesta.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<AmbitoEncuesta> GetAmbitosEncuesta(AmbitoEncuesta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AmbitoEncuesta_Select");
                if (entidad != null)
                {
                    if (entidad.idAmbitoEncuesta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbitoEncuesta", DbType.Int32, entidad.idAmbitoEncuesta);
                    if (!string.IsNullOrEmpty(entidad.nombre))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                    if (!string.IsNullOrEmpty(entidad.descripcion))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<AmbitoEncuesta> listaAmbitos = new List<AmbitoEncuesta>();
                AmbitoEncuesta objAmbito;
                while (reader.Read())
                {
                    objAmbito = new AmbitoEncuesta();

                    objAmbito.idAmbitoEncuesta = Convert.ToInt32(reader["idAmbito"]);
                    objAmbito.nombre = reader["nombre"].ToString();
                    objAmbito.descripcion = reader["descripcion"].ToString();

                    listaAmbitos.Add(objAmbito);
                }
                return listaAmbitos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAmbitosEncuesta()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAmbitosEncuesta()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
