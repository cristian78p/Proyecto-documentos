using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using Proyecto_documentos.BaseDatos;

namespace Proyecto_documentos
{
    public partial class FormGestionarEtiquetas : Form
    {
        private Conexion conexionBD;
        private int idDocumento;
        private string tituloDocumento;

        // Clase para manejar los items del ListBox
        public class EtiquetaItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }

            public override string ToString()
            {
                return Nombre;
            }
        }

        public FormGestionarEtiquetas(Conexion conexion, int documentoId, string titulo)
        {
            InitializeComponent();
            conexionBD = conexion;
            idDocumento = documentoId;
            tituloDocumento = titulo;

            // Configurar el formulario
            ConfigurarFormulario();
            CargarEtiquetasDocumento();
        }

        private void ConfigurarFormulario()
        {
            lblDocumentoTitulo.Text = $"Documento: {tituloDocumento}";

            // Configurar ListBox
            lstEtiquetas.DisplayMember = "Nombre";
            lstEtiquetas.ValueMember = "Id";

            // Inicialmente deshabilitar el botón quitar
            btnQuitarEtiqueta.Enabled = false;
        }

        private void CargarEtiquetasDocumento()
        {
            try
            {
                // Limpiar lista actual
                lstEtiquetas.Items.Clear();

                // Consulta para obtener las etiquetas del documento
                string consulta = @"
                    SELECT e.id_etiqueta, e.nombre 
                    FROM Etiqueta e
                    INNER JOIN DocumentoEtiqueta de ON e.id_etiqueta = de.id_etiqueta
                    WHERE de.id_documento = " + idDocumento + @"
                    ORDER BY e.nombre";

                DataTable resultado = conexionBD.EjecutarConsulta(consulta);

                if (resultado != null && resultado.Rows.Count > 0)
                {
                    foreach (DataRow row in resultado.Rows)
                    {
                        EtiquetaItem etiqueta = new EtiquetaItem
                        {
                            Id = Convert.ToInt32(row["id_etiqueta"]),
                            Nombre = row["nombre"].ToString()
                        };
                        lstEtiquetas.Items.Add(etiqueta);
                    }
                }

                // Actualizar estado de botones
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar etiquetas: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEstadoBotones()
        {
            btnQuitarEtiqueta.Enabled = lstEtiquetas.SelectedIndex >= 0;
        }

        private void lstEtiquetas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void btnAgregarEtiqueta_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir formulario para agregar nueva etiqueta
                FormAgregarEtiqueta formNueva = new FormAgregarEtiqueta(conexionBD, idDocumento);
                if (formNueva.ShowDialog() == DialogResult.OK)
                {
                    // Recargar la lista de etiquetas
                    CargarEtiquetasDocumento();
                    MessageBox.Show("Etiqueta agregada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar etiqueta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnQuitarEtiqueta_Click(object sender, EventArgs e)
        {
            if (lstEtiquetas.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una etiqueta para quitar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                EtiquetaItem etiquetaSeleccionada = (EtiquetaItem)lstEtiquetas.SelectedItem;

                // Confirmar eliminación
                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de que desea quitar la etiqueta '{etiquetaSeleccionada.Nombre}' de este documento?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Deshabilitar botones durante la operación
                    btnQuitarEtiqueta.Enabled = false;
                    btnAgregarEtiqueta.Enabled = false;

                    bool exito = await QuitarEtiquetaDocumento(etiquetaSeleccionada.Id);

                    if (exito)
                    {
                        MessageBox.Show("Etiqueta quitada correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarEtiquetasDocumento(); // Recargar lista
                    }
                    else
                    {
                        MessageBox.Show("Error al quitar la etiqueta.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar etiqueta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Rehabilitar botones
                btnAgregarEtiqueta.Enabled = true;
                ActualizarEstadoBotones();
            }
        }

        private async Task<bool> QuitarEtiquetaDocumento(int idEtiqueta)
        {
            try
            {
                // Solo quitar la relación documento-etiqueta, no eliminar la etiqueta
                string consulta = @"
                    DELETE FROM DocumentoEtiqueta 
                    WHERE id_documento = @IdDocumento AND id_etiqueta = @IdEtiqueta";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdDocumento", idDocumento),
                    new SqlParameter("@IdEtiqueta", idEtiqueta)
                };

                int filasAfectadas = await conexionBD.EjecutarComandoConParametrosAsync(consulta, parametros);
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al quitar etiqueta: {ex.Message}", ex);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}