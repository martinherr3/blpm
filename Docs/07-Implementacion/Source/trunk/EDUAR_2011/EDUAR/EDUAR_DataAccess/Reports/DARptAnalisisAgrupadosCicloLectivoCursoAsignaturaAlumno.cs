using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using EDUAR_Entities.Security;

namespace EDUAR_DataAccess.Reports
{
    public class DARptAnalisisAgrupadosCicloLectivoCursoAsignaturaAlumno : DataAccesBase<RptAnalisisCicloLectivoCursoAsignaturaAlumno>
    {
        #region --[Atributos]--
        private const string ClassName = "DARptAnalisisAgrupadosCicloLectivoCursoAsignaturaAlumno";
        #endregion

        #region --[Constructor]--
        public DARptAnalisisAgrupadosCicloLectivoCursoAsignaturaAlumno()
        {
        }

        public DARptAnalisisAgrupadosCicloLectivoCursoAsignaturaAlumno(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--

		/// <summary>
		/// Gets the RPT análisis de promedios por ciclo lectivo-curso-asignatura-alumno.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<RptAnalisisCicloLectivoCursoAsignaturaAlumno> GetRptAnalisisCicloLectivoCursoAsignaturaAlumno(FilAnalisisAgrupados entidad)
		{
			try
			{
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reporte_AnalisisPromedios-CicloLectivo-Nivel-Asignatura-Alumnos");
				if (entidad != null)
				{
                    string ciclosLectivosParam = string.Empty;
                    if (entidad.listaCicloLectivo.Count > 0)
                    {
                        foreach (CicloLectivo cicloLectivo in entidad.listaCicloLectivo)
                            ciclosLectivosParam += string.Format("{0},", cicloLectivo.idCicloLectivo);

                        ciclosLectivosParam = ciclosLectivosParam.Substring(0, ciclosLectivosParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaCicloLectivo", DbType.String, ciclosLectivosParam);
                    }

                    string nivelesParam = string.Empty;
                    if (entidad.listaNiveles.Count > 0)
                    {
                        foreach (Nivel nivel in entidad.listaNiveles)
                            nivelesParam += string.Format("{0},", nivel.idNivel);

                        nivelesParam = nivelesParam.Substring(0, nivelesParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaNivel", DbType.String, nivelesParam);
                    }

                    string asignaturasParam = string.Empty;
                    if (entidad.listaAsignaturas.Count > 0)
                    {
                        foreach (Asignatura asignatura in entidad.listaAsignaturas)
                            asignaturasParam += string.Format("{0},", asignatura.idAsignatura);

                        asignaturasParam = asignaturasParam.Substring(0, asignaturasParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaAsignaturas", DbType.String, asignaturasParam);
                    }

                    string alumnosParam = string.Empty;
                    if (entidad.listaAlumnos.Count > 0)
                    {
                        foreach (Alumno alumno in entidad.listaAlumnos)
                            alumnosParam += string.Format("{0},", alumno.idAlumno);

                        alumnosParam = alumnosParam.Substring(0, alumnosParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaAlumnos", DbType.String, alumnosParam);
                    }
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<RptAnalisisCicloLectivoCursoAsignaturaAlumno> listaReporte = new List<RptAnalisisCicloLectivoCursoAsignaturaAlumno>();
				RptAnalisisCicloLectivoCursoAsignaturaAlumno objReporte;
				while (reader.Read())
				{
                    objReporte = new RptAnalisisCicloLectivoCursoAsignaturaAlumno();

                    objReporte.ciclolectivo = reader["CicloLectivo"].ToString();
                    objReporte.curso = reader["Curso"].ToString();
                    objReporte.asignatura = reader["Asignatura"].ToString();
                    objReporte.alumno = reader["Alumno"].ToString();
                    objReporte.promedio = reader["Promedio"].ToString();

					listaReporte.Add(objReporte);
				}
				return listaReporte;
			}
			catch (SqlException ex)
			{
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptAnalisisCicloLectivoCursoAsignaturaAlumno()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
                throw new CustomizedException(string.Format("Fallo en {0} - GetRptAnalisisCicloLectivoCursoAsignaturaAlumno()", ClassName),
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

        public override RptAnalisisCicloLectivoCursoAsignaturaAlumno GetById(RptAnalisisCicloLectivoCursoAsignaturaAlumno entidad)
		{
			throw new NotImplementedException();
		}

        public override void Create(RptAnalisisCicloLectivoCursoAsignaturaAlumno entidad)
		{
			throw new NotImplementedException();
		}

        public override void Create(RptAnalisisCicloLectivoCursoAsignaturaAlumno entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

        public override void Update(RptAnalisisCicloLectivoCursoAsignaturaAlumno entidad)
		{
			throw new NotImplementedException();
		}

        public override void Delete(RptAnalisisCicloLectivoCursoAsignaturaAlumno entidad)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
