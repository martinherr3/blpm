using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Data;

namespace EDUAR_DataAccess.Common
{
    public class DATipoEventoInstitucional : DataAccesBase<TipoEventoInstitucional>
    {
        #region --[Atributos]--
        private const string ClassName = "DATipoEventoInstitucional";
        #endregion

        #region --[Constructor]--
        public DATipoEventoInstitucional()
        {
        }

        public DATipoEventoInstitucional(DATransaction objDATransaction)
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

        public override TipoEventoInstitucional GetById(TipoEventoInstitucional entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoEventoInstitucional_Select");

                if (entidad.idTipoEventoInstitucional > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEventoInstitucional", DbType.Int32, entidad.idTipoEventoInstitucional);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                TipoEventoInstitucional objTipoEvento = new TipoEventoInstitucional();

                while (reader.Read())
                {
                    objTipoEvento.idTipoEventoInstitucional = (int)(reader["idTipoEventoInstitucional"]);
                    objTipoEvento.descripcion = (string)(reader["descripcion"]);
                    objTipoEvento.activo = (bool)(reader["activo"]);
                }
                return objTipoEvento;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTipoEvento()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTipoEvento()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Create(TipoEventoInstitucional entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoEventoInstitucional entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TipoEventoInstitucional entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TipoEventoInstitucional entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--

        public List<TipoEventoInstitucional> GetTipoEventoInstitucional(TipoEventoInstitucional entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoEventoInstitucional_Select");
                if (entidad != null)
                {
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TipoEventoInstitucional> listTipoEventos = new List<TipoEventoInstitucional>();

                TipoEventoInstitucional objTipoEvento;

                while (reader.Read())
                {
                    objTipoEvento = new TipoEventoInstitucional();

                    objTipoEvento.idTipoEventoInstitucional = Convert.ToInt32(reader["idTipoEventoInstitucional"]);
                    objTipoEvento.descripcion = reader["descripcion"].ToString();
                    objTipoEvento.activo = (bool)(reader["activo"]);

                    listTipoEventos.Add(objTipoEvento);
                }

                return listTipoEventos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTipoEventoInstitucional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTipoEventoInstitucional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }

        }

        #endregion
    }
}
