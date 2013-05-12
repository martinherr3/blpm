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
		/// Gets or sets the filtros aplica2.
		/// </summary>
		/// <value>
		/// The filtros aplica2.
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
                //rptCalificaciones.PaginarGrilla += (PaginarGrilla);
				Master.BotonAvisoAceptar += (VentanaAceptar);
				rptCalificaciones.GraficarClick += (btnGraficar);

				if (!Page.IsPostBack)
				{
					TablaGrafico = null;
					CargarPresentacion();
					divFiltros.Visible = true;
					divReporte.Visible = false;
				}
				if (listaReporte != null)
					rptCalificaciones.CargarReporte<RptCalificacionesAlumnoPeriodo>(listaReporte);

				ddlCicloLectivo.Attributes.Add("onchange", "onChangeCicloLectivo('" + ddlCicloLectivo.ClientID + "','" + ddlCurso.ClientID + "')");
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
				AccionPagina = enumAcciones.Limpiar;
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
				fechas.ValidarRangoDesdeHasta(false);
				string mensaje = ValidarPagina();
				if (mensaje == string.Empty)
				{
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
				//GenerarDatosGrafico();
				AccionPagina = enumAcciones.Limpiar;
				float sumaNotas = 0;
				rptCalificaciones.graficoReporte.LimpiarSeries();
				string alumno = string.Empty;
				if (Convert.ToInt32(ddlAlumno.SelectedValue) > 0)
					alumno = "\n" + ddlAlumno.SelectedItem.Text + "\n";

				if (ddlAsignatura.SelectedIndex > 0)
				{ // so reporte   distribucion de calificaciones por asignatura 
					foreach (System.Web.UI.WebControls.ListItem asignatura in ddlAsignatura.Items)
					{
						if (asignatura.Selected)
						{
							var serie = new List<RptCalificacionesAlumnoPeriodo>();
							for (int i = 1; i < 11; i++)
							{
								var listaParcial = listaReporte.FindAll(p => p.calificacion == i.ToString() && p.asignatura == asignatura.Text);
								//if (listaParcial.Count > 0)
								//{
									serie.Add(new RptCalificacionesAlumnoPeriodo
									{
										calificacion = (listaParcial.Count > 0) ? listaParcial.Count.ToString() : string.Empty,
										asignatura = i.ToString()
									});
								//}

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
				{ // promedio de calificaciones por curso en un determinado periodo
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
					rptCalificaciones.graficoReporte.AgregarSerie("Promedio", dt, "asignatura", "calificacion");

					BLValoresEscalaCalificacion objBLNivelAprobacion = new BLValoresEscalaCalificacion();
					ValoresEscalaCalificacion escala = new ValoresEscalaCalificacion();
					escala = objBLNivelAprobacion.GetNivelProbacion();

					List<ValoresEscalaCalificacion> listaEscala = new List<ValoresEscalaCalificacion>();
					foreach (RptCalificacionesAlumnoPeriodo item in serie)
					{
						listaEscala.Add(new ValoresEscalaCalificacion
						{
							valor = escala.valor,
							nombre = item.asignatura
						});
					}
					DataTable dtEscala = UIUtilidades.BuildDataTable<ValoresEscalaCalificacion>(listaEscala);
					rptCalificaciones.graficoReporte.AgregarSerie("Nivel de Aprobación (" + escala.nombre + ")", dtEscala, "nombre", "valor", true);

					rptCalificaciones.graficoReporte.Titulo = "Promedio Por Asignatura \n" + ddlCurso.SelectedItem.Text + " - " + ddlCicloLectivo.SelectedItem.Text + alumno;
				}

				GenerarDatosGrafico();
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
				if (idCicloLectivo > 0)
				{
					fechas.startDate = listaCicloLectivo.Find(p => p.idCicloLectivo == idCicloLectivo).fechaInicio;
					fechas.endDate = listaCicloLectivo.Find(p => p.idCicloLectivo == idCicloLectivo).fechaFin;
					CargarComboCursos(idCicloLectivo, ddlCurso);
				}
				else
				{
					if (ddlCurso.Items.Count > 0)
					{
						ddlCurso.Items.Clear();
						ddlCurso.Enabled = false;
					}
				}
				ddlAsignatura.Items.Clear();
				ddlAlumno.Items.Clear();
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
				ddlAsignatura.Items.Clear();
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
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			string mensaje = string.Empty;

			if (string.IsNullOrEmpty(ddlCicloLectivo.SelectedValue) || Convert.ToInt32(ddlCicloLectivo.SelectedValue) <= 0)
				mensaje = "- Ciclo Lectivo<br />";
			if (string.IsNullOrEmpty(ddlCurso.SelectedValue) || Convert.ToInt32(ddlCurso.SelectedValue) <= 0)
				mensaje += "- Curso<br />";
			return mensaje;
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
			ddlAsignatura.Items.Clear();

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
			TablaGrafico = new List<TablaGrafico>();
			TablaGrafico tabla3 = new TablaGrafico();
			tabla3.listaCuerpo = new List<List<string>>();
			List<string> encabezado3 = new List<string>();
			List<string> fila3 = new List<string>();

			var fechaMin =
			   from p in listaReporte
			   group p by p.alumno into g
			   select new { Alumno = g.Key, Fecha = g.Min(p => p.fecha) };

			var fechaMax =
			   from p in listaReporte
			   group p by p.alumno into g
			   select new { Alumno = g.Key, Fecha = g.Max(p => p.fecha) };
			//TablaGrafico.Add("- Periodo de notas: " + fechaMin.First().Fecha.ToShortDateString() + " - " + fechaMax.First().Fecha.ToShortDateString());

			tabla3.titulo = "Periodo Analizado " + fechaMin.First().Fecha.ToShortDateString() + " - " + fechaMax.First().Fecha.ToShortDateString();

			var cantAlumnos =
				from p in listaReporte
				group p by p.alumno into g
				select new { Alumno = g.Key, Cantidad = g.Count() };

			//TablaGrafico.Add("- Cantidad de Alumnos analiza2: " + cantAlumnos.Count().ToString());
			encabezado3.Add("Cantidad de Alumnos");
			fila3.Add(cantAlumnos.Count().ToString());

			//TablaGrafico.Add("- Cantidad de Calificaciones: " + listaReporte.Count.ToString());
			encabezado3.Add("Cantidad de Calificaciones");
			fila3.Add(listaReporte.Count().ToString());

			tabla3.listaEncabezados = encabezado3;
			tabla3.listaCuerpo.Add(fila3);
			TablaGrafico.Add(tabla3);

			TablaGrafico tabla2 = new TablaGrafico();
			tabla2.listaCuerpo = new List<List<string>>();
			List<string> encabezado2 = new List<string>();
			List<List<string>> filasTabla2 = new List<List<string>>();
			List<string> fila2 = new List<string>();

			//TablaGrafico.Add("- Desviacion Estandar por materia: ");
			tabla2.titulo = "Desviación Estandar por Asignatura";
			encabezado2.Add("Asignatura");
			encabezado2.Add("Promedio");
			encabezado2.Add("Desviación Estándar");

			double sumaNotas, promedio, desvStd, dif, cociente, sumaDifCuad = 0;

			var serie = new List<RptCalificacionesAlumnoPeriodo>();
			foreach (var item in listaAsignatura)
			{
				promedio = 0;
				cociente = 0;
				desvStd = 0;
				sumaNotas = 0;
				dif = 0;
				sumaDifCuad = 0;

				var listaParcial = listaReporte.FindAll(p => p.asignatura == item.nombre);
				if (listaParcial.Count > 0)
				{
					foreach (var nota in listaParcial)
					{
						sumaNotas += Convert.ToInt16(nota.calificacion);
					}
					promedio = sumaNotas / listaParcial.Count;
					foreach (var nota in listaParcial)
					{
						dif = (Convert.ToInt32(nota.calificacion) - promedio);
						sumaDifCuad += Math.Pow(dif, 2);
					}
					// Revisar la formula de desviacion standard
					//cociente = (sumaDifCuad / (listaParcial.Count-1));
					cociente = (sumaDifCuad / (listaParcial.Count));
					desvStd = Math.Sqrt(cociente);
					//TablaGrafico.Add(item.nombre + " Promedio: " + promedio.ToString("#.##") + " , Desviacion Standard: " + desvStd.ToString("#.##"));
					fila2 = new List<string>();
					fila2.Add(item.nombre);
					fila2.Add(promedio.ToString("#.##"));
					fila2.Add(desvStd.ToString("#.##"));
					filasTabla2.Add(fila2);
				}
			}
			tabla2.listaEncabezados = encabezado2;
			tabla2.listaCuerpo = filasTabla2;
			TablaGrafico.Add(tabla2);

			TablaGrafico tabla4 = new TablaGrafico();
			tabla4.listaCuerpo = new List<List<string>>();
			List<string> encabezado4 = new List<string>();
			List<List<string>> filasTabla4 = new List<List<string>>();
			List<string> fila4 = new List<string>();

			if (Convert.ToInt32(ddlAsignatura.SelectedIndex) < 0)
			{
				var topPromedio =
				   (from p in listaReporte
					group p by p.asignatura into g
					orderby g.Average(p => Convert.ToInt32(p.calificacion)) descending
					select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.calificacion)), Cantidad = g.Count() }).Distinct().Take(3);

				if (topPromedio.Count() > 1)
				{
					//TablaGrafico.Add("- Top 3 Materias con mejor desempeño:");
					tabla4.titulo = "Top Asignaturas con mejor desempeño";
					encabezado4.Add("Asignatura");
					encabezado4.Add("Promedio");
					encabezado4.Add("Cantidad de Evaluaciones");

					tabla4.listaEncabezados = encabezado4;

					foreach (var item in topPromedio)
					{
						//TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString("#.##") + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
						fila4 = new List<string>();
						fila4.Add(item.Asignatura);
						fila4.Add(item.Promedio.ToString("#.##"));
						fila4.Add(item.Cantidad.ToString());
						filasTabla4.Add(fila4);
					}
					tabla4.listaEncabezados = encabezado4;
					tabla4.listaCuerpo = filasTabla4;
					TablaGrafico.Add(tabla4);
				}

				var worstPromedio =
				   (from p in listaReporte
					group p by p.asignatura into g
					orderby g.Average(p => Convert.ToInt32(p.calificacion)) ascending
					select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.calificacion)), Cantidad = g.Count() }).Distinct().Take(3);

				if (worstPromedio.Count() > 1)
				{
					TablaGrafico tabla5 = new TablaGrafico();
					tabla5.listaCuerpo = new List<List<string>>();
					List<string> encabezado5 = new List<string>();
					List<List<string>> filasTabla5 = new List<List<string>>();
					List<string> fila5 = new List<string>();

					tabla5.titulo = "Top Asignaturas con bajo desempeño";
					encabezado5.Add("Asignatura");
					encabezado5.Add("Promedio");
					encabezado5.Add("Cantidad de Evaluaciones");
					//TablaGrafico.Add("- Top 3 Materias con bajo desempeño:");
					foreach (var item in worstPromedio)
					{
						//TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString("#.##") + " - Cantidad de Evaluaciones: " + item.Cantidad.ToString());
						fila5 = new List<string>();
						fila5.Add(item.Asignatura);
						fila5.Add(item.Promedio.ToString("#.##"));
						fila5.Add(item.Cantidad.ToString());
						filasTabla5.Add(fila5);
					}
					tabla5.listaEncabezados = encabezado5;
					tabla5.listaCuerpo = filasTabla5;
					TablaGrafico.Add(tabla5);
				}
			}

			if (Convert.ToInt32(ddlAlumno.SelectedValue) < 0)
			{
				var worstAlumnos =
				   (from p in listaReporte
					group p by p.alumno into g
					orderby g.Average(p => Convert.ToInt32(p.calificacion)) ascending
					select new { Alumno = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.calificacion)) }).Distinct().Take(3);

				if (worstAlumnos.Count() > 1)
				{
					TablaGrafico tabla6 = new TablaGrafico();
					tabla6.listaCuerpo = new List<List<string>>();
					List<string> encabezado6 = new List<string>();
					List<List<string>> filasTabla6 = new List<List<string>>();
					List<string> fila6 = new List<string>();

					tabla6.titulo = "Top Alumnos a observar";
					encabezado6.Add("Alumno");
					encabezado6.Add("Promedio General");
					//TablaGrafico.Add("- Top 3 de Alumnos a observar:");
					foreach (var item in worstAlumnos)
					{
						//TablaGrafico.Add(item.Alumno + " - Promedio General: " + item.Promedio.ToString("#.##"));
						fila6 = new List<string>();
						fila6.Add(item.Alumno);
						fila6.Add(item.Promedio.ToString("#.##"));
						filasTabla6.Add(fila6);
					}
					tabla6.listaEncabezados = encabezado6;
					tabla6.listaCuerpo = filasTabla6;
					TablaGrafico.Add(tabla6);
				}
			}

			if (Convert.ToInt32(ddlAlumno.SelectedValue) < 0)
			{
				var worstAlumnos =
				   (from p in listaReporte
					group p by p.alumno into g
					orderby g.Average(p => Convert.ToInt32(p.calificacion)) descending
					select new { Alumno = g.Key, Promedio = g.Average(p => Convert.ToInt32(p.calificacion)) }).Distinct().Take(3);

				if (worstAlumnos.Count() > 1)
				{
					TablaGrafico tabla7 = new TablaGrafico();
					tabla7.listaCuerpo = new List<List<string>>();
					List<string> encabezado7 = new List<string>();
					List<List<string>> filasTabla7 = new List<List<string>>();
					List<string> fila7 = new List<string>();

					tabla7.titulo = "Top Alumnos con mejores Promedios";
					encabezado7.Add("Alumno");
					encabezado7.Add("Promedio General");

					//TablaGrafico.Add("- Top 3 de Alumnos con mejores notas:");
					foreach (var item in worstAlumnos)
					{
						//TablaGrafico.Add(item.Alumno + " - Promedio General: " + item.Promedio.ToString("#.##"));
						fila7 = new List<string>();
						fila7.Add(item.Alumno);
						fila7.Add(item.Promedio.ToString("#.##"));
						filasTabla7.Add(fila7);
					}
					tabla7.listaEncabezados = encabezado7;
					tabla7.listaCuerpo = filasTabla7;
					TablaGrafico.Add(tabla7);
				}
			}
		}
		#endregion
	}
}