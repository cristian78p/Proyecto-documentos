namespace Proyecto_documentos
{
    partial class FormGestionarEtiquetas
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnQuitarEtiqueta = new System.Windows.Forms.Button();
            this.btnAgregarEtiqueta = new System.Windows.Forms.Button();
            this.lstEtiquetas = new System.Windows.Forms.ListBox();
            this.lblDocumentoTitulo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 60);
            this.panel1.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(266, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Etiquetas del Documento";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnCerrar);
            this.panel2.Controls.Add(this.btnQuitarEtiqueta);
            this.panel2.Controls.Add(this.btnAgregarEtiqueta);
            this.panel2.Controls.Add(this.lstEtiquetas);
            this.panel2.Controls.Add(this.lblDocumentoTitulo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(20);
            this.panel2.Size = new System.Drawing.Size(484, 379);
            this.panel2.TabIndex = 1;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(364, 320);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 35);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnQuitarEtiqueta
            // 
            this.btnQuitarEtiqueta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnQuitarEtiqueta.FlatAppearance.BorderSize = 0;
            this.btnQuitarEtiqueta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitarEtiqueta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitarEtiqueta.ForeColor = System.Drawing.Color.White;
            this.btnQuitarEtiqueta.Location = new System.Drawing.Point(164, 270);
            this.btnQuitarEtiqueta.Name = "btnQuitarEtiqueta";
            this.btnQuitarEtiqueta.Size = new System.Drawing.Size(130, 35);
            this.btnQuitarEtiqueta.TabIndex = 3;
            this.btnQuitarEtiqueta.Text = "Quitar Etiqueta";
            this.btnQuitarEtiqueta.UseVisualStyleBackColor = false;
            this.btnQuitarEtiqueta.Click += new System.EventHandler(this.btnQuitarEtiqueta_Click);
            // 
            // btnAgregarEtiqueta
            // 
            this.btnAgregarEtiqueta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAgregarEtiqueta.FlatAppearance.BorderSize = 0;
            this.btnAgregarEtiqueta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarEtiqueta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarEtiqueta.ForeColor = System.Drawing.Color.White;
            this.btnAgregarEtiqueta.Location = new System.Drawing.Point(20, 270);
            this.btnAgregarEtiqueta.Name = "btnAgregarEtiqueta";
            this.btnAgregarEtiqueta.Size = new System.Drawing.Size(130, 35);
            this.btnAgregarEtiqueta.TabIndex = 2;
            this.btnAgregarEtiqueta.Text = "Agregar Etiqueta";
            this.btnAgregarEtiqueta.UseVisualStyleBackColor = false;
            this.btnAgregarEtiqueta.Click += new System.EventHandler(this.btnAgregarEtiqueta_Click);
            // 
            // lstEtiquetas
            // 
            this.lstEtiquetas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEtiquetas.FormattingEnabled = true;
            this.lstEtiquetas.ItemHeight = 17;
            this.lstEtiquetas.Location = new System.Drawing.Point(20, 60);
            this.lstEtiquetas.Name = "lstEtiquetas";
            this.lstEtiquetas.Size = new System.Drawing.Size(444, 191);
            this.lstEtiquetas.TabIndex = 1;
            this.lstEtiquetas.SelectedIndexChanged += new System.EventHandler(this.lstEtiquetas_SelectedIndexChanged);
            // 
            // lblDocumentoTitulo
            // 
            this.lblDocumentoTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblDocumentoTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblDocumentoTitulo.Name = "lblDocumentoTitulo";
            this.lblDocumentoTitulo.Size = new System.Drawing.Size(444, 25);
            this.lblDocumentoTitulo.TabIndex = 0;
            this.lblDocumentoTitulo.Text = "Documento: [Título del documento]";
            // 
            // FormGestionarEtiquetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 439);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGestionarEtiquetas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestionar Etiquetas";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDocumentoTitulo;
        private System.Windows.Forms.ListBox lstEtiquetas;
        private System.Windows.Forms.Button btnAgregarEtiqueta;
        private System.Windows.Forms.Button btnQuitarEtiqueta;
        private System.Windows.Forms.Button btnCerrar;
    }
}