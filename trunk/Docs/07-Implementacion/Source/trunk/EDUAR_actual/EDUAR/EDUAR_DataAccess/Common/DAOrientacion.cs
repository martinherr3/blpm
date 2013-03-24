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
    public class DAOrientacion : DataAccesBase<Orientacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAOrientacion";
        #endregion

        #region --[Constructor]--
        public DAOrientacion()
        {
        }

        public DAOrientacion(DATransaction objDATransaction)
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

        public override Orientacion GetById(Orientacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Orientacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Orientacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Orientacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Orientacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the orientaciones by asignatura nivel.
        /// </summary>
        /// <param name="objAsignatura">The obj asignatura.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<Orientacion> GetOrientacionesByAsignaturaNivel(AsignaturaNivel entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AsignaturaNivel_Select");
                if (entidad != null)
                {
                    if (entidad.asignatura.idAsignatura > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.asignatura.idAsignatura);
                    if (entidad.nivel.idNivel > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, entidad.nivel.idNivel);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Orientacion> listaOrientacion = new List<Orientacion>();
                Orientacion objOrientacion;
                while (reader.Read())
                {
                    objOrientacion = new Orientacion();
                    objOrientacion.idOrientacion = Convert.ToInt32(reader["idOrientacion"]);
                    objOrientacion.nombre = reader["Orientacion"].ToString();

                    listaOrientacion.Add(objOrientacion);
                }
                return listaOrientacion;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetOrientacionesByAsignaturaNivel()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetOrientacionesByAsignaturaNivel()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
