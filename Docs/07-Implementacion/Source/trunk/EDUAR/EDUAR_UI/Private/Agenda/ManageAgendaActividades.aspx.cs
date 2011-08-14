using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
	public partial class ManageAgendaActividades : EDUARBasePage
	{
		#region --[Atributos]--
		private BLAgendaActividades objBLAgenda;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Mantiene la agenda seleccionada en la grilla.
		/// Se utiliza para el manejo de eventos de agenda (evaluación, excursión, reunión).
		/// </summary>
		/// <value>
		/// The prop agenda.
		/// </value>
		public AgendaActividades propAgenda
		{
			get
			{
				if (Session["propAgenda"] == null)
					Session["propAgenda"] = new AgendaActividades();

				return (AgendaActividades)Session["propAgenda"];
			}
			set { Session["propAgenda"] = value; }
		}

		/// <summary>
		/// Gets or sets the prop filtro agenda.
		/// </summary>
		/// <value>
		/// The prop filtro agenda.
		/// </value>
		public AgendaActividades propFiltroAgenda
		{
			get
			{
				if (ViewState["propFiltroAgenda"] == null)
					return new AgendaActividades();

				return (AgendaActividades)ViewState["propFiltroAgenda"];
			}
			set { ViewState["propFiltroAgenda"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista agenda.
		/// </summary>
		/// <value>
		/// The lista agenda.
		/// </value>
		public List<AgendaActividades> listaAgenda
		{
			get
			{
				if (ViewState["listaAgenda"] == null)
					return new List<AgendaActividades>();

				return (List<AgendaActividades>)ViewState["listaAgenda"];
			}
			set { ViewState["listaAgenda"] = value; }
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
					if (propAgenda.idAgendaActividad > 0)
					{
						CargarLista(propAgenda);
						CargaAgenda();
					}
					else
						BuscarAgenda(null);
				}
				this.txtDescripcionEdit.Attributes.Add("onkeyup", " ValidarCaracteres(this, 4000);");
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
		void VentanaAceptar(object sender, EventArgs e)
		{
			try
			{
				switch (AccionPagina)
				{
					case enumAcciones.Buscar:
						break;
					case enumAcciones.Nuevo:
						break;
					case enumAcciones.Modificar:
						break;
					case enumAcciones.Eliminar:
						break;
					case enumAcciones.Seleccionar:
						break;
					case enumAcciones.Limpiar:
						CargarPresentacion();
						BuscarAgenda(null);
						break;
					case enumAcciones.Aceptar:
						break;
					case enumAcciones.Salir:
						break;
					case enumAcciones.Redirect:
						break;
					case enumAcciones.Guardar:
						AccionPagina = enumAcciones.Limpiar;
						GuardarAgenda(ObtenerValoresDePantalla());
						Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
						break;
					case enumAcciones.Ingresar:
						break;
					case enumAcciones.Desbloquear:
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnEvaluacion control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnEvaluacion_Click(object sender, EventArgs e)
		{
			try
			{
				//idAgenda = propAgenda.idAgendaActividad;
				Response.Redirect("ManageEvaluaciones.aspx", false);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnExcursion control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnExcursion_Click(object sender, EventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnReunion control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnReunion_Click(object sender, EventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
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
				BuscarFiltrando();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// DESACTIVADO!!!!!!
		/// Handles the Click event of the btnNuevo control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			try
			{
				AccionPagina = enumAcciones.Nuevo;
				LimpiarCampos();
				CargarCombos(ddlCicloLectivoEdit, ddlCursoEdit);
				esNuevo = true;
				btnGuardar.Visible = true;
				btnBuscar.Visible = false;
				btnVolver.Visible = true;
				//btnNuevo.Visible = false;
				gvwReporte.Visible = false;
				litEditar.Visible = false;
				litNuevo.Visible = true;
				udpEdit.Visible = true;
				udpFiltrosBusqueda.Visible = false;
				udpFiltros.Update();
				udpGrilla.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnAsignarRol control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				string mensaje = ValidarPagina();
				if (mensaje == string.Empty)
				{
					if (Page.IsValid)
					{
						AccionPagina = enumAcciones.Guardar;
						Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
					}
				}
				else
				{
					Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
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
				CargarPresentacion();
				BuscarAgenda(propFiltroAgenda);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Método que se llama al hacer click sobre las acciones de la grilla
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
		protected void gvwReporte_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Editar":
						propAgenda.idAgendaActividad = Convert.ToInt32(e.CommandArgument.ToString());
						CargaAgenda();
						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the ddlCicloLectivo control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ddlCicloLectivo_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
				CargarComboCursos(idCicloLectivo, ddlCurso);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		protected void ddlCicloLectivoEdit_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int idCicloLectivo = Convert.ToInt32(ddlCicloLectivoEdit.SelectedValue);
				CargarComboCursos(idCicloLectivo, ddlCursoEdit);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the grilla.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		private void CargarGrilla()
		{
			gvwReporte.DataSource = UIUtilidades.BuildDataTable<AgendaActividades>(listaAgenda).DefaultView;
			gvwReporte.DataBind();
			udpEdit.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			LimpiarCampos();
			CargarCombos(ddlCicloLectivo, ddlCurso);
			udpEdit.Visible = false;
			btnVolver.Visible = false;
			btnGuardar.Visible = false;
			udpFiltrosBusqueda.Visible = true;
			//btnNuevo.Visible = true;
			btnBuscar.Visible = true;
			gvwReporte.Visible = true;
			udpFiltros.Update();
			udpGrilla.Update();
		}

		private void HabilitarBotonesDetalle(bool habilitar)
		{
			btnExcursion.Visible = habilitar;
			btnEvaluacion.Visible = habilitar;
			btnReunion.Visible = habilitar;
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		private void CargarCombos(DropDownList ddlCiclo, DropDownList ddlCurso)
		{
			List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
			BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
			listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);

			List<Curso> listaCurso = new List<Curso>();
			UIUtilidades.BindCombo<CicloLectivo>(ddlCiclo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);

			ddlCurso.Enabled = false;
		}

		/// <summary>
		/// Cargars the combo cursos.
		/// </summary>
		/// <param name="idCicloLectivo">The id ciclo lectivo.</param>
		/// <param name="ddlCurso">The DDL curso.</param>
		private void CargarComboCursos(int idCicloLectivo, DropDownList ddlCurso)
		{
			if (idCicloLectivo > 0)
			{
				List<Curso> listaCurso = new List<Curso>();
				BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
				Curso objCurso = new Curso();

				listaCurso = objBLCicloLectivo.GetCursosByCicloLectivo(idCicloLectivo);
				UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "nombre", true);
				ddlCurso.Enabled = true;
			}
			else
			{
				//ddlCurso.SelectedIndex = 0;
				ddlCurso.Enabled = false;
			}
		}

		/// <summary>
		/// Limpiars the campos.
		/// </summary>
		private void LimpiarCampos()
		{
			ddlCicloLectivo.SelectedIndex = 0;
			if (ddlCicloLectivoEdit.Items.Count > 0) ddlCicloLectivoEdit.SelectedIndex = 0;
			if (ddlCurso.Items.Count > 0) ddlCurso.SelectedIndex = 0;
			if (ddlCursoEdit.Items.Count > 0) ddlCursoEdit.SelectedIndex = 0;
			HabilitarBotonesDetalle(false);
			chkActivo.Checked = true;
			chkActivoEdit.Checked = false;
			txtDescripcionEdit.Text = string.Empty;
		}

		/// <summary>
		/// Buscars the filtrando.
		/// </summary>
		private void BuscarFiltrando()
		{
			calfecha.ValidarRangoDesde();
			AgendaActividades entidad = new AgendaActividades();
			entidad.cursoCicloLectivo.idCurso = Convert.ToInt32(ddlCurso.SelectedValue);
			entidad.cursoCicloLectivo.idCicloLectivo = Convert.ToInt32(ddlCicloLectivo.SelectedValue);
			entidad.fechaCreacion = Convert.ToDateTime(calfecha.ValorFecha);
			entidad.activo = chkActivo.Checked;
			propFiltroAgenda = entidad;
			BuscarAgenda(entidad);
		}

		/// <summary>
		/// Buscars the entidads.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void BuscarAgenda(AgendaActividades entidad)
		{
			CargarLista(entidad);

			CargarGrilla();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void CargarLista(AgendaActividades entidad)
		{
			objBLAgenda = new BLAgendaActividades(entidad);
			listaAgenda = objBLAgenda.GetAgendaActividades(entidad);
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private AgendaActividades ObtenerValoresDePantalla()
		{
			AgendaActividades entidad = new AgendaActividades();
			entidad = propAgenda;
			if (!esNuevo)
			{
				entidad.idAgendaActividad = propAgenda.idAgendaActividad;
				entidad.cursoCicloLectivo.idCursoCicloLectivo = propAgenda.cursoCicloLectivo.idCursoCicloLectivo;
			}
			entidad.descripcion = txtDescripcionEdit.Text;
			entidad.fechaCreacion = Convert.ToDateTime(txtFechaEdit.Text);
			entidad.cursoCicloLectivo.idCicloLectivo = Convert.ToInt32(ddlCicloLectivoEdit.SelectedValue);
			entidad.cursoCicloLectivo.idCurso = Convert.ToInt32(ddlCursoEdit.SelectedValue);
			entidad.activo = chkActivoEdit.Checked;
			return entidad;
		}

		/// <summary>
		/// Guardars the agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void GuardarAgenda(AgendaActividades entidad)
		{
			objBLAgenda = new BLAgendaActividades(entidad);
			objBLAgenda.Save();
		}

		/// <summary>
		/// Cargars the entidad.
		/// </summary>
		private void CargarValoresEnPantalla(int idAgendaActividad)
		{
			AgendaActividades entidad = listaAgenda.Find(c => c.idAgendaActividad == idAgendaActividad);
			propAgenda = entidad;
			//propAgenda.cursoCicloLectivo.idCursoCicloLectivo = entidad.cursoCicloLectivo.idCursoCicloLectivo;
			txtDescripcionEdit.Text = entidad.descripcion;
			txtFechaEdit.Text = DateTime.Now.ToShortDateString();
			ddlCicloLectivoEdit.SelectedValue = entidad.cursoCicloLectivo.idCicloLectivo.ToString();
			CargarComboCursos(entidad.cursoCicloLectivo.idCicloLectivo, ddlCursoEdit);
			ddlCursoEdit.SelectedValue = entidad.cursoCicloLectivo.idCurso.ToString();
			ddlCicloLectivoEdit.Enabled = false;
			ddlCursoEdit.Enabled = false;
			chkActivoEdit.Checked = entidad.activo;
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			string mensaje = string.Empty;
			if (txtDescripcionEdit.Text.Trim().Length == 0)
				mensaje = "- Descripcion<br />";
			//if (calFechaEdit.Fecha.Text.Trim().Length == 0)
			//    mensaje += "- Fecha<br />";
			if (!(Convert.ToInt32(ddlCicloLectivoEdit.SelectedValue) > 0))
				mensaje += "- Ciclo Lectivo";
			if (!(Convert.ToInt32(ddlCursoEdit.SelectedValue) > 0))
				mensaje += "- Curso";
			return mensaje;
		}

		/// <summary>
		/// Cargas the agenda.
		/// </summary>
		private void CargaAgenda()
		{
			//propAgenda = new AgendaActividades();
			//propAgenda.idAgendaActividad = idAgenda;
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;
			CargarCombos(ddlCicloLectivoEdit, ddlCursoEdit);
			CargarValoresEnPantalla(propAgenda.idAgendaActividad);
			litEditar.Visible = true;
			litNuevo.Visible = false;
			HabilitarBotonesDetalle(true);
			btnBuscar.Visible = false;
			//btnNuevo.Visible = false;
			btnVolver.Visible = true;
			btnGuardar.Visible = true;
			gvwReporte.Visible = false;
			udpFiltrosBusqueda.Visible = false;
			udpEdit.Visible = true;
			udpEdit.Update();
		}
		#endregion

	}
}