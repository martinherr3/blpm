﻿using System;
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
    public class DARptSancionesAlumnoPeriodo : DataAccesBase<RptSancionesAlumnoPeriodo>
    {
        #region --[Atributos]--
        private const string ClassName = "DARptSancionesAlumnoPeriodo";
        #endregion

        #region --[Constructor]--
        public DARptSancionesAlumnoPeriodo()
        {
        }

        public DARptSancionesAlumnoPeriodo(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--

        public List<RptSancionesAlumnoPeriodo> GetRptSancionesAlumnoPeriodo(FilSancionesAlumnoPeriodo entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_SancionesPeriodoAlumno");
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
					if (!string.IsNullOrEmpty(entidad.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.username);

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

                List<RptSancionesAlumnoPeriodo> listaReporte = new List<RptSancionesAlumnoPeriodo>();
                RptSancionesAlumnoPeriodo objReporte;
                while (reader.Read())
                {
                    objReporte = new RptSancionesAlumnoPeriodo();

                    objReporte.alumno = reader["Nombre"].ToString();
					objReporte.fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    objReporte.cantidad = Convert.ToInt32(reader["Cantidad"]);
                    objReporte.tipo = reader["Tipo"].ToString();
                    objReporte.motivo = reader["Motivo"].ToString();
                    
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

        public override RptSancionesAlumnoPeriodo GetById(RptSancionesAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptSancionesAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(RptSancionesAlumnoPeriodo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(RptSancionesAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(RptSancionesAlumnoPeriodo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
