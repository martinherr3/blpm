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
    public class DATipoRegistroClases : DataAccesBase<TipoRegistroClases>
    {
        #region --[Atributos]--
        private const string ClassName = "DATipoRegistroClases";
        #endregion

        #region --[Constructor]--
        public DATipoRegistroClases()
        {
        }

        public DATipoRegistroClases(DATransaction objDATransaction)
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

        public override TipoRegistroClases GetById(TipoRegistroClases entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoRegistroClases_Select");

                if (entidad.idTipoRegistroClases > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoRegistroClases", DbType.Int32, entidad.idTipoRegistroClases);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                TipoRegistroClases objTipoEvento = new TipoRegistroClases();

                while (reader.Read())
                {
                    objTipoEvento.idTipoRegistroClases = (int)(reader["idTipoRegistroClases"]);
					objTipoEvento.nombre = (string)(reader["nombre"]);
                }
                return objTipoEvento;
            }
            catch (SqlException ex)
            {
				throw new CustomizedException(string.Format("Fallo en {0} - GetById()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
				throw new CustomizedException(string.Format("Fallo en {0} - GetById()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Create(TipoRegistroClases entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoRegistroClases entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TipoRegistroClases entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TipoRegistroClases entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--

        public List<TipoRegistroClases> GetTipoRegistroClases(TipoRegistroClases entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoRegistroClases_Select");
                if (entidad != null)
                {
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TipoRegistroClases> listTipoEventos = new List<TipoRegistroClases>();

                TipoRegistroClases objTipoEvento;

                while (reader.Read())
                {
                    objTipoEvento = new TipoRegistroClases();

                    objTipoEvento.idTipoRegistroClases = Convert.ToInt32(reader["idTipoRegistroClases"]);
					objTipoEvento.nombre = reader["nombre"].ToString();

                    listTipoEventos.Add(objTipoEvento);
                }

                return listTipoEventos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTipoRegistroClases()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTipoRegistroClases()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }

        }

        #endregion
    }
}
