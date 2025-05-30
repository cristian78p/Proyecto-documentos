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
    public partial class FormDocumentos : Form
    {
        private Conexion conexionBD;
        private string usuarioActual;
        private bool filtrandoPorEtiqueta = false;
        private string terminoBusquedaActual = "";

        // Clase para manejar etiquetas en el ComboBox
        public class EtiquetaComboItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }

            public override string ToString()
            {
                return Nombre;
            }
        }

        public FormDocumentos(string email)
        {
            InitializeComponent();
            conexionBD = new Conexion();
            usuarioActual = email;
            lblUsuarioActual.Text = "Usuario: " + email;

            // Configurar controles
            ConfigurarBusquedaFiltros();

            // Cargar datos iniciales
            CargarEtiquetasComboBox();
            CargarDocumentos();
        }

        private void ConfigurarBusquedaFiltros()
        {
            // Configurar ComboBox de etiquetas
            cmbEtiquetas.DisplayMember = "Nombre";
            cmbEtiquetas.ValueMember = "Id";

            // Placeholder para el TextBox de búsqueda
            txtBusqueda.Text = "Buscar por título o descripción...";
            txtBusqueda.ForeColor = Color.Gray;
            txtBusqueda.Enter += TxtBusqueda_Enter;
            txtBusqueda.Leave += TxtBusqueda_Leave;
        }

        private void TxtBusqueda_Enter(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == "Buscar por título o descripción...")
            {
                txtBusqueda.Text = "";
                txtBusqueda.ForeColor = Color.Black;
            }
        }

        private void TxtBusqueda_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                txtBusqueda.Text = "Buscar por título o descripción...";
                txtBusqueda.ForeColor = Color.Gray;
            }
        }

        private void CargarEtiquetasComboBox()
        {
            try
            {
                // Limpiar ComboBox
                cmbEtiquetas.Items.Clear();

                // Agregar opción "Todas"
                cmbEtiquetas.Items.Add(new EtiquetaComboItem { Id = -1, Nombre = "Todas las etiquetas" });

                // Obtener etiquetas que tienen documentos del usuario actual
                string consulta = @"
                    SELECT DISTINCT e.id_etiqueta, e.nombre 
                    FROM Etiqueta e
                    INNER JOIN DocumentoEtiqueta de ON e.id_etiqueta = de.id_etiqueta
                    INNER JOIN Documento d ON de.id_documento = d.id_documento
                    INNER JOIN Usuario u ON d.id_creador = u.id_usuario
                    WHERE u.correo = '" + usuarioActual + @"'
                    ORDER BY e.nombre";

                DataTable etiquetas = conexionBD.EjecutarConsulta(consulta);

                if (etiquetas != null && etiquetas.Rows.Count > 0)
                {
                    foreach (DataRow row in etiquetas.Rows)
                    {
                        cmbEtiquetas.Items.Add(new EtiquetaComboItem
                        {
                            Id = Convert.ToInt32(row["id_etiqueta"]),
                            Nombre = row["nombre"].ToString()
                        });
                    }
                }

                // Seleccionar "Todas las etiquetas" por defecto
                cmbEtiquetas.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar etiquetas: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picDocumento_Paint(object sender, PaintEventArgs e)
        {
            // Dibujar un ícono de documento similar a los otros formularios
            e.Graphics.FillRectangle(Brushes.White, 25, 10, 50, 70);
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(52, 152, 219), 2), 25, 10, 50, 70);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 45, 10, 45, 25);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 45, 25, 75, 25);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 35, 40, 65, 40);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 35, 50, 65, 50);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(52, 152, 219), 2), 35, 60, 65, 60);
        }

        private void CargarDocumentos()
        {
            try
            {
                // Limpiar tabla
                dgvDocumentos.Rows.Clear();

                // Construir consulta base
                string consultaBase = @"
                    SELECT 
                        d.id_documento, 
                        d.titulo, 
                        d.descripcion, 
                        d.fecha_creacion, 
                        d.fecha_modificacion, 
                        d.formato_archivo, 
                        d.tamano_bytes, 
                        d.nombre_original, 
                        d.tipo_mime,
                        STUFF((
                            SELECT ', ' + e.nombre
                            FROM DocumentoEtiqueta de
                            INNER JOIN Etiqueta e ON de.id_etiqueta = e.id_etiqueta
                            WHERE de.id_documento = d.id_documento
                            ORDER BY e.nombre
                            FOR XML PATH('')
                        ), 1, 2, '') AS etiquetas
                    FROM Documento d
                    JOIN Usuario u ON d.id_creador = u.id_usuario
                    WHERE u.correo = '{0}' AND d.contenido_archivo IS NOT NULL";

                string consulta = string.Format(consultaBase, usuarioActual);

                // Agregar filtros si existen
                if (!string.IsNullOrEmpty(terminoBusquedaActual))
                {
                    consulta += string.Format(@" AND (d.titulo LIKE '%{0}%' OR d.descripcion LIKE '%{0}%')",
                        terminoBusquedaActual.Replace("'", "''"));
                }

                if (filtrandoPorEtiqueta && cmbEtiquetas.SelectedItem != null)
                {
                    EtiquetaComboItem etiquetaSeleccionada = (EtiquetaComboItem)cmbEtiquetas.SelectedItem;
                    if (etiquetaSeleccionada.Id != -1)
                    {
                        consulta += string.Format(@" AND d.id_documento IN (
                            SELECT de.id_documento 
                            FROM DocumentoEtiqueta de 
                            WHERE de.id_etiqueta = {0})", etiquetaSeleccionada.Id);
                    }
                }

                consulta += " ORDER BY d.fecha_creacion DESC";

                DataTable datosDocumentos = conexionBD.EjecutarConsulta(consulta);

                if (datosDocumentos != null && datosDocumentos.Rows.Count > 0)
                {
                    foreach (DataRow row in datosDocumentos.Rows)
                    {
                        // Obtener etiquetas directamente de la consulta
                        string etiquetas = row["etiquetas"] != DBNull.Value ?
                            row["etiquetas"].ToString() : "-";

                        // Formatear tamaño del archivo
                        string tamanoFormateado = "N/A";
                        if (row["tamano_bytes"] != DBNull.Value)
                        {
                            long tamanoBytes = Convert.ToInt64(row["tamano_bytes"]);
                            tamanoFormateado = FormatearTamano(tamanoBytes);
                        }

                        // Obtener nombre original o usar título + formato
                        string nombreArchivo = row["nombre_original"] != DBNull.Value ?
                            row["nombre_original"].ToString() :
                            $"{row["titulo"]}.{row["formato_archivo"]}";

                        // Agregar a la tabla con los nuevos campos
                        dgvDocumentos.Rows.Add(
                            row["id_documento"],
                            row["titulo"],
                            row["descripcion"],
                            Convert.ToDateTime(row["fecha_creacion"]).ToString("dd/MM/yyyy"),
                            row["fecha_modificacion"] == DBNull.Value ? "-" :
                                Convert.ToDateTime(row["fecha_modificacion"]).ToString("dd/MM/yyyy"),
                            row["formato_archivo"],
                            etiquetas,
                            tamanoFormateado,
                            nombreArchivo
                        );
                    }
                }

                // Actualizar contador de resultados
                ActualizarContadorResultados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar documentos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarContadorResultados()
        {
            int totalDocumentos = dgvDocumentos.Rows.Count;

            if (filtrandoPorEtiqueta || !string.IsNullOrEmpty(terminoBusquedaActual))
            {
                lblResultados.Text = $"{totalDocumentos} resultado(s)";
                lblResultados.ForeColor = Color.FromArgb(40, 167, 69); // Verde para resultados filtrados
            }
            else
            {
                lblResultados.Text = $"{totalDocumentos} documento(s)";
                lblResultados.ForeColor = Color.FromArgb(108, 117, 125); // Gris para total normal
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            EjecutarBusqueda();
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir búsqueda con Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                EjecutarBusqueda();
                e.Handled = true;
            }
        }

        private void EjecutarBusqueda()
        {
            try
            {
                // Obtener término de búsqueda
                string busqueda = txtBusqueda.Text.Trim();

                // No buscar si es el placeholder
                if (busqueda == "Buscar por título o descripción...")
                {
                    busqueda = "";
                }

                terminoBusquedaActual = busqueda;

                // Recargar documentos con filtro
                CargarDocumentos();

                // Mostrar mensaje si no hay resultados
                if (dgvDocumentos.Rows.Count == 0 && !string.IsNullOrEmpty(terminoBusquedaActual))
                {
                    MessageBox.Show($"No se encontraron documentos que contengan '{terminoBusquedaActual}'",
                        "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar búsqueda: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbEtiquetas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbEtiquetas.SelectedItem != null)
                {
                    EtiquetaComboItem etiquetaSeleccionada = (EtiquetaComboItem)cmbEtiquetas.SelectedItem;

                    // Si selecciona "Todas las etiquetas", desactivar filtro
                    if (etiquetaSeleccionada.Id == -1)
                    {
                        filtrandoPorEtiqueta = false;
                    }
                    else
                    {
                        filtrandoPorEtiqueta = true;
                    }

                    // Recargar documentos con filtro
                    CargarDocumentos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar por etiqueta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar búsqueda
                txtBusqueda.Text = "Buscar por título o descripción...";
                txtBusqueda.ForeColor = Color.Gray;
                terminoBusquedaActual = "";

                // Limpiar filtro de etiqueta
                cmbEtiquetas.SelectedIndex = 0; // Seleccionar "Todas las etiquetas"
                filtrandoPorEtiqueta = false;

                // Recargar todos los documentos
                CargarDocumentos();

                MessageBox.Show("Filtros limpiados. Mostrando todos los documentos.", "Filtros",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar filtros: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDocumentos_SelectionChanged(object sender, EventArgs e)
        {
            // Habilitar/deshabilitar botones según la selección
            bool haySeleccion = dgvDocumentos.SelectedRows.Count > 0;
            btnDescargar.Enabled = haySeleccion;
            btnEliminar.Enabled = haySeleccion;
            btnEditar.Enabled = haySeleccion;
            btnVerEtiquetas.Enabled = haySeleccion;
        }

        private void btnVerEtiquetas_Click(object sender, EventArgs e)
        {
            if (dgvDocumentos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un documento para gestionar sus etiquetas.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Obtener información del documento seleccionado
                DataGridViewRow filaSeleccionada = dgvDocumentos.SelectedRows[0];
                int idDocumento = Convert.ToInt32(filaSeleccionada.Cells[0].Value); // id_documento
                string titulo = filaSeleccionada.Cells[1].Value.ToString(); // titulo

                // Abrir formulario de gestión de etiquetas
                FormGestionarEtiquetas formEtiquetas = new FormGestionarEtiquetas(conexionBD, idDocumento, titulo);
                if (formEtiquetas.ShowDialog() == DialogResult.OK)
                {
                    // Recargar la lista de documentos para actualizar las etiquetas mostradas
                    CargarDocumentos();
                    // Recargar también el ComboBox de etiquetas por si se agregaron nuevas
                    CargarEtiquetasComboBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al gestionar etiquetas: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormAgregarDocumento form = new FormAgregarDocumento(conexionBD, usuarioActual);
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarDocumentos();
                CargarEtiquetasComboBox(); // Recargar etiquetas por si se agregaron nuevas
            }
        }

        private async void btnDescargar_Click(object sender, EventArgs e)
        {
            if (dgvDocumentos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un documento para descargar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Obtener información del documento seleccionado
                DataGridViewRow filaSeleccionada = dgvDocumentos.SelectedRows[0];
                int idDocumento = Convert.ToInt32(filaSeleccionada.Cells[0].Value); // id_documento
                string titulo = filaSeleccionada.Cells[1].Value.ToString(); // titulo
                string formato = filaSeleccionada.Cells[5].Value.ToString(); // formato_archivo

                // Intentar obtener el nombre original (si existe en tu DataGridView)
                string nombreOriginal = "";
                if (filaSeleccionada.Cells.Count > 8)
                {
                    nombreOriginal = filaSeleccionada.Cells[8].Value?.ToString() ?? "";
                }

                // Determinar nombre del archivo para guardar
                string nombreArchivo = !string.IsNullOrEmpty(nombreOriginal) ?
                    nombreOriginal : $"{titulo}.{formato}";

                await DescargarDocumento(idDocumento, nombreArchivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar documento: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DescargarDocumento(int idDocumento, string nombreArchivo)
        {
            try
            {
                // Mostrar diálogo para seleccionar ubicación de descarga
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = nombreArchivo;
                    saveFileDialog.Filter = "Todos los archivos (*.*)|*.*";
                    saveFileDialog.Title = "Guardar documento como...";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Mostrar indicador de progreso
                        this.Cursor = Cursors.WaitCursor;
                        btnDescargar.Enabled = false;
                        btnDescargar.Text = "Descargando...";

                        // Obtener el archivo de la base de datos usando tu clase Conexion
                        byte[] contenidoArchivo = await ObtenerContenidoArchivo(idDocumento);

                        if (contenidoArchivo != null && contenidoArchivo.Length > 0)
                        {
                            // Guardar archivo en la ubicación seleccionada
                            File.WriteAllBytes(saveFileDialog.FileName, contenidoArchivo);

                            MessageBox.Show("Documento descargado exitosamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Preguntar si desea abrir el archivo
                            DialogResult resultado = MessageBox.Show("¿Desea abrir el archivo descargado?",
                                "Abrir archivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (resultado == DialogResult.Yes)
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("No se pudo abrir el archivo: " + ex.Message,
                                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se pudo obtener el contenido del documento.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar documento: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnDescargar.Enabled = true;
                btnDescargar.Text = "Descargar";
            }
        }

        private async Task<byte[]> ObtenerContenidoArchivo(int idDocumento)
        {
            try
            {
                // Usar tu clase Conexion para obtener el BLOB
                string consulta = "SELECT contenido_archivo FROM Documento WHERE id_documento = @IdDocumento";
                SqlParameter[] parametros = {
                    new SqlParameter("@IdDocumento", idDocumento)
                };

                return await conexionBD.ObtenerBlobAsync(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener contenido del archivo: {ex.Message}", ex);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDocumentos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un documento para eliminar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Obtener información del documento seleccionado
                DataGridViewRow filaSeleccionada = dgvDocumentos.SelectedRows[0];
                int idDocumento = Convert.ToInt32(filaSeleccionada.Cells[0].Value); // id_documento
                string titulo = filaSeleccionada.Cells[1].Value.ToString(); // titulo

                // Confirmar eliminación
                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar el documento '{titulo}'?\n\nEsta acción no se puede deshacer.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    await EliminarDocumento(idDocumento, titulo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar documento: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task EliminarDocumento(int idDocumento, string titulo)
        {
            try
            {
                // Mostrar indicador de progreso
                this.Cursor = Cursors.WaitCursor;
                btnEliminar.Enabled = false;
                btnEliminar.Text = "Eliminando...";

                // Usar tu clase Conexion con transacción
                bool exito = await conexionBD.EjecutarTransaccionAsync(async (conexion, transaccion) =>
                {
                    try
                    {
                        // Primero eliminar relaciones con etiquetas
                        string consultaEtiquetas = "DELETE FROM DocumentoEtiqueta WHERE id_documento = @IdDocumento";
                        using (SqlCommand cmdEtiquetas = new SqlCommand(consultaEtiquetas, conexion, transaccion))
                        {
                            cmdEtiquetas.Parameters.AddWithValue("@IdDocumento", idDocumento);
                            await cmdEtiquetas.ExecuteNonQueryAsync();
                        }

                        // Eliminar programaciones de envío (si existen)
                        string consultaProgramacion = "DELETE FROM ProgramacionEnvio WHERE documento_id = @IdDocumento";
                        using (SqlCommand cmdProgramacion = new SqlCommand(consultaProgramacion, conexion, transaccion))
                        {
                            cmdProgramacion.Parameters.AddWithValue("@IdDocumento", idDocumento);
                            await cmdProgramacion.ExecuteNonQueryAsync();
                        }

                        // Finalmente eliminar el documento
                        string consultaDocumento = "DELETE FROM Documento WHERE id_documento = @IdDocumento";
                        using (SqlCommand cmdDocumento = new SqlCommand(consultaDocumento, conexion, transaccion))
                        {
                            cmdDocumento.Parameters.AddWithValue("@IdDocumento", idDocumento);
                            int filasAfectadas = await cmdDocumento.ExecuteNonQueryAsync();
                            return filasAfectadas > 0;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                });

                if (exito)
                {
                    MessageBox.Show($"Documento '{titulo}' eliminado exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar la lista de documentos y etiquetas
                    CargarDocumentos();
                    CargarEtiquetasComboBox();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el documento.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar documento: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnEliminar.Enabled = true;
                btnEliminar.Text = "Eliminar";
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDocumentos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un documento para editar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Obtener información del documento seleccionado
                DataGridViewRow filaSeleccionada = dgvDocumentos.SelectedRows[0];
                int idDocumento = Convert.ToInt32(filaSeleccionada.Cells[0].Value);

                // Abrir formulario de edición
                FormEditarDocumento formEditar = new FormEditarDocumento(conexionBD, idDocumento);
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    // Recargar la lista de documentos si se guardaron cambios
                    CargarDocumentos();
                    MessageBox.Show("Los cambios se han guardado correctamente.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar documento: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDocumentos_Load(object sender, EventArgs e)
        {

        }
    }
}