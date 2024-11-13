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
            //ALT "YA EXISTE UN USUARIO CON ESE DNI"

            //if(dni.Length != 8 || nombre.Length < 15 || apellido.Length < 15 || telefono.Length < 12 || contaseña.Length < 6 /*|| fechaNacimiento == DateTime.Now*/)
            //{
            //    MessageBox.Show("Complete bien todos los campos");
            //    return;
            //}
            string idioma = "ES";
            BE.Usuario user = new BE.Usuario(dni, nombre, apellido, telefono, mail, contaseña, fechaNacimiento, familia, idioma);
            BLLUsuario bllUser = new BLLUsuario();
            Servicios.Resultado<BE.Usuario> resultado = bllUser.crearEntidad(user);
            if (!resultado.resultado)
            {
                MessageBox.Show(resultado.mensaje);
                return;
            }
            MessageBox.Show(resultado.mensaje);
            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "Registro de Usuario";
            int id_usuario = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha = DateTime.Now;
            int criticidad = 3;
            if (id_usuario == 3)
            {
                string actor3 = "ADMIN";
                BEBitacora bitacorita = new BEBitacora(id_usuario, operacion, fecha, actor3, criticidad);
                Servicios.Resultado<BEBitacora> resultadobita3 = bllbita.crearEntidad(bitacorita);
            }
            else if (id_usuario == 2)
            {
                string actor2 = "EMPLEADO";
                BEBitacora bitacorita = new BEBitacora(id_usuario, operacion, fecha, actor2, criticidad);
                Servicios.Resultado<BEBitacora> resultadobita2 = bllbita.crearEntidad(bitacorita);
            }
            else
            {
                string actor1 = "USUARIO";
                BEBitacora bitacorita = new BEBitacora(id_usuario, operacion, fecha, actor1, criticidad);
                Servicios.Resultado<BEBitacora> resultadobita1 = bllbita.crearEntidad(bitacorita);
            }
        }
        private void FRMRegistrarCliente_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
    }
}