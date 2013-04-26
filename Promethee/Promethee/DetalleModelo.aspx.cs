using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Entity;
using DataAccess;
using System.Data;
using Promethee.Utility;

namespace Promethee
{
    public partial class DetalleModelo : System.Web.UI.Page
    {
        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the id modelo.
        /// </summary>
        /// <value>
        /// The id modelo.
        /// </value>
        public int idModelo
        {
            get
            {
                if (Session["idModelo"] == null)
                    idModelo = 0;

                return (int)Session["idModelo"];
            }
            set { Session["idModelo"] = value; }
        }

        /// <summary>
        /// Gets or sets the id criterio.
        /// </summary>
        /// <value>
        /// The id criterio.
        /// </value>
        public int idCriterio
        {
            get
            {
                if (ViewState["idCriterio"] == null)
                    idCriterio = 0;

                return (int)ViewState["idCriterio"];
            }
            set { ViewState["idCriterio"] = value; }
        }

        /// <summary>
        /// Gets or sets the mi modelo.
        /// </summary>
        /// <value>
        /// The mi modelo.
        /// </value>
        public ModeloEntity miModelo
        {
            get
            {
                if (Session["miModelo"] == null)
                    miModelo = new ModeloEntity();

                return (ModeloEntity)Session["miModelo"];
            }
            set { Session["miModelo"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista alternativa.
        /// </summary>
        /// <value>
        /// The lista alternativa.
        /// </value>
        public List<AlternativaEntity> listaAlternativa
        {
            get
            {
                if (ViewState["listaAlternativa"] == null)
                    listaAlternativa = AlternativasDA.Select(new AlternativaEntity() { idModelo = miModelo.idModelo });

                return (List<AlternativaEntity>)ViewState["listaAlternativa"];
            }
            set { ViewState["listaAlternativa"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista criterios.
        /// </summary>
        /// <value>
        /// The lista criterios.
        /// </value>
        public List<CriterioEntity> listaCriterio
        {
            get
            {
                if (ViewState["listaCriterios"] == null)
                    listaCriterio = CriteriosDA.Select(new CriterioEntity() { idModelo = miModelo.idModelo });

                return (List<CriterioEntity>)ViewState["listaCriterios"];
            }
            set { ViewState["listaCriterios"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista valores.
        /// </summary>
        /// <value>
        /// The lista valores.
        /// </value>
        public List<RelAlternativaCriterioEntity> listaValores
        {
            get
            {
                if (ViewState["listaValores"] == null)
                    listaValores = ModelosDA.SelectValores(idModelo);

                return (List<RelAlternativaCriterioEntity>)ViewState["listaValores"];
            }
            set { ViewState["listaValores"] = value; }
        }

        /// <summary>
        /// Gets or sets the lista configuracion.
        /// </summary>
        /// <value>
        /// The lista configuracion.
        /// </value>
        public List<Utility.Promethee> listaConfiguracion
        {
            get
            {
                if (ViewState["listaConfiguracion"] == null)
                    listaConfiguracion = ObtenerConfiguracion();

                return (List<Utility.Promethee>)ViewState["listaConfiguracion"];
            }
            set { ViewState["listaConfiguracion"] = value; }
        }
        #endregion

        #region --[Eventos]--
        /// <summary>
        /// Gets or sets the tabla modelo.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <value>
        /// The tabla modelo.
        ///   </value>
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                DefinirColumnas();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblModelo.Text = miModelo.nombre;
                if (!Page.IsPostBack && idModelo > 0)
                {
                    CargarGrilla();

                    CargarGrillaCriterios();
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCerrarPopUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCerrarPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                mpeModelo.Hide();
                mpeError.Hide();
                mpeCriterios.Hide();
                //mpeAlternativas.Hide();
                //mpuUpload.Hide();
                //mpeCriterios.Hide();
                //mpeEliminar.Hide();
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
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            try
            {
                idModelo = 0;
                LimpiarCampos();
                Response.Redirect("Modelos.aspx", false);
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        #region --[Editar Modelo]--
        /// <summary>
        /// Handles the Click event of the lnkModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void lnkModelo_Click(object sender, EventArgs e)
        {
            try
            {
                txtNombre.Text = miModelo.nombre;
                udpModelosAsociados.Update();
                mpeModelo.Show();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnGuardarModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnGuardarModelo_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloEntity nuevoModelo = new ModeloEntity();
                nuevoModelo.idModelo = idModelo;
                nuevoModelo.nombre = txtNombre.Text.Trim();
                lblModelo.Text = nuevoModelo.nombre;
                miModelo.nombre = nuevoModelo.nombre;
                nuevoModelo.fechaCreacion = DateTime.Today;
                nuevoModelo.username = HttpContext.Current.User.Identity.Name;
                ModelosDA.Save(nuevoModelo);

                LimpiarCampos();
                mpeModelo.Hide();
                udpNombre.Update();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        #endregion

        #region --[Grilla Modelo]--
        /// <summary>
        /// Handles the RowEditing event of the gvwModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewEditEventArgs"/> instance containing the event data.</param>
        protected void gvwModelo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvwModelo.EditIndex = e.NewEditIndex;
                CargarGrilla();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the RowUpdating event of the gvwModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void gvwModelo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //PROCESAMIENTO
                // .....

                int idAlternativa = Convert.ToInt32(gvwModelo.DataKeys[e.RowIndex].Value);

                GridViewRow row = gvwModelo.Rows[e.RowIndex];

                if (ValidarFila(idAlternativa, row))
                {
                    GuardarCambios(idAlternativa, row);

                    // .....
                    gvwModelo.EditIndex = -1;
                    CargarGrilla();
                }
                else
                {
                    lblError.Text = "Por favor, verifique los datos ingresados.";
                    udpError.Update();
                    mpeError.Show();
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the RowCancelingEdit event of the gvwModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCancelEditEventArgs"/> instance containing the event data.</param>
        protected void gvwModelo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvwModelo.EditIndex = -1;
                CargarGrilla();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }
        #endregion

        #region --[Grilla Criterios]--
        /// <summary>
        /// Handles the Click event of the btnGuardarCriterio control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnGuardarCriterio_Click(object sender, EventArgs e)
        {
            try
            {
                if (nuevoCriterio.ValidarMétodo())
                {
                    List<CriterioEntity> listaCheck = listaCriterio.FindAll(p =>
                                                        p.nombre == nuevoCriterio.nombreCriterio
                                                        && p.idModelo == idModelo
                                                        && p.idCriterio != idCriterio);

                    if (listaCheck.Count == 0)
                    {
                        GuardarCriterio();

                        LimpiarCampos();
                        CargarGrillaCriterios();

                        mpeCriterios.Hide();
                    }
                    else
                    {
                        nuevoCriterio.error = "Ya existe un Criterio con el mismo nombre.";
                        mpeCriterios.Show();
                    }
                }
                else
                    mpeCriterios.Show();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gvwModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gvwCriterios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LimpiarCampos();
                int idCriterioTest = 0;
                int.TryParse(e.CommandArgument.ToString(), out idCriterioTest);
                switch (e.CommandName)
                {
                    case "editCriterio":
                        idCriterio = idCriterioTest;
                        Utility.Promethee miCriterio = listaConfiguracion.Find(p => p.idCriterio == idCriterio);
                        nuevoCriterio.error = string.Empty;
                        nuevoCriterio.nombreCriterio = miCriterio.nombreCriterio;
                        nuevoCriterio.pesoCriterio = miCriterio.pesoCriterio;
                        nuevoCriterio.esMaximzante = miCriterio.maximiza;
                        nuevoCriterio.TipoFuncionPreferencia = (enumFuncionPreferencia)miCriterio.tipoFuncion;
                        nuevoCriterio.limiteIndiferencia = miCriterio.limiteIndiferencia;
                        nuevoCriterio.limitePreferencia = miCriterio.limitePreferencia;
                        nuevoCriterio.limiteSigma = miCriterio.limiteSigma;
                        mpeCriterios.Show();
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }
        #endregion
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            listaConfiguracion = null;
            listaCriterio = null;
            nuevoCriterio.error = string.Empty;
            txtNombre.Text = string.Empty;
            lblError.Text = string.Empty;
            idCriterio = 0;
        }

        /// <summary>
        /// Gets the nombre funcion.
        /// </summary>
        /// <param name="cadena">The cadena.</param>
        /// <returns></returns>
        protected string GetNombreFuncion(string cadena)
        {
            string nombreFuncion = string.Empty;
            for (int i = 0; i < cadena.Length; i++)
            {
                if (char.IsLower(cadena[i]))
                    nombreFuncion += cadena[i];
                else
                    nombreFuncion += " " + cadena[i];
            }
            return nombreFuncion.Trim();
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        private void CargarGrilla()
        {
            gvwModelo.DataSource = buscarModelo();
            gvwModelo.DataBind();
        }

        /// <summary>
        /// Cargars the grilla criterios.
        /// </summary>
        private void CargarGrillaCriterios()
        {
            gvwCriterios.DataSource = listaConfiguracion;
            gvwCriterios.DataBind();
        }

        /// <summary>
        /// Buscars the alternativas criterios.
        /// </summary>
        private void buscarAlternativasCriterios()
        {
            listaAlternativa = AlternativasDA.Select(new AlternativaEntity() { idModelo = miModelo.idModelo });
            listaCriterio = CriteriosDA.Select(new CriterioEntity() { idModelo = miModelo.idModelo });
            listaValores = ModelosDA.SelectValores(idModelo);
        }

        /// <summary>
        /// Buscars the modelo.
        /// </summary>
        /// <param name="miModelo">The mi modelo.</param>
        /// <returns></returns>
        private DataTable buscarModelo()
        {
            buscarAlternativasCriterios();

            DataTable resultado = new DataTable();

            resultado.Columns.Add("idAlternativa");
            resultado.Columns.Add("nombreAlternativa");
            foreach (CriterioEntity itemCriterio in listaCriterio)
                resultado.Columns.Add(itemCriterio.idCriterio.ToString());

            DataRow nuevaFila;
            List<RelAlternativaCriterioEntity> listaAux = null;
            foreach (AlternativaEntity item in listaAlternativa)
            {
                nuevaFila = resultado.NewRow();
                nuevaFila[0] = item.idAlternativa;
                nuevaFila[1] = item.nombre;

                listaAux = listaValores.FindAll(p => p.idAlternativa == item.idAlternativa);
                foreach (RelAlternativaCriterioEntity itemValor in listaAux)
                    nuevaFila[itemValor.idCriterio.ToString()] = itemValor.valor;

                resultado.Rows.Add(nuevaFila);
            }
            return resultado;
        }

        /// <summary>
        /// Validars the fila.
        /// </summary>
        /// <param name="idAlternativa">The id alternativa.</param>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        private bool ValidarFila(int idAlternativa, GridViewRow row)
        {

            RelAlternativaCriterioEntity entidadGuardar;

            AlternativaEntity alternativaUPD = new AlternativaEntity();
            TextBox txtAlternativa = row.FindControl("txtnombreAlternativa_1") as TextBox;
            alternativaUPD.nombre = txtAlternativa.Text.Trim();
            alternativaUPD.idAlternativa = 0;
            alternativaUPD.idModelo = idModelo;

            List<RelAlternativaCriterioEntity> itemActualizado = listaValores.FindAll(p => p.idAlternativa == idAlternativa);

            if (string.IsNullOrEmpty(alternativaUPD.nombre))
                return false;
            else
            {
                List<AlternativaEntity> listaCheck = listaAlternativa.FindAll(p =>
                                                         p.nombre == alternativaUPD.nombre
                                                         && p.idModelo == alternativaUPD.idModelo
                                                         && p.idAlternativa != idAlternativa);
                if (listaCheck.Count > 0)
                    return false;
                else
                {
                    string nombreTXT = string.Empty;
                    decimal valor = 0;
                    foreach (RelAlternativaCriterioEntity item in itemActualizado)
                    {
                        entidadGuardar = new RelAlternativaCriterioEntity();
                        nombreTXT = string.Format("txt{0}_{1}", item.idCriterio.ToString(), item.idCriterio.ToString());
                        TextBox txtEdicion = row.FindControl(nombreTXT) as TextBox;
                        if (txtEdicion != null)
                        {
                            entidadGuardar.idRelAlternativaCriterio = item.idRelAlternativaCriterio;
                            return decimal.TryParse(txtEdicion.Text, out  valor);
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Guardars the cambios.
        /// </summary>
        /// <param name="idAlternativa">The id alternativa.</param>
        /// <param name="row">The row.</param>
        private void GuardarCambios(int idAlternativa, GridViewRow row)
        {
            List<RelAlternativaCriterioEntity> itemActualizado = listaValores.FindAll(p => p.idAlternativa == idAlternativa);

            List<RelAlternativaCriterioEntity> listaGuardar = new List<RelAlternativaCriterioEntity>();
            RelAlternativaCriterioEntity entidadGuardar;

            AlternativaEntity alternativaUPD = new AlternativaEntity();
            TextBox txtAlternativa = row.FindControl("txtnombreAlternativa_1") as TextBox;
            alternativaUPD.nombre = txtAlternativa.Text.Trim();
            alternativaUPD.idAlternativa = idAlternativa;
            alternativaUPD.idModelo = idModelo;
            string nombreTXT = string.Empty;
            if (itemActualizado.Count > 0)
            {
                foreach (RelAlternativaCriterioEntity item in itemActualizado)
                {
                    entidadGuardar = new RelAlternativaCriterioEntity();
                    nombreTXT = string.Format("txt{0}_{1}", item.idCriterio.ToString(), item.idCriterio.ToString());
                    TextBox txtEdicion = row.FindControl(nombreTXT) as TextBox;
                    if (txtEdicion != null)
                    {
                        entidadGuardar.idRelAlternativaCriterio = item.idRelAlternativaCriterio;
                        entidadGuardar.nombreAlternativa = alternativaUPD.nombre;
                        entidadGuardar.nombreCriterio = item.nombreCriterio;
                        entidadGuardar.valor = Convert.ToDecimal(txtEdicion.Text);
                        listaGuardar.Add(entidadGuardar);
                    }
                }
            }
            else
            {
                foreach (CriterioEntity item in listaCriterio)
                {
                    entidadGuardar = new RelAlternativaCriterioEntity();
                    nombreTXT = string.Format("txt{0}_{1}", item.idCriterio.ToString(), item.idCriterio.ToString());
                    TextBox txtEdicion = row.FindControl(nombreTXT) as TextBox;
                    if (txtEdicion != null)
                    {
                        entidadGuardar.idRelAlternativaCriterio = 0;
                        entidadGuardar.nombreAlternativa = alternativaUPD.nombre;
                        entidadGuardar.nombreCriterio = item.nombre;
                        entidadGuardar.valor = Convert.ToDecimal(txtEdicion.Text);
                        listaGuardar.Add(entidadGuardar);
                    }
                }
            }

            ModelosDA.ActualizarValores(alternativaUPD, listaGuardar);
            listaValores = null;
        }

        /// <summary>
        /// Obteners the configuracion.
        /// </summary>
        /// <returns></returns>
        private List<Utility.Promethee> ObtenerConfiguracion()
        {
            List<Utility.Promethee> listaCriteriosPromethee = new List<Utility.Promethee>();
            Utility.Promethee esteCriterio = null;

            List<ConfigFuncionPreferenciaEntity> listaConfiguracion =
                ConfigFuncionPreferenciaDA.Select(new ConfigFuncionPreferenciaEntity(), new CriterioEntity() { idModelo = miModelo.idModelo });

            List<ConfigFuncionPreferenciaEntity> listaConfiguracionAux = null;
            foreach (CriterioEntity item in listaCriterio)
            {
                listaConfiguracionAux = listaConfiguracion.FindAll(p => p.idCriterio == item.idCriterio);
                esteCriterio = new Utility.Promethee();
                esteCriterio.idCriterio = item.idCriterio;
                esteCriterio.pesoCriterio = item.pesoDefault;
                esteCriterio.nombreCriterio = item.nombre;
                esteCriterio.maximiza = item.maximiza;

                foreach (ConfigFuncionPreferenciaEntity itemConfig in listaConfiguracionAux)
                {
                    switch (itemConfig.idFuncionPreferencia)
                    {
                        case 1:
                            esteCriterio.tipoFuncion = enumFuncionPreferencia.VerdaderoCriterio;
                            break;
                        case 2:
                            esteCriterio.limiteIndiferencia = itemConfig.valorDefault;
                            esteCriterio.tipoFuncion = enumFuncionPreferencia.CuasiCriterio;
                            break;
                        case 3:
                            esteCriterio.limitePreferencia = itemConfig.valorDefault;
                            esteCriterio.tipoFuncion = enumFuncionPreferencia.PseudoCriterioConPreferenciaLineal;
                            break;
                        case 4:
                            esteCriterio.tipoFuncion = enumFuncionPreferencia.LevelCriterio;
                            switch (itemConfig.idValorFuncionPreferencia)
                            {
                                case (int)enumValorFuncionPreferencia.LimiteIndiferencia:
                                    esteCriterio.limiteIndiferencia = itemConfig.valorDefault;
                                    break;
                                case (int)enumValorFuncionPreferencia.LimitePreferencia:
                                    esteCriterio.limitePreferencia = itemConfig.valorDefault;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 5:
                            esteCriterio.tipoFuncion = enumFuncionPreferencia.CriterioConPreferenciaLinealYAreaDeIndiferencia;
                            switch (itemConfig.idValorFuncionPreferencia)
                            {
                                case (int)enumValorFuncionPreferencia.LimiteIndiferencia:
                                    esteCriterio.limiteIndiferencia = itemConfig.valorDefault;
                                    break;
                                case (int)enumValorFuncionPreferencia.LimitePreferencia:
                                    esteCriterio.limitePreferencia = itemConfig.valorDefault;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 6:
                            esteCriterio.tipoFuncion = enumFuncionPreferencia.CriterioGaussiano;
                            esteCriterio.limiteSigma = itemConfig.valorDefault;
                            break;
                        default:
                            break;
                    }
                }
                listaCriteriosPromethee.Add(esteCriterio);
            }
            return listaCriteriosPromethee;
        }

        /// <summary>
        /// Guardars the criterio.
        /// </summary>
        private void GuardarCriterio()
        {
            CriterioEntity nuevaEntidad = new CriterioEntity();
            nuevaEntidad.idModelo = idModelo;
            nuevaEntidad.idCriterio = idCriterio;
            nuevaEntidad.nombre = nuevoCriterio.nombreCriterio;
            nuevaEntidad.pesoDefault = nuevoCriterio.pesoCriterio;
            nuevaEntidad.maximiza = nuevoCriterio.esMaximzante;

            Promethee.Utility.Promethee datos = nuevoCriterio.obtenerValores();

            ValorFuncionPreferenciaEntity miValor = new ValorFuncionPreferenciaEntity();
            List<ConfigFuncionPreferenciaEntity> listaConfig = new List<ConfigFuncionPreferenciaEntity>();

            ConfigFuncionPreferenciaEntity miConfiguracion = new ConfigFuncionPreferenciaEntity();
            miConfiguracion.idCriterio = nuevaEntidad.idCriterio;
            miConfiguracion.idFuncionPreferencia = datos.tipoFuncion.GetHashCode();

            switch (datos.tipoFuncion)
            {
                case enumFuncionPreferencia.None:
                    break;
                case enumFuncionPreferencia.VerdaderoCriterio:
                    miConfiguracion.idValorFuncionPreferencia = enumValorFuncionPreferencia.Nulo.GetHashCode();
                    miConfiguracion.valorDefault = 0;
                    listaConfig.Add(miConfiguracion);
                    break;
                case enumFuncionPreferencia.CuasiCriterio:
                    miConfiguracion.idValorFuncionPreferencia = enumValorFuncionPreferencia.LimiteIndiferencia.GetHashCode();
                    miConfiguracion.valorDefault = datos.limiteIndiferencia;
                    listaConfig.Add(miConfiguracion);
                    break;
                case enumFuncionPreferencia.PseudoCriterioConPreferenciaLineal:
                    miConfiguracion.idValorFuncionPreferencia = enumValorFuncionPreferencia.LimitePreferencia.GetHashCode();
                    miConfiguracion.valorDefault = datos.limitePreferencia;
                    listaConfig.Add(miConfiguracion);
                    break;
                case enumFuncionPreferencia.LevelCriterio:
                case enumFuncionPreferencia.CriterioConPreferenciaLinealYAreaDeIndiferencia:
                    miConfiguracion.idValorFuncionPreferencia = enumValorFuncionPreferencia.LimitePreferencia.GetHashCode();
                    miConfiguracion.valorDefault = datos.limitePreferencia;
                    listaConfig.Add(miConfiguracion);

                    ConfigFuncionPreferenciaEntity otraConfiguracion = new ConfigFuncionPreferenciaEntity();
                    otraConfiguracion.idCriterio = nuevaEntidad.idCriterio;
                    otraConfiguracion.idValorFuncionPreferencia = enumValorFuncionPreferencia.LimiteIndiferencia.GetHashCode();
                    otraConfiguracion.valorDefault = datos.limiteIndiferencia;
                    otraConfiguracion.idFuncionPreferencia = datos.tipoFuncion.GetHashCode();

                    listaConfig.Add(otraConfiguracion);
                    break;
                case enumFuncionPreferencia.CriterioGaussiano:
                    miConfiguracion.idValorFuncionPreferencia = enumValorFuncionPreferencia.Sigma.GetHashCode();
                    miConfiguracion.valorDefault = datos.limiteSigma;
                    listaConfig.Add(miConfiguracion);
                    break;
                default:
                    break;
            }
            CriteriosDA.Save(nuevaEntidad, listaConfig);
        }

        #region --[Crear Grilla]--
        private void DefinirColumnas()
        {
            //
            // Se define el campo dentro de la grilla, 
            // para poder identificar cada item 
            //
            gvwModelo.DataKeyNames = new string[] { "idAlternativa" };
            gvwModelo.Columns.Clear();

            //
            // Columna Alternativas
            //
            TemplateField tempDesc = new TemplateField();
            tempDesc.HeaderTemplate = new GridViewHeaderTemplate("Alternativa", 0);
            tempDesc.ItemTemplate = new GridViewItemTemplate("nombreAlternativa", 1);
            tempDesc.EditItemTemplate = new GridViewEditTemplate("nombreAlternativa", 1);
            gvwModelo.Columns.Add(tempDesc);

            foreach (CriterioEntity item in listaCriterio)
            {
                //
                // Columna item.Nombre
                //
                TemplateField tempColumn = new TemplateField();
                tempColumn.HeaderTemplate = new GridViewHeaderTemplate(item.nombre, item.idCriterio);
                tempColumn.ItemTemplate = new GridViewItemTemplate(item.idCriterio.ToString(), item.idCriterio);
                tempColumn.EditItemTemplate = new GridViewEditTemplate(item.idCriterio.ToString(), item.idCriterio);
                gvwModelo.Columns.Add(tempColumn);
            }
        }
        #endregion
        #endregion
    }

    #region --[Clases Manejo Grilla]
    public class GridViewHeaderTemplate : ITemplate
    {
        string text;
        int idCriterio;
        public GridViewHeaderTemplate(string text, int idCriterio)
        {
            this.text = text;
            this.idCriterio = idCriterio;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();
            lc.Text = text;

            container.Controls.Add(lc);
        }
    }

    public class GridViewEditTemplate : ITemplate
    {
        private string columnName;
        private int idRelAlternativaCriterio;

        public GridViewEditTemplate(string columnName, int idRelAlternativaCriterio)
        {
            this.columnName = columnName;
            this.idRelAlternativaCriterio = idRelAlternativaCriterio;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            TextBox tb = new TextBox();
            tb.ID = string.Format("txt{0}_{1}", columnName, idRelAlternativaCriterio.ToString());
            tb.EnableViewState = false;
            tb.DataBinding += new EventHandler(tb_DataBinding);

            container.Controls.Add(tb);
        }

        void tb_DataBinding(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;

            GridViewRow row = (GridViewRow)t.NamingContainer;

            string RawValue = DataBinder.Eval(row.DataItem, columnName).ToString();

            t.Text = RawValue;
        }
    }

    public class GridViewItemTemplate : ITemplate
    {
        private string columnName;
        private int idRelAlternativaCriterio;

        public GridViewItemTemplate(string columnName, int idRelAlternativaCriterio)
        {
            this.columnName = columnName;
            this.idRelAlternativaCriterio = idRelAlternativaCriterio;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();

            lc.DataBinding += new EventHandler(lc_DataBinding);

            container.Controls.Add(lc);

        }

        void lc_DataBinding(object sender, EventArgs e)
        {
            Literal l = (Literal)sender;

            GridViewRow row = (GridViewRow)l.NamingContainer;

            string RawValue = DataBinder.Eval(row.DataItem, columnName).ToString();

            l.Text = RawValue;
        }
    }
    #endregion
}