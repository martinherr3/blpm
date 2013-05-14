using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
    public class DAPagina : DataAccesBase<Pagina>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPagina";
        #endregion

        #region --[Constructor]--
        public DAPagina()
        {
        }

        public DAPagina(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--
        public List<Pagina> GetPaginas(Pagina entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Paginas_Select");
                if (entidad != null)
                {
                    if (entidad.idPagina > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, entidad.idPagina);
                    if (!string.IsNullOrEmpty(entidad.titulo))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo);
                    if (!string.IsNullOrEmpty(entidad.url))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@url", DbType.String, entidad.url);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Pagina> listaPaginas = new List<Pagina>();
                Pagina objPagina;
                while (reader.Read())
                {
                    objPagina = new Pagina();

                    objPagina.idPagina = Convert.ToInt32(reader["idPagina"]);
                    objPagina.titulo = reader["titulo"].ToString();
                    objPagina.url = reader["url"].ToString();

                    listaPaginas.Add(objPagina);
                }
                return listaPaginas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPaginas()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPaginas()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
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

        public override Pagina GetById(Pagina entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Pagina entidad)
        {
            throw new NotImplementedException();

        }

        public override void Create(Pagina entidad, out int identificador)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Paginas_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, 0);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@url", DbType.String, entidad.url);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.titulo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idPagina"].Value.ToString());

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

        public override void Update(Pagina entidad)
        {

        }

        public override void Delete(Pagina entidad)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
