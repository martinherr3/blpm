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
		public static void ExportarPDF(string TituloInforme, DataTable dtReporte, string username, string filtros, string nombrePNG)
		{
			itsEvents ev = new itsEvents();

			Persona usuario = new Persona();
			usuario.username = username;
			BLPersona objBLPersona = new BLPersona(usuario);
			objBLPersona.GetPersonaByEntidad();
			usuario = objBLPersona.Data;

			int columnCount = dtReporte.Columns.Count;
			int rowCount = dtReporte.Rows.Count;

            Document documento = new Document(PageSize.A4, 10, 10, 80, 50);
			PdfWriter writerPdf = PdfWriter.GetInstance(documento, HttpContext.Current.Response.OutputStream);
			writerPdf.PageEvent = ev;
			documento.Open();

			string strTitulo = TituloInforme;
			string fecha = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');

			Font font24B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, Font.BOLD, BaseColor.GRAY);
			Phrase Titulo = new Phrase("EDU@R 2.0", font24B);

			Font font15B = FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLDITALIC);
			ev.tituloReporte = strTitulo;
			ev.fechaReporte = fecha;

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
			Font LetraTituloTabla = FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLDITALIC, BaseColor.BLUE);
			float[] espacios = new float[columnCount];
			for (int i = 0; i < columnCount; i++)
			{
				espacios[i] = 80 / columnCount;
			}

			grdTable.SetWidths(espacios);
			grdTable.WidthPercentage = 90;

			//Creamos las cabeceras de la tabla
			//Adicionamos las cabeceras a la tabla
			foreach (DataColumn columna in dtReporte.Columns)
			{
				grdTable.AddCell(new PdfPCell(new Phrase(columna.ColumnName.ToUpperInvariant(), LetraTituloTabla)));
			}

			Font LetraDefecto = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);

			grdTable.CompleteRow();

            grdTable.HeaderRows = 1;

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
			itsEvents ev = new itsEvents();

			Persona usuario = new Persona();
			usuario.username = username;
			BLPersona objBLPersona = new BLPersona(usuario);
			objBLPersona.GetPersonaByEntidad();
			usuario = objBLPersona.Data;

            Document documento = new Document(PageSize.A4, 10, 10, 80, 50);
			PdfWriter writerPdf = PdfWriter.GetInstance(documento, HttpContext.Current.Response.OutputStream);
			writerPdf.PageEvent = ev;
			documento.Open();

			string strTitulo = TituloInforme;
			string fecha = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');

			Font font24B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, Font.BOLD, BaseColor.GRAY);
			Phrase Titulo = new Phrase("EDU@R 2.0", font24B);

			Font font15B = FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLDITALIC);
			ev.tituloReporte = strTitulo;
			ev.fechaReporte = fecha;

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

            Document documento = new Document(PageSize.A4, 10, 10, 80, 50);

			PdfWriter writerPdf = PdfWriter.GetInstance(documento, HttpContext.Current.Response.OutputStream);
			writerPdf.PageEvent = ev;

			string strTitulo = TituloInforme;
			string fecha = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');

			Font font24B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, Font.BOLD, BaseColor.GRAY);
			Phrase Titulo = new Phrase("EDU@R 2.0", font24B);

			Font font15B = FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLDITALIC);
			ev.tituloReporte = strTitulo;
			ev.fechaReporte = fecha;

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

			strTitulo = strTitulo.Replace(" ", string.Empty);
			HttpContext.Current.Response.ContentType = "application/pdf";
			HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + strTitulo.Trim().Replace(" ", string.Empty) + "-" + fecha.Replace(' ', '_').Trim() + ".pdf");
			HttpContext.Current.Response.Flush();
		}

		public class itsEvents : PdfPageEventHelper
		{
			// This is the contentbyte object of the writer
			PdfContentByte cb;

			// this is the BaseFont we are going to use for the header / footer
			BaseFont bf = null;

			public string tituloReporte;
			public string fechaReporte;

			// we will put the final number of pages in a template
			protected PdfTemplate total;
			protected BaseFont helv;

			// we override the onOpenDocument method
			public override void OnOpenDocument(PdfWriter writer, Document document)
			{
				try
				{
					bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
					cb = writer.DirectContent;

					total = writer.DirectContent.CreateTemplate(100, 100);
					total.BoundingBox = new Rectangle(-20, -20, 100, 100);
					helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
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

			public override void OnStartPage(PdfWriter writer, Document document)
			{
				cb.PdfDocument.SetMargins(10, 10, 80, 50);
			}

			// we override the onEndPage method
			public override void OnEndPage(PdfWriter writer, Document documento)
			{
				#region --[Encabezados]--
				Font font24B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, Font.BOLD, BaseColor.GRAY);
				Font font15B = FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLDITALIC);

				Phrase Titulo = new Phrase("EDU@R 2.0", font24B);
				Phrase tipo = new Phrase(tituloReporte, font15B);
				Phrase fechas = new Phrase(fechaReporte, font15B);

				cb.BeginText();
				cb.SetTextMatrix(280, 300);
				ColumnText ct = new ColumnText(cb);
				ct.SetSimpleColumn(Titulo, documento.Left, 0, documento.Right, documento.Top + 38, 0, Element.ALIGN_CENTER);
				ct.Go();

				ColumnText ci = new ColumnText(cb);
				ci.SetSimpleColumn(tipo, documento.Left, 0, documento.Right, documento.Top + 20, 0, Element.ALIGN_CENTER);
				ci.Go();

				ColumnText cf = new ColumnText(cb);
				cf.SetSimpleColumn(fechas, documento.Left, 0, documento.Right, documento.Top + 5, 0, Element.ALIGN_CENTER);
				cf.Go();
				cb.EndText();
				#endregion

				cb.SaveState();
				string text = "Página " + writer.PageNumber.ToString().PadLeft(2, ' ') + " de ";
				float textBase = documento.Bottom - 20;
				cb.BeginText();
				cb.SetFontAndSize(helv, 12);

				cb.SetTextMatrix(255, 30);
				cb.ShowText(text);
				cb.EndText();
				cb.AddTemplate(total, 330, 30);

				cb.RestoreState();
			}

			// we override the onCloseDocument method
			public override void OnCloseDocument(PdfWriter writer, Document document)
			{
				if (writer.PageNumber > 0)
					writer.ViewerPreferences = PdfWriter.PageLayoutTwoPageLeft;
				else
					writer.ViewerPreferences = PdfWriter.PageLayoutSinglePage;

				total.BeginText();
				total.SetFontAndSize(helv, 12);
				total.SetTextMatrix(0, 0);
				int pageNumber = writer.PageNumber - 1;
				total.ShowText(Convert.ToString(pageNumber));
				total.EndText();
			}
		}
	}
}
