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
	public class DANivel : DataAccesBase<Nivel>
	{
		#region --[Atributos]--
		private const string ClassName = "DANivel";
		#endregion

		#region --[Constructor]--
		public DANivel()
		{
		}

		public DANivel(DATransaction objDATransaction)
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

		public override Nivel GetById(Nivel entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Nivel entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Nivel entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(Nivel entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Nivel entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the by curso ciclo lectivo.
		/// </summary>
		/// <param name="idCicloLectivo">The id ciclo lectivo.</param>
		/// <returns></returns>
		public List<Nivel> GetByCursoCicloLectivo(CicloLectivo entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("NivelesCursoCicloLectivo_Select");
				if (entidad.idCicloLectivo > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.idCicloLectivo);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Nivel> listaNiveles = new List<Nivel>();
				Nivel objNivel;
				while (reader.Read())
				{
					objNivel = new Nivel();

					objNivel.idNivel = Convert.ToInt32(reader["idNivel"]);
					objNivel.nombre = reader["nombre"].ToString();

					listaNiveles.Add(objNivel);
				}
				return listaNiveles;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetByCursoCicloLectivo()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetByCursoCicloLectivo()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

        /// <summary>
        /// Gets the niveles.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<Nivel> GetNiveles()
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("NivelesCursoCicloLectivo_Select");

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Nivel> listaNiveles = new List<Nivel>();
                Nivel objNivel;
                while (reader.Read())
                {
                    objNivel = new Nivel();

                    objNivel.idNivel = Convert.ToInt32(reader["idNivel"]);
                    objNivel.nombre = reader["nombre"].ToString();

                    listaNiveles.Add(objNivel);
                }
                return listaNiveles;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetNiveles()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetNiveles()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
		#endregion

	}
}
