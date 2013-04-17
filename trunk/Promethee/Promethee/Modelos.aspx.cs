using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using DataAccess.Entity;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Promethee.Scripts;
using Promethee.Utility;

namespace Promethee
{
    public partial class Modelos : System.Web.UI.Page
    {
        #region --[Atributos]--
        /// <summary>
        /// The excel file
        /// </summary>
        HSSFWorkbook excelFile;
        //Variables para calcular el tamaño de cada columna
        Single sngMayorValor, sngMayor1, sngMayor2, sngMayor3;
        Single valor1, valor2, valor3;
        string Alternativa1, Alternativa2, Alternativa3;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the modelo entity.
        /// </summary>
        /// <value>
        /// The modelo entity.
        /// </value>
        public List<ModeloEntity> listaModelos
        {
            get
            {
                if (ViewState["listaModelos"] == null)
                    listaModelos = new List<ModeloEntity>();

                return (List<ModeloEntity>)ViewState["listaModelos"];
            }
            set { ViewState["listaModelos"] = value; }
        }

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
                if (ViewState["idModelo"] == null)
                    idModelo = 0;

                return (int)ViewState["idModelo"];
            }
            set { ViewState["idModelo"] = value; }
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
                if (ViewState["miModelo"] == null)
                    miModelo = new ModeloEntity();

