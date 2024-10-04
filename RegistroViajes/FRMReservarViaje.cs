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
    public partial class FRMReservarViaje : Form, IObserver
    {
        public FRMReservarViaje()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
            BLLDestino bLLDestino = new BLLDestino();
            dataGridView1.DataSource = bLLDestino.leerEntidades();

            BLLEmpresa bLLEmpresa = new BLLEmpresa();
            var empresas = bLLEmpresa.leerEntidades();

            foreach (var item in empresas)
            {
                comboBox1.Items.Add(item.nombre);
            }


            BLLTransporte bLLTransporte = new BLLTransporte();

            var transportes = bLLTransporte.leerEntidades();

            foreach(var item in transportes)
            {
                comboBox2.Items.Add(item.nombre);
            }


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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el clic fue en una celda válida
            if (e.RowIndex >= 0)
            {
                // Obtener la fila que fue clickeada
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];


                string nombreDestino = fila.Cells["nombre"].Value.ToString();

                textBox1.Text = nombreDestino;


                BLLDestino bLLDestino = new BLLDestino();
                List<BE.Destino> destinos = bLLDestino.leerEntidades();
                BE.Destino destinoelegido = destinos.FirstOrDefault(destino => destino.nombre == nombreDestino);

                BLLEmpresa bLLEmpresa = new BLLEmpresa();
                var empresas = bLLEmpresa.leerEntidades();
                var empresaElegida = empresas.FirstOrDefault(empresa => empresa.nombre == comboBox1.SelectedItem.ToString());


                BLLTransporte bLLTransporte = new BLLTransporte();
                var transportes = bLLTransporte.leerEntidades();

                var transportelegido = transportes.FirstOrDefault(t => t.nombre == comboBox2.SelectedItem.ToString());


                lblprecio.Text = (Decimal.Parse(destinoelegido.precio_base.ToString() + destinoelegido.precio_base * empresaElegida.porcentaje_extra + ((Decimal.Parse(destinoelegido.precio_base.ToString()) * transportelegido.porcentaje_extra) / 100 )).ToString());



            }
        }
    }
}
