using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.Reports;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Reports
{
    public class DARptEncuestasPorEstado : DataAccesBase<RptEncuestasPorEstado>
    {
        #region --[Atributos]--
        private const string ClassName = "DARptEncuestasPorEstado";
        #endregion

        #region --[Constructor]--
        public DARptEncuestasPorEstado()
        {
        }

        public DARptEncuestasPorEstado(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--

        public List<RptEncuestasPorEstado> GetRptEncuestasPorEstado(FilEncuestasPorEstado entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_EncuestasPorStatus");
                if (entidad != null)
                {
                    if (entidad.idCurso > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.idCurso);
                    if (entidad.idAsignatura > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.idAsignatura);

					#region --[Ambitos]--
					string tipoAmbitoParam = string.Empty;
					if (entidad.listaAmbitos.Count > 0)
					{
						foreach (AmbitoEncuesta ambitoEncuesta in entidad.listaAmbitos)
                            tipoAmbitoParam += string.Format("{0},", ambitoEncuesta.idAmbitoEncuesta);

						tipoAmbitoParam = tipoAmbitoParam.Substring(0, tipoAmbitoParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaAmbitos", DbType.String, tipoAmbitoParam);
					}
					#endregion
				}
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<RptEncuestasPorEstado> listaReporte = new List<RptEncuestasPorEstado>();
                RptEncuestasPorEstado objReporte;
                while (reader.Read())
                {
                    objReporte = new RptEncuestasPorEstado();

                    objReporte.status = reader["Status"].ToString();
					objReporte.total = Convert.ToInt32(reader["Total"]);
                    objReporte.ambito = reader["Ambito"].ToString();
                    
                    listaReporte.Add(objReporte);
                }
                return listaReporte;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptEncuestasPorEstado()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptEncuestasPorEstado()", ClassName),
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

        public override RptEncuestasPorEstado GetById(RptEncuestasPorEstado entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptEncuestasPorEstado entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptEncuestasPorEstado entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(RptEncuestasPorEstado entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(RptEncuestasPorEstado entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
