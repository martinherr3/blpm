using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
    public class DADocente : DataAccesBase<Docente>
    {
        #region --[Atributos]--
        private const string ClassName = "DADocente";
        #endregion

        #region --[Constructor]--
        public DADocente()
        {
        }

        public DADocente(DATransaction objDATransaction)
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

        public override Docente GetById(Docente entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Docente entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Docente entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Docente entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Docente entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--

        /// <summary>
        /// Gets the alumnos.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Docente> GetDocentes(Docente entidad = null)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Docentes_Select");
                if (entidad != null)
                {
                    if (entidad.idPersonal > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersonal", DbType.Int32, entidad.idPersonal);
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Docente> listaDocentes = new List<Docente>();
                Docente objDocente;
                while (reader.Read())
                {
                    objDocente = new Docente();

                    objDocente.idPersonal = Convert.ToInt32(reader["idPersonal"]);
                    objDocente.nombre = reader["nombre"].ToString();
                    objDocente.apellido = reader["apellido"].ToString();
                    if (!string.IsNullOrEmpty(reader["fechaAlta"].ToString()))
                        objDocente.fechaAlta = (DateTime)reader["fechaAlta"];
                    if (!string.IsNullOrEmpty(reader["fechaBaja"].ToString()))
                        objDocente.fechaBaja = (DateTime)reader["fechaBaja"];
                    objDocente.activo = Convert.ToBoolean(reader["activo"]);
                    objDocente.idPersona = Convert.ToInt32(reader["idPersona"]);
                    objDocente.username = reader["username"].ToString();
                    //TODO: Completar los miembros que faltan de alumno

                    listaDocentes.Add(objDocente);
                }
                return listaDocentes;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetDocentes()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetDocentes()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
