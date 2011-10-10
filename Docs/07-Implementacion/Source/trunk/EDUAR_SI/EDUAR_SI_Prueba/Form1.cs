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
            ddlCadenaConexion.Items.Add(@"Data Source=alulau.redirectme.net,1433;Initial Catalog=EDUAR;Persist Security Info=True;User ID=sa;Password=tesis");
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

		private void btnPaisProvinciaLocalidad_Click(object sender, EventArgs e)
		{
			try
            {
                btnPaisProvinciaLocalidad.Enabled = false;
                BLImportarPaisProvLoc objImportarDatos = new BLImportarPaisProvLoc(ddlCadenaConexion.SelectedItem.ToString());
                objImportarDatos.ImportarPaisProvinciaLocalidad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { btnPaisProvinciaLocalidad.Enabled = true; }
		}
    }
}