                return (ModeloEntity)ViewState["miModelo"];
            }
            set { ViewState["miModelo"] = value; }
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
                    listaAlternativa = new List<AlternativaEntity>();

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
                    listaCriterio = new List<CriterioEntity>();

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
                    listaValores = new List<RelAlternativaCriterioEntity>();

                return (List<RelAlternativaCriterioEntity>)ViewState["listaValores"];
            }
            set { ViewState["listaValores"] = value; }
        }

        public string NombrePNG
        {
            get
            {
                if (ViewState["NombrePNG"] == null)
                    ViewState["NombrePNG"] = string.Empty;
                return ViewState["NombrePNG"].ToString();
            }
            set
            {
                ViewState["NombrePNG"] = value;
            }
        }

        #region --[Tablas]--
        public DataTable tablaResultado
        {
            get
            {
                if (Session["tablaResultado"] == null)
                    Session["tablaResultado"] = new DataTable();
                return (DataTable)Session["tablaResultado"];
            }
            set
            {
                Session["tablaResultado"] = value;
            }
        }

        /// <summary>
        /// Contiene los datos del EXCEL
        /// </summary>
        /// <value>
        /// The tabla paso0.
        /// </value>
        public DataTable tablaPaso0
        {
            get
            {
                if (Session["tablaPaso0"] == null)
                    Session["tablaPaso0"] = new DataTable();
                return (DataTable)Session["tablaPaso0"];
            }
            set
            {
                Session["tablaPaso0"] = value;
            }
        }

        public DataTable tablaPaso1
        {
            get
            {
                if (Session["tablaPaso1"] == null)
                    Session["tablaPaso1"] = new DataTable();
                return (DataTable)Session["tablaPaso1"];
            }
            set
            {
                Session["tablaPaso1"] = value;
            }
        }

        public DataTable tablaPaso2
        {
            get
            {
                if (Session["tablaPaso2"] == null)
                    Session["tablaPaso2"] = new DataTable();
                return (DataTable)Session["tablaPaso2"];
            }
            set
            {
                Session["tablaPaso2"] = value;
            }
        }

        public DataTable tablaPaso3
        {
            get
            {
                if (Session["tablaPaso3"] == null)
                    Session["tablaPaso3"] = new DataTable();
                return (DataTable)Session["tablaPaso3"];
            }
            set
            {
                Session["tablaPaso3"] = value;
            }
        }
        #endregion
        #endregion

        #region --[Eventos]--
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Label1.Text = string.Format("Bienvenido al Sistema {0}", Thread.CurrentPrincipal.Identity.Name.ToString().ToUpper());
                    CargarGrilla();
                }
                //CargarGrilla();
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
                //btnGuardarModelo.Visible = true;
                mpeModelo.Show();
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
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            miModelo = (ModeloEntity)e.Row.DataItem;

            GridView gvDetails = (GridView)e.Row.FindControl("gvwDetalle");

            gvDetails.DataSource = buscarModelo().DefaultView;
            gvDetails.DataBind();
        }

        /// <summary>
        /// Método que se llama al hacer click sobre las acciones de la grilla
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gvwModelo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LimpiarCampos();
                int idModeloEdit = 0;
                int.TryParse(e.CommandArgument.ToString(), out idModeloEdit);
                miModelo = listaModelos.Find(p => p.idModelo == idModeloEdit);
                if (miModelo != null)
                    idModelo = idModeloEdit;
                switch (e.CommandName)
                {
                    case "nuevoModelo":
                        mpeModelo.Show();
                        break;
                    case "editModelo":
                        txtNombre.Text = miModelo.nombre;
                        udpModelos.Update();
                        mpeModelo.Show();
                        break;
                    case "addAlternativa":
                        mpeAlternativas.Show();
                        break;
                    case "addCriterio":
                        mpeCriterios.Show();
                        break;
                    case "download":
                        CrearPlantilla();
                        Response.Clear();

                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", miModelo.nombre.Replace(" ", "") + ".xls"));
                        Response.BinaryWrite(WriteToStream().GetBuffer());
                        //Response.Flush();
                        Response.End();
                        //CargarGrilla();
                        udpModelos.Update();
                        break;
                    case "upload":
                        mpuUpload.Show();
                        break;
                    case "solve":
                        //imgPodio.ImageUrl = string.Empty;
                        //imgPodio.Visible = false;
                        //udpImgPodio.Update();
                        ResolverModelo();
                        //udpModelos.Update();
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the OnClick event of the btnDownload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnDownload_OnClick(object sender, EventArgs e)
        {
            try
            {
                int miModeloID = 0;
                int.TryParse(((ImageButton)sender).CommandArgument, out miModeloID);
                miModelo = listaModelos.Find(p => p.idModelo == miModeloID);

                CrearPlantilla();
                Response.Clear();

                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", miModelo.nombre.Replace(" ", "") + ".xls"));
                Response.BinaryWrite(WriteToStream().GetBuffer());
                Response.Flush();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
            finally
            {
                Response.End();
                //udpGrilla.Update();
            }
        }

        /// <summary>
        /// Handles the OnClick event of the btnUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            try
            {
                string FileExtension = Path.GetExtension(fuCargarArchivo.PostedFile.FileName).Substring(1);
                //string FileExtension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName).Substring(1);

                string FileName = Guid.NewGuid().ToString() + "." + FileExtension;

                fuCargarArchivo.SaveAs(MapPath("~/Files/" + FileName));
                //AsyncFileUpload1.SaveAs(MapPath("~/Files/" + FileName));

                ModelosDA.SaveFile(idModelo, FileName);

                GuardarValores(FileName);

                mpuUpload.Hide();

                //CargarGrilla();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the gvwModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvwModelo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvwModelo.PageIndex = e.NewPageIndex;
                CargarGrilla();
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
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnGuardarModelo_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloEntity nuevoModelo = new ModeloEntity();
                nuevoModelo.idModelo = idModelo;
                nuevoModelo.nombre = txtNombre.Text.Trim();
                nuevoModelo.fechaCreacion = DateTime.Today;
                nuevoModelo.username = HttpContext.Current.User.Identity.Name;
                ModelosDA.Save(nuevoModelo);

                mpeModelo.Hide();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnGuardarAlternativa control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnGuardarAlternativa_Click(object sender, EventArgs e)
        {
            try
            {
                AlternativaEntity test = new AlternativaEntity();
                test.idModelo = miModelo.idModelo;
                test.nombre = txtAlternativa.Text.Trim();
                List<AlternativaEntity> listaAlternativa = AlternativasDA.Select(test);

                if (listaAlternativa.Count == 0)
                {
                    GuardarAlternativa();

                    LimpiarCampos();
                    mpeAlternativas.Hide();

                    CargarGrilla();
                }
                else
                {
                    lblErrorAlternativa.Text = "Ya existe una Alternativa con el mismo nombre.";
                    mpeAlternativas.Show();
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

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
                    CriterioEntity test = new CriterioEntity();
                    test.idModelo = idModelo;
                    test.nombre = nuevoCriterio.nombreCriterio;
                    List<CriterioEntity> listaCriterios = CriteriosDA.Select(test);

                    if (listaCriterios.Count == 0)
                    {
                        GuardarCriterio();

                        CargarGrilla();

                        LimpiarCampos();
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
        /// Handles the Click event of the btnVolverModelo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnCerrarPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                mpeModelo.Hide();
                mpeAlternativas.Hide();
                mpuUpload.Hide();
                mpeCriterios.Hide();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void gvwResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvwResultado.PageIndex = e.NewPageIndex;
                CargarGrillaResultado();
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
            listaModelos = ModelosDA.Select(new UsuarioEntity() { username = HttpContext.Current.User.Identity.Name });
            gvwModelo.DataSource = listaModelos;
            gvwModelo.DataBind();
        }

        /// <summary>
        /// Limpiars the campos.
        /// </summary>
        private void LimpiarCampos()
        {
            idModelo = 0;
            txtAlternativa.Text = string.Empty;
            txtNombre.Text = string.Empty;
            nuevoCriterio.nombreCriterio = string.Empty;
            nuevoCriterio.pesoCriterio = 0;
            nuevoCriterio.LimpiarControles();
            lblErrorAlternativa.Text = string.Empty;
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

            resultado.Columns.Add("Alternativa\\Criterio");
            foreach (CriterioEntity itemCriterio in listaCriterio)
                resultado.Columns.Add(itemCriterio.nombre);

            DataRow nuevaFila;
            List<RelAlternativaCriterioEntity> listaAux = null;
            foreach (AlternativaEntity item in listaAlternativa)
            {
                nuevaFila = resultado.NewRow();
                nuevaFila[0] = item.nombre;

                listaAux = listaValores.FindAll(p => p.idAlternativa == item.idAlternativa);
                foreach (RelAlternativaCriterioEntity itemValor in listaAux)
                    nuevaFila[itemValor.nombreCriterio] = itemValor.valor;

                resultado.Rows.Add(nuevaFila);
            }
            return resultado;
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
        /// Guardars the alternativa.
        /// </summary>
        private void GuardarAlternativa()
        {
            AlternativaEntity nuevaAlternativa = new AlternativaEntity();
            nuevaAlternativa.idModelo = miModelo.idModelo;
            nuevaAlternativa.nombre = txtAlternativa.Text.Trim();
            AlternativasDA.Save(nuevaAlternativa);
        }

        /// <summary>
        /// Guardars the criterio.
        /// </summary>
        private void GuardarCriterio()
        {
            CriterioEntity nuevaEntidad = new CriterioEntity();
            nuevaEntidad.idModelo = idModelo;
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

        /// <summary>
        /// Guardars the valores.
        /// </summary>
        private void GuardarValores(string FileName)
        {
            InitializeWorkbook(MapPath("~/Files/" + FileName));

            tablaPaso0 = ConvertToDataTable();

            if (tablaPaso0.Rows.Count > 0)
            {
                List<RelAlternativaCriterioEntity> listaValores = new List<RelAlternativaCriterioEntity>();
                RelAlternativaCriterioEntity nuevoValor = null;
                decimal valor = 0;
                foreach (DataRow item in tablaPaso0.Rows)
                {
                    for (int i = 1; i < tablaPaso0.Columns.Count; i++)
                    {
                        nuevoValor = new RelAlternativaCriterioEntity();
                        nuevoValor.nombreAlternativa = item[0].ToString();
                        nuevoValor.nombreCriterio = tablaPaso0.Columns[i].ColumnName;
                        decimal.TryParse(item[i].ToString(), out valor);
                        nuevoValor.valor = valor;
                        listaValores.Add(nuevoValor);
                    }
                }
                ModelosDA.SaveValores(listaValores, miModelo.idModelo);
            }
            File.Delete(MapPath("~/Files/" + miModelo.filename));
        }

        /// <summary>
        /// Descargars the plantilla.
        /// </summary>
        private void DescargarPlantilla()
        {
            CrearPlantilla();
            Response.Clear();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", miModelo.nombre.Replace(" ", "") + ".xls"));
            Response.BinaryWrite(WriteToStream().GetBuffer());

            Response.End();
        }

        /// <summary>
        /// Crears the plantilla.
        /// </summary>
        private void CrearPlantilla()
        {
            string[] strCriterios = new string[miModelo.criterios];
            string[] strAlternativas = new string[miModelo.alternativas];

            DataTable modelo = buscarModelo();

            for (int i = 1; i < modelo.Columns.Count; i++)
                strCriterios[i - 1] = modelo.Columns[i].ColumnName;
            for (int i = 0; i < modelo.Rows.Count; i++)
                strAlternativas[i] = modelo.Rows[i][0].ToString();

            InitializeWorkbook();

            GenerarPlantilla(strCriterios, strAlternativas);
        }

        #region --[Generación de Excel]--
        /// <summary>
        /// Initializes the workbook.
        /// </summary>
        void InitializeWorkbook()
        {
            excelFile = new HSSFWorkbook();

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Promethee";
            excelFile.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Archivo generado medinte la librería NPOI";
            excelFile.SummaryInformation = si;
        }

        /// <summary>
        /// Writes to stream.
        /// </summary>
        /// <returns></returns>
        MemoryStream WriteToStream()
        {
            //Write the stream data of workbook to the root directory
            MemoryStream file = new MemoryStream();
            excelFile.Write(file);
            return file;
        }

        /// <summary>
        /// Generates the data.
        /// </summary>
        /// <param name="strCriterios">The STR criterios.</param>
        /// <param name="strAlternativas">The STR alternativas.</param>
        private void GenerarPlantilla(string[] strCriterios, string[] strAlternativas)
        {
            #region --[Estilos]--
            IFont fuenteTitulo = excelFile.CreateFont();
            fuenteTitulo.FontName = "Calibri";
            fuenteTitulo.Boldweight = (short)FontBoldWeight.BOLD.GetHashCode();

            IFont unaFuente = excelFile.CreateFont();
            unaFuente.FontName = "Tahoma";

            IFont fuenteEncabezado = excelFile.CreateFont();
            fuenteEncabezado.FontName = "Tahoma";
            fuenteEncabezado.Boldweight = (short)FontBoldWeight.BOLD.GetHashCode();

            ICellStyle unEstiloDecimal = excelFile.CreateCellStyle();
            IDataFormat format = excelFile.CreateDataFormat();
            unEstiloDecimal.DataFormat = format.GetFormat("0.00");
            unEstiloDecimal.SetFont(unaFuente);

            ICellStyle estiloBloqueada = excelFile.CreateCellStyle();
            estiloBloqueada.IsLocked = true;
            estiloBloqueada.SetFont(fuenteEncabezado);
            #endregion

            NPOI.SS.Util.CellRangeAddress rango = new NPOI.SS.Util.CellRangeAddress(0, 0, 1, strCriterios.Length);

            #region --[Hoja Datos]--
            ISheet hojaUno = excelFile.CreateSheet("Cargar Datos");

            hojaUno.AddMergedRegion(rango);
            hojaUno.ProtectSheet("laura");

            IRow filaEncabezado = hojaUno.CreateRow(0);
            int auxNumRow = 0;
            filaEncabezado.CreateCell(1).SetCellValue("Criterio");
            filaEncabezado.Cells[0].CellStyle.SetFont(fuenteTitulo);
            filaEncabezado.Cells[0].CellStyle.Alignment = HorizontalAlignment.CENTER;

            filaEncabezado = hojaUno.CreateRow(1);
            auxNumRow++;
            filaEncabezado.CreateCell(0).SetCellValue("Alternativas");
            filaEncabezado.Cells[0].CellStyle.SetFont(fuenteTitulo);
            filaEncabezado.Cells[0].CellStyle.Alignment = HorizontalAlignment.CENTER;

            for (int i = 0; i < strCriterios.Length; i++)
            {
                filaEncabezado.CreateCell(i + 1).SetCellValue(strCriterios[i]);
                filaEncabezado.Cells[i + 1].CellStyle.SetFont(fuenteTitulo);
            }

            HSSFPatriarch patr = (HSSFPatriarch)hojaUno.CreateDrawingPatriarch();
            IComment comment = patr.CreateCellComment(new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 4, 4));
            comment.String = new HSSFRichTextString("Completa los datos del modelo, luego deberas cargar el archivo para obtener los resultados. Gracias");
            comment.Author = "Promethee";
            filaEncabezado.CreateCell(strCriterios.Length + 1).CellComment = comment;

            auxNumRow++;
            for (int i = 0; i < strAlternativas.Length; i++, auxNumRow++)
            {
                filaEncabezado = hojaUno.CreateRow(auxNumRow);
                filaEncabezado.CreateCell(0).SetCellValue(strAlternativas[i]);
                filaEncabezado.Cells[0].CellStyle.SetFont(unaFuente);

                for (int j = 1; j < strCriterios.Length + 1; j++)
                {
                    filaEncabezado.CreateCell(j).SetCellType(CellType.NUMERIC);
                    filaEncabezado.Cells[j].CellStyle = unEstiloDecimal;
                    filaEncabezado.Cells[j].CellStyle.IsLocked = false;
                }
            }

            for (int i = 0; i <= strCriterios.Length; i++)
                hojaUno.AutoSizeColumn(i);

            #endregion

            ISheet hojaDos = excelFile.CreateSheet("Datos Protegidos");

            filaEncabezado = hojaDos.CreateRow(0);
            filaEncabezado.CreateCell(0).SetCellValue("idModelo");
            filaEncabezado.Cells[0].CellStyle = estiloBloqueada;

            filaEncabezado.CreateCell(1).SetCellValue(miModelo.idModelo);
            filaEncabezado.Cells[1].CellStyle = estiloBloqueada;

            filaEncabezado = hojaDos.CreateRow(1);
            filaEncabezado.CreateCell(0).SetCellValue("Total Criterios");
            filaEncabezado.Cells[0].CellStyle = estiloBloqueada;

            filaEncabezado.CreateCell(1).SetCellValue(strCriterios.Length);
            filaEncabezado.Cells[1].CellStyle = estiloBloqueada;

            filaEncabezado = hojaDos.CreateRow(2);
            filaEncabezado.CreateCell(0).SetCellValue("Total Alternativas");
            filaEncabezado.Cells[0].CellStyle = estiloBloqueada;

            filaEncabezado.CreateCell(1).SetCellValue(strAlternativas.Length);
            filaEncabezado.Cells[1].CellStyle = estiloBloqueada;

            excelFile.SetSheetHidden(1, SheetState.HIDDEN);
        }

        #endregion

        #region --[Resolver]--
        /// <summary>
        /// Resolvers the modelo.
        /// </summary>
        private void ResolverModelo()
        {
            buscarAlternativasCriterios();
            List<Utility.Promethee> listaConfiguracion = new List<Utility.Promethee>();
            listaConfiguracion = ObtenerConfiguracion();

            CargarTablaPaso0();

            EjecutarPaso1(listaConfiguracion);

            EjecutarPaso2(listaConfiguracion);

            EjecutarPaso3();

            EjecutarPaso4();

            PresentarResultado();

            tablaPaso0 = new DataTable();
            tablaPaso1 = new DataTable();
            tablaPaso2 = new DataTable();
            tablaPaso3 = new DataTable();
            tablaResultado = new DataTable();
        }

        /// <summary>
        /// Presentars the resultado.
        /// </summary>
        private void PresentarResultado()
        {
            AlternativaEntity aux = null;
            for (int i = 0; i < tablaResultado.Rows.Count; i++)
            {
                aux = listaAlternativa.Find(p => p.idAlternativa == Convert.ToInt16(tablaResultado.Rows[i][0].ToString()));
                tablaResultado.Rows[i][0] = aux.nombre;
            }
            foreach (AlternativaEntity item in listaAlternativa)
                tablaResultado.Columns[item.idAlternativa.ToString()].ColumnName = item.nombre;
            
            tablaResultado.Columns.Remove("FlujoEntrante");

            //CargarGrilla();

            tablaPaso3.Columns.Add("Ranking", System.Type.GetType("System.Decimal"));
            tablaPaso3.Rows[0][tablaPaso3.Columns.Count - 1] = DBNull.Value;

            GraficarPodioResultado();
        }

        /// <summary>
        /// Graficars the podio resultado.
        /// </summary>
        private void GraficarPodioResultado()
        {
            #region --[Top 3 Alternativas]--
            int cantidad = listaAlternativa.Count;
            int iterador = cantidad < 3 ? cantidad : 3;
            for (int i = 0; i < iterador; i++)
            {
                var TopAlumno = from p in listaAlternativa
                                where p.nombre == tablaResultado.Rows[i][0].ToString()
                                select p.nombre;
                switch (i)
                {
                    case 0:
                        valor1 = Convert.ToSingle(100);
                        Alternativa1 = TopAlumno.ElementAt(0).ToString();
                        break;
                    case 1:
                        valor2 = Convert.ToSingle(75);
                        Alternativa2 = TopAlumno.ElementAt(0).ToString();
                        break;
                    case 2:
                        valor3 = Convert.ToSingle(50);
                        Alternativa3 = TopAlumno.ElementAt(0).ToString();
                        break;
                    default:
                        break;
                }
            }
            #endregion

            //Dreclaramos el objeto BitMap y Graphic
            Bitmap objBitmap = new Bitmap(340, 250);

            Graphics objGraphic = Graphics.FromImage(objBitmap);

            //Declaramos las barras asignándoles un color
            SolidBrush TurquoiseBrush = new SolidBrush(Color.MediumAquamarine);
            SolidBrush VioletBrush = new SolidBrush(Color.SteelBlue);
            SolidBrush SalmonBrush = new SolidBrush(Color.SeaGreen);

            //Definimos el fondo de color blanco
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            //Aquí es donde creamos el fondo, de color
            //blanco tal y como especificamos anteriormente
            objGraphic.FillRectangle(whiteBrush, 0, 0, 340, 250);

            //Comprobamos cual es la más grande, que tendrá un tamaño
            //del 100%, y las otras 2 serán más pequeñas en proporción
            //a la diferencia de tamaño con respecto a la mayor.
            if (valor1 > valor2)
                sngMayorValor = valor1;
            else
                sngMayorValor = valor2;

            if (valor3 > sngMayorValor)
                sngMayorValor = valor3;

            if (sngMayorValor == 0)
                sngMayorValor = 1;

            sngMayor1 = (valor1 / sngMayorValor) * 190;
            sngMayor2 = (valor2 / sngMayorValor) * 190;
            sngMayor3 = (valor3 / sngMayorValor) * 190;

            //Con todos los cálculos realizado, creamos ahora sí
            //las columnas de la imagen 
            objGraphic.FillRectangle(TurquoiseBrush, 10, 244 - sngMayor2, 100, sngMayor2);
            objGraphic.FillRectangle(VioletBrush, 120, 244 - sngMayor1, 100, sngMayor1);
            objGraphic.FillRectangle(SalmonBrush, 230, 244 - sngMayor3, 100, sngMayor3);

            // Create font and brush.
            Font drawFont = new Font("Tahoma", 10);
            SolidBrush drawBrush = new SolidBrush(Color.DimGray);
            SolidBrush drawBrushNegro = new SolidBrush(Color.Black);
            Font drawFontTitulo = new Font("Tahoma", 14);

            // Create rectangle for drawing.
            RectangleF drawRect1 = new RectangleF(10, 244 - sngMayor2, 100, sngMayor2);
            RectangleF drawRect2 = new RectangleF(120, 244 - sngMayor1, 100, sngMayor1);
            RectangleF drawRect3 = new RectangleF(230, 244 - sngMayor3, 100, sngMayor3);
            RectangleF drawRectTitulo = new RectangleF(10, 10, 300, 75);

            // Draw rectangle to screen.
            Pen blackPen = new Pen(Color.Transparent);
            objGraphic.DrawRectangle(blackPen, 10, 194 - sngMayor2, 100, sngMayor2);
            objGraphic.DrawRectangle(blackPen, 120, 194 - sngMayor1, 100, sngMayor1);
            objGraphic.DrawRectangle(blackPen, 230, 194 - sngMayor3, 100, sngMayor3);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            // Draw string to screen.
            objGraphic.DrawString(Alternativa1, drawFont, drawBrushNegro, drawRect2, drawFormat);
            objGraphic.DrawString(Alternativa2, drawFont, drawBrushNegro, drawRect1, drawFormat);
            objGraphic.DrawString(Alternativa3, drawFont, drawBrushNegro, drawRect3, drawFormat);
            objGraphic.DrawString("Resultados - " + miModelo.nombre, drawFontTitulo, drawBrush, drawRectTitulo, drawFormat);

            //Definimos el tipo de fichero
            //Response.ContentType = "image/png";

            string TmpPath = System.Configuration.ConfigurationManager.AppSettings["oImgPath"];
            UIUtility.EliminarArchivosSession(Session.SessionID);
            //Crea el directorio.
            if (!System.IO.Directory.Exists(TmpPath))
                System.IO.Directory.CreateDirectory(TmpPath);

            NombrePNG = TmpPath + "\\Podio_" + Session.SessionID + ".png";
            string ruta = Request.PhysicalApplicationPath + "Images\\TMP\\Podio_" + Session.SessionID + ".png";
            //Y finalmente lo guardamos
            objBitmap.Save(NombrePNG, ImageFormat.Png);

            File.Copy(NombrePNG, ruta);
            objBitmap.Dispose();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            imgPodio.ImageUrl = "http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath + "/Images/TMP/Podio_" + Session.SessionID + ".png";
            imgPodio.Visible = true;
            udpImgPodio.Update();
            divResultado.Visible = true;
            CargarGrillaResultado();
        }

        /// <summary>
        /// Cargars the grilla resultado.
        /// </summary>
        private void CargarGrillaResultado()
        {
            gvwResultado.DataSource = tablaResultado.DefaultView;
            gvwResultado.DataBind();
            udpResultado.Update();
        }

        /// <summary>
        /// Paso 4: Obtener el Preorden Total
        /// </summary>
        private void EjecutarPaso4()
        {
            #region --[Paso 4]--
            tablaPaso2.Columns.Add("Ranking", System.Type.GetType("System.Decimal"));
            for (int i = 0; i < tablaPaso2.Rows.Count; i++)
            {
                tablaPaso2.Rows[i][tablaPaso2.Columns.Count - 1] = Convert.ToDecimal(tablaPaso2.Rows[i][tablaPaso2.Columns.Count - 2])
                    - Convert.ToDecimal(tablaPaso3.Rows[0][i + 1]);
            }

            tablaPaso2.DefaultView.Sort = "Ranking DESC";
            tablaResultado = tablaPaso2.DefaultView.ToTable();
            #endregion
        }

        /// <summary>
        /// Paso 3: Expresar como Xi supera a las demás alternativas y cómo es superada por las otras.
        /// </summary>
        private void EjecutarPaso3()
        {
            #region --[Paso 3]--
            tablaPaso3 = new DataTable("Promethee2");
            tablaPaso3 = tablaPaso2.Clone();
            tablaPaso3.Columns[0].DataType = System.Type.GetType("System.String");
            DataRow nuevaFila = tablaPaso3.NewRow();
            nuevaFila[0] = "FlujoSaliente";
            tablaPaso3.Rows.Add(nuevaFila);

            decimal acumuladorFila, acumuladorColumna;
            for (int i = 0; i < tablaPaso2.Rows.Count; i++)
            {
                acumuladorFila = 0;
                for (int j = 1; j < tablaPaso2.Columns.Count - 1; j++)
                {
                    acumuladorFila += (tablaPaso2.Rows[i][j] != DBNull.Value) ? Convert.ToDecimal(tablaPaso2.Rows[i][j]) : 0;
                }
                tablaPaso2.Rows[i][tablaPaso2.Columns.Count - 1] = acumuladorFila;
            }

            for (int i = 1; i < tablaPaso2.Columns.Count - 1; i++)
            {
                acumuladorColumna = 0;
                for (int j = 0; j < tablaPaso2.Rows.Count; j++)
                {
                    acumuladorColumna += (tablaPaso2.Rows[j][i] != DBNull.Value) ? Convert.ToDecimal(tablaPaso2.Rows[j][i]) : 0;
                }
                tablaPaso3.Rows[0][i] = acumuladorColumna;
            }
            #endregion
        }

        /// <summary>
        /// Paso 2: Expresar la intensidad de la preferencia de la alternativa Xi comparada con Xk
        /// </summary>
        /// <param name="listaConfiguracion">The lista configuracion.</param>
        private void EjecutarPaso2(List<Utility.Promethee> listaConfiguracion)
        {
            #region --[Paso 2]--
            tablaPaso2 = new DataTable("Promethee2");
            tablaPaso2.Columns.Add("Alternativas");
            DataRow nuevaFila;
            AlternativaEntity altAux = new AlternativaEntity();
            AlternativaEntity altAux2 = new AlternativaEntity();

            for (int i = 0; i < tablaPaso0.Rows.Count; i++)
            {
                altAux = listaAlternativa.Find(p => p.nombre == tablaPaso0.Rows[i][0].ToString());

                tablaPaso2.Columns.Add(altAux.idAlternativa.ToString(), System.Type.GetType("System.Decimal"));
                nuevaFila = tablaPaso2.NewRow();
                tablaPaso2.Rows.Add(nuevaFila);
                tablaPaso2.Rows[i]["Alternativas"] = altAux.idAlternativa.ToString();
            }
            tablaPaso2.Columns.Add("FlujoEntrante", System.Type.GetType("System.Decimal"));

            string[] alternativas;
            int indexFila = 0;
            int indexColumna = 0;
            decimal sumaPesos = 0;
            foreach (Utility.Promethee item in listaConfiguracion)
                sumaPesos += item.pesoCriterio;

            int nroFila;
            decimal valorAcumulado = 0;
            decimal valorCriterio = 0;

            foreach (DataRow item in tablaPaso1.Rows)
            {
                valorAcumulado = 0;
                valorCriterio = 0;
                alternativas = item[0].ToString().Split('-');
                //item 0: fila
                //item 1: columna
                int.TryParse(alternativas[0], out indexFila);
                int.TryParse(alternativas[1], out indexColumna);
                nroFila = tablaPaso2.Rows.IndexOf(tablaPaso2.Select("Alternativas='" + indexFila.ToString() + "'")[0]);

                foreach (Utility.Promethee criterio in listaConfiguracion)
                {
                    valorCriterio = (item[criterio.nombreCriterio] != DBNull.Value) ? Convert.ToDecimal(item[criterio.nombreCriterio]) : 0;
                    valorAcumulado += valorCriterio * criterio.pesoCriterio;
                }
                tablaPaso2.Rows[nroFila][indexColumna.ToString()] = Math.Round((valorAcumulado / (sumaPesos)), 2);
            }
            #endregion
        }

        /// <summary>
        /// Paso 1: determinar como se situan las alternativas con respecto a cada atributo.
        /// </summary>
        /// <param name="listaConfiguracion">The lista configuracion.</param>
        private void EjecutarPaso1(List<Utility.Promethee> listaConfiguracion)
        {
            decimal diferenciaCriterio = 0;
            decimal valorFuncPreferencia = 0;

            decimal valorPrincipal = 0, valorSecundario = 0;
            tablaPaso1 = new DataTable("Promethee1");

            tablaPaso1.Columns.Add("Alternativas");
            foreach (Utility.Promethee item in listaConfiguracion)
                tablaPaso1.Columns.Add(item.nombreCriterio);

            DataRow fila;
            AlternativaEntity altAux = new AlternativaEntity();
            AlternativaEntity altAux2 = new AlternativaEntity();

            #region --[Paso 1]--
            for (int i = 0; i < tablaPaso0.Rows.Count; i++)
            {
                for (int j = 0; j < tablaPaso0.Rows.Count; j++)
                {
                    if (i != j)
                    {
                        fila = tablaPaso1.NewRow();
                        altAux = listaAlternativa.Find(p => p.nombre == tablaPaso0.Rows[i][0].ToString());
                        altAux2 = listaAlternativa.Find(p => p.nombre == tablaPaso0.Rows[j][0].ToString());

                        fila["Alternativas"] = altAux.idAlternativa + "-" + altAux2.idAlternativa;

                        foreach (Utility.Promethee item in listaConfiguracion)
                        {
                            #region --[CRITERIOS]--
                            valorFuncPreferencia = -1;
                            diferenciaCriterio = 0;
                            decimal.TryParse(tablaPaso0.Rows[i][item.nombreCriterio].ToString(), out valorPrincipal);
                            decimal.TryParse(tablaPaso0.Rows[j][item.nombreCriterio].ToString(), out valorSecundario);

                            if (item.maximiza)
                            {
                                if (valorPrincipal >= valorSecundario)
                                {
                                    diferenciaCriterio = Math.Abs(valorPrincipal - valorSecundario);
                                    valorFuncPreferencia = Utility.Promethee.obtenerValorFuncPreferencia(item, diferenciaCriterio);
                                }
                            }
                            else
                            {
                                if (valorPrincipal <= valorSecundario)
                                {
                                    diferenciaCriterio = Math.Abs(valorPrincipal - valorSecundario);
                                    valorFuncPreferencia = Utility.Promethee.obtenerValorFuncPreferencia(item, diferenciaCriterio);
                                }
                            }
                            if (valorFuncPreferencia >= 0) fila[item.nombreCriterio] = valorFuncPreferencia;
                            else fila[item.nombreCriterio] = DBNull.Value;
                            #endregion
                        }
                        tablaPaso1.Rows.Add(fila);
                    }
                }
            }
            #endregion
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
                            esteCriterio.tipoFuncion = enumFuncionPreferencia.PseudoCriterioConPreferenciaLineal;
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
        /// Cargars the tabla paso1.
        /// </summary>
        /// <returns></returns>
        private void CargarTablaPaso0()
        {
            tablaPaso0 = new DataTable();

            tablaPaso0.Columns.Add("Alternativas");
            foreach (CriterioEntity itemCriterio in listaCriterio)
                tablaPaso0.Columns.Add(itemCriterio.nombre);

            DataRow nuevaFila;
            List<RelAlternativaCriterioEntity> listaAux = null;
            foreach (AlternativaEntity item in listaAlternativa)
            {
                nuevaFila = tablaPaso0.NewRow();
                nuevaFila[0] = item.nombre;

                listaAux = listaValores.FindAll(p => p.idAlternativa == item.idAlternativa);
                foreach (RelAlternativaCriterioEntity itemValor in listaAux)
                    nuevaFila[itemValor.nombreCriterio] = itemValor.valor;

                tablaPaso0.Rows.Add(nuevaFila);
            }
        }

        /// <summary>
        /// Initializes the workbook.
        /// </summary>
        /// <param name="path">The path.</param>
        void InitializeWorkbook(string path)
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                excelFile = new HSSFWorkbook(file);
            }
        }

        /// <summary>
        /// Converts to data table.
        /// </summary>
        /// <returns></returns>
        private DataTable ConvertToDataTable()
        {
            ISheet sheet = excelFile.GetSheetAt(1);
            //valido que la plantilla se haya creado para el modelo 
            lblError.Text = string.Empty;
            if (miModelo.idModelo == sheet.GetRow(0).Cells[1].NumericCellValue)
            {
                sheet = excelFile.GetSheetAt(0);

                DataTable dt = new DataTable();
                dt = buscarModelo();

                //for recorriendo las filas o cantidades de mimodelo.alternativas
                //tengo que arrancar en fila 2, columna 1
                double valor = 0;
                for (int i = 0; i < miModelo.alternativas; i++)
                    for (int j = 1; j <= miModelo.criterios; j++)
                    {
                        try { valor = sheet.GetRow(i + 2).Cells[j].NumericCellValue; }
                        catch { valor = 0; }
                        dt.Rows[i][j] = valor;
                    }
                return dt;
            }
            else
                lblError.Text = "El archivo seleccionado no corresponde al modelo indicado.";
            return null;
        }
        #endregion
        #endregion

    }
}