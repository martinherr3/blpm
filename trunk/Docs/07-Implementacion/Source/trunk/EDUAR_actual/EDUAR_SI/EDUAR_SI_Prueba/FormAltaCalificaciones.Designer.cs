namespace EDUAR_SI_Prueba
{
    partial class FormAltaCalificaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.ddlCursos = new System.Windows.Forms.ComboBox();
            this.lblCurso = new System.Windows.Forms.Label();
            this.dgvAlumnos = new System.Windows.Forms.DataGridView();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnRegistrarCalificacion = new System.Windows.Forms.Button();
            this.lblAlumnos = new System.Windows.Forms.Label();
            this.ddlAsignatura = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlumnos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(321, 24);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 24;
            this.lblFecha.Text = "Fecha";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(324, 42);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha.TabIndex = 23;
            // 
            // ddlCursos
            // 
            this.ddlCursos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCursos.FormattingEnabled = true;
            this.ddlCursos.Location = new System.Drawing.Point(25, 42);
            this.ddlCursos.Name = "ddlCursos";
            this.ddlCursos.Size = new System.Drawing.Size(241, 21);
            this.ddlCursos.TabIndex = 22;
            this.ddlCursos.SelectedIndexChanged += new System.EventHandler(this.ddlCurso_SelectedIndexChanged);
            // 
            // lblCurso
            // 
            this.lblCurso.AutoSize = true;
            this.lblCurso.Location = new System.Drawing.Point(23, 24);
            this.lblCurso.Name = "lblCurso";
            this.lblCurso.Size = new System.Drawing.Size(34, 13);
            this.lblCurso.TabIndex = 21;
            this.lblCurso.Text = "Curso";
            // 
            // dgvAlumnos
            // 
            this.dgvAlumnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlumnos.Location = new System.Drawing.Point(26, 141);
            this.dgvAlumnos.Name = "dgvAlumnos";
            this.dgvAlumnos.Size = new System.Drawing.Size(546, 205);
            this.dgvAlumnos.TabIndex = 27;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(438, 369);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(145, 36);
            this.btnSalir.TabIndex = 26;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnRegistrarCalificacion
            // 
            this.btnRegistrarCalificacion.Location = new System.Drawing.Point(26, 369);
            this.btnRegistrarCalificacion.Name = "btnRegistrarCalificacion";
            this.btnRegistrarCalificacion.Size = new System.Drawing.Size(145, 36);
            this.btnRegistrarCalificacion.TabIndex = 25;
            this.btnRegistrarCalificacion.Text = "Registrar Calificaciones";
            this.btnRegistrarCalificacion.UseVisualStyleBackColor = true;
            this.btnRegistrarCalificacion.Click += new System.EventHandler(this.btnRegistrarCalificacion_Click);
            // 
            // lblAlumnos
            // 
            this.lblAlumnos.AutoSize = true;
            this.lblAlumnos.Location = new System.Drawing.Point(23, 117);
            this.lblAlumnos.Name = "lblAlumnos";
            this.lblAlumnos.Size = new System.Drawing.Size(47, 13);
            this.lblAlumnos.TabIndex = 28;
            this.lblAlumnos.Text = "Alumnos";
            // 
            // ddlAsignatura
            // 
            this.ddlAsignatura.FormattingEnabled = true;
            this.ddlAsignatura.Location = new System.Drawing.Point(89, 84);
            this.ddlAsignatura.Name = "ddlAsignatura";
            this.ddlAsignatura.Size = new System.Drawing.Size(435, 21);
            this.ddlAsignatura.TabIndex = 29;
            this.ddlAsignatura.SelectedIndexChanged += new System.EventHandler(this.ddlAsignatura_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Asignatura";
            // 
            // FormAltaCalificaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 434);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddlAsignatura);
            this.Controls.Add(this.lblAlumnos);
            this.Controls.Add(this.dgvAlumnos);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnRegistrarCalificacion);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.ddlCursos);
            this.Controls.Add(this.lblCurso);
            this.Name = "FormAltaCalificaciones";
            this.Text = "Registrar Calificaciones";
            this.Load += new System.EventHandler(this.FormAltaCalificaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlumnos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ComboBox ddlCursos;
        private System.Windows.Forms.Label lblCurso;
        private System.Windows.Forms.DataGridView dgvAlumnos;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnRegistrarCalificacion;
        private System.Windows.Forms.Label lblAlumnos;
        private System.Windows.Forms.ComboBox ddlAsignatura;
        private System.Windows.Forms.Label label1;
    }
}