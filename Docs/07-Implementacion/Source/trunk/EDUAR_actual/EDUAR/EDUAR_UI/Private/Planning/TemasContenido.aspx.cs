using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Common;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
	public partial class TemasContenido : EDUARBasePage
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
		/// Gets or sets the id contenido.
		/// </summary>
		/// <value>
		/// The id contenido.
		/// </value>
		public int idContenido
		{
			get
			{
				if (Session["idContenido"] == null)
					Session["idContenido"] = 0;
				return (int)Session["idContenido"];
			}
			set { Session["idContenido"] = value; }
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
				Master.BotonAvisoAceptar += (VentanaAceptar);

				if (!Page.IsPostBack)
				{
					CargarPresentacion();
				}
				else
				{
					if (Request.Form["__EVENTTARGET"] == "btnGuardar")
						//llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Button2
						btnGuardar_Click(this, new EventArgs());
				}
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
			try
			{

			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
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

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			try
			{
				pnlNuevoContenido.Attributes["display"] = "inherit";
				//pnlNuevoContenido.Visible = true;
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
				if (!string.IsNullOrEmpty(txtDescripcion.Text))
				{
					EDUAR_Entities.Contenido nuevoContenido = new EDUAR_Entities.Contenido();
					nuevoContenido.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignaturaCurso;
					nuevoContenido.descripcion = txtDescripcion.Text;
					nuevoContenido.idContenido = idContenido;

					GuardarContenido(nuevoContenido);
				}
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
					CargarComboAsignatura(idCursoCicloLectivo);
				else
				{
					ddlAsignatura.SelectedIndex = 0;
					ddlAsignatura.Items.Clear();
				}
				ddlAsignatura.Enabled = idCursoCicloLectivo > 0;
				udpAsignatura.Update();
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
					CargarContenido(idAsignatura);
				}
				udpBotonera.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		private void CargarContenido(int idAsignaturaCicloLectivo)
		{
			EDUAR_Entities.Contenido objContenido = new EDUAR_Entities.Contenido();
			objContenido.asignaturaCicloLectivo.idAsignaturaCicloLectivo = idAsignaturaCicloLectivo;
			BLContenido objBL = new BLContenido();
			listaContenido = objBL.GetByAsignaturaCicloLectivo(objContenido);
			CargarGrilla();
		}

		protected void gvwContenido_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}

		protected void gvwContenido_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Editar":
						lblTitulo.Text = "Editar Contenido";
						idContenido = Convert.ToInt32(e.CommandArgument.ToString());
						var lista = listaContenido.Find(p => p.idContenido == idContenido);
						txtDescripcion.Text = lista.descripcion;
						udpBotonera.Update();
						mpeContenido.Show();
						break;
					case "Eliminar":
						AccionPagina = enumAcciones.Eliminar;
						idContenido = Convert.ToInt32(e.CommandArgument.ToString());
						EliminarContenido();
						break;
					case "Temas":
						AccionPagina = enumAcciones.Redirect;
						Response.Redirect("TemasContenido.aspx", false);
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
			pnlNuevoContenido.Attributes["display"] = "none";
			//pnlNuevoContenido.Visible = false;

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
			curso.idCursoCicloLectivo = idCursoCicloLectivo;
			listaAsignaturas = objBLAsignatura.GetAsignaturasCurso(new Asignatura() { cursoCicloLectivo = curso });
			if (listaAsignaturas != null && listaAsignaturas.Count > 0)
				UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, listaAsignaturas, "idAsignatura", "Nombre", true);
		}

		/// <summary>
		/// Cargars the grilla.
		/// </summary>
		private void CargarGrilla()
		{
			gvwContenido.DataSource = UIUtilidades.BuildDataTable<EDUAR_Entities.Contenido>(listaContenido).DefaultView;
			gvwContenido.DataBind();
			udpGrilla.Update();
		}

		/// <summary>
		/// Guardars the contenido.
		/// </summary>
		/// <param name="nuevoContenido">The nuevo contenido.</param>
		private void GuardarContenido(EDUAR_Entities.Contenido nuevoContenido)
		{
			BLContenido objBLContenido = new BLContenido(nuevoContenido);
			objBLContenido.Save();

			lblTitulo.Text = "Nuevo Contenido";
			txtDescripcion.Text = string.Empty;
			idContenido = 0;
			pnlNuevoContenido.Attributes["display"] = "none";
			udpBotonera.Update();
			CargarContenido(idAsignaturaCurso);
		}

		/// <summary>
		/// Eliminars the contenido.
		/// </summary>
		private void EliminarContenido()
		{
			EDUAR_Entities.Contenido objEliminar = new EDUAR_Entities.Contenido();
			objEliminar.idContenido = idContenido;
			BLContenido ojbBLContenido = new BLContenido(objEliminar);
			ojbBLContenido.Delete();

			CargarContenido(idAsignaturaCurso);
		}
		#endregion
	}
}