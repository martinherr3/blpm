using System;
using System.Data;
using System.Web;
using System.Web.UI;
using EDUAR_UI.Utilidades;
using System.Text;
using EDUAR_Entities;

namespace EDUAR_UI.UserControls
{
	public partial class Exportador : UserControl
	{
		#region --[Atributos]--
		private string _Titulo;
		private DataTable _Datos;
		#endregion

		#region --[Propiedades]--
		public DataTable Datos
		{
			get
			{
				if (_Datos == null)
					if (ViewState["_Datos"] != null)
						_Datos = (DataTable)ViewState["_Datos"];
				return _Datos;
			}
			set { ViewState["_Datos"] = value; _Datos = value; }
		}

		public string Titulo
		{
			get
			{
				if (String.IsNullOrEmpty(_Titulo))
					if (ViewState["_Titulo"] != null)
						_Titulo = ViewState["_Titulo"].ToString();
				return _Titulo;
			}
			set
			{
				ViewState["_Titulo"] = value;
				_Titulo = value;
			}
		}

		public DateTime Desde
		{
			get
			{
				if (Session["Fecha_Desde_Indicador"] != null)
					return Convert.ToDateTime(Session["Fecha_Desde_Indicador"].ToString());
				return DateTime.MinValue;
			}
			set { Session["Fecha_Desde_Indicador"] = value; }
		}

		public DateTime Hasta
		{
			get
			{
				if (Session["Fecha_Hasta_Indicador"] != null)
					return Convert.ToDateTime(Session["Fecha_Hasta_Indicador"].ToString());
				return DateTime.MinValue;
			}
			set { Session["Fecha_Hasta_Indicador"] = value; }
		}

		/// <summary>
		/// Gets or sets the curso actual.
		/// </summary>
		/// <value>
		/// The curso actual.
		/// </value>
		public CursoCicloLectivo cursoActual
		{
			get
			{
				if (Session["cursoActual"] == null)
					Session["cursoActual"] = new CursoCicloLectivo();
				return (CursoCicloLectivo)Session["cursoActual"];
			}
			set { Session["cursoActual"] = value; }
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

		}

		/// <summary>
		/// Handles the Click event of the btnExportar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnExportar_Click(object sender, EventArgs e)
		{
			StringBuilder filtros = new StringBuilder();
			filtros.AppendLine("- Curso: " + cursoActual.curso.nombre);

			if (Desde > DateTime.MinValue)
				filtros.AppendLine("- Fecha Desde: " + Desde.ToShortDateString());
			if (Hasta > DateTime.MinValue)
				filtros.AppendLine("- Fecha Hasta: " + Hasta.ToShortDateString());

			if (Datos != null && !String.IsNullOrEmpty(Titulo))
				ExportPDF.ExportarPDF(Titulo, Datos, HttpContext.Current.User.Identity.Name, filtros.ToString());
		}
		#endregion

		#region --[Métodos Públicos]--
		public void Clean()
		{
			Titulo = "";
			Datos = null;
		}
		#endregion
	}
}