using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Org.BouncyCastle.Crypto;
using Servicios;

namespace RegistroViajes
{
    public partial class FRMRegistrarCliente : Form, IObserver
    {
        BLLUsuario BLLUser = new BLLUsuario();
        BLLPerfil bllp = new BLLPerfil();
        public FRMRegistrarCliente()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
            hh();
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void hh()
        {
            List<BEPerfil> listaPerfiles = bllp.cargarCBPerfil();
            comboBox1.DataSource = listaPerfiles;
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "id_permiso";
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            string dniPattern = @"^\d{2}\.\d{3}\.\d{3}$";
            bool isValid = Regex.IsMatch(gmail.Text, emailPattern);
            bool isValid2 = Regex.IsMatch(this.dni.Text, dniPattern);
            if (!isValid)
            {
                MessageBox.Show("Debe ingresar un mail válido");
                return;
            }
            if (!isValid2)
            {
                MessageBox.Show("Debe ingresar un dni válido");
                return;
            }
            string dni = this.dni.Text;
            string nombre = name.Text;
            string apellido = ap.Text;
            string telefono = tel.Text;
            string mail = gmail.Text;
            string contaseña = pass.Text;
            DateTime fechaNacimiento = fechnac.Value;
            int rol = (int)comboBox1.SelectedValue;
            string idioma = "ES";
            BE.Usuario user = new BE.Usuario(dni, nombre, apellido, telefono, mail, contaseña, fechaNacimiento, rol, idioma);
            BLLUsuario bllUser = new BLLUsuario();
            Servicios.Resultado<BE.Usuario> resultado = bllUser.crearEntidad(user);
            string dataSource = "DESKTOP-Q714KGU\\SQLEXPRESS";
            //string dbName = "SistemaViajes";
            string conexionMaster = $"Data source={dataSource};Initial Catalog=SistemaViajes;Integrated Security=True;";
            SqlConnection Connection = new SqlConnection(conexionMaster);

            int nuevoIdUsuario = bllUser.obtenerIDUsuario(user);
            try
            {
                string query = $"USE SistemaViajes; INSERT INTO UsuarioPermiso (id_usuario, id_permiso) VALUES ({nuevoIdUsuario}, {rol})";
                Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, Connection))
                {
                        cmd.Parameters.AddWithValue("@IdUsuario", nuevoIdUsuario);
                        cmd.Parameters.AddWithValue("@IdPermiso", rol);
                        cmd.ExecuteNonQuery();
                }
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }
            MessageBox.Show("Usuario creado con exito");
            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "Registro de Usuario";
            int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha1 = DateTime.Now;
            int criticidad = 7;
            BLLUsuario BLLUser = new BLLUsuario();
            int idpermi = BLLUser.DevolverIdPermisoPorId(id_usuario1);
            string actor = BLLUser.obtenernamepermisoporID(idpermi.ToString());
            BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
            bllbita.crearEntidad(bitacorita);
        }
        private void FRMRegistrarCliente_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
    }
}