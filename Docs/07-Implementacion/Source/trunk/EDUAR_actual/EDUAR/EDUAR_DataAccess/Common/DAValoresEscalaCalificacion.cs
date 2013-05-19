using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Data.SqlClient;
using System.Data;

namespace EDUAR_DataAccess.Common
{
    public class DAValoresEscalaCalificacion : DataAccesBase<ValoresEscalaCalificacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAValoresEscalaCalificacion";
        #endregion

        #region --[Constructor]--
        public DAValoresEscalaCalificacion()
        {
        }

        public DAValoresEscalaCalificacion(DATransaction objDATransaction)
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

        public override ValoresEscalaCalificacion GetById(ValoresEscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(ValoresEscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(ValoresEscalaCalificacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(ValoresEscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(ValoresEscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
		/// <summary>
		/// Gets the nivel probacion.
		/// </summary>
		/// <returns></returns>
		public ValoresEscalaCalificacion GetNivelProbacion()
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("NivelAprobacion_Select");

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				ValoresEscalaCalificacion valorEscala;
				while (reader.Read())
				{
					valorEscala = new ValoresEscalaCalificacion();
					valorEscala.nombre = reader["nombre"].ToString();
					valorEscala.idValorEscalaCalificacion = Convert.ToInt32(reader["idValorEscalaCalificacion"]);
					valorEscala.descripcion = reader["descripcion"].ToString();
					valorEscala.aprobado =(bool)reader["aprobado"];
					valorEscala.activo =(bool)reader["activo"];
					valorEscala.valor = reader["valor"].ToString();

					return valorEscala;
				}
				return null;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetNivelProbacion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetNivelProbacion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
        #endregion
    }
}
