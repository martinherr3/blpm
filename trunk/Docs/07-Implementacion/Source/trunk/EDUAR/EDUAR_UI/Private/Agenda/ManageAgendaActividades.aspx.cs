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
using EDUAR_Entities;

namespace EDUAR_UI
{
	public partial class ManageAgendaActividades : EDUARBasePage
	{
		#region --[Atributos]--
		private BLAgendaActividades objBLAgenda;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the prop agenda.
		/// </summary>
		/// <value>
		/// The prop agenda.
		/// </value>
		public AgendaActividades propAgenda
		{
			get
			{
				if (ViewState["propAgenda"] == null)
					return new AgendaActividades();

				return (AgendaActividades)ViewState["propAgenda"];
			}
			set { ViewState["propAgenda"] = value; }
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
				if (ViewState["listaentidad"] == null)
					return new List<AgendaActividades>();

				return (List<AgendaActividades>)ViewState["listaentidad"];
			}
			set { ViewState["listaentidad"] = value; }
		}
		#endregion

		#region --[entidads]--
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
					BuscarAgenda(null);
				}
				//this.txtDescripcionEdit.Attributes.Add("onkeyup", " ValidarCaracteres(this, 4000);");
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
						//CargarPresentacion();
						//Buscarentidads(null);
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
				//LimpiarCampos();
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
				//CargarPresentacion();
				//Buscarentidads(propFiltroentidad);
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
						//propentidad = new AgendaActividades();
						//propentidad.idAgendaActividad = Convert.ToInt32(e.CommandArgument.ToString());
						AccionPagina = enumAcciones.Modificar;
						esNuevo = false;
						//CargarValoresEnPantalla(Convert.ToInt32(e.CommandArgument.ToString()));
						litEditar.Visible = true;
						litNuevo.Visible = false;
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
				if (idCicloLectivo > 0)
				{
					List<Curso> listaCurso = new List<Curso>();
					BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
					Curso objCurso = new Curso();

					listaCurso = objBLCicloLectivo.GetCursosByCicloLectivo(Convert.ToInt32(ddlCicloLectivo.SelectedValue));
					UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "nombre", true);
					ddlCurso.Enabled = true;
				}
				else
				{
					ddlCurso.SelectedIndex = 0;
					ddlCurso.Enabled = false;
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
			List<CicloLectivo> listaCicloLectivo = new List<CicloLectivo>();
			BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
			listaCicloLectivo = objBLCicloLectivo.GetCicloLectivos(null);
			//List<AgendaActividades> listaTipoentidad = new List<AgendaActividades>();
			//BLTipoAgendaActividades objBLTipoentidad = new BLTipoAgendaActividades();
			//listaTipoentidad = objBLTipoentidad.GetTipoAgendaActividades(null);

			List<Curso> listaCurso = new List<Curso>();
			UIUtilidades.BindCombo<CicloLectivo>(ddlCicloLectivo, listaCicloLectivo, "idCicloLectivo", "nombre", true);
			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCurso, "idCurso", "Nombre", true);
		
			ddlCurso.Enabled = false;
		}

		/// <summary>
		/// Limpiars the campos.
		/// </summary>
		private void LimpiarCampos()
		{
			ddlCicloLectivo.SelectedValue = "-1";
			//txtHora.Text = string.Empty;
			//txtLugar.Text = string.Empty;
			//txtTitulo.Text = string.Empty;
			//calfecha.LimpiarControles();
			txtHoraEdit.Text = string.Empty;
			txtLugarEdit.Text = string.Empty;
			txtTituloEdit.Text = string.Empty;
			//calFechaEdit.LimpiarControles();
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
			objBLAgenda = new BLAgendaActividades(entidad);
			listaAgenda = objBLAgenda.GetAgendaActividadess(entidad);

			CargarGrilla();
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private AgendaActividades ObtenerValoresDePantalla()
		{
			AgendaActividades entidad = new AgendaActividades();

			if (!esNuevo)
			{
				entidad.idAgendaActividad = propAgenda.idAgendaActividad;
			}
			//entidad.lugar = txtLugarEdit.Text.Trim();
			//entidad.fecha = Convert.ToDateTime(calFechaEdit.ValorFecha);
			//entidad.hora = Convert.ToDateTime(txtHoraEdit.Text.Trim());
			//entidad.titulo = txtTituloEdit.Text.Trim();
			//entidad.detalle = txtDescripcionEdit.Text.Trim();
			//entidad.activo = chkActivoEdit.Checked;
			//entidad.tipoAgendaActividades.idTipoAgendaActividades = Convert.ToInt32(ddlTipoentidadEdit.SelectedValue);
			//entidad.organizador.username = ObjDTSessionDataUI.ObjDTUsuario.Nombre;
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
			//txtDescripcionEdit.Text = entidad.detalle;
			//txtHoraEdit.Text = Convert.ToDateTime(entidad.hora).ToShortTimeString();
			//calFechaEdit.Fecha.Text = Convert.ToDateTime(entidad.fecha).ToShortDateString();
			//txtTituloEdit.Text = entidad.titulo;
			//txtLugarEdit.Text = entidad.lugar;
			//ddlTipoentidadEdit.SelectedValue = entidad.tipoAgendaActividades.idTipoAgendaActividades.ToString();
			chkActivoEdit.Checked = entidad.activo;
		}

		private string ValidarPagina()
		{
			string mensaje = string.Empty;
			if (txtDescripcionEdit.Text.Trim().Length == 0)
				mensaje = "- Descripcion<br />";
			if (txtHoraEdit.Text.Trim().Length == 0)
				mensaje += "- Hora<br />";
			//if (calFechaEdit.Fecha.Text.Trim().Length == 0)
			//    mensaje += "- Fecha<br />";
			if (txtTituloEdit.Text.Trim().Length == 0)
				mensaje += "- Titulo<br />";
			if (txtLugarEdit.Text.Trim().Length == 0)
				mensaje += "- Lugar<br />";
			//if (!(Convert.ToInt32(ddlTipoentidadEdit.SelectedValue) > 0))
			//mensaje += "- Tipo de entidad";

			return mensaje;
		}
		#endregion

	}
}