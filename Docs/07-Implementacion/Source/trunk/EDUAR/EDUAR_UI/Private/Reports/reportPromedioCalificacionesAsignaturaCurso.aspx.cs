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
using System.Data;

namespace EDUAR_UI
{
	public partial class reportPromedioCalificacionesAsignaturaCurso : EDUARBasePage
	{
		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the filtro PromedioCalificacionesAsignaturaCurso.
		/// </summary>
		/// <value>
		/// The filtro sanciones.
		/// </value>
		public FilPromedioCalificacionesPeriodo filtroReporte
		{
			get
			{
				if (ViewState["filtroPromediosCurso"] == null)
					filtroReporte = new FilPromedioCalificacionesPeriodo();
				return (FilPromedioCalificacionesPeriodo)ViewState["filtroPromediosCurso"];
			}
			set
			{
				ViewState["filtroPromediosCurso"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista PromedioCalificacionesAsignaturaCurso.
		/// </summary>
		/// <value>
		/// The lista sanciones.
		/// </value>
        public List<RptPromedioCalificacionesAsignaturaCursoPeriodo> listaReporte
		{
			get
			{
				if (Session["listaPromedioCalificacionesAsignaturaCurso"] == null)
                    listaReporte = new List<RptPromedioCalificacionesAsignaturaCursoPeriodo>();
                return (List<RptPromedioCalificacionesAsignaturaCursoPeriodo>)Session["listaPromedioCalificacionesAsignaturaCurso"];
			}
			set
			{
				Session["listaPromedioCalificacionesAsignaturaCurso"] = value;
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
				rptPromedioCalificacionesAsignaturaCurso.ExportarPDFClick += (ExportarPDF);
				rptPromedioCalificacionesAsignaturaCurso.VolverClick += (VolverReporte);
				rptPromedioCalificacionesAsignaturaCurso.PaginarGrilla += (PaginarGrilla);
                rptPromedioCalificacionesAsignaturaCurso.GraficarClick += (btnGraficar);

				Master.BotonAvisoAceptar += (VentanaAceptar);

				if (!Page.IsPostBack)
				{
					CargarPresentacion();
					BLRptPromedioCalificacionesPeriodo objBLRptPromedioCalificacionesAsignaturaCurso = new BLRptPromedioCalificacionesPeriodo();
                    objBLRptPromedioCalificacionesAsignaturaCurso.GetRptPromedioCalificaciones(null);
					divFiltros.Visible = true;
					divPromedioPeriodo.Visible = false;
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
                if (BuscarPromedioCurso())
				{
					divFiltros.Visible = false;
					divPromedioPeriodo.Visible = true;
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
				ExportPDF.ExportarPDF(Page.Title, rptPromedioCalificacionesAsignaturaCurso.dtReporte, ObjDTSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
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
				divPromedioPeriodo.Visible = false;
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}


        protected void btnGraficar(object sender, EventArgs e)
        {
            try
            {
                rptPromedioCalificacionesAsignaturaCurso.graficoReporte.LimpiarSeries();
                bool filtroRoles = false;
                //if (ddlAsignatura.SelectedIndex > 0)
                {
                    List<RptPromedioCalificacionesAsignaturaCursoPeriodo> listaPromedioCalificaciones = new List<RptPromedioCalificacionesAsignaturaCursoPeriodo>();

                    if (Session["listaPromedioCalificacionesAsignaturaCurso"] != null)
                    {
                        listaPromedioCalificaciones = (List<RptPromedioCalificacionesAsignaturaCursoPeriodo>)Session["listaPromedioCalificacionesAsignaturaCurso"];
                    }
                    for (int i = 0; i <= 10; i++)
                    {
                        List<RptPromedioCalificacionesAsignaturaCursoPeriodo> lista = listaPromedioCalificaciones.FindAll(c => c.promedio == Convert.ToString(i));
                        if (lista.Count > 0)
                        {
                            DataTable dt = UIUtilidades.BuildDataTable<RptPromedioCalificacionesAsignaturaCursoPeriodo>(lista);
                            rptPromedioCalificacionesAsignaturaCurso.graficoReporte.AgregarSerie(Convert.ToString(i), dt, "asignatura", "promedio");
                        }
                        rptPromedioCalificacionesAsignaturaCurso.graficoReporte.Titulo = "Promedio de Calificaciones en las Materia ";
                        filtroRoles = true;

                    }

                    ////////////////
                    //foreach (enumRoles item in enumRoles.GetValues(typeof(enumRoles)))
                    //{
                    //    List<RptAccesos> lista = listaAcceso.FindAll(c => c.rol == item.ToString());
                    //    if (lista.Count > 0)
                    //    {
                    //        DataTable dt = UIUtilidades.BuildDataTable<RptAccesos>(lista);
                    //        rptAccesos.graficoReporte.AgregarSerie(item.ToString(), dt, "fecha", "accesos");
                    //    }
                    //}
                    //rptAccesos.graficoReporte.Titulo = "Accesos Página " + ddlPagina.SelectedItem.Text;
                    //filtroRoles = true;
                }
                //else
                {
                    // ToDo: Contemplar otros casos para graficos 
                }

                rptPromedioCalificacionesAsignaturaCurso.graficoReporte.GraficarBarra();
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

				if (rptPromedioCalificacionesAsignaturaCurso.GrillaReporte.PageCount > pagina)
				{
					rptPromedioCalificacionesAsignaturaCurso.GrillaReporte.PageIndex = pagina;
                    rptPromedioCalificacionesAsignaturaCurso.CargarReporte<RptPromedioCalificacionesAsignaturaCursoPeriodo>(listaReporte);
                    //                                                    RptPromedioCalificacionesAsignaturaCurso
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
				int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
				CargarComboCursos(idCicloLectivo, ddlCurso);
				CargarComboPeriodos(idCicloLectivo, ddlPeriodo);
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
				//CargarAlumnos(Convert.ToInt32(ddlCurso.SelectedValue));
				//ddlAlumno.Enabled = true;
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

        //private void CargarAlumnos(int idCurso)
        //{
        //    BLAlumno objBLAlumno = new BLAlumno();
        //    UIUtilidades.BindCombo<Alumno>(ddlAlumno, objBLAlumno.GetAlumnos(new AlumnoCurso(idCurso)), "idAlumno", "apellido", "nombre", true);
        //    ddlAlumno.Enabled = true;
        //}
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
		/// Buscars the sanciones.
		/// </summary>
		private bool BuscarPromedioCurso()
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

				BLRptPromedioCalificacionesPeriodo objBLReporte = new BLRptPromedioCalificacionesPeriodo();
				//listaReporte = objBLReporte.GetRptPromedioCalificacionesAsignaturaCurso(filtroReporte);
                listaReporte = objBLReporte.GetRptPromedioCalificacionesAsignaturaCurso(filtroReporte);
                
				listaReporte.Sort((p, q) => String.Compare(p.asignatura, q.asignatura));

				filtrosAplicados = filtros.ToString();

                rptPromedioCalificacionesAsignaturaCurso.CargarReporte<RptPromedioCalificacionesAsignaturaCursoPeriodo>(listaReporte);

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

			//BLAsignatura objBLAsignatura = new BLAsignatura();
			//UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, objBLAsignatura.GetAsignaturas(null), "idAsignatura", "nombre", true);

			List<Curso> listaCurso = new List<Curso>();
			List<Alumno> listaAlumno = new List<Alumno>();

			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);

			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);

			ddlPeriodo.Enabled = false;
			//ddlAsignatura.Enabled = true;
			ddlCurso.Enabled = false;
            //ddlAlumno.Enabled = false;
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
		#endregion
	}
}