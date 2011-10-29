using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
	public partial class Boletin : EDUARBasePage
	{
		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the filtro promedios.
		/// </summary>
		/// <value>
		/// The filtro sanciones.
		/// </value>
		public FilCalificacionesAlumnoPeriodo filtroReporte
		{
			get
			{
				if (ViewState["filtroCalificaciones"] == null)
					filtroReporte = new FilCalificacionesAlumnoPeriodo();
				return (FilCalificacionesAlumnoPeriodo)ViewState["filtroCalificaciones"];
			}
			set
			{
				ViewState["filtroCalificaciones"] = value;
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
		/// Gets or sets the lista calificaciones.
		/// </summary>
		/// <value>
		/// The lista sanciones.
		/// </value>
		public List<RptCalificacionesAlumnoPeriodo> listaReporte
		{
			get
			{
				if (Session["listaCalificaciones"] == null)
					listaReporte = new List<RptCalificacionesAlumnoPeriodo>();
				return (List<RptCalificacionesAlumnoPeriodo>)Session["listaCalificaciones"];
			}
			set
			{
				Session["listaCalificaciones"] = value;
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


		/// <summary>
		/// Gets or sets the obj session persona.
		/// </summary>
		/// <value>
		/// The obj session persona.
		/// </value>
		public Persona objSessionPersona
		{
			get
			{
				if (Session["objSessionPersona"] == null)
				{
					BLPersona objBLPersona = new BLPersona(new Persona() { username = ObjSessionDataUI.ObjDTUsuario.Nombre });
					objBLPersona.GetPersonaByEntidad();
					//objSessionPersona = new Persona();
					objSessionPersona = objBLPersona.Data;
				}
				return (Persona)Session["objSessionPersona"];
			}
			set { Session["objSessionPersona"] = value; }
		}


		public Alumno miAlumno
		{
			get
			{
				if (ViewState["miAlumno"] == null)
					miAlumno = new Alumno();
				return (Alumno)ViewState["miAlumno"];
			}
			set
			{
				ViewState["miAlumno"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista asignaturas.
		/// </summary>
		/// <value>
		/// The lista sanciones.
		/// </value>
		public List<Asignatura> listaAsignatura
		{
			get
			{
				if (ViewState["listaAsignatura"] == null)
					listaAsignatura = new List<Asignatura>();
				return (List<Asignatura>)ViewState["listaAsignatura"];
			}
			set
			{
				ViewState["listaAsignatura"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista asignaturas.
		/// </summary>
		/// <value>
		/// The lista sanciones.
		/// </value>
		public List<Alumno> listaAlumnos
		{
			get
			{
				if (ViewState["listaAlumnos"] == null)
					listaAlumnos = new List<Alumno>();
				return (List<Alumno>)ViewState["listaAlumnos"];
			}
			set
			{
				ViewState["listaAlumnos"] = value;
			}
		}


		/// <summary>
		/// Gets or sets the lista tipo asistencia.
		/// </summary>
		/// <value>
		/// The lista tipo asistencia.
		/// </value>
		public List<TipoAsistencia> listaTipoAsistencia
		{
			get
			{
				if (ViewState["listaTipoAsistencia"] == null)
				{
					listaTipoAsistencia = new List<TipoAsistencia>();
					BLTipoAsistencia objBLTipoAsistencia = new BLTipoAsistencia();
					listaTipoAsistencia = objBLTipoAsistencia.GetTipoAsistencia(null);
				}
				return (List<TipoAsistencia>)ViewState["listaTipoAsistencia"];
			}
			set
			{
				ViewState["listaTipoAsistencia"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista tipo sancion.
		/// </summary>
		/// <value>
		/// The lista tipo sancion.
		/// </value>
		public List<TipoSancion> listaTipoSancion
		{
			get
			{
				if (ViewState["listaTipoSancion"] == null)
				{
					listaTipoSancion = new List<TipoSancion>();
					BLTipoSancion objBLTipoSancion = new BLTipoSancion();
					listaTipoSancion = objBLTipoSancion.GetTipoSancion(null);
				}
				return (List<TipoSancion>)ViewState["listaTipoSancion"];
			}
			set
			{
				ViewState["listaTipoSancion"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista motivo sancion.
		/// </summary>
		/// <value>
		/// The lista motivo sancion.
		/// </value>
		public List<MotivoSancion> listaMotivoSancion
		{
			get
			{
				if (ViewState["listaMotivoSancion"] == null)
				{
					listaMotivoSancion = new List<MotivoSancion>();
					BLMotivoSancion objBLMotivoSancion = new BLMotivoSancion();
					listaMotivoSancion = objBLMotivoSancion.GetMotivoSanciones(null);
				}
				return (List<MotivoSancion>)ViewState["listaMotivoSancion"];
			}
			set
			{
				ViewState["listaMotivoSancion"] = value;
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

				Master.BotonAvisoAceptar += (VentanaAceptar);
				if (!Page.IsPostBack)
				{
					if (HttpContext.Current.User.IsInRole(enumRoles.Tutor.ToString()))
					{
						BLTutor objTutor = new BLTutor();

						objTutor.GetTutores(new Tutor() { username = HttpContext.Current.User.Identity.Name });
						listaAlumnos = objTutor.GetAlumnosDeTutor(new Tutor() { username = HttpContext.Current.User.Identity.Name });

					}

					if (HttpContext.Current.User.IsInRole(enumRoles.Alumno.ToString()))
					{
						BLAlumno objAlumnos = new BLAlumno();

						Alumno a = new Alumno();

						AlumnoCurso alu = new AlumnoCurso();
						alu.alumno = new Alumno();
						alu.alumno.username = HttpContext.Current.User.Identity.Name;

						listaAlumnos = objAlumnos.GetAlumnos(alu).GetRange(0, 1);
					}

					TablaPropiaGrafico = null;
					CargarPresentacion();
				}
				CargarGrillaResultado();
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
				string aux = objSessionPersona.apellido;
				bool faltanDatos = false;
				switch (rdlAccion.SelectedValue)
				{
					case "0":
						if (BuscarCalificaciones())
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
					divAccion.Visible = false;
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
				ExportPDF.ExportarPDF(Page.Title, rptResultado.dtReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
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

				CargarValoresEnControles();

				btnBuscar.Visible = true;
				divAccion.Visible = true;
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
						rptResultado.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);
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
		protected void ddlAlumnosTutor_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int idAlumno = 0;
				int.TryParse(ddlAlumnosTutor.SelectedValue, out idAlumno);
				if (idAlumno > 0)
					miAlumno.idAlumno = idAlumno;
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the valores en controles.
		/// </summary>
		private void CargarValoresEnControles()
		{
			switch (rdlAccion.SelectedValue)
			{
				case "0":
					//ddlPeriodo.SelectedValue = (filtroReporte.idPeriodo > 0) ? filtroReporte.idPeriodo.ToString() : "-1";
					break;
				case "1":
					ddlPeriodo.SelectedValue = (filtroReporteIncidencias.idPeriodo > 0) ? filtroReporteIncidencias.idPeriodo.ToString() : "-1";
					break;
				case "2":
					ddlPeriodo.SelectedValue = (filtroReporteIncidencias.idPeriodo > 0) ? filtroReporteIncidencias.idPeriodo.ToString() : "-1";
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			ddlAlumnosTutor.Items.Clear();
			CargarCombos();
			btnBuscar.Visible = true;
			divAccion.Visible = true;
			divReporte.Visible = false;
			udpPeriodo.Visible = false;
			lblPeriodo.Visible = false;
		}

		/// <summary>
		/// Buscars the promedios.
		/// </summary>
		private bool BuscarCalificaciones()
		{
			StringBuilder filtros = new StringBuilder();
			int alumno = 0;
			int.TryParse(ddlAlumnosTutor.SelectedValue, out alumno);
			if (alumno > 0)
			{
				filtroReporte.idAlumno = alumno;
				BLRptCalificacionesAlumnoPeriodo objBLReporte = new BLRptCalificacionesAlumnoPeriodo();
				listaReporte = objBLReporte.GetRptCalificacionesAlumnoPeriodo(filtroReporte);

				listaReporte.Sort((p, q) => String.Compare(p.alumno, q.alumno));

				filtrosAplicados = filtros.ToString();

				rptResultado.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);

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
			//if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlAlumnosTutor.SelectedValue) > 0)
			{
				//filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);


				filtroReporteIncidencias.idAlumno = 0;
				if (ddlAlumnosTutor.SelectedIndex > 0)
					filtroReporteIncidencias.idAlumno = Convert.ToInt32(ddlAlumnosTutor.SelectedValue);

				//List<TipoAsistencia> listaTipoAsistencia = new List<TipoAsistencia>();
				//foreach (System.Web.UI.WebControls.ListItem item in ddlAsistencia.Items)
				//{
				//    if (item.Selected)
				//    {
				//        if (!filtros.ToString().Contains("- Tipo de Inasistencia"))
				//            filtros.AppendLine("- Tipo de Inasistencia");
				//        filtros.AppendLine(" * " + item.Text);
				//        listaTipoAsistencia.Add(new TipoAsistencia() { idTipoAsistencia = Convert.ToInt16(item.Value) });
				//    }
				//}
				filtroReporteIncidencias.listaTiposAsistencia = listaTipoAsistencia;


				BLRptConsolidadoInasistenciasPeriodo objBLReporte = new BLRptConsolidadoInasistenciasPeriodo();
				listaReporteInasistencias = objBLReporte.GetRptConsolidadoInasistencias(filtroReporteIncidencias);

				listaReporteInasistencias.Sort((p, q) => String.Compare(p.alumno, q.alumno));

				filtrosAplicados = filtros.ToString();

				rptResultado.CargarReporte<RptConsolidadoInasistenciasPeriodo>(listaReporteInasistencias);

				return true;
			}
			//else
			//    return false;
		}

		/// <summary>
		/// Buscars the sanciones.
		/// </summary>
		private bool BuscarSanciones()
		{
			StringBuilder filtros = new StringBuilder();
			//if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlAlumnosTutor.SelectedValue) > 0)
			//{
			//filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);

			//filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

			//filtroReporteIncidencias.idCurso = 0;
			//if (ddlCurso.SelectedIndex > 0)
			//    filtroReporteIncidencias.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);

			//filtroReporteIncidencias.idPeriodo = 0;
			//if (ddlPeriodo.SelectedIndex > 0)
			//    filtroReporteIncidencias.idPeriodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

			filtroReporteIncidencias.idAlumno = 0;
			if (ddlAlumnosTutor.SelectedIndex > 0)
				filtroReporteIncidencias.idAlumno = Convert.ToInt32(ddlAlumnosTutor.SelectedValue);

			#region --[Tipo Sanción]--
			//List<TipoSancion> listaTipoSancionSelect = new List<TipoSancion>();
			//foreach (System.Web.UI.WebControls.ListItem item in ddlTipoSancion.Items)
			//{
			//    if (item.Selected)
			//    {
			//        if (!filtros.ToString().Contains("- Tipo de Sanción"))
			//            filtros.AppendLine("- Tipo de Sanción");
			//        filtros.AppendLine(" * " + item.Text);
			//        listaTipoSancionSelect.Add(new TipoSancion() { idTipoSancion = Convert.ToInt16(item.Value) });
			//    }
			//}
			//filtroReporteIncidencias.listaTipoSancion = listaTipoSancionSelect;
			#endregion

			#region --[Motivo Sanción]--
			List<MotivoSancion> listaMotivoSancionSelect = new List<MotivoSancion>();
			//foreach (System.Web.UI.WebControls.ListItem item in ddlMotivoSancion.Items)
			//{
			//    if (item.Selected)
			//    {
			//        if (!filtros.ToString().Contains("- Motivo de Sanción"))
			//            filtros.AppendLine("- Motivo de Sanción");
			//        filtros.AppendLine(" * " + item.Text);
			//        listaMotivoSancionSelect.Add(new MotivoSancion() { idMotivoSancion = Convert.ToInt16(item.Value) });
			//    }
			//}
			//filtroReporteIncidencias.listaMotivoSancion = listaMotivoSancionSelect;
			#endregion

			BLRptConsolidadoSancionesPeriodo objBLReporte = new BLRptConsolidadoSancionesPeriodo();
			listaReporteSanciones = objBLReporte.GetRptConsolidadoSanciones(filtroReporteIncidencias);

			listaReporteSanciones.Sort((p, q) => String.Compare(p.alumno, q.alumno));

			filtrosAplicados = filtros.ToString();

			rptResultado.CargarReporte<RptConsolidadoSancionesPeriodo>(listaReporteSanciones);

			return true;
			//}
			//else
			//    return false;
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		private void CargarCombos()
		{
			UIUtilidades.BindCombo<Alumno>(ddlAlumnosTutor, listaAlumnos, "idAlumno", "apellido", "nombre", true);

			if (ddlAlumnosTutor.Items.Count > 0)
			{
				ddlAlumnosTutor.SelectedIndex = 1;
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
			//BLAsignatura objBLAsignatura = new BLAsignatura();
			//Asignatura materia = new Asignatura();
			//materia.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(ddlCurso.SelectedValue);
			//listaAsignatura = objBLAsignatura.GetAsignaturasCurso(materia);

			//listaAsignatura.Sort((p, q) => string.Compare(p.nombre, q.nombre));
			//ddlAsignatura.Items.Clear();

			//foreach (Asignatura asignatura in listaAsignatura)
			//{
			//    ddlAsignatura.Items.Add(new System.Web.UI.WebControls.ListItem(asignatura.nombre, asignatura.idAsignatura.ToString()));
			//}

			if (ddlAsignatura.Items.Count > 0)
				ddlAsignatura.Disabled = false;
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
						divAccion.Visible = true;
						divFiltros.Visible = true;
						break;
					case "1":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divAccion.Visible = true;
						divFiltros.Visible = true;
						break;
					case "2":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divAccion.Visible = true;
						divFiltros.Visible = true;
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
		/// Cargars the grilla resultado.
		/// </summary>
		private void CargarGrillaResultado()
		{
			switch (rdlAccion.SelectedValue)
			{
				case "0":
					if (listaReporte != null)
						rptResultado.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);
					break;
				case "1":
					if (listaReporteInasistencias != null)
						rptResultado.CargarReporte<RptConsolidadoInasistenciasPeriodo>(listaReporteInasistencias);
					break;
				case "2":
					if (listaReporteSanciones != null)
						rptResultado.CargarReporte<RptConsolidadoSancionesPeriodo>(listaReporteSanciones);
					break;
				default:
					break;
			}
		}
		#endregion
	}
}


