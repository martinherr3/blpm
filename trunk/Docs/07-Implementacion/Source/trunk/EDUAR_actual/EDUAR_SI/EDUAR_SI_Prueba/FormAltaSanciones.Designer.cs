namespace EDUAR_SI_Prueba
{
    partial class FormAltaSanciones
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
            this.ddlCantidadSanciones = new System.Windows.Forms.ComboBox();
            this.ddlTiposSancion = new System.Windows.Forms.ComboBox();
            this.lblTipoSancion = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnRegistrarSancion = new System.Windows.Forms.Button();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.ddlMotivosSancion = new System.Windows.Forms.ComboBox();
            this.lblMotivoSancion = new System.Windows.Forms.Label();
            this.ddlAlumnos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlCursos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(288, 145);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 32;
            this.lblFecha.Text = "Fecha";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(291, 163);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha.TabIndex = 31;
            // 
            // ddlCantidadSanciones
            // 
            this.ddlCantidadSanciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCantidadSanciones.FormattingEnabled = true;
            this.ddlCantidadSanciones.Location = new System.Drawing.Point(29, 163);
            this.ddlCantidadSanciones.Name = "ddlCantidadSanciones";
            this.ddlCantidadSanciones.Size = new System.Drawing.Size(53, 21);
            this.ddlCantidadSanciones.TabIndex = 30;
            // 
            // ddlTiposSancion
            // 
            this.ddlTiposSancion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlTiposSancion.FormattingEnabled = true;
            this.ddlTiposSancion.Location = new System.Drawing.Point(291, 106);
            this.ddlTiposSancion.Name = "ddlTiposSancion";
            this.ddlTiposSancion.Size = new System.Drawing.Size(207, 21);
            this.ddlTiposSancion.TabIndex = 29;
            // 
            // lblTipoSancion
            // 
            this.lblTipoSancion.AutoSize = true;
            this.lblTipoSancion.Location = new System.Drawing.Point(288, 88);
            this.lblTipoSancion.Name = "lblTipoSancion";
            this.lblTipoSancion.Size = new System.Drawing.Size(70, 13);
            this.lblTipoSancion.TabIndex = 28;
            this.lblTipoSancion.Text = "Tipo Sanción";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(372, 211);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(145, 36);
            this.btnSalir.TabIndex = 27;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnRegistrarSancion
            // 
            this.btnRegistrarSancion.Location = new System.Drawing.Point(28, 211);
            this.btnRegistrarSancion.Name = "btnRegistrarSancion";
            this.btnRegistrarSancion.Size = new System.Drawing.Size(145, 36);
            this.btnRegistrarSancion.TabIndex = 21;
            this.btnRegistrarSancion.Text = "Registrar Sanción";
            this.btnRegistrarSancion.UseVisualStyleBackColor = true;
            this.btnRegistrarSancion.Click += new System.EventHandler(this.btnRegistrarSancion_Click);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(26, 145);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(49, 13);
            this.lblCantidad.TabIndex = 26;
            this.lblCantidad.Text = "Cantidad";
            // 
            // ddlMotivosSancion
            // 
            this.ddlMotivosSancion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMotivosSancion.FormattingEnabled = true;
            this.ddlMotivosSancion.Location = new System.Drawing.Point(29, 106);
            this.ddlMotivosSancion.Name = "ddlMotivosSancion";
            this.ddlMotivosSancion.Size = new System.Drawing.Size(207, 21);
            this.ddlMotivosSancion.TabIndex = 25;
            // 
            // lblMotivoSancion
            // 
            this.lblMotivoSancion.AutoSize = true;
            this.lblMotivoSancion.Location = new System.Drawing.Point(26, 88);
            this.lblMotivoSancion.Name = "lblMotivoSancion";
            this.lblMotivoSancion.Size = new System.Drawing.Size(81, 13);
            this.lblMotivoSancion.TabIndex = 24;
            this.lblMotivoSancion.Text = "Motivo Sanción";
            // 
            // ddlAlumnos
            // 
            this.ddlAlumnos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAlumnos.FormattingEnabled = true;
            this.ddlAlumnos.Location = new System.Drawing.Point(291, 39);
            this.ddlAlumnos.Name = "ddlAlumnos";
            this.ddlAlumnos.Size = new System.Drawing.Size(240, 21);
            this.ddlAlumnos.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Alumno";
            // 
            // ddlCursos
            // 
            this.ddlCursos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCursos.FormattingEnabled = true;
            this.ddlCursos.Location = new System.Drawing.Point(28, 39);
            this.ddlCursos.Name = "ddlCursos";
            this.ddlCursos.Size = new System.Drawing.Size(241, 21);
            this.ddlCursos.TabIndex = 20;
            this.ddlCursos.SelectedIndexChanged += new System.EventHandler(this.ddlCurso_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Curso";
            // 
            // FormAltaSanciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 262);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.ddlCantidadSanciones);
            this.Controls.Add(this.ddlTiposSancion);
            this.Controls.Add(this.lblTipoSancion);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnRegistrarSancion);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.ddlMotivosSancion);
            this.Controls.Add(this.lblMotivoSancion);
            this.Controls.Add(this.ddlAlumnos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlCursos);
            this.Controls.Add(this.label1);
            this.Name = "FormAltaSanciones";
            this.Text = "Registrar Sanciones";
            this.Load += new System.EventHandler(this.FormAltaSanciones_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ComboBox ddlCantidadSanciones;
        private System.Windows.Forms.ComboBox ddlTiposSancion;
        private System.Windows.Forms.Label lblTipoSancion;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnRegistrarSancion;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.ComboBox ddlMotivosSancion;
        private System.Windows.Forms.Label lblMotivoSancion;
        private System.Windows.Forms.ComboBox ddlAlumnos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlCursos;
        private System.Windows.Forms.Label label1;
    }
}