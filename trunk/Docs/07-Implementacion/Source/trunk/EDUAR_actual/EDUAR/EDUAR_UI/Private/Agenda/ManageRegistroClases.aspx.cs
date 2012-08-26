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
using System.Collections;
using System.Web;
using System.Data;

namespace EDUAR_UI
{
	public partial class ManageRegistroClases : EDUARBasePage
	{
		#region --[Atributos]--
		private BLAgendaActividades objBLAgenda;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Mantiene la agenda seleccionada en la grilla.
		/// Se utiliza para el manejo de eventos de agenda (evaluación, excursión, reunión, registro de clases).
		/// En las pantallas hijas no permito la edición.
		/// </summary>
		/// <value>
		/// The prop agenda.
		/// </value>
		public AgendaActividades propAgenda
		{
			get
			{ return (AgendaActividades)Session["propAgenda"]; }
		}

		/// <summary>
		/// Gets or sets the prop evento.
		/// </summary>
		/// <value>
		/// The prop evento.
		/// </value>
		public RegistroClases propEvento
		{
			get
			{
				if (ViewState["propEvento"] == null)
					propEvento = new RegistroClases();

				return (RegistroClases)ViewState["propEvento"];
			}
			set { ViewState["propEvento"] = value; }
		}

		/// <summary>
		/// Gets or sets the prop filtro evento.
		/// </summary>
		/// <value>
		/// The prop filtro evento.
		/// </value>
		public RegistroClases propFiltroEvento
		{
			get
			{
				if (ViewState["propFiltroEvento"] == null)
					propFiltroEvento = new RegistroClases();

				return (RegistroClases)ViewState["propFiltroEvento"];
			}
			set { ViewState["propFiltroEvento"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista agenda.
		/// </summary>
		/// <value>
		/// The lista agenda.
		/// </value>
		public List<RegistroClases> listaEventos
		{
			get
			{
				if (ViewState["listaEventos"] == null)
					listaEventos = new List<RegistroClases>();

				return (List<RegistroClases>)ViewState["listaEventos"];
			}
			set { ViewState["listaEventos"] = value; }
		}

		/// <summary>
		/// Lista de TODOS los contenidos registrados
		/// </summary>
		/// <value>
		/// The lista contenido.
		/// </value>
		protected List<TemaContenido> listaContenido
		{
			get
			{
				if (ViewState["listaContenido"] == null
					|| ((List<TemaContenido>)ViewState["listaContenido"]).Count == 0)
				{
					ViewState["listaContenido"] = new List<TemaContenido>();
					TemasContenido listaTemas = new TemasContenido();
					BLTemaContenido objBLTemas = new BLTemaContenido();
					AsignaturaCicloLectivo objAsignatura = new AsignaturaCicloLectivo();
					objAsignatura.cursoCicloLectivo.curso.idCurso = propAgenda.cursoCicloLectivo.idCurso;
					objAsignatura.idAsignaturaCicloLectivo = Convert.ToInt32(ddlAsignaturaEdit.SelectedValue);
					objAsignatura.cursoCicloLectivo.cicloLectivo = base.cicloLectivoActual;
					ViewState["listaContenido"] = objBLTemas.GetContenidosPlanificados(objAsignatura);
				}
				return (List<TemaContenido>)ViewState["listaContenido"];
			}
			set { ViewState["listaContenido"] = value; }
		}

		/// <summary>
		/// Lista de contenidos SELECCIONADOS
		/// </summary>
		/// <value>
		/// The lista seleccion.
		/// </value>
		protected List<int> listaSeleccion
		{
			get
			{
				if (HttpContext.Current.Session["listaSeleccion"] == null)
					HttpContext.Current.Session["listaSeleccion"] = new List<int>();
				return (List<int>)HttpContext.Current.Session["listaSeleccion"];
			}
			set { HttpContext.Current.Session["listaSeleccion"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista seleccion porcentajes.
		/// </summary>
		/// <value>
		/// The lista seleccion porcentajes.
		/// </value>
		protected List<string> listaSeleccionPorcentajes
		{
			get
			{
				if (HttpContext.Current.Session["listaSeleccionPorcentajes"] == null)
					HttpContext.Current.Session["listaSeleccionPorcentajes"] = new List<string>();
				return (List<string>)HttpContext.Current.Session["listaSeleccionPorcentajes"];
			}
			set { HttpContext.Current.Session["listaSeleccionPorcentajes"] = value; }
		}

		/// <summary>
		/// Lista de Contenidos seleccionados en el momento que presiona GUARDAR
		/// </summary>
		/// <value>
		/// The lista seleccion guardar.
		/// </value>
		protected List<int> listaSeleccionGuardar
		{
			get
			{
				if (HttpContext.Current.Session["listaSeleccionGuardar"] == null)
					HttpContext.Current.Session["listaSeleccionGuardar"] = new List<int>();
				return (List<int>)HttpContext.Current.Session["listaSeleccionGuardar"];
			}
			set { HttpContext.Current.Session["listaSeleccionGuardar"] = value; }
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
					//Siempre que se acceda a la página debiera existir una agenda
					propEvento.idAgendaActividad = propAgenda.idAgendaActividad;
					if (propEvento.idAgendaActividad > 0)
					{
						BuscarAgenda(propEvento);
					}
					//else
					//    BuscarAgenda(null);
				}
				calfechas.startDate = cicloLectivoActual.fechaInicio;
				calfechas.endDate = cicloLectivoActual.fechaFin;
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
					case enumAcciones.Limpiar:
						CargarPresentacion();
						BuscarFiltrando();
						//BuscarAgenda(propEvento);
						break;
					case enumAcciones.Guardar:
						AccionPagina = enumAcciones.Limpiar;
						GuardarEntidad(ObtenerValoresDePantalla());
						Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
						break;
					case enumAcciones.Error:
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
				CargarComboAsignatura();
				ddlTipoRegistroClase.SelectedValue = EDUAR_Utility.Enumeraciones.enumTipoRegistroClases.ClaseNormal.GetHashCode().ToString();
				esNuevo = true;
				btnGuardar.Visible = true;
				btnBuscar.Visible = false;
				btnVolver.Visible = true;
				btnNuevo.Visible = false;
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
					//DateTime fechaEvento = new DateTime(DateTime.Now.Year, Convert.ToInt32(ddlMeses.SelectedValue), Convert.ToInt32(ddlDia.SelectedValue));
					//if (fechaEvento < DateTime.Today)
					//{
					//    AccionPagina = enumAcciones.Error;
					//    Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeFechaMenorActual, enumTipoVentanaInformacion.Advertencia);
					//}
					//else
					{
						if (Page.IsValid)
						{
							AccionPagina = enumAcciones.Guardar;
							Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
						}
					}
				}
				else
				{
					AccionPagina = enumAcciones.Error;
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
				//CargarPresentacion();
				//BuscarAgenda(propFiltroAgenda);
				Response.Redirect("ManageAgendaActividades.aspx", false);
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
						propEvento.idEventoAgenda = Convert.ToInt32(e.CommandArgument.ToString());
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

		/// <summary>
		/// Handles the SelectedIndexChanged event of the ddlAsignaturaEdit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ddlAsignaturaEdit_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int idAsignatura = 0;
				int.TryParse(ddlAsignaturaEdit.SelectedValue, out idAsignatura);
				if (idAsignatura > 0)
				{
					btnContenidosPopUp.Enabled = true;
					ddlMeses.Enabled = true;
					ddlMeses.SelectedValue = DateTime.Now.Month.ToString();
					ddlDia.Enabled = true;
					BindComboModulos(DateTime.Now.Month);
				}
				else
				{
					btnContenidosPopUp.Enabled = false;
					ddlMeses.Enabled = false;
					ddlDia.Enabled = false;
				}
				udpEdit.Update();
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
		/// Handles the Click event of the btnContenidosPopUp control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnContenidosPopUp_Click(object sender, EventArgs e)
		{
			try
			{
				CargarContenidos();
				listaSeleccion = listaSeleccionGuardar;
				ProductsSelectionManager.RestoreSelection(gvwContenidos, "listaSeleccion");
				//btnGuardarPopUp.Visible = !planificacionEditar.fechaAprobada.HasValue;
				mpeContenido.Show();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnVolverPopUp control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnVolverPopUp_Click(object sender, EventArgs e)
		{
			try
			{
				mpeContenido.Hide();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnGuardarPopUp control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnGuardarPopUp_Click(object sender, EventArgs e)
		{
			try
			{
				ProductsSelectionManager.KeepSelection(gvwContenidos, "listaSeleccion");
				listaSeleccionGuardar = listaSeleccion;
				mpeContenido.Hide();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwContenido control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		protected void gvwContenidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
				ProductsSelectionManager.KeepSelection(gvwContenidos, "listaSeleccion");

				gvwContenidos.PageIndex = e.NewPageIndex;
				CargarContenidos();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the PageIndexChanged event of the gvwContenidos control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void gvwContenidos_PageIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ProductsSelectionManager.RestoreSelection(gvwContenidos, "listaSeleccion");
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
			lblTitulo.Text = propAgenda.cursoCicloLectivo.curso.nombre + " - " + propAgenda.cursoCicloLectivo.cicloLectivo.nombre;
			LimpiarCampos();
			CargarCombos();
			udpEdit.Visible = false;
			btnVolver.Visible = true;
			btnGuardar.Visible = false;
			udpFiltrosBusqueda.Visible = true;
			btnNuevo.Visible = true;
			btnBuscar.Visible = true;
			gvwReporte.Visible = true;
			udpFiltros.Update();
			udpGrilla.Update();
		}

		/// <summary>
		/// Limpiars the campos.
		/// </summary>
		private void LimpiarCampos()
		{
			chkActivo.Checked = true;
			chkActivoEdit.Checked = false;
			if (ddlMeses.Items.Count > 0) ddlMeses.SelectedIndex = 0;
			if (ddlDia.Items.Count > 0) ddlDia.SelectedIndex = 0;
			calfechas.LimpiarControles();
			if (ddlAsignatura.Items.Count > 0) ddlAsignatura.SelectedIndex = 0;
			if (ddlAsignaturaEdit.Items.Count > 0) ddlAsignaturaEdit.SelectedIndex = 0;
			//if (listaContenido != null && listaContenido.Count > 0) listaContenido.Clear();
			txtDescripcionEdit.Text = string.Empty;
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		private void CargarCombos()
		{
			BLAsignatura objBLAsignatura = new BLAsignatura();
			Asignatura objAsignatura = new Asignatura();
			objAsignatura.cursoCicloLectivo.idCursoCicloLectivo = propAgenda.cursoCicloLectivo.idCursoCicloLectivo;
			objAsignatura.cursoCicloLectivo.idCicloLectivo = propAgenda.cursoCicloLectivo.idCicloLectivo;
			if (User.IsInRole(enumRoles.Docente.ToString()))
				objAsignatura.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

			ddlAsignatura.Items.Clear();
			ddlAsignaturaEdit.Items.Clear();
			ddlMeses.Items.Clear();
			UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, objBLAsignatura.GetAsignaturasCurso(objAsignatura), "idAsignatura", "nombre", false, true);
			UIUtilidades.BindComboMeses(ddlMeses, false, cicloLectivoActual.fechaInicio.Month);
			ddlMeses.Enabled = false;

			BLTipoRegistroClases objBLTipoRegistroClase = new BLTipoRegistroClases();
			List<TipoRegistroClases> listaRegistros = new List<TipoRegistroClases>();
			listaRegistros = objBLTipoRegistroClase.GetTipoRegistroClases(new TipoRegistroClases());
			UIUtilidades.BindCombo<TipoRegistroClases>(ddlTipoRegistroClase, listaRegistros, "idTipoRegistroClases", "nombre", true, false);
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
				objDiaHorario.idAsignaturaCurso = Convert.ToInt32(ddlAsignaturaEdit.SelectedValue);
			else
				objDiaHorario.idAsignaturaCurso = propEvento.asignatura.idAsignatura;
			listaHorario = objBLHorario.GetHorariosCurso(objDiaHorario, propAgenda.cursoCicloLectivo);
			int anio = propAgenda.cursoCicloLectivo.cicloLectivo.fechaInicio.Year;
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

		/// Cargars the grilla.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		private void CargarGrilla()
		{
			gvwReporte.DataSource = UIUtilidades.BuildDataTable<RegistroClases>(listaEventos).DefaultView;
			gvwReporte.DataBind();
			udpEdit.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Buscars the filtrando.
		/// </summary>
		private void BuscarFiltrando()
		{
			calfechas.ValidarRangoDesdeHasta(false);
			RegistroClases entidad = new RegistroClases();
			entidad.asignatura.idAsignatura = Convert.ToInt32(ddlAsignatura.SelectedValue);
			entidad.fechaEventoDesde = Convert.ToDateTime(calfechas.ValorFechaDesde);
			entidad.fechaEventoHasta = Convert.ToDateTime(calfechas.ValorFechaHasta);
			entidad.activo = chkActivo.Checked;
			propFiltroEvento = entidad;
			BuscarAgenda(entidad);
		}

		/// <summary>
		/// Buscars the entidads.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void BuscarAgenda(RegistroClases entidad)
		{
			CargarLista(entidad);

			CargarGrilla();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void CargarLista(RegistroClases entidad)
		{
			objBLAgenda = new BLAgendaActividades();
			entidad.idAgendaActividad = propAgenda.idAgendaActividad;
			listaEventos = objBLAgenda.GetRegistroClasesAgenda(entidad);
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private RegistroClases ObtenerValoresDePantalla()
		{
			RegistroClases entidad = new RegistroClases();
			entidad = propEvento;
			if (!esNuevo)
			{
				entidad.idAgendaActividad = propAgenda.idAgendaActividad;
				entidad.idEventoAgenda = propEvento.idEventoAgenda;
				//entidad.cursoCicloLectivo.idCursoCicloLectivo = propAgenda.cursoCicloLectivo.idCursoCicloLectivo;
			}
			entidad.asignatura.idAsignatura = Convert.ToInt32(ddlAsignaturaEdit.SelectedValue);
			entidad.descripcion = txtDescripcionEdit.Text;
			//entidad.fechaEvento = Convert.ToDateTime(calFechaEvento.ValorFecha);
			entidad.fechaEvento = Convert.ToDateTime(new DateTime(propAgenda.cursoCicloLectivo.cicloLectivo.fechaInicio.Year, Convert.ToInt32(ddlMeses.SelectedValue), Convert.ToInt32(ddlDia.SelectedValue)));
			entidad.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
			entidad.activo = chkActivoEdit.Checked;
			entidad.fechaAlta = DateTime.Now;
			entidad.tipoRegistro.idTipoRegistroClases = Convert.ToInt32(ddlTipoRegistroClase.SelectedValue);

			List<DetalleRegistroClases> listaTemporal = new List<DetalleRegistroClases>();
			foreach (int item in listaSeleccionGuardar)
			{
				listaTemporal.Add(new DetalleRegistroClases() { temaContenido = new TemaContenido() { idTemaContenido = item } });
			}
			entidad.listaDetalleRegistro = listaTemporal;
			return entidad;
		}

		/// <summary>
		/// Guardars the agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void GuardarEntidad(EventoAgenda entidad)
		{
			objBLAgenda = new BLAgendaActividades(propAgenda);
			objBLAgenda.GetById();

			int idAsignatura = Convert.ToInt32(ddlAsignaturaEdit.SelectedValue);

			entidad.tipoEventoAgenda.idTipoEventoAgenda = (int)enumEventoAgendaType.ClaseDiaria;
			if (objBLAgenda.VerificarAgenda(entidad, idAsignatura))
			{
				objBLAgenda.Data.listaRegistroClases.Add((RegistroClases)entidad);
				objBLAgenda.Save();
			}
		}

		/// <summary>
		/// Cargars the entidad.
		/// </summary>
		private void CargarValoresEnPantalla(int idEventoAgenda)
		{
			RegistroClases entidad = listaEventos.Find(c => c.idEventoAgenda == idEventoAgenda);
			propEvento = entidad;
			if (entidad != null)
			{
				txtDescripcionEdit.Text = entidad.descripcion;
				BindComboModulos(entidad.fechaEvento.Month);
				//calFechaEvento.Fecha.Text = entidad.fechaEvento.ToShortDateString();
				ddlMeses.SelectedValue = entidad.fechaEvento.Month.ToString();
				if (ddlDia.Items.FindByValue(entidad.fechaEvento.Day.ToString()) != null)
				{
					ddlDia.SelectedValue = entidad.fechaEvento.Day.ToString();
					ddlDia.Enabled = true;
				}
				else
					ddlDia.SelectedIndex = 0;
				ddlAsignaturaEdit.SelectedValue = entidad.asignatura.idAsignatura.ToString();
				btnContenidosPopUp.Enabled = true;
				ddlAsignaturaEdit.Enabled = false;
				ddlMeses.Enabled = true;
				chkActivoEdit.Checked = entidad.activo;
				ddlTipoRegistroClase.SelectedValue = entidad.tipoRegistro.idTipoRegistroClases.ToString();
			}
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
			if (string.IsNullOrEmpty(ddlAsignaturaEdit.SelectedValue) || !(Convert.ToInt32(ddlAsignaturaEdit.SelectedValue) > 0))
				mensaje += "- Asignatura<br />";
			if (string.IsNullOrEmpty(ddlMeses.SelectedValue)
				|| !(Convert.ToInt32(ddlMeses.SelectedValue) > 0)
				|| string.IsNullOrEmpty(ddlDia.SelectedValue)
				|| !(Convert.ToInt32(ddlDia.SelectedValue) > 0))
				mensaje += "- Fecha de Registro<br />";
			int idTipoClase = 0;
			int.TryParse(ddlTipoRegistroClase.SelectedValue, out idTipoClase);
			if (idTipoClase <= 0)
				mensaje += "- Tipo de Registro de Clase<br />";
			return mensaje;
		}

		/// <summary>
		/// Cargas the agenda.
		/// </summary>
		private void CargaAgenda()
		{
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;
			CargarComboAsignatura();
			CargarValoresEnPantalla(propEvento.idEventoAgenda);
			litEditar.Visible = true;
			litNuevo.Visible = false;
			btnBuscar.Visible = false;
			btnNuevo.Visible = false;
			btnVolver.Visible = true;
			btnGuardar.Visible = true;
			gvwReporte.Visible = false;
			udpFiltrosBusqueda.Visible = false;
			udpEdit.Visible = true;
			udpFiltros.Update();
			udpEdit.Update();
		}

		/// <summary>
		/// Cargars the combo asignatura.
		/// </summary>
		private void CargarComboAsignatura()
		{
			BLAsignatura objBLAsignatura = new BLAsignatura();
			Asignatura objAsignatura = new Asignatura();
			objAsignatura.cursoCicloLectivo.idCursoCicloLectivo = propAgenda.cursoCicloLectivo.idCursoCicloLectivo;
			objAsignatura.curso.cicloLectivo.idCicloLectivo = propAgenda.cursoCicloLectivo.idCicloLectivo;
			if (User.IsInRole(enumRoles.Docente.ToString()))
				objAsignatura.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

			UIUtilidades.BindCombo<Asignatura>(ddlAsignaturaEdit, objBLAsignatura.GetAsignaturasCurso(objAsignatura), "idAsignatura", "nombre", true);
		}

		/// <summary>
		/// Cargars the contenidos.
		/// </summary>
		private void CargarContenidos()
		{
			gvwContenidos.DataSource = listaContenido;
			gvwContenidos.DataBind();
		}
		#endregion
	}
}