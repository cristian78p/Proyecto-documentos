using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Proyecto_documentos.BaseDatos;
using BCrypt.Net;
using System.Data.SqlClient;

namespace Proyecto_documentos
{
    public partial class FormLogin : Form
    {
        private Conexion conexionBD;
        private int intentosFallidos = 0;
        private const int MAX_INTENTOS = 5;
        private DateTime ultimoIntentoFallido = DateTime.MinValue;
        private const int TIEMPO_BLOQUEO_MINUTOS = 2;

        public FormLogin()
        {
            InitializeComponent();
            conexionBD = new Conexion();
        }

        public FormLogin(Conexion conexion) : this()
        {
            conexionBD = conexion;
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

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Verificar si está bloqueado por intentos fallidos
            if (EstaBloqueado())
            {
                TimeSpan tiempoRestante = ultimoIntentoFallido.AddMinutes(TIEMPO_BLOQUEO_MINUTOS) - DateTime.Now;
                lblMensaje.Text = $"Demasiados intentos fallidos. Intente en {tiempoRestante.Minutes} minutos.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            // Validar campos
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMensaje.Text = "Todos los campos son obligatorios.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            // Validar formato de email
            if (!EsEmailValido(txtEmail.Text))
            {
                lblMensaje.Text = "Correo electrónico no válido.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            try
            {
                // Buscar usuario por email
                var datosUsuario = ObtenerDatosUsuario(txtEmail.Text.Trim().ToLower());

                if (datosUsuario != null)
                {
                    string passwordAlmacenada = datosUsuario["contraseña"].ToString();
                    string estadoUsuario = datosUsuario["estado"].ToString();

                    // Verificar si el usuario está activo
                    if (estadoUsuario.ToLower() != "activo")
                    {
                        lblMensaje.Text = "Su cuenta está inactiva. Contacte al administrador.";
                        lblMensaje.ForeColor = Color.Red;
                        return;
                    }

                    // Verificar contraseña con BCrypt
                    bool passwordCorrecta = BCrypt.Net.BCrypt.Verify(txtPassword.Text, passwordAlmacenada);

                    if (passwordCorrecta)
                    {
                        // Login exitoso
                        intentosFallidos = 0; // Resetear intentos fallidos

                        lblMensaje.Text = "Iniciando sesión...";
                        lblMensaje.ForeColor = Color.Green;

                        // Registrar login exitoso en auditoría
      

                        MessageBox.Show("Inicio de sesión exitoso. Bienvenido al Sistema de Gestión Documental.",
                            "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;

                        // CORRECCIÓN: Crear el formulario de documentos SIN cerrarlo inmediatamente
                        FormDocumentos formDocumentos = new FormDocumentos(txtEmail.Text.Trim().ToLower());

                        // IMPORTANTE: Ocultar este formulario (login) pero NO cerrarlo aún
                        this.Hide();

                        // Mostrar el formulario de documentos como MODAL
                        DialogResult resultadoDocumentos = formDocumentos.ShowDialog();

                        // Cuando se cierre FormDocumentos, liberar recursos
                        formDocumentos.Dispose();

                        // CORRECCIÓN: Ahora SÍ cerrar este formulario de login
                        this.Close();
                        return;
                    }
                    else
                    {
                        // Password incorrecta
                        ManejarIntentoFallido(datosUsuario);
                    }
                }
                else
                {
                    // Usuario no existe
                    ManejarIntentoFallido(null);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error del sistema. Intente más tarde.";
                lblMensaje.ForeColor = Color.Red;
                System.Diagnostics.Debug.WriteLine($"Error en login: {ex.Message}");
            }
        }

        // CORRECCIÓN: Evento del botón Cancelar mejorado
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // CORRECCIÓN: Evento del enlace para registrarse mejorado
        private void lnkRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Cerrar este formulario con Cancel para volver al registro
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // CORRECCIÓN: Manejar el cierre del formulario
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Si no se ha establecido un DialogResult, establecer Cancel
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            base.OnFormClosing(e);
        }

        // Método para validar email
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

        // Método para obtener datos del usuario
        private DataRow ObtenerDatosUsuario(string email)
        {
            try
            {
                string consulta = "SELECT id_usuario, nombre, correo, contraseña, estado FROM Usuario WHERE correo = @correo";
                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@correo", email)
                };

                DataTable resultado = EjecutarConsultaConParametros(consulta, parametros);

                if (resultado != null && resultado.Rows.Count > 0)
                {
                    return resultado.Rows[0];
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error obteniendo datos usuario: {ex.Message}");
                return null;
            }
        }

        // Método para manejar intentos fallidos
        private void ManejarIntentoFallido(DataRow datosUsuario)
        {
            intentosFallidos++;
            ultimoIntentoFallido = DateTime.Now;


            if (intentosFallidos >= MAX_INTENTOS)
            {
                lblMensaje.Text = $"Demasiados intentos fallidos. Cuenta bloqueada por {TIEMPO_BLOQUEO_MINUTOS} minutos.";
                lblMensaje.ForeColor = Color.Red;
            }
            else
            {
                int intentosRestantes = MAX_INTENTOS - intentosFallidos;
                lblMensaje.Text = $"Credenciales incorrectas. {intentosRestantes} intento(s) restante(s).";
                lblMensaje.ForeColor = Color.Red;
            }

            txtPassword.Text = "";
        }

        // Método para verificar si está bloqueado
        private bool EstaBloqueado()
        {
            if (intentosFallidos >= MAX_INTENTOS)
            {
                TimeSpan tiempoTranscurrido = DateTime.Now - ultimoIntentoFallido;
                if (tiempoTranscurrido.TotalMinutes < TIEMPO_BLOQUEO_MINUTOS)
                {
                    return true;
                }
                else
                {
                    intentosFallidos = 0;
                    return false;
                }
            }
            return false;
        }

        // Método para registrar eventos de auditoría
       

        private string ObtenerIPLocal()
        {
            try
            {
                return System.Net.Dns.GetHostName();
            }
            catch
            {
                return "IP_DESCONOCIDA";
            }
        }

        // Método auxiliar para ejecutar consultas con parámetros
        private DataTable EjecutarConsultaConParametros(string consulta, SqlParameter[] parametros)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var conexion = new SqlConnection("Server=DESKTOP-E8CTSRV;Database=pruebaaa;Trusted_Connection=True;"))
                {
                    conexion.Open();
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        if (parametros != null)
                        {
                            comando.Parameters.AddRange(parametros);
                        }
                        using (var adapter = new SqlDataAdapter(comando))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ejecutando consulta: {ex.Message}", ex);
            }
        }

        // Método auxiliar para ejecutar comandos con parámetros
        private int EjecutarComandoConParametros(string consulta, SqlParameter[] parametros)
        {
            try
            {
                using (var conexion = new SqlConnection("Server=DESKTOP-E8CTSRV;Database=pruebaaa;Trusted_Connection=True;"))
                {
                    conexion.Open();
                    using (var comando = new SqlCommand(consulta, conexion))
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

        // Eventos de interfaz
        private void FormLogin_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            txtEmail.Focus();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnIngresar_Click(sender, e);
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (lblMensaje.ForeColor == Color.Red)
            {
                lblMensaje.Text = "";
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (lblMensaje.ForeColor == Color.Red)
            {
                lblMensaje.Text = "";
            }
        }
    }
}