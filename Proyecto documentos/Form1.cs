using System;
using System.Drawing;
using System.Windows.Forms;
using Proyecto_documentos.BaseDatos;
using BCrypt.Net;
using System.Data;

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
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            // Validar email
            if (!EsEmailValido(txtEmail.Text))
            {
                lblMensaje.Text = "Correo electrónico no válido.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                lblMensaje.Text = "Las contraseñas no coinciden.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            // Validar términos y condiciones
            if (!chkTerminos.Checked)
            {
                lblMensaje.Text = "Debe aceptar los términos y condiciones.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            // Verificar si el email ya existe
            if (EmailYaExiste(txtEmail.Text))
            {
                lblMensaje.Text = "Este correo electrónico ya está registrado.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            try
            {
                // Encriptar contraseña con BCrypt
                string passwordEncriptada = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, BCrypt.Net.BCrypt.GenerateSalt());

                // Preparar consulta SQL con parámetros para evitar inyección SQL
                string consulta = "INSERT INTO Usuario (nombre, correo, contraseña, estado) VALUES (@nombre, @correo, @password, @estado)";

                // Usar parámetros seguros
                var parametros = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@nombre", txtNombre.Text.Trim()),
                    new System.Data.SqlClient.SqlParameter("@correo", txtEmail.Text.Trim().ToLower()),
                    new System.Data.SqlClient.SqlParameter("@password", passwordEncriptada),
                    new System.Data.SqlClient.SqlParameter("@estado", "activo")
                };

                // Ejecutar consulta usando el método expandido de la clase Conexion
                int resultado = EjecutarComandoConParametros(consulta, parametros);

                if (resultado > 0)
                {
                    lblMensaje.Text = "¡Registro exitoso!";
                    lblMensaje.ForeColor = Color.Green;

                    MessageBox.Show("¡Registro exitoso! Ya puedes iniciar sesión en el sistema de gestión documental.",
                        "Registro Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar campos
                    LimpiarCampos();

                    // CORRECCIÓN: Usar ShowDialog para controlar mejor el flujo
                    DialogResult result = MessageBox.Show("¿Deseas iniciar sesión ahora?",
                        "Iniciar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // MÉTODO CORREGIDO: Usar ShowDialog y controlar el resultado
                        using (FormLogin formLogin = new FormLogin(conexionBD))
                        {
                            this.Hide(); // Ocultar temporalmente
                            DialogResult loginResult = formLogin.ShowDialog(this);

                            // Si el login fue exitoso, cerrar el formulario de registro
                            if (loginResult == DialogResult.OK)
                            {
                                this.Close(); // Cerrar completamente el formulario de registro
                                return;
                            }
                            else
                            {
                                this.Show(); // Mostrar de nuevo el registro si canceló el login
                            }
                        }
                    }
                }
                else
                {
                    lblMensaje.Text = "Error al registrar. Inténtelo de nuevo.";
                    lblMensaje.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = Color.Red;
                System.Diagnostics.Debug.WriteLine($"Error en registro: {ex.Message}");
            }
        }

        // CORRECCIÓN: Método mejorado para ir al login
        private void lnkIniciarSesion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // MÉTODO CORREGIDO: Usar ShowDialog para controlar mejor el flujo
            using (FormLogin formLogin = new FormLogin(conexionBD))
            {
                this.Hide(); // Ocultar temporalmente
                DialogResult loginResult = formLogin.ShowDialog(this);

                // Si el login fue exitoso, cerrar el formulario de registro
                if (loginResult == DialogResult.OK)
                {
                    this.Close(); // Cerrar completamente el formulario de registro
                }
                else
                {
                    this.Show(); // Mostrar de nuevo el registro si canceló el login
                }
            }
        }

        // CORRECCIÓN: Manejar el cierre del formulario correctamente
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Verificar si hay cambios sin guardar
            if (!string.IsNullOrEmpty(txtNombre.Text) ||
                !string.IsNullOrEmpty(txtEmail.Text) ||
                !string.IsNullOrEmpty(txtPassword.Text))
            {
                DialogResult result = MessageBox.Show(
                    "¿Está seguro de que desea cerrar? Los datos no guardados se perderán.",
                    "Confirmar cierre",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }

        // Método para validar email con expresión regular
        private bool EsEmailValido(string email)
        {
            try
            {
                var emailPattern = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return emailPattern.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        // Método para validar fortaleza de contraseña
        private string ValidarFortalezaPassword(string password)
        {
            if (password.Length < 6)
            {
                return "La contraseña debe tener al menos 6 caracteres.";
            }

            bool tieneMayuscula = false;
            bool tieneMinuscula = false;
            bool tieneNumero = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) tieneMayuscula = true;
                if (char.IsLower(c)) tieneMinuscula = true;
                if (char.IsDigit(c)) tieneNumero = true;
            }

            if (!tieneMayuscula)
            {
                return "La contraseña debe contener al menos una letra mayúscula.";
            }

            if (!tieneMinuscula)
            {
                return "La contraseña debe contener al menos una letra minúscula.";
            }

            if (!tieneNumero)
            {
                return "La contraseña debe contener al menos un número.";
            }

            return string.Empty; // Contraseña válida
        }

        // Método para verificar si el email ya existe
        private bool EmailYaExiste(string email)
        {
            try
            {
                string consulta = "SELECT COUNT(*) FROM Usuario WHERE correo = @correo";
                var parametros = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@correo", email.Trim().ToLower())
                };

                object resultado = EjecutarConsultaEscalar(consulta, parametros);
                return Convert.ToInt32(resultado) > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error verificando email: {ex.Message}");
                return false;
            }
        }

        // Método auxiliar para ejecutar comandos con parámetros
        private int EjecutarComandoConParametros(string consulta, System.Data.SqlClient.SqlParameter[] parametros)
        {
            try
            {
                using (var conexion = new System.Data.SqlClient.SqlConnection("Server=DESKTOP-E8CTSRV;Database=pruebaaa;Trusted_Connection=True;"))
                {
                    conexion.Open();
                    using (var comando = new System.Data.SqlClient.SqlCommand(consulta, conexion))
                    {
                        if (parametros != null)
                        {
                            comando.Parameters.AddRange(parametros);
                        }
                        return comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ejecutando comando: {ex.Message}", ex);
            }
        }

        // Método auxiliar para consultas escalares
        private object EjecutarConsultaEscalar(string consulta, System.Data.SqlClient.SqlParameter[] parametros)
        {
            try
            {
                using (var conexion = new System.Data.SqlClient.SqlConnection("Server=DESKTOP-E8CTSRV;Database=pruebaaa;Trusted_Connection=True;"))
                {
                    conexion.Open();
                    using (var comando = new System.Data.SqlClient.SqlCommand(consulta, conexion))
                    {
                        if (parametros != null)
                        {
                            comando.Parameters.AddRange(parametros);
                        }
                        return comando.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ejecutando consulta escalar: {ex.Message}", ex);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
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
            lblMensaje.Text = "";
        }

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

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            MostrarIndicadorFortaleza();
        }

        private void MostrarIndicadorFortaleza()
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMensaje.Text = "";
                return;
            }

            string error = ValidarFortalezaPassword(txtPassword.Text);
            if (string.IsNullOrEmpty(error))
            {
                lblMensaje.Text = "✓ Contraseña segura";
                lblMensaje.ForeColor = Color.Green;
            }
            else
            {
                lblMensaje.Text = error;
                lblMensaje.ForeColor = Color.Orange;
            }
        }
    }
}