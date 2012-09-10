using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_UI.UserControls;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_BusinessLogic.Common;
using EDUAR_UI.Utilidades;

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

        protected void btnInformeIndicador_Click(object sender, EventArgs e)
        {
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
                InformeIndicador1.Desde = DateTime.Today;

                if (btn.CommandName == "Principal")
                    InformeIndicador1.Hasta = DateTime.Today.AddDays(indi.HastaPrincipal);
                else if (btn.CommandName == "Intermedio")
                    InformeIndicador1.Hasta = DateTime.Today.AddDays(indi.HastaIntermedio);
                else if (btn.CommandName == "Secundario")
                    InformeIndicador1.Hasta = DateTime.Today.AddDays(indi.HastaSecundario);

                #region [Filtros]
                for (int i = 0; i < indi.Filtros.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            InformeIndicador1.Filtro1 = indi.Filtros[i].ToString();
                            break;
                        case 1:
                            InformeIndicador1.Filtro2 = indi.Filtros[i].ToString();
                            break;
                        case 2:
                            InformeIndicador1.Filtro3 = indi.Filtros[i].ToString();
                            break;
                        case 3:
                            InformeIndicador1.Filtro4 = indi.Filtros[i].ToString();
                            break;
                        case 4:
                            InformeIndicador1.Filtro5 = indi.Filtros[i].ToString();
                            break;
                        case 5:
                            InformeIndicador1.Filtro6 = indi.Filtros[i].ToString();
                            break;
                        default:
                            break;
                    }
                }
                #endregion

                InformeIndicador1.Show();
            }
        }
        #endregion

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
    }
}