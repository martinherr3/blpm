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
			this.btnCalificaciones = new System.Windows.Forms.Button();
			this.btnInfoAcademica = new System.Windows.Forms.Button();
			this.btnPersonal = new System.Windows.Forms.Button();
			this.btnTutores = new System.Windows.Forms.Button();
			this.btnAuxiliares = new System.Windows.Forms.Button();
			this.btnAlumnos = new System.Windows.Forms.Button();
			this.btnImportarMySQL = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnInformeSanciones = new System.Windows.Forms.Button();
			this.btnInformeInasistencia = new System.Windows.Forms.Button();
			this.btnSanciones = new System.Windows.Forms.Button();
			this.btnAsistencia = new System.Windows.Forms.Button();
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
			this.ddlCadenaConexion.Size = new System.Drawing.Size(469, 23);
			this.ddlCadenaConexion.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnAsistencia);
			this.groupBox1.Controls.Add(this.btnSanciones);
			this.groupBox1.Controls.Add(this.btnCalificaciones);
			this.groupBox1.Controls.Add(this.btnInfoAcademica);
			this.groupBox1.Controls.Add(this.btnPersonal);
			this.groupBox1.Controls.Add(this.btnTutores);
			this.groupBox1.Controls.Add(this.btnAuxiliares);
			this.groupBox1.Controls.Add(this.btnAlumnos);
			this.groupBox1.Controls.Add(this.btnImportarMySQL);
			this.groupBox1.Location = new System.Drawing.Point(18, 71);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(468, 148);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Importación de Datos";
			// 
			// btnCalificaciones
			// 
			this.btnCalificaciones.Location = new System.Drawing.Point(351, 22);
			this.btnCalificaciones.Name = "btnCalificaciones";
			this.btnCalificaciones.Size = new System.Drawing.Size(109, 36);
			this.btnCalificaciones.TabIndex = 8;
			this.btnCalificaciones.Text = "Calificaciones";
			this.btnCalificaciones.UseVisualStyleBackColor = true;
			this.btnCalificaciones.Click += new System.EventHandler(this.btnCalificaciones_Click);
			// 
			// btnInfoAcademica
			// 
			this.btnInfoAcademica.Location = new System.Drawing.Point(236, 64);
			this.btnInfoAcademica.Name = "btnInfoAcademica";
			this.btnInfoAcademica.Size = new System.Drawing.Size(109, 36);
			this.btnInfoAcademica.TabIndex = 7;
			this.btnInfoAcademica.Text = "Info Académica";
			this.btnInfoAcademica.UseVisualStyleBackColor = true;
			this.btnInfoAcademica.Click += new System.EventHandler(this.btnInfoAcademica_Click);
			// 
			// btnPersonal
			// 
			this.btnPersonal.Location = new System.Drawing.Point(236, 22);
			this.btnPersonal.Name = "btnPersonal";
			this.btnPersonal.Size = new System.Drawing.Size(109, 36);
			this.btnPersonal.TabIndex = 6;
			this.btnPersonal.Text = "Personal";
			this.btnPersonal.UseVisualStyleBackColor = true;
			this.btnPersonal.Click += new System.EventHandler(this.btnPersonal_Click);
			// 
			// btnTutores
			// 
			this.btnTutores.Location = new System.Drawing.Point(121, 64);
			this.btnTutores.Name = "btnTutores";
			this.btnTutores.Size = new System.Drawing.Size(109, 36);
			this.btnTutores.TabIndex = 5;
			this.btnTutores.Text = "Tutores";
			this.btnTutores.UseVisualStyleBackColor = true;
			this.btnTutores.Click += new System.EventHandler(this.btnTutores_Click);
			// 
			// btnAuxiliares
			// 
			this.btnAuxiliares.Location = new System.Drawing.Point(6, 64);
			this.btnAuxiliares.Name = "btnAuxiliares";
			this.btnAuxiliares.Size = new System.Drawing.Size(109, 36);
			this.btnAuxiliares.TabIndex = 4;
			this.btnAuxiliares.Text = "Auxiliares";
			this.btnAuxiliares.UseVisualStyleBackColor = true;
			this.btnAuxiliares.Click += new System.EventHandler(this.btnAuxiliares_Click);
			// 
			// btnAlumnos
			// 
			this.btnAlumnos.Location = new System.Drawing.Point(121, 22);
			this.btnAlumnos.Name = "btnAlumnos";
			this.btnAlumnos.Size = new System.Drawing.Size(109, 36);
			this.btnAlumnos.TabIndex = 3;
			this.btnAlumnos.Text = "Alumnos";
			this.btnAlumnos.UseVisualStyleBackColor = true;
			this.btnAlumnos.Click += new System.EventHandler(this.btnAlumnos_Click);
			// 
			// btnImportarMySQL
			// 
			this.btnImportarMySQL.Enabled = false;
			this.btnImportarMySQL.Location = new System.Drawing.Point(6, 22);
			this.btnImportarMySQL.Name = "btnImportarMySQL";
			this.btnImportarMySQL.Size = new System.Drawing.Size(109, 36);
			this.btnImportarMySQL.TabIndex = 1;
			this.btnImportarMySQL.Text = "Importar My SQL";
			this.btnImportarMySQL.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnInformeSanciones);
			this.groupBox2.Controls.Add(this.btnInformeInasistencia);
			this.groupBox2.Location = new System.Drawing.Point(18, 253);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(468, 105);
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
			// btnSanciones
			// 
			this.btnSanciones.Location = new System.Drawing.Point(351, 64);
			this.btnSanciones.Name = "btnSanciones";
			this.btnSanciones.Size = new System.Drawing.Size(109, 36);
			this.btnSanciones.TabIndex = 9;
			this.btnSanciones.Text = "Sanciones";
			this.btnSanciones.UseVisualStyleBackColor = true;
			this.btnSanciones.Click += new System.EventHandler(this.btnSanciones_Click);
			// 
			// btnAsistencia
			// 
			this.btnAsistencia.Location = new System.Drawing.Point(351, 106);
			this.btnAsistencia.Name = "btnAsistencia";
			this.btnAsistencia.Size = new System.Drawing.Size(109, 36);
			this.btnAsistencia.TabIndex = 10;
			this.btnAsistencia.Text = "Asistencia";
			this.btnAsistencia.UseVisualStyleBackColor = true;
			this.btnAsistencia.Click += new System.EventHandler(this.btnAsistencia_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(557, 390);
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
		private System.Windows.Forms.Button btnImportarMySQL;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnInformeSanciones;
		private System.Windows.Forms.Button btnInformeInasistencia;
		private System.Windows.Forms.Button btnAlumnos;
		private System.Windows.Forms.Button btnAuxiliares;
		private System.Windows.Forms.Button btnTutores;
		private System.Windows.Forms.Button btnPersonal;
		private System.Windows.Forms.Button btnInfoAcademica;
		private System.Windows.Forms.Button btnCalificaciones;
		private System.Windows.Forms.Button btnSanciones;
		private System.Windows.Forms.Button btnAsistencia;
    }
}

