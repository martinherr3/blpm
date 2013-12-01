using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_SI_DataAccess;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_SI_BusinessLogic
{
	public class BLImportarDiasHorarios : BLProcesoBase
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
        public BLImportarDiasHorarios(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Ejecuta la importación de Pais, Provincia y Localidad
		/// </summary>
		public void ImportarDiasHorarios()
		{
			try
			{
				objDAImportarDatos = new DAImportarDatos(ConnectionString);

                objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigenDEV);
				//objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigenDesdeRemoto);

				//Método que realmente lleva a cabo las tareas
				ImportarDatos();

				//Inserta un registro en la tabla ProcesosEjecutado, en este caso, indica que se corrio correctamente el proceso (resultado = 1)
				ProcesosEjecutadosCreate(enumProcesosAutomaticos.ImportarAsistencia.GetHashCode(), true);
			}
			catch (Exception ex)
			{
				//ante cualquier error que se produzca, se graba en la tabla ProcesosEjecutados, 
				//un registro con el detalle de en que método se produjo el error (resultado = 0)
				OnErrorProcess(enumProcesosAutomaticos.ImportarAsistencia.GetHashCode(), ex);
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

					objDAImportarDatos.GrabarDiasHorarios(objDAObtenerDatos.obtenerHorarios(objConfiguracion), transaccion);
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


		#endregion
	}
}