using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;

//TODO: botón Eliminar mensaje, Responder.
//TODO: filtrar destinatarios según el usuario logueado.

namespace EDUAR_UI
{
	public partial class MsjeEntrada : EDUARBasePage
	{
		#region --[Atributos]--
		private BLMensaje objBLMensaje;

		PagedDataSource pds = new PagedDataSource();

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

		public int CurrentPage
		{
			get
			{
				if (this.ViewState["CurrentPage"] == null)
				{
					return 0;
				}
				else
				{
					return Convert.ToInt16(this.ViewState["CurrentPage"].ToString());
				}
			}
			set
			{
				this.ViewState["CurrentPage"] = value;
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
				//Master.BotonAvisoAceptar += (VentanaAceptar);
				MyAccordion.ItemCommand += (MyAccordion_ItemCommand);
				if (!Page.IsPostBack)
				{
					//CargarPresentacion();
					BuscarMensajes();
				}
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
		/// Handles the ItemCommand event of the MyAccordion control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
		protected void MyAccordion_ItemCommand(object sender, CommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Leer":
					int idMensaje = Convert.ToInt32(e.CommandArgument);

					Mensaje objMensaje = listaMensajes.Find(p => p.idMensaje == idMensaje);
					if (!objMensaje.leido)
					{
						//objMensaje.idMensaje = idMensaje;
						objMensaje.leido = true;
						BLMensaje objBLMensaje = new BLMensaje(objMensaje);
						objBLMensaje.LeerMensaje();
						listaMensajes.Find(p => p.idMensaje == idMensaje).leido = true;
						Master.RaiseCallbackEvent(e.ToString());
						CargarGrilla();
					}
					break;
			}
			udpGrilla.Update();
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
				//gvwReporte.PageIndex = e.NewPageIndex;
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
		private void BuscarMensajes()
		{
			Mensaje entidad = new Mensaje();
			entidad.destinatario.username = ObjDTSessionDataUI.ObjDTUsuario.Nombre;
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
			DataTable dt = UIUtilidades.BuildDataTable<Mensaje>(listaMensajes);
			pds.DataSource = dt.DefaultView;
			pds.AllowPaging = true;
			pds.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
			pds.CurrentPageIndex = CurrentPage;
			lnkbtnNext.Visible = !pds.IsLastPage;
			lnkbtnLast.Visible = !pds.IsLastPage;
			lnkbtnPrevious.Visible = !pds.IsFirstPage;
			lnkbtnFirst.Visible = !pds.IsFirstPage;
			MyAccordion.DataSource = pds;
			MyAccordion.DataBind();
			doPaging();
			lblCantidad.Text = dt.Rows.Count.ToString() + " Mensajes";
			udpGrilla.Update();
		}
		#endregion

		private void doPaging()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("PageIndex");
			dt.Columns.Add("PageText");
			for (int i = 0; i < pds.PageCount; i++)
			{
				DataRow dr = dt.NewRow();
				dr[0] = i;
				dr[1] = i + 1;
				dt.Rows.Add(dr);
			}
			dlPaging.DataSource = dt;
			dlPaging.DataBind();
		}

		protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
		{
			if (e.CommandName.Equals("lnkbtnPaging"))
			{
				CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
				BuscarMensajes();
			}
		}

		protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
			if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
			{
				lnkbtnPage.Enabled = false;
				lnkbtnPage.Font.Bold = true;
			}
		}

		protected void lnkbtnPrevious_Click(object sender, EventArgs e)
		{
			CurrentPage -= 1;
			BuscarMensajes();
		}

		protected void lnkbtnNext_Click(object sender, EventArgs e)
		{
			CurrentPage += 1;
			BuscarMensajes();
		}

		protected void lnkbtnLast_Click(object sender, EventArgs e)
		{
			CurrentPage = dlPaging.Controls.Count - 1;
			BuscarMensajes();
		}

		protected void lnkbtnFirst_Click(object sender, EventArgs e)
		{
			CurrentPage = 0;
			BuscarMensajes();
		}

		protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			CurrentPage = 0;
			BuscarMensajes();
		}
	}
}