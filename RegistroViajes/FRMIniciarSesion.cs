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
        BLLUsuario BLLUser = new BLLUsuario();
        private void btniniciar_Click(object sender, EventArgs e)
        
        {
            if (txtusuario.Text == string.Empty || txtcontraseña.Text == string.Empty)
            {
                MessageBox.Show("Complete los campos");
                return;
            }

            if (SessionManager.ObtenerInstancia().Usuario != null)
            {
                MessageBox.Show("Ya hay una sesión activa.");
                return;
            }

            Usuario user = BLLUser.recuperarUsuario(txtusuario.Text.Trim(),txtcontraseña.Text.Trim());
            if(user == null)
            {
                MessageBox.Show("Usuario No Encontrado");
                return;
            }
            if (user != null)
            {
                MessageBox.Show("Sesion Iniciada Correctamente");
                SessionManager.ObtenerInstancia().IniciarSesion(user);
                Close();
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            if (SessionManager.ObtenerInstancia().Usuario == null)
            {
                MessageBox.Show("Sesion no Iniciada");
                return;
            }
            if (SessionManager.ObtenerInstancia().Usuario != null)
            {
                SessionManager.ObtenerInstancia().CerrarSesion();
                MessageBox.Show("Sesión Cerrada");
                return;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            FRMRegistrarCliente fRMRegistrarCliente = new FRMRegistrarCliente();
            fRMRegistrarCliente.ShowDialog();
        }
    }
}
