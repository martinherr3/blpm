using System;
using System.Data;
using System.Web;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.SessionState;
using System.Collections.Generic;
using System.IO;

namespace EDUAR_UI.Utilidades
{
    public class ExportPDF
    {
        /// <summary>
        /// Exportars the PDF.
        /// </summary>
        /// <param name="TituloPagina">The titulo pagina.</param>
        /// <param name="dtReporte">The dt reporte.</param>
        /// <param name="username">The username.</param>
        /// <param name="filtros">The filtros.</param>
        public static void ExportarPDF(string TituloPagina, DataTable dtReporte, string username, string filtros)
        {
            ExportarPDF(TituloPagina, dtReporte, username, filtros, string.Empty);
        }

        /// <summary>
        /// Exportars the PDF.
        /// </summary>
        /// <param name="TituloPagina">The titulo pagina.</param>
        /// <param name="dtReporte">The dt reporte.</param>
        /// <param name="username">The username.</param>
        /// <param name="filtros">The filtros.</param>
        /// <param name="nombrePNG">The nombre PNG.</param>
        public static void ExportarPDF(string TituloPagina, DataTable dtReporte, string username, string filtros, string nombrePNG)
        {
            Persona usuario = new Persona();
            usuario.username = username;
            BLPersona objBLPersona = new BLPersona(usuario);
            objBLPersona.GetPersonaByEntidad();
            usuario = objBLPersona.Data;

            string strTitulo = TituloPagina;
            int columnCount = dtReporte.Columns.Count;
            int rowCount = dtReporte.Rows.Count;
            string fecha = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');

            Document documento = new Document(PageSize.A4, 10, 10, 100, 30);
            PdfWriter writerPdf = PdfWriter.GetInstance(documento, HttpContext.Current.Response.OutputStream);
            documento.Open();

            Font font24B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, Font.BOLD, BaseColor.GRAY);
            Phrase Titulo = new Phrase("EDU@R 2.0", font24B);

            Font font15B = FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLDITALIC);
            Phrase tipo = new Phrase(strTitulo, font15B);
            Phrase fechas = new Phrase(fecha, font15B);

            PdfContentByte cb = writerPdf.DirectContent;

            ColumnText ct = new ColumnText(cb);
            ct.SetSimpleColumn(Titulo, documento.Left, 0, documento.Right, documento.Top + 75, 24, Element.ALIGN_CENTER);
            ct.Go();

            ColumnText ci = new ColumnText(cb);
            ct.SetSimpleColumn(tipo, documento.Left, 0, documento.Right, documento.Top + 45, 15, Element.ALIGN_CENTER);
            ct.Go();

            ColumnText cf = new ColumnText(cb);
            ct.SetSimpleColumn(fechas, documento.Left, 0, documento.Right, documento.Top + 25, 15, Element.ALIGN_CENTER);
            ct.Go();

