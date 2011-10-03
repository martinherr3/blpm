using System;
using System.Data;
using System.Web;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EDUAR_UI.Utilidades
{
	public class ExportPDF
	{
		public static void ExportarPDF(string TituloPagina, DataTable dtReporte, string username, string filtros)
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
	}
}
