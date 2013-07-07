using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Utilidades;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using System.IO;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using System.Text;

namespace EDUAR_UI.UserControls
{
    public partial class Reporte : UserControl
    {
        #region --[Atributos]--
        PagedDataSource pds = new PagedDataSource();

        /// <summary>
        /// The excel file
        /// </summary>
        HSSFWorkbook excelFile;
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Gets or sets the grilla reporte.
        /// </summary>
        /// <value>
        /// The grilla reporte.
        /// </value>
        public GridView GrillaReporte
        {
            get
            { return gvwReporte; }
            set
            { gvwReporte = value; }
        }

        /// <summary>
        /// Gets or sets the grafico reporte.
        /// </summary>
        /// <value>
        /// The grafico reporte.
        /// </value>
        public Grafico graficoReporte
        {
            get { return grafico; }
            set { grafico = value; }
        }

        /// <summary>
        /// Gets or sets the dt reporte.
        /// </summary>
        /// <value>
        /// The dt reporte.
        /// </value>
        public DataTable dtReporte
        {
            get
            {
                if (Session["dtReporte"] == null)
                    Session["dtReporte"] = new DataTable();
                return (DataTable)Session["dtReporte"];
            }
            set { Session["dtReporte"] = value; }
        }

        /// <summary>
        /// Gets or sets the titulo reporte.
        /// </summary>
        /// <value>
        /// The titulo reporte.
        /// </value>
        public string tituloReporte
        {
            get
            {
                if (Session["tituloReporte"] == null)
                    Session["tituloReporte"] = string.Empty;
                return Session["tituloReporte"].ToString();
            }
            set { Session["tituloReporte"] = value; }
        }

