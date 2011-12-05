using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Enumeraciones;
using System.Linq;
using System.Web.UI;

namespace EDUAR_UI
{
	public partial class reportIndicadores : EDUARBasePage
	{
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
		/// Handles the Click event of the btnBuscar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnCalcular_Click(object sender, EventArgs e)
		{
			try
			{
				int idCursoCicloLectivo = 0;
				int.TryParse(ddlCurso.SelectedValue, out idCursoCicloLectivo);
				if (idCursoCicloLectivo > 0)
				{
					lblResultado.Text = string.Empty;
					AccionPagina = enumAcciones.Buscar;
					obtenerDatos(idCursoCicloLectivo);
				}
				else
					Master.MostrarMensaje("Advertencia", "Debe seleccionar un curso.", enumTipoVentanaInformacion.Advertencia);
			}
			catch (Exception ex)
			{ Master.ManageExceptions(ex); }
		}

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
			//CargarGrilla();
		}

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
		}

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
			if (e.Row.Cells[0].Text == "SuperacionSaliente")
			{
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
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			CargarComboCursos();
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
		private void obtenerDatos(int idCursoCicloLectivo)
		{
			RptIndicadores objFiltroReporte = new RptIndicadores();
			objFiltroReporte.idCursoCicloLectivo = idCursoCicloLectivo;
			BLRptIndicadores objBLIndicadores = new BLRptIndicadores();

			List<RptIndicadores> lista = objBLIndicadores.GetRptIndicadores(objFiltroReporte);

			string validacion = criterioCalificacion.ValidarMétodo();

			if (string.IsNullOrEmpty(validacion))
			{
				validacion = criterioInasistencia.ValidarMétodo();
				if (string.IsNullOrEmpty(validacion))
				{
					validacion = criterioSancion.ValidarMétodo();
					if (string.IsNullOrEmpty(validacion))
						AplicarMetodo(lista);
					else
						Master.MostrarMensaje("Criterio de Sanciones", validacion, enumTipoVentanaInformacion.Advertencia);
				}
				else
					Master.MostrarMensaje("Criterio de Inasistencias", validacion, enumTipoVentanaInformacion.Advertencia);
			}
			else
				Master.MostrarMensaje("Criterio de Calificación", validacion, enumTipoVentanaInformacion.Advertencia);
		}

		/// <summary>
		/// Aplicars the metodo.
		/// </summary>
		/// <param name="lista">The lista.</param>
		private void AplicarMetodo(List<RptIndicadores> lista)
		{
			Promethee valoresCalificacion = criterioCalificacion.obtenerValores();
			Promethee valoresInasistencia = criterioInasistencia.obtenerValores();
			Promethee valoresSancion = criterioSancion.obtenerValores();

			decimal diferenciaCriterio = 0;
			decimal valorFuncPreferencia = 0;
			DataTable tablaPaso1 = new DataTable("Promethee1");
			tablaPaso1.Columns.Add("Alternativas");
			tablaPaso1.Columns.Add("Calificacion", System.Type.GetType("System.Decimal"));
			tablaPaso1.Columns.Add("Inasistencia", System.Type.GetType("System.Decimal"));
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
					fila["Alternativas"] = item.idAlumno.ToString() + "-" + alumno.idAlumno.ToString();

					#region --[Promedios]--
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
					#endregion

					#region --[Inasistencias]--
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
					#endregion

					#region --[Sanciones]--
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
					#endregion
					//fila.AcceptChanges();
					tablaPaso1.Rows.Add(fila);
				}
			}
			#endregion

			// Paso 2: Expresar la intensidad de la preferencia de la alternativa Xi comparada con Xk
			#region --[Paso 2]--
			DataTable tablaPaso2 = new DataTable("Promethee2");
			tablaPaso2.Columns.Add("Alternativas");
			DataRow nuevaFila;
			for (int i = 0; i < lista.Count; i++)
			{
				tablaPaso2.Columns.Add(lista[i].idAlumno.ToString(), System.Type.GetType("System.Decimal"));
				nuevaFila = tablaPaso2.NewRow();
				tablaPaso2.Rows.Add(nuevaFila);
				tablaPaso2.Rows[i]["Alternativas"] = lista[i].idAlumno.ToString();
			}
			tablaPaso2.Columns.Add("SuperacionEntrante", System.Type.GetType("System.Decimal"));

			string[] alternativas;
			int indexFila = 0;
			int indexColumna = 0;
			decimal valorCalificacion = 0;
			decimal valorInasistencia = 0;
			decimal valorSancion = 0;
			decimal sumaPesos = valoresCalificacion.pesoCriterio + valoresInasistencia.pesoCriterio + valoresSancion.pesoCriterio;
			int nroFila;
			foreach (DataRow item in tablaPaso1.Rows)
			{
				alternativas = item[0].ToString().Split('-');
				//item 0: fila
				//item 1: columna
				int.TryParse(alternativas[0], out indexFila);
				int.TryParse(alternativas[1], out indexColumna);
				nroFila = tablaPaso2.Rows.IndexOf(tablaPaso2.Select("Alternativas='" + indexFila.ToString() + "'")[0]);

				valorCalificacion = (item[1] != DBNull.Value) ? Convert.ToDecimal(item[1]) : 0;
				valorInasistencia = (item[2] != DBNull.Value) ? Convert.ToDecimal(item[2]) : 0;
				valorSancion = (item[3] != DBNull.Value) ? Convert.ToDecimal(item[3]) : 0;
				tablaPaso2.Rows[nroFila][indexColumna.ToString()] = Math.Round((valorCalificacion * valoresCalificacion.pesoCriterio + valorInasistencia * valoresInasistencia.pesoCriterio + valorSancion * valoresSancion.pesoCriterio
					/ (sumaPesos)), 2);
			}
			nuevaFila = tablaPaso2.NewRow();
			nuevaFila[0] = "SuperacionSaliente";
			tablaPaso2.Rows.Add(nuevaFila);
			#endregion

			// Paso 3: Expresar como Xi supera a las demás alternativas y cómo es superada por las otras.
			#region --[Paso 3]--
			decimal acumuladorFila, acumuladorColumna;
			for (int i = 0; i < tablaPaso2.Rows.Count - 1; i++)
			{
				acumuladorFila = 0;
				acumuladorColumna = 0;

				for (int j = 1; j < tablaPaso2.Columns.Count - 1; j++)
				{
					acumuladorFila += (tablaPaso2.Rows[i][j] != DBNull.Value) ? Convert.ToDecimal(tablaPaso2.Rows[i][j]) : 0;
					acumuladorColumna += (tablaPaso2.Rows[j][i + 1] != DBNull.Value) ? Convert.ToDecimal(tablaPaso2.Rows[j][i + 1]) : 0;
				}
				tablaPaso2.Rows[i][tablaPaso2.Columns.Count - 1] = acumuladorFila;
				tablaPaso2.Rows[tablaPaso2.Rows.Count - 1][i + 1] = acumuladorColumna;
			}
			#endregion

			// Paso 4: Obtener el Preorden deseado
			#region --[Paso 4]--
			tablaPaso2.DefaultView.Sort = "SuperacionEntrante DESC";

			//tablaResultado = tablaPaso2.DefaultView.ToTable();
			#endregion

			#region --[Paso 4.1]--
			// Paso 4.1: Obtener el Preorden Total
			tablaPaso2.Columns.Add("FlujoNeto", System.Type.GetType("System.Decimal"));
			for (int i = 0; i < tablaPaso2.Rows.Count - 1; i++)
			{
				tablaPaso2.Rows[i][tablaPaso2.Columns.Count - 1] = Convert.ToDecimal(tablaPaso2.Rows[i][tablaPaso2.Columns.Count - 2])
					- Convert.ToDecimal(tablaPaso2.Rows[tablaPaso2.Rows.Count - 1][i + 1]);
			}

			tablaResultado = tablaPaso2.DefaultView.ToTable();
			#endregion

			CargarGrilla();

			#region --[Top 3 Alumnos]--
			for (int i = 0; i < 3; i++)
			{
				var alumno = from p in lista
							 where p.idAlumno == Convert.ToInt32(tablaResultado.Rows[i][0])
							 select p.idAlumno + " - " + p.alumnoApellido + " " + p.alumnoNombre;
				lblResultado.Text += alumno.ElementAt(0).ToString() + "<br />";
			}
			#endregion
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
			gvwResultado.Rows[gvwResultado.Rows.Count - 1].RowType = DataControlRowType.Footer;
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
			Image sortImage = new Image();

			if (showImage) // this is a boolean variable which should be false 
			{            //  on page load so that image wont show up initially.
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
			//bool eliminar = false;
			if (dataTable != null)
			{
				//if (dataTable.Rows[dataTable.Rows.Count - 1][0].ToString() == "SuperacionSaliente")
				//{
				//    eliminar = true;
				//    for (int i = 0; i < dataTable.Rows[dataTable.Rows.Count - 1].ItemArray.Count(); i++)
				//    {
				//        filaPie[i] = dataTable.Rows[dataTable.Rows.Count - 1].ItemArray[i];
				//    }
				//    dataTable.Rows.RemoveAt(dataTable.Rows.Count - 1);
				//}

				DataView dataView = new DataView(dataTable);
				dataView.Sort = sortExpression + direction;
				
				//if (eliminar)
				//{
				//    DataRowView newDRV = dataView.AddNew();
				//    for (int i = 0; i < filaPie.ItemArray.Count(); i++)
				//    {
				//        newDRV[i] = filaPie[i];
				//    }
				//    newDRV.EndEdit();
				//}
				//dataView.RowStateFilter = DataViewRowState.Added | DataViewRowState.ModifiedCurrent;
				gvwResultado.DataSource = dataView;
				gvwResultado.DataBind();
				udpResultado.Update();
			}
		}
		#endregion

		protected void gvwResultado_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{

		}

		protected void gvwResultado_RowDeleted(object sender, GridViewDeletedEventArgs e)
		{

		}
	}
}