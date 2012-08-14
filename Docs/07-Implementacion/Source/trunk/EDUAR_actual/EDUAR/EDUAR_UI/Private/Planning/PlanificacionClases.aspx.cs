using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI
{
	public partial class PlanificacionClases : EDUARBasePage
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
		protected List<TemaContenido> listaContenido
		{
			get
			{
				if (ViewState["listaContenido"] == null)
					ViewState["listaContenido"] = new List<TemaContenido>();
				return (List<TemaContenido>)ViewState["listaContenido"];
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
				if (ViewState["idCurso"] == null)
					ViewState["idCurso"] = 0;
				return (int)ViewState["idCurso"];
			}
			set { ViewState["idCurso"] = value; }
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
				if (ViewState["idAsignaturaCurso"] == null)
					ViewState["idAsignaturaCurso"] = 0;
				return (int)ViewState["idAsignaturaCurso"];
			}
			set { ViewState["idAsignaturaCurso"] = value; }
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
				if (ViewState["planificacionEditar"] == null)
					ViewState["planificacionEditar"] = new PlanificacionAnual();
				return (PlanificacionAnual)ViewState["planificacionEditar"];
			}
			set { ViewState["planificacionEditar"] = value; }
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

		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
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
					ddlAsignatura.Enabled = false;
					ddlAsignatura.Items.Clear();
					ddlAsignatura.Items.Add("[Seleccione Curso]");
				}
				//divAprobacion.Visible = false;
				//gvwPlanificacion.DataSource = null;
				//gvwPlanificacion.DataBind();

				ddlAsignatura.Enabled = idCursoCicloLectivo > 0;
				//btnGuardar.Visible = false;
				//divControles.Visible = false;
				udpAsignatura.Update();
				//udpBotonera.Update();
				//udpDivControles.Update();
				//udpGrilla.Update();
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
				//btnNuevo.Visible = idAsignatura > 0;
				LimpiarCombos();
				if (idAsignatura > 0)
				{
					idTemaPlanificacion = 0;
					idAsignaturaCurso = idAsignatura;
					UIUtilidades.BindComboMeses(ddlMeses, false, DateTime.Now.Month);
					ddlMeses.SelectedValue = DateTime.Now.Month.ToString();
					ddlDia.Enabled = false;
					ddlMeses.Enabled = true;
					CargarContenidos();
					//BindComboModulos(DateTime.Now.Month);
					//ObtenerPlanificacion(idAsignatura);
					//if (planificacionEditar.fechaAprobada.HasValue) btnNuevo.Visible = false;
					//else btnNuevo.Visible = true;
					//btnPDF.Visible = planificacionEditar.listaTemasPlanificacion.Count > 0;
					//divControles.Visible = false;
					//udpDivControles.Update();
				}
				else
				{
					ddlMeses.Enabled = false;
					ddlDia.Enabled = false;
					//LimpiarCampos();
					//btnPDF.Visible = false;
					//divAprobacion.Visible = false;
					//chkAprobada.Enabled = false;
					//chkSolicitarAprobacion.Enabled = false;
					//chkAprobada.Checked = false;
					//chkSolicitarAprobacion.Checked = false;
					//gvwPlanificacion.DataSource = null;
					//gvwPlanificacion.DataBind();
					//udpGrilla.Update();
				}
				//udpBotonera.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the ddlMeses control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ddlMeses_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int mes = 0;
				int.TryParse(ddlMeses.SelectedValue, out mes);
				if (mes > 0)
				{
					ddlDia.Enabled = true;
					BindComboModulos(mes);
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
			if (idCurso > 0)
			{
				ddlCurso.SelectedValue = idCurso.ToString();
				CargarComboAsignatura(idCurso);
				if (idAsignaturaCurso > 0)
					ddlAsignatura.SelectedValue = idAsignaturaCurso.ToString();
				ddlAsignatura.Enabled = true;
			}
			else
			{
				ddlAsignatura.Enabled = false;
				ddlAsignatura.Items.Clear();
				ddlAsignatura.Items.Add("[Seleccione Curso]");
			}
			//divFiltros.Visible = true;
			//divControles.Visible = false;
			//btnVolver.Visible = false;
			//btnGuardar.Visible = false;
			//divFiltros.Visible = true;
			ddlMeses.Enabled = false;
			ddlDia.Enabled = false;
			ddlCurso.Enabled = true;
			LimpiarCombos();
			//udpBotonera.Update();
			//udpDivControles.Update();
			//udpGrilla.Update();
		}

		/// <summary>
		/// Cargars the contenidos.
		/// </summary>
		private void CargarContenidos()
		{
			TemasContenido listaTemas = new TemasContenido();
			BLTemaContenido objBLTemas = new BLTemaContenido();
			AsignaturaCicloLectivo objAsignatura = new AsignaturaCicloLectivo();
			objAsignatura.cursoCicloLectivo.curso.idCurso = idCurso;
			objAsignatura.idAsignaturaCicloLectivo = idAsignaturaCurso;
			objAsignatura.cursoCicloLectivo.cicloLectivo = base.cicloLectivoActual;
			listaContenido = objBLTemas.GetTemasByCursoAsignatura(objAsignatura);
			
			ltvContenidos.DataSource = listaContenido;
			ltvContenidos.DataBind();
			udpContenidos.Update();
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

		/// <summary>
		/// Binds the combo modulos.
		/// </summary>
		/// <param name="mes">The mes.</param>
		private void BindComboModulos(int mes)
		{
			BLDiasHorarios objBLHorario = new BLDiasHorarios();
			List<DiasHorarios> listaHorario = new List<DiasHorarios>();
			DiasHorarios objDiaHorario = new DiasHorarios();
			if (esNuevo)
				objDiaHorario.idAsignaturaCurso = Convert.ToInt32(ddlAsignatura.SelectedValue);
			else
				objDiaHorario.idAsignaturaCurso = idAsignaturaCurso;
			CursoCicloLectivo objCursoCicloLectivo = new CursoCicloLectivo();
			objCursoCicloLectivo.cicloLectivo = base.cicloLectivoActual;
			objCursoCicloLectivo.idCursoCicloLectivo = idCurso;
			listaHorario = objBLHorario.GetHorariosCurso(objDiaHorario, objCursoCicloLectivo);
			int anio = base.cicloLectivoActual.fechaInicio.Year;
			int cantDias = DateTime.DaysInMonth(anio, mes);
			cantDias++;
			DateTime fecha = new DateTime(anio, mes, 1);
			ddlDia.Items.Clear();
			foreach (DiasHorarios item in listaHorario)
			{
				if ((int)item.unDia >= (int)enumDiasSemana.Lunes && (int)item.unDia <= (int)enumDiasSemana.Viernes)
				{
					for (int i = 1; i < cantDias; i++)
					{
						fecha = new DateTime(anio, mes, i);
						if ((int)fecha.Date.DayOfWeek == (int)item.unDia && !ddlDia.Items.Contains(ddlDia.Items.FindByValue(i.ToString())))
						{
							ddlDia.Items.Add(new ListItem(item.unDia + " " + i.ToString(), i.ToString()));
						}
					}
				}
			}
			UIUtilidades.SortByValue(ddlDia);
			udpMeses.Update();
		}

		/// <summary>
		/// Limpiars the combos.
		/// </summary>
		private void LimpiarCombos()
		{
			ddlDia.Items.Clear();
			ddlMeses.Items.Clear();
			ddlDia.Items.Add("[Seleccione Mes]");
			ddlMeses.Items.Add("[Seleccione Asignatura]");
		}
		#endregion
	}
}
