using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_BusinessLogic.Encuestas;
using EDUAR_Entities.Security;

namespace EDUAR_UI
{
	public partial class ManageContenidoEncuestas : EDUARBasePage
	{
		#region --[Atributos]--
		private BLEncuesta objBLEncuesta;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Mantiene la encuesta seleccionada en la grilla.
		/// </summary>
		/// <value>
		/// The prop encuesta.
		/// </value>
		public Encuesta propEncuesta
		{
			get
			{
				if (ViewState["propEncuesta"] == null)
					propEncuesta = new Encuesta();

				return (Encuesta)ViewState["propEncuesta"];
			}
			set { ViewState["propEncuesta"] = value; }
		}

		/// <summary>
		/// Gets or sets the prop filtro encuesta.
		/// </summary>
		/// <value>
		/// The prop filtro encuesta.
		/// </value>
		public Encuesta propFiltroEncuesta
		{
			get
			{
				if (ViewState["propFiltroEncuesta"] == null)
					propFiltroEncuesta = new Encuesta();

				return (Encuesta)ViewState["propFiltroEncuesta"];
			}
			set { ViewState["propFiltroEncuesta"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista encuesta.
		/// </summary>
		/// <value>
		/// The lista agenda.
		/// </value>
		public List<Encuesta> listaEncuesta
		{
			get
			{
				if (ViewState["listaEncuesta"] == null) listaEncuesta = new List<Encuesta>();

				return (List<Encuesta>)ViewState["listaEncuesta"];
			}
			set { ViewState["listaEncuesta"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista de ambitos.
		/// </summary>
		/// <value>
		/// The lista ambitos.
		/// </value>
		public List<AmbitoEncuesta> listaAmbitos
		{
			get
			{
				if (ViewState["listaAmbitos"] == null)
				{
					BLAmbitoEncuesta objBLAmbitoEncuesta = new BLAmbitoEncuesta();

					listaAmbitos = objBLAmbitoEncuesta.GetAmbitosEncuesta(null);
				}
				return (List<AmbitoEncuesta>)ViewState["listaAmbitos"];
			}
			set { ViewState["listaAmbitos"] = value; }
		}

		public int idEncuesta
		{
			get
			{
				if (ViewState["idEncuesta"] == null) idEncuesta = 0;

				return (int)ViewState["idEncuesta"];
			}
			set { ViewState["idEncuesta"] = value; }
		}

		public int idAmbito
		{
			get
			{
				if (ViewState["idAmbito"] == null) idAmbito = 0;

				return (int)ViewState["idAmbito"];
			}
			set { ViewState["idAmbito"] = value; }
		}

		public Encuesta encuestaSesion
		{
			get
			{
				if (Session["encuestaSesion"] == null)
					encuestaSesion = new Encuesta();

				return (Encuesta)Session["encuestaSesion"];
			}
			set { Session["encuestaSesion"] = value; }
		}
		#endregion

		#region --[Eventos]--
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
					BuscarEncuesta(null);

					if (Request.UrlReferrer.AbsolutePath.Contains("ContenidoEncuestas.aspx"))
					{
						//Lo que se hace en este bloque es restablecer los elementos a su estado anterior, dado que está volviendo desde otra página
					}
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
		void VentanaAceptar(object sender, EventArgs e)
		{
			try
			{
				switch (AccionPagina)
				{
					case enumAcciones.Limpiar:
						CargarPresentacion();
						BuscarEncuesta(null);
						break;
					case enumAcciones.Guardar:
						AccionPagina = enumAcciones.Limpiar;
						GuardarEncuesta(ObtenerValoresDePantalla());
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
				CargarCombos();
				btnGuardar.Visible = true;
				btnBuscar.Visible = false;
				btnVolver.Visible = true;
				btnNuevo.Visible = false;
				gvwEncuestas.Visible = false;
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
				BuscarEncuesta(propFiltroEncuesta);
				propEncuesta = new Encuesta();
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
		protected void gvwEncuestas_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Editar":
						propEncuesta.idEncuesta = Convert.ToInt32(e.CommandArgument.ToString());
						CargaEncuesta();
						break;
					case "Preguntas":
						AccionPagina = enumAcciones.Redirect;
						idEncuesta = Convert.ToInt32(e.CommandArgument.ToString());

						encuestaSesion = listaEncuesta.Find(p => p.idEncuesta == idEncuesta);

						Response.Redirect("ContenidoEncuestas.aspx", false);
						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwEncuesta control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		protected void gvwEncuesta_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
				gvwEncuestas.PageIndex = e.NewPageIndex;
				CargarGrilla();
				btnBuscar.Visible = false;
				btnVolver.Visible = true;
				gvwEncuestas.Visible = false;
				udpFiltrosBusqueda.Visible = false;
				udpEdit.Visible = true;
				udpEdit.Update();
			}
			catch (Exception ex) { Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the ddlAmbitoEdit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ddlAmbitoEdit_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int idAmbito = 0;
				ltbRoles.Items.Clear();
				if (int.TryParse(ddlAmbitoEdit.SelectedValue, out idAmbito) && idAmbito > 0)
				{
					CargarRolesAmbito(idAmbito);
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
			gvwEncuestas.DataSource = UIUtilidades.BuildDataTable<Encuesta>(listaEncuesta).DefaultView;
			gvwEncuestas.DataBind();
			udpEdit.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Carga el contenido de la grilla de encuestas.
		/// </summary>
		private void CargarPresentacion()
		{
			LimpiarCampos();
			lblTitulo.Text = "Encuesta";
			CargarCombos();
			udpEdit.Visible = false;
			btnVolver.Visible = false;
			btnGuardar.Visible = false;
			udpFiltrosBusqueda.Visible = true;
			btnBuscar.Visible = true;
			btnNuevo.Visible = true;
			gvwEncuestas.Visible = true;
			udpFiltros.Update();
			udpGrilla.Update();
		}

		/// <summary>
		/// Buscars the entidads.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void BuscarEncuesta(Encuesta entidad)
		{
			CargarLista(entidad);
			CargarGrilla();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void CargarLista(Encuesta entidad)
		{
			objBLEncuesta = new BLEncuesta(entidad);
			listaEncuesta = objBLEncuesta.GetEncuestas(entidad);
		}

		/// <summary>
		/// Cargars the combos.
		/// </summary>
		private void CargarCombos()
		{
			UIUtilidades.BindCombo<AmbitoEncuesta>(ddlAmbito, listaAmbitos, "idAmbitoEncuesta", "nombre", true);
			UIUtilidades.BindCombo<AmbitoEncuesta>(ddlAmbitoEdit, listaAmbitos, "idAmbitoEncuesta", "nombre", true);
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private Encuesta ObtenerValoresDePantalla()
		{
			Encuesta entidad = new Encuesta();
			entidad = propEncuesta;

			if (!esNuevo)
			{
				entidad.idEncuesta = propEncuesta.idEncuesta;
				entidad.fechaModificacion = DateTime.Now;
			}

			if (Convert.ToInt32(ddlAmbitoEdit.SelectedValue) > 0)
			{
				entidad.ambito.idAmbitoEncuesta = Convert.ToInt32(ddlAmbitoEdit.SelectedValue);
				entidad.nombreEncuesta = txtNombreEdit.Text.Trim();
				entidad.fechaCreacion = DateTime.Now;
				entidad.activo = chkActivoEdit.Checked;
				entidad.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
				entidad.objetivo = txtObjetivoEdit.Text.Trim();
			}

			return entidad;
		}

		/// <summary>
		/// Guardars the encuesta.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void GuardarEncuesta(Encuesta entidad)
		{
			objBLEncuesta = new BLEncuesta(entidad);
			objBLEncuesta.Save();
		}

		/// <summary>
		/// Cargars the entidad.
		/// </summary>
		private void CargarValoresEnPantalla(int idEntidad)
		{
			Encuesta encuesta = listaEncuesta.Find(c => c.idEncuesta == idEntidad);

			chkActivoEdit.Checked = encuesta.activo;
			ddlAmbitoEdit.SelectedValue = encuesta.ambito.idAmbitoEncuesta.ToString();
			txtNombreEdit.Text = encuesta.nombreEncuesta;
			txtObjetivoEdit.Text = encuesta.objetivo;
			CargarRolesAmbito(encuesta.ambito.idAmbitoEncuesta);
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
		private void CargaEncuesta()
		{
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;

			CargarValoresEnPantalla(propEncuesta.idEncuesta);

			litEditar.Visible = true;
			litNuevo.Visible = false;
			btnBuscar.Visible = false;
			btnNuevo.Visible = false;
			btnVolver.Visible = true;
			btnGuardar.Visible = true;
			gvwEncuestas.Visible = false;
			udpFiltrosBusqueda.Visible = false;
			udpEdit.Visible = true;
			udpFiltros.Update();
			udpEdit.Update();
		}

		/// <summary>
		/// Buscars the filtrando.
		/// </summary>
		private void BuscarFiltrando()
		{
			lblTitulo.Text = "Encuestas";
			Encuesta entidad = new Encuesta();
			entidad.activo = chkActivo.Checked;

			AmbitoEncuesta ambito = new AmbitoEncuesta();
			ambito.idAmbitoEncuesta = Convert.ToInt32(ddlAmbito.SelectedValue);

			entidad.ambito = ambito;

			propFiltroEncuesta = entidad;
			BuscarEncuesta(entidad);
		}

		/// <summary>
		/// Limpiar the campos.
		/// </summary>
		private void LimpiarCampos()
		{
			if (ddlAmbito.Items.Count > 0) ddlAmbito.SelectedIndex = 0;
			chkActivo.Checked = false;
		}

		/// <summary>
		/// Cargars the roles ambito.
		/// </summary>
		/// <param name="idAmbito">The id ambito.</param>
		private void CargarRolesAmbito(int idAmbito)
		{
			ltbRoles.Items.Clear();
			List<DTRol> listaRoles = (new BLEncuesta()).GetRolesAmbito(new AmbitoEncuesta() { idAmbitoEncuesta = idAmbito });
			foreach (DTRol item in listaRoles)
			{
				ltbRoles.Items.Add(new ListItem(item.Nombre));
			}
			udpAmbitoRol.Update();
		}
		#endregion
	}
}