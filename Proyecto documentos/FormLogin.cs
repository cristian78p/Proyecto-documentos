using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Proyecto_documentos.BaseDatos;

namespace Proyecto_documentos
{
    public partial class FormLogin : Form
    {
        private Conexion conexionBD;

        public FormLogin()
        {
            InitializeComponent();
            conexionBD = new Conexion();
        }

        // Constructor sobrecargado que acepta una conexión existente
        public FormLogin(Conexion conexion) : this()
        {
            conexionBD = conexion;
        }

        // Evento para dibujar el ícono de documento
        private void picDocumento_Paint(object sender, PaintEventArgs e)
        {
            // Dibujar un ícono de documento
            e.Graphics.FillRectangle(Brushes.White, 25, 10, 50, 70);
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(52, 152, 219), 2), 25, 10, 50, 70);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 45, 10, 45, 25);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 45, 25, 75, 25);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 35, 40, 65, 40);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 35, 50, 65, 50);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 35, 60, 65, 60);
        }

        // Evento del botón Ingresar
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Validar campos
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMensaje.Text = "Todos los campos son obligatorios.";
                return;
            }

            // Validar email básico
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                lblMensaje.Text = "Correo electrónico no válido.";
                return;
            }

            try
            {
                // Consulta para verificar credenciales
                string consulta = $"SELECT * FROM Usuario WHERE correo='{txtEmail.Text}' AND contraseña='{txtPassword.Text}'";
                DataTable resultado = conexionBD.EjecutarConsulta(consulta);

                if (resultado != null && resultado.Rows.Count > 0)
                {
                    // Login exitoso - aquí se podría abrir el formulario principal
                    MessageBox.Show("Inicio de sesión exitoso. Bienvenido al Sistema de Gestión Documental.",
                        "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FormDocumentos form = new FormDocumentos(txtEmail.Text);
                    form.Show(); // Mostrar el formulario de documentos
                    this.Hide(); // Ocultar el formulario de registro

                }
                else
                {
                    lblMensaje.Text = "Credenciales incorrectas. Verifique su correo y contraseña.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        // Evento del botón Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Establecer el resultado del diálogo como Cancel y cerrar
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Evento del enlace para registrarse
        private void lnkRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Cerrar este formulario y mostrar el de registro
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            // El Form1 se mostrará nuevamente ya que este formulario se cierra con Cancel
        }
    }
}