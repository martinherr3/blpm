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

namespace EDUAR_UI
{
	public partial class reportIndicadores : EDUARBasePage
	{
		#region --[Propiedades]--
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
			CargarGrilla();
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
			AplicarMetodo(lista);
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
			decimal? valorSigma = 0;
			DataTable tablaPaso1 = new DataTable("Promethee1");
			tablaPaso1.Columns.Add("Alternativas");
			tablaPaso1.Columns.Add("Calificacion");
			tablaPaso1.Columns.Add("Inasistencia");
			tablaPaso1.Columns.Add("Sancion");

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
					valorSigma = null;
					diferenciaCriterio = 0;
					if (criterioCalificacion.esMaximzante)
					{
						if (item.promedio >= alumno.promedio)
						{
							diferenciaCriterio = Math.Abs(item.promedio - alumno.promedio);
							valorSigma = Promethee.obtenerSigma(valoresCalificacion, diferenciaCriterio);
						}
					}
					else
					{
						if (item.promedio <= alumno.promedio)
						{
							diferenciaCriterio = Math.Abs(item.promedio - alumno.promedio);
							valorSigma = Promethee.obtenerSigma(valoresCalificacion, diferenciaCriterio);
						}
					}
					fila["Calificacion"] = valorSigma;
					#endregion

					#region --[Inasistencias]--
					valorSigma = null;
					diferenciaCriterio = 0;
					if (criterioInasistencia.esMaximzante)
					{
						if (item.inasistencias >= alumno.inasistencias)
						{
							diferenciaCriterio = Math.Abs(item.inasistencias - alumno.inasistencias);
							valorSigma = Promethee.obtenerSigma(valoresInasistencia, diferenciaCriterio);
						}
					}
					else
					{
						if (item.inasistencias <= alumno.inasistencias)
						{
							diferenciaCriterio = Math.Abs(item.inasistencias - alumno.inasistencias);
							valorSigma = Promethee.obtenerSigma(valoresInasistencia, diferenciaCriterio);
						}
					}
					fila["Inasistencia"] = valorSigma;
					#endregion

					#region --[Sanciones]--
					valorSigma = null;
					diferenciaCriterio = 0;
					if (criterioSancion.esMaximzante)
					{
						if (item.sanciones >= alumno.sanciones)
						{
							diferenciaCriterio = Math.Abs(item.sanciones - alumno.sanciones);
							valorSigma = Promethee.obtenerSigma(valoresSancion, diferenciaCriterio);
						}
					}
					else
					{
						if (item.sanciones <= alumno.sanciones)
						{
							diferenciaCriterio = Math.Abs(item.sanciones - alumno.sanciones);
							valorSigma = Promethee.obtenerSigma(valoresSancion, diferenciaCriterio);
						}
					}
					fila["Sancion"] = valorSigma;
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
				tablaPaso2.Columns.Add(lista[i].idAlumno.ToString());
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
			foreach (DataRow item in tablaPaso1.Rows)
			{
				alternativas = item[0].ToString().Split('-');
				//item 0: fila
				//item 1: columna
				int.TryParse(alternativas[0], out indexFila);
				int.TryParse(alternativas[1], out indexColumna);

				int nroFila = tablaPaso2.Rows.IndexOf(tablaPaso2.Select("Alternativas=" + indexFila.ToString())[0]);

				valorCalificacion = (item[1] != DBNull.Value) ? Convert.ToDecimal(item[1]) : 0;
				valorInasistencia = (item[2] != DBNull.Value) ? Convert.ToDecimal(item[2]) : 0;
				valorSancion = (item[3] != DBNull.Value) ? Convert.ToDecimal(item[3]) : 0;
				tablaPaso2.Rows[nroFila][indexColumna.ToString()] = Math.Round((valorCalificacion * valoresCalificacion.pesoCriterio + valorInasistencia * valoresInasistencia.pesoCriterio + valorSancion * valoresSancion.pesoCriterio
					/ (sumaPesos)), 4);
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
				try
				{
					for (int j = 1; j < tablaPaso2.Columns.Count - 1; j++)
					{
						acumuladorFila += (tablaPaso2.Rows[i][j] != DBNull.Value) ? Convert.ToDecimal(tablaPaso2.Rows[i][j]) : 0;
						acumuladorColumna += (tablaPaso2.Rows[j][i + 1] != DBNull.Value) ? Convert.ToDecimal(tablaPaso2.Rows[j][i + 1]) : 0;
					}
					tablaPaso2.Rows[i][tablaPaso2.Columns.Count - 1] = acumuladorFila;
					tablaPaso2.Rows[tablaPaso2.Rows.Count - 1][i + 1] = acumuladorColumna;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			#endregion

			// Paso 4: Obtener el Preorden deseado
			#region --[Paso 4]--
			tablaPaso2.DefaultView.Sort = "SuperacionEntrante DESC";

			tablaResultado = tablaPaso2.DefaultView.ToTable();

			//DataTable tablaPaso5 = GenerateTransposedTable(tablaPaso2);

			CargarGrilla();

			#endregion

		}

		private DataTable GenerateTransposedTable(DataTable inputTable)
		{
			DataTable outputTable = new DataTable();

			// Add columns by looping rows

			// Header row's first column is same as in inputTable
			outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

			// Header row's second column onwards, 'inputTable's first column taken
			foreach (DataRow inRow in inputTable.Rows)
			{
				string newColName = inRow[0].ToString();
				outputTable.Columns.Add(newColName);
			}

			// Add rows by looping columns        
			for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
			{
				DataRow newRow = outputTable.NewRow();

				// First column is inputTable's Header row's second column
				newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
				for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
				{
					string colValue = inputTable.Rows[cCount][rCount].ToString();
					newRow[cCount + 1] = colValue;
				}
				outputTable.Rows.Add(newRow);
			}

			return outputTable;
		}

		private void CargarGrilla()
		{
			lblResultado.Visible = true;
			gvwResultado.DataSource = tablaResultado.DefaultView;
			gvwResultado.DataBind();
			udpResultado.Update();
		}
		#endregion
	}
}