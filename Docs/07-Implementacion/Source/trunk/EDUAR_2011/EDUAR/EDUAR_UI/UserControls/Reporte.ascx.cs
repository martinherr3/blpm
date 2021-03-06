﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI.UserControls
{
	public partial class Reporte : UserControl
	{
		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the grilla reporte.
		/// </summary>
		/// <value>
		/// The grilla reporte.
		/// </value>
		public GridView GrillaReporte
		{
			get { return gvwReporte; }
			set { gvwReporte = value; }
		}

		/// <summary>
		/// Gets or sets the grafico reporte.
		/// </summary>
		/// <value>
		/// The grafico reporte.
		/// </value>
		public Grafico graficoReporte
		{
			get { return grafico; }
			set { grafico = value; }
		}

		/// <summary>
		/// Gets or sets the dt reporte.
		/// </summary>
		/// <value>
		/// The dt reporte.
		/// </value>
		public DataTable dtReporte
		{
			get
			{
				if (Session["dtReporte"] == null)
					Session["dtReporte"] = new DataTable();
				return (DataTable)Session["dtReporte"];
			}
			set { Session["dtReporte"] = value; }
		}

		/// <summary>
		/// Gets or sets the titulo reporte.
		/// </summary>
		/// <value>
		/// The titulo reporte.
		/// </value>
		public string tituloReporte
		{
			get
			{
				if (Session["tituloReporte"] == null)
					Session["tituloReporte"] = string.Empty;
				return Session["tituloReporte"].ToString();
			}
			set { Session["tituloReporte"] = value; }
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
					return string.Empty;
				return Session["filtrosAplicados"].ToString();
			}
		}

		public bool verGrafico
		{
			get
			{
				if (ViewState["verGrafico"] == null)
					ViewState["verGrafico"] = false;
				return (bool)ViewState["verGrafico"];
			}
			set { ViewState["verGrafico"] = value; }
		}

		public bool verBotonGrafico
		{
			get
			{
				if (ViewState["verBotonGrafico"] == null)
					verBotonGrafico = true;
				return (bool)ViewState["verBotonGrafico"];
			}
			set { ViewState["verBotonGrafico"] = value; }
		}

		private string GridSampleSortDirection
		{
			get
			{
				if (Session["SortDirection"] == null)
					Session["SortDirection"] = "ASC";
				return Session["SortDirection"].ToString();
			}
			set { Session["SortDirection"] = value; }
		}

		public string GridSampleSortExpression
		{
			get
			{
				if (Session["SortExpression"] == null)
					Session["SortExpression"] = "fecha";
				return Session["SortExpression"].ToString();
			}
			set { Session["SortExpression"] = value; }
		}

		private string getSortDirection()
		{
			switch (GridSampleSortDirection)
			{
				case "ASC":
					GridSampleSortDirection = "DESC";
					break;

				case "DESC":
					GridSampleSortDirection = "ASC";
					break;
			}
			return GridSampleSortDirection;
		}
		#endregion

		#region --[Eventos]--
		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				btnPDF.Click += (ExportarPDF);
				btnVolver.Click += (Volver);
				btnGraficar.Click += (Graficar);
				gvwReporte.PageIndexChanging += (PaginandoGrilla);
				//gvwReporte.Sorting += (OrdenandoGrilla);

				if (!Page.IsPostBack)
				{
					grafico.habilitarTorta = true;
					tituloReporte = Page.Title;
					btnPDF.Visible = false;
					btnGraficar.Visible = false;
					btnVolver.Visible = false;
					btnImprimir.Visible = false;
				}
			}
			catch (Exception ex)
			{ throw ex; }
		}

		/// <summary>
		/// Handles the Click event of the btnImprimir control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnImprimir_Click(object sender, EventArgs e)
		{
			try
			{
				ScriptManager.RegisterStartupScript(Page, GetType(), "Imprimir", "AbrirPopup();", true);
			}
			catch (Exception ex)
			{ throw ex; }
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Cargars the reporte.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		public void CargarReporte<T>(List<T> lista)
		{
			try
			{
				if (lista.Count != 0)
				{
					GrillaReporte = UIUtilidades.GenerarGrilla(lista, GrillaReporte);
					//GrillaReporte.Sorting += (OrdenandoGrilla);
					btnVolver.Visible = true;
					btnPDF.Visible = true;
					btnGraficar.Visible = verBotonGrafico;
					btnImprimir.Visible = true;
					gvwReporte.Visible = true;
					lblSinDatos.Visible = false;
					CargarGrilla(lista);
				}
				else
				{
					//udpReporte.ContentTemplateContainer.Controls.Add(new LiteralControl("<h3>" + UIConstantesGenerales.MensajeSinResultados + "</h3>"));
					lblFiltros.Text = string.Empty;
					lblSinDatos.Visible = true;
					gvwReporte.Visible = false;
					btnVolver.Visible = true;
					btnPDF.Visible = false;
					btnGraficar.Visible = false;
					btnImprimir.Visible = false;
				}
				udpReporte.Update();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Exportars the PDF.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void ExportarPDF(object sender, EventArgs e)
		{
			OnExportarPDFClick(ExportarPDFClick, e);
		}

		/// <summary>
		/// Volvers the specified sender.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Volver(object sender, EventArgs e)
		{
			OnVolverClick(VolverClick, e);
		}

		/// <summary>
		/// Graficars the specified sender.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Graficar(object sender, EventArgs e)
		{
			verGrafico = true;
			graficoReporte.TablaGrafico.Clear();
			OnGraficarClick(GraficarClick, e);
			//divGrafico.Visible = true;
		}

		/// <summary>
		/// Paginandoes the grilla.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		void PaginandoGrilla(object sender, GridViewPageEventArgs e)
		{
			//GrillaReporte.DataSource = sortDataView(GrillaReporte.DataSource as DataView, true);
			//GrillaReporte.PageIndex = e.NewPageIndex;
			//GrillaReporte.DataBind();
			onPaginandoGrilla(PaginarGrilla, e);
		}

		//protected void OrdenandoGrilla(object sender, GridViewSortEventArgs e)
		//{
		//    GridSampleSortExpression = e.SortExpression;
		//    int pageIndex = GrillaReporte.PageIndex;
		//    GrillaReporte.DataSource = sortDataView(GrillaReporte.DataSource as DataView, false);
		//    GrillaReporte.DataBind();
		//    GrillaReporte.PageIndex = pageIndex;

		//    onOrdenandoGrilla(OrdenarGrilla, e);
		//}

		/// <summary>
		/// Cargars the grilla.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		private void CargarGrilla<T>(List<T> lista)
		{
			if (lista.Count > 0) lblFiltros.Text = filtrosAplicados.Replace("\n", "<br />");
			else lblFiltros.Text = string.Empty;
			dtReporte = UIUtilidades.BuildDataTable<T>(lista);
			//GrillaReporte.DataSource = sortDataView(dtReporte.DefaultView, true);
			GrillaReporte.DataSource = dtReporte.DefaultView;
			GrillaReporte.DataBind();
			udpReporte.Update();
		}

		//protected void gvwReporte_RowCreated(object sender, GridViewRowEventArgs e)
		//{
		//    GridView grd = (GridView)sender;

		//    if (e.Row.RowType == DataControlRowType.Header)
		//    {
		//        foreach (TableCell tc in e.Row.Cells)
		//        {
		//            if (tc.HasControls())
		//            {
		//                // Buscar el enlace de la cabecera
		//                LinkButton lnk = tc.Controls[0] as LinkButton;
		//                if (lnk != null && grd.SortExpression == lnk.CommandArgument)
		//                {
		//                    GridSampleSortExpression = grd.SortExpression;
		//                    // Verificar que se está ordenando por el campo indicado en el comando de ordenación
		//                    // Crear una imagen
		//                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
		//                    // Ajustar dinámicamente el icono adecuado
		//                    img.ImageUrl = "~/Images/view-sort-" + (grd.SortDirection == SortDirection.Ascending ? "ascending" : "descending") + ".png";
		//                    img.ImageAlign = ImageAlign.AbsMiddle;
		//                    // Le metemos un espacio delante de la imagen para que no se pegue al enlace
		//                    tc.Controls.Add(new LiteralControl(" "));
		//                    tc.Controls.Add(img);
		//                }
		//            }
		//        }
		//    }
		//}
		#endregion

		#region --[Delegados ]--

		public delegate void VentanaBotonClickHandler(object sender, EventArgs e);
		public delegate void PaginarGrillaHandler(object sender, GridViewPageEventArgs e);
		public delegate void OrdenarGrillaHandler(object sender, GridViewSortEventArgs e);

		public event VentanaBotonClickHandler ExportarPDFClick;
		public event VentanaBotonClickHandler VolverClick;
		public event VentanaBotonClickHandler GraficarClick;
		public event PaginarGrillaHandler PaginarGrilla;
		public event OrdenarGrillaHandler OrdenarGrilla;

		/// <summary>
		/// Called when [exportar PDF click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		public virtual void OnExportarPDFClick(VentanaBotonClickHandler sender, EventArgs e)
		{
			if (sender != null)
			{
				//Invoca el delegados
				sender(this, e);
			}
		}

		/// <summary>
		/// Called when [volver click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		public virtual void OnVolverClick(VentanaBotonClickHandler sender, EventArgs e)
		{
			if (sender != null)
			{
				//Invoca el delegados
				sender(this, e);
			}
		}

		/// <summary>
		/// Called when [graficar click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		public virtual void OnGraficarClick(VentanaBotonClickHandler sender, EventArgs e)
		{
			if (sender != null)
			{
				//Invoca el delegados
				sender(this, e);
			}
		}

		/// <summary>
		/// Ons the paginando grilla.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		public virtual void onPaginandoGrilla(PaginarGrillaHandler sender, GridViewPageEventArgs e)
		{
			if (sender != null)
			{
				//Invoca el delegados
				sender(this, e);
			}
		}

		public virtual void onOrdenandoGrilla(OrdenarGrillaHandler sender, GridViewSortEventArgs e)
		{
			if (sender != null)
			{
				//Invoca el delegados
				sender(this, e);
			}
		}
		#endregion
	}
}
