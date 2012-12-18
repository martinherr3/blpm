using System;
using System.Collections.Generic;
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
		public List<RptInasistenciasAlumnoPeriodo> listaReporteInasistencias
		{
			get
			{
				if (Session["listaConsolidadaInasistencias"] == null)
					listaReporteInasistencias = new List<RptInasistenciasAlumnoPeriodo>();
				return (List<RptInasistenciasAlumnoPeriodo>)Session["listaConsolidadaInasistencias"];
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
		public List<RptSancionesAlumnoPeriodo> listaReporteSanciones
		{
			get
			{
				if (Session["listaConsolidadaSanciones"] == null)
					listaReporteSanciones = new List<RptSancionesAlumnoPeriodo>();
				return (List<RptSancionesAlumnoPeriodo>)Session["listaConsolidadaSanciones"];
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


		/// <summary>
		/// Gets or sets the alumno actual.
		/// </summary>
		/// <value>
		/// The alumno actual.
		/// </value>
		public AlumnoCursoCicloLectivo alumnoActual
		{
			get
			{
				if (ViewState["alumnoActual"] == null)
					alumnoActual = new AlumnoCursoCicloLectivo();
				return (AlumnoCursoCicloLectivo)ViewState["alumnoActual"];
			}
			set
			{
				ViewState["alumnoActual"] = value;
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
		/// Gets or sets the lista alumnos.
		/// </summary>
		/// <value>
		/// The lista alumnos.
		/// </value>
		public List<AlumnoCursoCicloLectivo> listaAlumnos
		{
			get
			{
				if (ViewState["listaAlumnos"] == null)
				{
					List<Tutor> lista = new List<Tutor>();
					lista.Add(new Tutor() { username = User.Identity.Name });
					BLAlumno objBLAlumno = new BLAlumno(new Alumno() { listaTutores = lista });
					listaAlumnos = objBLAlumno.GetAlumnosTutor(cicloLectivoActual);
				}
				return (List<AlumnoCursoCicloLectivo>)ViewState["listaAlumnos"];
			}
			set { ViewState["listaAlumnos"] = value; }
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
					CargarPresentacionPorRol();
					CargarPresentacion();
					rptResultado.verBotonGrafico = false;
				}
				//valida que las listas no sean null
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
		void VentanaAceptar(object sender, EventArgs e)
		{
			try
			{

			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
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
				string mensaje = ValidarPagina(rdlAccion.SelectedValue);
				if (string.IsNullOrEmpty(mensaje))
				{
					switch (rdlAccion.SelectedValue)
					{
						case "0":
							if (BuscarCalificaciones())
							{
								divFiltros.Visible = false;
							}
							break;
						case "1":
							if (BuscarInasistencias())
							{
								divFiltros.Visible = false;
							}
							break;
						case "2":
							if (BuscarSanciones())
							{
								divFiltros.Visible = false;
							}
							break;
						default:
							break;
					}
					divReporte.Visible = true;
					btnBuscar.Visible = false; //Se supone que no es mas necesario
					divAccion.Visible = false;
				}
				else
				{ Master.MostrarMensaje("Faltan Datos", UIConstantesGenerales.MensajeDatosFaltantes + mensaje, EDUAR_Utility.Enumeraciones.enumTipoVentanaInformacion.Advertencia); }
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
						rptResultado.CargarReporte<RptInasistenciasAlumnoPeriodo>(listaReporteInasistencias);
						break;
					case "2":
						rptResultado.CargarReporte<RptSancionesAlumnoPeriodo>(listaReporteSanciones);
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
				{
					AlumnoCursoCicloLectivo item = listaAlumnos.Find(p => p.alumno.idAlumno == idAlumno);
					CargarComboAsignatura(item.cursoCicloLectivo.idCursoCicloLectivo);
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the presentacion por rol.
		/// </summary>
		private void CargarPresentacionPorRol()
		{
			if (HttpContext.Current.User.IsInRole(enumRoles.Tutor.ToString()))
			{
				ddlAlumnosTutor.Items.Clear();
				ddlAlumnosTutor.DataSource = null;
				foreach (AlumnoCursoCicloLectivo item in listaAlumnos)
				{
					ddlAlumnosTutor.Items.Insert(ddlAlumnosTutor.Items.Count, new ListItem(item.alumno.apellido + " " + item.alumno.nombre, item.alumno.idAlumno.ToString()));
				}
				UIUtilidades.SortByText(ddlAlumnosTutor);
				ddlAlumnosTutor.Items.Insert(0, new ListItem("[Seleccione]", "-1"));
				ddlAlumnosTutor.SelectedValue = "-1";
			}

			if (HttpContext.Current.User.IsInRole(enumRoles.Alumno.ToString()))
			{
				alumnoActual.alumno.username = HttpContext.Current.User.Identity.Name;
				BLAlumno objBLAlumnos = new BLAlumno(alumnoActual.alumno);
				alumnoActual = objBLAlumnos.GetCursoActualAlumno(cicloLectivoActual);
				ddlAlumnosTutor.Items.Clear();
				ddlAlumnosTutor.Items.Insert(ddlAlumnosTutor.Items.Count, new ListItem(alumnoActual.alumno.apellido + " " + alumnoActual.alumno.nombre, alumnoActual.alumno.idAlumno.ToString()));
				ddlAlumnosTutor.Visible = false;
				lblAlumnos.Visible = false;
				CargarComboAsignatura(alumnoActual.cursoCicloLectivo.idCursoCicloLectivo);
			}
			CargarComboPeriodos(cicloLectivoActual.idCicloLectivo, ddlPeriodo);
		}

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
			btnBuscar.Visible = true;
			divAccion.Visible = true;
			divReporte.Visible = false;
		}

		/// <summary>
		/// Buscars the promedios.
		/// </summary>
		private bool BuscarCalificaciones()
		{
			StringBuilder filtros = new StringBuilder();
			int idAlumno = 0;
			int.TryParse(ddlAlumnosTutor.SelectedValue, out idAlumno);
			if (idAlumno > 0)
			{
				filtroReporte.idAlumno = idAlumno;
				filtros.AppendLine("- Alumno: " + ddlAlumnosTutor.SelectedItem.Text);

				List<Asignatura> listaAsignatura = new List<Asignatura>();
				foreach (System.Web.UI.WebControls.ListItem item in ddlAsignatura.Items)
				{
					if (item.Selected)
					{
						if (!filtros.ToString().Contains("- Asignatura"))
							filtros.AppendLine("- Asignatura");
						filtros.AppendLine(" * " + item.Text);
						listaAsignatura.Add(new Asignatura() { idAsignatura = Convert.ToInt16(item.Value) });
					}
				}
				filtroReporte.listaAsignaturas = listaAsignatura;

				filtroReporte.idCicloLectivo = cicloLectivoActual.idCicloLectivo;

				int idPeriodo = 0;
				int.TryParse(ddlPeriodo.SelectedValue, out idPeriodo);
				if (idPeriodo > 0)
					filtros.AppendLine("- Periodo: " + ddlPeriodo.SelectedItem.Text);
				filtroReporte.idPeriodo = idPeriodo;

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
			int idAlumno = 0;
			int.TryParse(ddlAlumnosTutor.SelectedValue, out idAlumno);
			if (idAlumno > 0)
			{
				filtroReporteIncidencias.idAlumno = idAlumno;
				filtros.AppendLine("- Alumno: " + ddlAlumnosTutor.SelectedItem.Text);

				filtroReporte.idCicloLectivo = cicloLectivoActual.idCicloLectivo;
                filtroReporteIncidencias.idCicloLectivo = cicloLectivoActual.idCicloLectivo;

				int idPeriodo = 0;
				int.TryParse(ddlPeriodo.SelectedValue, out idPeriodo);
				if (idPeriodo > 0)
					filtros.AppendLine("- Periodo: " + ddlPeriodo.SelectedItem.Text);
				filtroReporteIncidencias.idPeriodo = idPeriodo;

				BLRptInasistenciasAlumnoPeriodo objBLReporte = new BLRptInasistenciasAlumnoPeriodo();
				listaReporteInasistencias = objBLReporte.GetRptInasistenciasAlumnoPeriodo(filtroReporteIncidencias);

				listaReporteInasistencias.Sort((p, q) => String.Compare(p.alumno, q.alumno));

				filtrosAplicados = filtros.ToString();

				rptResultado.CargarReporte<RptInasistenciasAlumnoPeriodo>(listaReporteInasistencias);

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
			int idAlumno = 0;
			int.TryParse(ddlAlumnosTutor.SelectedValue, out idAlumno);
			if (idAlumno > 0)
			{
				filtroReporteIncidencias.idAlumno = idAlumno;
				filtros.AppendLine("- Alumno: " + ddlAlumnosTutor.SelectedItem.Text);

				filtroReporte.idCicloLectivo = cicloLectivoActual.idCicloLectivo;

				int idPeriodo = 0;
				int.TryParse(ddlPeriodo.SelectedValue, out idPeriodo);
				if (idPeriodo > 0)
					filtros.AppendLine("- Periodo: " + ddlPeriodo.SelectedItem.Text);
				filtroReporteIncidencias.idPeriodo = idPeriodo;

				BLRptSancionesAlumnoPeriodo objBLReporte = new BLRptSancionesAlumnoPeriodo();
				listaReporteSanciones = objBLReporte.GetRptSancionesAlumnoPeriodo(filtroReporteIncidencias);

				listaReporteSanciones.Sort((p, q) => String.Compare(p.alumno, q.alumno));

				filtrosAplicados = filtros.ToString();

				rptResultado.CargarReporte<RptSancionesAlumnoPeriodo>(listaReporteSanciones);

				return true;
			}
			else
				return false;
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
		private void CargarComboAsignatura(int idCursoCicloLectivo)
		{
			BLAlumno objBLAlumno = new BLAlumno();
			AlumnoCursoCicloLectivo objAlumno = new AlumnoCursoCicloLectivo();
			objAlumno.cursoCicloLectivo.idCursoCicloLectivo = idCursoCicloLectivo;
			objAlumno.cursoCicloLectivo.cicloLectivo = cicloLectivoActual;

			BLAsignatura objBLAsignatura = new BLAsignatura();
			Asignatura materia = new Asignatura();
			materia.cursoCicloLectivo.idCursoCicloLectivo = idCursoCicloLectivo;
			listaAsignatura = objBLAsignatura.GetAsignaturasCurso(materia);

			listaAsignatura.Sort((p, q) => string.Compare(p.nombre, q.nombre));
			ddlAsignatura.Items.Clear();

			foreach (Asignatura asignatura in listaAsignatura)
			{
				ddlAsignatura.Items.Add(new System.Web.UI.WebControls.ListItem(asignatura.nombre, asignatura.idAsignatura.ToString()));
			}

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
				CargarPresentacion();

				switch (rdlAccion.SelectedValue)
				{
					case "0":
						lblAsignatura.Visible = true;
						ddlAsignatura.Visible = true;
						break;
					case "1":
					case "2":
						lblAsignatura.Visible = false;
						ddlAsignatura.Visible = false;
						break;
					default:
						break;
				}
				divReporte.Visible = false;
				btnBuscar.Visible = true;
				divAccion.Visible = true;
				divFiltros.Visible = true;
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
						rptResultado.CargarReporte<RptInasistenciasAlumnoPeriodo>(listaReporteInasistencias);
					break;
				case "2":
					if (listaReporteSanciones != null)
						rptResultado.CargarReporte<RptSancionesAlumnoPeriodo>(listaReporteSanciones);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <param name="opcion">The opcion.</param>
		/// <returns></returns>
		private string ValidarPagina(string opcion)
		{
			string mensaje = string.Empty;
			int idAlumno = 0;
			int.TryParse(ddlAlumnosTutor.SelectedValue, out idAlumno);
			if (!(idAlumno > 0))
				mensaje += "- Alumno<br />";
			//switch (opcion)
			//{
			//    case "0":
			//        if (string.IsNullOrEmpty(ddlCurso.SelectedValue) || Convert.ToInt32(ddlCurso.SelectedValue) <= 0)
			//            mensaje += "- Curso<br />";
			//        break;
			//    case "1":
			//    case "2":
			//        if (string.IsNullOrEmpty(ddlCicloLectivo.SelectedValue) || Convert.ToInt32(ddlCicloLectivo.SelectedValue) <= 0)
			//            mensaje = "- Ciclo Lectivo<br />";
			//        //if (string.IsNullOrEmpty(ddlCurso.SelectedValue) || Convert.ToInt32(ddlCurso.SelectedValue) <= 0)
			//        //    mensaje += "- Curso<br />";
			//        break;
			//    default:
			//        break;
			//}
			return mensaje;
		}
		#endregion
	}
}


