using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_Entities;
using EDUAR_UI.Utilidades;
using EDUAR_BusinessLogic.Common;

namespace EDUAR_UI
{
	public partial class MsjeEntrada : EDUARBasePage
	{
		#region --[Atributos]--
		private BLMensaje objBLMensaje;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the prop mensaje.
		/// </summary>
		/// <value>
		/// The prop mensaje.
		/// </value>
		public Mensaje propMensaje
		{
			get
			{
				if (ViewState["propMensaje"] == null)
					propMensaje = new Mensaje();
				return (Mensaje)ViewState["propMensaje"];
			}
			set
			{ ViewState["propMensaje"] = value; }

		}

		/// <summary>
		/// Gets or sets the lista mensajes.
		/// </summary>
		/// <value>
		/// The lista mensajes.
		/// </value>
		public List<Mensaje> listaMensajes
		{
			get
			{
				if (ViewState["listaMensajes"] == null)
					listaMensajes = new List<Mensaje>();
				return (List<Mensaje>)ViewState["listaMensajes"];
			}
			set
			{ ViewState["listaMensajes"] = value; }

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
				//Master.BotonAvisoAceptar += (VentanaAceptar);
				if (!Page.IsPostBack)
				{
					//CargarPresentacion();
					BuscarMensajes(new Mensaje() { destinatario = new Persona() { username = ObjDTSessionDataUI.ObjDTUsuario.Nombre } });
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
					case "Abrir":
						propMensaje.idMensaje = Convert.ToInt32(e.CommandArgument.ToString());
						//CargaCitacion();
						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwReporte control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		protected void gvwReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
				gvwReporte.PageIndex = e.NewPageIndex;
				//CargarGrilla();
			}
			catch (Exception ex) { Master.ManageExceptions(ex); }
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Buscars the entidads.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void BuscarMensajes(Mensaje entidad)
		{
			CargarLista(entidad);
			CargarGrilla();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void CargarLista(Mensaje entidad)
		{
			objBLMensaje = new BLMensaje();
			listaMensajes = objBLMensaje.GetMensajes(entidad);
		}

		/// Cargars the grilla.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		private void CargarGrilla()
		{
			gvwReporte.DataSource = UIUtilidades.BuildDataTable<Mensaje>(listaMensajes).DefaultView;
			gvwReporte.DataBind();
			//udpEdit.Visible = false;
			udpGrilla.Update();
		}
		#endregion
	}
}