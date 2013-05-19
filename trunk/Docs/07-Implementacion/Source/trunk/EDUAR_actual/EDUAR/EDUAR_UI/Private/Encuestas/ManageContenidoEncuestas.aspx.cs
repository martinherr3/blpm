using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Encuestas;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

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

		/// <summary>
		/// Gets or sets the encuesta sesion.
		/// </summary>
		/// <value>
		/// The encuesta sesion.
		/// </value>
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

                    encuestaSesion.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

                    BuscarEncuesta(encuestaSesion);

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
					case enumAcciones.Enviar:
						AccionPagina = enumAcciones.Limpiar;
						LanzarEncuesta();
						BuscarFiltrando();
						break;
					case enumAcciones.Eliminar:
						AccionPagina = enumAcciones.Limpiar;
						EliminarEncuesta();
						BuscarFiltrando();
						break;
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
				lstRoles.Enabled = true;
				ddlAmbitoEdit.Enabled = true;
				ddlCurso.Enabled = true;
				ddlAsignatura.Visible = false;
				lblAsignatura.Visible = false;
				ddlAsignatura.Enabled = true;
				btnGuardar.Visible = true;
				btnBuscar.Visible = false;
				btnVolver.Visible = true;
				btnNuevo.Visible = false;
				gvwEncuestas.Visible = false;
				udpEdit.Visible = true;
				udpFiltrosBusqueda.Visible = false;
				udpAsignatura.Update();
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
					Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
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
                ViewState.Clear();
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
						ddlAmbitoEdit.Enabled = false;
						ddlAsignatura.Enabled = false;
						ddlCurso.Enabled = false;
						lstRoles.Enabled = false;
						break;
					case "Eliminar":
						propEncuesta.idEncuesta = Convert.ToInt32(e.CommandArgument.ToString());
						AccionPagina = enumAcciones.Eliminar;
						Master.MostrarMensaje("Eliminar Encuesta", "¿Desea <b>eliminar</b> la encuesta?", enumTipoVentanaInformacion.Confirmación);
						break;
					case "Lanzar":
						propEncuesta.idEncuesta = Convert.ToInt32(e.CommandArgument.ToString());
						Encuesta miEncuesta = listaEncuesta.Find(p => p.idEncuesta == propEncuesta.idEncuesta);
						ValidarPreguntas(miEncuesta);
						AccionPagina = enumAcciones.Enviar;
						Master.MostrarMensaje("Lanzar Encuesta", "¿Desea <b>enviar</b> la encuesta a los usuarios?", enumTipoVentanaInformacion.Confirmación);
						break;
					case "Preguntas":
						AccionPagina = enumAcciones.Redirect;
						idEncuesta = Convert.ToInt32(e.CommandArgument.ToString());
						encuestaSesion = listaEncuesta.Find(p => p.idEncuesta == idEncuesta);
						Response.Redirect("ContenidoEncuestas.aspx", false);
						break;
					case "Resultados":
						AccionPagina = enumAcciones.Redirect;
						idEncuesta = Convert.ToInt32(e.CommandArgument.ToString());
						encuestaSesion = listaEncuesta.Find(p => p.idEncuesta == idEncuesta);
						Response.Redirect("ResultadosEncuestas.aspx", false);
						break;
				}
				udpAmbitoRol.Update();
				udpAsignatura.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Validars the preguntas.
		/// </summary>
		/// <param name="propEncuesta">The prop encuesta.</param>
		private void ValidarPreguntas(Encuesta propEncuesta)
		{
			BLEncuesta objBLEncuesta = new BLEncuesta(propEncuesta);
			objBLEncuesta.ValidarLanzamiento();
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
				lstRoles.Items.Clear();
				if (int.TryParse(ddlAmbitoEdit.SelectedValue, out idAmbito) && idAmbito > 0)
				{
					CargarRolesAmbito(idAmbito);
					CargarComboAsignatura();
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
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
				CargarComboAsignatura();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Checks the lanzada.
		/// </summary>
		/// <param name="objUsername">The obj username.</param>
		/// <param name="objFechaLanzamiento">The obj fecha lanzamiento.</param>
		/// <param name="editar">if set to <c>true</c> [editar].</param>
		/// <returns></returns>
		protected bool CheckLanzada(object objUsername, object objFechaLanzamiento, bool editar)
		{
			bool hayUsuario = false;
			if (object.ReferenceEquals(objUsername, null))
				return hayUsuario;
			if (editar)
			{
				if (objUsername.ToString() == User.Identity.Name)
				{
					if (objFechaLanzamiento.ToString() == string.Empty)
						return true;
					else
						return false;
				}
				else
					return false;
			}
			else
			{
				if (objUsername.ToString() == User.Identity.Name)
				{
					if (objFechaLanzamiento.ToString() == string.Empty)
						return false;
					else
						return true;
				}
				else
					return false;
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
			calFechaCierre.startDate = cicloLectivoActual.fechaInicio;
			calFechaCierre.endDate = cicloLectivoActual.fechaFin;
			lblTitulo.Text = "Encuestas";
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
			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "nombre", true, true);
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
				entidad.listaRoles.Clear();
				foreach (ListItem item in lstRoles.Items)
				{
					if (item.Selected)
						entidad.listaRoles.Add(new DTRol() { Nombre = item.Text });
				}

				entidad.nombreEncuesta = txtNombreEdit.Text.Trim();
				entidad.fechaCreacion = DateTime.Now;
				entidad.activo = chkActivoEdit.Checked;
				entidad.usuario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
				entidad.objetivo = txtObjetivoEdit.Text.Trim();
				DateTime fechaVencimiento;

				if (calFechaCierre.ValorFecha.HasValue)
					if (DateTime.TryParse(calFechaCierre.ValorFecha.Value.ToString(), out fechaVencimiento))
						entidad.fechaVencimiento = fechaVencimiento;

				if (entidad.ambito.idAmbitoEncuesta == enumAmbitoEncuesta.Asignatura.GetHashCode())
					entidad.asignatura.idAsignaturaCicloLectivo = Convert.ToInt32(ddlAsignatura.SelectedValue);
				entidad.curso.idCursoCicloLectivo = Convert.ToInt32(ddlCurso.SelectedValue);
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

			if (encuesta.curso.idCursoCicloLectivo > 0)
				ddlCurso.SelectedValue = encuesta.curso.idCursoCicloLectivo.ToString();
			else
				ddlCurso.SelectedValue = (-2).ToString();

			if (encuesta.listaRoles != null)
				foreach (DTRol item in encuesta.listaRoles)
					(lstRoles.Items.FindByText(item.Nombre)).Selected = true;

			if (encuesta.fechaVencimiento.HasValue)
				calFechaCierre.Fecha.Text = encuesta.fechaVencimiento.ToString();

			if (encuesta.ambito.idAmbitoEncuesta == enumAmbitoEncuesta.Asignatura.GetHashCode())
			{
				CargarComboAsignatura();
				ddlAsignatura.SelectedValue = encuesta.asignatura.idAsignaturaCicloLectivo.ToString();
				lblAsignatura.Visible = true;
				ddlAsignatura.Visible = true;
			}
			else
			{
				ddlAsignatura.Items.Clear();
				lblAsignatura.Visible = false;
				ddlAsignatura.Visible = false;
			}
			lstRoles.Enabled = false;
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			string mensaje = string.Empty;
			calFechaCierre.ValidarRangoDesde(false, true);

			if (!calFechaCierre.ValorFecha.HasValue)
				mensaje += "- Fecha de cierre<br />";

			if (string.IsNullOrEmpty(txtNombreEdit.Text))
				mensaje += "- Nombre Encuesta<br />";

			int validador = 0;

			int.TryParse(ddlAmbitoEdit.SelectedValue, out validador);

			if (validador <= 0)
				mensaje += "- Ambito<br />";

			if (validador == enumAmbitoEncuesta.Asignatura.GetHashCode())
			{
				int.TryParse(ddlCurso.SelectedValue, out validador);
				if (validador == -2)
					mensaje += "- UN Curso<br />";
			}

			int.TryParse(ddlCurso.SelectedValue, out validador);
			if (validador <= 0 && validador != -2)
				mensaje += "- Curso<br />";

			bool hayAmbito = false;
			foreach (ListItem item in lstRoles.Items)
				if (item.Selected)
				{
					hayAmbito = true;
					break;
				}

			if (!hayAmbito)
				mensaje += "- Rol o Roles a Asociar<br />";

			int.TryParse(ddlAsignatura.SelectedValue, out validador);
			if (ddlAsignatura.Visible)
			{
				if (validador <= 0)
					mensaje += "- Asignatura<br />";
			}

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

            //litEditar.Visible = true;
            //litNuevo.Visible = false;
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
			lstRoles.Items.Clear();
			txtNombreEdit.Text = string.Empty;
			txtObjetivoEdit.Text = string.Empty;
		}

		/// <summary>
		/// Cargars the roles ambito.
		/// </summary>
		/// <param name="idAmbito">The id ambito.</param>
		private void CargarRolesAmbito(int idAmbito)
		{
			lstRoles.Items.Clear();
			List<DTRol> listaRoles = (new BLEncuesta()).GetRolesAmbito(new AmbitoEncuesta() { idAmbitoEncuesta = idAmbito });
			foreach (DTRol item in listaRoles)
			{
				lstRoles.Items.Add(new ListItem(item.Nombre));
			}
			udpAmbitoRol.Update();
		}

		/// <summary>
		/// Cargars the combo asignatura.
		/// </summary>
		private void CargarComboAsignatura()
		{
			int idAmbito = 0;
			int idCursoSeleccionado = 0;
			int.TryParse(ddlAmbitoEdit.SelectedValue, out idAmbito);

			ddlAsignatura.Items.Clear();
			if (idAmbito > 0 && idAmbito == enumAmbitoEncuesta.Asignatura.GetHashCode())
			{
				if (int.TryParse(ddlCurso.SelectedValue, out idCursoSeleccionado) && idCursoSeleccionado > 0)
				{
					lblAsignatura.Visible = (idAmbito > 0 && idCursoSeleccionado > 0);
					ddlAsignatura.Visible = (idAmbito > 0 && idCursoSeleccionado > 0);
					udpAsignatura.Update();
					BLAsignatura objBLAsignatura = new BLAsignatura();
					Asignatura objAsignatura = new Asignatura();
					objAsignatura.idCursoCicloLectivo = idCursoSeleccionado;
					objAsignatura.curso.cicloLectivo.idCicloLectivo = cicloLectivoActual.idCicloLectivo;
					if (User.IsInRole(enumRoles.Docente.ToString()))
						objAsignatura.docente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;

					lblAsignatura.Visible = true;
					ddlAsignatura.Visible = true;
					UIUtilidades.BindCombo<Asignatura>(ddlAsignatura, objBLAsignatura.GetAsignaturasCurso(objAsignatura), "idAsignatura", "nombre", true);
				}
				else
				{
					lblAsignatura.Visible = false;
					ddlAsignatura.Visible = false;
				}
			}
			else
			{
				lblAsignatura.Visible = false;
				ddlAsignatura.Visible = false;
			}
		}

		/// <summary>
		/// Lanzars the encuesta.
		/// </summary>
		private void LanzarEncuesta()
		{
			Encuesta encuesta = new Encuesta() { idEncuesta = propEncuesta.idEncuesta };
			objBLEncuesta = new BLEncuesta();
			objBLEncuesta.LanzarEncuesta(encuesta);
		}

		/// <summary>
		/// Eliminars the encuesta.
		/// </summary>
		private void EliminarEncuesta()
		{
			Encuesta encuesta = new Encuesta() { idEncuesta = propEncuesta.idEncuesta };
			objBLEncuesta = new BLEncuesta(encuesta);
			objBLEncuesta.Delete();
		}
		#endregion
	}
}