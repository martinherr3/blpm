using System;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_Utility.Constantes;
using System.Collections.Generic;
using EDUAR_Entities.Reports;
using System.Data;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Reports;
using System.Text;

namespace EDUAR_UI
{
	public partial class reportRendimiento : EDUARBasePage
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
		public List<RptRendimientoHistorico> listaReporte
		{
			get
			{
				if (Session["listaReporte"] == null)
					listaReporte = new List<RptRendimientoHistorico>();
				return (List<RptRendimientoHistorico>)Session["listaReporte"];
			}
			set
			{
				Session["listaReporte"] = value;
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
				if (BuscarCalificaciones())
				{
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
				ExportPDF.ExportarPDF(Page.Title, rptCalificaciones.dtReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
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
				//int sumaNotas = 0;
				//rptCalificaciones.graficoReporte.LimpiarSeries();
				//if (ddlAsignatura.SelectedIndex > 0)
				//{
				//    var serie = new List<RptCalificacionesAlumnoPeriodo>();
				//    for (int i = 1; i < 11; i++)
				//    {
				//        sumaNotas = 0;
				//        var listaParcial = listaReporte.FindAll(p => p.calificacion == i.ToString());
				//        if (listaParcial.Count > 0)
				//        {
				//            serie.Add(new RptCalificacionesAlumnoPeriodo
				//            {
				//                calificacion = listaParcial.Count.ToString(),
				//                alumno = i.ToString()
				//            });
				//        }
				//    }
				//    if (serie != null)
				//    {
				//        DataTable dt = UIUtilidades.BuildDataTable<RptCalificacionesAlumnoPeriodo>(serie);
				//         En alumno envio la nota y en calificación la cantidad de esa nota que se produjo
				//        rptCalificaciones.graficoReporte.AgregarSerie(ddlAsignatura.SelectedItem.Text, dt, "alumno", "calificacion");
				//        rptCalificaciones.graficoReporte.Titulo = "Distribución de Calificaciones " + ddlAsignatura.SelectedItem.Text;
				//    }
				//}
				//else
				//{
				//    foreach (var item in listaAsignatura)
				//    {
				//        sumaNotas = 0;
				//        var listaParcial = listaReporte.FindAll(p => p.asignatura == item.nombre);
				//        if (listaParcial.Count > 0)
				//        {
				//            foreach (var nota in listaParcial)
				//            {
				//                sumaNotas += Convert.ToInt16(nota.calificacion);
				//            }
				//            var serie = new List<RptCalificacionesAlumnoPeriodo>();
				//            serie.Add(new RptCalificacionesAlumnoPeriodo
				//            {
				//                calificacion = (sumaNotas / listaParcial.Count).ToString(),
				//                asignatura = item.nombre
				//            });

				//            DataTable dt = UIUtilidades.BuildDataTable<RptCalificacionesAlumnoPeriodo>(serie);
				//            rptCalificaciones.graficoReporte.AgregarSerie(item.nombre, dt, "asignatura", "calificacion");
				//            rptCalificaciones.graficoReporte.Titulo = "Promedio de Calificaciones por Asignatura ";
				//        }
				//    }
				//}
				//rptCalificaciones.graficoReporte.GraficarBarra();
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

					rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporte);
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
				ddlNivel.Items.Clear();
				ddlCurso.Items.Clear();
				int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

				BLNivel objBLNivel = new BLNivel();
				List<Nivel> listaNiveles = new List<Nivel>();
				listaNiveles = objBLNivel.GetByCursoCicloLectivo(idCicloLectivo);
				UIUtilidades.BindCombo<Nivel>(ddlNivel, listaNiveles, "idNivel", "nombre", true);
				CargarComboCursos(idCicloLectivo, ddlCurso);
				if (Convert.ToInt16(ddlCicloLectivo.SelectedValue) > 0)
				{
					ddlNivel.Enabled = true;
					ddlCurso.Enabled = true;
				}
				else
				{
					ddlNivel.Items.Clear();
					ddlNivel.Enabled = false;
					ddlCurso.Items.Clear();
					ddlCurso.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the ddlNivel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
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
			if (Page.IsPostBack)
			{
				filtroReporte = new FilCalificacionesAlumnoPeriodo();
				StringBuilder filtros = new StringBuilder();
				//if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0 && Convert.ToInt32(ddlCurso.SelectedValue) > 0 /*&& Convert.ToInt32(ddlAsignatura.SelectedValue) > 0*/)
				//{
				//filtros.AppendLine("- " + ddlCicloLectivo.SelectedItem.Text + " - Curso: " + ddlCurso.SelectedItem.Text);
				if (ddlAsignatura.SelectedIndex > 0)
				{
					filtroReporte.idAsignatura = Convert.ToInt32(ddlAsignatura.SelectedValue);
					if (filtroReporte.idAsignatura > 0) filtros.AppendLine("- Asignatura: " + ddlAsignatura.SelectedItem.Text);
				}
				//if (fechas.ValorFechaDesde != null)
				//{
				//    filtros.AppendLine("- Fecha Desde: " + ((DateTime)fechas.ValorFechaDesde).ToShortDateString());
				//    filtroReporte.fechaDesde = (DateTime)fechas.ValorFechaDesde;
				//}
				//if (fechas.ValorFechaHasta != null)
				//{
				//    filtros.AppendLine("- Fecha Hasta: " + ((DateTime)fechas.ValorFechaHasta).ToShortDateString());
				//    filtroReporte.fechaHasta = (DateTime)fechas.ValorFechaHasta;
				//}
				if (ddlCurso.Items.Count > 0 && Convert.ToInt32(ddlCurso.SelectedValue) > 0)
					filtroReporte.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);

				if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0)
					filtroReporte.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

				if (ddlNivel.Items.Count > 0 && Convert.ToInt32(ddlNivel.SelectedValue) > 0)
					filtroReporte.idNivel = Convert.ToInt32(ddlNivel.SelectedValue);

				if (Convert.ToInt32(ddlAlumno.SelectedValue) > 0)
					filtroReporte.idAlumno = Convert.ToInt32(ddlAlumno.SelectedValue);

				if (Context.User.IsInRole(enumRoles.Docente.ToString()))
					filtroReporte.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

				BLRptCalificacionesAlumnoPeriodo objBLReporte = new BLRptCalificacionesAlumnoPeriodo();
				listaReporte = objBLReporte.GetRptRendimientoHistorico(filtroReporte);
				filtrosAplicados = filtros.ToString();

				rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporte);
				return true;
				//}
				//return false;
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
			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);

			CargarAlumnos();
			CargarComboAsignatura();

			//ddlAsignatura.Enabled = false;
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
			//Asignatura materia = new Asignatura();
			//materia.curso.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);
			//if (User.IsInRole(enumRoles.Docente.ToString()))
			//materia.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
			listaAsignatura = objBLAsignatura.GetAsignaturas(null);
			UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, listaAsignatura, "idAsignatura", "nombre", true);
			ddlAsignatura.Enabled = false;
			if (ddlAsignatura.Items.Count > 0)
				ddlAsignatura.Enabled = true;
		}

		/// <summary>
		/// Cargars the alumnos.
		/// </summary>
		/// <param name="idCurso">The id curso.</param>
		private void CargarAlumnos()
		{
			BLAlumno objBLAlumno = new BLAlumno();
			UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(null), "idAlumno", "apellido", "nombre", true);
			ddlAlumno.Enabled = true;
		}
		#endregion

	}
}