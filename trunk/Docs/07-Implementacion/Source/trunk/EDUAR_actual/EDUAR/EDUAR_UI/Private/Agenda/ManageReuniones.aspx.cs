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
	public partial class ManageReuniones : EDUARBasePage
	{
		#region --[Atributos]--
		//private BLReunion objBLReunion;
        private BLAgendaActividades objBLAgenda;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Mantiene la agenda seleccionada en la grilla.
		/// Se utiliza para el manejo de eventos de agenda (evaluación, excursión, reunión).
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
		public Reunion propFiltroEvento
		{
			get
			{
				if (ViewState["propFiltroEvento"] == null)
					propFiltroEvento = new Reunion();

				return (Reunion)ViewState["propFiltroEvento"];
			}
			set { ViewState["propFiltroEvento"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista evento.
		/// </summary>
		/// <value>
		/// The lista evento.
		/// </value>
		public List<Reunion> listaEventos
		{
			get
			{
                if (ViewState["listaEventos"] == null)
					listaEventos = new List<Reunion>();

				return (List<Reunion>)ViewState["listaEventos"];
			}
			set { ViewState["listaEventos"] = value; }
		}

		/// <summary>
		/// Gets or sets the prop evento.
		/// </summary>
		/// <value>
		/// The prop evento.
		/// </value>
		public Reunion propEvento
		{
			get
			{
				if (ViewState["propEvento"] == null)
					propEvento = new Reunion();

				return (Reunion)ViewState["propEvento"];
			}
			set { ViewState["propEvento"] = value; }
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
				}
				calfechas.startDate = cicloLectivoActual.fechaInicio;
				calfechas.endDate = cicloLectivoActual.fechaFin;
				calFechaEvento.startDate = cicloLectivoActual.fechaInicio;
				calFechaEvento.endDate = cicloLectivoActual.fechaFin;
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
                        //BuscarAgenda(propEvento);
                        BuscarFiltrando();
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
				//TODO: validar que para la misma persona que está dando de alta la reunión, no exista otra en el mismo horario, 
				//tuvimos en cuenta la duración aproximada? para poder tener varias reuniones en un día
				//validar que no exista ya otra reunión para el curso en el mismo día - horario
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
						CargarEvento();
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
			gvwReporte.DataSource = UIUtilidades.BuildDataTable<Reunion>(listaEventos).DefaultView;
			gvwReporte.DataBind();
			udpEdit.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			lblTitulo.Text = propAgenda.cursoCicloLectivo.curso.nombre + " - " + propAgenda.cursoCicloLectivo.cicloLectivo.nombre;
			LimpiarCampos();
			//CargarCombos();
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
			txtHoraEdit.Text = string.Empty;
			chkActivo.Checked = true;
            chkActivoEdit.Checked = false;
            calFechaEvento.LimpiarControles();
            calfechas.LimpiarControles();
            txtDescripcionEdit.Text = string.Empty;
		}

		/// <summary>
		/// Buscars the filtrando.
		/// </summary>
		private void BuscarFiltrando()
		{
			//calfechas.ValidarRangoDesde();
            calfechas.ValidarRangoDesdeHasta(false);
			Reunion evento = new Reunion();

			//evento.fechaEvento = Convert.ToDateTime(calfechas.ValorFecha);
			evento.activo = chkActivo.Checked;
            evento.fechaEventoDesde = Convert.ToDateTime(calfechas.ValorFechaDesde);
            evento.fechaEventoHasta = Convert.ToDateTime(calfechas.ValorFechaHasta);

			if (txtHoraEdit.Text.Trim().Length > 1)
				evento.horario = Convert.ToDateTime(txtHoraEdit.Text);
			
            propFiltroEvento = evento;
            BuscarAgenda(evento);
		}

		/// <summary>
		/// Buscars the eventos.
		/// </summary>
		/// <param name="evento">The evento.</param>
		private void BuscarAgenda(Reunion evento)
		{
			CargarLista(evento);
			CargarGrilla();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="evento">The evento.</param>
		private void CargarLista(Reunion evento)
		{
			objBLAgenda = new BLAgendaActividades();
            evento.idAgendaActividad = propAgenda.idAgendaActividad;
			listaEventos = objBLAgenda.GetReunionesAgenda(evento);
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private Reunion ObtenerValoresDePantalla()
		{
			Reunion evento = new Reunion();
            evento = propEvento;

			if (!esNuevo)
			{
                evento.idAgendaActividad = propAgenda.idAgendaActividad;
                evento.idEventoAgenda = propEvento.idEventoAgenda;
			}

			evento.fechaEvento = Convert.ToDateTime(calFechaEvento.ValorFecha);
			evento.horario = Convert.ToDateTime(txtHoraEdit.Text.Trim());
			evento.descripcion = txtDescripcionEdit.Text.Trim();
			evento.activo = chkActivoEdit.Checked;
			evento.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
            evento.fechaAlta = DateTime.Now;

            return evento;
		}

		/// <summary>
		/// Registrar el evento.
		/// </summary>
		/// <param name="evento">The evento.</param>
		private void GuardarEvento(Reunion evento)
		{
            objBLAgenda = new BLAgendaActividades(propAgenda);
            objBLAgenda.GetById();
            objBLAgenda.VerificarAgendaReuniones(evento);

            objBLAgenda.Data.listaReuniones.Add(evento);
            objBLAgenda.Save();
		}

		/// <summary>
		/// Cargars the evento.
		/// </summary>
		private void CargarValoresEnPantalla(int idEventoAgenda)
		{
			propEvento = listaEventos.Find(c => c.idEventoAgenda == idEventoAgenda);
            
            txtDescripcionEdit.Text = propEvento.descripcion;
            txtHoraEdit.Text = Convert.ToDateTime(propEvento.horario).ToString("HH:mm");
            calFechaEvento.Fecha.Text = Convert.ToDateTime(propEvento.fechaEvento).ToShortDateString();
            chkActivoEdit.Checked = propEvento.activo;
		}

		private string ValidarPagina()
		{
			calFechaEvento.ValidarRangoDesde(true);
			string mensaje = string.Empty;
			if (txtDescripcionEdit.Text.Trim().Length == 0)
				mensaje = "- Descripcion<br />";
			if (txtHoraEdit.Text.Trim().Length == 0)
				mensaje += "- Hora<br />";
			if (calFechaEvento.Fecha.Text.Trim().Length == 0)
				mensaje += "- Fecha<br />";

			return mensaje;
		}

		/// <summary>
		/// Cargars the evento.
		/// </summary>
		private void CargarEvento()
		{
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;
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
		#endregion

	}

}