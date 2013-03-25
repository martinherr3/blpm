using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Encuestas;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
	public partial class ManageCategoriasPregunta : EDUARBasePage
	{
		#region --[Atributos]--
        private BLCategoriaPregunta objBLCategoriaPregunta;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Mantiene la categoria de pregunta seleccionada en la grilla.
		/// </summary>
		/// <value>
        /// The prop categoria de pregunta.
		/// </value>
        public CategoriaPregunta propCategoriaPregunta
		{
			get
			{
                if (ViewState["propCategoriaPregunta"] == null)
                    propCategoriaPregunta = new CategoriaPregunta();

                return (CategoriaPregunta)ViewState["propCategoriaPregunta"];
			}
            set { ViewState["propCategoriaPregunta"] = value; }
		}

		/// <summary>
        /// Gets or sets the prop filtro CategoriaPregunta.
		/// </summary>
		/// <value>
        /// The prop filtro CategoriaPregunta.
		/// </value>
        public CategoriaPregunta propFiltroCategoriaPregunta
		{
			get
			{
				if (ViewState["propFiltroEncuesta"] == null)
                    propFiltroCategoriaPregunta = new CategoriaPregunta();

                return (CategoriaPregunta)ViewState["propFiltroCategoriaPregunta"];
			}
            set { ViewState["propFiltroCategoriaPregunta"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista encuesta.
		/// </summary>
		/// <value>
		/// The lista agenda.
		/// </value>
        public List<CategoriaPregunta> listaCategoriaPregunta
		{
			get
			{
                if (ViewState["listaCategoriaPregunta"] == null) listaCategoriaPregunta = new List<CategoriaPregunta>();

                return (List<CategoriaPregunta>)ViewState["listaCategoriaPregunta"];
			}
            set { ViewState["listaCategoriaPregunta"] = value; }
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

        public int idCategoriaPregunta
		{
			get
			{
                if (ViewState["idCategoriaPregunta"] == null) idCategoriaPregunta = 0;

                return (int)ViewState["idCategoriaPregunta"];
			}
            set { ViewState["idCategoriaPregunta"] = value; }
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
		/// Gets or sets the categoria sesion.
		/// </summary>
		/// <value>
		/// The encuesta sesion.
		/// </value>
        public CategoriaPregunta categoriaPreguntaSesion
		{
			get
			{
                if (Session["categoriaPreguntaSesion"] == null)
                    categoriaPreguntaSesion = new CategoriaPregunta();

                return (CategoriaPregunta)Session["categoriaPreguntaSesion"];
			}
            set { Session["categoriaPreguntaSesion"] = value; }
		}

        //public bool categoriaUtilizada
        //{
        //    get
        //    {
        //        if (ViewState["categoriaUtilizada"] == null)
        //        {
        //            objBLCategoriaPregunta = new BLCategoriaPregunta(propCategoriaPregunta);
        //            categoriaUtilizada = objBLCategoriaPregunta.EsCategoriaUtilizada(propCategoriaPregunta);
        //        }
        //        return (bool)ViewState["categoriaUtilizada"];
        //    }
        //    set { ViewState["categoriaUtilizada"] = value; }
        //}
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
					BuscarCategoriaPregunta(null);
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
					case enumAcciones.Eliminar:
						AccionPagina = enumAcciones.Limpiar;
                        EliminarCategoria();
						BuscarFiltrando();
						break;
					case enumAcciones.Limpiar:
						CargarPresentacion();
						BuscarCategoriaPregunta(null);
						break;
					case enumAcciones.Guardar:
						AccionPagina = enumAcciones.Limpiar;
						GuardarCategoriaPregunta(ObtenerValoresDePantalla());
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
				ddlAmbitoEdit.Enabled = true;
				btnGuardar.Visible = true;
				btnBuscar.Visible = false;
				btnVolver.Visible = true;
				btnNuevo.Visible = false;
				gvwCategorias.Visible = false;
				litEditar.Visible = false;
				litNuevo.Visible = true;
				udpEdit.Visible = true;
				udpFiltrosBusqueda.Visible = false;
                lblUtilizada.Visible = false;
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
				BuscarCategoriaPregunta(propFiltroCategoriaPregunta);
				propCategoriaPregunta = new CategoriaPregunta();
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
        protected void gvwCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Editar":
                        propCategoriaPregunta.idCategoriaPregunta = Convert.ToInt32(e.CommandArgument.ToString());
						CargaCategoria();
						ddlAmbitoEdit.Enabled = false;
						break;
					case "Eliminar":
						propCategoriaPregunta.idCategoriaPregunta = Convert.ToInt32(e.CommandArgument.ToString());
						AccionPagina = enumAcciones.Eliminar;
                        Master.MostrarMensaje("Eliminar Categoría de Pregunta", "¿Desea <b>eliminar</b> la categoría de pregunta?", enumTipoVentanaInformacion.Confirmación);
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
        protected void gvwCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
                gvwCategorias.PageIndex = e.NewPageIndex;
				CargarGrilla();
				btnBuscar.Visible = false;
				btnVolver.Visible = true;
                gvwCategorias.Visible = false;
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
				if (int.TryParse(ddlAmbitoEdit.SelectedValue, out idAmbito) && idAmbito > 0)
				{
                    //TODO: No se si hace falta un if
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
			gvwCategorias.DataSource = UIUtilidades.BuildDataTable<CategoriaPregunta>(listaCategoriaPregunta).DefaultView;
			gvwCategorias.DataBind();
			udpEdit.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Carga el contenido de la grilla de encuestas.
		/// </summary>
		private void CargarPresentacion()
		{
			LimpiarCampos();
			lblTitulo.Text = "Categorías de Pregunta";
			CargarCombos();
			udpEdit.Visible = false;
			btnVolver.Visible = false;
			btnGuardar.Visible = false;
			udpFiltrosBusqueda.Visible = true;
			btnBuscar.Visible = true;
			btnNuevo.Visible = true;
			gvwCategorias.Visible = true;
			udpFiltros.Update();
			udpGrilla.Update();
		}

		/// <summary>
		/// Buscars the entidads.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void BuscarCategoriaPregunta(CategoriaPregunta entidad)
		{
			CargarLista(entidad);
			CargarGrilla();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void CargarLista(CategoriaPregunta entidad)
		{
			objBLCategoriaPregunta = new BLCategoriaPregunta(entidad);
			listaCategoriaPregunta = objBLCategoriaPregunta.GetCategoriasPregunta(entidad);
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
		private CategoriaPregunta ObtenerValoresDePantalla()
		{
            CategoriaPregunta entidad = new CategoriaPregunta();
			entidad = propCategoriaPregunta;

			if (!esNuevo)
			{
				entidad.idCategoriaPregunta = propCategoriaPregunta.idCategoriaPregunta;
			}

			if (Convert.ToInt32(ddlAmbitoEdit.SelectedValue) > 0)
			{
                entidad.ambito.idAmbitoEncuesta = Convert.ToInt32(ddlAmbitoEdit.SelectedValue);
				entidad.nombre = txtNombreEdit.Text.Trim();
                entidad.descripcion = txtDescripcionEdit.Text.Trim();
			}

			return entidad;
		}

		/// <summary>
		/// Guardars the encuesta.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void GuardarCategoriaPregunta(CategoriaPregunta entidad)
		{
			objBLCategoriaPregunta = new BLCategoriaPregunta(entidad);
			objBLCategoriaPregunta.Save();
		}

		/// <summary>
		/// Cargars the entidad.
		/// </summary>
		private void CargarValoresEnPantalla(int idEntidad)
		{
			CategoriaPregunta categoria = listaCategoriaPregunta.Find(c => c.idCategoriaPregunta == idEntidad);

            objBLCategoriaPregunta = new BLCategoriaPregunta(categoria);

			ddlAmbitoEdit.SelectedValue = categoria.ambito.idAmbitoEncuesta.ToString();
            txtNombreEdit.Text = categoria.nombre;
			txtDescripcionEdit.Text = categoria.descripcion;
            if (!objBLCategoriaPregunta.EsCategoriaDisponible(idEntidad)) lblUtilizada.Text = "<b>Esta categoría está siendo actualmente utilizada en al menos una encuesta</b>";
            else lblUtilizada.Text = "<b>Esta categoría no está siendo actualmente utilizada en ninguna encuesta</b>";
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			string mensaje = string.Empty;
		    
            if (string.IsNullOrEmpty(txtNombreEdit.Text))
                mensaje += "- Nombre Categoría<br />";

            if (string.IsNullOrEmpty(txtDescripcionEdit.Text))
                mensaje += "- Descripción Categoría<br />";

			int validador = 0;
			
            int.TryParse(ddlAmbitoEdit.SelectedValue, out validador);
            
            if (validador <= 0)
				mensaje += "- Ambito<br />";
			
			return mensaje;
		}

		/// <summary>
		/// Cargas the agenda.
		/// </summary>
		private void CargaCategoria()
		{
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;

            ViewState["categoriaUtilizada"] = null;

			CargarValoresEnPantalla(propCategoriaPregunta.idCategoriaPregunta);

            lblUtilizada.Visible = true;
			litEditar.Visible = true;
			litNuevo.Visible = false;
			btnBuscar.Visible = false;
			btnNuevo.Visible = false;
			btnVolver.Visible = true;
			btnGuardar.Visible = true;
			gvwCategorias.Visible = false;
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
			lblTitulo.Text = "Categorías";
			CategoriaPregunta entidad = new CategoriaPregunta();

			AmbitoEncuesta ambito = new AmbitoEncuesta();
			ambito.idAmbitoEncuesta = Convert.ToInt32(ddlAmbito.SelectedValue);

			entidad.ambito = ambito;

			propFiltroCategoriaPregunta = entidad;
			BuscarCategoriaPregunta(entidad);
		}

		/// <summary>
		/// Limpiar the campos.
		/// </summary>
		private void LimpiarCampos()
		{
			if (ddlAmbito.Items.Count > 0) ddlAmbito.SelectedIndex = 0;
			txtNombreEdit.Text = string.Empty;
			txtDescripcionEdit.Text = string.Empty;
		}

		/// <summary>
		/// Eliminars the encuesta.
		/// </summary>
		private void EliminarCategoria()
		{
			CategoriaPregunta categoria = new CategoriaPregunta() { idCategoriaPregunta = propCategoriaPregunta.idCategoriaPregunta};
			objBLCategoriaPregunta = new BLCategoriaPregunta(categoria);

            objBLCategoriaPregunta.Delete();
		}

        //public bool esEliminable(int id)
        //{
        //    CategoriaPregunta categoria = new CategoriaPregunta();// { idCategoriaPregunta = id };
        //    objBLCategoriaPregunta = new BLCategoriaPregunta(); //(categoria);

        //    return objBLCategoriaPregunta.EsCategoriaDisponible(id);
        //}
		#endregion
	}
}