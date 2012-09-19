using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Common;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;

namespace EDUAR_UI.Private.Novedades
{
	public partial class RegNovedadAula : EDUARBasePage
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
		/// Gets or sets the lista estados novedad.
		/// </summary>
		/// <value>
		/// The lista estados novedad.
		/// </value>
		public List<EstadoNovedad> listaEstadosNovedad
		{
			get
			{
				if (ViewState["listaEstadosNovedad"] == null)
				{
					BLEstadoNovedad objBLEstadoNovedad = new BLEstadoNovedad();

					listaEstadosNovedad = objBLEstadoNovedad.GetEstadosNovedad(new EstadoNovedad());
				}
				return (List<EstadoNovedad>)ViewState["listaEstadosNovedad"];
			}
			set { ViewState["listaEstadosNovedad"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista tipos novedad.
		/// </summary>
		/// <value>
		/// The lista tipos novedad.
		/// </value>
		public List<TipoNovedad> listaTiposNovedad
		{
			get
			{
				if (ViewState["listaTiposNovedad"] == null)
				{
					BLTipoNovedad objBLTipoNovedad = new BLTipoNovedad();

					listaTiposNovedad = objBLTipoNovedad.GetTiposNovedad(new TipoNovedad());
				}
				return (List<TipoNovedad>)ViewState["listaTiposNovedad"];
			}
			set { ViewState["listaTiposNovedad"] = value; }
		}

		/// <summary>
		/// Gets or sets the id curso ciclo lectivo.
		/// </summary>
		/// <value>
		/// The id curso ciclo lectivo.
		/// </value>
		public int idCursoCicloLectivo
		{
			get
			{
				if (ViewState["idCursoCicloLectivo"] == null)
					ViewState["idCursoCicloLectivo"] = 0;
				return (int)ViewState["idCursoCicloLectivo"];
			}
			set
			{
				ViewState["idCursoCicloLectivo"] = value;
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
				//Master.BotonAvisoAceptar += (VentanaAceptar);
				if (!Page.IsPostBack)
				{
					UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "Nombre", true);
					UIUtilidades.BindCombo<EstadoNovedad>(ddlEstado, listaEstadosNovedad, "idEstadoNovedad", "nombre", true);
					UIUtilidades.BindCombo<TipoNovedad>(ddlNovedad, listaTiposNovedad, "idTipoNovedad", "nombre", true);
					udpNueva.Visible = false;
					//    CargarPresentacion();
					//    BuscarEventos(null);
				}
				//calfecha.startDate = cicloLectivoActual.fechaInicio;
				//calfecha.endDate = cicloLectivoActual.fechaFin;
				//calFechaEdit.startDate = cicloLectivoActual.fechaInicio;
				//calFechaEdit.endDate = cicloLectivoActual.fechaFin;
				this.txtObservaciones.Attributes.Add("onkeyup", " ValidarCaracteres(this, 1000);");
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
					this.idCursoCicloLectivo = idCursoCicloLectivo;
					//CargarIndicadores();
				}
				btnNuevo.Visible = idCursoCicloLectivo > 0;
				//divIndicadores.Visible = idCursoCicloLectivo > 0;
				//divNovedades.Visible = idCursoCicloLectivo > 0;
				//udpIndicadores.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
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
				udpNueva.Visible = true;
				udpNueva.Update();
				//AccionPagina = enumAcciones.Nuevo;
				//LimpiarCampos();
				//esNuevo = true;
				//btnGuardar.Visible = true;
				//btnBuscar.Visible = false;
				//btnVolver.Visible = true;
				//btnNuevo.Visible = false;
				//gvwReporte.Visible = false;
				//litEditar.Visible = false;
				//litNuevo.Visible = true;
				//udpEdit.Visible = true;
				//udpFiltrosBusqueda.Visible = false;
				//udpFiltros.Update();
				//udpGrilla.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				if (ValidarPagina())
				{
					Novedad objEntidad = new Novedad();
					objEntidad.fecha = DateTime.Now;
					objEntidad.usuario.username = HttpContext.Current.User.Identity.Name;
					objEntidad.tipo.idTipoNovedad = Convert.ToInt32(ddlNovedad.SelectedValue);
					objEntidad.estado.idEstadoNovedad = Convert.ToInt32(ddlEstado.SelectedValue);
					objEntidad.curso.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);
					objEntidad.observaciones = txtObservaciones.Text.Trim();

					GuardarNovedad(objEntidad);

					btnGuardar.Visible = false;
					udpNueva.Visible = false;
					udpNueva.Update();

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
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private bool ValidarPagina()
		{
			int validar = 0;
			int.TryParse(ddlCurso.SelectedValue, out validar);
			if (validar > 0)
			{
				int.TryParse(ddlEstado.SelectedValue, out validar);
				if (validar > 0)
				{
					int.TryParse(ddlNovedad.SelectedValue, out validar);
					return validar > 0;
				}
				return false;
			}
			else
				return false;
		}

		private void GuardarNovedad(Novedad objEntidad)
		{
			BLNovedad objBLNovedad = new BLNovedad(objEntidad);
			objBLNovedad.Save();
		}
		#endregion
	}
}