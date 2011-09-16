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
    public class DARptPromedioCalificacionesPeriodo : DataAccesBase<RptPromedioCalificacionesPeriodo>
    {
        #region --[Atributos]--
        private const string ClassName = "DARptPromedioCalificaciones";
        #endregion

        #region --[Constructor]--
        public DARptPromedioCalificacionesPeriodo()
        {
        }

        public DARptPromedioCalificacionesPeriodo(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--

        public List<RptPromedioCalificacionesPeriodo> GetRptPromedioCalificaciones(FilPromedioCalificacionesPeriodo entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_PromedioCalificacionesPeriodo");
                if (entidad != null)
                {
                    if (entidad.idAlumno > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.idAlumno);
                    if (entidad.idAsignatura > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.idAsignatura);
                    if (entidad.idCurso > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.idCurso);
					if (entidad.idCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.idCicloLectivo);
                    //if (entidad.idPeriodo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPeriodo", DbType.Int32, entidad.idPeriodo);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<RptPromedioCalificacionesPeriodo> listaReporte = new List<RptPromedioCalificacionesPeriodo>();
                RptPromedioCalificacionesPeriodo objReporte;
                while (reader.Read())
                {
                    objReporte = new RptPromedioCalificacionesPeriodo();
                    
                    objReporte.alumno = reader["Alumno"].ToString();
                    objReporte.asignatura = reader["Asignatura"].ToString();
                    objReporte.periodo = reader["Periodo"].ToString();
                    objReporte.promedio = reader["Promedio"].ToString();
                    
                    listaReporte.Add(objReporte);
                }
                return listaReporte;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptPromedioCalificaciones(1)", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptPromedioCalificaciones(2)", ClassName),
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

        public override RptPromedioCalificacionesPeriodo GetById(RptPromedioCalificacionesPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptPromedioCalificacionesPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptPromedioCalificacionesPeriodo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(RptPromedioCalificacionesPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(RptPromedioCalificacionesPeriodo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
