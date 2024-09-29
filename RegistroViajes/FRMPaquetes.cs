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
        public FRMPaquetes()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);

            List<BE.Paquete> paquetes = new List<BE.Paquete>();


            BLLPaquete bll = new BLLPaquete();

            paquetes = bll.leerEntidades();



        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        //private void FRMPaquetes_Load(object sender, EventArgs e)
        //{
        //    ActualizarIdioma();
        //}

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
    }
}
