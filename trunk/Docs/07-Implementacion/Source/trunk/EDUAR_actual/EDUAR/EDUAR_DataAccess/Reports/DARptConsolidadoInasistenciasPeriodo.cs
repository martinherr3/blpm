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
    public class DARptConsolidadoInasistenciasPeriodo : DataAccesBase<RptConsolidadoInasistenciasPeriodo>
    {
        #region --[Atributos]--
        private const string ClassName = "DARptConsolidadoInasistenciasPeriodo";
        #endregion

        #region --[Constructor]--
        public DARptConsolidadoInasistenciasPeriodo()
        {
        }

        public DARptConsolidadoInasistenciasPeriodo(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--

        public List<RptConsolidadoInasistenciasPeriodo> GetRptConsolidadoInasistencias(FilIncidenciasAlumno entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_ConsolidadoInasistenciasPeriodo");
                if (entidad != null)
                {
                    if (entidad.idAlumno > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.idAlumno);

                    if (entidad.idCurso > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.idCurso);

                    if (entidad.idCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.idCicloLectivo);

                    if (entidad.idPeriodo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPeriodo", DbType.Int32, entidad.idPeriodo);

                    string asistenciasParam = string.Empty;
                    if (entidad.listaTiposAsistencia.Count > 0)
                    {
                        foreach (TipoAsistencia asistencia in entidad.listaTiposAsistencia)
                            asistenciasParam += string.Format("{0},", asistencia.idTipoAsistencia);

                        asistenciasParam = asistenciasParam.Substring(0, asistenciasParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaTiposAsistencia", DbType.String, asistenciasParam);
                    }
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<RptConsolidadoInasistenciasPeriodo> listaReporte = new List<RptConsolidadoInasistenciasPeriodo>();
                RptConsolidadoInasistenciasPeriodo objReporte;
                while (reader.Read())
                {
                    objReporte = new RptConsolidadoInasistenciasPeriodo();

                    objReporte.alumno = reader["Alumno"].ToString();
                    objReporte.periodo = reader["Periodo"].ToString();
                    objReporte.inasistencias = Convert.ToDouble(reader["Inasistencias"]);
                    objReporte.nivel = reader["Nivel"].ToString();
                    objReporte.motivo = reader["Motivo"].ToString();

                    listaReporte.Add(objReporte);
                }
                return listaReporte;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptConsolidadoInasistencias()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptConsolidadoInasistencias()", ClassName),
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

        public override RptConsolidadoInasistenciasPeriodo GetById(RptConsolidadoInasistenciasPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptConsolidadoInasistenciasPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptConsolidadoInasistenciasPeriodo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(RptConsolidadoInasistenciasPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(RptConsolidadoInasistenciasPeriodo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
