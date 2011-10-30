using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Security;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
	public partial class Welcome : EDUARBasePage
	{
		#region --[Atributos]--

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
				if (ViewState["propAgenda"] == null)
					propAgenda = new AgendaActividades();

				return (AgendaActividades)ViewState["propAgenda"];
			}
			set { ViewState["propAgenda"] = value; }
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
					//nombre del usuario logueado
					objFiltro.docente.username = User.Identity.Name;
					listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
				}
				return (List<Curso>)ViewState["listaCursos"];
			}
			set { ViewState["listaCursos"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista alumnos.
		/// </summary>
		/// <value>
		/// The lista alumnos.
		/// </value>
		public List<AlumnoCursoCicloLectivo> listaAlumnos
		{
			get
			{
				if (ViewState["listaAlumnos"] == null)
				{
					List<Tutor> lista = new List<Tutor>();
					lista.Add(new Tutor() { username = User.Identity.Name });
					BLAlumno objBLAlumno = new BLAlumno(new Alumno() { listaTutores = lista });
					listaAlumnos = objBLAlumno.GetAlumnosTutor(cicloLectivoActual);
				}
				return (List<AlumnoCursoCicloLectivo>)ViewState["listaAlumnos"];
			}
			set { ViewState["listaAlumnos"] = value; }
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
					//Cargo en sesión los datos del usuario logueado
					DTSeguridad propSeguridad = new DTSeguridad();
					propSeguridad.Usuario.Nombre = User.Identity.Name;
					BLSeguridad objBLSeguridad = new BLSeguridad(propSeguridad);
					objBLSeguridad.GetUsuario();
					ObjSessionDataUI.ObjDTUsuario = objBLSeguridad.Data.Usuario;

					if (User.IsInRole(enumRoles.Alumno.ToString()))
					{
						habilitarAlumno(false);
						habilitarCurso(false);
						divAgenda.Visible = true;
						lblCurso.Visible = false;
						ddlCurso.Visible = false;
					}
					if (User.IsInRole(enumRoles.Docente.ToString()))
					{
						divAgenda.Visible = true;
						habilitarAlumno(false);
						UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "Nombre", true);
					}
					if (User.IsInRole(enumRoles.Tutor.ToString()))
					{
						divAgenda.Visible = true;
						habilitarCurso(false);
						ddlAlumnos.Items.Clear();
						ddlAlumnos.DataSource = null;
						foreach (AlumnoCursoCicloLectivo item in listaAlumnos)
						{
							ddlAlumnos.Items.Insert(ddlAlumnos.Items.Count, new ListItem(item.alumno.apellido + " " + item.alumno.nombre, item.alumno.idAlumno.ToString()));
						}
						UIUtilidades.SortByText(ddlAlumnos);
						ddlAlumnos.Items.Insert(0, new ListItem("Seleccione", "-1"));
						ddlAlumnos.SelectedValue = "-1";
					}

					fechas.startDate = cicloLectivoActual.fechaInicio;
					fechas.endDate = cicloLectivoActual.fechaFin;
					fechas.setSelectedDate(DateTime.Now, DateTime.Now.AddDays(15));

					BLMensaje objBLMensaje = new BLMensaje();
					List<Mensaje> objMensajes = new List<Mensaje>();
					objMensajes = objBLMensaje.GetMensajes(new Mensaje() { destinatario = new Persona() { username = ObjSessionDataUI.ObjDTUsuario.Nombre }, activo = true });
					objMensajes = objMensajes.FindAll(p => p.leido == false);
					if (objMensajes.Count > 0)
					{
						string mensaje = objMensajes.Count == 1 ? "mensaje" : "mensajes";

						lblMensajes.Text = lblMensajes.Text.Replace("<MENSAJES>", objMensajes.Count.ToString());
						lblMensajes.Text = lblMensajes.Text.Replace("<MSJ_STRING>", mensaje);
					}
					else
						divMensajes.Visible = false;
					CargarAgenda();
				}
				else
				{
					fechas.setSelectedDate(
							(fechas.ValorFechaDesde != null) ? (DateTime)fechas.ValorFechaDesde : DateTime.Now,
							(fechas.ValorFechaHasta != null) ? (DateTime)fechas.ValorFechaHasta : DateTime.Now.AddDays(15));
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
				//switch (AccionPagina)
				//{
				//    case enumAcciones.Limpiar:
				//        CargarPresentacion();
				//        BuscarAgenda(null);
				//        break;
				//    case enumAcciones.Guardar:
				//        AccionPagina = enumAcciones.Limpiar;
				//        GuardarAgenda(ObtenerValoresDePantalla());
				//        Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
				//        break;
				//    default:
				//        break;
				//}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
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
			}
			catch (Exception ex) { Master.ManageExceptions(ex); }
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
				fechas.ValidarRangoDesdeHasta(false);
				CargarAgenda();
				//if (BuscarSanciones())
				//{
				//    divFiltros.Visible = false;
				//    divReporte.Visible = true;
				//}
				//else
				//{ Master.MostrarMensaje("Faltan Datos", UIConstantesGenerales.MensajeDatosRequeridos, EDUAR_Utility.Enumeraciones.enumTipoVentanaInformacion.Advertencia); }
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the Click event of the btnMensaje control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnMensaje_Click(object sender, EventArgs e)
		{
			try
			{
				Response.Redirect("~/Private/Mensajes/MsjeEntrada.aspx", false);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Métodos Privados]
		/// <summary>
		/// Cargars the agenda.
		/// </summary>
		private void CargarAgenda()
		{
			CargarValoresEnPantalla();
			propAgenda.listaEventos.Sort((p, q) => DateTime.Compare(p.fechaEvento, q.fechaEvento));
			CargarGrillaAgenda();
			udpGrilla.Update();
		}

		/// <summary>
		/// Cargars the entidad.
		/// </summary>
		private void CargarValoresEnPantalla()
		{
			BLAgendaActividades objBLAgenda = new BLAgendaActividades();
			DateTime fechaDesde = DateTime.Now;
			DateTime fechaHasta = DateTime.Now.AddDays(15);
			if (fechas.ValorFechaDesde != null)
				fechaDesde = (DateTime)fechas.ValorFechaDesde;
			if (fechas.ValorFechaHasta != null)
				fechaHasta = (DateTime)fechas.ValorFechaHasta;
			enumRoles obj = (enumRoles)Enum.Parse(typeof(enumRoles), ObjSessionDataUI.ObjDTUsuario.ListaRoles[0].Nombre);
			CursoCicloLectivo objCurso = new CursoCicloLectivo();
			List<EventoAgenda> listaEventos = new List<EventoAgenda>();
			switch (obj)
			{
				case enumRoles.Alumno:
					objCurso.cicloLectivo = cicloLectivoActual;
					listaEventos = objBLAgenda.GetAgendaActividadesByRol(new Alumno() { username = ObjSessionDataUI.ObjDTUsuario.Nombre }, null, objCurso, fechaDesde, fechaHasta);
					break;
				case enumRoles.Docente:
					if (Convert.ToInt16(ddlCurso.SelectedValue) > 0)
					{
						objCurso.idCursoCicloLectivo = Convert.ToInt16(ddlCurso.SelectedValue);
						listaEventos = objBLAgenda.GetAgendaActividadesByRol(null, null, objCurso, fechaDesde, fechaHasta);
					}
					break;
				case enumRoles.Tutor:
					if (Convert.ToInt16(ddlAlumnos.SelectedValue) > 0)
					{
						objCurso.idCursoCicloLectivo = (listaAlumnos.Find(p => p.alumno.idAlumno == Convert.ToInt16(ddlAlumnos.SelectedValue))).cursoCicloLectivo.idCursoCicloLectivo;
						listaEventos = objBLAgenda.GetAgendaActividadesByRol(null, null, objCurso, fechaDesde, fechaHasta);
					}
					break;
				default:
					break;
			}
			propAgenda.listaEventos = listaEventos;
		}

		/// <summary>
		/// Cargars the grilla agenda.
		/// </summary>
		private void CargarGrillaAgenda()
		{
			gvwAgenda.DataSource = UIUtilidades.BuildDataTable<EventoAgenda>(propAgenda.listaEventos).DefaultView;
			gvwAgenda.DataBind();
		}

		/// <summary>
		/// Habilitars the curso.
		/// </summary>
		/// <param name="mostrar">if set to <c>true</c> [mostrar].</param>
		private void habilitarCurso(bool mostrar)
		{
			lblCurso.Visible = mostrar;
			ddlCurso.Visible = mostrar;
		}

		/// <summary>
		/// Habilitars the alumno.
		/// </summary>
		/// <param name="mostrar">if set to <c>true</c> [mostrar].</param>
		private void habilitarAlumno(bool mostrar)
		{
			lblAlumnos.Visible = mostrar;
			ddlAlumnos.Visible = mostrar;
		}
		#endregion
	}
}