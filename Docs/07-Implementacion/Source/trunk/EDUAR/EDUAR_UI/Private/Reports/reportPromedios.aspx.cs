using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using System.Linq;

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
					divReporte.Visible = false;
					btnBuscar.Visible = true;
					divAccion.Visible = true;
					divFiltros.Visible = true;
					lblAsignatura.Visible = true;
					ddlAsignatura.Visible = true;
				}
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
						var serie = new List<RptPromedioCalificacionesPeriodo>();

                        serie.Add(new RptPromedioCalificacionesPeriodo
                        {
                            promedio = Math.Round(sumaNotas / listaParcial.Count, 2).ToString(CultureInfo.InvariantCulture),
                            asignatura = item.nombre
                            //asignatura = ""
                        });

						DataTable dt = UIUtilidades.BuildDataTable<RptPromedioCalificacionesPeriodo>(serie);
                       rptResultado.graficoReporte.AgregarSerie(item.nombre, dt, "asignatura", "promedio");
                       //rptResultado.graficoReporte.AgregarSerie("", dt, "asignatura", "promedio");
                           
                        rptResultado.graficoReporte.Titulo = "Promedios ";
					}
				}
				rptResultado.graficoReporte.GraficarBarra();
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

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
                    for(int i=0; i<= 6; i++)
                    {
                        // Esto es una chanchada, si anda lo corrijo con un listado de niveles
                        var listaPorNivel = listaReporteInasistencias.FindAll(p => p.nivel.Substring(0,1) ==i.ToString());
                        if (listaPorNivel.Count > 0)
                        {
                            serie.Add(new RptInasistenciasAlumnoPeriodo
                            {
                                curso = i.ToString()+"º Año",
                                alumno = listaPorNivel.Count.ToString()
                            });
                        }
                    }
                    DataTable dt = UIUtilidades.BuildDataTable<RptInasistenciasAlumnoPeriodo>(serie);
                    rptResultado.graficoReporte.AgregarSerie("Inasistencias", dt, "curso", "alumno");

                    rptResultado.graficoReporte.Titulo = "Inasistencias por Cursos en el Ciclo Lectivo "+ ddlCicloLectivo.SelectedItem.ToString();
                }
                else if(ddlCurso.SelectedIndex > 0 && ddlAlumno.SelectedIndex <= 0)
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

                   rptResultado.graficoReporte.Titulo = "Inasistencias por Motivo en el Curso " + ddlCurso.SelectedItem.ToString();
                }
                               
				rptResultado.graficoReporte.GraficarBarra();
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }

		}

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

                    rptResultado.graficoReporte.Titulo = "Sanciones";
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
				ddlCicloLectivo.SelectedValue = (filtroReporte.idCicloLectivo > 0) ? filtroReporte.idCicloLectivo.ToString() : "-1";
				ddlCurso.SelectedValue = (filtroReporte.idCurso > 0) ? filtroReporte.idCurso.ToString() : "-1";
				ddlAsignatura.SelectedValue = (filtroReporte.idAsignatura > 0) ? filtroReporte.idAsignatura.ToString() : "-1";

				ddlPeriodo.SelectedValue = (filtroReporte.idPeriodo > 0) ? filtroReporte.idPeriodo.ToString() : "-1";
				if (filtroReporte.idCurso > 0)
				{
					CargarAlumnos(filtroReporte.idCurso);
				}
				ddlAlumno.SelectedValue = (filtroReporte.idAlumno > 0) ? filtroReporte.idAlumno.ToString() : "-1";
				ddlAlumno.Enabled = (filtroReporte.idCurso > 0);

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
					ddlAsignatura.Enabled = false;
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
			if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 /*&& Convert.ToInt32(ddlCurso.SelectedValue) > 0*/)
			{
				filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);

				filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

				filtroReporteIncidencias.idCurso = 0;
				if (ddlCurso.SelectedIndex > 0)
					filtroReporteIncidencias.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);

                filtroReporteIncidencias.idPeriodo = 0;
                if (ddlCurso.SelectedIndex > 0)
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

			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
			List<Curso> listaCurso = new List<Curso>();
			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);

			ddlCicloLectivo.SelectedIndex = ddlCicloLectivo.Items.Count - 1;
			HabilitaCursoYPeriodo();
			//ddlPeriodo.Enabled = false;
			ddlAsignatura.Enabled = false;
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
			listaAsignatura = objBLAsignatura.GetAsignaturasCurso(materia);
			UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, listaAsignatura, "idAsignatura", "nombre", true);
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
						divAccion.Visible = true;
						divFiltros.Visible = true;
						lblAsignatura.Visible = true;
						ddlAsignatura.Visible = true;
						break;
					case "1":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divAccion.Visible = true;
						divFiltros.Visible = true;
						lblAsignatura.Visible = false;
						ddlAsignatura.Visible = false;
						break;
					case "2":
						CargarPresentacion();
						divReporte.Visible = false;
						btnBuscar.Visible = true;
						divAccion.Visible = true;
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
            var cantAlumnos =
                from p in listaReporte
                group p by p.alumno into g
                select new { Alumno = g.Key, Cantidad = g.Count() };

            TablaGrafico.Add("- Cantidad de Alumnos analizados: " + cantAlumnos.Count().ToString());

            TablaGrafico.Add("- Cantidad de Calificaciones: " + listaReporte.Count.ToString());

            if (listaReporte.Count() > 0)
                TablaGrafico.Add("- Periodo de Inasistencias: " + listaReporte[0].periodo);

            var topPromedio =
               (from p in listaReporte
                group p by p.asignatura into g
                orderby g.Average(p => Convert.ToInt32(p.promedio)) descending
                select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.promedio)), Cantidad = g.Count() }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 Materias con mejor desempeño:");
            foreach (var item in topPromedio)
            {
                TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString() + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
            }

            var worstPromedio =
               (from p in listaReporte
                group p by p.asignatura into g
                orderby g.Average(p => Convert.ToInt32(p.promedio)) ascending
                select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.promedio)), Cantidad = g.Count() }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 Materias con bajo desempeño:");
            foreach (var item in worstPromedio)
            {
                TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString() + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
            }

            var worstAlumnos =
               (from p in listaReporte
                group p by p.alumno into g
                orderby g.Average(p => Convert.ToInt32(p.promedio)) ascending
                select new { Alumno = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.promedio)) }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 de Alumnos a observar:");
            foreach (var item in worstAlumnos)
            {
                TablaGrafico.Add(item.Alumno + " - Promedio General: " + item.Promedio.ToString("#.##"));
            }
        }


        /// <summary>
        /// Generars the datos grafico.
        /// </summary>
        private void GenerarDatosGraficoInasistencias()
        {
            var cantAlumnos =
                from p in listaReporteInasistencias
                group p by p.alumno into g
                select new { Alumno = g.Key, Cantidad = g.Count() };

            TablaGrafico.Add("- Cantidad de Alumnos analizados: " + cantAlumnos.Count().ToString());

            if (listaReporteInasistencias.Count() > 0)
                TablaGrafico.Add("- Periodo de notas: " + listaReporteInasistencias[0].periodo);

            var worstAlumnos =
                 (from p in listaReporteInasistencias
                  group p by p.alumno into g
                  orderby g.Count() descending
                  select new { Alumno = g.Key, Faltas = g.Count() }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad de Inasistencias:");
            foreach (var item in worstAlumnos)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " - Cantidad de Inasistencias: " + item.Faltas);
            }

            var FaltasPorMotivo =
                  (from p in listaReporteInasistencias
                   group p by p.motivo into g
                   orderby g.Count() descending
                   select new { Motivo = g.Key, Faltas = g.Count() }).Distinct().Take(3);

            TablaGrafico.Add("- Cantidad de Inasistencias según Motivo:");
            foreach (var item in FaltasPorMotivo)
            {
                TablaGrafico.Add("Motivo de Ausencia: " + item.Motivo + " - Cantidad de Ocurrencias: " + item.Faltas);
            }

            var worstAlumnosByMotivo =
            (from p in listaReporteInasistencias
             group p by new { p.alumno, p.motivo } into g
             orderby g.Count() descending
             select new { Alumno = g.Key.alumno, Motivo = g.Key.motivo, Faltas = g.Count() }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Motivo de Inasistencias:");
            foreach (var item in worstAlumnosByMotivo)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " Motivo: " + item.Motivo + " - Cantidad de Inasistencias: " + item.Faltas);
            }
        }

        /// <summary>
        /// Generars the datos grafico.
        /// </summary>
        private void GenerarDatosGraficoSanciones()
        {
            var cantAlumnos =
                from p in listaReporteSanciones
                group p by p.alumno into g
                select new { Alumno = g.Key, Cantidad = g.Count() };

            TablaGrafico.Add("- Cantidad de Alumnos analizados: " + cantAlumnos.Count().ToString());

            if (listaReporteSanciones.Count() > 0)
                TablaGrafico.Add("- Periodo de notas: " + listaReporteSanciones[0].periodo);


            var worstAlumnos =
                 (from p in listaReporteSanciones
                  group p by p.alumno into g
                  orderby g.Count() descending
                  select new { Alumno = g.Key, Sanciones = g.Sum(p => Convert.ToInt16(p.sanciones)) }).Distinct().Take(3);


            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad de Sanciones:");

            foreach (var item in worstAlumnos)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " - Cantidad de Sanciones: " + item.Sanciones);
            }

            var SancionesPorTipo =
                  (from p in listaReporteSanciones
                   group p by p.tipo into g
                   orderby g.Count() descending
                   select new { Tipo = g.Key, Sanciones = g.Sum(p => Convert.ToInt32(p.sanciones)) }).Distinct().Take(3);

            TablaGrafico.Add("- Cantidad de Sanciones según Tipo:");
            foreach (var item in SancionesPorTipo)
            {
                TablaGrafico.Add("Tipo de Sancion: " + item.Tipo + " - Cantidad de Sanciones: " + item.Sanciones);
            }

            var SancionesPorMotivo =
                  (from p in listaReporteSanciones
                   group p by p.motivo into g
                   orderby g.Count() descending
                   select new { Motivo = g.Key, Sanciones = g.Sum(p => Convert.ToInt32(p.sanciones)) }).Distinct().Take(3);

            TablaGrafico.Add("- Cantidad de Sanciones según Motivo:");
            foreach (var item in SancionesPorMotivo)
            {
                TablaGrafico.Add("Motivo de Sancion: " + item.Motivo + " - Cantidad de Sanciones: " + item.Sanciones);
            }


            var worstAlumnosByMotivo =
            (from p in listaReporteSanciones
             group p by new { p.alumno, p.motivo } into g
             orderby g.Sum(p => Convert.ToInt32(p.sanciones)) descending
             select new { Alumno = g.Key.alumno, Motivo = g.Key.motivo, Sanciones = g.Sum(p => Convert.ToInt32(p.sanciones)) }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Motivo de Sanciones:");
            foreach (var item in worstAlumnosByMotivo)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " Motivo: " + item.Motivo + " - Cantidad de Sanciones: " + item.Sanciones);
            }


            var worstAlumnosByTipo =
            (from p in listaReporteSanciones
             group p by new { p.alumno, p.tipo } into g
             orderby g.Sum(p => Convert.ToInt32(p.sanciones)) descending
             select new { Alumno = g.Key.alumno, Tipo = g.Key.tipo, Sanciones = g.Sum(p => Convert.ToInt32(p.sanciones)) }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 de Alumnos a observar por Cantidad y Tipo de Sanciones:");
            foreach (var item in worstAlumnosByTipo)
            {
                TablaGrafico.Add("Alumno: " + item.Alumno + " Tipo: " + item.Tipo + " - Cantidad de Sanciones: " + item.Sanciones);
            }
        }
		#endregion
	}


}