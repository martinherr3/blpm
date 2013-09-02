using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
    public partial class ConsultaNovedadAulica : EDUARBasePage
    {
        #region --[Propiedades]--
        public List<Novedad> listaNovedades
        {
            get
            {
                if (ViewState["listaNovedades"] == null)
                    ViewState["listaNovedades"] = new List<Novedad>();
                return (List<Novedad>)ViewState["listaNovedades"];
            }
            set { ViewState["listaNovedades"] = value; }

        }

        public Novedad novedadConversacion
        {
            get
            {
                if (ViewState["novedadConversacion"] == null)
                    ViewState["novedadConversacion"] = new Novedad();
                return (Novedad)ViewState["novedadConversacion"];
            }
            set { ViewState["novedadConversacion"] = value; }

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
                novControl.GuardarClick += (Guardar);
                Master.BotonAvisoAceptar += (VentanaAceptar);
                if (!Page.IsPostBack)
                {
                    CargarCurso();
                    if (base.idNovedadConsulta > 0)
                    {
                        CargarConversacion(base.idCursoCicloLectivo);
                        novedadConversacion.idNovedad = base.idNovedadConsulta;
                        novedadConversacion = listaNovedades.Find(p => p.idNovedad == base.idNovedadConsulta);
                        CargarConversacion();
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

            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Guardars the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Guardar(object sender, EventArgs e)
        {
            try
            {
                novControl.GuardarNovedad();

                CargarConversacion();
                if (novControl.novedadPadre.idNovedad == 0)
                    novControl.novedadPadre = novedadConversacion;

                novControl.visible = !novedadConversacion.estado.esFinal;

            }
            catch (Exception ex)
            { Master.ManageExceptions(ex); }
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
                novedadConversacion = new Novedad();
                CargarConversacion(idCursoCicloLectivo);
                novControl.visible = true;
                novControl.novedadPadre = null;
                novControl.ToolTip = "Registrar";
                btnVolver.Visible = false;
                btnVolverAnterior.Visible = true;
                udpBotonera.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnVolverAnterior control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnVolverAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                base.idCursoCicloLectivo = 0;
                base.cursoActual = new CursoCicloLectivo();
                Response.Redirect("~/Private/AccesoCursos.aspx", false);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the gvwNovedades control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvwNovedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvwNovedades.PageIndex = e.NewPageIndex;
                CargarGrilla();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gvwNovedades control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gvwNovedades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "verConversacion":
                        novedadConversacion = listaNovedades.Find(p => p.idNovedad == Convert.ToInt32(e.CommandArgument.ToString()));
                        CargarConversacion();
                        novControl.ToolTip = "Responder";
                        break;
                }
                udpBotonera.Update();
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
        private void CargarGrilla()
        {
            gvwNovedades.DataSource = listaNovedades;
            gvwNovedades.DataBind();
            udpGrilla.Update();
        }

        /// <summary>
        /// Cargars the conversacion.
        /// </summary>
        private void CargarConversacion()
        {
            btnVolver.Visible = true;
            novControl.ToolTip = "Registrar";
            btnVolverAnterior.Visible = false;
            novControl.novedadPadre = novedadConversacion;
            novControl.visible = !novedadConversacion.estado.esFinal;

            BLNovedad objBLNovedad = new BLNovedad(novedadConversacion);
            objBLNovedad.GetById();
            novedadConversacion = objBLNovedad.Data;

            List<Novedad> listaFiltro = objBLNovedad.GetNovedad(new Novedad() { novedadPadre = new Novedad() { idNovedad = novedadConversacion.idNovedad } });

            SetDivVisible(false);

            rptConversacion.DataSource = listaFiltro;
            rptConversacion.DataBind();
            udpConversacion.Update();
            udpBotonera.Update();
        }

        /// <summary>
        /// Sets the div visible.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        private void SetDivVisible(bool visible)
        {
            divGrilla.Visible = visible;
            udpGrilla.Update();
            divConversacion.Visible = !visible;
            udpConversacion.Update();
        }

        /// <summary>
        /// Cargars the conversacion.
        /// </summary>
        /// <param name="idCursoSeleccion">The id curso seleccion.</param>
        private void CargarConversacion(int idCursoSeleccion)
        {
            if (idCursoSeleccion > 0)
            {
                Novedad filtro = new Novedad();
                filtro.curso.idCurso = idCursoSeleccion;
                idCursoCicloLectivo = idCursoSeleccion;
                BLNovedad objBLNovedad = new BLNovedad();
                listaNovedades = objBLNovedad.GetNovedadesPadre(filtro);
                CargarGrilla();
                SetDivVisible(true);

            }
            else
            {
                divConversacion.Visible = false;
                divGrilla.Visible = false;
                udpConversacion.Update();
                udpGrilla.Update();
            }
            if (novedadConversacion.estado.esFinal) novControl.visible = false;
            else novControl.visible = idCursoSeleccion > 0;
            udpBotonera.Update();
        }

        /// <summary>
        /// Cargars the curso.
        /// </summary>
        private void CargarCurso()
        {
            novControl.visible = true;
            CargarConversacion(base.idCursoCicloLectivo);
            lblTitulo.Text = "Novedades Aulicas - " + base.cursoActual.curso.nombre;
        }
        #endregion
    }
}