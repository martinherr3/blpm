using System;
using System.Collections.Generic;
using EDUAR_Entities;
using EDUAR_SI_DataAccess;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_SI_BusinessLogic
{
	public class BLNotificarInasistenciaSancionSMS : BLProcesoBase
	{
		#region --[Atributos]--
		/// <summary>
		/// Se utiliza para acceder a la capa de datos
		/// </summary>
		DANotificarInasistenciaSancionSMS objDANotificar;

        //DANotificarSancion objDANotificarSancion;
        #endregion

		#region --[Propiedades]--
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor. LLama al constructor de la clase base BLProcesoBase.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos.</param>
		public BLNotificarInasistenciaSancionSMS(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Procedimientoes the importar datos.
		/// </summary>
		public void ProcedimientoNotificarInasistenciaSancionSMS()
		{
			try
			{
				objDANotificar = new DANotificarInasistenciaSancionSMS(ConnectionString);
				var listaInasistencias = objDANotificar.GetInformeInasistenciasSMS(enumProcesosAutomaticos.InformeInasistencias.GetHashCode());

                var listaSanciones = objDANotificar.GetInformeSancionesSMS(enumProcesosAutomaticos.InformeSanciones.GetHashCode());


                if (listaInasistencias.Count >= 0 || listaSanciones.Count > 0)
                {

                    EnviarSMSInasistenciaSancion(listaInasistencias, listaSanciones);
                    ProcesosEjecutadosCreate(enumProcesosAutomaticos.InformeInasistencias.GetHashCode(), true);
                }
			}
			catch (Exception ex)
			{
				OnErrorProcess(enumProcesosAutomaticos.InformeInasistencias.GetHashCode(), ex);
				throw ex;
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Importars the datos.
		/// </summary>
		private void EnviarSMSInasistenciaSancion(List<Asistencia> listaAsistencia,List<Sancion> listaSancion)
		{
            
			try
			{
                List<Alumno> Alumnos = new List<Alumno>();
				String txtMsg =  null;
                bool encontrado = false;

                foreach (var unaAsistencia in listaAsistencia)
                {
                    foreach (var unAlumno in Alumnos)
                    {
                        if (unaAsistencia.alumno.alumno.idPersona == unAlumno.idPersona)
                        {
                            encontrado = true;
                        }
                    }

                    if (encontrado == false)
                    {
                        Alumnos.Add(unaAsistencia.alumno.alumno);
                    }
                    else
                    {
                        encontrado = false;
                    }
                }

                foreach (var unaSancion in listaSancion)
                {
                    foreach (var unAlumno in Alumnos)
                    {
                        if (unaSancion.alumno.alumno.idPersona == unAlumno.idPersona)
                        {
                            encontrado = true;
                        }
                    }

                    if (encontrado == false)
                    {
                        Alumnos.Add(unaSancion.alumno.alumno);

                    }
                    else
                    {
                        encontrado = false;
                    }
                }


                if (SMS.Conectarse())
                {
                    foreach (var unAlumno in Alumnos)
                    {
                        // empiezo a armar un nuevo sms
                        txtMsg = "Buenos dias " + unAlumno.listaTutores[0].nombre + " " + unAlumno.listaTutores[0].apellido + ", ";
                        txtMsg = txtMsg + "Informamos inasistencias y sanciones en la ultima semana: ";
                        txtMsg = txtMsg + "El alumno " + unAlumno.nombre + " " + unAlumno.apellido + " ha tenido: ";
                        var listaInasistencia = listaAsistencia.FindAll(p => p.alumno.alumno.idPersona == unAlumno.idPersona);
                        txtMsg = txtMsg + "Inasistencias: " + listaInasistencia.Count;
                        var listaSanciones = listaSancion.FindAll(p => p.alumno.alumno.idPersona == unAlumno.idPersona);
                        txtMsg = txtMsg + " - Sanciones: " + listaSanciones.Count + " . Saludos cordiales. Enviado desde Edu @r 2.0";

                        //unAlumno.listaTutores[0].telefonoCelular = "3513262699"; // Para testear descomentar
                        SMS.EnviarSMS(unAlumno.listaTutores[0].telefonoCelular, SMS.RemoveAccentsWithRegEx(txtMsg));
                
                    
                    }

                    SMS.Desconectarse();
                }
                else
                {
                    throw(new Exception("No se puede conectar al Modem Celular"));
                }
			}
            
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}
