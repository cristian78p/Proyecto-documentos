using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using Proyecto_documentos.BaseDatos;

namespace Proyecto_documentos
{
    public partial class FormEditarDocumento : Form
    {
        private Conexion conexionBD;
        private int idDocumento;
        private string tituloOriginal;
        private string descripcionOriginal;

        public FormEditarDocumento(Conexion conexion, int documentoId)
        {
            InitializeComponent();
            conexionBD = conexion;
            idDocumento = documentoId;

            CargarDatosDocumento();
        }

        private void picDocumento_Paint(object sender, PaintEventArgs e)
        {
            // Dibujar un ícono de documento similar al diseño principal
            e.Graphics.FillRectangle(Brushes.White, 15, 5, 40, 40);
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(52, 152, 219), 2), 15, 5, 40, 40);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 30, 5, 30, 15);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 30, 15, 55, 15);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 20, 25, 50, 25);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 20, 30, 50, 30);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 20, 35, 50, 35);
        }

        private void CargarDatosDocumento()
        {
            try
            {
                string consulta = @"
                    SELECT titulo, descripcion, fecha_creacion, formato_archivo, 
                           tamano_bytes, nombre_original, tipo_mime
                    FROM Documento 
                    WHERE id_documento = " + idDocumento;

                DataTable resultado = conexionBD.EjecutarConsulta(consulta);

                if (resultado != null && resultado.Rows.Count > 0)
                {
                    DataRow row = resultado.Rows[0];

                    // Cargar datos editables
                    txtTitulo.Text = row["titulo"].ToString();
                    txtDescripcion.Text = row["descripcion"].ToString();

                    // Guardar valores originales para comparación
                    tituloOriginal = txtTitulo.Text;
                    descripcionOriginal = txtDescripcion.Text;

                    // Cargar información del archivo (solo lectura)
                    lblFechaCreacion.Text = Convert.ToDateTime(row["fecha_creacion"]).ToString("dd/MM/yyyy HH:mm");
                    lblFormatoArchivo.Text = row["formato_archivo"]?.ToString() ?? "N/A";
                    lblNombreOriginal.Text = row["nombre_original"]?.ToString() ?? "N/A";

                    // Formatear tamaño del archivo
                    if (row["tamano_bytes"] != DBNull.Value)
                    {
                        long tamanoBytes = Convert.ToInt64(row["tamano_bytes"]);
                        lblTamanoArchivo.Text = FormatearTamano(tamanoBytes);
                    }
                    else
                    {
                        lblTamanoArchivo.Text = "N/A";
                    }

                    // Limpiar mensaje
                    lblMensaje.Text = "";
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información del documento.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Abort;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del documento: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
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

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                lblMensaje.Text = "El título es obligatorio.";
                lblMensaje.ForeColor = Color.Red;
                txtTitulo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblMensaje.Text = "La descripción es obligatoria.";
                lblMensaje.ForeColor = Color.Red;
                txtDescripcion.Focus();
                return;
            }

            // Verificar si hay cambios
            if (txtTitulo.Text.Trim() == tituloOriginal && txtDescripcion.Text.Trim() == descripcionOriginal)
            {
                lblMensaje.Text = "No se detectaron cambios.";
                lblMensaje.ForeColor = Color.Orange;
                return;
            }

            try
            {
                // Deshabilitar controles durante la actualización
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                txtTitulo.Enabled = false;
                txtDescripcion.Enabled = false;
                lblMensaje.Text = "Guardando cambios...";
                lblMensaje.ForeColor = Color.Blue;

                // Actualizar documento
                bool exito = await ActualizarDocumento();

                if (exito)
                {
                    MessageBox.Show("Documento actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblMensaje.Text = "Error al actualizar el documento.";
                    lblMensaje.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
                lblMensaje.ForeColor = Color.Red;
                MessageBox.Show("Error al actualizar documento: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Rehabilitar controles
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                txtTitulo.Enabled = true;
                txtDescripcion.Enabled = true;
            }
        }

        private async Task<bool> ActualizarDocumento()
        {
            try
            {
                string consulta = @"
                    UPDATE Documento 
                    SET titulo = @Titulo, 
                        descripcion = @Descripcion, 
                        fecha_modificacion = @FechaModificacion
                    WHERE id_documento = @IdDocumento";

                SqlParameter[] parametros = {
                    new SqlParameter("@Titulo", txtTitulo.Text.Trim()),
                    new SqlParameter("@Descripcion", txtDescripcion.Text.Trim()),
                    new SqlParameter("@FechaModificacion", DateTime.Now),
                    new SqlParameter("@IdDocumento", idDocumento)
                };

                int filasAfectadas = await conexionBD.EjecutarComandoConParametrosAsync(consulta, parametros);
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar en la base de datos: {ex.Message}", ex);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Verificar si hay cambios sin guardar
            if (txtTitulo.Text.Trim() != tituloOriginal || txtDescripcion.Text.Trim() != descripcionOriginal)
            {
                DialogResult resultado = MessageBox.Show(
                    "Hay cambios sin guardar. ¿Está seguro de que desea cancelar?",
                    "Confirmar cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {
            // Limpiar mensaje de error cuando el usuario empiece a escribir
            if (lblMensaje.ForeColor == Color.Red)
            {
                lblMensaje.Text = "";
            }

            // Indicar si hay cambios
            MostrarEstadoCambios();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            // Limpiar mensaje de error cuando el usuario empiece a escribir
            if (lblMensaje.ForeColor == Color.Red)
            {
                lblMensaje.Text = "";
            }

            // Indicar si hay cambios
            MostrarEstadoCambios();
        }

        private void MostrarEstadoCambios()
        {
            // Verificar si hay cambios
            bool hayCambios = txtTitulo.Text.Trim() != tituloOriginal ||
                             txtDescripcion.Text.Trim() != descripcionOriginal;

            if (hayCambios && lblMensaje.ForeColor != Color.Red)
            {
                lblMensaje.Text = "* Hay cambios sin guardar";
                lblMensaje.ForeColor = Color.Orange;
            }
            else if (!hayCambios && lblMensaje.ForeColor == Color.Orange)
            {
                lblMensaje.Text = "";
            }
        }

        private void FormEditarDocumento_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Verificar cambios al cerrar el formulario
            if (this.DialogResult != DialogResult.OK &&
                (txtTitulo.Text.Trim() != tituloOriginal || txtDescripcion.Text.Trim() != descripcionOriginal))
            {
                DialogResult resultado = MessageBox.Show(
                    "Hay cambios sin guardar. ¿Está seguro de que desea cerrar?",
                    "Confirmar cierre",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}