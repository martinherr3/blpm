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
    public class DATipoSancion : DataAccesBase<TipoSancion>
    {
        #region --[Atributos]--
        private const string ClassName = "DATipoSancion";
        #endregion

        #region --[Constructor]--
        public DATipoSancion()
        {
        }

        public DATipoSancion(DATransaction objDATransaction)
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

        public override TipoSancion GetById(TipoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoSancion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TipoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TipoSancion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
		/// <summary>
		/// Gets the tipo sancion.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<TipoSancion> GetTipoSancion(TipoSancion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoSancion_Select");
				if (entidad != null)
				{

				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<TipoSancion> listaTipoSancions = new List<TipoSancion>();
				TipoSancion objTipoSancion;
				while (reader.Read())
				{
					objTipoSancion = new TipoSancion();

					objTipoSancion.idTipoSancion = (int)reader["idTipoSancion"];
					objTipoSancion.descripcion = (string)reader["descripcion"];
					objTipoSancion.nombre = (string)reader["nombre"];

					listaTipoSancions.Add(objTipoSancion);
				}
				return listaTipoSancions;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTipoSancion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTipoSancion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
