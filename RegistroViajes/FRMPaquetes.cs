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

            MostrarPrimeras3Ofertas(paquetes);
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
            MostrarPrimeras3Ofertas(paquetes);

        }


        void MostrarPrimeras3Ofertas(List<BE.Paquete> paquetes)
        {
            iconButton1.Text = paquetes[0].nombre;
            iconButton2.Text = paquetes[1].nombre;
            iconButton3.Text = paquetes[2].nombre;
            label4.Text = paquetes[0].descripcion;
            label8.Text = paquetes[1].descripcion;
            label12.Text = paquetes[2].descripcion;
            label5.Text = $"${paquetes[0].precio_base.ToString()}";
            label10.Text = $"${paquetes[1].precio_base.ToString()}";
            label15.Text = $"${paquetes[2].precio_base.ToString()}";   
            label6.Text = paquetes[0].cupo_personas.ToString();
            label9.Text = paquetes[1].cupo_personas.ToString();
            label14.Text = paquetes[2].cupo_personas.ToString();
        }



        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void FRMPaquetes_Load_1(object sender, EventArgs e)
        {
            ActualizarIdioma();
            MostrarPrimeras3Ofertas(paquetes);


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
    }
}
