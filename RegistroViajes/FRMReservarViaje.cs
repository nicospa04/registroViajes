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
    public partial class FRMReservarViaje : Form, IObserver
    {
        public FRMReservarViaje()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ha reservado un viaje con éxito ");
        }

        private void FRMReservarViaje_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
    }
}
