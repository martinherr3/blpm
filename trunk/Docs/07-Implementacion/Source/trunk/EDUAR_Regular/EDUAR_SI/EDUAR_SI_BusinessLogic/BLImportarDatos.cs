using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_SI_DataAccess;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_SI_BusinessLogic
{
	public class BLImportarDatos : BLProcesoBase
	{
		#region --[Atributos]--
		Configuraciones objConfiguracion;

		DAImportarDatos objDAImportarDatos;

		DAObtenerDatos objDAObtenerDatos;

		#endregion

		#region --[Propiedades]--
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor. LLama al constructor de la clase base BLProcesoBase.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos.</param>
		public BLImportarDatos(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Procedimientoes the importar datos.
		/// </summary>
		public void ProcedimientoImportarDatos()
		{
			try
			{
				objDAImportarDatos = new DAImportarDatos(ConnectionString);

				objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigenDEV);
				//objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigen);
				//objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigenDesdeRemoto);

				ImportarDatos();
			}
			catch (Exception ex)
			{
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
			try
			{
				objDAObtenerDatos = new DAObtenerDatos(objConfiguracion.valor);

				//queda pendiente -> serian exámenes finales
				objDAImportarDatos.GrabarCalificacion(objDAObtenerDatos.obtenerExamenBDTransaccional(objConfiguracion), null);


				objDAImportarDatos.GrabarDiasHorarios(objDAObtenerDatos.obtenerHorarios(objConfiguracion),null);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}
