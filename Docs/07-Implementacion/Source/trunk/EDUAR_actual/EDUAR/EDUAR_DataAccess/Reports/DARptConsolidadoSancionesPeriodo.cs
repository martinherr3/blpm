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
    public class DARptConsolidadoSancionesPeriodo : DataAccesBase<RptConsolidadoSancionesPeriodo>
    {
        #region --[Atributos]--
        private const string ClassName = "DARptConsolidadoSancionesPeriodo";
        #endregion

        #region --[Constructor]--
        public DARptConsolidadoSancionesPeriodo()
        {
        }

        public DARptConsolidadoSancionesPeriodo(DATransaction objDATransaction) : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--

        public List<RptConsolidadoSancionesPeriodo> GetRptConsolidadoSanciones(FilIncidenciasAlumno entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_ConsolidadoSancionesPeriodo");
                if (entidad != null)
                {
                    if (entidad.idAlumno > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.idAlumno);

                    if (entidad.idCurso > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.idCurso);

                    if (entidad.idCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.idCicloLectivo);

                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPeriodo", DbType.Int32, entidad.idPeriodo);

					#region --[Tipo Sancion]--
					string tipoSancionParam = string.Empty;
					if (entidad.listaTipoSancion.Count > 0)
					{
						foreach (TipoSancion tipoSancion in entidad.listaTipoSancion)
							tipoSancionParam += string.Format("{0},", tipoSancion.idTipoSancion);

						tipoSancionParam = tipoSancionParam.Substring(0, tipoSancionParam.Length - 1);
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaTiposSancion", DbType.String, tipoSancionParam);
					}
					#endregion

					#region --[Motivo Sancion]--
					string MotivoParam = string.Empty;
					if (entidad.listaMotivoSancion.Count > 0)
					{
						foreach (MotivoSancion motivoSancion in entidad.listaMotivoSancion)
							MotivoParam += string.Format("{0},", motivoSancion.idMotivoSancion);

						MotivoParam = MotivoParam.Substring(0, MotivoParam.Length - 1);
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaMotivoSancion", DbType.String, MotivoParam);
					}
					#endregion
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<RptConsolidadoSancionesPeriodo> listaReporte = new List<RptConsolidadoSancionesPeriodo>();
                RptConsolidadoSancionesPeriodo objReporte;
                while (reader.Read())
                {
                    objReporte = new RptConsolidadoSancionesPeriodo();

                    objReporte.alumno = reader["Alumno"].ToString();
                    objReporte.periodo = reader["Periodo"].ToString();
                    objReporte.sanciones = reader["Sanciones"].ToString();
                    objReporte.motivo = reader["Motivo"].ToString();
                    objReporte.tipo = reader["Tipo"].ToString();


                    listaReporte.Add(objReporte);
                }
                return listaReporte;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptConsolidadoSanciones()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptConsolidadoSanciones()", ClassName),
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

        public override RptConsolidadoSancionesPeriodo GetById(RptConsolidadoSancionesPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptConsolidadoSancionesPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptConsolidadoSancionesPeriodo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(RptConsolidadoSancionesPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(RptConsolidadoSancionesPeriodo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
