using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EDUAR_UI
{
	public partial class reportIndicadores : EDUARBasePage
	{
		#region --[Atributos]--
		HSSFWorkbook excelFile;
		//Variables para calcular el tamaño de cada columna
		Single sngMayorValor, sngMayor1, sngMayor2, sngMayor3;
		Single valor1, valor2, valor3;
		string Alumno1, Alumno2, Alumno3;
		#endregion

		#region --[Propiedades]--
		//1) Declaring the variables and Adding the RowCreated Event
		public static bool isSort = false;
		public static bool isAscend = false;
		private const string ASCENDING = " ASC";
		private const string DESCENDING = " DESC";
		public static bool showImage = false;
		public string GridSampleSortExpression { get; set; }

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

		private SortDirection GridViewSortDirection
		{
			get
			{
				if (ViewState["sortDirection"] == null)
					ViewState["sortDirection"] = SortDirection.Ascending;
				return (SortDirection)ViewState["sortDirection"];
			}
			set { ViewState["sortDirection"] = value; }
		}

		private List<RptIndicadores> lista
		{
			get
			{
				if (ViewState["lista"] == null)
					ViewState["lista"] = null;
				return (List<RptIndicadores>)ViewState["lista"];
			}
			set { ViewState["lista"] = value; }
		}

		public string Orden
		{
			get
			{
				if (ViewState["OrdenGrilla"] == null)
					ViewState["OrdenGrilla"] = "Resultados obtenidos en orden Descendente";
				return ViewState["OrdenGrilla"].ToString();
			}
			set
			{
				ViewState["OrdenGrilla"] = value;
			}
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
				Master.BotonAvisoAceptar += (VentanaAceptar);

				if (!Page.IsPostBack)
				{
					lblCicloLectivo.Text = cicloLectivoActual.nombre;
					CargarPresentacion();
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
		protected void VentanaAceptar(object sender, EventArgs e)
		{
			try
			{
				switch (AccionPagina)
				{
					case enumAcciones.Buscar:
						break;
					case enumAcciones.Nuevo:
						break;
					case enumAcciones.Modificar:
						break;
					case enumAcciones.Eliminar:
						break;
					case enumAcciones.Seleccionar:
						break;
					case enumAcciones.Limpiar:
						break;
					case enumAcciones.Aceptar:
						break;
					case enumAcciones.Salir:
						break;
					case enumAcciones.Redirect:
						break;
					case enumAcciones.Guardar:
						break;
					case enumAcciones.Ingresar:
						break;
					case enumAcciones.Desbloquear:
						break;
					case enumAcciones.Error:
						break;
					default:
						break;
				}
				AccionPagina = enumAcciones.Limpiar;
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the Click event of the btnExcel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnExcel_Click(object sender, EventArgs e)
		{
			try
			{
				string mensaje = ValidarDatos();
				if (!string.IsNullOrEmpty(mensaje))
					Master.MostrarMensaje("Advertencia", mensaje, enumTipoVentanaInformacion.Advertencia);
				else
				{
					if (tablaResultado != null && tablaResultado.Rows.Count > 0)
					{
						string filename = "Indicadores " + ddlCurso.SelectedItem.Text + " " + cicloLectivoActual.nombre + ".xls";
						filename = filename.Replace(" ", "_");
						Response.ContentType = "application/vnd.ms-excel";
						Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
						Response.Clear();

						InitializeWorkbook();
						GenerateData();
						Response.BinaryWrite(WriteToStream().GetBuffer());
					}
					else
						Master.MostrarMensaje("Advertencia", "No se ha realizado ningún cálculo.", enumTipoVentanaInformacion.Advertencia);
				}
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the Click event of the btnExportarPDF control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnExportarPDF_Click(object sender, EventArgs e)
		{
			try
			{
				string mensaje = ValidarDatos();
				if (!string.IsNullOrEmpty(mensaje))
					Master.MostrarMensaje("Advertencia", mensaje, enumTipoVentanaInformacion.Advertencia);
				else
				{
					if (tablaResultado != null && tablaResultado.Rows.Count > 0)
					{
						DataTable tablaExportPDF = tablaResultado.Copy();
						string nombre = string.Empty;

						for (int i = tablaExportPDF.Columns.Count - 2; i > 0; i--)
							tablaExportPDF.Columns.RemoveAt(i);

						ExportPDF.ExportarPDF("Indicadores " + ddlCurso.SelectedItem.Text + " " + cicloLectivoActual.nombre, tablaExportPDF, User.Identity.Name, "Resultados obtenidos en orden Descendente", NombrePNG);
					}
					else
						Master.MostrarMensaje("Advertencia", "No se ha realizado ningún cálculo.", enumTipoVentanaInformacion.Advertencia);
				}
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		/// <summary>
		/// Handles the Click event of the btnBuscar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnCalcular_Click(object sender, EventArgs e)
		{
			try
			{
				string mensaje = ValidarDatos();
				if (!string.IsNullOrEmpty(mensaje))
					Master.MostrarMensaje("Advertencia", mensaje, enumTipoVentanaInformacion.Advertencia);
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

		private void GraficarPodioResultado()
		{
			#region --[Top 3 Alumnos]--
			for (int i = 0; i < 3; i++)
			{
				var TopAlumno = from p in lista
								where p.alumnoApellido + " " + p.alumnoNombre == tablaResultado.Rows[i][0].ToString()
								//orderby p.promedio descending
								select p.alumnoApellido.Trim() + " " + p.alumnoNombre.Trim();
				//lblResultado.Text += alumno.ElementAt(0).ToString() + "<br />";
				switch (i)
				{
					case 0:
						valor1 = Convert.ToSingle(100);
						Alumno1 = TopAlumno.ElementAt(0).ToString();
						break;
					case 1:
						valor2 = Convert.ToSingle(75);
						Alumno2 = TopAlumno.ElementAt(0).ToString();
						break;
					case 2:
						valor3 = Convert.ToSingle(50);
						Alumno3 = TopAlumno.ElementAt(0).ToString();
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
			SolidBrush TurquoiseBrush = new SolidBrush(Color.Turquoise);
			SolidBrush VioletBrush = new SolidBrush(Color.Violet);
			SolidBrush SalmonBrush = new SolidBrush(Color.Salmon);

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
			Font drawFont = new Font("Verdana", 10);
			SolidBrush drawBrush = new SolidBrush(Color.Black);
			Font drawFontTitulo = new Font("Verdana", 14);

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
			objGraphic.DrawString(Alumno1, drawFont, drawBrush, drawRect2, drawFormat);
			objGraphic.DrawString(Alumno2, drawFont, drawBrush, drawRect1, drawFormat);
			objGraphic.DrawString(Alumno3, drawFont, drawBrush, drawRect3, drawFormat);
			objGraphic.DrawString("Podio de Resultados", drawFontTitulo, drawBrush, drawRectTitulo, drawFormat);

			//Definimos el tipo de fichero
			Response.ContentType = "image/png";

			string TmpPath = System.Configuration.ConfigurationManager.AppSettings["oImgPath"];
			UIUtilidades.EliminarArchivosSession(Session.SessionID);
			//Crea el directorio.
			if (!System.IO.Directory.Exists(TmpPath))
				System.IO.Directory.CreateDirectory(TmpPath);

			NombrePNG = TmpPath + "\\Podio_" + Session.SessionID + ".png";
			string ruta = Request.PhysicalApplicationPath + "\\Images\\TMP\\Podio_" + Session.SessionID + ".png";
			//Y finalmente lo guardamos
			objBitmap.Save(NombrePNG, ImageFormat.Png);
			
			File.Copy(NombrePNG, ruta);
			objBitmap.Dispose();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			imgPodio.ImageUrl = "~/Images/TMP/Podio_" + Session.SessionID + ".png";
			imgPodio.Visible = true;
			udpResultado.Update();

		}

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwResultado control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		protected void gvwResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvwResultado.PageIndex = e.NewPageIndex;

			if (!isSort) // this will get exectued if user clicks paging
			{            // before sorting istelf

				gvwResultado.DataSource = null; // I gave Datasource as null for
				gvwResultado.DataBind();     //instance. Provide your datasource
				// to bind the data
				udpResultado.Update();
			}

			else if (isAscend)// this will get exectued if user clicks paging
			// after cliclking ascending order
			{

				// I am passing only "DateRequest" as sortexpression for instance. because
				// i am implementing sorting for only one column. You can generalize it to 
				// pass that particular column on sorting.
				SortGridView("DateRequest", ASCENDING);
			}
			else // this will get exectued if user clicks paging
			// after cliclking descending order
			{
				SortGridView("DateRequest", DESCENDING);
			}
		}

		/// <summary>
		/// Handles the Sorting event of the gvwResultado control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
		protected void gvwResultado_Sorting(object sender, GridViewSortEventArgs e)
		{
			isSort = true;
			string sortExpression = e.SortExpression;
			GridSampleSortExpression = sortExpression;

			showImage = true;
			if (GridViewSortDirection == SortDirection.Ascending)
			{
				isAscend = true;
				SortGridView(sortExpression, ASCENDING);
				GridViewSortDirection = SortDirection.Descending;
			}
			else
			{
				isAscend = false;
				SortGridView(sortExpression, DESCENDING);
				GridViewSortDirection = SortDirection.Ascending;
			}
			GraficarPodioResultado();
		}

		/// <summary>
		/// Handles the RowCreated event of the gvwResultado control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
		protected void gvwResultado_RowCreated(object sender, GridViewRowEventArgs e)
		{
			//Use the RowType property to determine whether the 
			//row being created is the header row. 
			if (e.Row.RowType == DataControlRowType.Header)
			{
				// Call the GetSortColumnIndex helper method to determine
				// the index of the column being sorted.
				int sortColumnIndex = GetSortColumnIndex();

				if (sortColumnIndex != -1)
				{
					// Call the AddSortImage helper method to add
					// a sort direction image to the appropriate
					// column header. 
					AddSortImage(sortColumnIndex, e.Row);
				}
			}
		}

		// to set the height of row.
		/// <summary>
		/// Handles the RowDataBound event of the gvwResultado control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
		protected void gvwResultado_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			e.Row.Height = Unit.Pixel(24);
			e.Row.VerticalAlign = VerticalAlign.Middle;

			//Permito que solo se ordene por la última columna de la grilla - Flujo Neto
			if (e.Row.RowType == DataControlRowType.Header)
			{
				for (int i = 0; i < e.Row.Cells.Count - 1; i++) //excepto la que queremos dejar disponible.
				{
					LinkButton lb = (LinkButton)e.Row.Cells[i].Controls[0];
					string title = lb.Text;
					Label lbl = new Label();
					lbl.Text = title;
					e.Row.Cells[i].Controls.Clear();
					e.Row.Cells[i].Controls.Add(lbl);
				}
				if (lista != null)
				{
					for (int i = 1; i < e.Row.Cells.Count - 1; i++)
					{
						Label lb = (Label)e.Row.Cells[i].Controls[0];
						RptIndicadores alumno = new RptIndicadores();
						alumno = lista.Find(p => p.idAlumno == Convert.ToInt16(lb.Text));
						string nombre = alumno.alumnoApellido.Trim() + " " + alumno.alumnoNombre.Trim();
						lb.Text = nombre.Replace(" ", "<br />");
					}
				}
			}
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				string nombre = e.Row.Cells[0].Text;
				e.Row.Cells[0].Text = nombre.Replace(" ", "<br />");
			}
			//if (e.Row.RowType == DataControlRowType.Footer)
			//{
			//    e.Row.Cells[0].Text = tablaPaso3.Rows[0][0].ToString();
			//    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
			//    e.Row.Cells[0].VerticalAlign = VerticalAlign.Middle;
			//    for (int i = 1; i < e.Row.Cells.Count - 1; i++)
			//    {
			//        e.Row.Cells[i].Text = tablaPaso3.Rows[0][i].ToString();
			//        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
			//        e.Row.Cells[i].VerticalAlign = VerticalAlign.Middle;
			//    }
			//}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			CargarComboCursos();
			if (!User.IsInRole(enumRoles.Administrador.ToString()))
				btnExcel.Visible = false;
		}

		/// <summary>
		/// Cargars the combo cursos.
		/// </summary>
		private void CargarComboCursos()
		{
			int idCicloLectivo = cicloLectivoActual.idCicloLectivo;
			if (idCicloLectivo > 0)
			{
				BLCicloLectivo objBLCicloLectivo = new BLCicloLectivo();
				List<Curso> listaCursos = new List<Curso>();
				if (User.IsInRole(enumRoles.Docente.ToString()))
				{
					Asignatura objFiltro = new Asignatura();
					objFiltro.curso.cicloLectivo = cicloLectivoActual;
					listaCursos = objBLCicloLectivo.GetCursosByAsignatura(objFiltro);
				}
				else
				{
					Curso objCurso = new Curso();
					listaCursos = objBLCicloLectivo.GetCursosByCicloLectivo(idCicloLectivo);
				}
				UIUtilidades.BindCombo<Curso>(ddlCurso, listaCursos, "idCurso", "nombre", true);
				ddlCurso.Enabled = true;
			}
			else
			{
				ddlCurso.Enabled = false;
			}
		}

		/// <summary>
		/// Obteners the datos.
		/// </summary>
		/// <param name="idCursoCicloLectivo">The id curso ciclo lectivo.</param>
		/// <returns></returns>
		private string obtenerDatos(int idCursoCicloLectivo)
		{
			RptIndicadores objFiltroReporte = new RptIndicadores();
			objFiltroReporte.idCursoCicloLectivo = idCursoCicloLectivo;
			BLRptIndicadores objBLIndicadores = new BLRptIndicadores();

			lista = objBLIndicadores.GetRptIndicadores(objFiltroReporte);

			string validacion = criterioCalificacion.ValidarMétodo();
			bool datosValidos = true;
			if (criterioCalificacion.habilitarCriterio)
			{
				if (!string.IsNullOrEmpty(validacion))
				{
					datosValidos = false;
					return validacion;
				}
			}

			validacion = criterioInasistencia.ValidarMétodo();
			if (criterioInasistencia.habilitarCriterio)
			{
				if (!string.IsNullOrEmpty(validacion))
				{
					datosValidos = false;
					return validacion;
				}
			}

			validacion = criterioSancion.ValidarMétodo();
			if (criterioSancion.habilitarCriterio)
			{
				if (!string.IsNullOrEmpty(validacion))
				{
					datosValidos = false;
					return validacion;
				}
			}

			if (datosValidos) AplicarMetodo(lista);
			return string.Empty;
		}

		/// <summary>
		/// Aplicars the metodo.
		/// </summary>
		/// <param name="lista">The lista.</param>
		private void AplicarMetodo(List<RptIndicadores> lista)
		{
			try
			{
				Promethee valoresCalificacion = new Promethee(), valoresInasistencia = new Promethee(), valoresSancion = new Promethee();

				if (criterioCalificacion.habilitarCriterio)
					valoresCalificacion = criterioCalificacion.obtenerValores();
				if (criterioInasistencia.habilitarCriterio)
					valoresInasistencia = criterioInasistencia.obtenerValores();
				if (criterioSancion.habilitarCriterio)
					valoresSancion = criterioSancion.obtenerValores();

				decimal diferenciaCriterio = 0;
				decimal valorFuncPreferencia = 0;
				tablaPaso1 = new DataTable("Promethee1");
				tablaPaso1.Columns.Add("Alumnos");
				if (criterioCalificacion.habilitarCriterio)
					tablaPaso1.Columns.Add("Calificacion", System.Type.GetType("System.Decimal"));
				if (criterioInasistencia.habilitarCriterio)
					tablaPaso1.Columns.Add("Inasistencia", System.Type.GetType("System.Decimal"));
				if (criterioSancion.habilitarCriterio)
					tablaPaso1.Columns.Add("Sancion", System.Type.GetType("System.Decimal"));

				DataRow fila;
				// Paso 1: determinar como se situan las alternativas con respecto a cada atributo.
				#region --[Paso 1]--
				foreach (RptIndicadores item in lista)
				{
					List<RptIndicadores> filtro = lista.FindAll(p => p.idAlumno != item.idAlumno);

					foreach (RptIndicadores alumno in filtro)
					{
						fila = tablaPaso1.NewRow();
						fila["Alumnos"] = item.idAlumno.ToString() + "-" + alumno.idAlumno.ToString();

						#region --[Promedios]--
						if (criterioCalificacion.habilitarCriterio)
						{
							valorFuncPreferencia = -1;
							diferenciaCriterio = 0;
							if (criterioCalificacion.esMaximzante)
							{
								if (item.promedio >= alumno.promedio)
								{
									diferenciaCriterio = Math.Abs(item.promedio - alumno.promedio);
									valorFuncPreferencia = Promethee.obtenerValorFuncPreferencia(valoresCalificacion, diferenciaCriterio);
								}
							}
							else
							{
								if (item.promedio <= alumno.promedio)
								{
									diferenciaCriterio = Math.Abs(item.promedio - alumno.promedio);
									valorFuncPreferencia = Promethee.obtenerValorFuncPreferencia(valoresCalificacion, diferenciaCriterio);
								}
							}
							if (valorFuncPreferencia >= 0) fila["Calificacion"] = valorFuncPreferencia;
							else fila["Calificacion"] = DBNull.Value;
						}
						//else fila["Calificacion"] = DBNull.Value;
						#endregion

						#region --[Inasistencias]--
						if (criterioInasistencia.habilitarCriterio)
						{
							valorFuncPreferencia = -1;
							diferenciaCriterio = 0;
							if (criterioInasistencia.esMaximzante)
							{
								if (item.inasistencias >= alumno.inasistencias)
								{
									diferenciaCriterio = Math.Abs(item.inasistencias - alumno.inasistencias);
									valorFuncPreferencia = Promethee.obtenerValorFuncPreferencia(valoresInasistencia, diferenciaCriterio);
								}
							}
							else
							{
								if (item.inasistencias <= alumno.inasistencias)
								{
									diferenciaCriterio = Math.Abs(item.inasistencias - alumno.inasistencias);
									valorFuncPreferencia = Promethee.obtenerValorFuncPreferencia(valoresInasistencia, diferenciaCriterio);
								}
							}
							if (valorFuncPreferencia >= 0) fila["Inasistencia"] = valorFuncPreferencia;
							else fila["Inasistencia"] = DBNull.Value;
						}
						//else fila["Inasistencia"] = DBNull.Value;
						#endregion

						#region --[Sanciones]--
						if (criterioSancion.habilitarCriterio)
						{
							valorFuncPreferencia = -1;
							diferenciaCriterio = 0;
							if (criterioSancion.esMaximzante)
							{
								if (item.sanciones >= alumno.sanciones)
								{
									diferenciaCriterio = Math.Abs(item.sanciones - alumno.sanciones);
									valorFuncPreferencia = Promethee.obtenerValorFuncPreferencia(valoresSancion, diferenciaCriterio);
								}
							}
							else
							{
								if (item.sanciones <= alumno.sanciones)
								{
									diferenciaCriterio = Math.Abs(item.sanciones - alumno.sanciones);
									valorFuncPreferencia = Promethee.obtenerValorFuncPreferencia(valoresSancion, diferenciaCriterio);
								}
							}
							if (valorFuncPreferencia >= 0) fila["Sancion"] = valorFuncPreferencia;
							else fila["Sancion"] = DBNull.Value;
						}
						//else fila["Sancion"] = DBNull.Value;
						#endregion
						tablaPaso1.Rows.Add(fila);
					}
				}
				#endregion

				// Paso 2: Expresar la intensidad de la preferencia de la alternativa Xi comparada con Xk
				#region --[Paso 2]--
				tablaPaso2 = new DataTable("Promethee2");
				tablaPaso2.Columns.Add("Alumnos");
				DataRow nuevaFila;
				for (int i = 0; i < lista.Count; i++)
				{
					tablaPaso2.Columns.Add(lista[i].idAlumno.ToString(), System.Type.GetType("System.Decimal"));
					nuevaFila = tablaPaso2.NewRow();
					tablaPaso2.Rows.Add(nuevaFila);
					tablaPaso2.Rows[i]["Alumnos"] = lista[i].idAlumno.ToString();
				}
				tablaPaso2.Columns.Add("FlujoEntrante", System.Type.GetType("System.Decimal"));

				string[] alternativas;
				int indexFila = 0;
				int indexColumna = 0;
				decimal valorCalificacion = 0;
				decimal valorInasistencia = 0;
				decimal valorSancion = 0;
				decimal sumaPesos = 0;
				if (criterioCalificacion.habilitarCriterio) sumaPesos += valoresCalificacion.pesoCriterio;
				if (criterioInasistencia.habilitarCriterio) sumaPesos += valoresInasistencia.pesoCriterio;
				if (criterioSancion.habilitarCriterio) sumaPesos += valoresSancion.pesoCriterio;

				int nroFila;
				decimal valorAcumulado = 0;
				foreach (DataRow item in tablaPaso1.Rows)
				{
					valorAcumulado = 0;
					alternativas = item[0].ToString().Split('-');
					//item 0: fila
					//item 1: columna
					int.TryParse(alternativas[0], out indexFila);
					int.TryParse(alternativas[1], out indexColumna);
					nroFila = tablaPaso2.Rows.IndexOf(tablaPaso2.Select("Alumnos='" + indexFila.ToString() + "'")[0]);

					if (criterioCalificacion.habilitarCriterio)
					{
						valorCalificacion = (item[1] != DBNull.Value) ? Convert.ToDecimal(item[1]) : 0;
						if (criterioInasistencia.habilitarCriterio)
						{
							valorInasistencia = (item[2] != DBNull.Value) ? Convert.ToDecimal(item[2]) : 0;
							if (criterioSancion.habilitarCriterio)
								valorSancion = (item[3] != DBNull.Value) ? Convert.ToDecimal(item[3]) : 0;
						}
						else
							valorSancion = (item[2] != DBNull.Value) ? Convert.ToDecimal(item[2]) : 0;

					}
					else
					{
						if (criterioInasistencia.habilitarCriterio)
						{
							valorInasistencia = (item[1] != DBNull.Value) ? Convert.ToDecimal(item[1]) : 0;
							if (criterioSancion.habilitarCriterio)
								valorSancion = (item[2] != DBNull.Value) ? Convert.ToDecimal(item[2]) : 0;
						}
						else
							valorSancion = (item[1] != DBNull.Value) ? Convert.ToDecimal(item[1]) : 0;
					}

					if (criterioCalificacion.habilitarCriterio)
						valorAcumulado += (valorCalificacion * valoresCalificacion.pesoCriterio);
					if (criterioInasistencia.habilitarCriterio)
						valorAcumulado += (valorInasistencia * valoresInasistencia.pesoCriterio);
					if (criterioSancion.habilitarCriterio)
						valorAcumulado += (valorSancion * valoresSancion.pesoCriterio);
					tablaPaso2.Rows[nroFila][indexColumna.ToString()] = Math.Round((valorAcumulado / (sumaPesos)), 2);
				}
				#endregion

				// Paso 3: Expresar como Xi supera a las demás alternativas y cómo es superada por las otras.
				#region --[Paso 3]--
				tablaPaso3 = new DataTable("Promethee2");
				tablaPaso3 = tablaPaso2.Clone();
				tablaPaso3.Columns[0].DataType = System.Type.GetType("System.String");
				nuevaFila = tablaPaso3.NewRow();
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

				// Paso 4: Obtener el Preorden Total
				#region --[Paso 4]--
				//tablaPaso2.DefaultView.Sort = "FlujoEntrante DESC";
				tablaPaso2.Columns.Add("Ranking", System.Type.GetType("System.Decimal"));
				for (int i = 0; i < tablaPaso2.Rows.Count; i++)
				{
					tablaPaso2.Rows[i][tablaPaso2.Columns.Count - 1] = Convert.ToDecimal(tablaPaso2.Rows[i][tablaPaso2.Columns.Count - 2])
						- Convert.ToDecimal(tablaPaso3.Rows[0][i + 1]);
				}

				tablaPaso2.DefaultView.Sort = "Ranking DESC";
				tablaResultado = tablaPaso2.DefaultView.ToTable();
				#endregion

				for (int i = 0; i < tablaResultado.Rows.Count; i++)
				{
					RptIndicadores alumno = new RptIndicadores();
					alumno = lista.Find(p => p.idAlumno == Convert.ToInt16(tablaResultado.Rows[i][0].ToString()));
					tablaResultado.Rows[i][0] = alumno.alumnoApellido.Trim() + " " + alumno.alumnoNombre.Trim();
				}
				tablaResultado.Columns.Remove("FlujoEntrante");

				int idTop = 0;
				int.TryParse(ddlTop.SelectedValue, out idTop);
				if (idTop > 0)
				{
					DataTable tablaFiltro = tablaResultado.Clone();
					for (int i = 0; i < idTop; i++)
					{
						tablaFiltro.ImportRow(tablaResultado.Rows[i]);
					}
					tablaResultado.Clear();
					tablaResultado = tablaFiltro.Copy();
				}

				CargarGrilla();

				tablaPaso3.Columns.Add("Ranking", System.Type.GetType("System.Decimal"));
				tablaPaso3.Rows[0][tablaPaso3.Columns.Count - 1] = DBNull.Value;
				GraficarPodioResultado();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Cargars the grilla.
		/// </summary>
		private void CargarGrilla()
		{
			lblResultadoGrilla.Visible = true;
			lblResultado.Visible = true;

			gvwResultado.DataSource = tablaResultado.DefaultView;
			gvwResultado.DataBind();
			udpResultado.Update();
		}

		// 2) Adding a method which will return the Index of the column selected
		/// <summary>
		/// Gets the index of the sort column.
		/// </summary>
		/// <returns></returns>
		protected int GetSortColumnIndex()
		{
			// Iterate through the Columns collection to determine the index
			// of the column being sorted.
			for (int i = 0; i < tablaResultado.Columns.Count; i++)
			{
				if (tablaResultado.Columns[i].ColumnName == GridSampleSortExpression)
				{ return i; }
			}
			return -1;
		}

		//3) Adding the SortImage Method
		/// <summary>
		/// Adds the sort image.
		/// </summary>
		/// <param name="columnIndex">Index of the column.</param>
		/// <param name="HeaderRow">The header row.</param>
		protected void AddSortImage(int columnIndex, GridViewRow HeaderRow)
		{
			System.Web.UI.WebControls.Image sortImage = new System.Web.UI.WebControls.Image();

			if (showImage) // this is a boolean variable which should be false 
			{
				//  on page load so that image wont show up initially.
				if (GridViewSortDirection.ToString() == "Ascending")
				{
					sortImage.ImageUrl = "~/Images/view-sort-ascending.png";
					sortImage.AlternateText = "Orden Ascendente";
					sortImage.ToolTip = "Orden Ascendente";
				}
				else
				{
					sortImage.ImageUrl = "~/Images/view-sort-descending.png";
					sortImage.AlternateText = "Orden Descendente";
					sortImage.ToolTip = "Orden Descendente";
				}
				sortImage.ImageAlign = ImageAlign.AbsMiddle;
				HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
			}
		}

		/// <summary>
		/// Sorts the grid view.
		/// </summary>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="direction">The direction.</param>
		protected void SortGridView(string sortExpression, string direction)
		{
			DataTable dataTable = tablaResultado;
			DataRow filaPie = dataTable.NewRow();
			if (dataTable != null)
			{
				DataView dataView = new DataView(dataTable);
				dataView.Sort = sortExpression + direction;

				if (direction == ASCENDING)
					lblResultadoGrilla.Text = "Resultados obtenidos en orden Ascendente";
				else
					lblResultadoGrilla.Text = "Resultados obtenidos en orden Descendente";

				Orden = lblResultadoGrilla.Text;

				tablaResultado = dataView.ToTable();

				gvwResultado.DataSource = dataView;
				gvwResultado.DataBind();
				udpResultado.Update();
			}
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			int cantCriterios = 0;
			if (criterioCalificacion.habilitarCriterio)
				cantCriterios++;
			if (criterioInasistencia.habilitarCriterio)
				cantCriterios++;
			if (criterioSancion.habilitarCriterio)
				cantCriterios++;
			if (cantCriterios < 2)
				return "Debe habilitar al menos 2 criterios.";
			return string.Empty;
		}

		#region --[Generación de Excel]--
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
		private void GenerateData()
		{
			IFont fuenteTitulo = excelFile.CreateFont();
			fuenteTitulo.FontName = "Calibri";
			//fuenteTitulo.FontHeight = (short)FontSize.Large.GetHashCode();
			fuenteTitulo.Boldweight = (short)FontBoldWeight.BOLD.GetHashCode();

			IFont unaFuente = excelFile.CreateFont();
			unaFuente.FontName = "Tahoma";
			//unaFuente.FontHeight = (short)FontSize.Medium.GetHashCode();

			IFont fuenteEncabezado = excelFile.CreateFont();
			fuenteEncabezado.FontName = "Tahoma";
			//fuenteEncabezado.FontHeight = (short)FontSize.Medium.GetHashCode(); ;
			fuenteEncabezado.Boldweight = (short)FontBoldWeight.BOLD.GetHashCode();

			ICellStyle unEstiloDecimal = excelFile.CreateCellStyle();
			IDataFormat format = excelFile.CreateDataFormat();
			unEstiloDecimal.DataFormat = format.GetFormat("0.00");
			unEstiloDecimal.SetFont(unaFuente);

			#region --[Hoja Datos]--
			ISheet hojaUno = excelFile.CreateSheet("Datos");

			IRow filaEncabezado = hojaUno.CreateRow(1);
			filaEncabezado.CreateCell(0).SetCellValue("Criterio");
			filaEncabezado.CreateCell(1).SetCellValue("Peso");
			filaEncabezado.Cells[1].CellStyle.SetFont(fuenteTitulo);
			filaEncabezado.CreateCell(2).SetCellValue("Función de Preferencia");
			filaEncabezado.Cells[2].CellStyle.SetFont(fuenteTitulo);
			filaEncabezado.CreateCell(3).SetCellValue("Optimización");
			filaEncabezado.Cells[3].CellStyle.SetFont(fuenteTitulo);

			int idxUno = 2;
			IRow filaCriterio = hojaUno.CreateRow(idxUno);
			if (criterioCalificacion.habilitarCriterio)
			{
				filaCriterio.CreateCell(0).SetCellValue("Calificaciones");
				filaCriterio.Cells[0].CellStyle.SetFont(fuenteTitulo);
				filaCriterio.CreateCell(1).SetCellValue(Convert.ToDouble(criterioCalificacion.pesoCriterio));
				filaCriterio.Cells[1].CellStyle = unEstiloDecimal;
				filaCriterio.Cells[1].SetCellType(CellType.NUMERIC);
				filaCriterio.CreateCell(2).SetCellValue(criterioCalificacion.TipoFuncionPreferencia.ToString());
				filaCriterio.CreateCell(3).SetCellValue((criterioCalificacion.esMaximzante) ? "Maximizante" : "Minimizante");
				idxUno++;
			}

			filaCriterio = hojaUno.CreateRow(idxUno);
			if (criterioInasistencia.habilitarCriterio)
			{
				filaCriterio.CreateCell(0).SetCellValue("Inasistencias");
				filaCriterio.Cells[0].CellStyle.SetFont(fuenteTitulo);
				filaCriterio.CreateCell(1).SetCellValue(Convert.ToDouble(criterioInasistencia.pesoCriterio));
				filaCriterio.Cells[1].CellStyle = unEstiloDecimal;
				filaCriterio.Cells[1].SetCellType(CellType.NUMERIC);
				filaCriterio.CreateCell(2).SetCellValue(criterioInasistencia.TipoFuncionPreferencia.ToString());
				filaCriterio.CreateCell(3).SetCellValue((criterioInasistencia.esMaximzante) ? "Maximizante" : "Minimizante");
				idxUno++;
			}

			filaCriterio = hojaUno.CreateRow(idxUno);
			if (criterioSancion.habilitarCriterio)
			{
				filaCriterio.CreateCell(0).SetCellValue("Sanciones");
				filaCriterio.Cells[0].CellStyle.SetFont(fuenteTitulo);
				filaCriterio.CreateCell(1).SetCellValue(Convert.ToDouble(criterioSancion.pesoCriterio));
				filaCriterio.Cells[1].CellStyle = unEstiloDecimal;
				filaCriterio.Cells[1].SetCellType(CellType.NUMERIC);
				filaCriterio.CreateCell(2).SetCellValue(criterioSancion.TipoFuncionPreferencia.ToString());
				filaCriterio.CreateCell(3).SetCellValue((criterioSancion.esMaximzante) ? "Maximizante" : "Minimizante");
				idxUno++;
			}

			hojaUno.AutoSizeColumn(0);
			hojaUno.AutoSizeColumn(1);
			hojaUno.AutoSizeColumn(2);
			hojaUno.AutoSizeColumn(3);
			#endregion

			DataTable tabla = tablaPaso1.Copy();
			string[] alternativas;
			int indexFila, indexColumna;
			RptIndicadores alumno;
			string nombre = string.Empty;
			for (int i = 0; i < tabla.Rows.Count; i++)
			{
				nombre = string.Empty;
				alternativas = tabla.Rows[i][0].ToString().Split('-');
				int.TryParse(alternativas[0], out indexFila);
				int.TryParse(alternativas[1], out indexColumna);
				alumno = new RptIndicadores();

				if (indexFila > 0)
				{
					alumno = lista.Find(p => p.idAlumno == Convert.ToInt16(indexFila));
					nombre = alumno.alumnoApellido.Trim() + " " + alumno.alumnoNombre.Trim();
				}

				if (indexColumna > 0)
				{
					alumno = lista.Find(p => p.idAlumno == Convert.ToInt16(indexColumna));
					nombre += " - " + alumno.alumnoApellido.Trim() + " " + alumno.alumnoNombre.Trim();
				}
				tabla.Rows[i][0] = nombre;
			}

			CargaHojaExcel(tabla, "Resultados Parciales Paso 1", "Paso 1", false);

			tabla = tablaPaso2.Copy();
			for (int i = 0; i < tabla.Rows.Count; i++)
			{
				nombre = string.Empty;
				int.TryParse(tabla.Rows[i][0].ToString(), out indexFila);
				alumno = new RptIndicadores();

				if (indexFila > 0)
				{
					alumno = lista.Find(p => p.idAlumno == Convert.ToInt16(indexFila));
					nombre = alumno.alumnoApellido.Trim() + " " + alumno.alumnoNombre.Trim();
				}
				tabla.Rows[i][0] = nombre;
			}
			decimal acumuladorColumna;
			DataRow nuevaFila = tabla.NewRow();
			nuevaFila[0] = "Flujo Saliente";
			for (int i = 1; i < tabla.Columns.Count - 2; i++)
			{
				acumuladorColumna = 0;
				for (int j = 0; j < tabla.Rows.Count; j++)
				{
					acumuladorColumna += (tabla.Rows[j][i] != DBNull.Value) ? Convert.ToDecimal(tabla.Rows[j][i]) : 0;
				}
				nuevaFila[i] = acumuladorColumna;
			}
			//tabla.ImportRow(nuevaFila);
			tabla.Rows.Add(nuevaFila);

			CargaHojaExcel(tabla, "Resultados Parciales", "Paso 2", false);

			CargaHojaExcel(tablaResultado, "Resultados", "Resultados", true);

		}

		private void CargaHojaExcel(DataTable tablaExcel, string titulo, string nombreHoja, bool incluirGrafico)
		{
			#region --[Estilos]--
			IFont fuenteTitulo = excelFile.CreateFont();
			fuenteTitulo.FontName = "Calibri";
			//fuenteTitulo.FontHeight = (short)FontSize.Large.GetHashCode();
			fuenteTitulo.Boldweight = (short)FontBoldWeight.BOLD.GetHashCode();

			IFont unaFuente = excelFile.CreateFont();
			unaFuente.FontName = "Tahoma";
			//unaFuente.FontHeight = (short)FontSize.Medium.GetHashCode();

			IFont fuenteEncabezado = excelFile.CreateFont();
			fuenteEncabezado.FontName = "Tahoma";
			//fuenteEncabezado.FontHeight = (short)FontSize.Medium.GetHashCode(); ;
			fuenteEncabezado.Boldweight = (short)FontBoldWeight.BOLD.GetHashCode();

			ICellStyle unEstiloDecimal = excelFile.CreateCellStyle();
			IDataFormat format = excelFile.CreateDataFormat();
			unEstiloDecimal.DataFormat = format.GetFormat("0.00");
			unEstiloDecimal.SetFont(unaFuente);
			#endregion

			#region --[Hoja]--
			ISheet hojaExcel = excelFile.CreateSheet(nombreHoja);

			NPOI.SS.Util.CellRangeAddress rango = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, tablaExcel.Columns.Count - 1);
			hojaExcel.AddMergedRegion(rango);
			hojaExcel.AutoSizeColumn(0);
			hojaExcel.AutoSizeColumn(1);

			int idxAux = 0;
			IRow fila = hojaExcel.CreateRow(idxAux);
			fila.CreateCell(idxAux).SetCellValue(titulo);
			fila.Cells[idxAux].CellStyle.SetFont(fuenteTitulo);
			idxAux++;

			fila = hojaExcel.CreateRow(idxAux);
			idxAux++;
			RptIndicadores alumno = null;
			int idAlumno = 0;
			string nombre = string.Empty;

			hojaExcel.AutoSizeColumn(0);

			//--Agrego los encabezados--
			#region --[Encabezados]--
			for (int i = 0; i < tablaExcel.Columns.Count; i++)
			{
				fila.CreateCell(i).CellStyle.Alignment = HorizontalAlignment.CENTER;
				nombre = tablaExcel.Columns[i].ColumnName;
				alumno = new RptIndicadores();
				int.TryParse(tablaExcel.Columns[i].ColumnName, out idAlumno);
				if (idAlumno > 0)
				{
					alumno = lista.Find(p => p.idAlumno == Convert.ToInt16(idAlumno));
					nombre = alumno.alumnoApellido.Trim() + " " + alumno.alumnoNombre.Trim();
				}
				fila.CreateCell(i).SetCellValue(nombre);
				fila.Cells[i].CellStyle.SetFont(fuenteEncabezado);
				hojaExcel.AutoSizeColumn(i);
			}
			#endregion

			//--Agrego los datos hoja--
			#region --[Datos]--
			decimal valorDato = 0;
			int cantFilas = 0;
			try
			{
				for (int i = 0; i < tablaExcel.Rows.Count; i++)
				{
					fila = hojaExcel.CreateRow(idxAux);
					idxAux++;
					cantFilas++;
					for (int j = 0; j < tablaExcel.Columns.Count; j++)
					{
						try
						{
							valorDato = decimal.Parse(tablaExcel.Rows[i][j].ToString());
							fila.CreateCell(j).SetCellValue(Convert.ToDouble(valorDato));
							fila.Cells[j].CellStyle = unEstiloDecimal;
							fila.Cells[j].SetCellType(CellType.NUMERIC);
						}
						catch
						{
							fila.CreateCell(j).SetCellValue(tablaExcel.Rows[i][j].ToString());
							fila.Cells[j].CellStyle.SetFont(fuenteEncabezado);
						}
					}
				}
			}
			catch (Exception ex)
			{ throw ex; }
			#endregion

			//Acomodo las columnas
			for (int j = 0; j < tablaExcel.Columns.Count; j++)
			{
				hojaExcel.AutoSizeColumn(j);
			}
			hojaExcel.AutoSizeColumn(0);
			#endregion

			if (incluirGrafico)
			{
				#region --[Grafico]--
				//map the path to the img folder
				//string imagesPath = System.IO.Path.Combine(Server.MapPath("~"), "img");
				string imagesPath = System.IO.Path.GetFullPath(NombrePNG);
				//grab the image file
				//create an image from the path
				System.Drawing.Image image = System.Drawing.Image.FromFile(imagesPath);
				MemoryStream ms = new MemoryStream();
				//pull the memory stream from the image (I need this for the byte array later)
				image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				//the drawing patriarch will hold the anchor and the master information
				var patriarch = hojaExcel.CreateDrawingPatriarch();
				//store the coordinates of which cell and where in the cell the image goes
				HSSFClientAnchor anchor = new HSSFClientAnchor(20, 0, 40, 20, 3, cantFilas + 3, 4, cantFilas + 10);
				//types are 0, 2, and 3. 0 resizes within the cell, 2 doesn't
				anchor.AnchorType = 2;
				//add the byte array and encode it for the excel file
				int index = excelFile.AddPicture(ms.ToArray(), PictureType.PNG);
				var picture = patriarch.CreatePicture(anchor, index);
				picture.Resize();
				picture.LineStyle = HSSFPicture.LINESTYLE_DASHDOTGEL;
				hojaExcel.ForceFormulaRecalculation = true;
				#endregion
			}
		}

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
		#endregion

		/// <summary>
		/// Realiza las validaciones sobre los datos de la página para poder aplicar el método.
		/// </summary>
		/// <returns></returns>
		private string ValidarDatos()
		{
			int idCursoCicloLectivo = 0;
			int.TryParse(ddlCurso.SelectedValue, out idCursoCicloLectivo);
			if (idCursoCicloLectivo > 0)
			{
				string validacion = ValidarPagina();
				if (string.IsNullOrEmpty(validacion))
				{
					lblResultado.Text = string.Empty;
					AccionPagina = enumAcciones.Buscar;
					validacion = obtenerDatos(idCursoCicloLectivo);
					if (string.IsNullOrEmpty(validacion))
					{
						btnExcel.Enabled = true;
						return string.Empty;
					}
					else return validacion;
				}
				return validacion;
			}
			return "Debe seleccionar un curso.";
		}
		#endregion
	}
}