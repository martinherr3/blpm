using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Encuestas
{
    public class DACategoriaPregunta : DataAccesBase<CategoriaPregunta>
    {
        #region --[Atributos]--
        private const string ClassName = "DACategoriaPregunta";
        #endregion

        #region --[Constructor]--
        public DACategoriaPregunta()
        {
        }

        public DACategoriaPregunta(DATransaction objDATransaction)
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

        public override CategoriaPregunta GetById(CategoriaPregunta entidad)
        {
            try
            {
                if (entidad != null)
                {
                    //El identificador es mandatorio
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, entidad.idCategoriaPregunta);

                    if (!string.IsNullOrEmpty(entidad.nombre))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                    if (!string.IsNullOrEmpty(entidad.descripcion))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                CategoriaPregunta objCategoriaPregunta = null;
                if (reader.Read())
                {
                    objCategoriaPregunta = new CategoriaPregunta();
                    objCategoriaPregunta.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                    objCategoriaPregunta.nombre = reader["nombre"].ToString();
                    objCategoriaPregunta.descripcion = reader["descripcion"].ToString();
                }
                return objCategoriaPregunta;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Select()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Select()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Create(CategoriaPregunta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(CategoriaPregunta entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(CategoriaPregunta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(CategoriaPregunta entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the encuestas.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<CategoriaPregunta> GetCategoriasPregunta(CategoriaPregunta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CategoriaPregunta_Select");
                if (entidad != null)
                {
                    if (entidad.idCategoriaPregunta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, entidad.idCategoriaPregunta);
                    if (entidad.ambito.idAmbitoEncuesta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<CategoriaPregunta> listaCategoriasPregunta = new List<CategoriaPregunta>();
                CategoriaPregunta objCategoriaPregunta;

                while (reader.Read())
                {
                    objCategoriaPregunta = new CategoriaPregunta();

                    objCategoriaPregunta.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                    objCategoriaPregunta.nombre = reader["nombreCategoria"].ToString();
                    objCategoriaPregunta.descripcion = reader["descripcionCategoria"].ToString();

                    objCategoriaPregunta.ambito = new AmbitoEncuesta();
                    {
                        objCategoriaPregunta.ambito.idAmbitoEncuesta = Convert.ToInt32(reader["idAmbito"]);
                        objCategoriaPregunta.ambito.nombre = reader["nombreAmbito"].ToString();
                        objCategoriaPregunta.ambito.descripcion = reader["descripcionAmbito"].ToString();
                    }

                    listaCategoriasPregunta.Add(objCategoriaPregunta);
                }
                return listaCategoriasPregunta;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCategoriasPregunta()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCategoriasPregunta()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion

    }
}
