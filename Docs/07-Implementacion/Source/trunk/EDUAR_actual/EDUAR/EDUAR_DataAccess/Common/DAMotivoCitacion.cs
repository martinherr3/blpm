using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
    public class DAMotivoCitacion : DataAccesBase<MotivoCitacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAMotivoCitacion";
        #endregion

        #region --[Constructor]--
        public DAMotivoCitacion()
        {
        }

        public DAMotivoCitacion(DATransaction objDATransaction)
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

        public override MotivoCitacion GetById(MotivoCitacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoCitacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoCitacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(MotivoCitacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(MotivoCitacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
		/// <summary>
		/// Gets the motivos citacion.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<MotivoCitacion> GetMotivosCitacion(MotivoCitacion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MotivoCitacion_Select");
				if (entidad != null)
				{
					
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<MotivoCitacion> listaMotivos = new List<MotivoCitacion>();
				MotivoCitacion objMotivo;
				while (reader.Read())
				{
					objMotivo = new MotivoCitacion();

					objMotivo.idMotivoCitacion = Convert.ToInt32(reader["idMotivoCitacion"]);
					objMotivo.nombre = reader["descripcion"].ToString();

					listaMotivos.Add(objMotivo);
				}
				return listaMotivos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetMotivosCitacion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetMotivosCitacion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
