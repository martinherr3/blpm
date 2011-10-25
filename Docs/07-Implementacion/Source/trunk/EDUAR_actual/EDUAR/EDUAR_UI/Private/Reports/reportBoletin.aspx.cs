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
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_Entities.Shared;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_UI
{
	public partial class reportBoletin : EDUARBasePage
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
                    ////////////////
                    if (HttpContext.Current.User.IsInRole(enumRoles.Tutor.ToString()))
                    {
                        // lblTutor.Text = lblTutor.Text + HttpContext.Current.User.Identity.Name;
                        lblTutor.Text = "Tutor: " + objSessionPersona.nombre + " " + objSessionPersona.apellido;

                        BLTutor objTutor = new BLTutor();

                        objTutor.GetTutores(new Tutor() { username = HttpContext.Current.User.Identity.Name });
                        listaAlumnos = objTutor.GetAlumnosDeTutor(new Tutor() { username = HttpContext.Current.User.Identity.Name });

                    }

                    if (HttpContext.Current.User.IsInRole(enumRoles.Alumno.ToString()))
                    {
                        lblTutor.Visible = false;

                        BLAlumno objAlumnos = new BLAlumno();

                        Alumno a = new Alumno();
                        
                        AlumnoCurso alu = new AlumnoCurso();
                        alu.alumno = new Alumno();
                        alu.alumno.username = HttpContext.Current.User.Identity.Name;

                        listaAlumnos = objAlumnos.GetAlumnos(alu).GetRange(0,1);

                        ddlAlumnosTutor.Enabled = false;
                    }

                    ////////////////
					TablaPropiaGrafico = null;
					CargarPresentacion();
					divReporte.Visible = false;
					btnBuscar.Visible = true;
					divAccion.Visible = true;
					divFiltros.Visible = true;
					lblAsignatura.Visible = false;
					lblAsignatura.Visible = false;

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
				else if (ddlCurso.SelectedIndex > 0 && ddlAlumno.SelectedIndex <= 0)
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
                string aux = objSessionPersona.apellido;
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
					//ddlAlumno.SelectedValue = (filtroReporte.idAlumno > 0) ? filtroReporte.idAlumno.ToString() : "-1";
					//ddlAlumno.Enabled = (filtroReporte.idCurso > 0);

					//ddlAsignatura.SelectedIndex = (filtroReporte.idAsignatura > 0) ? filtroReporte.idAsignatura.ToString() : "-1";
					ddlAsignatura.Disabled = (filtroReporte.idCurso < 0);
					lblAsignatura.Visible = false;
					lblAsignatura.Visible = false;
					ddlTipoSancion.Visible = false;
					ddlTipoSancion.Visible = false;
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
					//ddlAlumno.SelectedValue = (filtroReporteIncidencias.idAlumno > 0) ? filtroReporteIncidencias.idAlumno.ToString() : "-1";
					//ddlAlumno.Enabled = (filtroReporteIncidencias.idCurso > 0);
					lblAsignatura.Visible = false;
					ddlAsignatura.Visible = false;
					ddlTipoSancion.Visible = false;
					ddlTipoSancion.Visible = false;
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
					//ddlAlumno.SelectedValue = (filtroReporteIncidencias.idAlumno > 0) ? filtroReporteIncidencias.idAlumno.ToString() : "-1";
					//ddlAlumno.Enabled = (filtroReporteIncidencias.idCurso > 0);
					lblAsignatura.Visible = false;
					ddlAsignatura.Visible = false;
					ddlTipoSancion.Visible = true;
					ddlTipoSancion.Visible = true;
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
		/// Handles the SelectedIndexChanged event of the ddlCicloLectivo control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlAlumnosTutor_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
                miAlumno.idAlumno = Convert.ToInt32(ddlAlumnosTutor.SelectedValue);
               
                

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
					//ddlAlumno.Enabled = true;
					CargarComboAsignatura();
				}
				else
				{
					//ddlAlumno.Items.Clear();
					//ddlAlumno.Enabled = false;
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
            ddlAlumnosTutor.Items.Clear();
			CargarCombos();
			lblAsignatura.Visible = false;
            ddlAsignatura.Visible = false;
			btnBuscar.Visible = true;
			divAccion.Visible = true;
			divReporte.Visible = false;
            udpCurso.Visible = false;
            udpPeriodo.Visible = false;
            udpAlumno.Visible = false;
            udpAsignatura.Visible = false;
            lblTipoAsistencia.Visible = false;
            ddlAsistencia.Visible = false;
            lblTipoSanción.Visible = false;
            ddlTipoSancion.Visible = false;
            lblPeriodo.Visible = false;
            lblAlumno.Visible = false;
            lblCurso.Visible = false;
            lblMotivoSanción.Visible = false;
            lblTipoSanción.Visible = false;
            ddlTipoSancion.Visible = false;
            ddlMotivoSancion.Visible = false;
    

        }
		//}

		/// <summary>
		/// Buscars the promedios.
		/// </summary>
		private bool BuscarPromedios()
		{
			StringBuilder filtros = new StringBuilder();
			if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlAlumnosTutor.SelectedValue) > 0)
			{
				filtroReporte.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);


				filtroReporte.idAlumno = 0;
                if (miAlumno != null)
                {    
                    filtroReporte.idAlumno = Convert.ToInt32(ddlAlumnosTutor.SelectedValue);
                }
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
			if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlAlumnosTutor.SelectedValue) > 0)
			{
				//filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);

				filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

				filtroReporteIncidencias.idAlumno = 0;
				if (ddlAlumnosTutor.SelectedIndex > 0)
					filtroReporteIncidencias.idAlumno = Convert.ToInt32(ddlAlumnosTutor.SelectedValue);

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
			if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlAlumnosTutor.SelectedValue) > 0)
			{
                //filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);

				filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

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
			//List<Curso> listaCurso = new List<Curso>();
			//UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);

          

			ddlCicloLectivo.SelectedIndex = ddlCicloLectivo.Items.Count - 1;

            /////////////
            UIUtilidades.BindCombo<Alumno>(ddlAlumnosTutor, listaAlumnos, "idAlumno", "apellido", "nombre", true);

            if (ddlAlumnosTutor.Items.Count > 0)
            {
                ddlAlumnosTutor.SelectedIndex = 1;
            }
            /////////////
			//HabilitaCursoYPeriodo();

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
			ddlAlumno.Enabled = true;
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

        //////////////////////
        /// <summary>
        /// Cargars the combo cursos.
        /// </summary>
        /// <param name="idCicloLectivo">The id ciclo lectivo.</param>
        /// <param name="ddlCurso">The DDL curso.</param>
        private void CargarComboAlumnos(List<Alumno> listaAlumnos, DropDownList ddlAlumno)
        {
            if (listaAlumnos.Count > 0)
            {
                
                UIUtilidades.BindCombo<Alumno>(ddlAlumno, listaAlumnos, "idAlumno","apellido","nombre", true);


                ddlAlumno.Enabled = true;
            }
            else
            {
                ddlAlumno.Enabled = false;
            }
        }


        /////////////////////
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
						lblAsignatura.Visible = false;
						lblAsignatura.Visible = false;
                        lblTipoSanción.Visible = false;
						ddlTipoSancion.Visible = false;
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
                        lblTipoSanción.Visible = false;
						ddlTipoSancion.Visible = false;
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
                        lblTipoSanción.Visible = true;
						ddlTipoSancion.Visible = true;
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

			//UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", "nombre", true);
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

            if (ddlAsignatura.Items.Count == 0 || Convert.ToInt32(ddlAsignatura.SelectedIndex) < 0)
            {
                var topPromedio =
                   (from p in listaReporte
                    group p by p.asignatura into g
                    orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
                    select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)), Cantidad = g.Count() }).Distinct().Take(3);

                TablaGrafico.Add("- Top 3 Materias con mejor desempeño:");
                foreach (var item in topPromedio)
                {
                    TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString("#.00") + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
                }

                var worstPromedio =
                   (from p in listaReporte
                    group p by p.asignatura into g
                    orderby g.Average(p => Convert.ToDouble(p.promedio)) ascending
                    select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)), Cantidad = g.Count() }).Distinct().Take(3);

                TablaGrafico.Add("- Top 3 Materias con bajo desempeño:");
                foreach (var item in worstPromedio)
                {
                    TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString("#.00") + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
                }
            }

            if (ddlAlumno.Items.Count > 0 && Convert.ToInt32(ddlAlumno.SelectedValue) < 0)
            {
                var worstAlumnos =
                   (from p in listaReporte
                    group p by p.alumno into g
                    orderby g.Average(p => Convert.ToDouble(p.promedio)) ascending
                    select new { Alumno = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct().Take(3);

                TablaGrafico.Add("- Top 3 de Alumnos a observar:");
                foreach (var item in worstAlumnos)
                {
                    TablaGrafico.Add(item.Alumno + " - Promedio General: " + item.Promedio.ToString("#.00"));
                }
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

        //private void CargaInforUsuario()
        //{
        //    lblUsuario.Text = objSessionPersona.nombre + " " + objSessionPersona.apellido;
        //    lblRol.Text = ObjSessionDataUI.ObjDTUsuario.ListaRoles[0].Nombre + ": ";

        //    if (HttpContext.Current.User.IsInRole(enumRoles.Alumno.ToString()))
        //    {
        //        BLAlumno objBLAlumno = new BLAlumno(new Alumno() { username = ObjSessionDataUI.ObjDTUsuario.Nombre });
        //        AlumnoCursoCicloLectivo objCurso = objBLAlumno.GetCursoActualAlumno(cicloLectivoActual);
        //        lblCursosAsignados.Text = "Curso Actual: " + objCurso.cursoCicloLectivo.curso.nivel.nombre + "  " + objCurso.cursoCicloLectivo.curso.nombre;
        //    }
        //    if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()))
        //    {
        //        BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
        //        Asignatura objFiltro = new Asignatura();
        //        objFiltro.curso.cicloLectivo = cicloLectivoActual;
        //        //nombre del usuario logueado
        //        objFiltro.docente.username = HttpContext.Current.User.Identity.Name;
        //        List<Curso> listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
        //        string cursos = string.Empty;
        //        if (listaCursos.Count > 0) cursos = "Cursos: <br />";
        //        foreach (Curso item in listaCursos)
        //        {
        //            if (!cursos.Contains(item.nombre))
        //            {
        //                cursos += item.nombre + " <br />";
        //            }
        //        }
        //        lblCursosAsignados.Text = cursos;
        //    }
        //    if (HttpContext.Current.User.IsInRole(enumRoles.Tutor.ToString()))
        //    {
        //        List<Tutor> lista = new List<Tutor>();
        //        lista.Add(new Tutor() { username = HttpContext.Current.User.Identity.Name });
        //        BLAlumno objBLAlumno = new BLAlumno(new Alumno() { listaTutores = lista });
        //        List<AlumnoCursoCicloLectivo> listaAlumnos = objBLAlumno.GetAlumnosTutor(cicloLectivoActual);
        //        string cursos = string.Empty;
        //        if (listaAlumnos.Count > 0) cursos = "Cursos: \n";
        //        foreach (AlumnoCursoCicloLectivo item in listaAlumnos)
        //        {
        //            if (!cursos.Contains(item.cursoCicloLectivo.curso.nivel.nombre + "  " + item.cursoCicloLectivo.curso.nombre))
        //            {
        //                cursos += item.cursoCicloLectivo.curso.nivel.nombre + "  " + item.cursoCicloLectivo.curso.nombre + " \n";
        //            }
        //        }
        //        lblCursosAsignados.Text = cursos;
        //    }
        //}
		#endregion
	}
}


