﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_SI_DataAccess;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_SI_BusinessLogic
{
	public class BLImportarConfiguracionAcademica : BLProcesoBase
	{
		#region --[Atributos]--
		Configuraciones objConfiguracion;

		DAImportarDatos objDAImportarDatos;

		DAObtenerDatos objDAObtenerDatos;
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor. LLama al constructor de la clase base BLProcesoBase.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos.</param>
		public BLImportarConfiguracionAcademica(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Ejecuta la importación de Pais, Provincia y Localidad
		/// </summary>
		public void ImportarConfiguracionAcademica()
		{
			try
			{
				objDAImportarDatos = new DAImportarDatos(ConnectionString);

				//objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigenDEVDesdeRemoto);
				objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigenDEVDesdeRemoto);

				//Método que realmente lleva a cabo las tareas
				ImportarDatos();

				//Inserta un registro en la tabla ProcesosEjecutado, en este caso, indica que se corrio correctamente el proceso (resultado = 1)
				ProcesosEjecutadosCreate(enumProcesosAutomaticos.ImportarConfiguracionAcademica.GetHashCode(), true);
			}
			catch (Exception ex)
			{
				//ante cualquier error que se produzca, se graba en la tabla ProcesosEjecutados, 
				//un registro con el detalle de en que método se produjo el error (resultado = 0)
				OnErrorProcess(enumProcesosAutomaticos.ImportarConfiguracionAcademica.GetHashCode(), ex);
				throw ex;
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Importars the datos.
		/// </summary>
		private void ImportarDatos()
		{
			//La transacción va a este nivel para poder hacer el rollback en el cath (si se produce error)
			SqlTransaction transaccion = null;
			try
			{
				//Al utillizar el "using" me aseguro que los recursos se liberen cuando termina el bloque
				SqlConnection conexion = new SqlConnection() { ConnectionString = ConnectionString };
				{
					//objeto que voy a utilizar para obtener los datos (BD Transaccional)
					objDAObtenerDatos = new DAObtenerDatos(objConfiguracion.valor);

					//abre la conexión a la bd
					conexion.Open();
					//le indica al objeto transaccion que va a iniciar una transacción
					transaccion = conexion.BeginTransaction();

					ImportarDatos(transaccion);
				}
				//si la importación de los objetos fue exitosa, entonces confirmo las modificaciones.
				transaccion.Commit();
				conexion.Close();
			}
			catch (Exception ex)
			{
				//Valido que la transacción no sea nula, sino daría error al intentar el rollback
				if (transaccion != null)
					transaccion.Rollback();
				//mando la excepción para arriba
				throw ex;
			}
		}

		/// <summary>
		/// Importars the datos.
		/// </summary>
		/// <param name="transaccion">The transaccion.</param>
		private void ImportarDatos(SqlTransaction transaccion)
		{
			try
			{
				//User Story 143
				objDAImportarDatos.GrabarAsignatura(objDAObtenerDatos.obtenerAsignaturaBDTransaccional(objConfiguracion), transaccion);

				objDAImportarDatos.GrabarCicloLectivo(objDAObtenerDatos.obtenerCicloLectivoBDTransaccional(objConfiguracion), transaccion);

				objDAImportarDatos.GrabarNivel(objDAObtenerDatos.obtenerNivelesBDTransaccional(objConfiguracion), transaccion);

				objDAImportarDatos.GrabarCursos(objDAObtenerDatos.obtenerCursosBDTransaccional(objConfiguracion), transaccion);

				objDAImportarDatos.GrabarCursoCicloLectivo(objDAObtenerDatos.obtenerCursoCicloLectivoBDTransaccional(objConfiguracion), transaccion);

				objDAImportarDatos.GrabarAlumnoCurso(objDAObtenerDatos.obtenerAlumnoCursoBDTransaccional(objConfiguracion), transaccion);

				objDAImportarDatos.GrabarOrientacion(objDAObtenerDatos.obtenerOrientacionesBDTransaccional(objConfiguracion), transaccion);

				objDAImportarDatos.GrabarAsignaturaCurso(objDAObtenerDatos.obtenerAsignaturasCursoBDTransaccional(objConfiguracion), transaccion);

				objDAImportarDatos.GrabarAsignaturaNivel(objDAObtenerDatos.obtenerAsignaturaNivelBDTransaccional(objConfiguracion), transaccion);

				objDAImportarDatos.GrabarPeriodo(objDAObtenerDatos.obtenerPeriodosBDTransaccional(objConfiguracion), transaccion);
			}
			catch (Exception ex)
			{ throw ex; }
		}
		#endregion
	}
}
