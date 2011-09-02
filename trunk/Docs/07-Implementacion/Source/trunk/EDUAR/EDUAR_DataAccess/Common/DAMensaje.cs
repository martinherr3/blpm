using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
    public class DAMensaje : DataAccesBase<Mensaje>
    {
        #region --[Atributos]--
        private const string ClassName = "DAMensaje";
        #endregion

        #region --[Constructor]--
        public DAMensaje()
        {
        }

        public DAMensaje(DATransaction objDATransaction)
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

        public override Mensaje GetById(Mensaje entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Mensaje entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Mensaje entidad, out int identificador)
        {
            try
            {
                using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Mensaje_Insert"))
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMensaje", DbType.Int32, 0);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEnvio", DbType.Date, entidad.fechaEnvio.Date.ToShortDateString());
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaEnvio", DbType.Time, entidad.horaEnvio.Hour + ":" + entidad.horaEnvio.Minute);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.remitente.Nombre);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@textoMensaje", DbType.String, entidad.textoMensaje);
                    //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.pagina.titulo);

                    if (Transaction.Transaction != null)
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                    else
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idMensaje"].Value.ToString());
                }
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

        public override void Update(Mensaje entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Mensaje entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
