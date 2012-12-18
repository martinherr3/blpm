using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_SI_DataAccess;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_SI_BusinessLogic
{
	public class BLImportarPersonal : BLProcesoBase
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
		public BLImportarPersonal(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Ejecuta la importación de Pais, Provincia y Localidad
		/// </summary>
		public void ImportarPersonal()
		{
			try
			{
				objDAImportarDatos = new DAImportarDatos(ConnectionString);

				objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigenDEV);
				//objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigenDesdeRemoto);

				//Método que realmente lleva a cabo las tareas
				ImportarDatos();

				//Inserta un registro en la tabla ProcesosEjecutado, en este caso, indica que se corrio correctamente el proceso (resultado = 1)
				ProcesosEjecutadosCreate(enumProcesosAutomaticos.ImportarPersonal.GetHashCode(), true);
			}
			catch (Exception ex)
			{
				//ante cualquier error que se produzca, se graba en la tabla ProcesosEjecutados, 
				//un registro con el detalle de en que método se produjo el error (resultado = 0)
				OnErrorProcess(enumProcesosAutomaticos.ImportarPersonal.GetHashCode(), ex);
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

					//User Story 142
					GrabarPersonal(transaccion);

					GrabarDocente(transaccion);
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
		/// Grabars the docente.
		/// </summary>
		private void GrabarDocente(SqlTransaction transaccion)
		{
			try
			{
				List<Docente> listaDocentes = objDAObtenerDatos.obtenerDocenteBDTransaccional(objConfiguracion);
				Persona persona = null;
				foreach (Docente docente in listaDocentes)
				{
					persona = new Persona()
					{
						idPersona = 0,
						nombre = docente.nombre,
						apellido = docente.apellido,
						numeroDocumento = docente.numeroDocumento,
						idTipoDocumento = docente.idTipoDocumento,
						activo = docente.activo,
						fechaNacimiento = docente.fechaNacimiento,
						domicilio = docente.domicilio,
						email = docente.email,
						telefonoCelular = docente.telefonoCelular,
						telefonoFijo = docente.telefonoFijo,
						localidad = new Localidades() { nombre = docente.localidad.nombre },
						sexo = docente.sexo,
						idTipoPersona = (int)enumTipoPersona.Personal
					};
					docente.idPersona = objDAImportarDatos.GrabarPersona(persona, transaccion, docente.cargo.idCargoTransaccional, docente.idDocenteTransaccional);

					Personal personal = new Personal()
					{
						fechaAlta = docente.fechaAlta,
						idPersona = docente.idPersona,
						idPersonalTransaccional = (int)docente.idDocenteTransaccional,
						legajo = docente.legajo,
						cargo = new CargoPersonal() { idCargo = docente.cargo.idCargo, idCargoTransaccional = docente.cargo.idCargoTransaccional },
						activo = docente.activo,
					};
					objDAImportarDatos.GrabarPersonal(personal, transaccion);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Grabars the personal.
		/// </summary>
		private void GrabarPersonal(SqlTransaction transaccion)
		{
			try
			{
				//Primero actualiza los cargos
				objDAImportarDatos.GrabarCargoPersonal(objDAObtenerDatos.obtenerCargosPersonalBDTransaccional(objConfiguracion), transaccion);

				//Busca e inserta o actualiza el personal
				List<Personal> listaPersonal = objDAObtenerDatos.obtenerPersonalBDTransaccional(objConfiguracion);
				Persona persona = null;
				foreach (Personal personal in listaPersonal)
				{
					persona = new Persona()
					{
						idPersona = 0,
						nombre = personal.nombre,
						apellido = personal.apellido,
						numeroDocumento = personal.numeroDocumento,
						idTipoDocumento = personal.idTipoDocumento,
						activo = personal.activo,
						fechaNacimiento = DateTime.MinValue,
						localidad = new Localidades(),
						idTipoPersona = (int)enumTipoPersona.Personal
					};
					personal.idPersona = objDAImportarDatos.GrabarPersona(persona, transaccion, personal.cargo.idCargoTransaccional, personal.idPersonalTransaccional);
					objDAImportarDatos.GrabarPersonal(personal, transaccion);
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
