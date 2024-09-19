using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios;
using BLL;
using BE;

namespace RegistroViajes
{
    public partial class FRMIniciarSesion : Form
    {
        public FRMIniciarSesion()
        {
            InitializeComponent();
        }

        private void btniniciar_Click(object sender, EventArgs e)
        {
            Usuario user = txtusuario.Text && txtcontraseña.Text;
            if(user != null)
            {
                SessionManager.ObtenerInstancia().IniciarSesion(user);
            }
            else
            {

            }
        }
    }
}
