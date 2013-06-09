using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_Entities.Security;
using EDUAR_Entities.Shared;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_UI
{
	public partial class EDUARMaster : MasterPage, ICallbackEventHandler
	{
		#region --[Propiedades]--
		public Boolean EsExepcion
		{
			get { return (Boolean)ViewState["esExepcion"]; }
			set { ViewState["esExepcion"] = value; }
		}

		private string _callbackResult;

		/// <summary>
		/// Mantiene los datos del usuario logueado.
		/// </summary>
		public DTSessionDataUI ObjSessionDataUI
		{
			get
			{
				if (Session["ObjSessionDataUI"] == null)
					Session["ObjSessionDataUI"] = new DTSessionDataUI();

				return (DTSessionDataUI)Session["ObjSessionDataUI"];
			}
			set { Session["ObjSessionDataUI"] = value; }
		}

		/// <summary>
		/// Gets or sets the obj session persona.
		/// </summary>
		/// <value>
		/// The obj session persona.
		/// </value>
		public Persona objSessionPersona
		{
			get
			{
				if (Session["objSessionPersona"] == null)
				{
					BLPersona objBLPersona = new BLPersona(new Persona() { username = ObjSessionDataUI.ObjDTUsuario.Nombre });
					objBLPersona.GetPersonaByEntidad();
					//objSessionPersona = new Persona();
					objSessionPersona = objBLPersona.Data;
				}
				return (Persona)Session["objSessionPersona"];
			}
			set { Session["objSessionPersona"] = value; }
		}

		/// <summary>
		/// Gets or sets the ciclo lectivo actual.
		/// </summary>
		/// <value>
		/// The ciclo lectivo actual.
		/// </value>
		public CicloLectivo cicloLectivoActual
		{
			get
			{
				if (ViewState["cicloLectivoActual"] == null)
				{
					BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
					cicloLectivoActual = objBLCicloLectivo.GetCicloLectivoActual();
				}
				return (CicloLectivo)ViewState["cicloLectivoActual"];
			}
			set { ViewState["cicloLectivoActual"] = value; }
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
				//Llama a la funcionalidad que redirecciona a la pagina de Login cuando finaliza el tiempo de session
				((EDUARBasePage)Page).DireccionamientoOnSessionEndScript();

				if (HttpContext.Current.User == null || (ObjSessionDataUI.ObjDTUsuario.Nombre == null && HttpContext.Current.User.Identity.Name != string.Empty))
				{
					HttpContext.Current.User = null;
					ObjSessionDataUI = null;
				}
				if (HttpContext.Current.User == null)
					NavigationMenu.DataSource = SiteMapAnonymusEDUAR;
				else
					if (HttpContext.Current.User.Identity.IsAuthenticated)
					{
						divInfo.Visible = true;

						CargaInforUsuario();

						// ~/Private/Manuales/{0}/index.htm
						string rol = string.Empty;
						if (HttpContext.Current.User.IsInRole(enumRoles.Administrador.ToString()))
							rol = enumRoles.Administrador.ToString();
						if (HttpContext.Current.User.IsInRole(enumRoles.Administrativo.ToString()))
							rol = enumRoles.Administrativo.ToString();
						if (HttpContext.Current.User.IsInRole(enumRoles.Alumno.ToString()))
							rol = enumRoles.Alumno.ToString();
						if (HttpContext.Current.User.IsInRole(enumRoles.Director.ToString()))
							rol = enumRoles.Director.ToString();
						if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()))
							rol = enumRoles.Docente.ToString();
						if (HttpContext.Current.User.IsInRole(enumRoles.Preceptor.ToString()))
							rol = enumRoles.Preceptor.ToString();
						if (HttpContext.Current.User.IsInRole(enumRoles.Psicopedagogo.ToString()))
							rol = enumRoles.Psicopedagogo.ToString();
						if (HttpContext.Current.User.IsInRole(enumRoles.Tutor.ToString()))
							rol = enumRoles.Tutor.ToString();

						if (!string.IsNullOrEmpty(rol) && ((HyperLink)Page.Master.FindControl("HeadLoginView").FindControl("linkAyuda")) != null)
							((HyperLink)Page.Master.FindControl("HeadLoginView").FindControl("linkAyuda")).NavigateUrl = string.Format("~/Private/Manuales/{0}/index.htm", rol);

						#region --[Mensajes en header]--
						//StringBuilder s = new StringBuilder();
						//string er;
						//  configura los llamados a RaiseCallbackEvent y GetCallbackResult
						//er = Page.ClientScript.GetCallbackEventReference(this, "clientTime('')", "putCallbackResult", "null", "clientErrorCallback", true);

						//  funcion que llama a RaiseCallbackEvent
						//s.Append(" function callServerTask() { ");
						//s.Append((er + ";"));
						//s.Append(" } ");
						////  inserta el script en la pgina
						//Page.ClientScript.RegisterClientScriptBlock(
						//     this.GetType(), "callServerTask", s.ToString(), true);
						//  NOTA:
						//  La función callServerTask() es llamada desde la function timerEvent()
						#endregion
					}
					else
					{
						NavigationMenu.DataSource = SiteMapAnonymusEDUAR;
					}

				NavigationMenu.MenuItemDataBound += (NavigationMenu_OnItemBound);
				NavigationMenu.DataBind();

				// Ocultar la ventana de información
				ventanaInfoMaster.Visible = false;

				//Suscribe los eventos de la ventana emergente. 
				ventanaInfoMaster.VentanaAceptarClick += (Aceptar);
				ventanaInfoMaster.VentanaCancelarClick += (Cancelar);

				if (!Page.IsPostBack)
					CargarMenu();
			}
			catch (Exception ex)
			{
				ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Cargas the infor usuario.
		/// </summary>
		private void CargaInforUsuario()
		{
			lblUsuario.Text = objSessionPersona.nombre + " " + objSessionPersona.apellido;
			lblRol.Text = ObjSessionDataUI.ObjDTUsuario.ListaRoles[0].Nombre + ": ";

			if (HttpContext.Current.User.IsInRole(enumRoles.Alumno.ToString()))
			{
				BLAlumno objBLAlumno = new BLAlumno(new Alumno() { username = ObjSessionDataUI.ObjDTUsuario.Nombre });
				AlumnoCursoCicloLectivo objCurso = objBLAlumno.GetCursoActualAlumno(cicloLectivoActual);
				lblCursosAsignados.Text = "Curso Actual: " + objCurso.cursoCicloLectivo.curso.nivel.nombre + "  " + objCurso.cursoCicloLectivo.curso.nombre;
			}
			if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()))
			{
				BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
				Asignatura objFiltro = new Asignatura();
				objFiltro.curso.cicloLectivo = cicloLectivoActual;
				//nombre del usuario logueado
				objFiltro.docente.username = HttpContext.Current.User.Identity.Name;
				List<Curso> listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
				string cursos = string.Empty;
				if (listaCursos.Count > 0) cursos = "Cursos: <br />";
				int i = 1;
				listaCursos.Sort((p, q) => string.Compare(p.nombre, q.nombre));
				foreach (Curso item in listaCursos)
				{
					if (!cursos.Contains(item.nombre))
					{
						if (i % 2 == 0)
							cursos += item.nombre + " <br />";
						else
							cursos += item.nombre + " - ";
						i++;
					}
				}
				lblCursosAsignados.Text = cursos;
			}
			if (HttpContext.Current.User.IsInRole(enumRoles.Tutor.ToString()))
			{
				List<Tutor> lista = new List<Tutor>();
				lista.Add(new Tutor() { username = HttpContext.Current.User.Identity.Name });
				BLAlumno objBLAlumno = new BLAlumno(new Alumno() { listaTutores = lista });
				List<AlumnoCursoCicloLectivo> listaAlumnos = objBLAlumno.GetAlumnosTutor(cicloLectivoActual);
				string cursos = string.Empty;
				if (listaAlumnos.Count > 0) cursos = "Cursos: \n";
				foreach (AlumnoCursoCicloLectivo item in listaAlumnos)
				{
					if (!cursos.Contains(item.cursoCicloLectivo.curso.nivel.nombre + "  " + item.cursoCicloLectivo.curso.nombre))
					{
						cursos += item.cursoCicloLectivo.curso.nivel.nombre + "  " + item.cursoCicloLectivo.curso.nombre + " \n";
					}
				}
				lblCursosAsignados.Text = cursos;
			}
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
				StringWriter sr = new StringWriter();
				HtmlTextWriter htm = new HtmlTextWriter(sr);

				((ImageButton)Page.Master.FindControl("HeadLoginView").FindControl("btnMensaje")).Visible = false;
				((ImageButton)Page.Master.FindControl("HeadLoginView").FindControl("btnMensaje")).RenderControl(htm);
				htm.Flush();
				Response.Redirect("~/Private/Mensajes/MsjeEntrada.aspx", false);
			}
			catch (Exception ex)
			{
				ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnLogout control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnLogout_Click(object sender, EventArgs e)
		{
			try
			{
				UIUtilidades.EliminarArchivosSession(Session.SessionID);
				Response.Cookies.Clear();
				Session.Abandon();
				//HttpContext.Current = null;
				ObjSessionDataUI = null;
				objSessionPersona = null;
				FormsAuthentication.SignOut();
				Response.Redirect("~/Login.aspx", false);
			}
			catch (Exception ex)
			{
				ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Método que cierra la sesión del usuario logueado.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HeadLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
		{
			try
			{
				UIUtilidades.EliminarArchivosSession(Session.SessionID);
				LoginStatus control = ((LoginStatus)Page.Master.FindControl("HeadLoginView").FindControl("HeadLoginStatus"));
				control.LogoutPageUrl = "~/Login.aspx";
				control.LogoutAction = LogoutAction.RedirectToLoginPage;
				Response.Cookies.Clear();
				//HttpContext.Current.User = null;
				Session.Abandon();
				FormsAuthentication.SignOut();
			}
			catch (Exception ex)
			{
				ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the PreRender event of the siteMapPathEDUAR control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void NavigationMenu_PreRender(object sender, EventArgs e)
		{
			//SiteMapNodeItem sepItem = new SiteMapNodeItem(-1, SiteMapNodeItemType.PathSeparator);
			//ITemplate sepTemplate = NavigationMenu.TemplateControl;
			//if (sepTemplate == null)
			//{
			//    Literal separator = new Literal { Text = siteMapPathEDUAR.PathSeparator };
			//    sepItem.Controls.Add(separator);
			//}
			//else
			//    sepTemplate.InstantiateIn(sepItem);

			//sepItem.ApplyStyle(siteMapPathEDUAR.PathSeparatorStyle);

			if (SiteMapEDUAR.Provider.RootNode != null)
			{
				foreach (SiteMapNode node in SiteMapEDUAR.Provider.RootNode.ChildNodes)
				{
					if (!ValidarNodo(node))
						continue;
					trvMenu.Visible = true;
					MenuItem objMenuItem = new MenuItem(node.Title);
					if (node.Url != string.Empty)
						objMenuItem.NavigateUrl = node.Url;

					//Recorre los nodos hijos
					foreach (SiteMapNode nodeChild in node.ChildNodes)
					{
						if (!ValidarNodo(nodeChild))
							continue;

						MenuItem objMenuItemChild = new MenuItem(nodeChild.Title) { NavigateUrl = nodeChild.Url };
						objMenuItem.ChildItems.Add(objMenuItemChild);
					}
					if (objMenuItem.ChildItems.Count > 0 || objMenuItem.Text.Contains("Inicio"))
						NavigationMenu.Items.Add(objMenuItem);
				}
			}
		}

		/// <summary>
		/// Valida si el nodo se debe mostrar. 
		/// Puede tener el atributo visible=false o puede que el perfil del usuario lo permita.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		protected Boolean ValidarNodo(SiteMapNode node)
		{
			//Si el nodo está marcado como visible False es porque solo se utiliza para que sea visible 
			//en el menu superior y no se debe mostrar en el menu lateral
			Boolean isVisible;
			if (bool.TryParse(node["visible"], out isVisible) && !isVisible)
				return false;

			foreach (DTRol rolUsuario in ObjSessionDataUI.ObjDTUsuario.ListaRoles)
			{
				if (node.Roles.Contains(rolUsuario.Nombre))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Click en botón aceptar de ventana de información / confirmación / error
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Aceptar(object sender, EventArgs e)
		{
			if (!EsExepcion)
				OnBotonClickAviso(BotonAvisoAceptar, e);
		}

		/// <summary>
		/// Click en botón Cancelar de ventana de información / confirmación / error
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Cancelar(object sender, EventArgs e)
		{
			if (!EsExepcion)
				OnBotonClickAviso(BotonAvisoCancelar, e);
		}

		/// <summary>
		/// Handles the OnItemBound event of the NavigationMenu control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="System.Web.UI.WebControls.MenuEventArgs"/> instance containing the event data.</param>
		protected void NavigationMenu_OnItemBound(object sender, MenuEventArgs args)
		{
			args.Item.ImageUrl = ((SiteMapNode)args.Item.DataItem)["ImageUrl"];
		}

		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Método que permite tratar las excepciones de forma standard. 
		/// </summary>
		/// <param name="ex">Excepción a tratar</param>
		public void ManageExceptions(Exception ex)
		{
			try
			{
				string exceptionName = ex.GetType().FullName;
				string Titulo = string.Empty;
				string Detalle = string.Empty;
				enumTipoVentanaInformacion tipoVentana = enumTipoVentanaInformacion.Error;
				Detalle = ex.Message;

				if (exceptionName.Contains("CustomizedException"))
				{
					switch (((CustomizedException)ex).ExceptionType)
					{
						case enuExceptionType.BusinessLogicException:
							Titulo = "Error en Negocio";
							Detalle = "Se ha producido un error al realizar una acción en el negocio.";
							break;
						case enuExceptionType.SqlException:
						case enuExceptionType.MySQLException:
						case enuExceptionType.DataAccesException:
							Titulo = "Error en Base de Datos";
							Detalle = "Se ha producido un error al realizar una acción en la Base de Datos.";
							break;
						case enuExceptionType.ServicesException:
							Titulo = "Error en Servicio";
							Detalle = "Se ha producido un error al realizar la consulta al Servicio.";
							break;
						case enuExceptionType.IntegrityDataException:
							Titulo = "Error de Integridad de Datos";
							break;
						case enuExceptionType.ConcurrencyException:
							Titulo = "Error de Concurrencia";
							break;
						case enuExceptionType.ValidationException:
							//Esta es una excepcion de tipo validacion que viene de UI.
							Titulo = "Error de Validación";
							tipoVentana = enumTipoVentanaInformacion.Advertencia;
							//MostrarMensaje("Error de Validación", ex.Message, enumTipoVentanaInformacion.Advertencia);
							break;
						case enuExceptionType.SecurityException:
							Titulo = "Error de seguridad";
							tipoVentana = enumTipoVentanaInformacion.Advertencia;
							break;
						case enuExceptionType.WorkFlowException:
							break;
						case enuExceptionType.Exception:
							Titulo = "Error en la Aplicación";
							Detalle = "Se ha producido un error interno en la aplicación.";
							break;
						default:
							break;
					}
					if (Detalle != ex.Message) Detalle += " " + ex.Message;
					//Detalle += " " + ex.Message;
					MostrarMensaje(Titulo, Detalle, tipoVentana);
					if (tipoVentana != enumTipoVentanaInformacion.Advertencia)
						ManageExceptionsLog(ex);
				}
				//Esta es una excepcion de tipo validacion que viene de BL.
				else if ((exceptionName.Contains("GenericException")))
				{
					///GenericException genericEx = ((GenericException)ex).Detail;
					if (((CustomizedException)ex).ExceptionType == enuExceptionType.ValidationException)
						MostrarMensaje("Error de Validación", ex.Message, enumTipoVentanaInformacion.Advertencia);
					else
						ventanaInfoMaster.GestionExcepciones(ex);
				}
				else
					ventanaInfoMaster.GestionExcepciones(ex);

				// Refrescar updatepanel
				updVentaneMensajes.Update();
				EsExepcion = true;
			}
			catch (Exception exNew)
			{
				ventanaInfoMaster.GestionExcepciones(exNew);
			}
		}

		/// <summary>
		/// Método que guardar un log
		/// </summary>
		/// <param name="exepcionControlada">Excepcion que se va a guardar</param>
		public void ManageExceptionsLog(Exception exepcionControlada)
		{
			try
			{
				ventanaInfoMaster.GestionExcepcionesLog(exepcionControlada);
			}
			catch (Exception ex)
			{
				ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Metodo que se encarga de mostrar mensajes en la aplicacion.
		/// </summary>
		/// <param name="titulo"></param>
		/// <param name="detalle"></param>
		/// <param name="tipoventana"></param>
		public void MostrarMensaje(string titulo, string detalle, enumTipoVentanaInformacion tipoventana)
		{
			try
			{
				EsExepcion = false;
				ventanaInfoMaster.TipoVentana = tipoventana;
				ventanaInfoMaster.Titulo = titulo;
				ventanaInfoMaster.Detalle = detalle;
				ventanaInfoMaster.MostrarMensaje();

				// Refrescar updatepanel
				updVentaneMensajes.Update();

			}
			catch (Exception ex)
			{
				ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Metodo que se encarga de ocultar los mensajes en la aplicacion.
		/// </summary>
		public void OcultarMensaje()
		{
			try
			{
				ventanaInfoMaster.OcultarMensaje();
				// Refrescar updatepanel
				updVentaneMensajes.Update();

			}
			catch (Exception ex)
			{
				ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Procesa un evento de devolución de llamada que tiene como destino un control.
		/// </summary>
		/// <param name="eventArgument">Cadena que representa un argumento del evento que se pasará al controlador de eventos.</param>
		public void RaiseCallbackEvent(string eventArgument)
		{
			if (!Page.Request.Url.ToString().Contains("MsjeEntrada"))
			{
				StringWriter sr = new StringWriter();
				HtmlTextWriter htm = new HtmlTextWriter(sr);
				//HyperLink link = ((HyperLink)Page.Master.FindControl("HeadLoginView").FindControl("lnkMensajes"));
				ImageButton boton = ((ImageButton)Page.Master.FindControl("HeadLoginView").FindControl("btnMensaje"));
				if (HttpContext.Current.User != null)
				{
					BLMensaje objBLMensaje = new BLMensaje();
					List<Mensaje> objMensajes = new List<Mensaje>();
					objMensajes = objBLMensaje.GetMensajes(new Mensaje() { destinatario = new Persona() { username = ObjSessionDataUI.ObjDTUsuario.Nombre }, activo = true });
					objMensajes = objMensajes.FindAll(p => p.leido == false);
					if (boton != null)
					{
						boton.Visible = true;
						if (objMensajes.Count > 0)
						{
							boton.ImageUrl = "/EDUAR_UI/Images/mail-new-message.gif";
							//link.AlternateText = "Nuevo Mensaje!";
							boton.ToolTip = "Nuevo Mensaje!";
						}
						else
						{
							boton.ImageUrl = "/EDUAR_UI/Images/mail-inbox.png";
							//btnMail.AlternateText = "Mensajes";
							boton.ToolTip = "Mensajes";
						}
						//boton.NavigateUrl = "Private/Mensajes/MsjeEntrada.aspx";
						boton.RenderControl(htm);
						htm.Flush();
						//htm = new HtmlTextWriter(sr);
						//boton.RenderControl(htm);
						//htm.Flush();
					}
				}
				else
				{
					boton.ImageUrl = "";
					boton.Visible = false;
					boton.RenderControl(htm);
					htm.Flush();
				}
				_callbackResult = sr.ToString();
			}
		}

		/// <summary>
		/// Devuelve los resultados de un evento de devolución de llamada que tiene como destino un control.
		/// </summary>
		/// <returns>
		/// Resultado de la devolución de llamada.
		/// </returns>
		public string GetCallbackResult()
		{
			//  ésta variable es pasada al argumento de putCallbackResult
			return _callbackResult;
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the menu.
		/// </summary>
		private void CargarMenu()
		{
			if (SiteMapEDUAR.Provider.RootNode != null)
			{
				foreach (SiteMapNode node in SiteMapEDUAR.Provider.RootNode.ChildNodes)
				{
					if (!ValidarNodo(node))
						continue;
					trvMenu.Visible = true;
					TreeNode objTreeNode = new TreeNode(node.Title);
					if (node.Url != string.Empty)
						objTreeNode.NavigateUrl = node.Url;

					objTreeNode.SelectAction = TreeNodeSelectAction.Expand;
					//Recorre los nodos hijos
					foreach (SiteMapNode nodeChild in node.ChildNodes)
					{
						if (!ValidarNodo(nodeChild))
							continue;

						TreeNode objTreeNodeChild = new TreeNode(nodeChild.Title) { NavigateUrl = nodeChild.Url };
						objTreeNode.ChildNodes.Add(objTreeNodeChild);
					}
					if (objTreeNode.ChildNodes.Count > 0 || objTreeNode.Text.Contains("Inicio"))
						trvMenu.Nodes.Add(objTreeNode);
				}
				trvMenu.ExpandAll();
			}
		}

		#endregion

		#region --[Delegados]--
		/// <summary>
		/// Delegado para capturar el evento de click sobre aceptar / cancelar en ventana de aviso
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void MasterPageAvisoClickHandler(object sender, EventArgs e);

		/// <summary>
		/// Evento click sobre botón aceptar de ventana de aviso
		/// </summary>
		public event MasterPageAvisoClickHandler BotonAvisoAceptar;

		/// <summary>
		/// Evento click sobre botón cancelar de ventana de aviso
		/// </summary>
		public event MasterPageAvisoClickHandler BotonAvisoCancelar;

		/// <summary>
		/// Invoca los delegados al evento de click al botón, para que los eventos de
		/// aceptar / cancelar puedan ser controlados desde las páginas "hijas" al masterpage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Argumentos del evento</param>
		protected virtual void OnBotonClickAviso(MasterPageAvisoClickHandler sender, EventArgs e)
		{
			if (sender != null)
			{
				//Invoca los delegados
				sender(this, e);
			}
		}

		#endregion
	}
}
