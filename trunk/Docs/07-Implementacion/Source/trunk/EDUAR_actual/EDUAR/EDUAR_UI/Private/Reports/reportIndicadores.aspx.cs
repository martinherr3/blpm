﻿using System;
using EDUAR_UI.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using System.Collections.Generic;
using EDUAR_UI.Utilidades;
using EDUAR_Entities.Reports;
using EDUAR_BusinessLogic.Reports;

namespace EDUAR_UI
{
	public partial class reportIndicadores : EDUARBasePage
	{
		#region --[Eventos]--
		/// <summary>
		/// Método que se ejecuta al dibujar los controles de la página.
		/// Se utiliza para gestionar las excepciones del método Page_Load().
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (AvisoMostrar)
			{
				AvisoMostrar = false;

				try
				{
					Master.ManageExceptions(AvisoExcepcion);
				}
				catch (Exception ex) { Master.ManageExceptions(ex); }
			}
		}

		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				Master.BotonAvisoAceptar += (VentanaAceptar);

				if (!Page.IsPostBack)
				{
					lblCicloLectivo.Text = cicloLectivoActual.nombre;
					CargarPresentacion();
				}
			}
			catch (Exception ex)
			{
				AvisoMostrar = true;
				AvisoExcepcion = ex;
			}
		}

		/// <summary>
		/// Ventanas the aceptar.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void VentanaAceptar(object sender, EventArgs e)
		{
			try
			{
				switch (AccionPagina)
				{
					case enumAcciones.Buscar:
						break;
					case enumAcciones.Nuevo:
						break;
					case enumAcciones.Modificar:
						break;
					case enumAcciones.Eliminar:
						break;
					case enumAcciones.Seleccionar:
						break;
					case enumAcciones.Limpiar:
						break;
					case enumAcciones.Aceptar:
						break;
					case enumAcciones.Salir:
						break;
					case enumAcciones.Redirect:
						break;
					case enumAcciones.Guardar:
						break;
					case enumAcciones.Ingresar:
						break;
					case enumAcciones.Desbloquear:
						break;
					case enumAcciones.Error:
						break;
					default:
						break;
				}
				AccionPagina = enumAcciones.Limpiar;
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the Click event of the btnBuscar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnCalcular_Click(object sender, EventArgs e)
		{
			try
			{
				int idCursoCicloLectivo = 0;
				int.TryParse(ddlCurso.SelectedValue, out idCursoCicloLectivo);
				if (idCursoCicloLectivo > 0)
				{
					AccionPagina = enumAcciones.Buscar;
					obtenerDatos(idCursoCicloLectivo);
				}
				else
					Master.MostrarMensaje("Advertencia", "Debe seleccionar un curso.", enumTipoVentanaInformacion.Advertencia);
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			CargarComboCursos();
		}

		/// <summary>
		/// Cargars the combo cursos.
		/// </summary>
		private void CargarComboCursos()
		{
			int idCicloLectivo = cicloLectivoActual.idCicloLectivo;
			if (idCicloLectivo > 0)
			{
				BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
				List<Curso> listaCursos = new List<Curso>();
				if (User.IsInRole(enumRoles.Docente.ToString()))
				{
					Asignatura objFiltro = new Asignatura();
					objFiltro.curso.cicloLectivo = cicloLectivoActual;
					listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
				}
				else
				{
					Curso objCurso = new Curso();
					listaCursos = objBLCicloLectivo.GetCursosByCicloLectivo(idCicloLectivo);
				}
				UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "nombre", true);
				ddlCurso.Enabled = true;
			}
			else
			{
				ddlCurso.Enabled = false;
			}
		}

		/// <summary>
		/// Obteners the datos.
		/// </summary>
		/// <param name="idCursoCicloLectivo">The id curso ciclo lectivo.</param>
		private void obtenerDatos(int idCursoCicloLectivo)
		{
			RptIndicadores objFiltroReporte = new RptIndicadores();
			objFiltroReporte.idCursoCicloLectivo = idCursoCicloLectivo;
			BLRptIndicadores objBLIndicadores = new BLRptIndicadores();

			List<RptIndicadores> lista = objBLIndicadores.GetRptIndicadores(objFiltroReporte);
			AplicarMetodo(lista);
		}

		/// <summary>
		/// Aplicars the metodo.
		/// </summary>
		/// <param name="lista">The lista.</param>
		private void AplicarMetodo(List<RptIndicadores> lista)
		{
			decimal pesoPromedio = 0, pesoInasistencia = 0, pesoSancion = 0;
			pesoPromedio = criterioCalificacion.pesoCriterio;
			pesoInasistencia = criterioInasistencia.pesoCriterio;
			pesoSancion = criterioSancion.pesoCriterio;
		}
		#endregion

		
	}
}