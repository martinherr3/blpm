using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

namespace EDUAR_UI
{
    public partial class reportAnalisisAgrupadoCicloLectivoCursoAsignaturaAlumno : EDUARBasePage
	{
		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the filtro calificaciones.
		/// </summary>
		/// <value>
		/// The filtro calificaciones.
		/// </value>
		public FilAnalisisAgrupados filtroAnalisis
		{
			get
			{
				if (ViewState["filtroAnalisis"] == null)
                    filtroAnalisis = new FilAnalisisAgrupados();
				return (FilAnalisisAgrupados)ViewState["filtroAnalisis"];
			}
			set
			{
                ViewState["filtroAnalisis"] = value;
			}
		}

		public List<RptAnalisisCicloLectivoCursoAsignaturaAlumno> listaReporte
		{
			get
			{
				if (Session["listaReporte"] == null)
					listaReporte = new List<RptAnalisisCicloLectivoCursoAsignaturaAlumno>();
				return (List<RptAnalisisCicloLectivoCursoAsignaturaAlumno>)Session["listaReporte"];
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
		/// Gets or sets the lista niveles.
		/// </summary>
		/// <value>
		/// The lista niveles.
		/// </value>
		public List<Nivel> listaNiveles
		{
			get
			{
				if (ViewState["listaNiveles"] == null)
				{
					listaNiveles = new List<Nivel>();
					BLNivel objBLNivel = new BLNivel();
					listaNiveles = objBLNivel.GetNiveles();
				}
				return (List<Nivel>)ViewState["listaNiveles"];
			}
			set
			{
				ViewState["listaNiveles"] = value;
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

        public List<Alumno> listaAlumnos
        {
            get
            {
                if (ViewState["listaAlumnos"] == null)
                {
                    listaAlumnos = new List<Alumno>();
                    BLAlumno objBLAlumno = new BLAlumno();
                    listaAlumnos = objBLAlumno.GetAlumnos(null);
                }
                return (List<Alumno>)ViewState["listaAlumnos"];
            }
            set
            {
                ViewState["listaAlumnos"] = value;
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
                rptPromediosAnalizados.ExportarPDFClick += (ExportarPDF);
                rptPromediosAnalizados.VolverClick += (VolverReporte);
                rptPromediosAnalizados.PaginarGrilla += (PaginarGrilla);
				Master.BotonAvisoAceptar += (VentanaAceptar);
                rptPromediosAnalizados.GraficarClick += (btnGraficar);

				if (!Page.IsPostBack)
				{
					CargarPresentacion();
					divFiltros.Visible = true;
					divReporte.Visible = false;
				}
				if (listaReporte != null)
                    rptPromediosAnalizados.CargarReporte<RptAnalisisCicloLectivoCursoAsignaturaAlumno>(listaReporte);
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
				if (BuscarPromediosAgrupados())
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
                if (rptPromediosAnalizados.verGrafico)
					nombreGrafico = nombrePNG;
                ExportPDF.ExportarPDF(Page.Title, rptPromediosAnalizados.dtReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
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
                rptPromediosAnalizados.verGrafico = false;
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
				double sumaNotas = 0;
                rptPromediosAnalizados.graficoReporte.LimpiarSeries();
				string alumno = string.Empty;

				if (ddlAsignatura.SelectedIndex > 0)
				{ // so reporte   distribucion de calificaciones por asignatura 
					foreach (ListItem itemAsig in ddlAsignatura.Items)
					{
						if (itemAsig.Selected)
						{
							var serie = new List<RptRendimientoHistorico>();
							foreach (ListItem itemCiclo in ddlCicloLectivo.Items)
							{
								sumaNotas = 0;
								var listaParcial = listaReporte.FindAll(p => p.asignatura == itemAsig.Text && p.ciclolectivo == itemCiclo.Text);

								if (listaParcial.Count > 0)
								{
									foreach (var nota in listaParcial)
									{
										sumaNotas += Convert.ToDouble(nota.promedio);
									}

									serie.Add(new RptRendimientoHistorico
									{
										promedio = Math.Round(sumaNotas / listaParcial.Count, 2).ToString(CultureInfo.InvariantCulture),
										ciclolectivo = itemCiclo.Text
									});
								}
							}
							if (serie != null && serie.Count > 0)
							{
								DataTable dt = UIUtilidades.BuildDataTable<RptRendimientoHistorico>(serie);
                                rptPromediosAnalizados.graficoReporte.AgregarSerie(itemAsig.Text, dt, "ciclolectivo", "promedio");
							}
						}
                        rptPromediosAnalizados.graficoReporte.Titulo = "Promedio de Calificaciones por Asignatura \n";
					}

				}
				else
				{ // promedio de calificaciones por año por asignatura
					var Ciclos = (from p in listaReporte
								  group p by p.ciclolectivo into g
								  //orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
								  select new { CicloLectivo = g.Key }).Distinct();

					//foreach (ListItem itemCiclo in ddlCicloLectivo.Items)
					var serie = new List<RptRendimientoHistorico>();

					foreach (var itemCiclo in Ciclos)
					{
						//if (itemCiclo.Selected)
						//{
						//foreach (var item in listaAsignatura)
						//{

						var Promedio =
					   (from p in listaReporte
						where p.ciclolectivo == itemCiclo.CicloLectivo
						group p by p.ciclolectivo into g
						//orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
						select new { CicloLectivo = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct();

						//sumaNotas = 0;
						////var listaParcial = listaReporte.FindAll(p => p.asignatura == item.nombre && p.ciclolectivo == itemCiclo.CicloLectivo);
						//var listaParcial = listaReporte.FindAll(p => p.ciclolectivo == itemCiclo.CicloLectivo);

						//if (listaParcial.Count > 0)
						{
							//foreach (var nota in listaParcial)
							//{
							//    sumaNotas += Convert.ToDouble(nota.promedio);
							//}
							foreach (var item in Promedio)
							{
								serie.Add(new RptRendimientoHistorico
								{
									//promedio = Math.Round(sumaNotas / listaParcial.Count, 2).ToString(CultureInfo.InvariantCulture),
									promedio = Math.Round(item.Promedio, 2).ToString(CultureInfo.InvariantCulture),
									asignatura = item.CicloLectivo
								});
							}
							//}
						}
						//}
					}
					if (serie != null && serie.Count > 0)
					{
						DataTable dt = UIUtilidades.BuildDataTable<RptRendimientoHistorico>(serie);
                        rptPromediosAnalizados.graficoReporte.AgregarSerie("Promedios", dt, "asignatura", "promedio");
					}
                    rptPromediosAnalizados.graficoReporte.Titulo = "Promedio de Calificaciones por Ciclo Lectivo \n";
                    rptPromediosAnalizados.graficoReporte.habilitarTorta = false;
				}
				GenerarDatosGrafico();
                rptPromediosAnalizados.graficoReporte.GraficarLinea();
                rptPromediosAnalizados.CargarReporte<RptAnalisisCicloLectivoCursoAsignaturaAlumno>(listaReporte);
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

                if (rptPromediosAnalizados.GrillaReporte.PageCount > pagina)
				{
                    rptPromediosAnalizados.GrillaReporte.PageIndex = pagina;

                    rptPromediosAnalizados.CargarReporte<RptAnalisisCicloLectivoCursoAsignaturaAlumno>(listaReporte);
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
			CargarCombos();
			btnBuscar.Visible = true;
		}

		/// <summary>
		/// Buscars the calificaciones.
		/// </summary>
		private bool BuscarPromediosAgrupados()
		{
			if (AccionPagina == enumAcciones.Buscar)
			{
				filtroAnalisis = new FilAnalisisAgrupados();
				StringBuilder filtros = new StringBuilder();

				List<Asignatura> listaAsignatura = new List<Asignatura>();
				foreach (System.Web.UI.WebControls.ListItem item in ddlAsignatura.Items)
				{
					if (item.Selected)
					{
                        if (!filtros.ToString().Contains("- Asignatura(s)"))
                            filtros.AppendLine("- Asignatura(s)");
						filtros.AppendLine(" * " + item.Text);
						listaAsignatura.Add(new Asignatura() { idAsignatura = Convert.ToInt16(item.Value) });
					}
				}
				filtroAnalisis.listaAsignaturas = listaAsignatura;

				List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
				foreach (System.Web.UI.WebControls.ListItem item in ddlCicloLectivo.Items)
				{
					if (item.Selected)
					{
                        if (!filtros.ToString().Contains("- Ciclo(s) Lectivo(s)"))
                            filtros.AppendLine("- Ciclo(s) Lectivo(s)");
						filtros.AppendLine(" * " + item.Text);
						listaCicloLectivo.Add(new CicloLectivo() { idCicloLectivo = Convert.ToInt16(item.Value) });
					}
				}
				filtroAnalisis.listaCicloLectivo = listaCicloLectivo;


                List<Nivel> listaNivel = new List<Nivel>();
                foreach (System.Web.UI.WebControls.ListItem item in ddlNivel.Items)
                {
                    if (item.Selected)
                    {
                        if (!filtros.ToString().Contains("- Nivel(es)"))
                            filtros.AppendLine("- Nivel(es)");
                        filtros.AppendLine(" * " + item.Text);
                        listaNivel.Add(new Nivel() { idNivel = Convert.ToInt16(item.Value) });
                    }
                }
                filtroAnalisis.listaNiveles = listaNivel;
                
                List<Alumno> listaAlumno = new List<Alumno>();
                foreach (System.Web.UI.WebControls.ListItem item in ddlAlumno.Items)
                {
                    if (item.Selected)
                    {
                        if (!filtros.ToString().Contains("- Alumno(s)"))
                            filtros.AppendLine("- Alumno(s)");
                        filtros.AppendLine(" * " + item.Text);
                        listaAlumno.Add(new Alumno() { idAlumno = Convert.ToInt16(item.Value) });
                    }
                }
                filtroAnalisis.listaNiveles = listaNiveles;

				BLRptAnalisisAgrupadosCicloLectivoCursoAsignaturaAlumno objBLReporte = new BLRptAnalisisAgrupadosCicloLectivoCursoAsignaturaAlumno();
				listaReporte = objBLReporte.GetRptAnalisisCicloLectivoCursoAsignaturaAlumno(filtroAnalisis);
				filtrosAplicados = filtros.ToString();

                rptPromediosAnalizados.CargarReporte<RptAnalisisCicloLectivoCursoAsignaturaAlumno>(listaReporte);
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
			//UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
			CargarComboCicloLectivo();
			CargarNiveles();
			CargarComboAsignatura();
            CargarComboAlumnos();
		}

		private void CargarComboCicloLectivo()
		{
			ddlCicloLectivo.Items.Clear();

			BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();

			listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);

			// Ordena la lista alfabéticamente por la descripción
			listaCicloLectivo.Sort((p, q) => string.Compare(p.nombre, q.nombre));

			foreach (CicloLectivo cicloLectivo in listaCicloLectivo)
			{
				ddlCicloLectivo.Items.Add(new System.Web.UI.WebControls.ListItem(cicloLectivo.nombre, cicloLectivo.idCicloLectivo.ToString()));
			}

			if (ddlCicloLectivo.Items.Count > 0)
				ddlCicloLectivo.Disabled = false;
		}

		/// <summary>
		/// Cargars the combo asignatura.
		/// </summary>
		private void CargarComboAsignatura()
		{
			ddlAsignatura.Items.Clear();

			BLAsignatura objBLAsignatura = new BLAsignatura();

            List<CicloLectivo> listaCicloLectivos = new List<CicloLectivo>();
            foreach (System.Web.UI.WebControls.ListItem item in ddlCicloLectivo.Items)
            {
                if (item.Selected)
                {
                    listaCicloLectivos.Add(new CicloLectivo() { idCicloLectivo = Convert.ToInt16(item.Value) });
                }
            }

            List<Nivel> listaNiveles = new List<Nivel>();
            foreach (System.Web.UI.WebControls.ListItem item in ddlNivel.Items)
            {
                if (item.Selected)
                {
                    listaNiveles.Add(new Nivel() { idNivel = Convert.ToInt16(item.Value) });
                }
            }

            listaAsignatura = objBLAsignatura.GetAsignaturasNivelesCiclosLectivos(listaCicloLectivos, listaNiveles);

			// Ordena la lista alfabéticamente por la descripción
			listaAsignatura.Sort((p, q) => string.Compare(p.nombre, q.nombre));

			foreach (Asignatura asignatura in listaAsignatura)
			{
				ddlAsignatura.Items.Add(new System.Web.UI.WebControls.ListItem(asignatura.nombre, asignatura.idAsignatura.ToString()));
			}

			if (ddlAsignatura.Items.Count > 0)
				ddlAsignatura.Disabled = false;

		}

        /// <summary>
        /// Cargars the combo asignatura.
        /// </summary>
        private void CargarComboAlumnos()
        {
            ddlAlumno.Items.Clear();

            BLAlumno objBLAlumno = new BLAlumno();

            List<CicloLectivo> listaCicloLectivos = new List<CicloLectivo>();
            foreach (System.Web.UI.WebControls.ListItem item in ddlCicloLectivo.Items)
            {
                if (item.Selected)
                {
                    listaCicloLectivos.Add(new CicloLectivo() { idCicloLectivo = Convert.ToInt16(item.Value) });
                }
            }

            List<Nivel> listaNiveles = new List<Nivel>();
            foreach (System.Web.UI.WebControls.ListItem item in ddlNivel.Items)
            {
                if (item.Selected)
                {
                    listaNiveles.Add(new Nivel() { idNivel = Convert.ToInt16(item.Value) });
                }
            }

            listaAlumnos = objBLAlumno.GetAlumnosNivelCicloLectivo(listaCicloLectivo, listaNiveles);

            // Ordena la lista alfabéticamente por la descripción
            listaAlumnos.Sort((p, q) => string.Compare(p.nombre, q.nombre));

            foreach (Alumno alumno in listaAlumnos)
            {
                ddlAlumno.Items.Add(new System.Web.UI.WebControls.ListItem(alumno.nombre, alumno.idAlumno.ToString()));
            }

            if (ddlAlumno.Items.Count > 0)
                ddlAlumno.Disabled = false;
        }


		/// <summary>
		/// Cargars the niveles.
		/// </summary>
		/// <param name="idCurso">The id curso.</param>
		private void CargarNiveles()
		{
            //ddlNivel.Items.Clear();

            BLNivel objBLNivel = new BLNivel();

            listaNiveles = objBLNivel.GetNiveles();

            // Ordena la lista alfabéticamente por la descripción
            listaNiveles.Sort((p, q) => string.Compare(p.nombre, q.nombre));

            foreach (Nivel unNivel in listaNiveles)
            {
                ddlNivel.Items.Add(new System.Web.UI.WebControls.ListItem(unNivel.nombre, unNivel.idNivel.ToString()));
            }

            if (ddlNivel.Items.Count > 0)
                ddlNivel.Disabled = false;
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

			//TablaGrafico.Add("- Registros Totales: " + listaReporte.Count.ToString());

			TablaGrafico = new List<TablaGrafico>();
			TablaGrafico tabla3 = new TablaGrafico();
			tabla3.listaCuerpo = new List<List<string>>();
			List<string> encabezado3 = new List<string>();
			List<string> fila3 = new List<string>();
			tabla3.titulo = "Periodos Analizados: \n";

			foreach (ListItem item in ddlCicloLectivo.Items)
			{
				if (item.Selected)
				{
					tabla3.titulo += item.Value + "\n";
				}
			}

			tabla3.listaEncabezados = encabezado3;
			tabla3.listaCuerpo.Add(fila3);
			TablaGrafico.Add(tabla3);

			//if (!string.IsNullOrEmpty(ddlAsignatura.Value) && Convert.ToInt32(ddlAsignatura.Value) > 0)
			//{
			//    #region --[Recorrer Ciclos Lectivos Seleccionados]--
			//    var Ciclos = (from p in listaReporte
			//                  group p by p.ciclolectivo into g
			//                  //orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
			//                  select new { CicloLectivo = g.Key }).Distinct();

			//    foreach (var item in Ciclos)
			//    {
			//        TablaGrafico tabla2 = new TablaGrafico();
			//        tabla2.listaCuerpo = new List<List<string>>();
			//        List<string> encabezado2 = new List<string>();
			//        List<List<string>> filasTabla2 = new List<List<string>>();
			//        List<string> fila2 = new List<string>();

			//        tabla2.titulo = "Top 3 Materias de Mejor Desempeño " + item.CicloLectivo;
			//        encabezado2.Add("Asignatura");
			//        encabezado2.Add("Promedio");
			//        //encabezado2.Add("Ciclo Lectivo");
			//        var topPromedio =
			//           (from p in listaReporte
			//            where p.ciclolectivo == item.CicloLectivo
			//            group p by p.asignatura into g
			//            orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
			//            select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct().Take(3);

			//        //TablaGrafico.Add("- Top 3 Materias con mejor desempeño por Ciclo Lectivo:");
			//        foreach (var materia in topPromedio)
			//        {
			//            //TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString());
			//            fila2 = new List<string>();
			//            fila2.Add(materia.Asignatura);
			//            fila2.Add(materia.Promedio.ToString("#.00"));
			//            //fila2.Add(item.Text);
			//            filasTabla2.Add(fila2);
			//        }
			//        if (filasTabla2.Count > 0)
			//        {
			//            tabla2.listaEncabezados = encabezado2;
			//            tabla2.listaCuerpo = filasTabla2;
			//            TablaPropiaGrafico.Add(tabla2);
			//        }

			//        TablaGrafico tabla4 = new TablaGrafico();
			//        tabla4.listaCuerpo = new List<List<string>>();
			//        List<string> encabezado4 = new List<string>();
			//        List<List<string>> filasTabla4 = new List<List<string>>();
			//        List<string> fila4 = new List<string>();

			//        tabla4.titulo = "Top 3 Materias de Bajo Desempeño " + item.CicloLectivo;
			//        encabezado4.Add("Asignatura");
			//        encabezado4.Add("Promedio");
			//        //encabezado4.Add("Ciclo Lectivo");
			//        var lowPromedio =
			//           (from p in listaReporte
			//            where p.ciclolectivo == item.CicloLectivo
			//            group p by p.asignatura into g
			//            orderby g.Average(p => Convert.ToDouble(p.promedio)) ascending
			//            select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct().Take(3);

			//        //TablaGrafico.Add("- Top 3 Materias con mejor desempeño por Ciclo Lectivo:");
			//        foreach (var materia in lowPromedio)
			//        {
			//            //TablaGrafico.Add(item.Asignatura + " - Promedio: " + item.Promedio.ToString());
			//            fila4 = new List<string>();
			//            fila4.Add(materia.Asignatura);
			//            fila4.Add(materia.Promedio.ToString("#.00"));
			//            //fila4.Add(item.Text);
			//            filasTabla4.Add(fila4);
			//        }
			//        if (filasTabla4.Count > 0)
			//        {
			//            tabla4.listaEncabezados = encabezado4;
			//            tabla4.listaCuerpo = filasTabla4;
			//            TablaPropiaGrafico.Add(tabla4);
			//        }
			//    }
			//    #endregion
			//}
			//else
			{
				TablaGrafico tabla2 = new TablaGrafico();
				tabla2.listaCuerpo = new List<List<string>>();
				List<string> encabezado2 = new List<string>();
				List<List<string>> filasTabla2 = new List<List<string>>();
				List<string> fila2 = new List<string>();

				var Ciclos =
				   (from p in listaReporte
					group p by p.ciclolectivo into g
					//orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
					select new { CicloLectivo = g.Key }).Distinct();

				var Asignaturas =
				   (from p in listaReporte
					group p by p.asignatura into g
					//orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
					select new { Asignatura = g.Key }).Distinct();

				tabla2.titulo = "Desempeño Por Ciclo Lectivo ";
				//encabezado2.Add("Promedio");
				encabezado2.Add("Asignatura");

				foreach (var item in Ciclos)
				{
					encabezado2.Add(item.CicloLectivo);
				}

				foreach (var materia in Asignaturas)
				{
					fila2 = new List<string>();
					fila2.Add(materia.Asignatura);
					foreach (var item in Ciclos)
					{
						//encabezado2.Add(item.CicloLectivo);

						var Promedio =
						   (from p in listaReporte
							where p.ciclolectivo == item.CicloLectivo && p.asignatura == materia.Asignatura
							group p by p.asignatura into g
							//orderby g.Average(p => Convert.ToDouble(p.promedio)) descending
							select new { Asignatura = g.Key, Promedio = g.Average(p => Convert.ToDouble(p.promedio)) }).Distinct();

						foreach (var itemPromedio in Promedio)
						{
							fila2.Add(itemPromedio.Promedio.ToString("#.00"));
							//fila2.Add(item.Text);
						}
						if (Promedio.Count() == 0)
							fila2.Add(string.Empty);
					}
					filasTabla2.Add(fila2);
				}
				if (filasTabla2.Count > 0)
				{
					tabla2.listaEncabezados = encabezado2;
					tabla2.listaCuerpo = filasTabla2;
					TablaGrafico.Add(tabla2);
				}
			}
		}
		#endregion
	}
}