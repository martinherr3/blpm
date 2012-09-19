using System;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
	public class DANovedad : DataAccesBase<Novedad>
	{
		#region --[Atributos]--
		private const string ClassName = "DANovedad";
		#endregion

		#region --[Constructor]--
		public DANovedad()
		{
		}

		public DANovedad(DATransaction objDATransaction)
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

		public override Novedad GetById(Novedad entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Novedad entidad)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="identificador">The identificador.</param>
		public override void Create(Novedad entidad, out int identificador)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("NovedadAulica_Insert");

				Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idNovedadAulica", DbType.Int32, 2);
				if (entidad.novedadPadre != null && entidad.novedadPadre.idNovedad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNovedadAulicaPrincipal", DbType.Int32, entidad.novedadPadre.idNovedad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuarioCreador", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, DateTime.Today);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, DateTime.Now);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@observaciones", DbType.String, entidad.observaciones);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.curso.idCurso);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoNovedad", DbType.Int32, entidad.tipo.idTipoNovedad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEstadoNovedad", DbType.Int32, entidad.estado.idEstadoNovedad);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

				identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idNovedadAulica"].Value.ToString());

			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		public override void Update(Novedad entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Novedad entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		#endregion
	}
}
