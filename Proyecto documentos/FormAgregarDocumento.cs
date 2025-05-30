using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using Proyecto_documentos.BaseDatos;

namespace Proyecto_documentos
{
    public partial class FormAgregarDocumento : Form
    {
        private Conexion conexionBD;
        private string usuarioActualCorreo;
        private int idCreador;
        private string rutaArchivoSeleccionado = "";

        public FormAgregarDocumento(Conexion conexion, string correoUsuario)
        {
            InitializeComponent();
            conexionBD = conexion;
            usuarioActualCorreo = correoUsuario;

            ObtenerIdCreador();
        }

        private void ObtenerIdCreador()
        {
            try
            {
                string consulta = $"SELECT id_usuario FROM Usuario WHERE correo = '{usuarioActualCorreo}'";
                DataTable resultado = conexionBD.EjecutarConsulta(consulta);
                if (resultado != null && resultado.Rows.Count > 0)
                {
                    idCreador = Convert.ToInt32(resultado.Rows[0]["id_usuario"]);
                }
                else
                {
                    MessageBox.Show("Error: No se pudo identificar al usuario creador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Abort;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener ID del creador: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private void btnSeleccionarArchivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "Todos los archivos (*.*)|*.*|Documentos PDF (*.pdf)|*.pdf|Documentos Word (*.docx;*.doc)|*.docx;*.doc|Imágenes (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|Hojas de cálculo (*.xlsx;*.xls)|*.xlsx;*.xls|Presentaciones (*.pptx;*.ppt)|*.pptx;*.ppt";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string archivoSeleccionado = openFileDialog1.FileName;

                // Validar el archivo antes de procesarlo
                if (ValidarArchivo(archivoSeleccionado))
                {
                    rutaArchivoSeleccionado = archivoSeleccionado;
                    txtRutaArchivo.Text = Path.GetFileName(rutaArchivoSeleccionado);
                    txtFormatoArchivo.Text = Path.GetExtension(rutaArchivoSeleccionado).TrimStart('.').ToLower();

                    if (string.IsNullOrWhiteSpace(txtTitulo.Text))
                    {
                        txtTitulo.Text = Path.GetFileNameWithoutExtension(rutaArchivoSeleccionado);
                    }

                    // Mostrar información del archivo
                    FileInfo fileInfo = new FileInfo(rutaArchivoSeleccionado);
                    lblMensaje.Text = $"Archivo seleccionado: {fileInfo.Name} ({FormatearTamano(fileInfo.Length)})";
                    lblMensaje.ForeColor = Color.Green;
                }
                else
                {
                    // Si la validación falla, limpiar la selección
                    rutaArchivoSeleccionado = "";
                    txtRutaArchivo.Text = "";
                    txtFormatoArchivo.Text = "";
                    // El mensaje de error ya se muestra en ValidarArchivo()
                }
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtFormatoArchivo.Text) ||
                string.IsNullOrWhiteSpace(rutaArchivoSeleccionado))
            {
                lblMensaje.Text = "Todos los campos son obligatorios y debe seleccionar un archivo.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            if (idCreador <= 0)
            {
                lblMensaje.Text = "Error: No se pudo identificar al usuario creador.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            // Validar el archivo nuevamente antes de guardar
            if (!ValidarArchivo(rutaArchivoSeleccionado))
            {
                return; // El mensaje de error ya se muestra en ValidarArchivo()
            }

            try
            {
                // Deshabilitar controles durante el guardado
                btnGuardar.Enabled = false;
                btnSeleccionarArchivo.Enabled = false;
                lblMensaje.Text = "Guardando documento...";
                lblMensaje.ForeColor = Color.Blue;

                // Leer el archivo como array de bytes
                byte[] contenidoArchivo = await Task.Run(() => File.ReadAllBytes(rutaArchivoSeleccionado));

                string extension = Path.GetExtension(rutaArchivoSeleccionado);
                string tipoMime = ObtenerTipoMime(extension);
                long tamanoBytes = contenidoArchivo.Length;
                string nombreOriginal = Path.GetFileName(rutaArchivoSeleccionado);

                // Verificar tamaño del archivo (limite de 50MB para evitar problemas)
                if (tamanoBytes > 1L * 1024 * 1024 * 1024)
                {
                    lblMensaje.Text = "El archivo es demasiado grande (máximo 1GB).";
                    lblMensaje.ForeColor = Color.Red;
                    return;
                }

                // Guardar documento en base de datos
                bool resultado = await GuardarDocumentoEnBD(contenidoArchivo, tipoMime, tamanoBytes, nombreOriginal);

                if (resultado)
                {
                    MessageBox.Show("Documento guardado correctamente en la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblMensaje.Text = "Error al guardar el documento en la base de datos.";
                    lblMensaje.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar el documento: " + ex.Message;
                lblMensaje.ForeColor = Color.Red;
                MessageBox.Show("Error detallado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Rehabilitar controles
                btnGuardar.Enabled = true;
                btnSeleccionarArchivo.Enabled = true;
            }
        }

        private async Task<bool> GuardarDocumentoEnBD(byte[] contenidoArchivo, string tipoMime, long tamanoBytes, string nombreOriginal)
        {
            try
            {
                // Usar la cadena de conexión de tu clase Conexion
                string cadenaConexion = "Server=DESKTOP-E8CTSRV;Database=pruebaaa;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    await connection.OpenAsync();

                    string consulta = @"
                        INSERT INTO Documento 
                        (titulo, descripcion, fecha_creacion, fecha_modificacion, id_creador, formato_archivo, 
                         contenido_archivo, tipo_mime, tamano_bytes, nombre_original)
                        VALUES 
                        (@Titulo, @Descripcion, @FechaCreacion, @FechaModificacion, @IdCreador, @FormatoArchivo,
                         @ContenidoArchivo, @TipoMime, @TamanoBytes, @NombreOriginal)";

                    using (SqlCommand comando = new SqlCommand(consulta, connection))
                    {
                        // Agregar parámetros
                        comando.Parameters.AddWithValue("@Titulo", txtTitulo.Text.Trim());
                        comando.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text.Trim());
                        comando.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);
                        comando.Parameters.AddWithValue("@FechaModificacion", DateTime.Now);
                        comando.Parameters.AddWithValue("@IdCreador", idCreador);
                        comando.Parameters.AddWithValue("@FormatoArchivo", txtFormatoArchivo.Text.Trim());
                        comando.Parameters.AddWithValue("@ContenidoArchivo", contenidoArchivo);
                        comando.Parameters.AddWithValue("@TipoMime", tipoMime);
                        comando.Parameters.AddWithValue("@TamanoBytes", tamanoBytes);
                        comando.Parameters.AddWithValue("@NombreOriginal", nombreOriginal);

                        int filasAfectadas = await comando.ExecuteNonQueryAsync();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la base de datos: {ex.Message}", ex);
            }
        }

        private string ObtenerTipoMime(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return "application/octet-stream";

            // Usar if-else en lugar de switch expressions para C# 7.3
            string ext = extension.ToLower();
            if (ext == ".pdf") return "application/pdf";
            if (ext == ".png") return "image/png";
            if (ext == ".jpg" || ext == ".jpeg") return "image/jpeg";
            if (ext == ".gif") return "image/gif";
            if (ext == ".bmp") return "image/bmp";
            if (ext == ".txt") return "text/plain";
            if (ext == ".doc") return "application/msword";
            if (ext == ".docx") return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            if (ext == ".xls") return "application/vnd.ms-excel";
            if (ext == ".xlsx") return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            if (ext == ".ppt") return "application/vnd.ms-powerpoint";
            if (ext == ".pptx") return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            if (ext == ".zip") return "application/zip";
            if (ext == ".rar") return "application/x-rar-compressed";
            if (ext == ".mp4") return "video/mp4";
            if (ext == ".mp3") return "audio/mpeg";
            if (ext == ".wav") return "audio/wav";

            return "application/octet-stream";
        }

        private string FormatearTamano(long bytes)
        {
            string[] unidades = { "B", "KB", "MB", "GB" };
            double tamano = bytes;
            int unidadIndex = 0;

            while (tamano >= 1024 && unidadIndex < unidades.Length - 1)
            {
                tamano /= 1024;
                unidadIndex++;
            }

            return $"{tamano:F2} {unidades[unidadIndex]}";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Método para validar el archivo seleccionado
        private bool ValidarArchivo(string rutaArchivo)
        {
            try
            {
                // Verificar que la ruta no esté vacía
                if (string.IsNullOrWhiteSpace(rutaArchivo))
                {
                    lblMensaje.Text = "No se ha seleccionado ningún archivo.";
                    lblMensaje.ForeColor = Color.Red;
                    return false;
                }

                FileInfo fileInfo = new FileInfo(rutaArchivo);

                // Verificar que el archivo existe
                if (!fileInfo.Exists)
                {
                    lblMensaje.Text = "El archivo seleccionado no existe.";
                    lblMensaje.ForeColor = Color.Red;
                    return false;
                }

                // Verificar que no sea un directorio
                if ((File.GetAttributes(rutaArchivo) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    lblMensaje.Text = "No se puede seleccionar una carpeta, debe ser un archivo.";
                    lblMensaje.ForeColor = Color.Red;
                    return false;
                }

                // Verificar tamaño (1GB máximo)
                if (fileInfo.Length > 1L * 1024 * 1024 * 1024)
                {
                    lblMensaje.Text = $"El archivo es demasiado grande ({FormatearTamano(fileInfo.Length)}). Máximo permitido: 1GB.";
                    lblMensaje.ForeColor = Color.Red;
                    return false;
                }

                // Verificar que el archivo no esté vacío
                if (fileInfo.Length == 0)
                {
                    lblMensaje.Text = "El archivo está vacío.";
                    lblMensaje.ForeColor = Color.Red;
                    return false;
                }

                // Verificar extensiones permitidas (opcional)
                string extension = fileInfo.Extension.ToLower();
                string[] extensionesPermitidas = {
                    ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
                    ".txt", ".jpg", ".jpeg", ".png", ".gif", ".bmp",
                    ".zip", ".rar", ".mp3", ".mp4", ".wav"
                };

                bool extensionValida = false;
                foreach (string ext in extensionesPermitidas)
                {
                    if (extension == ext)
                    {
                        extensionValida = true;
                        break;
                    }
                }

                if (!extensionValida)
                {
                    lblMensaje.Text = $"Tipo de archivo no permitido: {extension}";
                    lblMensaje.ForeColor = Color.Red;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al validar el archivo: " + ex.Message;
                lblMensaje.ForeColor = Color.Red;
                return false;
            }
        }

        // Evento que se ejecuta cuando cambia el texto del título
        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {
            // Limpiar mensaje de error cuando el usuario empiece a escribir
            if (lblMensaje.ForeColor == Color.Red)
            {
                lblMensaje.Text = "";
            }
        }

        // Evento que se ejecuta cuando cambia el texto de la descripción
        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            // Limpiar mensaje de error cuando el usuario empiece a escribir
            if (lblMensaje.ForeColor == Color.Red)
            {
                lblMensaje.Text = "";
            }
        }
    }
}