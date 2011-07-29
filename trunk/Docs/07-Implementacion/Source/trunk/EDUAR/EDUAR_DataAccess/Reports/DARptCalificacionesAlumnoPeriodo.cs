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
    public class DARptCalificacionesAlumnoPeriodo : DataAccesBase<RptCalificacionesAlumnoPeriodo>
    {
        #region --[Atributos]--
        private const string ClassName = "DARptCalificacionesAlumnoPeriodo";
        #endregion

        #region --[Constructor]--
        public DARptCalificacionesAlumnoPeriodo()
        {
        }

        public DARptCalificacionesAlumnoPeriodo(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--
        public List<RptCalificacionesAlumnoPeriodo> GetRptCalificacionesAlumnoPeriodo(FilCalificacionesAlumnoPeriodo entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_CalificacionesPeriodoAlumno");
                if (entidad != null)
                {
                    if (entidad.idAlumno > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.idAlumno);
                    if (entidad.idAsignatura > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.idAsignatura);
                    if (entidad.idCurso > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.idCurso);
                    if (entidad.idInstanciaEvaluacion > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idInstanciaEvaluacion", DbType.Int32, entidad.idInstanciaEvaluacion);
                    if (ValidarFechaSQL(entidad.fechaDesde))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaDesde);
                    if (ValidarFechaSQL(entidad.fechaHasta))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaHasta);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<RptCalificacionesAlumnoPeriodo> listaReporte = new List<RptCalificacionesAlumnoPeriodo>();
                RptCalificacionesAlumnoPeriodo objReporte;
                while (reader.Read())
                {
                    objReporte = new RptCalificacionesAlumnoPeriodo();

                    objReporte.alumno = reader["Nombre"].ToString();
                    objReporte.asignatura = reader["Asignatura"].ToString();
                    objReporte.curso = reader["Curso"].ToString();
                    objReporte.calificacion = reader["Calificacion"].ToString();
                    objReporte.instancia = reader["Instancia"].ToString();
                    objReporte.fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    
                    listaReporte.Add(objReporte);
                }
                return listaReporte;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptCalificacionesAlumnoPeriodo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptCalificacionesAlumnoPeriodo()", ClassName),
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

        public override RptCalificacionesAlumnoPeriodo GetById(RptCalificacionesAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptCalificacionesAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptCalificacionesAlumnoPeriodo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(RptCalificacionesAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(RptCalificacionesAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
