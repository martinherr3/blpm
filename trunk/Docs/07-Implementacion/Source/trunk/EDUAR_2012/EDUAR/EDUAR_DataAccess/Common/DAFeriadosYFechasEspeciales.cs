using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
	public class DAFeriadosYFechasEspeciales : DataAccesBase<FeriadosYFechasEspeciales>
	{
		#region --[Atributos]--
		private const string ClassName = "DAFeriadosYFechasEspeciales";
		#endregion

		#region --[Constructor]--
		public DAFeriadosYFechasEspeciales()
		{
		}

		public DAFeriadosYFechasEspeciales(DATransaction objDATransaction)
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

		public override FeriadosYFechasEspeciales GetById(FeriadosYFechasEspeciales entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(FeriadosYFechasEspeciales entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(FeriadosYFechasEspeciales entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(FeriadosYFechasEspeciales entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(FeriadosYFechasEspeciales entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Validars the fecha.
		/// </summary>
		/// <param name="fecha">The fecha.</param>
		/// <returns></returns>
		public bool ValidarFecha(DateTime fecha)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Feriados_Select"))
				{
					if (fecha != null)
					{
						if (ValidarFechaSQL(fecha))
							Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, fecha);
					}
					IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

					while (reader.Read())
					{
						return false;
					}
					return true;
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ValidarFecha()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ValidarFecha()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

        public DateTime getHorarioInicio()
        {
            DateTime retHorario = new DateTime();
                
            string HorarioInicio = "";
            string []horaYMinutosInicio;

            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ConfiguracionesEstablecimiento_Select");
      
                // Agregar como parametro de la consulta el id donde se que esta guardado el horario de inicio
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idConfiguracionEstablecimiento", DbType.Int32, 3);
      

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                while (reader.Read())
                {
                    HorarioInicio = reader["valor"].ToString();
                }

                horaYMinutosInicio = HorarioInicio.Split(':');
                retHorario = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, int.Parse(horaYMinutosInicio[0]), int.Parse(horaYMinutosInicio[1]), 0);     
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - getHorarioInicio()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - getHorarioInicio()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            ////////////////////////
            return (retHorario);
        }

        public DateTime getHorarioFinalizacion()
        {
            DateTime retHorario = new DateTime();
            string HorarioFinalizacion = "";
            string[] horaYMinutosFinalizacion;

            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ConfiguracionesEstablecimiento_Select");

                // Agregar como parametro de la consulta el id donde se que esta guardado el horario de finalizacion
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idConfiguracionEstablecimiento", DbType.Int32, 4);


                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                while (reader.Read())
                {
                    HorarioFinalizacion = reader["valor"].ToString();
                }
                horaYMinutosFinalizacion = HorarioFinalizacion.Split(':');
                retHorario = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, int.Parse(horaYMinutosFinalizacion[0]), int.Parse(horaYMinutosFinalizacion[1]), 0);     



            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - getHorarioFinalizacion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - getHorarioFinalizacion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            ////////////////////////


            return (retHorario);
        }
		#endregion
	}
}
