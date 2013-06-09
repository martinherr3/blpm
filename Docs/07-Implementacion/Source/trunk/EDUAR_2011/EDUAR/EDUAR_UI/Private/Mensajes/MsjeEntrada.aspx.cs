using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Constantes;
using System.Drawing;
using System.Text;
using System.IO;
using System.Web;

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
				Master.BotonAvisoAceptar += (VentanaAceptar);
				if (!Page.IsPostBack)
				{
					BuscarMensajes();
					divContenido.Visible = false;
					divReply.Visible = false;
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
					case enumAcciones.Modificar:
						AccionPagina = enumAcciones.Limpiar;
						//CargarGrilla();
						CargarPresentacion();
						udpGrilla.Update();
						break;
					case enumAcciones.Eliminar:
						AccionPagina = enumAcciones.Limpiar;
						EliminarMensaje(propMensaje.idMensajeDestinatario);
						//CargarGrilla();
						CargarPresentacion();
						udpGrilla.Update();
						break;
					case enumAcciones.Limpiar:
						CargarPresentacion();
						break;
					case enumAcciones.Seleccionar:
						EliminarSeleccionados();
						CargarPresentacion();
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
		/// Método que se llama al hacer click sobre las acciones de la grilla
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
		protected void gvwReporte_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				int idMensajeDestinatario = Convert.ToInt32(e.CommandArgument);
				Mensaje objMensaje = null;
				switch (e.CommandName)
				{
					case "Leer":
						CargarMensajeEnPantalla(idMensajeDestinatario);
						GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
						gvwReporte.Rows[row.RowIndex].BackColor = Color.Gainsboro;
						break;
					case "Responder":
						objMensaje = new Mensaje();
						objMensaje = listaMensajes.Find(p => p.idMensajeDestinatario == idMensajeDestinatario);
						txtAsunto.Text = "Re: " + objMensaje.asuntoMensaje;
						lblDestinatario.Text = objMensaje.remitente.apellido + "  " + objMensaje.remitente.nombre;
						hdfDestinatario.Value = objMensaje.remitente.idPersona.ToString();
                        textoMensaje.contenido = "<br /><hr style='border-style: dashed' />" + objMensaje.textoMensaje;
						btnEnviar.Visible = true;
						btnVolver.Visible = true;
						btnEliminar.Visible = false;
						udpReporte.Visible = false;
						divContenido.Visible = false;
						divPaginacion.Visible = false;
						divReply.Visible = true;
						break;
					case "Eliminar":
						AccionPagina = enumAcciones.Eliminar;
						propMensaje.idMensajeDestinatario = Convert.ToInt32(e.CommandArgument.ToString());
						divContenido.Visible = false;
						divReply.Visible = false;
						Master.MostrarMensaje(this.Page.Title, UIConstantesGenerales.MensajeEliminar, enumTipoVentanaInformacion.Confirmación);
						break;
				}
				udpGrilla.Update();
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
			Mensaje objMensaje = new Mensaje();

			objMensaje.asuntoMensaje = txtAsunto.Text;
			objMensaje.textoMensaje = textoMensaje.contenido;
			objMensaje.remitente.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
			objMensaje.fechaEnvio = DateTime.Now;
			objMensaje.horaEnvio = Convert.ToDateTime(DateTime.Now.Hour + ":" + DateTime.Now.Minute);

			Persona destinatario = new Persona();
			destinatario.idPersona = Convert.ToInt32(hdfDestinatario.Value);

			objMensaje.ListaDestinatarios.Add(destinatario);

			BLMensaje objBLMensaje = new BLMensaje(objMensaje);
			objBLMensaje.Save();
			AccionPagina = enumAcciones.Limpiar;

			Master.MostrarMensaje(this.Page.Title, UIConstantesGenerales.MensajeUnicoDestino, enumTipoVentanaInformacion.Satisfactorio);
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
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnEliminar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnEliminar_Click(object sender, EventArgs e)
		{
			try
			{
				bool haySeleccion = false;
				for (int i = 0; i < gvwReporte.Rows.Count; i++)
				{
					CheckBox checkbox = (CheckBox)gvwReporte.Rows[i].FindControl("checkEliminar");
					if (checkbox != null && checkbox.Checked)
					{
						haySeleccion = true;
						break;
					}
				}
				if (haySeleccion)
				{
					AccionPagina = enumAcciones.Seleccionar;
					Master.MostrarMensaje(this.Page.Title, UIConstantesGenerales.MensajeEliminarMensajesSeleccionados, enumTipoVentanaInformacion.Confirmación);
				}
				else
				{
					AccionPagina = enumAcciones.Limpiar;
					Master.MostrarMensaje(this.Page.Title, UIConstantesGenerales.MensajeSinSeleccion, enumTipoVentanaInformacion.Advertencia);
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Headers the checked changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void HeaderCheckedChanged(object sender, EventArgs e)//this is for header checkbox changed event
		{
			CheckBox cbSelectedHeader = (CheckBox)gvwReporte.HeaderRow.FindControl("cboxhead");
			//if u checked header checkbox automatically all the check boxes will be checked,viseversa
			foreach (GridViewRow row in gvwReporte.Rows)
			{
				CheckBox cbSelected = (CheckBox)row.FindControl("checkEliminar");
				if (cbSelectedHeader.Checked == true)
				{
					cbSelected.Checked = true;
				}
				else
				{
					cbSelected.Checked = false;
				}
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			BuscarMensajes();
			btnEnviar.Visible = false;
			btnVolver.Visible = false;
			btnEliminar.Visible = true;
			udpReporte.Visible = true;
			divPaginacion.Visible = true;
			divContenido.Visible = false;
			divReply.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Buscars the entidads.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void BuscarMensajes()
		{
			Mensaje entidad = new Mensaje();
			entidad.destinatario.username = ObjSessionDataUI.ObjDTUsuario.Nombre;
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
			gvwReporte.DataSource = pds;
			gvwReporte.DataBind();
			doPaging();
			lblCantidad.Text = dt.Rows.Count.ToString() + " Mensajes";
			divContenido.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Eliminars the mensaje.
		/// </summary>
		/// <param name="idMensajeDestinatario">The id mensaje destinatario.</param>
		private void EliminarMensaje(int idMensajeDestinatario)
		{
			Mensaje objMensaje = listaMensajes.Find(p => p.idMensajeDestinatario == idMensajeDestinatario);
			objMensaje.listaIDMensaje = idMensajeDestinatario.ToString();
			objMensaje.idMensaje = 0;
			objMensaje.leido = true;
			objMensaje.activo = false;
			BLMensaje objBLMensaje = new BLMensaje(objMensaje);
			objBLMensaje.EliminarMensaje();
			listaMensajes.Remove(objMensaje);
		}

		/// <summary>
		/// Eliminars the seleccionados.
		/// </summary>
		private void EliminarSeleccionados()
		{
			Mensaje objMensajesEliminar = new Mensaje();
			for (int i = 0; i < gvwReporte.Rows.Count; i++)
			{
				CheckBox checkbox = (CheckBox)gvwReporte.Rows[i].FindControl("checkEliminar");
				if (checkbox != null && checkbox.Checked)
				{
					int idMensajeDestinatario = 0;
					Int32.TryParse(checkbox.Text, out idMensajeDestinatario);
					if (idMensajeDestinatario > 0)
						objMensajesEliminar.listaIDMensaje += string.Format("{0},", idMensajeDestinatario.ToString());
				}
			}
			if (!string.IsNullOrEmpty(objMensajesEliminar.listaIDMensaje))
			{
				objMensajesEliminar.listaIDMensaje = objMensajesEliminar.listaIDMensaje.Substring(0, objMensajesEliminar.listaIDMensaje.Length - 1);
				objMensajesEliminar.idMensajeDestinatario = 1;
				objMensajesEliminar.idMensaje = 0;
				objMensajesEliminar.leido = true;
				objMensajesEliminar.activo = false;
				objBLMensaje = new BLMensaje(objMensajesEliminar);
				objBLMensaje.EliminarListaMensajes();
			}
		}

		/// <summary>
		/// Cargars the mensaje en pantalla.
		/// </summary>
		/// <param name="idMensajeDestinatario">The id mensaje destinatario.</param>
		private void CargarMensajeEnPantalla(int idMensajeDestinatario)
		{
			foreach (GridViewRow item in gvwReporte.Rows)
			{
				item.BackColor = Color.Transparent;
			}
			Mensaje objMensaje = new Mensaje();
			objMensaje = listaMensajes.Find(p => p.idMensajeDestinatario == idMensajeDestinatario);
			if (!objMensaje.leido)
			{
				//objMensaje.idMensajeDestinatario = idMensajeDestinatario;
				objMensaje.leido = true;
				BLMensaje objBLMensaje = new BLMensaje(objMensaje);
				objBLMensaje.LeerMensaje();
				listaMensajes.Find(p => p.idMensajeDestinatario == idMensajeDestinatario).leido = true;
				//Master.RaiseCallbackEvent(e.ToString());
				CargarGrilla();
			}
			litAsunto.Text = objMensaje.asuntoMensaje;
            litFecha.Text = objMensaje.fechaEnvio.ToShortDateString() + " " + objMensaje.horaEnvio.Hour.ToString().PadLeft(2, '0') + ":" + objMensaje.horaEnvio.Minute.ToString().PadLeft(2, '0');
			litRemitente.Text = objMensaje.remitente.apellido + "  " + objMensaje.remitente.nombre + " <b>(" + objMensaje.remitente.tipoPersona.nombre + ")</b>";
			litContenido.Text = objMensaje.textoMensaje;
			divContenido.Visible = true;
			divPaginacion.Visible = true;
			divReply.Visible = false;
		}
		#endregion

		#region --[Paginación]--
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
		#endregion
	}
}