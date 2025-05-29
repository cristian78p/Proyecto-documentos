namespace Proyecto_documentos
{
    partial class FormAgregarEtiqueta
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
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtNombreEtiqueta = new System.Windows.Forms.TextBox();
            this.lblNombreEtiqueta = new System.Windows.Forms.Label();
            this.groupBoxOpciones = new System.Windows.Forms.GroupBox();
            this.rbCrearYAsignar = new System.Windows.Forms.RadioButton();
            this.rbSeleccionarExistente = new System.Windows.Forms.RadioButton();
            this.cmbEtiquetasExistentes = new System.Windows.Forms.ComboBox();
            this.lblEtiquetasExistentes = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBoxOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 60);
            this.panel1.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(168, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Nueva Etiqueta";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblMensaje);
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Controls.Add(this.btnGuardar);
            this.panel2.Controls.Add(this.groupBoxOpciones);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(20);
            this.panel2.Size = new System.Drawing.Size(400, 340);
            this.panel2.TabIndex = 1;
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(25, 250);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(350, 20);
            this.lblMensaje.TabIndex = 4;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(220, 285);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(80, 285);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 35);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtNombreEtiqueta
            // 
            this.txtNombreEtiqueta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreEtiqueta.Location = new System.Drawing.Point(15, 75);
            this.txtNombreEtiqueta.MaxLength = 50;
            this.txtNombreEtiqueta.Name = "txtNombreEtiqueta";
            this.txtNombreEtiqueta.Size = new System.Drawing.Size(320, 25);
            this.txtNombreEtiqueta.TabIndex = 1;
            this.txtNombreEtiqueta.TextChanged += new System.EventHandler(this.txtNombreEtiqueta_TextChanged);
            // 
            // lblNombreEtiqueta
            // 
            this.lblNombreEtiqueta.AutoSize = true;
            this.lblNombreEtiqueta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreEtiqueta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNombreEtiqueta.Location = new System.Drawing.Point(15, 50);
            this.lblNombreEtiqueta.Name = "lblNombreEtiqueta";
            this.lblNombreEtiqueta.Size = new System.Drawing.Size(156, 19);
            this.lblNombreEtiqueta.TabIndex = 0;
            this.lblNombreEtiqueta.Text = "Nombre de la etiqueta:";
            // 
            // groupBoxOpciones
            // 
            this.groupBoxOpciones.Controls.Add(this.lblEtiquetasExistentes);
            this.groupBoxOpciones.Controls.Add(this.cmbEtiquetasExistentes);
            this.groupBoxOpciones.Controls.Add(this.rbSeleccionarExistente);
            this.groupBoxOpciones.Controls.Add(this.rbCrearYAsignar);
            this.groupBoxOpciones.Controls.Add(this.txtNombreEtiqueta);
            this.groupBoxOpciones.Controls.Add(this.lblNombreEtiqueta);
            this.groupBoxOpciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxOpciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.groupBoxOpciones.Location = new System.Drawing.Point(25, 20);
            this.groupBoxOpciones.Name = "groupBoxOpciones";
            this.groupBoxOpciones.Size = new System.Drawing.Size(350, 220);
            this.groupBoxOpciones.TabIndex = 1;
            this.groupBoxOpciones.TabStop = false;
            this.groupBoxOpciones.Text = "Opciones";
            // 
            // rbCrearYAsignar
            // 
            this.rbCrearYAsignar.AutoSize = true;
            this.rbCrearYAsignar.Checked = true;
            this.rbCrearYAsignar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCrearYAsignar.Location = new System.Drawing.Point(15, 25);
            this.rbCrearYAsignar.Name = "rbCrearYAsignar";
            this.rbCrearYAsignar.Size = new System.Drawing.Size(183, 19);
            this.rbCrearYAsignar.TabIndex = 0;
            this.rbCrearYAsignar.TabStop = true;
            this.rbCrearYAsignar.Text = "Crear nueva etiqueta y asignar";
            this.rbCrearYAsignar.UseVisualStyleBackColor = true;
            this.rbCrearYAsignar.CheckedChanged += new System.EventHandler(this.rbCrearYAsignar_CheckedChanged);
            // 
            // rbSeleccionarExistente
            // 
            this.rbSeleccionarExistente.AutoSize = true;
            this.rbSeleccionarExistente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSeleccionarExistente.Location = new System.Drawing.Point(15, 120);
            this.rbSeleccionarExistente.Name = "rbSeleccionarExistente";
            this.rbSeleccionarExistente.Size = new System.Drawing.Size(200, 19);
            this.rbSeleccionarExistente.TabIndex = 2;
            this.rbSeleccionarExistente.Text = "Seleccionar etiqueta existente";
            this.rbSeleccionarExistente.UseVisualStyleBackColor = true;
            this.rbSeleccionarExistente.CheckedChanged += new System.EventHandler(this.rbSeleccionarExistente_CheckedChanged);
            // 
            // cmbEtiquetasExistentes
            // 
            this.cmbEtiquetasExistentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEtiquetasExistentes.Enabled = false;
            this.cmbEtiquetasExistentes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEtiquetasExistentes.FormattingEnabled = true;
            this.cmbEtiquetasExistentes.Location = new System.Drawing.Point(15, 170);
            this.cmbEtiquetasExistentes.Name = "cmbEtiquetasExistentes";
            this.cmbEtiquetasExistentes.Size = new System.Drawing.Size(320, 25);
            this.cmbEtiquetasExistentes.TabIndex = 4;
            // 
            // lblEtiquetasExistentes
            // 
            this.lblEtiquetasExistentes.AutoSize = true;
            this.lblEtiquetasExistentes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetasExistentes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblEtiquetasExistentes.Location = new System.Drawing.Point(15, 145);
            this.lblEtiquetasExistentes.Name = "lblEtiquetasExistentes";
            this.lblEtiquetasExistentes.Size = new System.Drawing.Size(141, 19);
            this.lblEtiquetasExistentes.TabIndex = 3;
            this.lblEtiquetasExistentes.Text = "Etiquetas disponibles:";
            // 
            // FormNuevaEtiqueta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNuevaEtiqueta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nueva Etiqueta";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBoxOpciones.ResumeLayout(false);
            this.groupBoxOpciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxOpciones;
        private System.Windows.Forms.TextBox txtNombreEtiqueta;
        private System.Windows.Forms.Label lblNombreEtiqueta;
        private System.Windows.Forms.RadioButton rbCrearYAsignar;
        private System.Windows.Forms.RadioButton rbSeleccionarExistente;
        private System.Windows.Forms.ComboBox cmbEtiquetasExistentes;
        private System.Windows.Forms.Label lblEtiquetasExistentes;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblMensaje;
    }
}