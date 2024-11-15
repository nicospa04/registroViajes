using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroViajes
{
    public partial class BackUp : Form,IObserver
    {
        public BackUp()
        {
            InitializeComponent();
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void BackUp_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }

        private void btnSeleccionarBackUp_Click(object sender, EventArgs e)
        {
            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "Back Up";
            int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha1 = DateTime.Now;
            int criticidad = 12;

            string actor;
            if (id_usuario1 == 3)
                actor = "ADMIN";
            else if (id_usuario1 == 2)
                actor = "EMPLEADO";
            else
                actor = "USUARIO";

            BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
            bllbita.crearEntidad(bitacorita);

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    
                    textBox1.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "Restore";
            int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha1 = DateTime.Now;
            int criticidad = 13;

            string actor;
            if (id_usuario1 == 3)
                actor = "ADMIN";
            else if (id_usuario1 == 2)
                actor = "EMPLEADO";
            else
                actor = "USUARIO";

            BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
            bllbita.crearEntidad(bitacorita);

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "SQL Backup Files (*.bak)|*.bak";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = openFileDialog.FileName;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != string.Empty)
            {
                   string nombreArchivo = $"MiSistema.BCK_{DateTime.Now:ddMMyy_HHmm}.bak";
                    string rutaCompleta = System.IO.Path.Combine(textBox1.Text, nombreArchivo);
                    string comandoBackup = $"BACKUP DATABASE SistemaViajes TO DISK='{rutaCompleta}'";


                     string dataSource = "090L3PC16-80598";
            string dbName = "SistemaViajes";
              string conexionMaster = $"Data source={dataSource};Initial Catalog=master;Integrated Security=True;";
          

                    using (SqlConnection conn = new SqlConnection(conexionMaster))
                    {
                        SqlCommand cmd = new SqlCommand(comandoBackup, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string dataSource = "090L3PC16-80598";
            string dbName = "SistemaViajes";
            string conexionMaster = $"Data source={dataSource};Initial Catalog=master;Integrated Security=True;";



            try
            {
                using (SqlConnection conn = new SqlConnection(conexionMaster))
                {
                    conn.Open();

                    using (SqlCommand setMaster = new SqlCommand("USE master;", conn))
                    {
                        setMaster.ExecuteNonQuery();
                    }

                    using (SqlCommand setSingleUser = new SqlCommand("ALTER DATABASE SistemaViajes SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", conn))
                    {
                        setSingleUser.ExecuteNonQuery();
                    }

                    string query = $"RESTORE DATABASE SistemaViajes FROM DISK = '{textBox2.Text}' WITH REPLACE;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Se restauró");
                    }

                    using (SqlCommand setMultiUser = new SqlCommand("ALTER DATABASE SistemaViajes SET MULTI_USER;", conn))
                    {
                        setMultiUser.ExecuteNonQuery();
                    }

               
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al restaurar la base de datos: {ex.Message}");
            }
        }
    }
}
