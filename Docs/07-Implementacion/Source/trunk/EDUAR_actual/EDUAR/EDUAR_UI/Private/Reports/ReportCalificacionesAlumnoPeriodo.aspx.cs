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
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using iTextSharp.text;

namespace EDUAR_UI
{
	public partial class ReportCalificacionesAlumnoPeriodo : EDUARBasePage
	{
		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the filtro calificaciones.
		/// </summary>
		/// <value>
		/// The filtro calificaciones.
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
		/// Gets or sets the lista calificaciones.
		/// </summary>
		/// <value>
		/// The lista calificaciones.
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
				rptCalificaciones.ExportarPDFClick += (ExportarPDF);
				rptCalificaciones.VolverClick += (VolverReporte);
				rptCalificaciones.PaginarGrilla += (PaginarGrilla);
				Master.BotonAvisoAceptar += (VentanaAceptar);
				rptCalificaciones.GraficarClick += (btnGraficar);

				if (!Page.IsPostBack)
				{
					CargarPresentacion();
					divFiltros.Visible = true;
					divReporte.Visible = false;
				}
				if (listaReporte != null)
					rptCalificaciones.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);
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
			AccionPagina = enumAcciones.Limpiar;
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
				fechas.ValidarRangoDesdeHasta(false);
				AccionPagina = enumAcciones.Buscar;
				if (BuscarCalificaciones())
				{
					AccionPagina = enumAcciones.Limpiar;
					divFiltros.Visible = false;
					divReporte.Visible = true;
				}
				else
				{ Master.MostrarMensaje("Faltan Datos", UIConstantesGenerales.MensajeDatosRequeridos, EDUAR_Utility.Enumeraciones.enumTipoVentanaInformacion.Advertencia); }
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
				AccionPagina = enumAcciones.Limpiar;
				string nombreGrafico = string.Empty;
				if (rptCalificaciones.verGrafico)
					nombreGrafico = nombrePNG;
				ExportPDF.ExportarPDF(Page.Title, rptCalificaciones.dtReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados, nombreGrafico);
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
				AccionPagina = enumAcciones.Limpiar;
				rptCalificaciones.verGrafico = false;
				divFiltros.Visible = true;
				divReporte.Visible = false;
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// BTNs the graficar.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnGraficar(object sender, EventArgs e)
		{
			try
			{
				GenerarDatosGrafico();
				AccionPagina = enumAcciones.Limpiar;
				float sumaNotas = 0;
				rptCalificaciones.graficoReporte.LimpiarSeries();
				string alumno = string.Empty;
				if (Convert.ToInt32(ddlAlumno.SelectedValue) > 0)
					alumno = "\n" + ddlAlumno.SelectedItem.Text + "\n";

				if (ddlAsignatura.SelectedIndex > 0)
				{
					foreach (System.Web.UI.WebControls.ListItem asignatura in ddlAsignatura.Items)
					{
						if (asignatura.Selected)
						{
							var serie = new List<RptCalificacionesAlumnoPeriodo>();
							for (int i = 1; i < 11; i++)
							{
								var listaParcial = listaReporte.FindAll(p => p.calificacion == i.ToString() && p.asignatura == asignatura.Text);
								if (listaParcial.Count > 0)
								{
									serie.Add(new RptCalificacionesAlumnoPeriodo
									{
										calificacion = listaParcial.Count.ToString(),
										asignatura = i.ToString()
									});
								}

							}
							if (serie != null && serie.Count > 0)
							{
								DataTable dt = UIUtilidades.BuildDataTable<RptCalificacionesAlumnoPeriodo>(serie);
								// En alumno envio la nota y en calificación la cantidad de esa nota que se produjo
								rptCalificaciones.graficoReporte.AgregarSerie(asignatura.Text, dt, "asignatura", "calificacion");
							}
						}
					}
					rptCalificaciones.graficoReporte.Titulo = "Distribución de Calificaciones \n" + alumno;
				}
				else
				{
					var serie = new List<RptCalificacionesAlumnoPeriodo>();
					foreach (var item in listaAsignatura)
					{
						sumaNotas = 0;
						var listaParcial = listaReporte.FindAll(p => p.asignatura == item.nombre);
						if (listaParcial.Count > 0)
						{
							foreach (var nota in listaParcial)
							{
								sumaNotas += Convert.ToInt16(nota.calificacion);
							}
							serie.Add(new RptCalificacionesAlumnoPeriodo
							{
								calificacion = Math.Round(sumaNotas / listaParcial.Count, 2).ToString(CultureInfo.InvariantCulture),
								asignatura = item.nombre
							});
						}
					}
					DataTable dt = UIUtilidades.BuildDataTable<RptCalificacionesAlumnoPeriodo>(serie);
					rptCalificaciones.graficoReporte.AgregarSerie("Promedios", dt, "asignatura", "calificacion");
					rptCalificaciones.graficoReporte.Titulo = "Promedio Por Asignatura \n" + ddlCurso.SelectedItem.Text + " - " + ddlCicloLectivo.SelectedItem.Text + alumno;
				}
				rptCalificaciones.graficoReporte.GraficarBarra();
				rptCalificaciones.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);
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

				if (rptCalificaciones.GrillaReporte.PageCount > pagina)
				{
					rptCalificaciones.GrillaReporte.PageIndex = pagina;

					rptCalificaciones.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);
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
				AccionPagina = enumAcciones.Limpiar;
				int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
				fechas.startDate = listaCicloLectivo.Find(p => p.idCicloLectivo == idCicloLectivo).fechaInicio;
				fechas.endDate = listaCicloLectivo.Find(p => p.idCicloLectivo == idCicloLectivo).fechaFin;
				CargarComboCursos(idCicloLectivo, ddlCurso);
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
				AccionPagina = enumAcciones.Limpiar;
				CargarAlumnos(Convert.ToInt32(ddlCurso.SelectedValue));
				ddlAlumno.Enabled = true;
				CargarComboAsignatura();
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
			CargarCombos();
			btnBuscar.Visible = true;
		}

