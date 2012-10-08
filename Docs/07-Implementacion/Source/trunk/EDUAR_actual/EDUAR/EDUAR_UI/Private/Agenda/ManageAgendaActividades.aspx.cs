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
					propAgenda = new AgendaActividades();

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
					propFiltroAgenda = new AgendaActividades();

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
					listaAgenda = new List<AgendaActividades>();

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
				novControl.GuardarClick += (Guardar);
				if (!Page.IsPostBack)
				{
					base.idNovedadConsulta = 0;
					CargarPresentacion();
					if (propAgenda.idAgendaActividad > 0)
					{
						CargarLista(propAgenda);
						CargaAgenda();
					}
					else
						BuscarAgenda(propAgenda);
					CargarAgendaCurso();
				}
			}
			catch (Exception ex)
			{
				AvisoMostrar = true;
				AvisoExcepcion = ex;
			}
		}

		private void CargarAgendaCurso()
		{
			//propAgenda.idAgendaActividad = Convert.ToInt32(e.CommandArgument.ToString());
			propAgenda.cursoCicloLectivo.idCursoCicloLectivo = base.idCursoCicloLectivo;
			CargaAgenda();
			lblTitulo.Text = "Agenda de Actividades - " + propAgenda.cursoCicloLectivo.curso.nombre; //+ " - " + propAgenda.cursoCicloLectivo.cicloLectivo.nombre;
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
					case enumAcciones.Limpiar:
						CargarPresentacion();
						BuscarAgenda(null);
						break;
					case enumAcciones.Guardar:
						AccionPagina = enumAcciones.Limpiar;
						GuardarAgenda(ObtenerValoresDePantalla());
						Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
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
		/// Guardars the specified sender.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Guardar(object sender, EventArgs e)
		{
			try
			{
				novControl.GuardarNovedad();
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
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
				Response.Redirect("ManageExcursiones.aspx", false);
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
				Response.Redirect("ManageReuniones.aspx", false);
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
						//TODO: Aquí hay que llamar a la validación de disponibilidad de agenda
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
				base.idCursoCicloLectivo = 0;
				base.cursoActual = new CursoCicloLectivo();
				Response.Redirect("~/Private/AccesoCursos.aspx", false);
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

						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwReporte control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		protected void gvwReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
				gvwReporte.PageIndex = e.NewPageIndex;
				CargarGrilla();
			}
			catch (Exception ex) { Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwAgenda control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		protected void gvwAgenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
				gvwAgenda.PageIndex = e.NewPageIndex;
				CargarGrillaAgenda();
				btnBuscar.Visible = false;
				btnVolver.Visible = true;
				gvwReporte.Visible = false;
				udpEdit.Visible = true;
				udpEdit.Update();
			}
			catch (Exception ex) { Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the Click event of the btnRegistroClases control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnRegistroClases_Click(object sender, EventArgs e)
		{
			try
			{
				Response.Redirect("ManageRegistroClases.aspx", false);
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
		/// Cargars the grilla agenda.
		/// </summary>
		private void CargarGrillaAgenda()
		{
			gvwAgenda.DataSource = UIUtilidades.BuildDataTable<EventoAgenda>(propAgenda.listaEventos).DefaultView;
			gvwAgenda.DataBind();
			udpEdit.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			LimpiarCampos();
			lblTitulo.Text = "Actividades";
			udpEdit.Visible = false;
			btnVolver.Visible = false;
			btnGuardar.Visible = false;
			btnBuscar.Visible = true;
			gvwReporte.Visible = true;
			udpFiltros.Update();
			udpGrilla.Update();
		}

		/// <summary>
		/// Habilitars the botones detalle.
		/// </summary>
		/// <param name="habilitar">if set to <c>true</c> [habilitar].</param>
		private void HabilitarBotonesDetalle(bool habilitar)
		{
			btnExcursion.Visible = habilitar;
			btnEvaluacion.Visible = habilitar;
			btnReunion.Visible = habilitar;
			btnRegistroClases.Visible = habilitar;
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		//private void CargarCombos(DropDownList ddlCurso)
		//{
		//    lblTitulo.Text = "Actividades - " + cicloLectivoActual.nombre;
		//    UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "Nombre", true);
		//}

		/// <summary>
		/// Limpiars the campos.
		/// </summary>
		private void LimpiarCampos()
		{
			HabilitarBotonesDetalle(false);
		}

		/// <summary>
		/// Buscars the filtrando.
		/// </summary>
		private void BuscarFiltrando()
		{
			lblTitulo.Text = "Actividades";
			AgendaActividades entidad = new AgendaActividades();
			entidad.cursoCicloLectivo.idCicloLectivo = cicloLectivoActual.idCicloLectivo;
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
			entidad.activo = true;
			if (User.IsInRole(enumRoles.Docente.ToString()))
				entidad.usuario = ObjSessionDataUI.ObjDTUsuario.Nombre;
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
		private void CargarValoresEnPantalla(int idCursoCicloLectivo)
		{
			BLAgendaActividades objBLAgenda = new BLAgendaActividades(new AgendaActividades() { cursoCicloLectivo = new CursoCicloLectivo() { idCursoCicloLectivo = idCursoCicloLectivo } });
			objBLAgenda.GetById();
			propAgenda = objBLAgenda.Data;
			novControl.visible = true;
			idCursoCicloLectivo = propAgenda.cursoCicloLectivo.idCursoCicloLectivo;
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			string mensaje = string.Empty;
			return mensaje;
		}

		/// <summary>
		/// Cargas the agenda.
		/// </summary>
		private void CargaAgenda()
		{
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;
			CargarValoresEnPantalla(propAgenda.cursoCicloLectivo.idCursoCicloLectivo);
			propAgenda.listaEventos.Sort((p, q) => DateTime.Compare(p.fechaEvento, q.fechaEvento));
			CargarGrillaAgenda();
			HabilitarBotonesDetalle(propAgenda.activo);
			btnBuscar.Visible = false;
			btnVolver.Visible = true;
			gvwReporte.Visible = false;
			udpEdit.Visible = true;
			udpEdit.Update();
		}

		#endregion
	}
}