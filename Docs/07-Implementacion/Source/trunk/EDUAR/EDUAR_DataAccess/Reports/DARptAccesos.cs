using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using EDUAR_Entities.Security;

namespace EDUAR_DataAccess.Reports
{
    public class DARptAccesos : DataAccesBase<RptAccesos>
    {
        #region --[Atributos]--
        private const string ClassName = "DARptAcceso";
        #endregion

        #region --[Constructor]--
        public DARptAccesos()
        {
        }

        public DARptAccesos(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--
        public List<RptAccesos> GetRptAccesos(RptAccesos entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_Accesos");
                if (entidad != null)
                {
                    if (entidad.idAcceso > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAcceso", DbType.Int32, entidad.idAcceso);
                    if (entidad.idPagina > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, entidad.idPagina);
                    if (ValidarFechaSQL(entidad.fecha))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
                    if (ValidarFechaSQL(entidad.hora))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, entidad.hora);
                    if (ValidarFechaSQL(entidad.fechaDesde))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaDesde);
                    if (ValidarFechaSQL(entidad.fechaHasta))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaHasta);

                    string rolesParam = string.Empty;
                    if (entidad.listaRoles.Count != 0)
                    {
                        foreach (DTRol rol in entidad.listaRoles)
                            rolesParam += string.Format("'{0}',", rol.Nombre);

                        rolesParam = rolesParam.Substring(0, rolesParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaRoles", DbType.String, rolesParam);
                    }
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<RptAccesos> listaAccesos = new List<RptAccesos>();
                RptAccesos objAcceso;
                while (reader.Read())
                {
                    objAcceso = new RptAccesos();

                    objAcceso.idAcceso = Convert.ToInt32(reader["idAcceso"]);
                    objAcceso.titulo = reader["titulo"].ToString();
                    objAcceso.fecha = Convert.ToDateTime(reader["fecha"].ToString());
                    objAcceso.hora = Convert.ToDateTime(reader["hora"].ToString());
                    objAcceso.username = reader["username"].ToString();

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

        public override RptAccesos GetById(RptAccesos entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptAccesos entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptAccesos entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(RptAccesos entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(RptAccesos entidad)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