		/// <summary>
		/// Buscars the calificaciones.
		/// </summary>
		private bool BuscarCalificaciones()
		{
			if (AccionPagina == enumAcciones.Buscar)
			{
				filtroReporte = new FilCalificacionesAlumnoPeriodo();
				StringBuilder filtros = new StringBuilder();
				if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlCurso.SelectedValue) > 0 /*&& Convert.ToInt32(ddlAsignatura.SelectedValue) > 0*/)
				{
					filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);

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

					if (fechas.ValorFechaDesde != null)
					{
						filtros.AppendLine("- Fecha Desde: " + ((DateTime)fechas.ValorFechaDesde).ToShortDateString());
						filtroReporte.fechaDesde = (DateTime)fechas.ValorFechaDesde;
					}
					if (fechas.ValorFechaHasta != null)
					{
						filtros.AppendLine("- Fecha Hasta: " + ((DateTime)fechas.ValorFechaHasta).ToShortDateString());
						filtroReporte.fechaHasta = (DateTime)fechas.ValorFechaHasta;
					}
					filtroReporte.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);
					filtroReporte.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

					if (Convert.ToInt32(ddlAlumno.SelectedValue) > 0)
					{
						filtroReporte.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);
						filtros.AppendLine("- Alumno: " + ddlAlumno.SelectedItem.Text);
					}

					if (Context.User.IsInRole(enumRoles.Docente.ToString()))
						filtroReporte.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

					BLRptCalificacionesAlumnoPeriodo objBLReporte = new BLRptCalificacionesAlumnoPeriodo();
					listaReporte = objBLReporte.GetRptCalificacionesAlumnoPeriodo(filtroReporte);
					filtrosAplicados = filtros.ToString();

					rptCalificaciones.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);
					//udpReporte.Update();
					return true;
				}
				return false;
			}
			else
				return false;
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		private void CargarCombos()
		{
			List<Curso> listaCurso = new List<Curso>();
			List<Alumno> listaAlumno = new List<Alumno>();
			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "nombre", true);

			if (ddlCicloLectivo.Items.Count > 0)
			{
				ddlCicloLectivo.SelectedIndex = ddlCicloLectivo.Items.Count - 1;
				fechas.startDate = listaCicloLectivo.Find(p => p.idCicloLectivo == Convert.ToInt16(ddlCicloLectivo.SelectedValue)).fechaInicio;
				fechas.endDate = listaCicloLectivo.Find(p => p.idCicloLectivo == Convert.ToInt16(ddlCicloLectivo.SelectedValue)).fechaFin;
				CargarComboCursos(Convert.ToInt16(ddlCicloLectivo.SelectedValue), ddlCurso);
				ddlCurso.Enabled = true;
				ddlCurso.SelectedIndex = -1;
			}

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
		/// Cargars the combo asignatura.
		/// </summary>
		private void CargarComboAsignatura()
		{
			BLAsignatura objBLAsignatura = new BLAsignatura();
			Asignatura materia = new Asignatura();
			materia.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(ddlCurso.SelectedValue);
			if (User.IsInRole(enumRoles.Docente.ToString()))
				materia.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
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
		/// Cargars the alumnos.
		/// </summary>
		/// <param name="idCurso">The id curso.</param>
		private void CargarAlumnos(int idCurso)
		{
			BLAlumno objBLAlumno = new BLAlumno();
			UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", "nombre", true);
			ddlAlumno.Enabled = true;
		}

		/// <summary>
		/// Generars the datos grafico.
		/// </summary>
		private void GenerarDatosGrafico()
		{
			var cantAlumnos =
				from p in listaReporte
				group p by p.alumno into g
				select new { Alumno = g.Key, Cantidad = g.Count() };

			TablaGrafico.Add("- Cantidad de Alumnos analizados: " + cantAlumnos.Count().ToString());

			TablaGrafico.Add("- Cantidad de Calificaciones: " + listaReporte.Count.ToString());

			var fechaMin =
			   from p in listaReporte
			   group p by p.alumno into g
			   select new { Alumno = g.Key, Fecha = g.Min(p => p.fecha) };

			var fechaMax =
			   from p in listaReporte
			   group p by p.alumno into g
			   select new { Alumno = g.Key, Fecha = g.Max(p => p.fecha) };

			TablaGrafico.Add("- Periodo de notas: " + fechaMin.First().Fecha.ToShortDateString() + " - " + fechaMax.First().Fecha.ToShortDateString());

			if (Convert.ToInt32(ddlAsignatura.SelectedIndex) < 0)
			{
				var topPromedio =
				   (from p in listaReporte
					group p by p.asignatura into g
					orderby g.Average(p => Convert.ToInt32(p.calificacion)) descending
					select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.calificacion)), Cantidad = g.Count() }).Distinct().Take(3);

				TablaGrafico.Add("- Top 3 Materias con mejor desempeño:");
				foreach (var item in topPromedio)
				{
					TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString("#.##") + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
				}

				var worstPromedio =
				   (from p in listaReporte
					group p by p.asignatura into g
					orderby g.Average(p => Convert.ToInt32(p.calificacion)) ascending
					select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.calificacion)), Cantidad = g.Count() }).Distinct().Take(3);

				TablaGrafico.Add("- Top 3 Materias con bajo desempeño:");
				foreach (var item in worstPromedio)
				{
					TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString("#.##") + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
				}
			}

			if (Convert.ToInt32(ddlAlumno.SelectedValue) < 0)
			{
				var worstAlumnos =
				   (from p in listaReporte
					group p by p.alumno into g
					orderby g.Average(p => Convert.ToInt32(p.calificacion)) ascending
					select new { Alumno = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.calificacion)) }).Distinct().Take(3);

				TablaGrafico.Add("- Top 3 de Alumnos a observar:");
				foreach (var item in worstAlumnos)
				{
					TablaGrafico.Add(item.Alumno + " - Promedio General: " + item.Promedio.ToString("#.##"));
				}
			}
		}
		#endregion
	}
}