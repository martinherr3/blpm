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
    public class DAModulo : DataAccesBase<Modulo>
    {
        #region --[Atributos]--
        private const string ClassName = "DAModulo";
        #endregion

        #region --[Constructor]--
        public DAModulo()
        {
        }

        public DAModulo(DATransaction objDATransaction)
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

        public override Modulo GetById(Modulo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Modulo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Modulo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Modulo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Modulo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
		/// <summary>
		/// Gets the modulos.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Modulo> GetModulos(Modulo entidad)
		{
			try
			{
                using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("HorariosCurso_Select"))
                {
                    if (entidad != null)
                    {
                        if (entidad.idDiaHorario > 0)
                            Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idDiaHorario", DbType.Int32, entidad.idDiaHorario);
                    }
                    IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                    List<Modulo> listaModulos = new List<Modulo>();
                    Modulo objModulo;
                    while (reader.Read())
                    {
                        objModulo = new Modulo();
                        objModulo.idDiaHorario = Convert.ToInt32(reader["idDiaHorario"]);
                        objModulo.idModulo = Convert.ToInt32(reader["idModulo"]);
                        objModulo.horaInicio = Convert.ToDateTime(reader["horaInicio"].ToString());
                        objModulo.horaFinalizacion = Convert.ToDateTime(reader["horaFinalizacion"].ToString());

                        listaModulos.Add(objModulo);
                    }
                    return listaModulos;
                }
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetModulos()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetModulos()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
        #endregion
    }
}