        /// <summary>
        /// Gets or sets the filtros aplicados.
        /// </summary>
        /// <value>
        /// The filtros aplicados.
        /// </value>
        public string filtrosAplicados
        {
            get
            {
                if (Session["filtrosAplicados"] == null)
                    return string.Empty;
                return Session["filtrosAplicados"].ToString();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [ver grafico].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ver grafico]; otherwise, <c>false</c>.
        /// </value>
        public bool verGrafico
        {
            get
            {
                if (ViewState["verGrafico"] == null)
                    ViewState["verGrafico"] = false;
                return (bool)ViewState["verGrafico"];
            }
            set { ViewState["verGrafico"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [ver boton grafico].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ver boton grafico]; otherwise, <c>false</c>.
        /// </value>
        public bool verBotonGrafico
        {
            get
            {
                if (ViewState["verBotonGrafico"] == null)
                    verBotonGrafico = true;
                return (bool)ViewState["verBotonGrafico"];
            }
            set { ViewState["verBotonGrafico"] = value; }
        }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage
        {
            get
            {
                if (this.ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt16(this.ViewState["CurrentPage"].ToString());
                }
            }
            set
            {
                this.ViewState["CurrentPage"] = value;
            }
        }
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
                btnPDF.Click += (ExportarPDF);
                btnVolver.Click += (Volver);
                btnGraficar.Click += (Graficar);
                btnImprimir.Click += (Imprimir);
                grafico.CerrarClick += (CerrarGrafico);
                GrillaReporte.Sorting += (Ordenar);
                GrillaReporte.PageIndexChanging += (PaginandoGrilla);
                tituloReporte = Page.Title;

                if (!Page.IsPostBack)
                {
                    grafico.habilitarTorta = true;
                    btnPDF.Visible = false;
                    btnGraficar.Visible = false;
                    btnVolver.Visible = false;
                    btnImprimir.Visible = false;
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// Handles the Click event of the btnImprimir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Imprimir", "AbrirPopup();", true);
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// Handles the Click event of the btnExportarExcel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarExcel();
                Response.Clear();

                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", tituloReporte.Trim().Replace(" ", string.Empty) + "-" + DateTime.Now.ToShortDateString().Replace(':', '_').Trim() + ".xls"));
                Response.BinaryWrite(WriteToStream().GetBuffer());
                Response.Flush();
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// Cerrars the grafico.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void CerrarGrafico(object sender, EventArgs e)
        {
            OnCerrarGraficoClick(CerrarGraficoClick, e);
        }
        #endregion

        #region --[Delegados ]--
        public delegate void VentanaBotonClickHandler(object sender, EventArgs e);
        public delegate void PaginarGrillaHandler(object sender, GridViewPageEventArgs e);
        public delegate void OrdenarClickHandler(object sender, GridViewSortEventArgs e);

        public event VentanaBotonClickHandler ExportarPDFClick;
        public event VentanaBotonClickHandler VolverClick;
        public event VentanaBotonClickHandler GraficarClick;
        public event VentanaBotonClickHandler ImprimirClick;
        public event VentanaBotonClickHandler CerrarGraficoClick;
        public event PaginarGrillaHandler PaginarGrilla;
        public event OrdenarClickHandler OrdenarClick;

        /// <summary>
        /// Called when [exportar PDF click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public virtual void OnExportarPDFClick(VentanaBotonClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }

        /// <summary>
        /// Called when [volver click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public virtual void OnVolverClick(VentanaBotonClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }

        /// <summary>
        /// Called when [graficar click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public virtual void OnGraficarClick(VentanaBotonClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }

        /// <summary>
        /// Called when [imprimir click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public virtual void OnImprimirClick(VentanaBotonClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }

        /// <summary>
        /// Called when [cerrar grafico click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public virtual void OnCerrarGraficoClick(VentanaBotonClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }

        /// <summary>
        /// Ons the paginando grilla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        public virtual void onPaginandoGrilla(PaginarGrillaHandler sender, GridViewPageEventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }

        /// <summary>
        /// Called when [ordenar click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        public virtual void OnOrdenarClick(OrdenarClickHandler sender, GridViewSortEventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                //sender(this, e);
            }
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Cargars the reporte.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">The lista.</param>
        public void CargarReporte<T>(List<T> lista)
        {
            try
            {
                if (lista.Count != 0)
                {
                    //GrillaReporte = UIUtilidades.GenerarGrilla(lista, GrillaReporte);
                    dtReporte = UIUtilidades.BuildDataTable<T>(lista);
                    GrillaReporte = UIUtilidades.GenerarGrilla(GrillaReporte, dtReporte);
                    btnExcel.Visible = true;
                    btnVolver.Visible = true;
                    btnPDF.Visible = true;
                    btnGraficar.Visible = verBotonGrafico;
                    btnImprimir.Visible = true;
                    gvwReporte.Visible = true;
                    CargarGrilla(false);
                }
                else
                {
                    lblFiltros.Text = string.Empty;
                    GrillaReporte.Visible = false;
                    btnVolver.Visible = true;
                    btnPDF.Visible = false;
                    btnExcel.Visible = false;
                    btnGraficar.Visible = false;
                    btnImprimir.Visible = false;
                }
                lblSinDatos.Visible = (!(lista.Count != 0));
                divPaginacion.Visible = lista.Count != 0;
                udpReporte.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Exportars the PDF.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void ExportarPDF(object sender, EventArgs e)
        {
            OnExportarPDFClick(ExportarPDFClick, e);
        }

        private void ExportarExcel()
        {
            InitializeWorkbook();

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
            unEstiloDecimal.Alignment = HorizontalAlignment.CENTER;
            unEstiloDecimal.DataFormat = format.GetFormat("0.00");
            unEstiloDecimal.SetFont(unaFuente);

            ICellStyle estiloNormal = excelFile.CreateCellStyle();
            estiloNormal.Alignment = HorizontalAlignment.LEFT;
            estiloNormal.SetFont(unaFuente);

            ICellStyle estiloFecha = excelFile.CreateCellStyle();
            estiloFecha.Alignment = HorizontalAlignment.CENTER;
            estiloFecha.SetFont(unaFuente);

            // Pick font style settings to add to new style
            IFont rowCellFont = excelFile.CreateFont();
            rowCellFont.FontName = "Calibri";
            rowCellFont.FontHeightInPoints = 12;
            rowCellFont.Color = HSSFColor.WHITE.index;
            rowCellFont.Boldweight = (short)FontBoldWeight.BOLD.GetHashCode();

            // Create a new style, set background color, assign font style (style is created in the workbook object)
            ICellStyle rowCellStyle = excelFile.CreateCellStyle();
            rowCellStyle.FillForegroundColor = HSSFColor.ROYAL_BLUE.index;
            rowCellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            rowCellStyle.Alignment = HorizontalAlignment.CENTER;
            rowCellStyle.SetFont(rowCellFont);
            #endregion

            #region --[Hoja Datos]--
            //ISheet hojaUno = excelFile.CreateSheet(tituloReporte);

            HSSFSheet hojaUno = (HSSFSheet)excelFile.CreateSheet(tituloReporte);

            IRow filaEncabezado = hojaUno.CreateRow(0);
            int auxNumRow = 0;

            filaEncabezado = hojaUno.CreateRow(0);
            for (int i = 0; i < dtReporte.Columns.Count; i++)
            {
                filaEncabezado.CreateCell(i).SetCellValue(dtReporte.Columns[i].ColumnName.ToUpper());
                filaEncabezado.HeightInPoints = 16;
                filaEncabezado.Cells[i].CellStyle = rowCellStyle;
            }

            auxNumRow++;
            DateTime auxDate;
            Double auxDec;
            for (int i = 0; i < dtReporte.Rows.Count; i++, auxNumRow++)
            {
                filaEncabezado = hojaUno.CreateRow(auxNumRow);

                for (int j = 0; j < dtReporte.Columns.Count; j++)
                {
                    if (Double.TryParse(dtReporte.Rows[i][j].ToString(), out auxDec))
                    {
                        filaEncabezado.CreateCell(j).SetCellValue(auxDec);
                        filaEncabezado.Cells[j].CellStyle = unEstiloDecimal;
                    }
                    else
                        if (DateTime.TryParse(dtReporte.Rows[i][j].ToString(), out auxDate))
                        {
                            filaEncabezado.CreateCell(j).SetCellValue(auxDate.ToShortDateString());
                            filaEncabezado.Cells[j].CellStyle = estiloFecha;
                        }
                        else
                        {
                            filaEncabezado.CreateCell(j).SetCellValue(dtReporte.Rows[i][j].ToString());
                            filaEncabezado.Cells[j].CellStyle = estiloNormal;
                        }
                }

                if (i == 1)
                {
                    HSSFPatriarch patr = (HSSFPatriarch)hojaUno.CreateDrawingPatriarch();
                    IComment comment = patr.CreateCellComment(new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 4, 5));
                    StringBuilder miCadena = new StringBuilder();
                    miCadena.AppendLine("Filtros Aplicados");
                    miCadena.AppendLine(filtrosAplicados);
                    comment.String = new HSSFRichTextString(miCadena.ToString());
                    comment.Author = "EDU@R 2.0";
                    //comment.Visible = true;
                    comment.Column = dtReporte.Columns.Count + 1;
                    comment.Row = 2;
                    filaEncabezado.CreateCell(dtReporte.Columns.Count + 1).CellComment = comment;
                }
            }

            hojaUno.SetAutoFilter(new CellRangeAddress(0, dtReporte.Rows.Count, 0, dtReporte.Columns.Count - 1));

            hojaUno.CreateFreezePane(0, 1, 0, 1);

            for (int i = 0; i <= dtReporte.Columns.Count; i++)
                hojaUno.AutoSizeColumn(i);

            
            
            //hojaUno.set(new CellRangeAddress(firstCell.getRow(), lastCell.getRow(), firstCell.getCol(), lastCell.getCol()));

            #endregion
        }

        /// <summary>
        /// Volvers the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Volver(object sender, EventArgs e)
        {
            OnVolverClick(VolverClick, e);
        }

        /// <summary>
        /// Imprimirs the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void Imprimir(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Imprimir", "AbrirPopup();", true);
            OnImprimirClick(ImprimirClick, e);
        }

        /// <summary>
        /// Graficars the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Graficar(object sender, EventArgs e)
        {
            verGrafico = true;
            graficoReporte.TablaGrafico.Clear();
            OnGraficarClick(GraficarClick, e);
        }

        /// <summary>
        /// Ordenars the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        void Ordenar(object sender, GridViewSortEventArgs e)
        {
            GridSampleSortExpression = e.SortExpression;
            CargarGrilla(false);
            //GrillaReporte.DataSource = sortDataView(dtReporte.DefaultView, false);
            //GrillaReporte.DataBind();
            //GrillaReporte.PageIndex = pageIndex;
            //OnOrdenarClick(OrdenarClick, e);
        }

        /// <summary>
        /// Paginandoes the grilla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        void PaginandoGrilla(object sender, GridViewPageEventArgs e)
        {
            GrillaReporte.DataSource = sortDataView(GrillaReporte.DataSource as DataView, true);
            GrillaReporte.PageIndex = e.NewPageIndex;
            GrillaReporte.DataBind();
            onPaginandoGrilla(PaginarGrilla, e);
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">The lista.</param>
        private void CargarGrilla<T>(List<T> lista)
        {
            if (lista.Count > 0) lblFiltros.Text = filtrosAplicados.Replace("\n", "<br />");
            else lblFiltros.Text = string.Empty;
            dtReporte = UIUtilidades.BuildDataTable<T>(lista);

            DataSet ds = new DataSet();
            ds.Tables.Add(dtReporte);

            // Aqui llenamos nuestro DataSet
            DataView dv = ds.Tables[0].DefaultView;
            dv = sortDataView(dv, true);
            GrillaReporte.DataSource = dv;
            GrillaReporte.DataBind();
        }

        /// <summary>
        /// Cargars the grilla.
        /// </summary>
        /// <param name="paginando">if set to <c>true</c> [paginando].</param>
        private void CargarGrilla(bool paginando)
        {
            pds.DataSource = sortDataView(dtReporte.DefaultView, paginando);

            pds.AllowPaging = true;
            pds.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            pds.CurrentPageIndex = CurrentPage;
            lnkbtnNext.Visible = !pds.IsLastPage;
            lnkbtnLast.Visible = !pds.IsLastPage;
            lnkbtnPrevious.Visible = !pds.IsFirstPage;
            lnkbtnFirst.Visible = !pds.IsFirstPage;
            GrillaReporte = UIUtilidades.GenerarGrilla(GrillaReporte, dtReporte);
            GrillaReporte.PageSize = pds.PageSize;
            GrillaReporte.DataSource = pds;
            GrillaReporte.DataBind();
            doPaging();
            lblCantidad.Text = dtReporte.Rows.Count.ToString() + " Registros";
            udpReporte.Update();
        }
        #endregion

        #region --[Ordenamiento]--
        /// <summary>
        /// Gets or sets the grid sample sort direction.
        /// </summary>
        /// <value>
        /// The grid sample sort direction.
        /// </value>
        private string GridSampleSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        /// <summary>
        /// Gets or sets the grid sample sort expression.
        /// </summary>
        /// <value>
        /// The grid sample sort expression.
        /// </value>
        private string GridSampleSortExpression
        {
            get { return ViewState["SortExpression"] as string ?? dtReporte.Columns[0].Caption; }
            set { ViewState["SortExpression"] = value; }
        }

        /// <summary>
        /// Gets the sort direction.
        /// </summary>
        /// <returns></returns>
        private string getSortDirection()
        {
            switch (GridSampleSortDirection)
            {
                case "ASC":
                    GridSampleSortDirection = "DESC";
                    break;

                case "DESC":
                    GridSampleSortDirection = "ASC";
                    break;
            }
            return GridSampleSortDirection;
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the GrillaReporte control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void GrillaReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrillaReporte.DataSource = sortDataView(dtReporte.DefaultView, true);
            GrillaReporte.PageIndex = e.NewPageIndex;
            GrillaReporte.DataBind();
        }

        /// <summary>
        /// Sorts the data view.
        /// </summary>
        /// <param name="dataView">The data view.</param>
        /// <param name="isPageIndexChanging">if set to <c>true</c> [is page index changing].</param>
        /// <returns></returns>
        protected DataView sortDataView(DataView dataView, bool isPageIndexChanging)
        {
            if (isPageIndexChanging)
            {
                dataView.Sort = string.Format("{0} {1}",
                GridSampleSortExpression,
                GridSampleSortDirection);
            }
            else
            {
                dataView.Sort = string.Format("{0} {1}",
                GridSampleSortExpression,
                getSortDirection());
            }
            return dataView;
        }
        #endregion

        #region --[Paginación]--
        private void doPaging()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            for (int i = 0; i < pds.PageCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }
            dlPaging.DataSource = dt;
            dlPaging.DataBind();
        }

        protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("lnkbtnPaging"))
            {
                CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
                CargarGrilla(true);
            }
        }

        protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
            if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkbtnPage.Enabled = false;
                lnkbtnPage.Font.Bold = true;
            }
        }

        protected void lnkbtnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            CargarGrilla(true);
        }

        protected void lnkbtnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            CargarGrilla(true);
        }

        protected void lnkbtnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = dlPaging.Controls.Count - 1;
            CargarGrilla(true);
        }

        protected void lnkbtnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            CargarGrilla(true);
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 0;
            CargarGrilla(false);
        }
        #endregion

        #region --[Generación de Excel]--
        /// <summary>
        /// Initializes the workbook.
        /// </summary>
        void InitializeWorkbook()
        {
            excelFile = new HSSFWorkbook();

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "EDU@R 2.0";
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
        #endregion
    }
}
