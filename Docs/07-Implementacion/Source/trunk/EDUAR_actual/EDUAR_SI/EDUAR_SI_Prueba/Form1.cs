using System;
using System.Windows.Forms;
using EDUAR_SI_BusinessLogic;

namespace EDUAR_SI_Prueba
{
	public partial class Form1 : Form
	{

		public Form1()
		{
			InitializeComponent();
		}

		private void btnImportarMySQL_Click(object sender, EventArgs e)
		{
			try
			{
				btnImportarMySQL.Enabled = false;
				BLImportarDatos objImportarDatos = new BLImportarDatos(ddlCadenaConexion.SelectedItem.ToString());
				objImportarDatos.ProcedimientoImportarDatos();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnImportarMySQL.Enabled = true; }
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			ddlCadenaConexion.Items.Add(@"Data Source=SOFIA;Initial Catalog=EDUAR;Persist Security Info=True;User ID=sa;Password=tesis");
			ddlCadenaConexion.Items.Add(@"Data Source=SOFIA;Initial Catalog=EDUAR_DEV;Persist Security Info=True;User ID=sa;Password=tesis");
			ddlCadenaConexion.Items.Add(@"Data Source=alulau.redirectme.net,1433;Initial Catalog=EDUAR;Persist Security Info=True;User ID=sa;Password=tesis");
			ddlCadenaConexion.Items.Add(@"Data Source=alulau.redirectme.net,1433;Initial Catalog=EDUAR_DEV;Persist Security Info=True;User ID=sa;Password=tesis");
			ddlCadenaConexion.SelectedIndex = 1;
		}

		private void btnInformeInasitencia_Click(object sender, EventArgs e)
		{
			try
			{
				btnInformeInasistencia.Enabled = false;
				BLNotificarInasistencia objInforme = new BLNotificarInasistencia(ddlCadenaConexion.SelectedItem.ToString());
				objInforme.ProcedimientoNotificarInasistencia();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnInformeInasistencia.Enabled = true; }
		}

		private void btnInformeSanciones_Click(object sender, EventArgs e)
		{
			try
			{
				btnInformeSanciones.Enabled = false;
				BLNotificarSancion objInforme = new BLNotificarSancion(ddlCadenaConexion.SelectedItem.ToString());
				objInforme.ProcedimientoNotificarSancion();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnInformeSanciones.Enabled = true; }
		}

		private void btnAuxiliares_Click(object sender, EventArgs e)
		{
			try
			{
				btnAuxiliares.Enabled = false;
				BLImportarTablasAuxiliares objImportarDatos = new BLImportarTablasAuxiliares(ddlCadenaConexion.SelectedItem.ToString());
				objImportarDatos.ImportarPaisProvinciaLocalidad();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnAuxiliares.Enabled = true; }
		}

		private void btnAlumnos_Click(object sender, EventArgs e)
		{
			try
			{
				btnAlumnos.Enabled = false;
				BLImportarAlumnos objImportarDatos = new BLImportarAlumnos(ddlCadenaConexion.SelectedItem.ToString());
				objImportarDatos.ImportarAlumnos();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnAlumnos.Enabled = true; }
		}

		private void btnTutores_Click(object sender, EventArgs e)
		{
			try
			{
				btnTutores.Enabled = false;
				BLImportarTutores objImportarDatos = new BLImportarTutores(ddlCadenaConexion.SelectedItem.ToString());
				objImportarDatos.ImportarTutores();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnTutores.Enabled = true; }
		}

		private void btnPersonal_Click(object sender, EventArgs e)
		{
			try
			{
				btnPersonal.Enabled = false;
				BLImportarPersonal objImportarDatos = new BLImportarPersonal(ddlCadenaConexion.SelectedItem.ToString());
				objImportarDatos.ImportarPersonal();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnPersonal.Enabled = true; }
		}

		private void btnInfoAcademica_Click(object sender, EventArgs e)
		{
			try
			{
				btnInfoAcademica.Enabled = false;
				BLImportarConfiguracionAcademica objImportarDatos = new BLImportarConfiguracionAcademica(ddlCadenaConexion.SelectedItem.ToString());
				objImportarDatos.ImportarConfiguracionAcademica();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnInfoAcademica.Enabled = true; }
		}

		private void btnCalificaciones_Click(object sender, EventArgs e)
		{
			try
			{
				btnCalificaciones.Enabled = false;
				BLImportarCalificaciones objImportarDatos = new BLImportarCalificaciones(ddlCadenaConexion.SelectedItem.ToString());
				objImportarDatos.ImportarCalificaciones();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnCalificaciones.Enabled = true; }
		}

		private void btnSanciones_Click(object sender, EventArgs e)
		{
			try
			{
				btnSanciones.Enabled = false;
				BLImportarSanciones objImportarDatos = new BLImportarSanciones(ddlCadenaConexion.SelectedItem.ToString());
				objImportarDatos.ImportarSanciones();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnSanciones.Enabled = true; }
		}

		private void btnAsistencia_Click(object sender, EventArgs e)
		{
			try
			{
				btnAsistencia.Enabled = false;
				BLImportarAsistencia objImportarDatos = new BLImportarAsistencia(ddlCadenaConexion.SelectedItem.ToString());
				objImportarDatos.ImportarAsistencia();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{ btnAsistencia.Enabled = true; }
		}

        private void btnDiasHorarios_Click(object sender, EventArgs e)
        {
            try
            {
                btnDiasHorarios.Enabled = false;
                BLImportarDiasHorarios objImportarDatos = new BLImportarDiasHorarios(ddlCadenaConexion.SelectedItem.ToString());
                objImportarDatos.ImportarDiasHorarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { btnDiasHorarios.Enabled = true; }
        }

	}
}