            Font font12B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.NORMAL);
            PdfPTable grdTableEncabezado = new PdfPTable(1);
            grdTableEncabezado.WidthPercentage = 90;
            grdTableEncabezado.AddCell(new PdfPCell(new Phrase("- Usuario: " + usuario.apellido + " " + usuario.nombre, font12B)));
            grdTableEncabezado.CompleteRow();
            grdTableEncabezado.AddCell(new PdfPCell(new Phrase(filtros, font12B)));
            grdTableEncabezado.CompleteRow();
            documento.Add(grdTableEncabezado);

            //valido si mando el nombre de un gráfico
            if (!string.IsNullOrEmpty(nombrePNG))
            {
                //Verifica si existe el archivo
                if (System.IO.File.Exists(nombrePNG))
                {
                    string TmpPath = System.Configuration.ConfigurationManager.AppSettings["oTmpPath"];

                    documento.Add(new Paragraph(""));
                    Image grafico = Image.GetInstance(nombrePNG);
                    grafico.ScalePercent(50, 50);
                    grafico.Alignment = Element.ALIGN_CENTER;
                    if (grafico != null)
                        documento.Add(grafico);
                    documento.Add(new Paragraph(""));
                }
            }

            PdfPTable grdTable = new PdfPTable(columnCount);
            Font LetraTituloTabla = FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLDITALIC, BaseColor.DARK_GRAY);
            float[] espacios = new float[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                espacios[i] = 80 / columnCount;
            }

            grdTable.SetWidths(espacios);
            grdTable.WidthPercentage = 90;

            //Cremos las cabeceras de la tabla
            //Adicionamos las cabeceras a la tabla
            foreach (DataColumn columna in dtReporte.Columns)
            {
                grdTable.AddCell(new PdfPCell(new Phrase(columna.ColumnName.ToUpperInvariant(), LetraTituloTabla)));
            }

            Font LetraDefecto = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);

            grdTable.CompleteRow();

            foreach (DataRow fila in dtReporte.Rows)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    string dato = fila[i].ToString();
                    if (fila[i].GetType().Name == "DateTime")
                        dato = Convert.ToDateTime(fila[i].ToString()).ToShortDateString();
                    grdTable.AddCell(new Phrase(HttpUtility.HtmlDecode(dato), LetraDefecto));
                }
                grdTable.CompleteRow();
            }

            //Cerramos el Documento 
            documento.Add(grdTable);
            documento.Close();

            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + strTitulo.Trim().Replace(" ", string.Empty) + "-" + fecha.Replace(' ', '_').Trim() + ".pdf");
            HttpContext.Current.Response.Flush();//HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Exportars the grafico PDF.
        /// </summary>
        /// <param name="TituloInforme">The titulo informe.</param>
        /// <param name="username">The username.</param>
        /// <param name="filtros">The filtros.</param>
        /// <param name="nombrePNG">The nombre PNG.</param>
        /// <param name="tablaGrafico">The tabla grafico.</param>
        public static void ExportarGraficoPDF(string TituloInforme, string username, string filtros, string nombrePNG, List<string> tablaGrafico)
        {
            Persona usuario = new Persona();
            usuario.username = username;
            BLPersona objBLPersona = new BLPersona(usuario);
            objBLPersona.GetPersonaByEntidad();
            usuario = objBLPersona.Data;

            string strTitulo = TituloInforme;
            string fecha = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');

            Document documento = new Document(PageSize.A4, 10, 10, 100, 30);
            PdfWriter writerPdf = PdfWriter.GetInstance(documento, HttpContext.Current.Response.OutputStream);
            documento.Open();

            Font font24B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, Font.BOLD, BaseColor.GRAY);
            Phrase Titulo = new Phrase("EDU@R 2.0", font24B);

            Font font15B = FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLDITALIC);
            Phrase tipo = new Phrase(strTitulo, font15B);
            Phrase fechas = new Phrase(fecha, font15B);

            PdfContentByte cb = writerPdf.DirectContent;

            ColumnText ct = new ColumnText(cb);
            ct.SetSimpleColumn(Titulo, documento.Left, 0, documento.Right, documento.Top + 75, 24, Element.ALIGN_CENTER);
            ct.Go();

            ColumnText ci = new ColumnText(cb);
            ct.SetSimpleColumn(tipo, documento.Left, 0, documento.Right, documento.Top + 45, 15, Element.ALIGN_CENTER);
            ct.Go();

            ColumnText cf = new ColumnText(cb);
            ct.SetSimpleColumn(fechas, documento.Left, 0, documento.Right, documento.Top + 25, 15, Element.ALIGN_CENTER);
            ct.Go();

            Font font12B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.NORMAL);
            Font font10N = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL);
            PdfPTable grdTableEncabezado = new PdfPTable(1);
            grdTableEncabezado.WidthPercentage = 90;
            grdTableEncabezado.AddCell(new PdfPCell(new Phrase("- Usuario: " + usuario.apellido + " " + usuario.nombre, font12B)));
            grdTableEncabezado.CompleteRow();
            grdTableEncabezado.AddCell(new PdfPCell(new Phrase(filtros, font12B)));
            grdTableEncabezado.CompleteRow();
            documento.Add(grdTableEncabezado);

            //valido si mando el nombre de un gráfico
            if (!string.IsNullOrEmpty(nombrePNG))
            {
                //Verifica si existe el archivo
                if (System.IO.File.Exists(nombrePNG))
                {
                    documento.Add(new Paragraph(""));
                    Image grafico = Image.GetInstance(nombrePNG);
                    grafico.ScalePercent(50, 50);
                    grafico.Alignment = Element.ALIGN_CENTER;
                    if (grafico != null)
                        documento.Add(grafico);
                    documento.Add(new Paragraph(""));
                }
            }

            // Recorremos la variable tablaGrafico para agregar información adicional a la exportación del gráfico
            foreach (var item in tablaGrafico)
            {
                if (item.Substring(0, 1).Equals("-"))
                    documento.Add(new iTextSharp.text.Paragraph(item, font12B));
                else
                    documento.Add(new iTextSharp.text.Paragraph(item, font10N));
            }

            //Cerramos el Documento 
            documento.Close();
            strTitulo = strTitulo.Replace(" ", string.Empty);
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + strTitulo.Trim().Replace(" ", string.Empty) + "-" + fecha.Replace(' ', '_').Trim() + ".pdf");
            HttpContext.Current.Response.Flush();
        }

        /// <summary>
        /// Exportars the grafico PDF.
        /// </summary>
        /// <param name="TituloInforme">The titulo informe.</param>
        /// <param name="username">The username.</param>
        /// <param name="filtros">The filtros.</param>
        /// <param name="nombrePNG">The nombre PNG.</param>
        /// <param name="tablaGrafico">The tabla grafico.</param>
        public static void ExportarGraficoPDF(string TituloInforme, string username, string filtros, string nombrePNG, List<TablaGrafico> tablaGrafico)
        {
            itsEvents ev = new itsEvents();

            Persona usuario = new Persona();
            usuario.username = username;
            BLPersona objBLPersona = new BLPersona(usuario);
            objBLPersona.GetPersonaByEntidad();
            usuario = objBLPersona.Data;

            Document documento = new Document(PageSize.A4, 10, 10, 30, 30);

            PdfWriter writerPdf = PdfWriter.GetInstance(documento, HttpContext.Current.Response.OutputStream);
            writerPdf.PageEvent = ev;

            string strTitulo = TituloInforme;
            string fecha = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');

            Font font24B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, Font.BOLD, BaseColor.GRAY);
            Phrase Titulo = new Phrase("EDU@R 2.0", font24B);

            Font font15B = FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLDITALIC);
            Phrase tipo = new Phrase(strTitulo, font15B);
            Phrase fechas = new Phrase(fecha, font15B);

            Encabezado encTitulo = new Encabezado();
            encTitulo.titulo = Titulo;
            encTitulo.llx = documento.Left;
            encTitulo.lly = 0;
            encTitulo.urx = documento.Top + 75;
            encTitulo.ury = 24;
            encTitulo.alignment = Element.ALIGN_CENTER;
            ev.Header.Add(encTitulo);

            encTitulo = new Encabezado();
            encTitulo.titulo = tipo;
            encTitulo.llx = documento.Left;
            encTitulo.lly = 0;
            encTitulo.urx = documento.Top + 45;
            encTitulo.ury = 15;
            encTitulo.alignment = Element.ALIGN_CENTER;
            ev.Header.Add(encTitulo);

            encTitulo = new Encabezado();
            encTitulo.titulo = fechas;
            encTitulo.llx = documento.Left;
            encTitulo.lly = 0;
            encTitulo.urx = documento.Top + 25;
            encTitulo.ury = 15;
            encTitulo.alignment = Element.ALIGN_CENTER;
            ev.Header.Add(encTitulo);

            documento.Open();

            Font font12B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.NORMAL);
            Font font10N = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL);
            PdfPTable grdTableEncabezado = new PdfPTable(1);
            grdTableEncabezado.WidthPercentage = 90;
            grdTableEncabezado.AddCell(new PdfPCell(new Phrase("- Usuario: " + usuario.apellido + " " + usuario.nombre, font12B)));
            grdTableEncabezado.CompleteRow();
            grdTableEncabezado.AddCell(new PdfPCell(new Phrase(filtros, font12B)));
            grdTableEncabezado.CompleteRow();
            documento.Add(grdTableEncabezado);

            //valido si mando el nombre de un gráfico
            if (!string.IsNullOrEmpty(nombrePNG))
            {
                //Verifica si existe el archivo
                if (System.IO.File.Exists(nombrePNG))
                {
                    documento.Add(new Paragraph(""));
                    Image grafico = Image.GetInstance(nombrePNG);
                    grafico.ScalePercent(50, 50);
                    grafico.Alignment = Element.ALIGN_CENTER;
                    if (grafico != null)
                        documento.Add(grafico);
                    documento.Add(new Paragraph(""));
                }
            }

            // Recorremos la variable tablaGrafico para agregar información adicional a la exportación del gráfico
            foreach (TablaGrafico item in tablaGrafico)
            {
                if (item.listaEncabezados != null && item.listaEncabezados.Count > 0)
                {
                    PdfPTable tabla = new PdfPTable(item.listaEncabezados.Count);
                    tabla.SpacingBefore = 10;
                    tabla.SpacingAfter = 0;
                    tabla.KeepTogether = true;
                    tabla.WidthPercentage = 90;
                    PdfPCell celdaTitulo = new PdfPCell(new Phrase(item.titulo, font15B));
                    celdaTitulo.Colspan = item.listaEncabezados.Count;
                    tabla.AddCell(celdaTitulo);
                    tabla.CompleteRow();

                    foreach (var celdaEncabezado in item.listaEncabezados)
                    {
                        tabla.AddCell(new PdfPCell(new Phrase(celdaEncabezado.ToString(), font12B)));
                    }
                    tabla.CompleteRow();

                    foreach (var fila in item.listaCuerpo)
                    {
                        foreach (var celda in fila)
                        {
                            tabla.AddCell(new PdfPCell(new Phrase(celda.ToString(), font10N)));
                        }
                        tabla.CompleteRow();
                    }
                    documento.Add(tabla);
                }
            }
            //Cerramos el Documento 
            documento.Close();

            writerPdf.ViewerPreferences = PdfWriter.PageLayoutTwoColumnLeft;

            strTitulo = strTitulo.Replace(" ", string.Empty);
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + strTitulo.Trim().Replace(" ", string.Empty) + "-" + fecha.Replace(' ', '_').Trim() + ".pdf");
            HttpContext.Current.Response.Flush();
        }

        public class itsEvents : PdfPageEventHelper
        {
            // This is the contentbyte object of the writer
            PdfContentByte cb;

            // we will put the final number of pages in a template
            PdfTemplate template;

            // this is the BaseFont we are going to use for the header / footer
            BaseFont bf = null;

            // this is the current act of the play
            String act = "";


            public List<Encabezado> Header { get; set; }

            public override void OnStartPage(PdfWriter writer, Document document)
            {
                //Paragraph p = null;
                //foreach (var item in Header)
                //{
                //    p = new Paragraph(item.titulo.Content + "n");
                //    //p.Content = item.titulo.Content;
                //    p.IndentationLeft = item.llx;
                //    p.IndentationRight = item.urx;
                //    p.Leading = item.leading;
                //    p.Alignment = Element.ALIGN_CENTER;
                //    //p.SpacingBefore = 10;
                //    //p.SpacingBefore = 20;
                //    document.Add(item.titulo);
                //    document.Add(new Paragraph(" ")); //Salto de linea
                //    p.Clear();
                //}
            }


            // we override the onEndPage method
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                int pageN = writer.PageNumber;
                String text = "Página " + pageN;
                float len = bf.GetWidthPoint(text, 8);
                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.SetTextMatrix(280, 30);
                cb.ShowText(text);
                cb.EndText();
                //cb.AddTemplate(template, 280 + len, 30);
                //cb.BeginText();
                ////cb.SetFontAndSize(bf, 8);
                //cb.SetTextMatrix(280, 820);

                //    PdfContentByte cb = writerPdf.DirectContent;

                //ColumnText ct = new ColumnText(cb);
                //ct.SetSimpleColumn(Titulo, documento.Left, 0, documento.Right, documento.Top + 75, 24, Element.ALIGN_CENTER);
                //ct.Go()
                PdfTextArray vector = new PdfTextArray();
                float x, y = 0;
                if (pageN % 2 == 1)
                {
                    //cb.ShowText("Romeo and Juliet");
                    cb.BeginText();
                    cb.SetTextMatrix(280, 820);
                    foreach (var item in Header)
                    {
                        cb.SetFontAndSize(item.titulo.Font.BaseFont, item.titulo.Font.Size);
                        //cb.ShowText(item.titulo.Content);
                        cb.NewlineText();
                        cb.NewlineShowText(item.titulo.Content);
                        cb.NewlineText();
                        cb.SetLeading(item.leading);
                        cb.NewPath();
                    }
                    cb.EndText();
                }
                else
                {
                    //cb.ShowText("Romeo and Juliet 2");
                    cb.BeginText();
                    cb.SetTextMatrix(280, 820);
                    foreach (var item in Header)
                    {
                        cb.SetFontAndSize(item.titulo.Font.BaseFont, item.titulo.Font.Size);
                        //cb.NewlineShowText(item.titulo.Content);
                        cb.NewlineText();
                        cb.NewlineShowText(item.titulo.Content);
                        cb.NewlineText();
                        cb.SetLeading(item.leading);
                        cb.NewPath();
                    }
                    cb.EndText();
                }
                //cb.EndText();
            }

            // we override the onOpenDocument method
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb = writer.DirectContent;
                    template = cb.CreateTemplate(50, 50);
                }
                catch (DocumentException ex)
                {
                    throw ex;
                }
                catch (IOException ex)
                {
                    throw ex;
                }
            }


            public itsEvents()
            {
                Header = new List<Encabezado>();

            }
        }

        public struct Encabezado
        {
            public Phrase titulo;
            public float llx;
            public float lly;
            public float urx;
            public float ury;
            public float leading;
            public int alignment;
        }
    }
}
