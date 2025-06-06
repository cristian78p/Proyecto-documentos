﻿namespace Proyecto_documentos
{
    partial class FormDocumentos
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
            this.panelIzquierdo = new System.Windows.Forms.Panel();
            this.lblUsuarioActual = new System.Windows.Forms.Label();
            this.picDocumento = new System.Windows.Forms.PictureBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblTituloIzquierdo = new System.Windows.Forms.Label();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.panelDocumentos = new System.Windows.Forms.Panel();
            this.dgvDocumentos = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaModificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEtiquetas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBusquedaFiltros = new System.Windows.Forms.Panel();
            this.lblResultados = new System.Windows.Forms.Label();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.cmbEtiquetas = new System.Windows.Forms.ComboBox();
            this.lblFiltroEtiqueta = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.lblBusqueda = new System.Windows.Forms.Label();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnVerEtiquetas = new System.Windows.Forms.Button();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.panelIzquierdo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDocumento)).BeginInit();
            this.panelPrincipal.SuspendLayout();
            this.panelDocumentos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).BeginInit();
            this.panelBusquedaFiltros.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelIzquierdo
            // 
            this.panelIzquierdo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelIzquierdo.Controls.Add(this.lblUsuarioActual);
            this.panelIzquierdo.Controls.Add(this.picDocumento);
            this.panelIzquierdo.Controls.Add(this.lblDescripcion);
            this.panelIzquierdo.Controls.Add(this.lblTituloIzquierdo);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(250, 700);
            this.panelIzquierdo.TabIndex = 0;
            // 
            // lblUsuarioActual
            // 
            this.lblUsuarioActual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUsuarioActual.AutoSize = true;
            this.lblUsuarioActual.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsuarioActual.ForeColor = System.Drawing.Color.White;
            this.lblUsuarioActual.Location = new System.Drawing.Point(12, 660);
            this.lblUsuarioActual.Name = "lblUsuarioActual";
            this.lblUsuarioActual.Size = new System.Drawing.Size(147, 19);
            this.lblUsuarioActual.TabIndex = 3;
            this.lblUsuarioActual.Text = "Usuario: correo@email";
            // 
            // picDocumento
            // 
            this.picDocumento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.picDocumento.Location = new System.Drawing.Point(75, 30);
            this.picDocumento.Name = "picDocumento";
            this.picDocumento.Size = new System.Drawing.Size(100, 100);
            this.picDocumento.TabIndex = 2;
            this.picDocumento.TabStop = false;
            this.picDocumento.Paint += new System.Windows.Forms.PaintEventHandler(this.picDocumento_Paint);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDescripcion.ForeColor = System.Drawing.Color.White;
            this.lblDescripcion.Location = new System.Drawing.Point(12, 250);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(232, 40);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Administra todos tus documentos\r\nde manera sencilla y ordenada";
            // 
            // lblTituloIzquierdo
            // 
            this.lblTituloIzquierdo.AutoSize = true;
            this.lblTituloIzquierdo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTituloIzquierdo.ForeColor = System.Drawing.Color.White;
            this.lblTituloIzquierdo.Location = new System.Drawing.Point(10, 150);
            this.lblTituloIzquierdo.Name = "lblTituloIzquierdo";
            this.lblTituloIzquierdo.Size = new System.Drawing.Size(231, 64);
            this.lblTituloIzquierdo.TabIndex = 0;
            this.lblTituloIzquierdo.Text = "Sistema de Gestión\r\nDocumental";
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.BackColor = System.Drawing.Color.White;
            this.panelPrincipal.Controls.Add(this.panelDocumentos);
            this.panelPrincipal.Controls.Add(this.panelBusquedaFiltros);
            this.panelPrincipal.Controls.Add(this.panelSuperior);
            this.panelPrincipal.Controls.Add(this.panelBotones);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(250, 0);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(950, 700);
            this.panelPrincipal.TabIndex = 1;
            // 
            // panelDocumentos
            // 
            this.panelDocumentos.Controls.Add(this.dgvDocumentos);
            this.panelDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDocumentos.Location = new System.Drawing.Point(0, 170);
            this.panelDocumentos.Name = "panelDocumentos";
            this.panelDocumentos.Padding = new System.Windows.Forms.Padding(20);
            this.panelDocumentos.Size = new System.Drawing.Size(800, 530);
            this.panelDocumentos.TabIndex = 5;
            // 
            // dgvDocumentos
            // 
            this.dgvDocumentos.AllowUserToAddRows = false;
            this.dgvDocumentos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDocumentos.BackgroundColor = System.Drawing.Color.White;
            this.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocumentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colDescripcion,
            this.colFechaCreacion,
            this.colFechaModificacion,
            this.colTipo,
            this.colEtiquetas});
            this.dgvDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDocumentos.Location = new System.Drawing.Point(20, 20);
            this.dgvDocumentos.Name = "dgvDocumentos";
            this.dgvDocumentos.ReadOnly = true;
            this.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDocumentos.Size = new System.Drawing.Size(760, 490);
            this.dgvDocumentos.TabIndex = 0;
            this.dgvDocumentos.SelectionChanged += new System.EventHandler(this.dgvDocumentos_SelectionChanged);
            // 
            // colId
            // 
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colNombre
            // 
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            // 
            // colDescripcion
            // 
            this.colDescripcion.HeaderText = "Descripción";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.ReadOnly = true;
            // 
            // colFechaCreacion
            // 
            this.colFechaCreacion.HeaderText = "Fecha Creación";
            this.colFechaCreacion.Name = "colFechaCreacion";
            this.colFechaCreacion.ReadOnly = true;
            // 
            // colFechaModificacion
            // 
            this.colFechaModificacion.HeaderText = "Fecha Modificación";
            this.colFechaModificacion.Name = "colFechaModificacion";
            this.colFechaModificacion.ReadOnly = true;
            // 
            // colTipo
            // 
            this.colTipo.HeaderText = "Tipo";
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;
            // 
            // colEtiquetas
            // 
            this.colEtiquetas.HeaderText = "Etiquetas";
            this.colEtiquetas.Name = "colEtiquetas";
            this.colEtiquetas.ReadOnly = true;
            // 
            // panelBusquedaFiltros
            // 
            this.panelBusquedaFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelBusquedaFiltros.Controls.Add(this.lblResultados);
            this.panelBusquedaFiltros.Controls.Add(this.btnLimpiarFiltros);
            this.panelBusquedaFiltros.Controls.Add(this.cmbEtiquetas);
            this.panelBusquedaFiltros.Controls.Add(this.lblFiltroEtiqueta);
            this.panelBusquedaFiltros.Controls.Add(this.btnBuscar);
            this.panelBusquedaFiltros.Controls.Add(this.txtBusqueda);
            this.panelBusquedaFiltros.Controls.Add(this.lblBusqueda);
            this.panelBusquedaFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusquedaFiltros.Location = new System.Drawing.Point(0, 100);
            this.panelBusquedaFiltros.Name = "panelBusquedaFiltros";
            this.panelBusquedaFiltros.Size = new System.Drawing.Size(800, 70);
            this.panelBusquedaFiltros.TabIndex = 4;
            // 
            // lblResultados
            // 
            this.lblResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResultados.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblResultados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblResultados.Location = new System.Drawing.Point(650, 45);
            this.lblResultados.Name = "lblResultados";
            this.lblResultados.Size = new System.Drawing.Size(130, 20);
            this.lblResultados.TabIndex = 6;
            this.lblResultados.Text = "0 documentos";
            this.lblResultados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLimpiarFiltros.FlatAppearance.BorderSize = 0;
            this.btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarFiltros.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnLimpiarFiltros.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(570, 10);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(80, 28);
            this.btnLimpiarFiltros.TabIndex = 5;
            this.btnLimpiarFiltros.Text = "Limpiar";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = false;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // cmbEtiquetas
            // 
            this.cmbEtiquetas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEtiquetas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbEtiquetas.FormattingEnabled = true;
            this.cmbEtiquetas.Location = new System.Drawing.Point(415, 13);
            this.cmbEtiquetas.Name = "cmbEtiquetas";
            this.cmbEtiquetas.Size = new System.Drawing.Size(140, 23);
            this.cmbEtiquetas.TabIndex = 4;
            this.cmbEtiquetas.SelectedIndexChanged += new System.EventHandler(this.cmbEtiquetas_SelectedIndexChanged);
            // 
            // lblFiltroEtiqueta
            // 
            this.lblFiltroEtiqueta.AutoSize = true;
            this.lblFiltroEtiqueta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltroEtiqueta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.lblFiltroEtiqueta.Location = new System.Drawing.Point(350, 16);
            this.lblFiltroEtiqueta.Name = "lblFiltroEtiqueta";
            this.lblFiltroEtiqueta.Size = new System.Drawing.Size(56, 15);
            this.lblFiltroEtiqueta.TabIndex = 3;
            this.lblFiltroEtiqueta.Text = "Etiqueta:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(270, 10);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(70, 28);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBusqueda.Location = new System.Drawing.Point(80, 13);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(180, 23);
            this.txtBusqueda.TabIndex = 1;
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
            // 
            // lblBusqueda
            // 
            this.lblBusqueda.AutoSize = true;
            this.lblBusqueda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBusqueda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.lblBusqueda.Location = new System.Drawing.Point(20, 16);
            this.lblBusqueda.Name = "lblBusqueda";
            this.lblBusqueda.Size = new System.Drawing.Size(47, 15);
            this.lblBusqueda.TabIndex = 0;
            this.lblBusqueda.Text = "Buscar:";
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(800, 100);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitulo.Location = new System.Drawing.Point(13, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(194, 41);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Documentos";
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBotones.Controls.Add(this.btnVerEtiquetas);
            this.panelBotones.Controls.Add(this.btnDescargar);
            this.panelBotones.Controls.Add(this.btnEliminar);
            this.panelBotones.Controls.Add(this.btnEditar);
            this.panelBotones.Controls.Add(this.btnAgregar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelBotones.Location = new System.Drawing.Point(800, 0);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(150, 700);
            this.panelBotones.TabIndex = 2;
            // 
            // btnVerEtiquetas
            // 
            this.btnVerEtiquetas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnVerEtiquetas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerEtiquetas.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnVerEtiquetas.ForeColor = System.Drawing.Color.White;
            this.btnVerEtiquetas.Location = new System.Drawing.Point(15, 210);
            this.btnVerEtiquetas.Name = "btnVerEtiquetas";
            this.btnVerEtiquetas.Size = new System.Drawing.Size(120, 40);
            this.btnVerEtiquetas.TabIndex = 4;
            this.btnVerEtiquetas.Text = "Ver Etiquetas";
            this.btnVerEtiquetas.UseVisualStyleBackColor = false;
            this.btnVerEtiquetas.Click += new System.EventHandler(this.btnVerEtiquetas_Click);
            // 
            // btnDescargar
            // 
            this.btnDescargar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnDescargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDescargar.ForeColor = System.Drawing.Color.White;
            this.btnDescargar.Location = new System.Drawing.Point(15, 160);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(120, 40);
            this.btnDescargar.TabIndex = 3;
            this.btnDescargar.Text = "Descargar";
            this.btnDescargar.UseVisualStyleBackColor = false;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(15, 110);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 40);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(15, 60);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(120, 40);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(15, 10);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(120, 40);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // FormDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panelIzquierdo);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FormDocumentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Gestión Documental";
            this.Load += new System.EventHandler(this.FormDocumentos_Load);
            this.panelIzquierdo.ResumeLayout(false);
            this.panelIzquierdo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDocumento)).EndInit();
            this.panelPrincipal.ResumeLayout(false);
            this.panelDocumentos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).EndInit();
            this.panelBusquedaFiltros.ResumeLayout(false);
            this.panelBusquedaFiltros.PerformLayout();
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Label lblTituloIzquierdo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.PictureBox picDocumento;
        private System.Windows.Forms.Label lblUsuarioActual;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.Button btnVerEtiquetas;
        private System.Windows.Forms.Panel panelDocumentos;
        private System.Windows.Forms.DataGridView dgvDocumentos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaCreacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaModificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEtiquetas;
        private System.Windows.Forms.Panel panelBusquedaFiltros;
        private System.Windows.Forms.Label lblBusqueda;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblFiltroEtiqueta;
        private System.Windows.Forms.ComboBox cmbEtiquetas;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Label lblResultados;
    }
}