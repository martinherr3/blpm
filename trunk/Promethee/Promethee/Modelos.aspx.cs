using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using DataAccess.Entity;
using NPOI.HSSF.UserModel;
using Promethee.Utility;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using System.Collections;

namespace Promethee
{
    public partial class Modelos : System.Web.UI.Page
    {
        #region --[Atributos]--
        /// <summary>
        /// The excel file
        /// </summary>
        HSSFWorkbook excelFile;
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
                btnGuardarModelo.Visible = true;
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
                    case "editModelo":
                        txtNombre.Text = miModelo.nombre;
                        mpeModelo.Show();
                        break;
                    case "addAlternativa":
                        mpeAlternativas.Show();
                        break;
                    case "addCriterio":
                        mpeCriterios.Show();
                        break;
                    case "upload":
                        mpuUpload.Show();
                        break;
                    case "solve":
                        ResolverModelo();
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
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvwModelo.Rows)
            {
                if (rowItem.RowIndex == i)
                {
                    miModelo = listaModelos[i];
                    DescargarPlantilla();
                    udpModelos.Update();
                    break;
                }
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

                string FileName = Guid.NewGuid().ToString() + "." + FileExtension;

                fuCargarArchivo.SaveAs(MapPath("~/Files/" + FileName));

                ModelosDA.SaveFile(idModelo, FileName);
                //TODO: eliminar los archivos viejos y dejar solo el último subido para el modelo
                mpuUpload.Hide();
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
                //CargarGrilla();
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

                CargarGrilla();

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
                nuevoCriterio.LimpiarControles();
                LimpiarCampos();
                mpeModelo.Hide();
                mpeAlternativas.Hide();
                mpuUpload.Hide();
                udpModelosAsociados.Update();
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
            udpModelosAsociados.Update();
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
            List<AlternativaEntity> listaAlternativa = AlternativasDA.Select(new AlternativaEntity() { idModelo = miModelo.idModelo });
            List<CriterioEntity> listaCriterio = CriteriosDA.Select(new CriterioEntity() { idModelo = miModelo.idModelo });

            DataTable resultado = new DataTable();
            DataRow fila = null;
            DataColumn columna = null;
            resultado.Columns.Add("Alternativa\\Criterio");
            foreach (CriterioEntity item in listaCriterio)
            {
                columna = new DataColumn(item.nombre);
                columna.Caption = item.nombre;
                resultado.Columns.Add(columna);
            }

            foreach (AlternativaEntity item in listaAlternativa)
            {
                fila = resultado.NewRow();
                fila[0] = item.nombre;
                resultado.Rows.Add(fila);
            }
            return resultado;
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
        #endregion

        #region --[Resolver]--
        /// <summary>
        /// Resolvers the modelo.
        /// </summary>
        private void ResolverModelo()
        {
            List<Utility.Promethee> listaConfiguracion = ObtenerConfiguracion();

            tablaPaso0 = CargarTablaPaso0();

            EjecutarPaso1(listaConfiguracion);
        }

        /// <summary>
        /// Ejecutars the paso1.
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
            // Paso 1: determinar como se situan las alternativas con respecto a cada atributo.
            #region --[Paso 1]--
            for (int i = 0; i < tablaPaso0.Rows.Count; i++)
            {
                for (int j = 0; j < tablaPaso0.Rows.Count; j++)
                {
                    if (i != j)
                    {
                        fila = tablaPaso1.NewRow();
                        fila["Alternativas"] = tablaPaso0.Rows[i][0] + "-" + tablaPaso0.Rows[j][0];

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

            List<CriterioEntity> listaCriterio = CriteriosDA.Select(new CriterioEntity() { idModelo = miModelo.idModelo });

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
        private DataTable CargarTablaPaso0()
        {
            InitializeWorkbook(MapPath("~/Files/" + miModelo.filename));

            return ConvertToDataTable();
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
            if (miModelo.idModelo == sheet.GetRow(0).Cells[1].NumericCellValue)
            {
                sheet = excelFile.GetSheetAt(0);

                DataTable dt = new DataTable();
                dt = buscarModelo();

                //for recorriendo las filas o cantidades de mimodelo.alternativas
                //tengo que arrancar en fila 2, columna 1
                for (int i = 0; i < miModelo.alternativas; i++)
                    for (int j = 1; j <= miModelo.criterios; j++)
                        dt.Rows[i][j] = sheet.GetRow(i + 2).Cells[j].NumericCellValue;
                return dt;
            }
            return null;
        }
        #endregion
    }
}