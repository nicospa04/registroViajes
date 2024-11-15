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
    public partial class VerPerfil : Form
    {
        public VerPerfil()
        {
            InitializeComponent();

            var user = SessionManager.Obtenerdatosuser();

            textBox1.Text = user.nombre;

            textBox2.Text = user.telefono;


        }

        private void VerPerfil_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PasswordHasher passwordHasher = new PasswordHasher();

            var user = SessionManager.Obtenerdatosuser();

            string tele = new PasswordHasher().Decrypt(user.telefono, user.salt);

            if (checkBox1.Checked)
            {
                textBox2.Text = tele;
            }


        }
    }
}
