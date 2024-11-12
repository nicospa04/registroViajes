using BLL;
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
    public partial class FRMPaquetes : Form, IObserver
    {
        List<BE.Paquete> paquetes = new List<BE.Paquete>();


        BLLPaquete bll = new BLLPaquete();


        public FRMPaquetes()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
            paquetes = bll.leerEntidades();
            dataGridView1.DataSource = paquetes;

        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }



        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void FRMPaquetes_Load_1(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
