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
            //Usuario user = new Usuario(/*falta id*/int.Parse(dni.Text), name.Text, ap.Text, tel.Text, gmail.Text, pass.Text, fechnac.Value, rol.SelectedItem.ToString()/*falta salt*/);
            //BLLUser.crearEntidad(user);
        }

        private void FRMRegistrarCliente_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
    }
}
