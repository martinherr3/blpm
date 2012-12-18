using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.Reports;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Reports
{
    public class DAInformeIndicador : DataAccesBase<InformeIndicador>
    {
        #region --[Atributos]--
        private const string ClassName = "DAInformeIndicador";
        #endregion

        #region --[Constructor]--
        public DAInformeIndicador()
        {
        }

        public DAInformeIndicador(DATransaction objDATransaction)
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

        public override InformeIndicador GetById(InformeIndicador entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(InformeIndicador entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(InformeIndicador entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        public override void Update(InformeIndicador entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(InformeIndicador entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the valor indicador.
        /// </summary>
        /// <param name="nombreSP">The nombre SP.</param>
        /// <param name="idCursoCicloLectivo">The id curso ciclo lectivo.</param>
        /// <param name="fechaDesde">The fecha desde.</param>
        /// <param name="fechaHasta">The fecha hasta.</param>
        /// <returns></returns>
        public DataTable GetInformeIndicador(string nombreSP, int idCursoCicloLectivo, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(nombreSP))
                {
                    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand(nombreSP);

                    if (idCursoCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, idCursoCicloLectivo);
                    if (ValidarFechaSQL(fechaDesde))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@FechaDesde", DbType.Date, fechaDesde);
                    if (ValidarFechaSQL(fechaHasta))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@FechaHasta", DbType.Date, fechaHasta);

                    DataSet ds = Transaction.DataBase.ExecuteDataSet(Transaction.DBcomand);

                    if (ds.Tables.Count > 0)
                        dt = ds.Tables[0];

                }
                return dt;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetInformeIndicador()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetInformeIndicador()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
