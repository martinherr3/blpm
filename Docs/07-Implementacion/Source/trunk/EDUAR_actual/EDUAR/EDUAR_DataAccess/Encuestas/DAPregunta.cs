using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Encuestas
{
    public class DAPregunta : DataAccesBase<Pregunta>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPregunta";
        #endregion

        #region --[Constructor]--
        public DAPregunta()
        {
        }

        public DAPregunta(DATransaction objDATransaction)
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

        public override Pregunta GetById(Pregunta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Pregunta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Pregunta entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Pregunta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Pregunta entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the preguntas.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Pregunta> GetPreguntas(Pregunta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Pregunta_Select");
                if (entidad != null)
                {
                    if (entidad.idPregunta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, entidad.idPregunta);
                    if (entidad.categoria.idCategoriaPregunta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoriaPregunta", DbType.Int32, entidad.categoria.idCategoriaPregunta);
                    if (entidad.escala.idEscala > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaMedicion", DbType.Int32, entidad.escala.idEscala);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Pregunta> listaPreguntas = new List<Pregunta>();
                Pregunta objPregunta;

                while (reader.Read())
                {
                    objPregunta = new Pregunta();

                    objPregunta.idPregunta = Convert.ToInt32(reader["idPregunta"]);
                    objPregunta.textoPregunta = reader["textoPregunta"].ToString();
                    objPregunta.objetivoPregunta = reader["objetivo"].ToString();

                    objPregunta.categoria = new CategoriaPregunta();
                    {
                        objPregunta.categoria.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                        objPregunta.categoria.nombre = reader["nombre"].ToString();
                        objPregunta.categoria.descripcion = reader["descripcion"].ToString();
                    }

                    objPregunta.escala = new EscalaMedicion();
                    {
                        objPregunta.escala.idEscala = Convert.ToInt32(reader["idEscala"]);
                        objPregunta.escala.nombre = reader["nombre"].ToString();
                    }

                    listaPreguntas.Add(objPregunta);
                }
                return listaPreguntas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntas()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntas()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets las preguntas correspondientes a una categoria dada.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Pregunta> GetPreguntasPorCategoria(CategoriaPregunta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CategoriaPregunta_Select");
                
                if (entidad != null)
                {
                    if(entidad.idCategoriaPregunta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoriaPregunta", DbType.Int32, entidad.idCategoriaPregunta);
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Pregunta> listaPreguntas = new List<Pregunta>();
                Pregunta objPregunta;

                while (reader.Read())
                {
                    objPregunta = new Pregunta();

                    objPregunta.idPregunta = Convert.ToInt32(reader["idPregunta"]);
                    objPregunta.textoPregunta = reader["textoPregunta"].ToString();
                    objPregunta.objetivoPregunta = reader["objetivo"].ToString();

                    objPregunta.categoria = entidad;
                    
                    objPregunta.escala = new EscalaMedicion();
                    {
                        objPregunta.escala.idEscala = Convert.ToInt32(reader["idEscala"]);
                        objPregunta.escala.nombre = reader["nombre"].ToString();
                    }

                    listaPreguntas.Add(objPregunta);
                }
                return listaPreguntas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntas()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntas()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
