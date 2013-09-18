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
            this.btnBajaUsuarios = new System.Windows.Forms.Button();
            this.btnDiasHorarios = new System.Windows.Forms.Button();
            this.btnCalificaciones = new System.Windows.Forms.Button();
            this.btnSanciones = new System.Windows.Forms.Button();
            this.btnInfoAcademica = new System.Windows.Forms.Button();
            this.btnTutores = new System.Windows.Forms.Button();
            this.btnAuxiliares = new System.Windows.Forms.Button();
            this.btnAlumnos = new System.Windows.Forms.Button();
            this.btnAsistencia = new System.Windows.Forms.Button();
            this.btnPersonal = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSancionInasistenciaSMS = new System.Windows.Forms.Button();
            this.btnInformeSanciones = new System.Windows.Forms.Button();
            this.btnInformeInasistencia = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAltaSanciones = new System.Windows.Forms.Button();
            this.btnAltaInasistencias = new System.Windows.Forms.Button();
            this.btnAltaCalificaciones = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.btnBajaUsuarios);
            this.groupBox1.Controls.Add(this.btnDiasHorarios);
            this.groupBox1.Controls.Add(this.btnCalificaciones);
            this.groupBox1.Controls.Add(this.btnSanciones);
            this.groupBox1.Controls.Add(this.btnInfoAcademica);
            this.groupBox1.Controls.Add(this.btnTutores);
            this.groupBox1.Controls.Add(this.btnAuxiliares);
            this.groupBox1.Controls.Add(this.btnAlumnos);
            this.groupBox1.Location = new System.Drawing.Point(18, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 148);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Importación de Datos";
            // 
            // btnBajaUsuarios
            // 
            this.btnBajaUsuarios.Location = new System.Drawing.Point(145, 106);
            this.btnBajaUsuarios.Name = "btnBajaUsuarios";
            this.btnBajaUsuarios.Size = new System.Drawing.Size(129, 36);
            this.btnBajaUsuarios.TabIndex = 12;
            this.btnBajaUsuarios.Text = "10. Baja Usuarios";
            this.btnBajaUsuarios.UseVisualStyleBackColor = true;
            this.btnBajaUsuarios.Click += new System.EventHandler(this.btnBajaUsuarios_Click);
            // 
            // btnDiasHorarios
            // 
            this.btnDiasHorarios.Location = new System.Drawing.Point(145, 64);
            this.btnDiasHorarios.Name = "btnDiasHorarios";
            this.btnDiasHorarios.Size = new System.Drawing.Size(129, 36);
            this.btnDiasHorarios.TabIndex = 11;
            this.btnDiasHorarios.Text = "6. DiasHorario";
            this.btnDiasHorarios.UseVisualStyleBackColor = true;
            this.btnDiasHorarios.Click += new System.EventHandler(this.btnDiasHorarios_Click);
            // 
            // btnCalificaciones
            // 
            this.btnCalificaciones.Location = new System.Drawing.Point(280, 64);
            this.btnCalificaciones.Name = "btnCalificaciones";
            this.btnCalificaciones.Size = new System.Drawing.Size(129, 36);
            this.btnCalificaciones.TabIndex = 8;
            this.btnCalificaciones.Text = "7. Calificaciones";
            this.btnCalificaciones.UseVisualStyleBackColor = true;
            this.btnCalificaciones.Click += new System.EventHandler(this.btnCalificaciones_Click);
            // 
            // btnSanciones
            // 
            this.btnSanciones.Location = new System.Drawing.Point(10, 106);
            this.btnSanciones.Name = "btnSanciones";
            this.btnSanciones.Size = new System.Drawing.Size(129, 36);
            this.btnSanciones.TabIndex = 9;
            this.btnSanciones.Text = "9. Sanciones";
            this.btnSanciones.UseVisualStyleBackColor = true;
            this.btnSanciones.Click += new System.EventHandler(this.btnSanciones_Click);
            // 
            // btnInfoAcademica
            // 
            this.btnInfoAcademica.Location = new System.Drawing.Point(10, 64);
            this.btnInfoAcademica.Name = "btnInfoAcademica";
            this.btnInfoAcademica.Size = new System.Drawing.Size(129, 36);
            this.btnInfoAcademica.TabIndex = 7;
            this.btnInfoAcademica.Text = "5. Info Académica";
            this.btnInfoAcademica.UseVisualStyleBackColor = true;
            this.btnInfoAcademica.Click += new System.EventHandler(this.btnInfoAcademica_Click);
            // 
            // btnTutores
            // 
            this.btnTutores.Location = new System.Drawing.Point(280, 22);
            this.btnTutores.Name = "btnTutores";
            this.btnTutores.Size = new System.Drawing.Size(129, 36);
            this.btnTutores.TabIndex = 5;
            this.btnTutores.Text = "3. Tutores";
            this.btnTutores.UseVisualStyleBackColor = true;
            this.btnTutores.Click += new System.EventHandler(this.btnTutores_Click);
            // 
            // btnAuxiliares
            // 
            this.btnAuxiliares.Location = new System.Drawing.Point(10, 22);
            this.btnAuxiliares.Name = "btnAuxiliares";
            this.btnAuxiliares.Size = new System.Drawing.Size(129, 36);
            this.btnAuxiliares.TabIndex = 4;
            this.btnAuxiliares.Text = "1. Auxiliares";
            this.btnAuxiliares.UseVisualStyleBackColor = true;
            this.btnAuxiliares.Click += new System.EventHandler(this.btnAuxiliares_Click);
            // 
            // btnAlumnos
            // 
            this.btnAlumnos.Location = new System.Drawing.Point(145, 22);
            this.btnAlumnos.Name = "btnAlumnos";
            this.btnAlumnos.Size = new System.Drawing.Size(129, 36);
            this.btnAlumnos.TabIndex = 3;
            this.btnAlumnos.Text = "2. Alumnos";
            this.btnAlumnos.UseVisualStyleBackColor = true;
            this.btnAlumnos.Click += new System.EventHandler(this.btnAlumnos_Click);
            // 
            // btnAsistencia
            // 
            this.btnAsistencia.Location = new System.Drawing.Point(433, 135);
            this.btnAsistencia.Name = "btnAsistencia";
            this.btnAsistencia.Size = new System.Drawing.Size(129, 36);
            this.btnAsistencia.TabIndex = 10;
            this.btnAsistencia.Text = "8. Asistencia";
            this.btnAsistencia.UseVisualStyleBackColor = true;
            this.btnAsistencia.Click += new System.EventHandler(this.btnAsistencia_Click);
            // 
            // btnPersonal
            // 
            this.btnPersonal.Location = new System.Drawing.Point(433, 93);
            this.btnPersonal.Name = "btnPersonal";
            this.btnPersonal.Size = new System.Drawing.Size(129, 36);
            this.btnPersonal.TabIndex = 6;
            this.btnPersonal.Text = "4. Personal";
            this.btnPersonal.UseVisualStyleBackColor = true;
            this.btnPersonal.Click += new System.EventHandler(this.btnPersonal_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSancionInasistenciaSMS);
            this.groupBox2.Controls.Add(this.btnInformeSanciones);
            this.groupBox2.Controls.Add(this.btnInformeInasistencia);
            this.groupBox2.Location = new System.Drawing.Point(18, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(557, 105);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informes";
            // 
            // btnSancionInasistenciaSMS
            // 
            this.btnSancionInasistenciaSMS.Location = new System.Drawing.Point(10, 63);
            this.btnSancionInasistenciaSMS.Name = "btnSancionInasistenciaSMS";
            this.btnSancionInasistenciaSMS.Size = new System.Drawing.Size(316, 36);
            this.btnSancionInasistenciaSMS.TabIndex = 7;
            this.btnSancionInasistenciaSMS.Text = "Informe Sanciones - Inasistencia SMS";
            this.btnSancionInasistenciaSMS.UseVisualStyleBackColor = true;
            this.btnSancionInasistenciaSMS.Click += new System.EventHandler(this.btnSancionInasistenciaSMS_Click);
            // 
            // btnInformeSanciones
            // 
            this.btnInformeSanciones.Location = new System.Drawing.Point(181, 22);
            this.btnInformeSanciones.Name = "btnInformeSanciones";
            this.btnInformeSanciones.Size = new System.Drawing.Size(145, 36);
            this.btnInformeSanciones.TabIndex = 6;
            this.btnInformeSanciones.Text = "Informe Sanciones";
            this.btnInformeSanciones.UseVisualStyleBackColor = true;
            // 
            // btnInformeInasistencia
            // 
            this.btnInformeInasistencia.Location = new System.Drawing.Point(10, 21);
            this.btnInformeInasistencia.Name = "btnInformeInasistencia";
            this.btnInformeInasistencia.Size = new System.Drawing.Size(165, 36);
            this.btnInformeInasistencia.TabIndex = 5;
            this.btnInformeInasistencia.Text = "Informe Inasistencia";
            this.btnInformeInasistencia.UseVisualStyleBackColor = true;
            this.btnInformeInasistencia.Click += new System.EventHandler(this.btnInformeInasistencia_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAltaCalificaciones);
            this.groupBox3.Controls.Add(this.btnAltaSanciones);
            this.groupBox3.Controls.Add(this.btnAltaInasistencias);
            this.groupBox3.Location = new System.Drawing.Point(17, 347);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(558, 163);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Carga de Notificaciones";
            // 
            // btnAltaSanciones
            // 
            this.btnAltaSanciones.Location = new System.Drawing.Point(267, 43);
            this.btnAltaSanciones.Name = "btnAltaSanciones";
            this.btnAltaSanciones.Size = new System.Drawing.Size(250, 41);
            this.btnAltaSanciones.TabIndex = 1;
            this.btnAltaSanciones.Text = "Alta Sanciones";
            this.btnAltaSanciones.UseVisualStyleBackColor = true;
            this.btnAltaSanciones.Click += new System.EventHandler(this.btnAltaSanciones_Click);
            // 
            // btnAltaInasistencias
            // 
            this.btnAltaInasistencias.Location = new System.Drawing.Point(11, 43);
            this.btnAltaInasistencias.Name = "btnAltaInasistencias";
            this.btnAltaInasistencias.Size = new System.Drawing.Size(250, 41);
            this.btnAltaInasistencias.TabIndex = 0;
            this.btnAltaInasistencias.Text = "Alta Inasistencias";
            this.btnAltaInasistencias.UseVisualStyleBackColor = true;
            this.btnAltaInasistencias.Click += new System.EventHandler(this.btnAltaInasistencias_Click);
            // 
            // btnAltaCalificaciones
            // 
            this.btnAltaCalificaciones.Location = new System.Drawing.Point(11, 105);
            this.btnAltaCalificaciones.Name = "btnAltaCalificaciones";
            this.btnAltaCalificaciones.Size = new System.Drawing.Size(250, 41);
            this.btnAltaCalificaciones.TabIndex = 2;
            this.btnAltaCalificaciones.Text = "Alta Calificaciones";
            this.btnAltaCalificaciones.UseVisualStyleBackColor = true;
            this.btnAltaCalificaciones.Click += new System.EventHandler(this.btnAltaCalificaciones_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 522);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnAsistencia);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnPersonal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ddlCadenaConexion);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnDiasHorarios;
        private System.Windows.Forms.Button btnSancionInasistenciaSMS;
        private System.Windows.Forms.Button btnBajaUsuarios;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAltaSanciones;
        private System.Windows.Forms.Button btnAltaInasistencias;
        private System.Windows.Forms.Button btnAltaCalificaciones;
    }
}

