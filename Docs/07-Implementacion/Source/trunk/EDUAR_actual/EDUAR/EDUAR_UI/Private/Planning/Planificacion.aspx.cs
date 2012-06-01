﻿using System;
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
		/// Gets or sets the id contenido.
		/// </summary>
		/// <value>
		/// The id contenido.
		/// </value>
		public int idContenido
		{
			get
			{
				if (ViewState["idContenido"] == null)
					ViewState["idContenido"] = 0;
				return (int)ViewState["idContenido"];
			}
			set { ViewState["idContenido"] = value; }
		}

		/// <summary>
		/// Gets or sets the contenido editar.
		/// </summary>
		/// <value>
		/// The contenido editar.
		/// </value>
		public EDUAR_Entities.Contenido contenidoEditar
		{
			get
			{
				if (Session["contenidoEditar"] == null)
					Session["contenidoEditar"] = new EDUAR_Entities.Contenido();
				return (EDUAR_Entities.Contenido)Session["contenidoEditar"];
			}
			set { Session["contenidoEditar"] = value; }
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
		/// Handles the Click event of the btnBuscar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			try
			{

			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
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
				//pnlNuevoContenido.Attributes["display"] = "inherit";
				//pnlNuevoContenido.Visible = true;
				udpBotonera.Update();
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
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
				divControles.Visible = false;
				udpAsignatura.Update();
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
					idAsignaturaCurso = idAsignatura;
					//CargarContenido(idAsignatura);
					divControles.Visible = true;
					udpDivControles.Update();
				}
				udpBotonera.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		protected void gvwPlanificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}

		protected void gvwPlanificacion_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				//switch (e.CommandName)
				//{
					//case "Editar":
					//    lblTitulo.Text = "Editar Contenido";
					//    idContenido = Convert.ToInt32(e.CommandArgument.ToString());
					//    var lista = listaContenido.Find(p => p.idContenido == idContenido);
					//    txtDescripcion.Text = lista.descripcion;
					//    udpBotonera.Update();
					//    mpeContenido.Show();
					//    break;
					//case "Eliminar":
					//    AccionPagina = enumAcciones.Eliminar;
					//    idContenido = Convert.ToInt32(e.CommandArgument.ToString());
					//    EliminarContenido();
					//    break;
					//case "Temas":
					//    AccionPagina = enumAcciones.Redirect;
					//    idContenido = Convert.ToInt32(e.CommandArgument.ToString());
					//    contenidoEditar = listaContenido.Find(p => p.idContenido == idContenido);
					//    Response.Redirect("TemasContenido.aspx", false);
					//    break;
				//}
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
			//pnlNuevoContenido.Attributes["display"] = "none";
			//pnlNuevoContenido.Visible = false;
			divControles.Visible = false;
			udpBotonera.Update();
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