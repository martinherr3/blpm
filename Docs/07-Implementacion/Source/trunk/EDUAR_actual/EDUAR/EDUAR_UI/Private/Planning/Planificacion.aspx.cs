using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
	public partial class Planificacion : EDUARBasePage
	{
		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the lista cursos.
		/// </summary>
		/// <value>
		/// The lista cursos.
		/// </value>
		public List<Curso> listaCursos
		{
			get
			{
				if (ViewState["listaCursos"] == null && cicloLectivoActual != null)
				{
					BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();

					Asignatura objFiltro = new Asignatura();
					objFiltro.curso.cicloLectivo = cicloLectivoActual;
					if (User.IsInRole(enumRoles.Docente.ToString()))
						//nombre del usuario logueado
						objFiltro.docente.username = User.Identity.Name;
					listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
				}
				return (List<Curso>)ViewState["listaCursos"];
			}
			set { ViewState["listaCursos"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista contenido.
		/// </summary>
		/// <value>
		/// The lista contenido.
		/// </value>
		protected List<EDUAR_Entities.Contenido> listaContenido
		{
			get
			{
				if (ViewState["listaContenido"] == null)
					ViewState["listaContenido"] = new List<EDUAR_Entities.Contenido>();
				return (List<EDUAR_Entities.Contenido>)ViewState["listaContenido"];
			}
			set { ViewState["listaContenido"] = value; }
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
				if (Session["idCurso"] == null)
					Session["idCurso"] = 0;
				return (int)Session["idCurso"];
			}
			set { Session["idCurso"] = value; }
		}

		/// <summary>
		/// Gets or sets the id asignatura curso.
		/// </summary>
		/// <value>
		/// The id asignatura curso.
		/// </value>
		public int idAsignaturaCurso
		{
			get
			{
				if (Session["idAsignaturaCurso"] == null)
					Session["idAsignaturaCurso"] = 0;
				return (int)Session["idAsignaturaCurso"];
			}
			set { Session["idAsignaturaCurso"] = value; }
		}

		/// <summary>
		/// Gets or sets the id planificacion anual.
		/// </summary>
		/// <value>
		/// The id planificacion anual.
		/// </value>
		public int idPlanificacionAnual
		{
			get
			{
				if (ViewState["idPlanificacionAnual"] == null)
					ViewState["idPlanificacionAnual"] = 0;
				return (int)ViewState["idPlanificacionAnual"];
			}
			set { ViewState["idPlanificacionAnual"] = value; }
		}

		/// <summary>
		/// Gets or sets the planificacion editar.
		/// </summary>
		/// <value>
		/// The planificacion editar.
		/// </value>
		public PlanificacionAnual planificacionEditar
		{
			get
			{
				if (Session["planificacionEditar"] == null)
					Session["planificacionEditar"] = new PlanificacionAnual();
				return (PlanificacionAnual)Session["planificacionEditar"];
			}
			set { Session["planificacionEditar"] = value; }
		}

		/// <summary>
		/// Gets or sets the id tema planificacion.
		/// </summary>
		/// <value>
		/// The id tema planificacion.
		/// </value>
		public int idTemaPlanificacion
		{
			get
			{
				if (ViewState["idTemaPlanificacion"] == null)
					ViewState["idTemaPlanificacion"] = 0;
				return (int)ViewState["idTemaPlanificacion"];
			}
			set { ViewState["idTemaPlanificacion"] = value; }
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

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				//Master.BotonAvisoAceptar += (VentanaAceptar);

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
		/// Handles the Click event of the btnNuevo control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			try
			{
				btnGuardar.Visible = true;
				btnVolver.Visible = true;
				btnNuevo.Visible = false;
				gvwPlanificacion.Visible = false;
				divControles.Visible = true;
				udpGrilla.Update();
				udpDivControles.Update();
				udpBotonera.Update();
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the Click event of the btnGuardar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				TemaPlanificacionAnual objTema = new TemaPlanificacionAnual();
				objTema.contenidosActitudinales = txtCActitudinales.Text.Trim();
				objTema.contenidosConceptuales = txtCConceptuales.Text.Trim();
				objTema.contenidosProcedimentales = txtCProcedimentales.Text.Trim();
				objTema.criteriosEvaluacion = txtCriteriosEvaluacion.Text.Trim();
				objTema.estrategiasAprendizaje = txtEstrategias.Text.Trim();
				objTema.fechaFinEstimada = calFechaFin.ValorFecha;
				objTema.fechaInicioEstimada = calFechaDesde.ValorFecha;
				objTema.instrumentosEvaluacion = txtInstrumentosEvaluacion.Text.Trim();
				if (idTemaPlanificacion > 0)
					objTema.idTemaPlanificacion = idTemaPlanificacion;

				PlanificacionAnual objPlanificacion = new PlanificacionAnual();
				objPlanificacion.creador.username = (string.IsNullOrEmpty(planificacionEditar.creador.username)) ? User.Identity.Name : planificacionEditar.creador.username;
				objPlanificacion.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignaturaCurso;
				//objPlanificacion.asignaturaCicloLectivo.idAsignaturaCicloLectivo = planificacionEditar.asignaturaCicloLectivo.idAsignaturaCicloLectivo;
				objPlanificacion.idPlanificacionAnual = planificacionEditar.idPlanificacionAnual;
				objPlanificacion.listaTemasPlanificacion.Add(objTema);
				BLPlanificacionAnual objPlanificacionBL = new BLPlanificacionAnual(objPlanificacion);
				objPlanificacionBL.Save();
				idTemaPlanificacion = 0;
				udpBotonera.Update();
				CargarPresentacion();
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the Click event of the btnVolver control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnVolver_Click(object sender, EventArgs e)
		{
			try
			{
				idTemaPlanificacion = 0;
				ddlCurso.SelectedValue = idCurso.ToString();
				CargarComboAsignatura(idCurso);
				ddlAsignatura.SelectedValue = idAsignaturaCurso.ToString();
				CargarPresentacion();
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
				int idCursoCicloLectivo = 0;
				int.TryParse(ddlCurso.SelectedValue, out idCursoCicloLectivo);
				if (idCursoCicloLectivo > 0)
				{
					idCurso = idCursoCicloLectivo;
					CargarComboAsignatura(idCursoCicloLectivo);
				}
				else
				{
					ddlAsignatura.SelectedIndex = 0;
					ddlAsignatura.Items.Clear();
				}
				ddlAsignatura.Enabled = idCursoCicloLectivo > 0;
				btnGuardar.Visible = false;
				divControles.Visible = false;
				udpAsignatura.Update();
				udpBotonera.Update();
				udpDivControles.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the ddlAsignatura control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ddlAsignatura_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int idAsignatura = 0;
				int.TryParse(ddlAsignatura.SelectedValue, out idAsignatura);
				btnNuevo.Visible = idAsignatura > 0;
				if (idAsignatura > 0)
				{
					idTemaPlanificacion = 0;
					idAsignaturaCurso = idAsignatura;
					ObtenerPlanificacion(idAsignatura);
					btnNuevo.Visible = true;
					divControles.Visible = false;
					udpDivControles.Update();
					udpBotonera.Update();
				}
				udpBotonera.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		private void ObtenerPlanificacion(int idAsignatura)
		{
			BLPlanificacionAnual objBLPlanificacion = new BLPlanificacionAnual();
			planificacionEditar = objBLPlanificacion.GetPlanificacionByAsignatura(idAsignatura);
			gvwPlanificacion.DataSource = planificacionEditar.listaTemasPlanificacion;
			gvwPlanificacion.DataBind();
			udpGrilla.Update();
		}

		protected void gvwPlanificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}

		/// <summary>
		/// Handles the RowCommand event of the gvwPlanificacion control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
		protected void gvwPlanificacion_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Editar":
						idTemaPlanificacion = Convert.ToInt32(e.CommandArgument.ToString());
						//lblTitulo.Text = "Editar Contenido";
						//idContenido = Convert.ToInt32(e.CommandArgument.ToString());
						var lista = planificacionEditar.listaTemasPlanificacion.Find(p => p.idTemaPlanificacion == idTemaPlanificacion);
						txtCActitudinales.Text = lista.contenidosActitudinales;
						txtCConceptuales.Text = lista.contenidosConceptuales;
						txtCProcedimentales.Text = lista.contenidosProcedimentales;
						txtCriteriosEvaluacion.Text = lista.criteriosEvaluacion;
						txtEstrategias.Text = lista.estrategiasAprendizaje;
						txtInstrumentosEvaluacion.Text = lista.instrumentosEvaluacion;
						calFechaDesde.Fecha.Text = lista.fechaInicioEstimada.ToString();
						calFechaFin.Fecha.Text = lista.fechaFinEstimada.ToString();
						divControles.Visible = true;
						divFiltros.Visible = false;
						btnGuardar.Visible = true;
						btnNuevo.Visible = false;
						btnVolver.Visible = true;
						udpBotonera.Update();
						udpGrilla.Update();
						udpDivControles.Update();
					//txtDescripcion.Text = lista.descripcion;
						//udpBotonera.Update();
						//mpeContenido.Show();
						break;
					case "Eliminar":
						//AccionPagina = enumAcciones.Eliminar;
						//idContenido = Convert.ToInt32(e.CommandArgument.ToString());
						//EliminarContenido();
						break;
					case "Temas":
						//AccionPagina = enumAcciones.Redirect;
						//idContenido = Convert.ToInt32(e.CommandArgument.ToString());
						//contenidoEditar = listaContenido.Find(p => p.idContenido == idContenido);
						//Response.Redirect("TemasContenido.aspx", false);
						break;
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
			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "Nombre", true);
			divFiltros.Visible = true;
			divControles.Visible = false;
			btnNuevo.Visible = false;
			btnVolver.Visible = false;
			btnGuardar.Visible = false;
			divFiltros.Visible = true;
			udpBotonera.Update();
			udpDivControles.Update();
			udpGrilla.Update();
		}

		/// <summary>
		/// Cargars the asignaturas.
		/// </summary>
		private void CargarComboAsignatura(int idCursoCicloLectivo)
		{
			List<Asignatura> listaAsignaturas = new List<Asignatura>();
			BLAsignatura objBLAsignatura = new BLAsignatura();
			CursoCicloLectivo curso = new CursoCicloLectivo();
			Docente docente = null;
			if (User.IsInRole(enumRoles.Docente.ToString()))
				docente.username = User.Identity.Name;
			curso.idCursoCicloLectivo = idCursoCicloLectivo;
			listaAsignaturas = objBLAsignatura.GetAsignaturasCurso(new Asignatura() { cursoCicloLectivo = curso, docente = docente });
			if (listaAsignaturas != null && listaAsignaturas.Count > 0)
				UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, listaAsignaturas, "idAsignatura", "Nombre", true);
		}
		#endregion
	}
}