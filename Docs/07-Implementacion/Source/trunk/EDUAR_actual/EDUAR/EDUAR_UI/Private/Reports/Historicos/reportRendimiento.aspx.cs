using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using System.Globalization;

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

        public List<Asignatura> listaAsignatura
        {
            get
            {
                if (ViewState["listaAsignatura"] == null)
                {
                    listaAsignatura = new List<Asignatura>();
                    BLAsignatura objBLAsignatura = new BLAsignatura();
                    listaAsignatura = objBLAsignatura.GetAsignaturas(null);
                }
                return (List<Asignatura>)ViewState["listaAsignatura"];
            }
            set
            {
                ViewState["listaAsignatura"] = value;
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
        //public List<Asignatura> listaAsignatura
        //{
        //    get
        //    {
        //        if (ViewState["listaAsignatura"] == null)
        //            listaAsignatura = new List<Asignatura>();
        //        return (List<Asignatura>)ViewState["listaAsignatura"];
        //    }
        //    set
        //    {
        //        ViewState["listaAsignatura"] = value;
        //    }
        //}
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
					rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporte);
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
				AccionPagina = enumAcciones.Limpiar;
				rptCalificaciones.verGrafico = false;
				divFiltros.Visible = true;
				divReporte.Visible = false;
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

        ///// <summary>
        ///// BTNs the graficar.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnGraficar(object sender, EventArgs e)
        {
            try
            {

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
                AccionPagina = enumAcciones.Limpiar;
                //ddlCurso.Items.Clear();
                //int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);

                //CargarComboCursos(idCicloLectivo, ddlCurso);
                //if (Convert.ToInt16(ddlCicloLectivo.SelectedValue) > 0)
                //{

                //    ddlCurso.Enabled = true;
                //}
                //else
                //{
                //    ddlCurso.Items.Clear();
                //    ddlCurso.Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

         //<summary>
         //Handles the SelectedIndexChanged event of the ddlNivel control.
         //</summary>
         //<param name="sender">The source of the event.</param>
         //<param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AccionPagina = enumAcciones.Limpiar;
                int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
                //CargarComboCursos(idCicloLectivo, ddlCurso);
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
				//CargarComboAsignatura();
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

                //if (ddlAsignatura.SelectedIndex > 0)
                //{
                //    filtroReporte.idAsignatura = Convert.ToInt32(ddlAsignatura.SelectedValue);
                //    filtros.AppendLine("- Asignatura: " + ddlAsignatura.SelectedItem.Text);
                //}


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


				if (Convert.ToInt32(ddlCicloLectivo.SelectedValue) > 0)
				{
					filtroReporte.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
					filtros.AppendLine("- Ciclo Lectivo: " + ddlCicloLectivo.SelectedItem.Text);
				}
				if (ddlNivel.Items.Count > 0 && Convert.ToInt32(ddlNivel.SelectedValue) > 0)
				{
					filtroReporte.idNivel = Convert.ToInt32(ddlNivel.SelectedValue);
					filtros.AppendLine("- Nivel: " + ddlNivel.SelectedItem.Text);
				}
                //if (ddlCurso.Items.Count > 0 && Convert.ToInt32(ddlCurso.SelectedValue) > 0)
                //{
                //    filtroReporte.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);
                //    filtros.AppendLine("- Curso: " + ddlCurso.SelectedItem.Text);
                //}

				//if (Context.User.IsInRole(enumRoles.Docente.ToString()))
				//	filtroReporte.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

				BLRptCalificacionesAlumnoPeriodo objBLReporte = new BLRptCalificacionesAlumnoPeriodo();
				listaReporte = objBLReporte.GetRptRendimientoHistorico(filtroReporte);
				filtrosAplicados = filtros.ToString();

				rptCalificaciones.CargarReporte<RptRendimientoHistorico>(listaReporte);
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

			List<Curso> listaCurso = new List<Curso>();
			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);

            CargarNiveles();
            CargarComboAsignatura();

			//ddlAsignatura.Enabled = false;
		}

		/// <summary>
		/// Cargars the combo cursos.
		/// </summary>
		/// <param name="idCicloLectivo">The id ciclo lectivo.</param>
		/// <param name="ddlCurso">The DDL curso.</param>
        //private void CargarComboCursos(int idCicloLectivo, DropDownList ddlCurso)
        //{
        //    if (idCicloLectivo > 0)
        //    {
        //        List<Curso> listaCurso = new List<Curso>();
        //        BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();

        //        listaCurso = objBLCicloLectivo.GetCursosByCicloLectivo(idCicloLectivo);


        //        //if (User.IsInRole(enumRoles.Docente.ToString()))
        //        //	objAsignatura.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

        //        UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "nombre", true);
        //        ddlCurso.Enabled = true;
        //    }
        //    else
        //    {
        //        ddlCurso.Enabled = false;
        //    }
        //}

		/// <summary>
		/// Cargars the combo asignatura.
		/// </summary>
		private void CargarComboAsignatura()
		{
            //BLAsignatura objBLAsignatura = new BLAsignatura();
            ////if (User.IsInRole(enumRoles.Docente.ToString()))
            ////materia.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
            //listaAsignatura = objBLAsignatura.GetAsignaturas(null);
            //UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, listaAsignatura, "idAsignatura", "nombre", true);
            //ddlAsignatura.Enabled = false;
            //if (ddlAsignatura.Items.Count > 0)
            //    ddlAsignatura.Enabled = true;

                        // Ordena la lista alfabéticamente por la descripción
            listaAsignatura.Sort((p, q) => string.Compare(p.nombre, q.nombre));

            // Carga el combo de tipo de asistencia para filtrar
            foreach (Asignatura item in listaAsignatura)
            {
                ddlAsignatura.Items.Add(new System.Web.UI.WebControls.ListItem(item.nombre, item.idAsignatura.ToString()));
            }

		}

		/// <summary>
		/// Cargars the niveles.
		/// </summary>
		/// <param name="idCurso">The id curso.</param>
		private void CargarNiveles()
		{
			BLNivel objBLNivel = new BLNivel();
			UIUtilidades.BindCombo<Nivel>(ddlNivel, objBLNivel.GetNiveles(), "idNivel", "nombre", true);
			
            ddlNivel.Enabled = true;
		}

        ///// <summary>
        ///// Generars the datos grafico.
        ///// </summary>
        private void GenerarDatosGrafico()
        {
            var cantAsignaturas =
                from p in listaReporte
                group p by p.asignatura into g
                select new { Asignatura = g.Key, Cantidad = g.Count() };

            //TablaGrafico.Add("- Cantidad de Alumnos analizados: " + cantAlumnos.Count().ToString());

            TablaGrafico.Add("- Cantidad de Asignaturas: " + listaReporte.Count.ToString());

            //var fechaMin =
            //   from p in listaReporte
            //   group p by p.alumno into g
            //   select new { Alumno = g.Key, Fecha = g.Min(p => p.fecha) };

            //var fechaMax =
            //   from p in listaReporte
            //   group p by p.alumno into g
            //   select new { Alumno = g.Key, Fecha = g.Max(p => p.fecha) };

            //TablaGrafico.Add("- Periodo de notas: " + fechaMin.First().Fecha.ToShortDateString() + " - " + fechaMax.First().Fecha.ToShortDateString());

            var topPromedio =
               (from p in listaReporte
                group p by p.asignatura into g
                orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
                select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct().Take(3);

            TablaGrafico.Add("- Top 3 Materias con mejor desempeño:");
            foreach (var item in topPromedio)
            {
                TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString());
            }
            
        }
		#endregion

	}
}