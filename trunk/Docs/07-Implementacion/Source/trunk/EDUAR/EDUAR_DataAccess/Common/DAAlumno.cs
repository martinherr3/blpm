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
    public class DAAlumno : DataAccesBase<Alumno>
    {
        #region --[Atributos]--
        private const string ClassName = "DAAlumno";
        #endregion

        #region --[Constructor]--
        public DAAlumno()
        {
        }

        public DAAlumno(DATransaction objDATransaction)
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

        public override Alumno GetById(Alumno entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Alumno entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Alumno entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Alumno entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Alumno entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--

        //public List<Alumno> GetAlumnos(Alumno entidad)
        //{
        //    try
        //    {
        //        Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Alumno_Select");
        //        if (entidad != null)
        //        {
        //            if (entidad.idAlumno > 0)
        //                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.idAlumno);
        //            if (entidad.activo != null)
        //                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
        //            if (!string.IsNullOrEmpty(entidad.apellido))
        //                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@apellido", DbType.String, entidad.apellido);
        //            if (!string.IsNullOrEmpty(entidad.barrio))
        //                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "barrio", DbType.String, entidad.barrio);
        //            if (!string.IsNullOrEmpty(entidad.domicilio))
        //                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@domicilio", DbType.String, entidad.domicilio);
        //            if (!string.IsNullOrEmpty(entidad.email))
        //                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@email", DbType.String, entidad.email);
        //            if (ValidarFechaSQL(entidad.fechaAlta))
        //                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaAlta", DbType.Date, entidad.fechaAlta);
        //            if (ValidarFechaSQL(entidad.fechaBaja))
        //                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaBaja", DbType.Date, entidad.fechaBaja);
        //            if (entidad.fechaNacimiento != null && ValidarFechaSQL((DateTime)entidad.fechaNacimiento))
        //                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaNacimiento", DbType.Date, entidad.fechaNacimiento);
        //        }
            
        //        IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

        //        List<Alumno> listaAlumnos = new List<Alumno>();
        //        Alumno objAlumno;
        //        while (reader.Read())
        //        {
        //            objAlumno = new Alumno();

        //            objAlumno.idAlumno = Convert.ToInt32(reader["idAlumno"]);
        //            objAlumno.apellido = reader["apellido"].ToString();
        //            if (!string.IsNullOrEmpty(reader["fechaAlta"].ToString()))
        //                objAlumno.fechaAlta = (DateTime)reader["fechaAlta"];
        //            if (!string.IsNullOrEmpty(reader["fechaBaja"].ToString()))
        //                objAlumno.fechaBaja = (DateTime)reader["fechaBaja"];
        //            objAlumno.activo = Convert.ToBoolean(reader["activo"]);
                   
        //             //TODO: Completar los miembros que faltan de alumno
                        
        //             listaAlumnos.Add(objAlumno);
        //        }
        //        return listaAlumnos;
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new CustomizedException(string.Format("Fallo en {0} - GetCiclosLectivos()", ClassName),
        //                            ex, enuExceptionType.SqlException);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new CustomizedException(string.Format("Fallo en {0} - GetCiclosLectivos()", ClassName),
        //                            ex, enuExceptionType.DataAccesException);
        //    }
        //}

		/// <summary>
		/// Gets the alumnos.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
        public List<Alumno> GetAlumnos(AlumnoCurso entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AlumnosPorCurso_Select");
                //'AlumnoPorCurso_Select
                if (entidad != null)
                {
                    if (entidad.alumno.idAlumno > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAlumno", DbType.Int32, entidad.alumno.idAlumno);
                    if (entidad.curso.idCurso > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.curso.idCurso);
                  
               }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Alumno> listaAlumnos = new List<Alumno>();
                Alumno objAlumno;
                while (reader.Read())
                {
                    objAlumno = new Alumno();

                    objAlumno.idAlumno = Convert.ToInt32(reader["idAlumno"]);
					objAlumno.nombre = reader["nombre"].ToString();
                    objAlumno.apellido = reader["apellido"].ToString();
                    if (!string.IsNullOrEmpty(reader["fechaAlta"].ToString()))
                        objAlumno.fechaAlta = (DateTime)reader["fechaAlta"];
                    if (!string.IsNullOrEmpty(reader["fechaBaja"].ToString()))
                        objAlumno.fechaBaja = (DateTime)reader["fechaBaja"];
                    objAlumno.activo = Convert.ToBoolean(reader["activo"]);
					objAlumno.idPersona = Convert.ToInt32(reader["idPersona"]);
                    //TODO: Completar los miembros que faltan de alumno

                    listaAlumnos.Add(objAlumno);
                }
                return listaAlumnos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnosPorCurso()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetalumnosPorCurso()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
