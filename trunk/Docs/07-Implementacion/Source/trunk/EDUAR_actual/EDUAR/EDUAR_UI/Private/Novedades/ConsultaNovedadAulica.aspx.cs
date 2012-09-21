using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_BusinessLogic.Common;

namespace EDUAR_UI
{
    public partial class ConsultaNovedadAulica : EDUARBasePage
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
        /// Gets or sets the id id Curso Ciclo Lectivo.
        /// </summary>
        /// <value>
        /// The id curso Ciclo Lectivo.
        /// </value>
        public int idCursoCicloLectivo
        {
            get
            {
                if (Session["idCursoCicloLectivo"] == null)
                    Session["idCursoCicloLectivo"] = 0;
                return (int)Session["idCursoCicloLectivo"];
            }
            set { Session["idCursoCicloLectivo"] = value; }
        }


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
                //Master.BotonAvisoAceptar += (VentanaAceptar);

                if (!Page.IsPostBack)
                {
                    UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "Nombre", true);
                    //CargarPresentacion();
                }
                //chkAprobada.Attributes.Add("onclick", "if(!jConfirm('¿Desea aprobar la presente planificación?','Confirmación')) {return false};");
                //chkSolicitarAprobacion.Attributes.Add("onclick", "if(!jConfirm('¿Desea solicitar la aprobación de la presente planificación?''Confirmación')) {return false};");
            }
            catch (Exception ex)
            {
                AvisoMostrar = true;
                AvisoExcepcion = ex;
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
                novControl.visible = false;
                btnVolver.Visible = false;
                SetDivVisible(true);
                CargarGrilla();
                novedadConversacion = new Novedad();
                udpBotonera.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCursoSeleccion = 0;
                int.TryParse(ddlCurso.SelectedValue, out idCursoSeleccion);

                if (idCursoSeleccion > 0)
                {
                    Novedad filtro = new Novedad();
                    filtro.curso.idCurso = idCursoSeleccion;
                    idCursoCicloLectivo = idCursoSeleccion;
                    BLNovedad objBLNovedad = new BLNovedad();
                    listaNovedades = objBLNovedad.GetNovedadesPadre(filtro);
                    CargarGrilla();
                    //novControl.visible = true;
                    SetDivVisible(true);
                    udpBotonera.Update();
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

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
        #endregion

        #region --[Métodos Privados]--
        private void CargarGrilla()
        {
            gvwNovedades.DataSource = listaNovedades;
            gvwNovedades.DataBind();
            udpGrilla.Update();
        }
        #endregion

        protected void gvwNovedades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "verConversacion":
                        novedadConversacion.idNovedad = Convert.ToInt32(e.CommandArgument.ToString());
                        btnVolver.Visible = true;
                        novControl.visible = true;
                        novControl.idNovedadPadre = novedadConversacion.idNovedad;
                        CargarConversacion();
                        //CargaAgenda();
                        //lblTitulo.Text = "Agenda " + propAgenda.cursoCicloLectivo.curso.nombre + " - " + propAgenda.cursoCicloLectivo.cicloLectivo.nombre;
                        break;
                }
                udpBotonera.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        private void CargarConversacion()
        {
            BLNovedad objBLNovedad = new BLNovedad();
            List<Novedad> listaFiltro = objBLNovedad.GetNovedad(new Novedad() { novedadPadre = new Novedad() { idNovedad = novedadConversacion.idNovedad } });

            SetDivVisible(false);

            rptConversacion.DataSource = listaFiltro;
            rptConversacion.DataBind();
            udpConversacion.Update();
        }

        private void SetDivVisible(bool visible)
        {
            divGrilla.Visible = visible;
            udpGrilla.Update();
            divConversacion.Visible = !visible;
            udpConversacion.Update();
        }
    }
}