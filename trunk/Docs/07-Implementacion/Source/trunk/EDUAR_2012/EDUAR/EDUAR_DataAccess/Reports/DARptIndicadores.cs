using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Reports
{
	public class DARptIndicadores : DataAccesBase<RptIndicadores>
	{
		#region --[Atributos]--
		private const string ClassName = "DARptIndicadores";
		#endregion

		#region --[Constructor]--
		public DARptIndicadores()
		{
		}

		public DARptIndicadores(DATransaction objDATransaction)
			: base(objDATransaction)
		{

		}
		#endregion

		#region --[Métodos Públicos]--

		public List<RptIndicadores> GetRptIndicadores(RptIndicadores entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_Indicadores");
				if (entidad != null)
				{
					if (entidad.idCursoCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicoloLectivo", DbType.Int32, entidad.idCursoCicloLectivo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<RptIndicadores> listaReporte = new List<RptIndicadores>();
				RptIndicadores objReporte;
				while (reader.Read())
				{
					objReporte = new RptIndicadores();

					objReporte.alumnoNombre = reader["nombre"].ToString();
					objReporte.alumnoApellido = reader["apellido"].ToString();
					objReporte.idAlumno = int.Parse(reader["idAlumno"].ToString());
					objReporte.promedio = decimal.Parse(reader["Promedio"].ToString());
					objReporte.inasistencias = decimal.Parse(reader["Inasistencias"].ToString());
					objReporte.sanciones = int.Parse(reader["Sanciones"].ToString());

					listaReporte.Add(objReporte);
				}
				return listaReporte;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRptIndicadores", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRptIndicadores", ClassName),
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

		public override RptIndicadores GetById(RptIndicadores entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(RptIndicadores entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(RptIndicadores entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(RptIndicadores entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(RptIndicadores entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

	}
}
