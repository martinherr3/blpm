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
            //{
            //    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Personas_Select");

            //    if (entidad.idPersona > 0)
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersona", DbType.Int32, entidad.idPersona);

            //    IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

            //    Persona objPersona = new Persona();
            //    while (reader.Read())
            //    {
            //        objPersona.idPersona = Convert.ToInt32(reader["idPersona"]);
            //        objPersona.nombre = reader["nombre"].ToString();
            //        objPersona.apellido = reader["apellido"].ToString();
            //        objPersona.numeroDocumento = Convert.ToInt32(reader["numeroDocumento"]);
            //        objPersona.idTipoDocumento = Convert.ToInt32(reader["idTipoDocumento"]);
            //        objPersona.domicilio = reader["domicilio"].ToString();
            //        objPersona.barrio = reader["barrio"].ToString();
            //        if (!string.IsNullOrEmpty(reader["idLocalidad"].ToString()))
            //            objPersona.localidad = new Localidades() { idLocalidad = Convert.ToInt32(reader["idLocalidad"]) };
            //        objPersona.sexo = reader["sexo"].ToString();
            //        if (!string.IsNullOrEmpty(reader["fechaNacimiento"].ToString()))
            //            objPersona.fechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
            //        objPersona.telefonoFijo = reader["telefonoFijo"].ToString();
            //        objPersona.telefonoCelular = reader["telefonoCelular"].ToString();
            //        objPersona.telefonoCelularAlternativo = reader["telefonoCelularAlternativo"].ToString();
            //        objPersona.email = reader["email"].ToString();
            //        objPersona.activo = Convert.ToBoolean(reader["activo"]);
            //        objPersona.username = reader["username"].ToString();
            //        objPersona.idTipoPersona = Convert.ToInt32(reader["idTipoPersona"]);
            //    }
            //    return objPersona;
            //}
            //catch (SqlException ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - GetPersonas()", ClassName),
            //                        ex, enuExceptionType.SqlException);
            //}
            //catch (Exception ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - GetPersonas()", ClassName),
            //                        ex, enuExceptionType.DataAccesException);
            //}
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
            //try
            //{
            //    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Personas_Update");

            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersona", DbType.Int32, entidad.idPersona);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@apellido", DbType.String, entidad.apellido);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@numeroDocumento", DbType.Int32, entidad.numeroDocumento);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoDocumento", DbType.Int32, entidad.idTipoDocumento);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@domicilio", DbType.String, entidad.domicilio);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@barrio", DbType.String, entidad.barrio);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idLocalidad", DbType.String, entidad.localidad.idLocalidad);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@sexo", DbType.String, entidad.sexo);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaNacimiento", DbType.Date, entidad.fechaNacimiento);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@telefonoFijo", DbType.String, entidad.telefonoFijo);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@telefonoCelular", DbType.String, entidad.telefonoCelular);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@telefonoCelularAlternativo", DbType.String, entidad.telefonoCelularAlternativo);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@email", DbType.String, entidad.email);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.username);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoPersona", DbType.Int32, entidad.idTipoPersona);

            //    if (Transaction.Transaction != null)
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
            //    else
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            //}
            //catch (SqlException ex)
            //{
            //    if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
            //        throw new CustomizedException(string.Format(
            //               "No se puede modificar la Persona {0}, debido a que otro usuario lo ha modificado.",
            //               entidad.nombre + " " + entidad.apellido), ex, enuExceptionType.ConcurrencyException);

            //    throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
            //                                          ex, enuExceptionType.SqlException);
            //}
            //catch (Exception ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
            //                                          ex, enuExceptionType.DataAccesException);
            //}
        }

        public override void Delete(Pagina entidad)
        {
            throw new NotImplementedException();
            //try
            //{
            //    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Personas_Delete");

            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersona", DbType.Int32, 0);


            //    if (Transaction.Transaction != null)
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
            //    else
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

            //}
            //catch (SqlException ex)
            //{
            //    if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
            //        throw new CustomizedException(string.Format(
            //               "No se puede eliminar la Persona {0}, debido a que otro usuario lo ha modificado.",
            //               entidad.nombre + " " + entidad.apellido), ex, enuExceptionType.ConcurrencyException);
            //    if (ex.Number == BLConstantesGenerales.IntegrityErrorNumber)
            //        throw new CustomizedException(string.Format("No se puede eliminar la Persona {0}, debido a que tiene registros asociados.",
            //                           entidad.nombre + " " + entidad.apellido), ex, enuExceptionType.IntegrityDataException);


            //    throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
            //                                           ex, enuExceptionType.SqlException);
            //}
            //catch (Exception ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
            //                                           ex, enuExceptionType.DataAccesException);
            //}
        }
        #endregion
    }
}
