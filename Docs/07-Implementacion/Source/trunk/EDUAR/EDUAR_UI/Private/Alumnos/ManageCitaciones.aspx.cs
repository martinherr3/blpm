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

namespace EDUAR_UI
{
	public partial class ManageCitaciones : EDUARBasePage
	{
		#region --[Atributos]--
		private BLCitacion objBLCitacion;
		#endregion

		#region --[Propiedades]--
		public Citacion propCitacion
		{
			get
			{
				if (ViewState["propCitacion"] == null)
					propCitacion = new Citacion();
				return (Citacion)ViewState["propCitacion"];
			}
			set
			{
				ViewState["propCitacion"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the prop filtro citación.
		/// </summary>
		/// <value>
		/// The prop filtro citacion.
		/// </value>
		public Citacion propFiltroCitacion
		{
			get
			{
				if (ViewState["propFiltroCitacion"] == null)
					propFiltroCitacion = new Citacion();

				return (Citacion)ViewState["propFiltroCitacion"];
			}
			set { ViewState["propFiltroCitacion"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista agenda.
		/// </summary>
		/// <value>
		/// The lista agenda.
		/// </value>
		public List<Citacion> listaCitaciones
		{
			get
			{
				if (ViewState["listaCitaciones"] == null)
					listaCitaciones = new List<Citacion>();

				return (List<Citacion>)ViewState["listaCitaciones"];
			}
			set { ViewState["listaCitaciones"] = value; }
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
					BuscarCitacion(null);
					//Siempre que se acceda a la página debiera existir una agenda
					//propEvento.idAgendaActividad = propAgenda.idAgendaActividad;
					//if (propEvento.idAgendaActividad > 0)
					//{
					//    BuscarAgenda(propEvento);
					//}
					//else
					//    BuscarAgenda(null);
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
				//TODO: hay que cargar días disponibles en función de los días que se dicta la materia!
				AccionPagina = enumAcciones.Nuevo;
				LimpiarCampos();
				CargarCombos();
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
				//DateTime fechaEvento = new DateTime(DateTime.Now.Year, Convert.ToInt32(ddlMeses.SelectedValue), Convert.ToInt32(ddlDia.SelectedValue));
				//if (fechaEvento <= DateTime.Today)
				//{
				//    AccionPagina = enumAcciones.Error;
				//    Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeFechaMenorActual, enumTipoVentanaInformacion.Advertencia);
				//}
				//else
				//{
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
					AccionPagina = enumAcciones.Error;
					Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
				}
				//}
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
				//BuscarAgenda(propFiltroAgenda);
				//Response.Redirect("ManageAgendaActividades.aspx", false);
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
						propCitacion.idCitacion = Convert.ToInt32(e.CommandArgument.ToString());
						CargaCitacion();
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
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			//lblTitulo.Text = propAgenda.cursoCicloLectivo.curso.nombre + " - " + propAgenda.cursoCicloLectivo.cicloLectivo.nombre;
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
			//if (ddlMeses.Items.Count > 0) ddlMeses.SelectedIndex = 0;
			//if (ddlDia.Items.Count > 0) ddlDia.SelectedIndex = 0;
			calfechas.LimpiarControles();
			//ddlAsignatura.SelectedIndex = 0;
			//ddlAsignaturaEdit.SelectedIndex = 0;
			txtDescripcionEdit.Text = string.Empty;
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		private void CargarCombos()
		{
			List<Tutor> listaTutores = new List<Tutor>();
			BLTutor objBLTutor = new BLTutor();
			listaTutores = objBLTutor.GetTutores(new Tutor() { activo = true });
			UIUtilidades.BindCombo<Tutor>(ddlTutores, listaTutores, "idTutor", "apellido", "nombre", true);

			List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
			BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
			listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(new CicloLectivo() { activo = true });

			List<Curso> listaCurso = new List<Curso>();
			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
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
				ddlCurso.Enabled = false;
			}
		}

		/// Cargars the grilla.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		private void CargarGrilla()
		{
			gvwReporte.DataSource = UIUtilidades.BuildDataTable<Citacion>(listaCitaciones).DefaultView;
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
			Citacion entidad = new Citacion();
			//entidad.asignatura.idAsignatura = Convert.ToInt32(ddlAsignatura.SelectedValue);
			//entidad.fechaEventoDesde = Convert.ToDateTime(calfechas.ValorFechaDesde);
			//entidad.fechaEventoHasta = Convert.ToDateTime(calfechas.ValorFechaHasta);
			entidad.activo = chkActivo.Checked;
			propFiltroCitacion = entidad;
			BuscarCitacion(entidad);
		}

		/// <summary>
		/// Buscars the entidads.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void BuscarCitacion(Citacion entidad)
		{
			CargarLista(entidad);
			CargarGrilla();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void CargarLista(Citacion entidad)
		{
			objBLCitacion = new BLCitacion();
			listaCitaciones = objBLCitacion.GetCitaciones(entidad);
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private Citacion ObtenerValoresDePantalla()
		{
			Citacion entidad = new Citacion();
			entidad = propCitacion;
			if (!esNuevo)
			{
				entidad.idCitacion = propCitacion.idCitacion;
				//entidad.cursoCicloLectivo.idCursoCicloLectivo = propAgenda.cursoCicloLectivo.idCursoCicloLectivo;
			}
			//entidad.asignatura.idAsignatura = Convert.ToInt32(ddlAsignaturaEdit.SelectedValue);
			entidad.detalles = txtDescripcionEdit.Text;
			//entidad.fechaEvento = Convert.ToDateTime(calFechaEvento.ValorFecha);
			//entidad.fechaEvento = Convert.ToDateTime(new DateTime(propAgenda.cursoCicloLectivo.cicloLectivo.fechaInicio.Year, Convert.ToInt32(ddlMeses.SelectedValue), Convert.ToInt32(ddlDia.SelectedValue)));
			entidad.organizador.username = ObjDTSessionDataUI.ObjDTUsuario.Nombre;
			entidad.activo = chkActivoEdit.Checked;
			//entidad.fechaAlta = DateTime.Now;
			return entidad;
		}

		/// <summary>
		/// Guardars the agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void GuardarEntidad(Citacion entidad)
		{
			objBLCitacion = new BLCitacion(entidad);
			objBLCitacion.Save();
		}

		/// <summary>
		/// Cargars the entidad.
		/// </summary>
		private void CargarValoresEnPantalla(int idCitacion)
		{
			Citacion entidad = listaCitaciones.Find(c => c.idCitacion == idCitacion);
			propCitacion = entidad;
			if (entidad != null)
			{
				txtDescripcionEdit.Text = entidad.detalles;
				//BindComboModulos(entidad.fechaEvento.Month);
				//calFechaEvento.Fecha.Text = entidad.fechaEvento.ToShortDateString();
				//ddlMeses.SelectedValue = entidad.fechaEvento.Month.ToString();
				//if (ddlDia.Items.FindByValue(entidad.fechaEvento.Day.ToString()) != null)
				//    ddlDia.SelectedValue = entidad.fechaEvento.Day.ToString();
				//ddlAsignaturaEdit.SelectedValue = entidad.asignatura.idAsignatura.ToString();
				//ddlAsignaturaEdit.Enabled = false;
				chkActivoEdit.Checked = entidad.activo;
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
			//if (calFechaEdit.Fecha.Text.Trim().Length == 0)
			//    mensaje += "- Fecha<br />";
			//if (!(Convert.ToInt32(ddlAsignaturaEdit.SelectedValue) > 0))
			//    mensaje += "- Asignatura";
			//if (!(Convert.ToInt32(ddlMeses.SelectedValue) > 0) || !(Convert.ToInt32(ddlDia.SelectedValue) > 0))
			//    mensaje += "- Fecha de Evaluación";
			//if (!(Convert.ToInt32(ddlCursoEdit.SelectedValue) > 0))
			//    mensaje += "- Curso";
			return mensaje;
		}

		/// <summary>
		/// Cargas the agenda.
		/// </summary>
		private void CargaCitacion()
		{
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;
			CargarValoresEnPantalla(propCitacion.idCitacion);
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
		#endregion
	}
}