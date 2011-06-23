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
            this.btnImportarMySQL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlCadenaConexion = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnImportarMySQL
            // 
            this.btnImportarMySQL.Location = new System.Drawing.Point(18, 73);
            this.btnImportarMySQL.Name = "btnImportarMySQL";
            this.btnImportarMySQL.Size = new System.Drawing.Size(145, 36);
            this.btnImportarMySQL.TabIndex = 0;
            this.btnImportarMySQL.Text = "Importar My SQL";
            this.btnImportarMySQL.UseVisualStyleBackColor = true;
            this.btnImportarMySQL.Click += new System.EventHandler(this.btnImportarMySQL_Click);
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
            this.ddlCadenaConexion.Location = new System.Drawing.Point(18, 44);
            this.ddlCadenaConexion.Name = "ddlCadenaConexion";
            this.ddlCadenaConexion.Size = new System.Drawing.Size(520, 23);
            this.ddlCadenaConexion.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 340);
            this.Controls.Add(this.ddlCadenaConexion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImportarMySQL);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportarMySQL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlCadenaConexion;
    }
}

