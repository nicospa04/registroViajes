using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroViajes
{
    public partial class VerPerfil : Form, IObserver
    {
        public VerPerfil()
        {
            InitializeComponent();

            var user = SessionManager.Obtenerdatosuser();

            textBox1.Text = user.nombre;

            textBox2.Text = user.telefono;
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void VerPerfil_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)  // FALLA DESENCRIPTADO
        {
            PasswordHasher passwordHasher = new PasswordHasher();
            var user = SessionManager.Obtenerdatosuser();
            string tele = new PasswordHasher().Decrypt(user.telefono, user.salt);

            if (checkBox1.Checked)
            {
                textBox2.Text = tele;
                BLLBitacora bllbita = new BLLBitacora();
                string operacion = "Desencriptado";
                int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
                DateTime fecha1 = DateTime.Now;
                int criticidad = 15;
                BLLUsuario BLLUser = new BLLUsuario();
                int idpermi = BLLUser.DevolverIdPermisoPorId(id_usuario1);
                string actor = BLLUser.obtenernamepermisoporID(idpermi.ToString());

                BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
                bllbita.crearEntidad(bitacorita);
            }
            else
            {
                textBox2.Text = user.telefono;
            }
        }
    }
}
