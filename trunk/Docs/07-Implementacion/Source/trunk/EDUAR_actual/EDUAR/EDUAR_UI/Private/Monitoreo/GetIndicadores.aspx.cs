using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.UserControls;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
	public partial class GetIndicadores : EDUARBasePage
	{
		#region --[Propiedades]--
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

		/// <summary>
		/// Gets or sets the id curso ciclo lectivo.
		/// </summary>
		/// <value>
		/// The id curso ciclo lectivo.
		/// </value>
		public int idCursoCicloLectivo
		{
			get
			{
				if (Session["idCursoCicloLectivo"] == null)
					Session["idCursoCicloLectivo"] = 0;
				return (int)Session["idCursoCicloLectivo"];
			}
			set
			{
				Session["idCursoCicloLectivo"] = value;
			}
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
				Indicador1.SetEventoClick(this.btnInformeIndicador_Click);
				Indicador2.SetEventoClick(this.btnInformeIndicador_Click);
				Indicador3.SetEventoClick(this.btnInformeIndicador_Click);
				Indicador4.SetEventoClick(this.btnInformeIndicador_Click);
				Indicador5.SetEventoClick(this.btnInformeIndicador_Click);
				Indicador6.SetEventoClick(this.btnInformeIndicador_Click);

				InformeIndicador1.SalirClick += (this.btnInformeIndicadorSalir_Click);

				Master.BotonAvisoAceptar += (VentanaAceptar);
				if (!IsPostBack)
				{
					UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "Nombre", true);
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
				int idCursoCicloLectivo = 0;
				int.TryParse(ddlCurso.SelectedValue, out idCursoCicloLectivo);
				if (idCursoCicloLectivo > 0)
				{
					this.idCursoCicloLectivo = idCursoCicloLectivo;
					CargarIndicadores();
					CargarNovedades();
				}
				divIndicadores.Visible = idCursoCicloLectivo > 0;
				divNovedades.Visible = idCursoCicloLectivo > 0;
				udpIndicadores.Update();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnInformeIndicador control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnInformeIndicador_Click(object sender, EventArgs e)
		{
			try
			{
				mpeContenido.Show();
				EDUAR_UI.UserControls.Indicador indi = null;
				Button btn = (Button)sender;
				switch (btn.CommandArgument)
				{
					case "Indicador1":
						indi = Indicador1;
						break;
					case "Indicador2":
						indi = Indicador2;
						break;
					case "Indicador3":
						indi = Indicador3;
						break;
					case "Indicador4":
						indi = Indicador4;
						break;
					case "Indicador5":
						indi = Indicador5;
						break;
					case "Indicador6":
						indi = Indicador6;
						break;
					default:
						break;
				}

				if (indi != null)
				{
					InformeIndicador1.Titulo = indi.Título;
					InformeIndicador1.SP = "DatosIndicador_" + indi.nombreSP;
					InformeIndicador1.Hasta = DateTime.Today;

					if (btn.CommandName == "Principal")
						InformeIndicador1.Desde = DateTime.Today.AddDays(indi.HastaPrincipal * -1);
					else if (btn.CommandName == "Intermedio")
						InformeIndicador1.Desde = DateTime.Today.AddDays(indi.HastaIntermedio * -1);
					else if (btn.CommandName == "Secundario")
						InformeIndicador1.Desde = DateTime.Today.AddDays(indi.HastaSecundario * -1);

					InformeIndicador1.Show();
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnInformeIndicadorSalir control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnInformeIndicadorSalir_Click(object sender, EventArgs e)
		{
			try
			{
				InformeIndicador1.Hide();
				mpeContenido.Hide();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the ItemCommand event of the rptConversacion control.
		/// </summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
		protected void rptConversacion_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			try
			{
				base.idNovedadConsulta = Convert.ToInt32(e.CommandArgument);
				Response.Redirect("~/Private/Novedades/ConsultaNovedadAulica.aspx", false);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the indicadores.
		/// </summary>
		private void CargarIndicadores()
		{
			Indicador indi = null;
			BLIndicador objBLIndicador = new BLIndicador();
			List<EDUAR_Entities.Reports.Indicador> listaIndicadores = objBLIndicador.GetIndicadores(null);

			for (int i = 1; i <= listaIndicadores.Count; i++)
			{
				switch (i)
				{
					case 1:
						indi = Indicador1;
						break;
					case 2:
						indi = Indicador2;
						break;
					case 3:
						indi = Indicador3;
						break;
					case 4:
						indi = Indicador4;
						break;
					case 5:
						indi = Indicador5;
						break;
					case 6:
						indi = Indicador6;
						break;
					default:
						break;
				}
				if (indi != null)
				{
					indi.Visible = true;
					indi.InvertirEscala = Convert.ToBoolean(listaIndicadores[i - 1].invertirEscala.ToString());
					indi.HastaPrincipal = listaIndicadores[i - 1].diasHastaPrincipal;
					indi.HastaIntermedio = listaIndicadores[i - 1].diasHastaIntermedio;
					indi.HastaSecundario = listaIndicadores[i - 1].diasHastaSecundario;
					indi.VerdePrincipal = listaIndicadores[i - 1].verdeNivelPrincipal;
					indi.RojoPrincipal = listaIndicadores[i - 1].rojoNivelPrincipal;
					indi.VerdeIntermedio = listaIndicadores[i - 1].verdeNivelIntermedio;
					indi.RojoSecundario = listaIndicadores[i - 1].rojoNivelIntermedio;
					indi.VerdeSecundario = listaIndicadores[i - 1].verdeNivelSecundario;
					indi.RojoSecundario = listaIndicadores[i - 1].rojoNivelSecundario;
					indi.nombreSP = listaIndicadores[i - 1].nombreSP;
					indi.Título = listaIndicadores[i - 1].nombre;
					//indi.Filtros = dr["filtros"].ToString().Split(',');
					indi.CargarIndicador();
				}
			}
		}

		/// <summary>
		/// Cargars the novedades.
		/// </summary>
		private void CargarNovedades()
		{
			BLNovedad objBLNovedad = new BLNovedad();
			EDUAR_Entities.Novedad entidad = new EDUAR_Entities.Novedad();
			entidad.curso.idCurso = idCursoCicloLectivo;
			
			List<EDUAR_Entities.Novedad> listaNovedades = objBLNovedad.GetNovedadIndicadores(entidad);

			lblNoHay.Visible = !(listaNovedades.Count > 0);
			rptConversacion.DataSource = listaNovedades;
			rptConversacion.DataBind();
			udpConversacion.Update();
		}
		#endregion
	}
}