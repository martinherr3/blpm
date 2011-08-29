using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

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

				if (!Page.IsPostBack)
				{
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
					btnVolver.Visible = true;
					btnPDF.Visible = true;
					btnGraficar.Visible = true;
					btnImprimir.Visible = true;
					gvwReporte.Visible = true;
					lblSinDatos.Visible = false;
					CargarGrilla(lista);
				}
				else
				{
					//udpReporte.ContentTemplateContainer.Controls.Add(new LiteralControl("<h3>" + UIConstantesGenerales.MensajeSinResultados + "</h3>"));
					lblSinDatos.Visible = true;
					gvwReporte.Visible = false;
					btnVolver.Visible = true;
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
			onPaginandoGrilla(PaginarGrilla, e);
		}

		/// <summary>
		/// Cargars the grilla.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		private void CargarGrilla<T>(List<T> lista)
		{
			dtReporte = UIUtilidades.BuildDataTable<T>(lista);
			GrillaReporte.DataSource = dtReporte.DefaultView;
			GrillaReporte.DataBind();
			udpReporte.Update();
		}
		#endregion

		#region --[Delegados ]--

		public delegate void VentanaBotonClickHandler(object sender, EventArgs e);
		public delegate void PaginarGrillaHandler(object sender, GridViewPageEventArgs e);

		public event VentanaBotonClickHandler ExportarPDFClick;
		public event VentanaBotonClickHandler VolverClick;
		public event VentanaBotonClickHandler GraficarClick;
		public event PaginarGrillaHandler PaginarGrilla;

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
		#endregion
	}
}
