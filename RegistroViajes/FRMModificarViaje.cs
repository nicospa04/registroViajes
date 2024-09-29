using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroViajes
{
    public partial class FRMModificarViaje : Form, IObserver
    {
        public FRMModificarViaje()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMModificarViaje_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
