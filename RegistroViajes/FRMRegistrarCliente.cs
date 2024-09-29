using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Servicios;

namespace RegistroViajes
{
    public partial class FRMRegistrarCliente : Form, IObserver
    {
        BLLUsuario BLLUser = new BLLUsuario();
        public FRMRegistrarCliente()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
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

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string dni = this.dni.Text;
            string nombre = name.Text;
            string apellido = ap.Text;
            string telefono = tel.Text;
            string mail = gmail.Text;
            string contaseña = pass.Text;
            DateTime fechaNacimiento = fechnac.Value;
            int familia = 1;

            if(dni.Length != 8 || nombre.Length < 6 || apellido.Length < 6 || telefono.Length < 8 || contaseña.Length < 6 || fechaNacimiento == DateTime.Now)
            {
                MessageBox.Show("Complete bien todos los campos");
                return;
            }


            string idioma = Lenguaje.ObtenerInstancia().IdiomaActual;

            BE.Usuario user = new BE.Usuario(dni,nombre,apellido,telefono,mail,contaseña,fechaNacimiento, familia,idioma);

            BLLUsuario bllUser = new BLLUsuario();
            Servicios.Resultado<BE.Usuario> resultado = bllUser.crearEntidad(user);


            //Si hubo un error al crear el usuario (manejar error en este cas):
            if (!resultado.resultado)
            {
                MessageBox.Show(resultado.mensaje); 
                return;
            }

            MessageBox.Show(resultado.mensaje);




        }

        private void FRMRegistrarCliente_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
    }
}
