using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_Entities.Reports;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Utility.Constantes;
using EDUAR_UI.Utilidades;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using System.Text;

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
                //divFiltros.Visible = false;
                //divFiltrosIncidencia.Visible = false;

                if (rdlAccion.SelectedValue == "0") 
                {
                    rptPromedios.ExportarPDFClick += (ExportarPDF);
                    rptPromedios.VolverClick += (VolverReporte);
                    rptPromedios.PaginarGrilla += (PaginarGrilla);

                    Master.BotonAvisoAceptar += (VentanaAceptar);

                    if (!Page.IsPostBack)
                    {
                        CargarPresentacion();
                        BLRptPromedioCalificacionesPeriodo objBLRptPromedios = new BLRptPromedioCalificacionesPeriodo();
                        objBLRptPromedios.GetRptPromedioCalificaciones(null);
                        divFiltros.Visible = true;
                        divFiltrosIncidencia.Visible = false;
                        divPromedioPeriodo.Visible = false;
                        divInasistenciasPeriodo.Visible = false;
                        divSancionesPeriodo.Visible = false;
                    }
                }
                if (rdlAccion.SelectedValue == "1")
                {
                    rptInasistencias.ExportarPDFClick += (ExportarPDF);
                    rptInasistencias.VolverClick += (VolverReporte);
                    rptInasistencias.PaginarGrilla += (PaginarGrilla);

                    Master.BotonAvisoAceptar += (VentanaAceptar);

                    if (!Page.IsPostBack)
                    {
                        CargarPresentacionIncidencias();
                        BLRptConsolidadoInasistenciasPeriodo objBLRptInasistencias = new BLRptConsolidadoInasistenciasPeriodo();
                        objBLRptInasistencias.GetRptConsolidadoInasistencias(null);
                        divFiltros.Visible = false;
                        divFiltrosIncidencia.Visible = true;
                        divPromedioPeriodo.Visible = false;
                        divInasistenciasPeriodo.Visible = false;
                        divSancionesPeriodo.Visible = false;
                    }
                }
                if (rdlAccion.SelectedValue == "2")
                {
                    rptSanciones.ExportarPDFClick += (ExportarPDF);
                    rptSanciones.VolverClick += (VolverReporte);
                    rptSanciones.PaginarGrilla += (PaginarGrilla);

                    Master.BotonAvisoAceptar += (VentanaAceptar);

                    if (!Page.IsPostBack)
                    {
                        CargarPresentacionIncidencias();
                        BLRptConsolidadoSancionesPeriodo objBLRptSanciones = new BLRptConsolidadoSancionesPeriodo();
                        objBLRptSanciones.GetRptConsolidadoSanciones(null);
                        divFiltros.Visible = false;
                        divFiltrosIncidencia.Visible = true;
                        divPromedioPeriodo.Visible = false;
                        divInasistenciasPeriodo.Visible = false;
                        divSancionesPeriodo.Visible = false;
                    }
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
                if (rdlAccion.SelectedValue == "0"){
                    if (BuscarPromedios())
                    {
                        divFiltros.Visible = false;
                        divFiltrosIncidencia.Visible = false;
                        divPromedioPeriodo.Visible = true;
                        divInasistenciasPeriodo.Visible = false;
                        divSancionesPeriodo.Visible = false;
                    }
				}
                if (rdlAccion.SelectedValue == "1")
                {
                    if (BuscarInasistencias())
                    {
                        divFiltros.Visible = false;
                        divFiltrosIncidencia.Visible = false;
                        divPromedioPeriodo.Visible = false;
                        divInasistenciasPeriodo.Visible = true;
                        divSancionesPeriodo.Visible = false;
                    }
                }
                if (rdlAccion.SelectedValue == "2")
                {
                    if (BuscarSanciones())
                    {
                        divFiltros.Visible = false;
                        divFiltrosIncidencia.Visible = false;
                        divPromedioPeriodo.Visible = false;
                        divInasistenciasPeriodo.Visible = false;
                        divSancionesPeriodo.Visible = true;
                    }
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
                if (rdlAccion.SelectedValue == "0") ExportPDF.ExportarPDF(Page.Title, rptPromedios.dtReporte, ObjDTSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
                if (rdlAccion.SelectedValue == "1") ExportPDF.ExportarPDF(Page.Title, rptInasistencias.dtReporte, ObjDTSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
                if (rdlAccion.SelectedValue == "2") ExportPDF.ExportarPDF(Page.Title, rptSanciones.dtReporte, ObjDTSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
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
                divAccion.Visible = true;
                divFiltros.Visible = false;
                divFiltrosIncidencia.Visible = false;
				divPromedioPeriodo.Visible = false;
                divInasistenciasPeriodo.Visible = false;
                divSancionesPeriodo.Visible = false;
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

				if (rptPromedios.GrillaReporte.PageCount > pagina && rdlAccion.SelectedValue == "0")
				{
					rptPromedios.GrillaReporte.PageIndex = pagina;
					rptPromedios.CargarReporte<RptPromedioCalificacionesPeriodo>(listaReporte);
				}

                if (rptPromedios.GrillaReporte.PageCount > pagina && rdlAccion.SelectedValue == "1")
                {
                    rptInasistencias.GrillaReporte.PageIndex = pagina;
                    rptInasistencias.CargarReporte<RptConsolidadoInasistenciasPeriodo>(listaReporteInasistencias);
                }

                if (rptPromedios.GrillaReporte.PageCount > pagina && rdlAccion.SelectedValue == "2")
                {
                    rptSanciones.GrillaReporte.PageIndex = pagina;
                    rptSanciones.CargarReporte<RptConsolidadoSancionesPeriodo>(listaReporteSanciones);
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
                    idCicloLectivo = Convert.ToInt32(ddlCicloLectivo2.SelectedValue);
                    CargarComboCursos(idCicloLectivo, ddlCurso2);
                    CargarComboPeriodos(idCicloLectivo, ddlPeriodo2);
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
                    CargarAlumnos(Convert.ToInt32(ddlCurso2.SelectedValue));
                    ddlAlumno2.Enabled = true;
                }
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

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
                UIUtilidades.BindCombo<Alumno>(ddlAlumno2, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", "nombre", true);
                ddlAlumno2.Enabled = true;
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

        private void CargarPresentacionIncidencias()
        {
            CargarCombosIncidencias();
            btnBuscarIncidencia.Visible = true;
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

				rptPromedios.CargarReporte<RptPromedioCalificacionesPeriodo>(listaReporte);

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
            if (Convert.ToInt32(ddlCicloLectivo2.SelectedValue) > 0 && Convert.ToInt32(ddlCurso2.SelectedValue) > 0)
            {
                filtros.AppendLine("- " + ddlCicloLectivo2.SelectedItem.Text + " - Curso: " + ddlCurso2.SelectedItem.Text);

                filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo2.SelectedValue);

                filtroReporteIncidencias.idCurso = 0;
                if (ddlCurso2.SelectedIndex > 0)
                    filtroReporteIncidencias.idCurso = Convert.ToInt32(ddlCurso2.SelectedValue);

                filtroReporteIncidencias.idPeriodo = Convert.ToInt32(ddlPeriodo2.SelectedValue);

                filtroReporteIncidencias.idAlumno = 0;
                if (ddlAlumno2.SelectedIndex > 0)
                    filtroReporteIncidencias.idAlumno = Convert.ToInt32(ddlAlumno2.SelectedValue);

                BLRptConsolidadoInasistenciasPeriodo objBLReporte = new BLRptConsolidadoInasistenciasPeriodo();
                listaReporteInasistencias = objBLReporte.GetRptConsolidadoInasistencias(filtroReporteIncidencias);

                listaReporteInasistencias.Sort((p, q) => String.Compare(p.alumno, q.alumno));

                filtrosAplicados = filtros.ToString();

                rptInasistencias.CargarReporte<RptConsolidadoInasistenciasPeriodo>(listaReporteInasistencias);

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
            if (Convert.ToInt32(ddlCicloLectivo2.SelectedValue) > 0 && Convert.ToInt32(ddlCurso2.SelectedValue) > 0)
            {
                filtros.AppendLine("- " + ddlCicloLectivo2.SelectedItem.Text + " - Curso: " + ddlCurso2.SelectedItem.Text);

                filtroReporteIncidencias.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo2.SelectedValue);

                filtroReporteIncidencias.idCurso = 0;
                if (ddlCurso2.SelectedIndex > 0)
                    filtroReporteIncidencias.idCurso = Convert.ToInt32(ddlCurso2.SelectedValue);

                filtroReporteIncidencias.idPeriodo = Convert.ToInt32(ddlPeriodo2.SelectedValue);

                filtroReporteIncidencias.idAlumno = 0;
                if (ddlAlumno2.SelectedIndex > 0)
                    filtroReporteIncidencias.idAlumno = Convert.ToInt32(ddlAlumno2.SelectedValue);

                BLRptConsolidadoSancionesPeriodo objBLReporte = new BLRptConsolidadoSancionesPeriodo();
                listaReporteSanciones = objBLReporte.GetRptConsolidadoSanciones(filtroReporteIncidencias);

                listaReporteSanciones.Sort((p, q) => String.Compare(p.alumno, q.alumno));

                filtrosAplicados = filtros.ToString();

                rptSanciones.CargarReporte<RptConsolidadoSancionesPeriodo>(listaReporteSanciones);

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

			BLAsignatura objBLAsignatura = new BLAsignatura();
			UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, objBLAsignatura.GetAsignaturas(null), "idAsignatura", "nombre", true);

			List<Curso> listaCurso = new List<Curso>();
			List<Alumno> listaAlumno = new List<Alumno>();

			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);

			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);

			ddlPeriodo.Enabled = false;
			ddlAsignatura.Enabled = true;
			ddlCurso.Enabled = false;
			ddlAlumno.Enabled = false;
		}

        /// <summary>
        /// Cargars the combos.
        /// </summary>
        private void CargarCombosIncidencias()
        {
            List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
            BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
            listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);

            List<Curso> listaCurso = new List<Curso>();
            List<Alumno> listaAlumno = new List<Alumno>();

            UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo2, listaCicloLectivo, "idCicloLectivo", "nombre", true);

            UIUtilidades.BindCombo<Curso>(ddlCurso2, listaCurso, "idCurso", "Nombre", true);

            ddlPeriodo2.Enabled = false;
            ddlCurso2.Enabled = false;
            ddlAlumno2.Enabled = false;
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
                UIUtilidades.BindCombo<Curso>(ddlCurso2, listaCurso, "idCurso", "nombre", true);
				
                ddlCurso.Enabled = true;
                ddlCurso2.Enabled = true;
			}
			else
			{
				ddlCurso.Enabled = false;
                ddlCurso2.Enabled = false;
			}
		}

		private void CargarComboPeriodos(int idCicloLectivo, DropDownList ddlPeriodo)
		{
			if (idCicloLectivo > 0)
			{
				BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();

				UIUtilidades.BindCombo<Periodo>(ddlPeriodo, objBLCicloLectivo.GetPeriodosByCicloLectivo(idCicloLectivo), "idPeriodo", "nombre", true);
                UIUtilidades.BindCombo<Periodo>(ddlPeriodo2, objBLCicloLectivo.GetPeriodosByCicloLectivo(idCicloLectivo), "idPeriodo", "nombre", true);

				ddlPeriodo.Enabled = true;
                ddlPeriodo2.Enabled = true;
			}
			else
			{
				ddlPeriodo.Enabled = false;
                ddlPeriodo2.Enabled = false;
			}
		}

        protected void rdlAccion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rdlAccion.SelectedValue)
                {
                    case "0":
                        CargarPresentacion();
                        divFiltrosIncidencia.Visible = false;
                        divFiltros.Visible = true;
                        divPromedioPeriodo.Visible = true;
                        divSancionesPeriodo.Visible = false;
                        divInasistenciasPeriodo.Visible = false;
                        break;
                    case "1":
                        CargarPresentacionIncidencias();
                        divFiltrosIncidencia.Visible = true;
                        divFiltros.Visible = false;
                        divPromedioPeriodo.Visible = false;
                        divSancionesPeriodo.Visible = false;
                        divInasistenciasPeriodo.Visible = true;
                        break;
                    case "2":
                        CargarPresentacionIncidencias();
                        divFiltrosIncidencia.Visible = true;
                        divFiltros.Visible = false;
                        divPromedioPeriodo.Visible = false;
                        divSancionesPeriodo.Visible = true;
                        divInasistenciasPeriodo.Visible = false;
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