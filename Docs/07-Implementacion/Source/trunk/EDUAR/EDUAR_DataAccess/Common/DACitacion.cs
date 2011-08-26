using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
	public class DACitacion : DataAccesBase<Citacion>
	{
		#region --[Atributos]--
		private const string ClassName = "DACitacion";
		#endregion

		#region --[Constructor]--
		public DACitacion()
		{
		}

		public DACitacion(DATransaction objDATransaction)
			: base(objDATransaction)
		{

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

		public override Citacion GetById(Citacion entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Citacion entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Citacion entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(Citacion entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Citacion entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the citaciones.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Citacion> GetCitaciones(Citacion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Citacion_Select");
				if (entidad != null)
				{
					if (entidad.idCitacion > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCitacion", DbType.Int32, entidad.idCitacion);
					if (entidad.motivoCitacion.idMotivoCitacion > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMotivoCitacion", DbType.Int32, entidad.motivoCitacion.idMotivoCitacion);
					if (entidad.tutor.idTutor > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTutor", DbType.Int32, entidad.tutor.idTutor);
					if (!string.IsNullOrEmpty(entidad.organizador.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuario", DbType.String, entidad.organizador.username);
					if (!string.IsNullOrEmpty(entidad.detalles))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalles);
					if (entidad.activo)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Citacion> listaCitaciones = new List<Citacion>();
				Citacion objCitacion;
				while (reader.Read())
				{
					objCitacion = new Citacion();
					objCitacion.idCitacion = Convert.ToInt32(reader["idCitacion"]);
					objCitacion.detalles = reader["detalle"].ToString();
					objCitacion.fecha = Convert.ToDateTime(reader["fecha"].ToString());
					if (!string.IsNullOrEmpty(reader["hora"].ToString()))
						objCitacion.hora = Convert.ToDateTime(reader["hora"].ToString());
					objCitacion.organizador.IdPersonal = Convert.ToInt32(reader["idOrganizador"]);
					objCitacion.organizador.nombre = reader["nombreOrganizador"].ToString();
					objCitacion.organizador.apellido = reader["apellidoOrganizador"].ToString();
					objCitacion.tutor.idTutor = Convert.ToInt32(reader["idTutor"]);
					objCitacion.tutor.nombre = reader["nombreTutor"].ToString();
					objCitacion.tutor.apellido = reader["apellidoTutor"].ToString();
					objCitacion.motivoCitacion.idMotivoCitacion = Convert.ToInt32(reader["idMotivoCitacion"]);
					objCitacion.motivoCitacion.nombre = reader["motivoCitacion"].ToString();

					listaCitaciones.Add(objCitacion);
				}
				return listaCitaciones;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCitaciones()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCitaciones()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion

	}
}
