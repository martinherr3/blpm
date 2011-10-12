using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
    public class DAPeriodo : DataAccesBase<Periodo>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPeriodo";
        #endregion

        #region --[Constructor]--
        public DAPeriodo()
        {
        }

        public DAPeriodo(DATransaction objDATransaction)
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

        public override Periodo GetById(Periodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Periodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Periodo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Periodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Periodo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--

        public List<Periodo> GetPeriodos(Periodo entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Periodo_Select");
                if (entidad != null)
                {
                    if (entidad.idPeriodo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPeriodo", DbType.Int32, entidad.idPeriodo);
                    if (entidad.cicloLectivo.idCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.cicloLectivo.idCicloLectivo);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Periodo> listaPeriodos = new List<Periodo>();
                Periodo objPeriodo;
                while (reader.Read())
                {
                    objPeriodo = new Periodo();

                    objPeriodo.idPeriodo = Convert.ToInt32(reader["idPeriodo"]);
                    objPeriodo.nombre = reader["nombre"].ToString();

                    listaPeriodos.Add(objPeriodo);
                }
                return listaPeriodos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturas()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturas()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        #endregion
    }
}
