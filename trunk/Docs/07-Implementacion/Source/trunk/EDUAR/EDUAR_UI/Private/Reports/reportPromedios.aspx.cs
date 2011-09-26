using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;

namespace EDUAR_UI
{
	public partial class reportPromedios : EDUARBasePage
	{
		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the filtro promedios.
		/// </summary>
		/// <value>
		/// The filtro sanciones.
		/// </value>
		public FilPromedioCalificacionesPeriodo filtroReporte
		{
			get
			{
				if (ViewState["filtroPromedios"] == null)
					filtroReporte = new FilPromedioCalificacionesPeriodo();
				return (FilPromedioCalificacionesPeriodo)ViewState["filtroPromedios"];
			}
			set
			{
				ViewState["filtroPromedios"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the filtro incidencias.
		/// </summary>
		/// <value>
		/// The filtro incidencias.
		/// </value>
		public FilIncidenciasAlumno filtroReporteIncidencias
		{
			get
			{
				if (ViewState["filtroIncidencias"] == null)
					filtroReporteIncidencias = new FilIncidenciasAlumno();
				return (FilIncidenciasAlumno)ViewState["filtroIncidencias"];
			}
			set
			{
				ViewState["filtroIncidencias"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista promedios.
		/// </summary>
		/// <value>
		/// The lista sanciones.
		/// </value>
		public List<RptPromedioCalificacionesPeriodo> listaReporte
		{
			get
			{
				if (Session["listaPromedios"] == null)
					listaReporte = new List<RptPromedioCalificacionesPeriodo>();
				return (List<RptPromedioCalificacionesPeriodo>)Session["listaPromedios"];
			}
			set
			{
				Session["listaPromedios"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista consolidada inasistencias.
		/// </summary>
		/// <value>
		/// The lista consolidada inasistencias.
		/// </value>
		public List<RptConsolidadoInasistenciasPeriodo> listaReporteInasistencias
		{
			get
			{
				if (Session["listaConsolidadaInasistencias"] == null)
					listaReporteInasistencias = new List<RptConsolidadoInasistenciasPeriodo>();
				return (List<RptConsolidadoInasistenciasPeriodo>)Session["listaConsolidadaInasistencias"];
			}
			set
			{
				Session["listaConsolidadaInasistencias"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista consolidada sanciones.
		/// </summary>
		/// <value>
		/// The lista consolidada sanciones.
		/// </value>
		public List<RptConsolidadoSancionesPeriodo> listaReporteSanciones
		{
			get
			{
				if (Session["listaConsolidadaSanciones"] == null)
					listaReporteSanciones = new List<RptConsolidadoSancionesPeriodo>();
				return (List<RptConsolidadoSancionesPeriodo>)Session["listaConsolidadaSanciones"];
			}
			set
			{
				Session["listaConsolidadaSanciones"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the filtros aplicados.
		/// </summary>
		/// <value>
		/// The filtros aplicados.
		/// </value>
		public string filtrosAplicados
		{
			get
			{
				if (Session["filtrosAplicados"] == null)
					filtrosAplicados = string.Empty;
				return Session["filtrosAplicados"].ToString();
			}
			set
			{
				Session["filtrosAplicados"] = value;
			}
		}
		#endregion

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
				rptResultado.ExportarPDFClick += (ExportarPDF);
				rptResultado.VolverClick += (VolverReporte);
				rptResultado.PaginarGrilla += (PaginarGrilla);
				rptResultado.GraficarClick += (GraficarReporte);

				Master.BotonAvisoAceptar += (VentanaAceptar);
				if (!Page.IsPostBack)
				{
					CargarPresentacion();
					btnBuscar.Visible = false;
					divFiltros.Visible = false;
					divReporte.Visible = false;
				}

			}
			catch (Exception ex)
			{
				AvisoMostrar = true;
				AvisoExcepcion = ex;
			}
		}

		protected void GraficarReporte(object sender, EventArgs e)
		{
			try
			{
				switch (rdlAccion.SelectedValue)
				{
					case "0":
						GraficarPromedios();
						break;
					case "1":
						GraficarInasistencia();
						break;
					case "2":
						GraficarSanciones();
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		private void GraficarPromedios()
		{
			throw new NotImplementedException();
		}

		private void GraficarInasistencia()
		{
			throw new NotImplementedException();
		}

		private void GraficarSanciones()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Ventanas the aceptar.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void VentanaAceptar(object sender, EventArgs e)
		{
			//divFiltros.Visible = true;
			//divReporte.Visible = false;
		}

		/// <summary>
		/// Handles the Click event of the btnBuscar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			try
			{
				bool faltanDatos = false;
				switch (rdlAccion.SelectedValue)
				{
					case "0":
						if (BuscarPromedios())
						{
							divFiltros.Visible = false;
						}
						else { faltanDatos = true; }
						break;
					case "1":
						if (BuscarInasistencias())
						{
							divFiltros.Visible = false;
						}
						else { faltanDatos = true; }
						break;
					case "2":
						if (BuscarSanciones())
						{
							divFiltros.Visible = false;
						}
						else { faltanDatos = true; }
						break;
					default:
						break;
				}
				if (faltanDatos)
				{ Master.MostrarMensaje("Faltan Datos", UIConstantesGenerales.MensajeDatosRequeridos, EDUAR_Utility.Enumeraciones.enumTipoVentanaInformacion.Advertencia); }
				else
				{
					divReporte.Visible = true;
					btnBuscar.Visible = false; //Se supone que no es mas necesario
				}
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Exportars the PDF.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ExportarPDF(object sender, EventArgs e)
		{
			try
			{
				ExportPDF.ExportarPDF(Page.Title, rptResultado.dtReporte, ObjDTSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Volvers the reporte.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void VolverReporte(object sender, EventArgs e)
		{
			try
			{
				CargarPresentacion();
				btnBuscar.Visible = true;
				divFiltros.Visible = true;
				divReporte.Visible = false;
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Paginars the grilla.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		protected void PaginarGrilla(object sender, GridViewPageEventArgs e)
		{
			try
			{
				int pagina = e.NewPageIndex;
				rptResultado.GrillaReporte.PageIndex = pagina;

				switch (rdlAccion.SelectedValue)
				{
					case "0":
						rptResultado.CargarReporte<RptPromedioCalificacionesPeriodo>(listaReporte);
						break;
					case "1":
						rptResultado.CargarReporte<RptConsolidadoInasistenciasPeriodo>(listaReporteInasistencias);
						break;
					case "2":
						rptResultado.CargarReporte<RptConsolidadoSancionesPeriodo>(listaReporteSanciones);
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the ddlCicloLectivo control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ddlCicloLectivo_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int idCicloLectivo = 0;
				if (rdlAccion.SelectedValue == "0")
				{
					idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
					CargarComboCursos(idCicloLectivo, ddlCurso);
					CargarComboPeriodos(idCicloLectivo, ddlPeriodo);
				}
				if (rdlAccion.SelectedValue == "1" || rdlAccion.SelectedValue == "2")
				{
					idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
					CargarComboCursos(idCicloLectivo, ddlCurso);
					CargarComboPeriodos(idCicloLectivo, ddlPeriodo);
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the ddlCurso control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (rdlAccion.SelectedValue == "0")
				{
					CargarAlumnos(Convert.ToInt32(ddlCurso.SelectedValue));
					ddlAlumno.Enabled = true;
				}
				if (rdlAccion.SelectedValue == "1" || rdlAccion.SelectedValue == "2")
				{
					CargarAlumnos(Convert.ToInt32(ddlCurso.SelectedValue));
					ddlAlumno.Enabled = true;
				}

				CargarComboAsignatura();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Cargars the alumnos.
		/// </summary>
		/// <param name="idCurso">The id curso.</param>
		private void CargarAlumnos(int idCurso)
		{
			BLAlumno objBLAlumno = new BLAlumno();

			if (rdlAccion.SelectedValue == "0")
			{
				UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", "nombre", true);
				ddlAlumno.Enabled = true;
			}
			if (rdlAccion.SelectedValue == "1" || rdlAccion.SelectedValue == "2")
			{
				UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", "nombre", true);
				ddlAlumno.Enabled = true;
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			CargarCombos();
			btnBuscar.Visible = true;
			divReporte.Visible = false;
		}

		/// <summary>
		/// Buscars the promedios.
		/// </summary>
		private bool BuscarPromedios()
		{
			StringBuilder filtros = new StringBuilder();
			if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlCurso.SelectedValue) > 0)
			{
				filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);

				filtroReporte.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

				filtroReporte.idCurso = 0;
				if (ddlCurso.SelectedIndex > 0)
					filtroReporte.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);

				filtroReporte.idPeriodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

				filtroReporte.idAsignatura = 0;
				if (ddlAsignatura.SelectedIndex > 0)
					filtroReporte.idAsignatura = Convert.ToInt32(ddlAsignatura.SelectedValue);

				filtroReporte.idAlumno = 0;
				if (ddlAlumno.SelectedIndex > 0)
					filtroReporte.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);

				BLRptPromedioCalificacionesPeriodo objBLReporte = new BLRptPromedioCalificacionesPeriodo();
				listaReporte = objBLReporte.GetRptPromedioCalificaciones(filtroReporte);

				listaReporte.Sort((p, q) => String.Compare(p.alumno, q.alumno));

				filtrosAplicados = filtros.ToString();

				rptResultado.CargarReporte<RptPromedioCalificacionesPeriodo>(listaReporte);

				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Buscars the inasistencias.
		/// </summary>
		private bool BuscarInasistencias()
		{
			StringBuilder filtros = new StringBuilder();
			if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlCurso.SelectedValue) > 0)
			{
				filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);

				filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

				filtroReporteIncidencias.idCurso = 0;
				if (ddlCurso.SelectedIndex > 0)
					filtroReporteIncidencias.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);

				filtroReporteIncidencias.idPeriodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

				filtroReporteIncidencias.idAlumno = 0;
				if (ddlAlumno.SelectedIndex > 0)
					filtroReporteIncidencias.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);

				BLRptConsolidadoInasistenciasPeriodo objBLReporte = new BLRptConsolidadoInasistenciasPeriodo();
				listaReporteInasistencias = objBLReporte.GetRptConsolidadoInasistencias(filtroReporteIncidencias);

				listaReporteInasistencias.Sort((p, q) => String.Compare(p.alumno, q.alumno));

				filtrosAplicados = filtros.ToString();

				rptResultado.CargarReporte<RptConsolidadoInasistenciasPeriodo>(listaReporteInasistencias);

				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Buscars the sanciones.
		/// </summary>
		private bool BuscarSanciones()
		{
			StringBuilder filtros = new StringBuilder();
			if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlCurso.SelectedValue) > 0)
			{
				filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);

				filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

				filtroReporteIncidencias.idCurso = 0;
				if (ddlCurso.SelectedIndex > 0)
					filtroReporteIncidencias.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);

				filtroReporteIncidencias.idPeriodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

				filtroReporteIncidencias.idAlumno = 0;
				if (ddlAlumno.SelectedIndex > 0)
					filtroReporteIncidencias.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);

				BLRptConsolidadoSancionesPeriodo objBLReporte = new BLRptConsolidadoSancionesPeriodo();
				listaReporteSanciones = objBLReporte.GetRptConsolidadoSanciones(filtroReporteIncidencias);

				listaReporteSanciones.Sort((p, q) => String.Compare(p.alumno, q.alumno));

				filtrosAplicados = filtros.ToString();

				rptResultado.CargarReporte<RptConsolidadoSancionesPeriodo>(listaReporteSanciones);

				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		private void CargarCombos()
		{
			List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
			BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
			listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);

			List<Curso> listaCurso = new List<Curso>();
			List<Alumno> listaAlumno = new List<Alumno>();

			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);

			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);

			ddlPeriodo.Enabled = false;
			ddlAsignatura.Enabled = false;
			ddlCurso.Enabled = false;
			ddlAlumno.Enabled = false;
		}

		/// <summary>
		/// Cargars the combo cursos.
		/// </summary>
		/// <param name="idCicloLectivo">The id ciclo lectivo.</param>
		/// <param name="ddlCurso">The DDL curso.</param>
		private void CargarComboCursos(int idCicloLectivo, DropDownList ddlCurso)
		{
			if (idCicloLectivo > 0)
			{
				List<Curso> listaCurso = new List<Curso>();
				BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
				Curso objCurso = new Curso();

				listaCurso = objBLCicloLectivo.GetCursosByCicloLectivo(idCicloLectivo);

				UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "nombre", true);

				ddlCurso.Enabled = true;
			}
			else
			{
				ddlCurso.Enabled = false;
			}
		}

		/// <summary>
		/// Cargars the combo periodos.
		/// </summary>
		/// <param name="idCicloLectivo">The id ciclo lectivo.</param>
		/// <param name="ddlPeriodo">The DDL periodo.</param>
		private void CargarComboPeriodos(int idCicloLectivo, DropDownList ddlPeriodo)
		{
			if (idCicloLectivo > 0)
			{
				BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();

				UIUtilidades.BindCombo<Periodo>(ddlPeriodo, objBLCicloLectivo.GetPeriodosByCicloLectivo(idCicloLectivo), "idPeriodo", "nombre", true);

				ddlPeriodo.Enabled = true;
			}
			else
			{
				ddlPeriodo.Enabled = false;
			}
		}

		/// <summary>
		/// Cargars the combo asignatura.
		/// </summary>
		private void CargarComboAsignatura()
		{
			BLAsignatura objBLAsignatura = new BLAsignatura();
			Asignatura materia = new Asignatura();
			materia.curso.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);
			UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, objBLAsignatura.GetAsignaturasCurso(materia), "idAsignatura", "nombre", true);
			if (ddlAsignatura.Items.Count > 0)
				ddlAsignatura.Enabled = true;
		}

		/// <summary>
		/// Handles the OnSelectedIndexChanged event of the rdlAccion control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void rdlAccion_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				switch (rdlAccion.SelectedValue)
				{
					case "0":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divFiltros.Visible = true;
						lblAsignatura.Visible = true;
						ddlAsignatura.Visible = true;
						break;
					case "1":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divFiltros.Visible = true;
						lblAsignatura.Visible = false;
						ddlAsignatura.Visible = false;
						break;
					case "2":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divFiltros.Visible = true;
						lblAsignatura.Visible = false;
						ddlAsignatura.Visible = false;
						break;
					default:
						break;
				}

			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion
	}
}