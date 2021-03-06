﻿using System;
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
		public List<RptAccesos> GetRptAccesos(FilAccesos entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_Accesos");
				if (entidad != null)
				{
					if (ValidarFechaSQL(entidad.fechaDesde))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaDesde);
					if (ValidarFechaSQL(entidad.fechaHasta))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaHasta);

					string paginasParam = string.Empty;
					if (entidad.listaPaginas.Count > 0)
					{
						foreach (Pagina pagina in entidad.listaPaginas)
							paginasParam += string.Format("{0},", pagina.idPagina);

						paginasParam = paginasParam.Substring(0, paginasParam.Length - 1);
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaPaginas", DbType.String, paginasParam);
					}

					string rolesParam = string.Empty;
					if (entidad.listaRoles.Count != 0)
					{
						foreach (DTRol rol in entidad.listaRoles)
							rolesParam += string.Format("{0},", rol.Nombre);

						rolesParam = rolesParam.Substring(0, rolesParam.Length - 1);
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaRoles", DbType.String, rolesParam);
					}
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<RptAccesos> listaReporte = new List<RptAccesos>();
				RptAccesos objReporte;
				while (reader.Read())
				{
					objReporte = new RptAccesos();

					objReporte.pagina = reader["titulo"].ToString();
					objReporte.fecha = Convert.ToDateTime(reader["fecha"].ToString());
					if (!string.IsNullOrEmpty(reader["RoleName"].ToString()))
						objReporte.rol = reader["RoleName"].ToString();
					else
						objReporte.rol = enumRoles.Anonimo.ToString();
					objReporte.accesos = (int)reader["cantidadAccesos"];
					listaReporte.Add(objReporte);
				}
				return listaReporte;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRptAccesos()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRptAccesos()", ClassName),
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
