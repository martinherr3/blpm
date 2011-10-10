namespace EDUAR_SI_Prueba
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.label1 = new System.Windows.Forms.Label();
			this.ddlCadenaConexion = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnImportarMySQL = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnInformeSanciones = new System.Windows.Forms.Button();
			this.btnInformeInasistencia = new System.Windows.Forms.Button();
			this.btnPaisProvinciaLocalidad = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Cadena de Conexión";
			// 
			// ddlCadenaConexion
			// 
			this.ddlCadenaConexion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlCadenaConexion.FormattingEnabled = true;
			this.ddlCadenaConexion.Location = new System.Drawing.Point(17, 33);
			this.ddlCadenaConexion.Name = "ddlCadenaConexion";
			this.ddlCadenaConexion.Size = new System.Drawing.Size(447, 23);
			this.ddlCadenaConexion.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnPaisProvinciaLocalidad);
			this.groupBox1.Controls.Add(this.btnImportarMySQL);
			this.groupBox1.Location = new System.Drawing.Point(18, 71);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(446, 97);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Importación de Datos";
			// 
			// btnImportarMySQL
			// 
			this.btnImportarMySQL.Location = new System.Drawing.Point(6, 22);
			this.btnImportarMySQL.Name = "btnImportarMySQL";
			this.btnImportarMySQL.Size = new System.Drawing.Size(145, 36);
			this.btnImportarMySQL.TabIndex = 1;
			this.btnImportarMySQL.Text = "Importar My SQL";
			this.btnImportarMySQL.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnInformeSanciones);
			this.groupBox2.Controls.Add(this.btnInformeInasistencia);
			this.groupBox2.Location = new System.Drawing.Point(18, 185);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(446, 105);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Informes";
			// 
			// btnInformeSanciones
			// 
			this.btnInformeSanciones.Location = new System.Drawing.Point(161, 22);
			this.btnInformeSanciones.Name = "btnInformeSanciones";
			this.btnInformeSanciones.Size = new System.Drawing.Size(145, 36);
			this.btnInformeSanciones.TabIndex = 6;
			this.btnInformeSanciones.Text = "Informe Sanciones";
			this.btnInformeSanciones.UseVisualStyleBackColor = true;
			// 
			// btnInformeInasistencia
			// 
			this.btnInformeInasistencia.Location = new System.Drawing.Point(10, 22);
			this.btnInformeInasistencia.Name = "btnInformeInasistencia";
			this.btnInformeInasistencia.Size = new System.Drawing.Size(145, 36);
			this.btnInformeInasistencia.TabIndex = 5;
			this.btnInformeInasistencia.Text = "Informe Inasistencia";
			this.btnInformeInasistencia.UseVisualStyleBackColor = true;
			// 
			// btnPaisProvinciaLocalidad
			// 
			this.btnPaisProvinciaLocalidad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnPaisProvinciaLocalidad.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPaisProvinciaLocalidad.Location = new System.Drawing.Point(161, 22);
			this.btnPaisProvinciaLocalidad.Name = "btnPaisProvinciaLocalidad";
			this.btnPaisProvinciaLocalidad.Size = new System.Drawing.Size(156, 36);
			this.btnPaisProvinciaLocalidad.TabIndex = 2;
			this.btnPaisProvinciaLocalidad.Text = "Pais Provincia Localidad";
			this.btnPaisProvinciaLocalidad.UseVisualStyleBackColor = true;
			this.btnPaisProvinciaLocalidad.Click += new System.EventHandler(this.btnPaisProvinciaLocalidad_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(557, 313);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ddlCadenaConexion);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox ddlCadenaConexion;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnPaisProvinciaLocalidad;
		private System.Windows.Forms.Button btnImportarMySQL;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnInformeSanciones;
		private System.Windows.Forms.Button btnInformeInasistencia;
    }
}

