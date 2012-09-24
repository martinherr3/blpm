using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI.UserControls
{
	public partial class Novedad : UserControl
	{
		#region --[Atributos]--
		//private int idCursoCicloLectivo = 0;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Gets the ciclo lectivo actual.
		/// </summary>
		/// <value>
		/// The ciclo lectivo actual.
		/// </value>
		public CicloLectivo cicloLectivoActual
		{
			get
			{
				return (CicloLectivo)ViewState["cicloLectivoActual"];
			}
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
					if (HttpContext.Current.User.IsInRole(enumRoles.Docente.ToString()))
						//nombre del usuario logueado
						objFiltro.docente.username = HttpContext.Current.User.Identity.Name;
					listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
				}
				return (List<Curso>)ViewState["listaCursos"];
			}
			set { ViewState["listaCursos"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista estados novedad.
		/// </summary>
		/// <value>
		/// The lista estados novedad.
		/// </value>
		public List<EstadoNovedad> listaEstadosNovedad
		{
			get
			{
				if (ViewState["listaEstadosNovedad"] == null)
				{
					BLEstadoNovedad objBLEstadoNovedad = new BLEstadoNovedad();

					listaEstadosNovedad = objBLEstadoNovedad.GetEstadosNovedad(new EstadoNovedad());
				}
				return (List<EstadoNovedad>)ViewState["listaEstadosNovedad"];
			}
			set { ViewState["listaEstadosNovedad"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista tipos novedad.
		/// </summary>
		/// <value>
		/// The lista tipos novedad.
		/// </value>
		public List<TipoNovedad> listaTiposNovedad
		{
			get
			{
				if (ViewState["listaTiposNovedad"] == null)
				{
					BLTipoNovedad objBLTipoNovedad = new BLTipoNovedad();

					listaTiposNovedad = objBLTipoNovedad.GetTiposNovedad(new TipoNovedad());
				}
				return (List<TipoNovedad>)ViewState["listaTiposNovedad"];
			}
			set { ViewState["listaTiposNovedad"] = value; }
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
				return (int)Session["idCursoCicloLectivo"];
			}
		}

		public bool visible
		{
			get { return btnNuevaNovedad.Visible; }
			set { btnNuevaNovedad.Visible = value; }
		}

		public string ToolTip
		{
			get { return btnNuevaNovedad.ToolTip; }
			set { btnNuevaNovedad.ToolTip = value; }
		}

		public EDUAR_Entities.Novedad novedadPadre
		{
			get
			{
				if (ViewState[this.UniqueID + "_idNovedadPadre"] == null)
					ViewState[this.UniqueID + "_idNovedadPadre"] = new EDUAR_Entities.Novedad();
				return (EDUAR_Entities.Novedad)ViewState[this.UniqueID + "_idNovedadPadre"];
			}
			set { ViewState[this.UniqueID + "_idNovedadPadre"] = value; }
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
				//btnOcultoNovedad.Click += (Guardar);
				btnGuardar.Click += (Guardar);
				btnOcultoNovedad.Click += (Guardar);
				if (!Page.IsPostBack)
				{
					UIUtilidades.BindCombo<EstadoNovedad>(ddlEstado, listaEstadosNovedad, "idEstadoNovedad", "nombre", true);
					UIUtilidades.BindCombo<TipoNovedad>(ddlNovedad, listaTiposNovedad, "idTipoNovedad", "nombre", true);
				}

				this.txtObservaciones.Attributes.Add("onkeyup", " ValidarCaracteres(this, 1000);");
			}
			catch (Exception)
			{
				//Master.ManageExceptions(ex);
			}
		}

		protected void btnNuevaNovedad_Click(object sender, EventArgs e)
		{
			try
			{
				LimpiarCampos();

				if (novedadPadre.idNovedad > 0)
				{
					ddlEstado.SelectedValue = novedadPadre.estado.idEstadoNovedad.ToString();
					ddlNovedad.SelectedValue = novedadPadre.tipo.idTipoNovedad.ToString();
					listaEstadosNovedad = null;
				}
				else
					listaEstadosNovedad = listaEstadosNovedad.FindAll(p => p.esFinal == false);

				UIUtilidades.BindCombo<EstadoNovedad>(ddlEstado, listaEstadosNovedad, "idEstadoNovedad", "nombre", true);

				ddlNovedad.Enabled = !(novedadPadre.idNovedad > 0);
				mpeNueva.Show();
				udpNueva.Update();
			}
			catch (Exception)
			{
				//Master.ManageExceptions(ex);
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
				LimpiarCampos();
				mpeNueva.Hide();
			}
			catch (Exception)
			{
				//Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Delegados]--
		public delegate void VentanaBotonClickHandler(object sender, EventArgs e);

		public event VentanaBotonClickHandler GuardarClick;

		public virtual void OnGuardarClick(VentanaBotonClickHandler sender, EventArgs e)
		{
			if (sender != null)
			{
				//Invoca el delegados
				sender(this, e);
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private void ValidarPagina()
		{
			Page.Validate("Novedad");
			if (!Page.IsValid)
				mpeNueva.Show();
		}

		private void GuardarNovedad(EDUAR_Entities.Novedad objEntidad)
		{
			BLNovedad objBLNovedad = new BLNovedad(objEntidad);
			objBLNovedad.Save();
		}

		private void LimpiarCampos()
		{
			listaEstadosNovedad = null;
			listaTiposNovedad = null;
			ddlEstado.SelectedIndex = 0;
			ddlNovedad.SelectedIndex = 0;
			txtObservaciones.Text = string.Empty;
		}

		void Guardar(object sender, EventArgs e)
		{
			OnGuardarClick(GuardarClick, e);
		}
		#endregion

		#region --[Métodos Públicos]--
		public void GuardarNovedad()
		{
			EDUAR_Entities.Novedad objEntidad = new EDUAR_Entities.Novedad();
			objEntidad.fecha = DateTime.Now;
			objEntidad.usuario.username = HttpContext.Current.User.Identity.Name;
			objEntidad.tipo.idTipoNovedad = Convert.ToInt32(ddlNovedad.SelectedValue);
			objEntidad.estado.idEstadoNovedad = Convert.ToInt32(ddlEstado.SelectedValue);
			objEntidad.curso.idCurso = idCursoCicloLectivo;
			objEntidad.observaciones = txtObservaciones.Text.Trim();

			if (novedadPadre.idNovedad > 0)
			{
				objEntidad.novedadPadre = new EDUAR_Entities.Novedad();
				objEntidad.novedadPadre.idNovedad = novedadPadre.idNovedad;
			}

			GuardarNovedad(objEntidad);
			LimpiarCampos();

			mpeNueva.Hide();
		}
		#endregion

	}
}