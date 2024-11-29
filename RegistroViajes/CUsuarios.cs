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
    public partial class CUsuarios : Form,IObserver
    {
        public CUsuarios()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
            leerdata();
            hh();
        }
        private void leerdata()
        {
            List<BE.Usuario> users = new List<BE.Usuario>();
            BLL.BLLUsuario bll = new BLL.BLLUsuario();
            users = bll.leerEntidades();
            dataGridView1.DataSource = users;
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        private void CUsuarios_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
        BLLPerfil bllp = new BLLPerfil();
        BLLUsuario blluser = new BLLUsuario();
        private void hh()
        {
            List<BEPerfil> listaPerfiles = bllp.cargarCBPerfil();
            comboBox1.DataSource = listaPerfiles;
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "id_permiso";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener la fila que fue clickeada
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                int user = fila.Cells["id_usuario"].Value != null ? int.Parse(fila.Cells["id_usuario"].Value.ToString()) : 0;
                //int dni = fila.Cells["dni"].Value != null ? int.Parse(fila.Cells["dni"].Value.ToString()) : 0;
                string name = fila.Cells["nombre"].Value != null ? fila.Cells["nombre"].Value.ToString() : "";
                //string pass = fila.Cells["contraseña"].Value != null ? fila.Cells["contraseña"].Value.ToString() : "";
                //string ape = fila.Cells["apellido"].Value != null ? fila.Cells["apellido"].Value.ToString() : "";
                //string tele = fila.Cells["telefono"].Value != null ? fila.Cells["telefono"].Value.ToString() : "";
                //string email = fila.Cells["email"].Value != null ? fila.Cells["email"].Value.ToString() : "";
                //DateTime fechanaci = fila.Cells["fecha_nacimiento"].Value != null ? DateTime.Parse(fila.Cells["fecha_nacimiento"].Value.ToString()) : DateTime.Now;
                //int id_perfil = fila.Cells["id_familia"].Value != null ? int.Parse(fila.Cells["id_familia"].Value.ToString()) : 0;
                textBox1.Text = name;
                textBox2.Text = user.ToString();
                int idpermi = blluser.DevolverIdPermisoPorId(user);
                textBox3.Text = blluser.obtenernamepermisoporID(idpermi.ToString());
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int user = Convert.ToInt32(textBox2.Text); // ID del usuario
                int idperfil = (int)comboBox1.SelectedValue; // ID del permiso seleccionado
                string query = $"USE SistemaViajes; UPDATE UsuarioPermiso SET id_permiso = @IdPermiso WHERE id_usuario = @IdUsuario";
                string dataSource = "DESKTOP-Q714KGU\\SQLEXPRESS";
                string conexionMaster = $"Data source={dataSource};Initial Catalog=SistemaViajes;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(conexionMaster))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", user);
                        cmd.Parameters.AddWithValue("@IdPermiso", idperfil);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El Perfil fue agregado Correctamente.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            changeperfil();
            leerdata();
        }
        private void changeperfil()
        {
            try
            {
                int userId = Convert.ToInt32(textBox2.Text); // ID del usuario
                int perfilId = (int)comboBox1.SelectedValue; // ID del perfil seleccionado
                string query = $"USE SistemaViajes; UPDATE Usuario SET id_familia = @PerfilId WHERE id_usuario = @UserId";
                string dataSource = "DESKTOP-Q714KGU\\SQLEXPRESS";
                string conexionMaster = $"Data source={dataSource};Initial Catalog=SistemaViajes;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(conexionMaster))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@PerfilId", perfilId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
