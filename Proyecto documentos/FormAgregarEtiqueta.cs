using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using Proyecto_documentos.BaseDatos;

namespace Proyecto_documentos
{
    public partial class FormAgregarEtiqueta : Form
    {
        private Conexion conexionBD;
        private int idDocumento;

        // Clase para manejar los items del ComboBox
        public class EtiquetaComboItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }

            public override string ToString()
            {
                return Nombre;
            }
        }

        public FormAgregarEtiqueta(Conexion conexion, int documentoId)
        {
            InitializeComponent();
            conexionBD = conexion;
            idDocumento = documentoId;

            ConfigurarFormulario();
            CargarEtiquetasDisponibles();
        }

        private void ConfigurarFormulario()
        {
            // Configurar ComboBox
            cmbEtiquetasExistentes.DisplayMember = "Nombre";
            cmbEtiquetasExistentes.ValueMember = "Id";

            // Configurar estado inicial
            ActualizarEstadoControles();
        }

        private void CargarEtiquetasDisponibles()
        {
            try
            {
                // Obtener etiquetas que NO están asignadas al documento actual
                string consulta = @"
                    SELECT e.id_etiqueta, e.nombre 
                    FROM Etiqueta e
                    WHERE e.id_etiqueta NOT IN (
                        SELECT de.id_etiqueta 
                        FROM DocumentoEtiqueta de 
                        WHERE de.id_documento = " + idDocumento + @"
                    )
                    ORDER BY e.nombre";

                DataTable resultado = conexionBD.EjecutarConsulta(consulta);

                cmbEtiquetasExistentes.Items.Clear();

                if (resultado != null && resultado.Rows.Count > 0)
                {
                    foreach (DataRow row in resultado.Rows)
                    {
                        EtiquetaComboItem etiqueta = new EtiquetaComboItem
                        {
                            Id = Convert.ToInt32(row["id_etiqueta"]),
                            Nombre = row["nombre"].ToString()
                        };
                        cmbEtiquetasExistentes.Items.Add(etiqueta);
                    }

                    if (cmbEtiquetasExistentes.Items.Count > 0)
                    {
                        cmbEtiquetasExistentes.SelectedIndex = 0;
                    }
                }

                // Si no hay etiquetas disponibles, deshabilitar la opción
                if (cmbEtiquetasExistentes.Items.Count == 0)
                {
                    rbSeleccionarExistente.Enabled = false;
                    rbCrearYAsignar.Checked = true;
                    lblMensaje.Text = "No hay etiquetas disponibles para asignar. Cree una nueva.";
                    lblMensaje.ForeColor = Color.Orange;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar etiquetas disponibles: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEstadoControles()
        {
            // Habilitar/deshabilitar controles según la opción seleccionada
            bool crearNueva = rbCrearYAsignar.Checked;

            txtNombreEtiqueta.Enabled = crearNueva;
            lblNombreEtiqueta.Enabled = crearNueva;

            cmbEtiquetasExistentes.Enabled = !crearNueva;
            lblEtiquetasExistentes.Enabled = !crearNueva;

            // Limpiar mensaje si hay
            if (lblMensaje.ForeColor != Color.Orange)
            {
                lblMensaje.Text = "";
            }
        }

        private void rbCrearYAsignar_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoControles();
            if (rbCrearYAsignar.Checked)
            {
                txtNombreEtiqueta.Focus();
            }
        }

        private void rbSeleccionarExistente_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoControles();
            if (rbSeleccionarExistente.Checked)
            {
                cmbEtiquetasExistentes.Focus();
            }
        }

        private void txtNombreEtiqueta_TextChanged(object sender, EventArgs e)
        {
            // Limpiar mensaje de error
            if (lblMensaje.ForeColor == Color.Red)
            {
                lblMensaje.Text = "";
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar según la opción seleccionada
                if (rbCrearYAsignar.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtNombreEtiqueta.Text))
                    {
                        lblMensaje.Text = "El nombre de la etiqueta es obligatorio.";
                        lblMensaje.ForeColor = Color.Red;
                        txtNombreEtiqueta.Focus();
                        return;
                    }

                    // Verificar si la etiqueta ya existe
                    if (await ExisteEtiqueta(txtNombreEtiqueta.Text.Trim()))
                    {
                        lblMensaje.Text = "Ya existe una etiqueta con ese nombre.";
                        lblMensaje.ForeColor = Color.Red;
                        txtNombreEtiqueta.Focus();
                        return;
                    }
                }
                else if (rbSeleccionarExistente.Checked)
                {
                    if (cmbEtiquetasExistentes.SelectedItem == null)
                    {
                        lblMensaje.Text = "Seleccione una etiqueta de la lista.";
                        lblMensaje.ForeColor = Color.Red;
                        cmbEtiquetasExistentes.Focus();
                        return;
                    }
                }

                // Deshabilitar controles durante la operación
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                lblMensaje.Text = "Procesando...";
                lblMensaje.ForeColor = Color.Blue;

                bool exito = false;

                if (rbCrearYAsignar.Checked)
                {
                    exito = await CrearYAsignarEtiqueta();
                }
                else
                {
                    exito = await AsignarEtiquetaExistente();
                }

                if (exito)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblMensaje.Text = "Error al procesar la etiqueta.";
                    lblMensaje.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = Color.Red;
                MessageBox.Show("Error al guardar etiqueta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
        }

        private async Task<bool> ExisteEtiqueta(string nombreEtiqueta)
        {
            try
            {
                string consulta = "SELECT COUNT(*) FROM Etiqueta WHERE LOWER(nombre) = LOWER(@Nombre)";
                SqlParameter[] parametros = {
                    new SqlParameter("@Nombre", nombreEtiqueta)
                };

                object resultado = await conexionBD.EjecutarEscalarAsync(consulta, parametros);
                return Convert.ToInt32(resultado) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar etiqueta existente: {ex.Message}", ex);
            }
        }

        private async Task<bool> CrearYAsignarEtiqueta()
        {
            try
            {
                return await conexionBD.EjecutarTransaccionAsync(async (conexion, transaccion) =>
                {
                    // Crear la nueva etiqueta
                    string consultaCrear = @"
                        INSERT INTO Etiqueta (nombre) 
                        OUTPUT INSERTED.id_etiqueta
                        VALUES (@Nombre)";

                    using (SqlCommand cmdCrear = new SqlCommand(consultaCrear, conexion, transaccion))
                    {
                        cmdCrear.Parameters.AddWithValue("@Nombre", txtNombreEtiqueta.Text.Trim());
                        object resultado = await cmdCrear.ExecuteScalarAsync();

                        if (resultado != null)
                        {
                            int idEtiquetaNueva = Convert.ToInt32(resultado);

                            // Asignar la etiqueta al documento
                            string consultaAsignar = @"
                                INSERT INTO DocumentoEtiqueta (id_documento, id_etiqueta)
                                VALUES (@IdDocumento, @IdEtiqueta)";

                            using (SqlCommand cmdAsignar = new SqlCommand(consultaAsignar, conexion, transaccion))
                            {
                                cmdAsignar.Parameters.AddWithValue("@IdDocumento", idDocumento);
                                cmdAsignar.Parameters.AddWithValue("@IdEtiqueta", idEtiquetaNueva);
                                int filasAfectadas = await cmdAsignar.ExecuteNonQueryAsync();
                                return filasAfectadas > 0;
                            }
                        }
                    }
                    return false;
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear y asignar etiqueta: {ex.Message}", ex);
            }
        }

        private async Task<bool> AsignarEtiquetaExistente()
        {
            try
            {
                EtiquetaComboItem etiquetaSeleccionada = (EtiquetaComboItem)cmbEtiquetasExistentes.SelectedItem;

                string consulta = @"
                    INSERT INTO DocumentoEtiqueta (id_documento, id_etiqueta)
                    VALUES (@IdDocumento, @IdEtiqueta)";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdDocumento", idDocumento),
                    new SqlParameter("@IdEtiqueta", etiquetaSeleccionada.Id)
                };

                int filasAfectadas = await conexionBD.EjecutarComandoConParametrosAsync(consulta, parametros);
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al asignar etiqueta existente: {ex.Message}", ex);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}