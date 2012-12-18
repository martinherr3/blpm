using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI.UserControls
{
	public partial class Cursos : System.Web.UI.UserControl
	{
		#region --[Atributos]--
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the titulo.
		/// </summary>
		/// <value>
		/// The titulo.
		/// </value>
		public string titulo
		{
			get
			{
				return this.lblTitulo.Text;
			}
			set
			{
				this.lblTitulo.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the id curso.
		/// </summary>
		/// <value>
		/// The id curso.
		/// </value>
		public int idCurso
		{
			get
			{
				if (ViewState[this.UniqueID + "_idCurso"] == null)
					ViewState[this.UniqueID + "_idCurso"] = 0;

				return (int)ViewState[this.UniqueID + "_idCurso"];
			}
			set { ViewState[this.UniqueID + "_idCurso"] = value; }
		}

		/// <summary>
		/// Gets or sets the id id Curso Ciclo Lectivo.
		/// </summary>
		/// <value>
		/// The id curso Ciclo Lectivo.
		/// </value>
		public int idCursoCicloLectivo
		{
			get
			{
				if (Session["idCursoCicloLectivo"] == null)
					Session["idCursoCicloLectivo"] = 0;
				return (int)Session["idCursoCicloLectivo"];
			}
			set {
				if (value > 0)
				{
					BLCurso objBLCurso = new BLCurso();
					cursoActual.idCursoCicloLectivo = value;
					cursoActual = objBLCurso.GetCursoCicloLectivo(cursoActual);
				}
				Session["idCursoCicloLectivo"] = value; }
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

		protected void Page_Load(object sender, EventArgs e)
		{
			if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()))
			{
				btnIndicador.Enabled = false;
				btnIndicador.Visible = false;
			}
		}

		protected void btnActividad_Click(object sender, EventArgs e)
		{
			idCursoCicloLectivo = this.idCurso;
			Response.Redirect("~/Private/Agenda/ManageAgendaActividades.aspx", false);
		}

		protected void btnPlanficacion_Click(object sender, EventArgs e)
		{
			idCursoCicloLectivo = this.idCurso;
			Response.Redirect("~/Private/Planning/PlanificacionAnual.aspx", false);
		}

		protected void btnNotificacion_Click(object sender, EventArgs e)
		{
			idCursoCicloLectivo = this.idCurso;
			Response.Redirect("~/Private/Novedades/ConsultaNovedadAulica.aspx", false);
		}

		protected void btnIndicadorClick(object sender, EventArgs e)
		{
			idCursoCicloLectivo = this.idCurso;
			Response.Redirect("~/Private/Monitoreo/GetIndicadores.aspx", false);
		}
	}
}