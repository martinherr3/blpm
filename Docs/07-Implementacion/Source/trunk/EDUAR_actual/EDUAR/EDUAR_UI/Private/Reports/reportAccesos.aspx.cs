using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using iTextSharp.text;

namespace EDUAR_UI
{
	public partial class reportAccesos : EDUARBasePage
	{
		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the filtro acceso.
		/// </summary>
		/// <value>
		/// The filtro acceso.
		/// </value>
		public FilAccesos filtroAcceso
		{
			get
			{
				if (ViewState["filtroAcceso"] == null)
					filtroAcceso = new FilAccesos();
				return (FilAccesos)ViewState["filtroAcceso"];
			}
			set
			{
				ViewState["filtroAcceso"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the lista acceso.
		/// </summary>
		/// <value>
		/// The lista acceso.
		/// </value>
		public List<RptAccesos> listaAcceso
		{
			get
			{
				if (ViewState["listaAcceso"] == null)
					listaAcceso = new List<RptAccesos>();
				return (List<RptAccesos>)ViewState["listaAcceso"];
			}
			set
			{
				ViewState["listaAcceso"] = value;
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
		/// Gets or sets the lista paginas.
		/// </summary>
		/// <value>
		/// The lista paginas.
		/// </value>
		public List<Pagina> listaPaginas
		{
			get
			{
				if (ViewState["listaPaginas"] == null)
				{
					listaPaginas = new List<Pagina>();
					BLPagina objBLPagina = new BLPagina();
					listaPaginas = objBLPagina.GetPaginas(null);
				}
				return (List<Pagina>)ViewState["listaPaginas"];
			}
			set
			{
				ViewState["listaPaginas"] = value;
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
				rptAccesos.ExportarPDFClick += (ExportarPDF);
				rptAccesos.VolverClick += (VolverReporte);
				rptAccesos.PaginarGrilla += (PaginarGrilla);
				rptAccesos.GraficarClick += (btnGraficar);
				rptAccesos.OrdenarGrilla += (OrdenarGrilla);

				if (!Page.IsPostBack)
				{
					TablaGrafico = null;
					CargarPresentacion();
					BLRptAccesos objBLAcceso = new BLRptAccesos();
					objBLAcceso.GetRptAccesos(null);
					divFiltros.Visible = true;
					divReporte.Visible = false;
				}
				//BuscarAccesos();
				if (listaAcceso != null)
					rptAccesos.CargarReporte<RptAccesos>(listaAcceso);
			}
			catch (Exception ex)
			{
				AvisoMostrar = true;
				AvisoExcepcion = ex;
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
				if (fechas.ValorFechaDesde.ToString() == string.Empty)
					fechas.FechaDesde.Text = DateTime.Now.AddDays(-7).ToShortDateString();
				if (fechas.ValorFechaHasta.ToString() == string.Empty)
					fechas.FechaHasta.Text = DateTime.Now.ToShortDateString();
				fechas.ValidarRangoDesdeHasta(false);
				BuscarAccesos();
				divFiltros.Visible = false;
				divReporte.Visible = true;
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
				ExportPDF.ExportarPDF(Page.Title, rptAccesos.dtReporte, ObjSessionDataUI.ObjDTUsuario.Nombre, filtrosAplicados);
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

		protected void btnGraficar(object sender, EventArgs e)
		{
			try
			{
				rptAccesos.graficoReporte.LimpiarSeries();
				bool filtroRoles = false;
				if (ddlDestino.SelectedIndex > 0)
				{
					foreach (enumRoles item in enumRoles.GetValues(typeof(enumRoles)))
					{
						var serie = new List<RptAccesos>();
						foreach (System.Web.UI.WebControls.ListItem pagina in ddlDestino.Items)
						{
							if (pagina.Selected)
							{
								List<RptAccesos> listaParcial = listaAcceso.FindAll(c => c.rol == item.ToString() && c.pagina == pagina.Text);
								if (listaParcial.Count > 0)
								{
									serie.Add(new RptAccesos
									{
										accesos = listaParcial.Count,
										pagina = pagina.Text
									});
								}
							}
						}
						if (serie != null && serie.Count > 0)
						{
							DataTable dt = UIUtilidades.BuildDataTable<RptAccesos>(serie);
							// En alumno envio la nota y en calificación la cantidad de esa nota que se produjo
							rptAccesos.graficoReporte.AgregarSerie(item.ToString(), dt, "pagina", "accesos");
						}
					}
					rptAccesos.graficoReporte.Titulo = "Reporte de Accesos \n" + Convert.ToDateTime(fechas.ValorFechaDesde).ToShortDateString() + " al " + Convert.ToDateTime(fechas.ValorFechaHasta).ToShortDateString();
					filtroRoles = true;
				}
				else
				{
					DateTime diasSeleccionados = Convert.ToDateTime(fechas.ValorFechaDesde);
					int cantDias = (Convert.ToDateTime(fechas.ValorFechaHasta) - diasSeleccionados).Days + 1;
					foreach (System.Web.UI.WebControls.ListItem item in ddlRoles.Items)
					{
						diasSeleccionados = Convert.ToDateTime(fechas.ValorFechaDesde);
						if (item.Selected)
						{
							filtroRoles = true;
							List<RptAccesos> listaFinal = new List<RptAccesos>();
							for (int i = 0; i < cantDias; i++)
							{
								List<RptAccesos> lista = listaAcceso.FindAll(c => c.rol == item.Text && c.fecha == diasSeleccionados);
								RptAccesos acceso = new RptAccesos();
								if (lista.Count > 0)
								{
									int contador = 0;
									acceso.rol = string.Empty;
									acceso.fecha = diasSeleccionados;
									acceso.pagina = "totalizada";
									foreach (RptAccesos pagina in lista)
									{
										contador += pagina.accesos;
									}
									if (contador > 0)
									{
										acceso.accesos = contador;
										listaFinal.Add(acceso);
									}
								}
								diasSeleccionados = diasSeleccionados.AddDays(1);
							}
							if (listaFinal.Count > 0)
							{
								DataTable dt = UIUtilidades.BuildDataTable<RptAccesos>(listaFinal);
								rptAccesos.graficoReporte.AgregarSerie(item.Text + " " + Convert.ToDateTime(fechas.ValorFechaDesde).ToShortDateString() + " al " + Convert.ToDateTime(fechas.ValorFechaHasta).ToShortDateString(), dt, "fecha", "accesos");
							}
						}
					}
					rptAccesos.graficoReporte.Titulo = "Accesos " + Convert.ToDateTime(fechas.ValorFechaDesde).ToShortDateString() + " al " + Convert.ToDateTime(fechas.ValorFechaHasta).ToShortDateString();
				}
				if (!filtroRoles)
				{
					DateTime diasSeleccionados = Convert.ToDateTime(fechas.ValorFechaDesde);
					int cantDias = (Convert.ToDateTime(fechas.ValorFechaHasta) - diasSeleccionados).Days + 1;
					List<RptAccesos> listaFinal = new List<RptAccesos>();
					for (int i = 0; i < cantDias; i++)
					{
						List<RptAccesos> lista = listaAcceso.FindAll(c => c.fecha == diasSeleccionados);
						RptAccesos acceso = new RptAccesos();
						if (lista.Count > 0)
						{
							int contador = 0;
							acceso.rol = string.Empty;
							acceso.fecha = diasSeleccionados;
							acceso.pagina = "totalizada";
							foreach (RptAccesos pagina in lista)
							{
								contador += pagina.accesos;
							}
							if (contador > 0)
							{
								acceso.accesos = contador;
								listaFinal.Add(acceso);
							}
						}
						diasSeleccionados = diasSeleccionados.AddDays(1);
					}
					DataTable dt = UIUtilidades.BuildDataTable<RptAccesos>(listaFinal);
					rptAccesos.graficoReporte.AgregarSerie(Convert.ToDateTime(fechas.ValorFechaDesde).ToShortDateString() + " al " + Convert.ToDateTime(fechas.ValorFechaHasta).ToShortDateString(), dt, "fecha", "accesos");
					rptAccesos.graficoReporte.Titulo = "Accesos " + Convert.ToDateTime(fechas.ValorFechaDesde).ToShortDateString() + " al " + Convert.ToDateTime(fechas.ValorFechaHasta).ToShortDateString();
				}

				rptAccesos.graficoReporte.GraficarBarra();
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

				if (rptAccesos.GrillaReporte.PageCount > pagina)
				{
					rptAccesos.GrillaReporte.PageIndex = pagina;

					rptAccesos.CargarReporte<RptAccesos>(listaAcceso);
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		protected void OrdenarGrilla(object sender, GridViewSortEventArgs e)
		{
			rptAccesos.GridSampleSortExpression = e.SortExpression;
			int pageIndex = rptAccesos.GrillaReporte.PageIndex;
			rptAccesos.GrillaReporte.DataSource = rptAccesos.sortDataView(rptAccesos.GrillaReporte.DataSource as DataView, false);
			rptAccesos.GrillaReporte.DataBind();
			rptAccesos.GrillaReporte.PageIndex = pageIndex;
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the combo pagina.
		/// </summary>
		private void CargarComboPagina()
		{
			//UIUtilidades.BindCombo<Pagina>(ddlDestino, listaPaginas, "idPagina", "titulo", true);

			foreach (Pagina item in listaPaginas)
			{
				ddlDestino.Items.Add(new System.Web.UI.WebControls.ListItem(item.titulo, item.idPagina.ToString()));
			}
		}

		/// <summary>
		/// Buscars the accesos.
		/// </summary>
		private void BuscarAccesos()
		{
			StringBuilder filtros = new StringBuilder();
			//filtroAcceso.idPagina = Convert.ToInt32(ddlPagina.SelectedValue);
			//if (filtroAcceso.idPagina > 0) filtros.AppendLine("- Página: " + ddlPagina.SelectedItem.Text);
			List<Pagina> listaPagina = new List<Pagina>();
			foreach (System.Web.UI.WebControls.ListItem item in ddlDestino.Items)
			{
				if (item.Selected)
				{
					if (!filtros.ToString().Contains("- Página"))
						filtros.AppendLine("- Página");
					filtros.AppendLine(" * " + item.Text);
					listaPagina.Add(new Pagina() { idPagina = Convert.ToInt16(item.Value) });
				}
			}
			filtroAcceso.listaPaginas = listaPagina;

			if (fechas.ValorFechaDesde != null)
			{
				filtros.AppendLine("- Fecha Desde: " + ((DateTime)fechas.ValorFechaDesde).ToShortDateString());
				filtroAcceso.fechaDesde = (DateTime)fechas.ValorFechaDesde;
			}
			if (fechas.ValorFechaHasta != null)
			{
				filtros.AppendLine("- Fecha Hasta: " + ((DateTime)fechas.ValorFechaHasta).ToShortDateString());
				filtroAcceso.fechaHasta = (DateTime)fechas.ValorFechaHasta;
			}
			List<DTRol> ListaRoles = new List<DTRol>();
			foreach (System.Web.UI.WebControls.ListItem item in ddlRoles.Items)
			{
				if (item.Selected)
				{
					if (!filtros.ToString().Contains("- Roles"))
						filtros.AppendLine("- Roles");
					filtros.AppendLine(" * " + item.Text);
					ListaRoles.Add(new DTRol() { Nombre = item.Value });
				}
			}
			filtroAcceso.listaRoles = ListaRoles;

			BLRptAccesos objBLReporte = new BLRptAccesos();
			listaAcceso = objBLReporte.GetRptAccesos(filtroAcceso);
			filtrosAplicados = filtros.ToString();

			rptAccesos.CargarReporte<RptAccesos>(listaAcceso);
		}

		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			CargarComboPagina();
			CargarListRoles();
		}

		/// <summary>
		/// Cargars the list roles.
		/// </summary>
		private void CargarListRoles()
		{
			DTSeguridad objSeguridad = new DTSeguridad();
			BLSeguridad objBLSeguridad = new BLSeguridad(objSeguridad);

			objBLSeguridad.GetRoles();
			foreach (DTRol rol in objBLSeguridad.Data.ListaRoles)
			{
				//chkListRolesBusqueda.Items.Add(new System.Web.UI.WebControls.ListItem(rol.Nombre, rol.NombreCorto));
				ddlRoles.Items.Add(new System.Web.UI.WebControls.ListItem(rol.Nombre, rol.NombreCorto));
			}
		}
		#endregion
	}
}