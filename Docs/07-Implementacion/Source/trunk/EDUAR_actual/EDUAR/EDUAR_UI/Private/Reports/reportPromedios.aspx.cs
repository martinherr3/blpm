using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_BusinessLogic.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

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

		/// <summary>
		/// Gets or sets the lista ciclo lectivo.
		/// </summary>
		/// <value>
		/// The lista ciclo lectivo.
		/// </value>
		public List<CicloLectivo> listaCicloLectivo
		{
			get
			{
				if (ViewState["listaCicloLectivo"] == null)
				{
					listaCicloLectivo = new List<CicloLectivo>();
					BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
					listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);
				}
				return (List<CicloLectivo>)ViewState["listaCicloLectivo"];
			}
			set
			{
				ViewState["listaCicloLectivo"] = value;
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
					TablaPropiaGrafico = null;
					CargarPresentacion();
					divReporte.Visible = false;
					btnBuscar.Visible = true;
					divAccion.Visible = true;
					divFiltros.Visible = true;
					lblAsignatura.Visible = true;
					ddlAsignatura.Visible = true;
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
		/// Graficars the reporte.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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

		/// <summary>
		/// Graficars the promedios.
		/// </summary>
		private void GraficarPromedios()
		{

			// Cuando se elija 
			try
			{
				GenerarDatosGraficoCalificaciones();
				Double sumaNotas = 0;
				rptResultado.graficoReporte.LimpiarSeries();
				var serie = new List<RptPromedioCalificacionesPeriodo>();
				rptResultado.graficoReporte.habilitarTorta = false;
				foreach (var item in listaAsignatura)
				{
					sumaNotas = 0;
					var listaParcial = listaReporte.FindAll(p => p.asignatura == item.nombre);
					if (listaParcial.Count > 0)
					{
						foreach (var nota in listaParcial)
						{
							sumaNotas += Convert.ToDouble(nota.promedio);
						}

						serie.Add(new RptPromedioCalificacionesPeriodo
						{
							promedio = Math.Round(sumaNotas / listaParcial.Count, 2).ToString(CultureInfo.InvariantCulture),
							asignatura = item.nombre
							//asignatura = ""
						});
					}
				}
				DataTable dt = UIUtilidades.BuildDataTable<RptPromedioCalificacionesPeriodo>(serie);
				rptResultado.graficoReporte.AgregarSerie("Asignatura", dt, "asignatura", "promedio");
				//rptResultado.graficoReporte.AgregarSerie("", dt, "asignatura", "promedio");

				BLValoresEscalaCalificacion objBLNivelAprobacion = new BLValoresEscalaCalificacion();
				ValoresEscalaCalificacion escala = new ValoresEscalaCalificacion();
				escala = objBLNivelAprobacion.GetNivelProbacion();

				List<ValoresEscalaCalificacion> listaEscala = new List<ValoresEscalaCalificacion>();
				foreach (RptPromedioCalificacionesPeriodo item in serie)
				{
					listaEscala.Add(new ValoresEscalaCalificacion
					{
						valor = escala.valor,
						nombre = item.asignatura
					});
				}
				DataTable dtEscala = UIUtilidades.BuildDataTable<ValoresEscalaCalificacion>(listaEscala);
				rptResultado.graficoReporte.AgregarSerie("Nivel de Aprobación (" + escala.nombre + ")", dtEscala, "nombre", "valor", true);

				string alumno = string.Empty;
				if (ddlAlumno.Items.Count > 0 && Convert.ToInt32(ddlAlumno.SelectedValue) > 0)
					alumno = "\n" + ddlAlumno.SelectedItem.Text + "\n";
				string Titulo = "Promedio Por Asignatura \n" + ddlCurso.SelectedItem.Text + " - " + ddlCicloLectivo.SelectedItem.Text;
				if (ddlPeriodo.Items.Count > 0 && Convert.ToInt32(ddlPeriodo.SelectedValue) > 0)
					Titulo += " - " + ddlPeriodo.SelectedItem.Text;

				Titulo += alumno;
				rptResultado.graficoReporte.Titulo = Titulo;

				rptResultado.graficoReporte.GraficarBarra();
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Graficars the inasistencia.
		/// </summary>
		private void GraficarInasistencia()
		{
			try
			{
				GenerarDatosGraficoInasistencias();
				rptResultado.graficoReporte.LimpiarSeries();
				var serie = new List<RptInasistenciasAlumnoPeriodo>();
				if (ddlAlumno.SelectedIndex <= 0 && ddlCurso.SelectedIndex <= 0)
				{
					//foreach (var item in listaTipoAsistencia)
					for (int i = 0; i <= 6; i++)
					{
						// Esto es una chanchada, si anda lo corrijo con un listado de niveles
						var listaPorNivel = listaReporteInasistencias.FindAll(p => p.nivel.Substring(0, 1) == i.ToString());
						if (listaPorNivel.Count > 0)
						{
							serie.Add(new RptInasistenciasAlumnoPeriodo
							{
								curso = i.ToString() + "º Año",
								alumno = listaPorNivel.Count.ToString()
							});
						}
					}
					DataTable dt = UIUtilidades.BuildDataTable<RptInasistenciasAlumnoPeriodo>(serie);
					rptResultado.graficoReporte.AgregarSerie("Inasistencias", dt, "curso", "alumno");

					string alumno = string.Empty;
					if (ddlAlumno.Items.Count > 0 && Convert.ToInt32(ddlAlumno.SelectedValue) > 0)
						alumno = "\n" + ddlAlumno.SelectedItem.Text + "\n";
					string Titulo = "Inasistencias \n" + ddlCurso.SelectedItem.Text + " - " + ddlCicloLectivo.SelectedItem.Text;
					if (ddlPeriodo.Items.Count > 0 && Convert.ToInt32(ddlPeriodo.SelectedValue) > 0)
						Titulo += " - " + ddlPeriodo.SelectedItem.Text;

					Titulo += alumno;
					rptResultado.graficoReporte.Titulo = Titulo;
				}
				else //if (ddlCurso.SelectedIndex > 0 && ddlAlumno.SelectedIndex <= 0)
				{
					foreach (var item in listaTipoAsistencia)
					{
						var listaPorTipoAsistencia = listaReporteInasistencias.FindAll(p => p.motivo == item.descripcion);
						if (listaPorTipoAsistencia.Count > 0)
						{
							serie.Add(new RptInasistenciasAlumnoPeriodo
							{
								motivo = item.descripcion,
								alumno = listaPorTipoAsistencia.Count.ToString()
							});
						}
					}
					DataTable dt = UIUtilidades.BuildDataTable<RptInasistenciasAlumnoPeriodo>(serie);
					rptResultado.graficoReporte.AgregarSerie("Inasistencias", dt, "motivo", "alumno");

					string alumno = string.Empty;
					if (ddlAlumno.Items.Count > 0 && Convert.ToInt32(ddlAlumno.SelectedValue) > 0)
						alumno = "\n" + ddlAlumno.SelectedItem.Text + "\n";
					string Titulo = "Motivos de Inasistencias \n" + ddlCurso.SelectedItem.Text + " - " + ddlCicloLectivo.SelectedItem.Text;
					if (ddlPeriodo.Items.Count > 0 && Convert.ToInt32(ddlPeriodo.SelectedValue) > 0)
						Titulo += " - " + ddlPeriodo.SelectedItem.Text;

					Titulo += alumno;
					rptResultado.graficoReporte.Titulo = Titulo;

					string escala = BLConfiguracionGlobal.ObtenerConfiguracion(EDUAR_Utility.Enumeraciones.enumConfiguraciones.LimiteInasistencias);

					List<ValoresEscalaCalificacion> listaEscala = new List<ValoresEscalaCalificacion>();
					foreach (RptInasistenciasAlumnoPeriodo item in serie)
					{
						listaEscala.Add(new ValoresEscalaCalificacion
						{
							valor = escala,
							nombre = item.motivo
						});
					}
					DataTable dtEscala = UIUtilidades.BuildDataTable<ValoresEscalaCalificacion>(listaEscala);
					rptResultado.graficoReporte.AgregarSerie("Máximo de Faltas (" + escala + ")", dtEscala, "nombre", "valor", true);
				}

				rptResultado.graficoReporte.GraficarBarra();
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }

		}

		/// <summary>
		/// Graficars the sanciones.
		/// </summary>
		private void GraficarSanciones()
		{
			try
			{
				GenerarDatosGraficoSanciones();
				RptSancionesAlumnoPeriodo rptAux;
				rptResultado.graficoReporte.LimpiarSeries();
				var serie = new List<RptSancionesAlumnoPeriodo>();
				if (ddlAlumno.SelectedIndex > 0)
				{
					var listaParcial = listaReporteSanciones.FindAll(p => p.alumno == ddlAlumno.SelectedItem.Text);

					foreach (var item in listaTipoSancion)
					{
						var listaPorTipoSancion = listaParcial.FindAll(p => p.tipo == item.nombre);
						if (listaPorTipoSancion.Count > 0)
						{
							rptAux = new RptSancionesAlumnoPeriodo();
							rptAux.tipo = item.nombre;
							rptAux.cantidad = 0;
							foreach (var item2 in listaPorTipoSancion)
							{
								rptAux.cantidad += Convert.ToInt16(item2.sanciones);
							}

							serie.Add(rptAux);
						}
					}
					if (serie != null)
					{
						DataTable dt = UIUtilidades.BuildDataTable<RptSancionesAlumnoPeriodo>(serie);
						// En cantidad envio la cantidad de sanciones y en tipo la sancion
						rptResultado.graficoReporte.AgregarSerie(ddlAlumno.SelectedItem.Text, dt, "tipo", "cantidad");
						rptResultado.graficoReporte.Titulo = "Sanciones " + ddlAlumno.SelectedItem.Text;
					}

					string escala = BLConfiguracionGlobal.ObtenerConfiguracion(EDUAR_Utility.Enumeraciones.enumConfiguraciones.SancionesExpulsion);

					List<ValoresEscalaCalificacion> listaEscala = new List<ValoresEscalaCalificacion>();
					foreach (RptSancionesAlumnoPeriodo item in serie)
					{
						listaEscala.Add(new ValoresEscalaCalificacion
						{
							valor = escala,
							nombre = item.tipo
						});
					}
					DataTable dtEscala = UIUtilidades.BuildDataTable<ValoresEscalaCalificacion>(listaEscala);
					rptResultado.graficoReporte.AgregarSerie("Nivel de Expulsión (" + escala + ")", dtEscala, "nombre", "valor", true);
				}
				else
				{
					foreach (var item in listaTipoSancion)
					{
						var listaPorTipoSancion = listaReporteSanciones.FindAll(p => p.tipo == item.nombre);
						if (listaPorTipoSancion.Count > 0)
						{
							rptAux = new RptSancionesAlumnoPeriodo();
							rptAux.tipo = item.nombre;
							rptAux.cantidad = 0;
							foreach (var item2 in listaPorTipoSancion)
							{
								rptAux.cantidad += Convert.ToInt16(item2.sanciones);
							}
							serie.Add(rptAux);
						}
					}
					DataTable dt = UIUtilidades.BuildDataTable<RptSancionesAlumnoPeriodo>(serie);
					rptResultado.graficoReporte.AgregarSerie("Sanciones", dt, "tipo", "cantidad");

					string alumno = string.Empty;
					if (ddlAlumno.Items.Count > 0 && Convert.ToInt32(ddlAlumno.SelectedValue) > 0)
						alumno = "\n" + ddlAlumno.SelectedItem.Text + "\n";
					string Titulo = "Sanciones \n" + ddlCurso.SelectedItem.Text + " - " + ddlCicloLectivo.SelectedItem.Text;
					if (ddlPeriodo.Items.Count > 0 && Convert.ToInt32(ddlPeriodo.SelectedValue) > 0)
						Titulo += " - " + ddlPeriodo.SelectedItem.Text;

					Titulo += alumno;
					rptResultado.graficoReporte.Titulo = Titulo;
				}

				rptResultado.graficoReporte.GraficarBarra();
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
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
				string mensaje = ValidarPagina(rdlAccion.SelectedValue);
				if (string.IsNullOrEmpty(mensaje))
				{
					switch (rdlAccion.SelectedValue)
					{
						case "0":
							if (BuscarPromedios())
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

		private string ValidarPagina(string opcion)
		{
			string mensaje = string.Empty;

			switch (opcion)
			{
				case "0":
					if (string.IsNullOrEmpty(ddlCicloLectivo.SelectedValue) || Convert.ToInt32(ddlCicloLectivo.SelectedValue) <= 0)
						mensaje = "- Ciclo Lectivo<br />";
					if (string.IsNullOrEmpty(ddlCurso.SelectedValue) || Convert.ToInt32(ddlCurso.SelectedValue) <= 0)
						mensaje += "- Curso<br />";
					break;
				case "1":
				case "2":
					if (string.IsNullOrEmpty(ddlCicloLectivo.SelectedValue) || Convert.ToInt32(ddlCicloLectivo.SelectedValue) <= 0)
						mensaje = "- Ciclo Lectivo<br />";
					//if (string.IsNullOrEmpty(ddlCurso.SelectedValue) || Convert.ToInt32(ddlCurso.SelectedValue) <= 0)
					//    mensaje += "- Curso<br />";
					break;
				default:
					break;
			}
			return mensaje;
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
		/// Cargars the valores en controles.
		/// </summary>
		private void CargarValoresEnControles()
		{
			switch (rdlAccion.SelectedValue)
			{
				case "0":
					ddlCicloLectivo.SelectedValue = (filtroReporte.idCicloLectivo > 0) ? filtroReporte.idCicloLectivo.ToString() : "-1";
					if (filtroReporte.idCicloLectivo > 0)
						CargarComboCursos(filtroReporte.idCicloLectivo, ddlCurso);
					ddlCurso.SelectedValue = (filtroReporte.idCurso > 0) ? filtroReporte.idCurso.ToString() : "-1";
					ddlPeriodo.SelectedValue = (filtroReporte.idPeriodo > 0) ? filtroReporte.idPeriodo.ToString() : "-1";
					if (filtroReporte.idCurso > 0)
					{
						CargarAlumnos(filtroReporte.idCurso);
						CargarComboAsignatura();
					}
					ddlAlumno.SelectedValue = (filtroReporte.idAlumno > 0) ? filtroReporte.idAlumno.ToString() : "-1";
					ddlAlumno.Enabled = (filtroReporte.idCurso > 0);

					//ddlAsignatura.SelectedIndex = (filtroReporte.idAsignatura > 0) ? filtroReporte.idAsignatura.ToString() : "-1";
					ddlAsignatura.Disabled = (filtroReporte.idCurso < 0);
					lblAsignatura.Visible = true;
					ddlAsignatura.Visible = true;
					ddlTipoSancion.Visible = false;
					lblTipoSanción.Visible = false;
					ddlMotivoSancion.Visible = false;
					lblMotivoSanción.Visible = false;
					ddlAsistencia.Visible = false;
					lblTipoAsistencia.Visible = false;
					break;
				case "1":
					ddlCicloLectivo.SelectedValue = (filtroReporteIncidencias.idCicloLectivo > 0) ? filtroReporteIncidencias.idCicloLectivo.ToString() : "-1";
					if (filtroReporteIncidencias.idCicloLectivo > 0)
						CargarComboCursos(filtroReporteIncidencias.idCicloLectivo, ddlCurso);
					ddlCurso.SelectedValue = (filtroReporteIncidencias.idCurso > 0) ? filtroReporteIncidencias.idCurso.ToString() : "-1";
					ddlPeriodo.SelectedValue = (filtroReporteIncidencias.idPeriodo > 0) ? filtroReporteIncidencias.idPeriodo.ToString() : "-1";
					if (filtroReporteIncidencias.idCurso > 0)
					{
						CargarAlumnos(filtroReporteIncidencias.idCurso);
					}
					ddlAlumno.SelectedValue = (filtroReporteIncidencias.idAlumno > 0) ? filtroReporteIncidencias.idAlumno.ToString() : "-1";
					ddlAlumno.Enabled = (filtroReporteIncidencias.idCurso > 0);
					lblAsignatura.Visible = false;
					ddlAsignatura.Visible = false;
					ddlTipoSancion.Visible = false;
					lblTipoSanción.Visible = false;
					ddlMotivoSancion.Visible = false;
					lblMotivoSanción.Visible = false;
					ddlAsistencia.Visible = true;
					lblTipoAsistencia.Visible = true;
					break;
				case "2":
					ddlCicloLectivo.SelectedValue = (filtroReporteIncidencias.idCicloLectivo > 0) ? filtroReporteIncidencias.idCicloLectivo.ToString() : "-1";
					if (filtroReporteIncidencias.idCicloLectivo > 0)
						CargarComboCursos(filtroReporteIncidencias.idCicloLectivo, ddlCurso);
					ddlCurso.SelectedValue = (filtroReporteIncidencias.idCurso > 0) ? filtroReporteIncidencias.idCurso.ToString() : "-1";
					ddlPeriodo.SelectedValue = (filtroReporteIncidencias.idPeriodo > 0) ? filtroReporteIncidencias.idPeriodo.ToString() : "-1";
					if (filtroReporteIncidencias.idCurso > 0)
					{
						CargarAlumnos(filtroReporteIncidencias.idCurso);
					}
					ddlAlumno.SelectedValue = (filtroReporteIncidencias.idAlumno > 0) ? filtroReporteIncidencias.idAlumno.ToString() : "-1";
					ddlAlumno.Enabled = (filtroReporteIncidencias.idCurso > 0);
					lblAsignatura.Visible = false;
					ddlAsignatura.Visible = false;
					ddlTipoSancion.Visible = true;
					lblTipoSanción.Visible = true;
					ddlMotivoSancion.Visible = true;
					lblMotivoSanción.Visible = true;
					ddlAsistencia.Visible = false;
					lblTipoAsistencia.Visible = false;

					break;
				default:
					break;
			}

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
				HabilitaCursoYPeriodo();

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
				int idCurso = Convert.ToInt32(ddlCurso.SelectedValue);
				if (idCurso > 0)
				{
					CargarAlumnos(idCurso);
					ddlAlumno.Enabled = true;
					CargarComboAsignatura();
				}
				else
				{
					ddlAlumno.Items.Clear();
					ddlAlumno.Enabled = false;
					ddlAsignatura.Items.Clear();
					ddlAsignatura.Disabled = true;
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
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			ddlAlumno.Items.Clear();
			ddlAsignatura.Items.Clear();
			CargarCombos();
			lblAsignatura.Visible = true;
			ddlAsignatura.Visible = true;
			ddlTipoSancion.Visible = false;
			lblTipoSanción.Visible = false;
			ddlMotivoSancion.Visible = false;
			lblMotivoSanción.Visible = false;
			ddlAsistencia.Visible = false;
			lblTipoAsistencia.Visible = false;
			btnBuscar.Visible = true;
			divAccion.Visible = true;
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
			if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 /*&& Convert.ToInt32(ddlCurso.SelectedValue) > 0*/)
			{
				filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text);

				if (Convert.ToInt32(ddlCurso.SelectedValue) > 0)
					filtros.AppendLine(" - Curso: " + ddlCurso.SelectedItem.Text);

				filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

				filtroReporteIncidencias.idCurso = 0;
				if (ddlCurso.SelectedIndex > 0)
					filtroReporteIncidencias.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);

				filtroReporteIncidencias.idPeriodo = 0;
				//TODO: Testear el siguiente condicional
				if (ddlPeriodo.SelectedIndex > 0)
					filtroReporteIncidencias.idPeriodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

				filtroReporteIncidencias.idAlumno = 0;
				if (ddlAlumno.SelectedIndex > 0)
					filtroReporteIncidencias.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);

				List<TipoAsistencia> listaTipoAsistencia = new List<TipoAsistencia>();
				foreach (System.Web.UI.WebControls.ListItem item in ddlAsistencia.Items)
				{
					if (item.Selected)
					{
						if (!filtros.ToString().Contains("- Tipo de Inasistencia"))
							filtros.AppendLine("- Tipo de Inasistencia");
						filtros.AppendLine(" * " + item.Text);
						listaTipoAsistencia.Add(new TipoAsistencia() { idTipoAsistencia = Convert.ToInt16(item.Value) });
					}
				}
				filtroReporteIncidencias.listaTiposAsistencia = listaTipoAsistencia;

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
			if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 /*&& Convert.ToInt32(ddlCurso.SelectedValue) > 0*/)
			{
				filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text);

				if (Convert.ToInt32(ddlCurso.SelectedValue) > 0)
					filtros.AppendLine(" - Curso: " + ddlCurso.SelectedItem.Text);

				filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

				filtroReporteIncidencias.idCurso = 0;
				if (ddlCurso.SelectedIndex > 0)
					filtroReporteIncidencias.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);

				filtroReporteIncidencias.idPeriodo = 0;
				if (ddlPeriodo.SelectedIndex > 0)
					filtroReporteIncidencias.idPeriodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

				filtroReporteIncidencias.idAlumno = 0;
				if (ddlAlumno.SelectedIndex > 0)
					filtroReporteIncidencias.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);

				#region --[Tipo Sanción]--
				List<TipoSancion> listaTipoSancionSelect = new List<TipoSancion>();
				foreach (System.Web.UI.WebControls.ListItem item in ddlTipoSancion.Items)
				{
					if (item.Selected)
					{
						if (!filtros.ToString().Contains("- Tipo de Sanción"))
							filtros.AppendLine("- Tipo de Sanción");
						filtros.AppendLine(" * " + item.Text);
						listaTipoSancionSelect.Add(new TipoSancion() { idTipoSancion = Convert.ToInt16(item.Value) });
					}
				}
				filtroReporteIncidencias.listaTipoSancion = listaTipoSancionSelect;
				#endregion

				#region --[Motivo Sanción]--
				List<MotivoSancion> listaMotivoSancionSelect = new List<MotivoSancion>();
				foreach (System.Web.UI.WebControls.ListItem item in ddlMotivoSancion.Items)
				{
					if (item.Selected)
					{
						if (!filtros.ToString().Contains("- Motivo de Sanción"))
							filtros.AppendLine("- Motivo de Sanción");
						filtros.AppendLine(" * " + item.Text);
						listaMotivoSancionSelect.Add(new MotivoSancion() { idMotivoSancion = Convert.ToInt16(item.Value) });
					}
				}
				filtroReporteIncidencias.listaMotivoSancion = listaMotivoSancionSelect;
				#endregion

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
			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
			List<Curso> listaCurso = new List<Curso>();
			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);

			ddlCicloLectivo.SelectedIndex = ddlCicloLectivo.Items.Count - 1;
			HabilitaCursoYPeriodo();

			#region --[Tipo Asistencia]--
			// Ordena la lista alfabéticamente por la descripción
			listaTipoAsistencia.Sort((p, q) => string.Compare(p.descripcion, q.descripcion));

			if (ddlAsistencia.Items.Count == 0)
				// Carga el combo de tipo de asistencia para filtrar
				foreach (TipoAsistencia item in listaTipoAsistencia)
				{
					ddlAsistencia.Items.Add(new System.Web.UI.WebControls.ListItem(item.descripcion, item.idTipoAsistencia.ToString()));
				}
			#endregion

			#region --[Tipo Sanción]--
			// Ordena la lista alfabéticamente por la descripción
			listaTipoSancion.Sort((p, q) => string.Compare(p.nombre, q.nombre));

			if (ddlTipoSancion.Items.Count == 0)
				// Carga el combo de tipo de asistencia para filtrar
				foreach (TipoSancion item in listaTipoSancion)
				{
					ddlTipoSancion.Items.Add(new System.Web.UI.WebControls.ListItem(item.nombre, item.idTipoSancion.ToString()));
				}
			#endregion

			#region --[Motivo Sanción]--
			// Ordena la lista alfabéticamente por la descripción
			listaMotivoSancion.Sort((p, q) => string.Compare(p.descripcion, q.descripcion));

			if (ddlMotivoSancion.Items.Count == 0)
				// Carga el combo de tipo de asistencia para filtrar
				foreach (MotivoSancion item in listaMotivoSancion)
				{
					ddlMotivoSancion.Items.Add(new System.Web.UI.WebControls.ListItem(item.descripcion, item.idMotivoSancion.ToString()));
				}
			#endregion

			//ddlPeriodo.Enabled = false;
			ddlAsignatura.Disabled = true;
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
				Asignatura objAsignatura = new Asignatura();
				objAsignatura.curso.cicloLectivo.idCicloLectivo = idCicloLectivo;

				if (User.IsInRole(enumRoles.Docente.ToString()))
					objAsignatura.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

				listaCurso = objBLCicloLectivo.GetCursosByAsignatura(objAsignatura);
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
			materia.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(ddlCurso.SelectedValue);
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
				switch (rdlAccion.SelectedValue)
				{
					case "0":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divAccion.Visible = true;
						divFiltros.Visible = true;
						lblAsignatura.Visible = true;
						ddlAsignatura.Visible = true;
						ddlTipoSancion.Visible = false;
						lblTipoSanción.Visible = false;
						ddlMotivoSancion.Visible = false;
						lblMotivoSanción.Visible = false;
						ddlAsistencia.Visible = false;
						lblTipoAsistencia.Visible = false;
						break;
					case "1":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divAccion.Visible = true;
						divFiltros.Visible = true;
						lblAsignatura.Visible = false;
						ddlAsignatura.Visible = false;
						ddlTipoSancion.Visible = false;
						lblTipoSanción.Visible = false;
						ddlMotivoSancion.Visible = false;
						lblMotivoSanción.Visible = false;
						ddlAsistencia.Visible = true;
						lblTipoAsistencia.Visible = true;
						break;
					case "2":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divAccion.Visible = true;
						divFiltros.Visible = true;
						lblAsignatura.Visible = false;
						ddlAsignatura.Visible = false;
						ddlTipoSancion.Visible = true;
						lblTipoSanción.Visible = true;
						ddlMotivoSancion.Visible = true;
						lblMotivoSanción.Visible = true;
						ddlAsistencia.Visible = false;
						lblTipoAsistencia.Visible = false;
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
		/// Cargars the alumnos.
		/// </summary>
		/// <param name="idCurso">The id curso.</param>
		private void CargarAlumnos(int idCurso)
		{
			BLAlumno objBLAlumno = new BLAlumno();

			UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", "nombre", true);
		}

		/// <summary>
		/// Habilitas the curso Y periodo.
		/// </summary>
		private void HabilitaCursoYPeriodo()
		{
			int idCicloLectivo = 0;
			idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
			CargarComboCursos(idCicloLectivo, ddlCurso);
			CargarComboPeriodos(idCicloLectivo, ddlPeriodo);
			ddlCurso.Enabled = true;
			ddlPeriodo.Enabled = true;
			udpCurso.Update();
			udpPeriodo.Update();
		}

		/// <summary>
		/// Generars the datos grafico.
		/// </summary>
		private void GenerarDatosGraficoCalificaciones()
		{
			TablaPropiaGrafico = new List<TablaGrafico>();
			TablaGrafico tabla3 = new TablaGrafico();
			tabla3.listaCuerpo = new List<List<string>>();
			List<string> encabezado3 = new List<string>();
			List<string> fila3 = new List<string>();

			tabla3.titulo = "Periodo Analizado: " + listaReporte[0].periodo;

			var cantAlumnos =
				from p in listaReporte
				group p by p.alumno into g
				select new { Alumno = g.Key, Cantidad = g.Count() };

			//TablaGrafico.Add("- Cantidad de Alumnos analizados: " + cantAlumnos.Count().ToString());
			encabezado3.Add("Cantidad de Alumnos");
			fila3.Add(cantAlumnos.Count().ToString());

			//TablaGrafico.Add("- Cantidad de Calificaciones: " + listaReporte.Count.ToString());
			encabezado3.Add("Cantidad de Calificaciones");
			fila3.Add(listaReporte.Count().ToString());

			tabla3.listaEncabezados = encabezado3;
			tabla3.listaCuerpo.Add(fila3);
			TablaPropiaGrafico.Add(tabla3);

			if (ddlAsignatura.Items.Count == 0 || Convert.ToInt32(ddlAsignatura.SelectedIndex) < 0)
			{
				TablaGrafico tabla2 = new TablaGrafico();
				tabla2.listaCuerpo = new List<List<string>>();
				List<string> encabezado2 = new List<string>();
				List<List<string>> filasTabla2 = new List<List<string>>();
				List<string> fila2 = new List<string>();

				//TablaGrafico.Add("- Desviacion Estandar por materia: ");
				tabla2.titulo = "Top 3 Materias con mejor desempeño";
				encabezado2.Add("Asignatura");
				encabezado2.Add("Promedio");

				var topPromedio =
				   (from p in listaReporte
					group p by p.asignatura into g
					orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
					select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)), Cantidad = g.Count() }).Distinct().Take(3);

				//TablaGrafico.Add("- Top 3 Materias con mejor desempeño:");
				foreach (var item in topPromedio)
				{
					//    TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString("#.00") + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
					fila2 = new List<string>();
					fila2.Add(item.Asignatura);
					fila2.Add(item.Promedio.ToString("#.00"));
					filasTabla2.Add(fila2);
				}
				tabla2.listaEncabezados = encabezado2;
				tabla2.listaCuerpo = filasTabla2;
				TablaPropiaGrafico.Add(tabla2);

				var worstPromedio =
				   (from p in listaReporte
					group p by p.asignatura into g
					orderby g.Average(p => Convert.ToDouble(p.promedio)) ascending
					select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)), Cantidad = g.Count() }).Distinct().Take(3);

				//TablaGrafico.Add("- Top 3 Materias con bajo desempeño:");
				TablaGrafico tabla4 = new TablaGrafico();
				tabla4.listaCuerpo = new List<List<string>>();
				List<string> encabezado4 = new List<string>();
				List<List<string>> filasTabla4 = new List<List<string>>();
				List<string> fila4 = new List<string>();

				//TablaGrafico.Add("- Top 3 Materias con mejor desempeño:");
				tabla4.titulo = "Top 3 Asignaturas con mejor desempeño";
				encabezado4.Add("Asignatura");
				encabezado4.Add("Promedio");

				tabla4.listaEncabezados = encabezado4;

				foreach (var item in worstPromedio)
				{
					//TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString("#.00") + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
					fila4 = new List<string>();
					fila4.Add(item.Asignatura);
					fila4.Add(item.Promedio.ToString("#.##"));
					filasTabla4.Add(fila4);
				}
				tabla4.listaEncabezados = encabezado4;
				tabla4.listaCuerpo = filasTabla4;
				TablaPropiaGrafico.Add(tabla4);
			}

			if (ddlAlumno.Items.Count > 0 && Convert.ToInt32(ddlAlumno.SelectedValue) < 0)
			{
				TablaGrafico tabla5 = new TablaGrafico();
				tabla5.listaCuerpo = new List<List<string>>();
				List<string> encabezado5 = new List<string>();
				List<List<string>> filasTabla5 = new List<List<string>>();
				List<string> fila5 = new List<string>();

				tabla5.titulo = "Top 3 de Alumnos a observar";
				encabezado5.Add("Alumno");
				encabezado5.Add("Promedio General");

				var worstAlumnos =
				   (from p in listaReporte
					group p by p.alumno into g
					orderby g.Average(p => Convert.ToDouble(p.promedio)) ascending
					select new { Alumno = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct().Take(3);

				//TablaGrafico.Add("- Top 3 de Alumnos a observar:");
				foreach (var item in worstAlumnos)
				{
					//TablaGrafico.Add(item.Alumno + " - Promedio General: " + item.Promedio.ToString("#.00"));
					fila5 = new List<string>();
					fila5.Add(item.Alumno);
					fila5.Add(item.Promedio.ToString("#.00"));
					filasTabla5.Add(fila5);
				}
				tabla5.listaEncabezados = encabezado5;
				tabla5.listaCuerpo = filasTabla5;
				TablaPropiaGrafico.Add(tabla5);
			}
		}

		/// <summary>
		/// Generars the datos grafico.
		/// </summary>
		private void GenerarDatosGraficoInasistencias()
		{
			TablaPropiaGrafico = new List<TablaGrafico>();
			TablaGrafico tabla3 = new TablaGrafico();
			tabla3.listaCuerpo = new List<List<string>>();
			List<string> encabezado3 = new List<string>();
			List<string> fila3 = new List<string>();

			tabla3.titulo = "Periodo Analizado: " + listaReporteInasistencias[0].periodo;

			var cantAlumnos =
				from p in listaReporteInasistencias
				group p by p.alumno into g
				select new { Alumno = g.Key, Cantidad = g.Count() };

			encabezado3.Add("Cantidad de Alumnos");
			fila3.Add(cantAlumnos.Count().ToString());

			encabezado3.Add("Inasistencias Totales");
			fila3.Add(listaReporteInasistencias.Count().ToString());

			tabla3.listaEncabezados = encabezado3;
			tabla3.listaCuerpo.Add(fila3);
			TablaPropiaGrafico.Add(tabla3);

			var worstAlumnos =
				 (from p in listaReporteInasistencias
				  group p by p.alumno into g
				  orderby g.Sum(p => Convert.ToDouble(p.inasistencias)) descending
				  select new { Alumno = g.Key, Faltas = g.Sum(p => Convert.ToDouble(p.inasistencias)) }).Distinct().Take(3);

			TablaGrafico tabla2 = new TablaGrafico();
			tabla2.listaCuerpo = new List<List<string>>();
			List<string> encabezado2 = new List<string>();
			List<List<string>> filasTabla2 = new List<List<string>>();
			List<string> fila2 = new List<string>();

			tabla2.titulo = "Top 3 de Alumnos a observar";
			encabezado2.Add("Alumno");
			encabezado2.Add("Inasistencias");

			foreach (var item in worstAlumnos)
			{
				fila2 = new List<string>();
				fila2.Add(item.Alumno);
				fila2.Add(item.Faltas.ToString());
				filasTabla2.Add(fila2);
			}
			tabla2.listaEncabezados = encabezado2;
			tabla2.listaCuerpo = filasTabla2;
			TablaPropiaGrafico.Add(tabla2);

			var worstAlumnosByMotivo =
			(from p in listaReporteInasistencias
			 group p by new { p.alumno, p.motivo } into g
			 orderby g.Sum(p => Convert.ToDouble(p.inasistencias)) descending
			 select new { Alumno = g.Key.alumno, Motivo = g.Key.motivo, Faltas = g.Sum(p => Convert.ToDouble(p.inasistencias)) }).Distinct().Take(3);

			TablaGrafico tabla5 = new TablaGrafico();
			tabla5.listaCuerpo = new List<List<string>>();
			List<string> encabezado5 = new List<string>();
			List<List<string>> filasTabla5 = new List<List<string>>();
			List<string> fila5 = new List<string>();

			tabla5.titulo = "Top 3 de Alumnos a observar por Cantidad y Motivo de Inasistencias";
			encabezado5.Add("Alumno");
			encabezado5.Add("Motivo");
			encabezado5.Add("Cantidad");

			//TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Motivo de Inasistencias:");
			foreach (var item in worstAlumnosByMotivo)
			{
				//TablaGrafico.Add("Alumno: " + item.Alumno + " Motivo: " + item.Motivo + " - Cantidad de Inasistencias: " + item.Faltas);
				fila5 = new List<string>();
				fila5.Add(item.Alumno);
				fila5.Add(item.Motivo);
				fila5.Add(item.Faltas.ToString());
				filasTabla5.Add(fila5);
			}
			tabla5.listaEncabezados = encabezado5;
			tabla5.listaCuerpo = filasTabla5;
			TablaPropiaGrafico.Add(tabla5);
		}

		/// <summary>
		/// Generars the datos grafico.
		/// </summary>
		private void GenerarDatosGraficoSanciones()
		{
			TablaPropiaGrafico = new List<TablaGrafico>();
			TablaGrafico tabla3 = new TablaGrafico();
			tabla3.listaCuerpo = new List<List<string>>();
			List<string> encabezado3 = new List<string>();
			List<string> fila3 = new List<string>();

			tabla3.titulo = "Periodo Analizado: " + listaReporteSanciones[0].periodo;

			var cantAlumnos =
				from p in listaReporteSanciones
				group p by p.alumno into g
				select new { Alumno = g.Key, Cantidad = g.Count() };

			//TablaGrafico.Add("- Cantidad de Alumnos analizados: " + cantAlumnos.Count().ToString());

			//if (listaReporteSanciones.Count() > 0)
			//    TablaGrafico.Add("- Periodo de notas: " + listaReporteSanciones[0].periodo);

			encabezado3.Add("Cantidad de Alumnos");
			fila3.Add(cantAlumnos.Count().ToString());

			//TablaGrafico.Add("- Cantidad de Calificaciones: " + listaReporte.Count.ToString());
			//encabezado3.Add("Sanciones Totales");
			//fila3.Add(listaReporteSanciones.Count().ToString());

			tabla3.listaEncabezados = encabezado3;
			tabla3.listaCuerpo.Add(fila3);
			TablaPropiaGrafico.Add(tabla3);

			var worstAlumnos =
				 (from p in listaReporteSanciones
				  group p by p.alumno into g
				  orderby g.Sum(p => Convert.ToInt16(p.sanciones)) descending
				  select new { Alumno = g.Key, Sanciones = g.Sum(p => Convert.ToInt16(p.sanciones)) }).Distinct().Take(3);

			TablaGrafico tabla2 = new TablaGrafico();
			tabla2.listaCuerpo = new List<List<string>>();
			List<string> encabezado2 = new List<string>();
			List<List<string>> filasTabla2 = new List<List<string>>();
			List<string> fila2 = new List<string>();

			//TablaGrafico.Add("- Desviacion Estandar por materia: ");
			tabla2.titulo = "Top 3 de Alumnos a observar por Cantidad de Sanciones";
			encabezado2.Add("Alumno");
			encabezado2.Add("Sanciones");

			//TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad de Sanciones:");

			foreach (var item in worstAlumnos)
			{
				//TablaGrafico.Add("Alumno: " + item.Alumno + " - Cantidad de Sanciones: " + item.Sanciones);
				fila2 = new List<string>();
				fila2.Add(item.Alumno);
				fila2.Add(item.Sanciones.ToString());
				filasTabla2.Add(fila2);
			}
			tabla2.listaEncabezados = encabezado2;
			tabla2.listaCuerpo = filasTabla2;
			TablaPropiaGrafico.Add(tabla2);

			var SancionesPorTipo =
				  (from p in listaReporteSanciones
				   group p by p.tipo into g
				   orderby g.Sum(p => Convert.ToInt32(p.sanciones)) descending
				   select new { Tipo = g.Key, Sanciones = g.Sum(p => Convert.ToInt32(p.sanciones)) }).Distinct().Take(3);

			TablaGrafico tabla4 = new TablaGrafico();
			tabla4.listaCuerpo = new List<List<string>>();
			List<string> encabezado4 = new List<string>();
			List<List<string>> filasTabla4 = new List<List<string>>();
			List<string> fila4 = new List<string>();

			//TablaGrafico.Add("- Desviacion Estandar por materia: ");
			tabla4.titulo = "Cantidad de Sanciones según Tipo";
			encabezado4.Add("Alumno");
			encabezado4.Add("Sanciones");

			//TablaGrafico.Add("- Cantidad de Sanciones según Tipo:");
			foreach (var item in SancionesPorTipo)
			{
				//TablaGrafico.Add("Tipo de Sancion: " + item.Tipo + " - Cantidad de Sanciones: " + item.Sanciones);
				fila4 = new List<string>();
				fila4.Add(item.Tipo);
				fila4.Add(item.Sanciones.ToString());
				filasTabla4.Add(fila4);
			}
			tabla4.listaEncabezados = encabezado4;
			tabla4.listaCuerpo = filasTabla4;
			TablaPropiaGrafico.Add(tabla4);

			var SancionesPorMotivo =
				  (from p in listaReporteSanciones
				   group p by p.motivo into g
				   orderby g.Sum(p => Convert.ToInt32(p.sanciones)) descending
				   select new { Motivo = g.Key, Sanciones = g.Sum(p => Convert.ToInt32(p.sanciones)) }).Distinct().Take(3);

			TablaGrafico tabla5 = new TablaGrafico();
			tabla5.listaCuerpo = new List<List<string>>();
			List<string> encabezado5 = new List<string>();
			List<List<string>> filasTabla5 = new List<List<string>>();
			List<string> fila5 = new List<string>();

			tabla5.titulo = "Cantidad de Sanciones según Motivo";
			encabezado5.Add("Motivo");
			encabezado5.Add("Cantidad");
			//TablaGrafico.Add("- Cantidad de Sanciones según Motivo:");
			foreach (var item in SancionesPorMotivo)
			{
				//TablaGrafico.Add("Motivo de Sancion: " + item.Motivo + " - Cantidad de Sanciones: " + item.Sanciones);
				fila5 = new List<string>();
				fila5.Add(item.Motivo);
				fila5.Add(item.Sanciones.ToString());
				filasTabla5.Add(fila5);
			}
			tabla5.listaEncabezados = encabezado5;
			tabla5.listaCuerpo = filasTabla5;
			TablaPropiaGrafico.Add(tabla5);

			var worstAlumnosByMotivo =
			(from p in listaReporteSanciones
			 group p by new { p.alumno, p.motivo } into g
			 orderby g.Sum(p => Convert.ToInt32(p.sanciones)) descending
			 select new { Alumno = g.Key.alumno, Motivo = g.Key.motivo, Sanciones = g.Sum(p => Convert.ToInt32(p.sanciones)) }).Distinct().Take(3);

			//TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Motivo de Sanciones:");
			TablaGrafico tabla6 = new TablaGrafico();
			tabla6.listaCuerpo = new List<List<string>>();
			List<string> encabezado6 = new List<string>();
			List<List<string>> filasTabla6 = new List<List<string>>();
			List<string> fila6 = new List<string>();

			tabla6.titulo = "Top 3 de Alumnos a observar por Cantidad y Motivo de Sanciones";
			encabezado6.Add("Alumno");
			encabezado6.Add("Motivo");
			encabezado6.Add("Cantidad");

			foreach (var item in worstAlumnosByMotivo)
			{
				//TablaGrafico.Add("Alumno: " + item.Alumno + " Motivo: " + item.Motivo + " - Cantidad de Sanciones: " + item.Sanciones);
				fila6 = new List<string>();
				fila6.Add(item.Alumno);
				fila6.Add(item.Motivo);
				fila6.Add(item.Sanciones.ToString());
				filasTabla6.Add(fila6);
			}
			tabla6.listaEncabezados = encabezado6;
			tabla6.listaCuerpo = filasTabla6;
			TablaPropiaGrafico.Add(tabla6);

			var worstAlumnosByTipo =
			(from p in listaReporteSanciones
			 group p by new { p.alumno, p.tipo } into g
			 orderby g.Sum(p => Convert.ToInt32(p.sanciones)) descending
			 select new { Alumno = g.Key.alumno, Tipo = g.Key.tipo, Sanciones = g.Sum(p => Convert.ToInt32(p.sanciones)) }).Distinct().Take(3);

			//TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Tipo de Sanciones:");
			TablaGrafico tabla7 = new TablaGrafico();
			tabla7.listaCuerpo = new List<List<string>>();
			List<string> encabezado7 = new List<string>();
			List<List<string>> filasTabla7 = new List<List<string>>();
			List<string> fila7 = new List<string>();

			tabla7.titulo = "Top 3 de Alumnos a observar por Cantidad y Tipo de Sanciones";
			encabezado7.Add("Alumno");
			encabezado7.Add("Tipo");
			encabezado7.Add("Cantidad");

			foreach (var item in worstAlumnosByTipo)
			{
				//TablaGrafico.Add("Alumno: " + item.Alumno + " Tipo: " + item.Tipo + " - Cantidad de Sanciones: " + item.Sanciones);
				fila7 = new List<string>();
				fila7.Add(item.Alumno);
				fila7.Add(item.Tipo);
				fila7.Add(item.Sanciones.ToString());
				filasTabla7.Add(fila7);
			}
			tabla7.listaEncabezados = encabezado7;
			tabla7.listaCuerpo = filasTabla7;
			TablaPropiaGrafico.Add(tabla7);
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
						rptResultado.CargarReporte<RptPromedioCalificacionesPeriodo>(listaReporte);
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