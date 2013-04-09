using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Entities.Reports;

namespace EDUAR_UI
{
    public partial class IndicadoresGenerales : EDUARBasePage
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
                    listaCursos.Sort((emp1, emp2) => emp1.nombre.CompareTo(emp2.nombre));
                }
                return (List<Curso>)ViewState["listaCursos"];
            }
            set { ViewState["listaCursos"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista indicadores.
        /// </summary>
        /// <value>
        /// The lista indicadores.
        /// </value>
        public List<EDUAR_Entities.Reports.Indicador> listaIndicadores
        {
            get
            {
                if (ViewState["listaIndicadores"] == null && cicloLectivoActual != null)
                {
                    EDUAR_BusinessLogic.Reports.BLIndicador objBLIndicador = new EDUAR_BusinessLogic.Reports.BLIndicador();
                    listaIndicadores = objBLIndicador.GetIndicadores(null);
                }
                return (List<EDUAR_Entities.Reports.Indicador>)ViewState["listaIndicadores"];
            }
            set { ViewState["listaIndicadores"] = value; }
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
                InformeIndicador1.SalirClick += (this.btnInformeIndicadorSalir_Click);

                Master.BotonAvisoAceptar += (VentanaAceptar);

                foreach (DataListItem item in dtlCursos.Controls)
                {
                    foreach (Control itemControl in item.Controls)
                    {
                        UserControls.Indicador indi = new UserControls.Indicador();

                        if (itemControl.GetType().BaseType == indi.GetType())
                        {
                            indi = (UserControls.Indicador)itemControl;
                            indi.SetEventoClick(this.btnInformeIndicador_Click);
                            break;
                        }
                    }
                }

                if (!IsPostBack)
                {
                    //CargarCurso();

                    dtlIndicadores.DataSource = listaIndicadores;
                    dtlIndicadores.DataBind();
                    udpIndicadores.Update();
                    divCursos.Visible = false;
                    udpCursos.Update();
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
                bool salir = false;
                foreach (DataListItem item in dtlCursos.Controls)
                {
                    foreach (Control itemControl in item.Controls)
                    {
                        indi = new UserControls.Indicador();
                        if (itemControl.GetType().BaseType == indi.GetType())
                        {
                            indi = (UserControls.Indicador)itemControl;
                            if (indi.idCursoIndicador == ((EDUAR_UI.UserControls.Indicador)(((System.Web.UI.Control)(sender)).TemplateControl)).idCursoIndicador)
                                salir = true;
                            break;
                        }
                    }
                    if (salir) break;

                }

                if (indi != null)
                {
                    InformeIndicador1.Titulo = indi.Título;
                    InformeIndicador1.SP = "DatosIndicador_" + indi.nombreSP;
                    InformeIndicador1.Hasta = DateTime.Today;
                    InformeIndicador1.idCursoIndicador = indi.idCursoIndicador;

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
        /// Handles the ItemCommand event of the dtlIndicadores control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="DataListCommandEventArgs"/> instance containing the event data.</param>
        protected void dtlIndicadores_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                int idIndicador = 0;
                int.TryParse(e.CommandArgument.ToString(), out idIndicador);
                if (e.CommandName == "Indicador")
                {
                    lblIndicador.Text = ((LinkButton)(e.CommandSource)).Text;
                    dtlCursos.DataSource = listaCursos;
                    dtlCursos.DataBind();

                    CargarIndicador(idIndicador);

                    divCursos.Visible = true;
                    udpCursos.Update();
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
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Cargars the indicador.
        /// </summary>
        /// <param name="idIndicador">The id indicador.</param>
        private void CargarIndicador(int idIndicador)
        {
            UserControls.Indicador indi = null;
            Indicador miIndicador = listaIndicadores.Find(p => p.idIndicador == idIndicador);

            foreach (DataListItem item in dtlCursos.Controls)
            {
                foreach (Control itemControl in item.Controls)
                {
                    indi = new UserControls.Indicador();
                    if (itemControl.GetType().BaseType == indi.GetType())
                    {
                        indi = (UserControls.Indicador)itemControl;
                        if (indi != null)
                        {
                            indi.SetEventoClick(this.btnInformeIndicador_Click);

                            indi.Visible = true;
                            indi.InvertirEscala = Convert.ToBoolean(miIndicador.invertirEscala.ToString());

                            indi.HastaPrincipal = miIndicador.diasHastaPrincipal;
                            if (DateTime.Today.Subtract(cicloLectivoActual.fechaInicio).Days < indi.HastaPrincipal)
                                indi.HastaPrincipal = DateTime.Today.Subtract(cicloLectivoActual.fechaInicio).Days;

                            indi.HastaIntermedio = miIndicador.diasHastaIntermedio;
                            if (DateTime.Today.Subtract(cicloLectivoActual.fechaInicio).Days < indi.HastaIntermedio)
                                indi.HastaIntermedio = DateTime.Today.Subtract(cicloLectivoActual.fechaInicio).Days;

                            indi.HastaSecundario = miIndicador.diasHastaSecundario;
                            if (DateTime.Today.Subtract(cicloLectivoActual.fechaInicio).Days < indi.HastaSecundario)
                                indi.HastaSecundario = DateTime.Today.Subtract(cicloLectivoActual.fechaInicio).Days;

                            indi.VerdePrincipal = miIndicador.verdeNivelPrincipal;
                            indi.RojoPrincipal = miIndicador.rojoNivelPrincipal;
                            indi.VerdeIntermedio = miIndicador.verdeNivelIntermedio;
                            indi.RojoSecundario = miIndicador.rojoNivelIntermedio;
                            indi.VerdeSecundario = miIndicador.verdeNivelSecundario;
                            indi.RojoSecundario = miIndicador.rojoNivelSecundario;
                            indi.nombreSP = miIndicador.nombreSP;
                            indi.Título = indi.cursoIndicador + " - " + miIndicador.nombre;
                            //indi.Título = indi.cursoIndicador;
                            indi.CargarIndicador();
                            break;
                        }
                    }
                }
            }
            udpCursos.Update();
        }
        #endregion
    }
}