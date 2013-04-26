using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Entity;
using DataAccess;
using System.Data;

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
        /// Gets or sets the tabla modelo.
        /// </summary>
        /// <value>
        /// The tabla modelo.
        /// </value>
        //public DataTable tablaModelo
        //{
        //    get
        //    {
        //        if (ViewState["tablaModelo"] == null)
        //            tablaModelo = new DataTable();

        //        return (DataTable)ViewState["tablaModelo"];
        //    }
        //    set { ViewState["tablaModelo"] = value; }
        //}
        #endregion

        #region --[Eventos]--
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
                if (!Page.IsPostBack)
                {
                    CargarGrilla();
                }
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
        #endregion

        #region --[Grilla]--
        /// <summary>
        /// Handles the RowCommand event of the gvwModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gvwModelo_RowCommand(object sender, GridViewCommandEventArgs e)
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
        /// Handles the RowDataBound event of the gvwModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gvwModelo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

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
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            lblError.Text = string.Empty;
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        private void CargarGrilla()
        {
            //tablaModelo = buscarModelo();
            //gvwModelo.DataSource = listaValores;
            gvwModelo.DataSource = buscarModelo();
            gvwModelo.DataBind();
            //udpGrilla.Update();
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
            foreach (RelAlternativaCriterioEntity item in itemActualizado)
            {
                entidadGuardar = new RelAlternativaCriterioEntity();
                nombreTXT = string.Format("txt{0}_{1}", item.idCriterio.ToString(), item.idCriterio.ToString());
                TextBox txtEdicion = row.FindControl(nombreTXT) as TextBox;
                if (txtEdicion != null)
                {
                    entidadGuardar.idRelAlternativaCriterio = item.idRelAlternativaCriterio;
                    entidadGuardar.valor = Convert.ToDecimal(txtEdicion.Text);
                    listaGuardar.Add(entidadGuardar);
                }
            }

            ModelosDA.ActualizarValores(alternativaUPD, listaGuardar);
            listaValores = null;
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
            tempDesc.HeaderTemplate = new GridViewHeaderTemplate("Alternativa");
            tempDesc.ItemTemplate = new GridViewItemTemplate("nombreAlternativa", 1);
            tempDesc.EditItemTemplate = new GridViewEditTemplate("nombreAlternativa", 1);
            gvwModelo.Columns.Add(tempDesc);

            foreach (CriterioEntity item in listaCriterio)
            {
                //
                // Columna item.Nombre
                //
                TemplateField tempColumn = new TemplateField();
                tempColumn.HeaderTemplate = new GridViewHeaderTemplate(item.nombre);
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

        public GridViewHeaderTemplate(string text)
        {
            this.text = text;
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