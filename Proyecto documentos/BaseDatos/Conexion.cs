using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Proyecto_documentos.BaseDatos
{
    public class Conexion
    {
        private string cadenaConexion;

        public Conexion()
        {
            cadenaConexion = "Server=DESKTOP-E8CTSRV;Database=pruebaaa;Trusted_Connection=True;";
        }

        public bool ProbarConexion()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    Console.WriteLine("Conexi�n exitosa");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public DataTable EjecutarConsulta(string consulta)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en consulta: " + ex.Message);
            }
            return dt;
        }

        public int EjecutarComando(string consulta)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en comando: " + ex.Message);
                return -1;
            }
        }

        // Nuevos m�todos para manejo de BLOB y operaciones as�ncronas

        public async Task<int> EjecutarComandoConParametrosAsync(string consulta, SqlParameter[] parametros)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    await conexion.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }
                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en comando con par�metros: " + ex.Message);
                return -1;
            }
        }

        public async Task<object> EjecutarEscalarAsync(string consulta, SqlParameter[] parametros = null)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    await conexion.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }
                        return await cmd.ExecuteScalarAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en consulta escalar: " + ex.Message);
                return null;
            }
        }

        public async Task<byte[]> ObtenerBlobAsync(string consulta, SqlParameter[] parametros)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    await conexion.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }

                        object resultado = await cmd.ExecuteScalarAsync();

                        if (resultado != null && resultado != DBNull.Value)
                        {
                            return (byte[])resultado;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener BLOB: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> EjecutarTransaccionAsync(Func<SqlConnection, SqlTransaction, Task<bool>> operacion)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    await conexion.OpenAsync();
                    using (SqlTransaction transaction = conexion.BeginTransaction())
                    {
                        try
                        {
                            bool resultado = await operacion(conexion, transaction);
                            if (resultado)
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en transacci�n: " + ex.Message);
                return false;
            }
        }

        // M�todo para obtener la cadena de conexi�n (para compatibilidad)
        public string ObtenerCadenaConexion()
        {
            return cadenaConexion;
        }
    }
}