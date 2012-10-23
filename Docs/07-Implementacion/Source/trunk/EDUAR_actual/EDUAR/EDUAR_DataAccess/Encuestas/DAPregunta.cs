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
            //try
            //{
            //    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Pregunta_Insert");

            //    if (!entidad.Equals(null))
            //    {
            //        //Inherentes a la tabla Pregunta

            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, entidad.idPregunta);
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@textoPregunta", DbType.String, entidad.textoPregunta);
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, entidad.categoria.idCategoriaPregunta);
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@objetivo", DbType.String, entidad.objetivoPregunta);

            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, entidad.escala.idEscala);
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@peso", DbType.Int32, entidad.ponderacion);

            //    }

            //    if (Transaction.Transaction != null)
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
            //    else
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

            //    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idPregunta"].Value.ToString());
            //}
            //catch (SqlException ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
            //                        ex, enuExceptionType.SqlException);
            //}
            //catch (Exception ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
            //                        ex, enuExceptionType.DataAccesException);
            //}

            throw new NotImplementedException();
        }

        public override void Update(Pregunta entidad)
        {
            //try
            //{
            //    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PreguntasEncuesta_Update");

            //    //Inherentes a la tabla Encuesta

            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);

            //    foreach (Pregunta pregunta in entidad.preguntas)
            //    {
            //        //Inherentes a la tabla Pregunta

            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, pregunta.idPregunta);
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@textoPregunta", DbType.String, pregunta.textoPregunta);
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, pregunta.categoria.idCategoriaPregunta);
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@objetivo", DbType.String, pregunta.objetivoPregunta);

            //        //Inherentes a la tabla PreguntaEncuesta

            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscalaPonderacion", DbType.Int32, entidad.idEncuesta);
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@peso", DbType.Double, entidad.idEncuesta);
            //    }

            //    if (Transaction.Transaction != null)
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
            //    else
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            //}
            //catch (SqlException ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
            //                        ex, enuExceptionType.SqlException);
            //}
            //catch (Exception ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
            //                        ex, enuExceptionType.DataAccesException);
            //}

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
		/// Gets the preguntas por categoria.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Pregunta> GetPreguntasPorCategoria(CategoriaPregunta entidad)
		{
			try
			{
				return GetPreguntasPorCategoria(entidad, null);
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasPorCategoria(CategoriaPregunta)", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasPorCategoria(CategoriaPregunta)", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

        /// <summary>
        /// Gets las preguntas correspondientes a una categoria dada.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Pregunta> GetPreguntasPorCategoria(CategoriaPregunta entidad, Encuesta miEncuesta)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Pregunta_Select");
                
                if (entidad != null)
                    if(entidad.idCategoriaPregunta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, entidad.idCategoriaPregunta);
				if (miEncuesta != null)
					if (miEncuesta.idEncuesta > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, miEncuesta.idEncuesta);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Pregunta> listaPreguntas = new List<Pregunta>();
                Pregunta objPregunta;

                while (reader.Read())
                {
                    objPregunta = new Pregunta();

                    objPregunta.idPregunta = Convert.ToInt32(reader["idPregunta"]);
                    objPregunta.textoPregunta = reader["textoPregunta"].ToString();
                    objPregunta.objetivoPregunta = reader["objetivo"].ToString();
                    objPregunta.ponderacion = Convert.ToDouble(reader["peso"]);

                    objPregunta.categoria = new CategoriaPregunta();
                    {
                        objPregunta.categoria.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                        objPregunta.categoria.nombre = reader["categoria"].ToString();
                    }

                    objPregunta.escala = new EscalaMedicion();
                    {
                        objPregunta.escala.idEscala = Convert.ToInt32(reader["idEscalaPonderacion"]);
                        objPregunta.escala.descripcion = reader["descripcionEscala"].ToString();
                        objPregunta.escala.nombre = reader["escala"].ToString();
                    }

                    listaPreguntas.Add(objPregunta);
                }
				reader.Dispose();
                return listaPreguntas;
            }
            catch (SqlException ex)
            {
				throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasPorCategoria(CategoriaPregunta, Encuesta)", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
				throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasPorCategoria(CategoriaPregunta, Encuesta)", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
