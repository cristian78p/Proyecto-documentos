using System;
using System.Drawing;
using System.Windows.Forms;
using Proyecto_documentos.BaseDatos;

namespace Proyecto_documentos
{
    public partial class Form1 : Form
    {
        private Conexion conexionBD;

        public Form1()
        {
            InitializeComponent();
            
            // Inicializar conexión a base de datos
            conexionBD = new Conexion();
        }

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

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            // Validar campos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmarPassword.Text))
            {
                lblMensaje.Text = "Todos los campos son obligatorios.";
                return;
            }

            // Validar email
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                lblMensaje.Text = "Correo electrónico no válido.";
                return;
            }

            // Validar contraseñas coincidentes
            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                lblMensaje.Text = "Las contraseñas no coinciden.";
                return;
            }

            // Validar términos y condiciones
            if (!chkTerminos.Checked)
            {
                lblMensaje.Text = "Debe aceptar los términos y condiciones.";
                return;
            }

            // Preparar consulta SQL ajustada a la estructura de la tabla
            string consulta = $"INSERT INTO Usuario (nombre, correo, contraseña, estado) " +
                             $"VALUES ('{txtNombre.Text}', '{txtEmail.Text}', " +
                             $"'{txtPassword.Text}', 'Activo')";

            try
            {
                // Ejecutar consulta
                int resultado = conexionBD.EjecutarComando(consulta);

                if (resultado > 0)
                {
                    MessageBox.Show("¡Registro exitoso! Ya puedes iniciar sesión en el sistema de gestión documental.",
                        "Registro Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar campos
                    LimpiarCampos();
                }
                else
                {
                    lblMensaje.Text = "Error al registrar. Inténtelo de nuevo.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            // Confirmar cancelación
            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas cancelar el registro?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtConfirmarPassword.Text = "";
            chkTerminos.Checked = false;
            lblMensaje.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Código para inicializar el formulario al cargar
        }

        // Agregamos un botón para probar la conexión después de que el formulario ya funcione
        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
            try
            {
                if (conexionBD.ProbarConexion())
                    MessageBox.Show("Conexión exitosa");
                else
                    MessageBox.Show("No se pudo conectar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void lnkIniciarSesion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Crear y mostrar el formulario de login
            FormLogin formLogin = new FormLogin(conexionBD);
            formLogin.Show();
            this.Hide(); // Ocultar el formulario de registro

        }
    }
}