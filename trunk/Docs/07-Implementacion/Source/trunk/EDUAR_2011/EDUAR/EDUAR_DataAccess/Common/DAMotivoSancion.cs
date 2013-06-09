using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
    public class DAMotivoSancion : DataAccesBase<MotivoSancion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAMotivoSancion";
        #endregion

        #region --[Constructor]--
        public DAMotivoSancion()
        {
        }

        public DAMotivoSancion(DATransaction objDATransaction)
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

        public override MotivoSancion GetById(MotivoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoSancion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(MotivoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(MotivoSancion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
		/// <summary>
		/// Gets the motivo sanciones.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<MotivoSancion> GetMotivoSanciones(MotivoSancion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MotivoSancion_Select");
				if (entidad != null)
				{

				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<MotivoSancion> listaMotivosSancion = new List<MotivoSancion>();
				MotivoSancion objMotivoSancion;
				while (reader.Read())
				{
					objMotivoSancion = new MotivoSancion();

					objMotivoSancion.idMotivoSancion = (int)reader["idMotivoSancion"];
					objMotivoSancion.descripcion = (string)reader["descripcion"];

					listaMotivosSancion.Add(objMotivoSancion);
				}
				return listaMotivosSancion;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetMotivoSanciones()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetMotivoSanciones()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
