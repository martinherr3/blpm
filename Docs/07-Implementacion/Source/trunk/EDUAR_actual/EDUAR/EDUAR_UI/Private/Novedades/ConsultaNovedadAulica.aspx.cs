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
                if (ViewState["idCursoCicloLectivo"] == null)
                    ViewState["idCursoCicloLectivo"] = 0;
                return (int)ViewState["idCursoCicloLectivo"];
            }
            set { ViewState["idCursoCicloLectivo"] = value; }
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
                    listaNovedades = objBLNovedad.GetNovedad(filtro);
                    CargarGrilla();
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

       
    }
}