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
	public partial class RegNovedadInstitucion : EDUARBasePage
	{
		#region --[Atributos]--
		private BLEventoInstitucional objBLEvento;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the prop evento.
		/// </summary>
		/// <value>
		/// The prop evento.
		/// </value>
		public EventoInstitucional propEvento
		{
			get
			{
				if (ViewState["propEvento"] == null)
					return new EventoInstitucional();

				return (EventoInstitucional)ViewState["propEvento"];
			}
			set { ViewState["propEvento"] = value; }
		}

		/// <summary>
		/// Gets or sets the prop evento.
		/// </summary>
		/// <value>
		/// The prop evento.
		/// </value>
		public EventoInstitucional propFiltroEvento
		{
			get
			{
				if (ViewState["propFiltroEvento"] == null)
					return new EventoInstitucional();

				return (EventoInstitucional)ViewState["propFiltroEvento"];
			}
			set { ViewState["propFiltroEvento"] = value; }
		}
		/// <summary>
		/// Gets or sets the lista evento.
		/// </summary>
		/// <value>
		/// The lista evento.
		/// </value>
		public List<EventoInstitucional> listaEvento
		{
			get
			{
				if (ViewState["listaEvento"] == null)
					return new List<EventoInstitucional>();

				return (List<EventoInstitucional>)ViewState["listaEvento"];
			}
			set { ViewState["listaEvento"] = value; }
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
					BuscarEventos(null);
				}
				calfecha.startDate = cicloLectivoActual.fechaInicio;
				calfecha.endDate = cicloLectivoActual.fechaFin;
				calFechaEdit.startDate = cicloLectivoActual.fechaInicio;
				calFechaEdit.endDate = cicloLectivoActual.fechaFin;
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
						BuscarEventos(null);
						break;
					case enumAcciones.Aceptar:
						break;
					case enumAcciones.Salir:
						break;
					case enumAcciones.Redirect:
						break;
					case enumAcciones.Guardar:
						AccionPagina = enumAcciones.Limpiar;
						GuardarEvento(ObtenerValoresDePantalla());
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
				esNuevo = true;
				btnGuardar.Visible = true;
				btnBuscar.Visible = false;
				btnVolver.Visible = true;
				btnNuevo.Visible = false;
				gvwReporte.Visible = false;
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
				BuscarEventos(propFiltroEvento);
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
						propEvento = new EventoInstitucional();
						propEvento.idEventoInstitucional = Convert.ToInt32(e.CommandArgument.ToString());
						AccionPagina = enumAcciones.Modificar;
						esNuevo = false;
						CargarValoresEnPantalla(Convert.ToInt32(e.CommandArgument.ToString()));
						btnBuscar.Visible = false;
						btnNuevo.Visible = false;
						btnVolver.Visible = true;
						btnGuardar.Visible = true;
						gvwReporte.Visible = false;
						udpFiltrosBusqueda.Visible = false;
						udpEdit.Visible = true;
						udpEdit.Update();
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
		/// Cargars the grilla.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		private void CargarGrilla()
		{
			gvwReporte.DataSource = UIUtilidades.BuildDataTable<EventoInstitucional>(listaEvento).DefaultView;
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
			CargarCombos();
			udpEdit.Visible = false;
			btnVolver.Visible = false;
			btnGuardar.Visible = false;
			udpFiltrosBusqueda.Visible = true;
			btnNuevo.Visible = true;
			btnBuscar.Visible = true;
			gvwReporte.Visible = true;
			udpFiltros.Update();
			udpGrilla.Update();
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		private void CargarCombos()
		{
			List<TipoEventoInstitucional> listaTipoEvento = new List<TipoEventoInstitucional>();
			BLTipoEventoInstitucional objBLTipoEvento = new BLTipoEventoInstitucional();
			listaTipoEvento = objBLTipoEvento.GetTipoEventoInstitucional(null);

			UIUtilidades.BindCombo<TipoEventoInstitucional>(ddlTipoEvento, listaTipoEvento, "idTipoEventoInstitucional", "descripcion", false, true);
			UIUtilidades.BindCombo<TipoEventoInstitucional>(ddlTipoEventoEdit, listaTipoEvento, "idTipoEventoInstitucional", "descripcion", true);
		}

		/// <summary>
		/// Limpiars the campos.
		/// </summary>
		private void LimpiarCampos()
		{
			txtHora.Text = string.Empty;
			txtLugar.Text = string.Empty;
			txtTitulo.Text = string.Empty;
			calfecha.LimpiarControles();
			txtHoraEdit.Text = string.Empty;
			txtLugarEdit.Text = string.Empty;
			txtTituloEdit.Text = string.Empty;
			calFechaEdit.LimpiarControles();
			txtDescripcionEdit.Text = string.Empty;
		}

		/// <summary>
		/// Buscars the filtrando.
		/// </summary>
		private void BuscarFiltrando()
		{
			calfecha.ValidarRangoDesde();
			EventoInstitucional evento = new EventoInstitucional();
			evento.lugar = txtLugar.Text.Trim();
			evento.titulo = txtTitulo.Text.Trim();
			evento.fecha = Convert.ToDateTime(calfecha.ValorFecha);
			evento.activo = chkActivo.Checked;
			evento.tipoEventoInstitucional.idTipoEventoInstitucional = Convert.ToInt32(ddlTipoEvento.SelectedValue);

			if (txtHora.Text.Trim().Length > 1)
				evento.hora = Convert.ToDateTime(txtHora.Text);
			else
				evento.hora = null;
			propFiltroEvento = evento;
			BuscarEventos(evento);
		}

		/// <summary>
		/// Buscars the eventos.
		/// </summary>
		/// <param name="evento">The evento.</param>
		private void BuscarEventos(EventoInstitucional evento)
		{
			objBLEvento = new BLEventoInstitucional(evento);
			listaEvento = objBLEvento.GetEventoInstitucional(evento);

			CargarGrilla();
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private EventoInstitucional ObtenerValoresDePantalla()
		{
			EventoInstitucional evento = new EventoInstitucional();

			if (!esNuevo)
			{
				evento.idEventoInstitucional = propEvento.idEventoInstitucional;
			}
			evento.lugar = txtLugarEdit.Text.Trim();
			evento.fecha = Convert.ToDateTime(calFechaEdit.ValorFecha);
			evento.hora = Convert.ToDateTime(txtHoraEdit.Text.Trim());
			evento.titulo = txtTituloEdit.Text.Trim();
			evento.detalle = txtDescripcionEdit.Text.Trim();
			evento.activo = chkActivoEdit.Checked;
			evento.tipoEventoInstitucional.idTipoEventoInstitucional = Convert.ToInt32(ddlTipoEventoEdit.SelectedValue);
			evento.organizador.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
			return evento;
		}

		/// <summary>
		/// Registrar el evento.
		/// </summary>
		/// <param name="evento">The evento.</param>
		private void GuardarEvento(EventoInstitucional evento)
		{
			objBLEvento = new BLEventoInstitucional(evento);
			objBLEvento.Save();
		}

		/// <summary>
		/// Cargars the evento.
		/// </summary>
		private void CargarValoresEnPantalla(int idEventoInstitucional)
		{
			EventoInstitucional evento = listaEvento.Find(c => c.idEventoInstitucional == idEventoInstitucional);
			txtDescripcionEdit.Text = evento.detalle;
			txtHoraEdit.Text = Convert.ToDateTime(evento.hora).ToString("HH:mm");
			calFechaEdit.Fecha.Text = Convert.ToDateTime(evento.fecha).ToShortDateString();
			txtTituloEdit.Text = evento.titulo;
			txtLugarEdit.Text = evento.lugar;
			ddlTipoEventoEdit.SelectedValue = evento.tipoEventoInstitucional.idTipoEventoInstitucional.ToString();
			chkActivoEdit.Checked = evento.activo;
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			calFechaEdit.ValidarRangoDesde(false);
			string mensaje = string.Empty;
			if (txtDescripcionEdit.Text.Trim().Length == 0)
				mensaje = "- Descripcion<br />";
			if (txtHoraEdit.Text.Trim().Length == 0)
				mensaje += "- Hora<br />";
			if (calFechaEdit.Fecha.Text.Trim().Length == 0)
				mensaje += "- Fecha<br />";
			if (txtTituloEdit.Text.Trim().Length == 0)
				mensaje += "- Titulo<br />";
			if (txtLugarEdit.Text.Trim().Length == 0)
				mensaje += "- Lugar<br />";
			if (!(Convert.ToInt32(ddlTipoEventoEdit.SelectedValue) > 0))
				mensaje += "- Tipo de Evento";

			return mensaje;
		}
		#endregion
	}
}