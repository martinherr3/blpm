using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;
using System.Web;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI
{
	public partial class MsjeRedactar : EDUARBasePage
	{
		#region --[Propiedades]
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
					CargarLista();
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
		protected void VentanaAceptar(object sender, EventArgs e)
		{
			try
			{
				switch (AccionPagina)
				{
					case enumAcciones.Salir:
						Response.Redirect("MsjeEntrada.aspx", false);
						break;
					case enumAcciones.Error:
						//Response.Redirect("MsjeRedactar.aspx", false);
						break;
					case enumAcciones.Enviar:
						PrepararEnvioMensaje();
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
		/// Handles the Click event of the btnEnviar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnEnviar_Click(object sender, EventArgs e)
		{
			try
			{
				bool destino = false;
				foreach (ListItem item in ddlDestino.Items)
				{
					if (item.Selected)
					{
						destino = true;
						break;
					}
				}
				if (destino)
				{
					if (txtAsunto.Value != "")
					{
						PrepararEnvioMensaje();
					}
					else
					{
						AccionPagina = enumAcciones.Enviar;
						Master.MostrarMensaje(this.Page.Title, UIConstantesGenerales.MensajeSinAsunto, enumTipoVentanaInformacion.Confirmación);
					}
				}
				else
				{
					AccionPagina = enumAcciones.Error;
					Master.MostrarMensaje(this.Page.Title, UIConstantesGenerales.MensajeSinDestinatario, enumTipoVentanaInformacion.Advertencia);
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
				Response.Redirect("MsjeEntrada.aspx", false);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the OnSelectedIndexChanged event of the ddlCurso control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void ddlCurso_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ddlDestino.Items.Clear();
				rdlDestinatarios.SelectedIndex = -1;
				rdlDestinatarios.Enabled = ddlCurso.SelectedIndex > 0;
				udpFiltros.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the OnSelectedIndexChanged event of the rdlDestinatarios control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void rdlDestinatarios_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				BLDocente objDocente = null;
				ddlDestino.Items.Clear();
				AlumnoCurso objCurso = null;
				List<Persona> lista = null;
				Persona persona = null;
				switch (rdlDestinatarios.SelectedValue)
				{
					case "0":
						ddlDestino.Items.Add(new ListItem(ddlCurso.SelectedItem.Text, ddlCurso.SelectedItem.Value));
						break;
					case "1":
						objCurso = new AlumnoCurso(Convert.ToInt32(ddlCurso.SelectedValue));
						BLAlumno objBLAlumno = new BLAlumno();
						List<Alumno> listaAlumnos = objBLAlumno.GetAlumnos(objCurso);
						ddlDestino.Items.Clear();
						lista = new List<Persona>();
						foreach (Alumno item in listaAlumnos)
						{
							persona = new Persona();
							persona.idPersona = item.idPersona;
							persona.nombre = item.nombre;
							persona.apellido = item.apellido;
							lista.Add(persona);
						}
						ddlDestino.Items.Add(new ListItem("Alumnos de " + ddlCurso.SelectedItem.Text, ddlCurso.SelectedItem.Value));
						CargarDestinos(lista);
						break;
					case "2":
						objCurso = new AlumnoCurso();
						objCurso.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(ddlCurso.SelectedValue);
						BLTutor objBLTutor = new BLTutor();
						List<Tutor> listaTutores = objBLTutor.GetTutoresPorCurso(objCurso);
						ddlDestino.Items.Clear();
						lista = new List<Persona>();
						foreach (Tutor item in listaTutores)
						{
							persona = new Persona();
							persona.idPersona = item.idPersona;
							persona.nombre = item.nombre;
							persona.apellido = item.apellido;
							lista.Add(persona);
						}
						ddlDestino.Items.Add(new ListItem("Tutores " + ddlCurso.SelectedItem.Text, ddlCurso.SelectedItem.Value));
						CargarDestinos(lista);
						break;
					case "3":
						objDocente = new BLDocente();
						List<Docente> listaDocentes = objDocente.GetDocentes();
						lista = new List<Persona>();
						foreach (Docente item in listaDocentes)
						{
							persona = new Persona();
							persona.idPersona = item.idPersona;
							persona.nombre = item.nombre;
							persona.apellido = item.apellido;
							lista.Add(persona);
						}
						ddlDestino.Items.Add(new ListItem("Docentes " + ddlCurso.SelectedItem.Text, ddlCurso.SelectedItem.Value));
						CargarDestinos(lista);
						break;
					default:
						break;
				}
				udpFiltros.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		// no se elimina hasta decidir definitivamente a quienes va a poder enviar mensajes un alumno, por el momento, solo a sus docentes
		/// <summary>
		/// Handles the OnSelectedIndexChanged event of the rdlDestAlumno control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void rdlDestAlumno_OnSelectedIndexChanged(object sender, EventArgs e)
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
		/// Handles the CheckedChanged1 event of the chkFiltrado control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void chkFiltrado_CheckedChanged1(object sender, EventArgs e)
		{
			try
			{
				//if (!chkFiltrado.Checked)
				//    CargarLista();
				divDocente.Visible = chkFiltrado.Checked;
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the lista.
		/// </summary>
		private void CargarLista()
		{
			BLPersona objpersona = new BLPersona();
			List<Persona> lista = null;

			////Docente: a personas o cursos
			if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()))
			{
				divDocente.Visible = true;
				CargarComboCursos();
				rdlDestinatarios.Enabled = false;
				ddlDestino.Disabled = true;
			}


			//Alumno: a SUS docentes o su curso
			if (HttpContext.Current.User.IsInRole(enumRoles.Alumno.ToString()))
			{
				Alumno objAlumno = new Alumno { username = ObjSessionDataUI.ObjDTUsuario.Nombre };
				BLAlumno objBLAlumno = new BLAlumno(objAlumno);
				lista = objBLAlumno.GetDocentesAlumno(cicloLectivoActual);
			}

			//Tutor: docentes de sus alumnos
			if (HttpContext.Current.User.IsInRole(enumRoles.Tutor.ToString()))
			{
				Tutor objTutor = new Tutor();
				objTutor.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
				BLTutor objBLTutor = new BLTutor(objTutor);
				lista = objBLTutor.GetDocentesAlumnos(cicloLectivoActual);
			}

			//Administrativo: a tutores
			if (HttpContext.Current.User.IsInRole(enumRoles.Administrativo.ToString()))
			{
				lista = objpersona.GetPersonas(new Persona() { activo = true, idTipoPersona = (int)enumTipoPersona.Tutor });
			}

			//Preceptor: a cualquier persona
			//Director: a cualquier persona
			//Psicopedagogo: a cualquier persona
			if (HttpContext.Current.User.IsInRole(enumRoles.Director.ToString())
				||
				HttpContext.Current.User.IsInRole(enumRoles.Psicopedagogo.ToString())
				||
				HttpContext.Current.User.IsInRole(enumRoles.Administrador.ToString())
				||
				HttpContext.Current.User.IsInRole(enumRoles.Preceptor.ToString())
				)
			{
				chkFiltrado.Visible = true;
				lblFiltrado.Visible = true;
				CargarComboTodosCursos();
				rdlDestinatarios.Enabled = false;
				ddlDestino.Disabled = true;

				lista = objpersona.GetPersonas(new Persona() { activo = true });
			}

			if (lista != null)
			{
				CargarDestinos(lista);
			}
		}

		/// <summary>
		/// Cargars the destinos.
		/// </summary>
		/// <param name="lista">The lista.</param>
		private void CargarDestinos(List<Persona> lista)
		{
			lista.Sort((p, q) => string.Compare(p.apellido + " " + p.nombre, q.apellido + " " + q.nombre));

			//Quien recibe, siempre puede responder al remitente.
			foreach (Persona item in lista)
			{
				string[] nombre;
				nombre = item.nombre.Trim().Split(' ');
				string nuevoNombre = string.Empty;
				for (int i = 0; i < nombre.Length; i++)
				{
					nuevoNombre += " " + nombre[i].ToUpper().Substring(0, 1) + nombre[i].ToLower().Substring(1, nombre[i].Length - 1);
				}
				nuevoNombre = nuevoNombre.Trim();
				ddlDestino.Items.Add(new System.Web.UI.WebControls.ListItem(item.apellido.ToUpper() + ", " + nuevoNombre, item.idPersona.ToString()));
			}
		}

		private void CargarComboTodosCursos()
		{
			List<Curso> listaTodosCursos = new List<Curso>();
			BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
			Asignatura objFiltro = new Asignatura();
			objFiltro.curso.cicloLectivo = cicloLectivoActual;
			listaTodosCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);

			UIUtilidades.BindCombo<Curso>(ddlCurso, listaTodosCursos, "idCurso", "Nombre", true);
		}
		/// <summary>
		/// Cargars the combo cursos.
		/// </summary>
		private void CargarComboCursos()
		{
			UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "Nombre", true);
		}

		private void PrepararEnvioMensaje()
		{
			BLDocente objDocente = null;
			int idCursoCicloLectivo = 0;
			AlumnoCurso objCurso;
			//Docente: a personas o cursos
			if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()))
			{
				switch (rdlDestinatarios.SelectedValue)
				{
					case "1":
						if (ddlDestino.Items.FindByValue(ddlCurso.SelectedValue).Selected)
						{
							objCurso = new AlumnoCurso(Convert.ToInt32(ddlCurso.SelectedValue));
							idCursoCicloLectivo = Convert.ToInt32(ddlDestino.Value);
							BLAlumno objBLAlumno = new BLAlumno();
							List<Alumno> listaAlumnos = objBLAlumno.GetAlumnos(objCurso);
							ddlDestino.Items.Clear();
							foreach (Alumno item in listaAlumnos)
							{
								ddlDestino.Items.Add(new ListItem("", item.idPersona.ToString()));
								ddlDestino.Items.FindByValue(item.idPersona.ToString()).Selected = true;
							}
						}
						break;
					case "2":
						if (ddlDestino.Items.FindByValue(ddlCurso.SelectedValue).Selected)
						{
							objCurso = new AlumnoCurso();
							objCurso.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(ddlCurso.SelectedValue);
							BLTutor objBLTutor = new BLTutor();
							List<Tutor> listaTutores = objBLTutor.GetTutoresPorCurso(objCurso);
							ddlDestino.Items.Clear();
							foreach (Tutor item in listaTutores)
							{
								ddlDestino.Items.Add(new ListItem("", item.idPersona.ToString()));
								ddlDestino.Items.FindByValue(item.idPersona.ToString()).Selected = true;
							}
						}
						break;
					case "3":
						if (ddlDestino.Items.FindByValue(ddlCurso.SelectedValue).Selected)
						{
							objDocente = new BLDocente();
							List<Docente> listaDocentes = objDocente.GetDocentes();
							foreach (Docente item in listaDocentes)
							{
								ddlDestino.Items.Add(new ListItem("", item.idPersona.ToString()));
								ddlDestino.Items.FindByValue(item.idPersona.ToString()).Selected = true;
							}
							break;
						}
						break;
					default:
						break;
				}

			}
			if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()))
			{

			}
			EnviarMensaje(idCursoCicloLectivo);
		}

		/// <summary>
		/// Enviars the mensaje.
		/// </summary>
		private void EnviarMensaje(int idCursoCicloLectivo)
		{
			Mensaje objMensaje = new Mensaje();

			objMensaje.asuntoMensaje = txtAsunto.Value;
			objMensaje.textoMensaje = textoMensaje.contenido;
			objMensaje.remitente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
			objMensaje.fechaEnvio = DateTime.Now;
			objMensaje.horaEnvio = Convert.ToDateTime(DateTime.Now.Hour + ":" + DateTime.Now.Minute);
			objMensaje.cursoCicloLectivo.idCursoCicloLectivo = idCursoCicloLectivo;

			Persona destinatario;
			int cantidad = 0;
			foreach (ListItem item in ddlDestino.Items)
			{
				if (item.Selected)
				{
					destinatario = new Persona();
					destinatario.idPersona = Convert.ToInt32(item.Value);
					objMensaje.ListaDestinatarios.Add(destinatario);
					cantidad++;
				}
			}
			BLMensaje objBLMensaje = new BLMensaje(objMensaje);
			objBLMensaje.Save();
			AccionPagina = enumAcciones.Salir;

			if (cantidad == 1)
				Master.MostrarMensaje(this.Page.Title, UIConstantesGenerales.MensajeUnicoDestino, enumTipoVentanaInformacion.Satisfactorio);
			else
				Master.MostrarMensaje(this.Page.Title, UIConstantesGenerales.MensajeMultiDestino, enumTipoVentanaInformacion.Satisfactorio);
		}
		#endregion



	}
}