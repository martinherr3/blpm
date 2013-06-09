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
    public class DARptInasistenciasAlumnoPeriodo : DataAccesBase<RptInasistenciasAlumnoPeriodo>
    {
        #region --[Atributos]--
        private const string ClassName = "DARptInasistenciasAlumnoPeriodo";
        #endregion

        #region --[Constructor]--
        public DARptInasistenciasAlumnoPeriodo()
        {
        }

        public DARptInasistenciasAlumnoPeriodo(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--

        public List<RptInasistenciasAlumnoPeriodo> GetRptInasistenciasAlumnoPeriodo(FilInasistenciasAlumnoPeriodo entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_InasistenciasPeriodoAlumno");
                if (entidad != null)
                {
                    if (entidad.idAlumno > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.idAlumno);
                    if (entidad.idCurso > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.idCurso);
					if (entidad.idCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.idCicloLectivo);
                    if (ValidarFechaSQL(entidad.fechaDesde))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaDesde);
                    if (ValidarFechaSQL(entidad.fechaHasta))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaHasta);

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

                List<RptInasistenciasAlumnoPeriodo> listaReporte = new List<RptInasistenciasAlumnoPeriodo>();
                RptInasistenciasAlumnoPeriodo objReporte;
                while (reader.Read())
                {
                    objReporte = new RptInasistenciasAlumnoPeriodo();

                    objReporte.alumno = reader["Nombre"].ToString();
                    objReporte.curso = reader["Curso"].ToString();
					objReporte.fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    objReporte.motivo = reader["Descripcion"].ToString();
                    
                    listaReporte.Add(objReporte);
                }
                return listaReporte;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptInasistenciasAlumnoPeriodo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptInasistenciasAlumnoPeriodo()", ClassName),
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

        public override RptInasistenciasAlumnoPeriodo GetById(RptInasistenciasAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptInasistenciasAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptInasistenciasAlumnoPeriodo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(RptInasistenciasAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(RptInasistenciasAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
