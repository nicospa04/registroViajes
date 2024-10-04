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

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;


        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == string.Empty)
            {
                MessageBox.Show("Debe seleccionar un destino");
            }

            int id_empresaa;

            BLLEmpresa bll = new BLLEmpresa();

            var empresas = bll.leerEntidades();

            id_empresaa = empresas.FirstOrDefault(e => e.nombre == comboBox1.SelectedItem.ToString()).id_empresa;


            BE.Viaje viaje = new BE.Viaje(SessionManager.Obtenerdatosuser().id_usuario, )



            
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


                decimal precioBase = Decimal.Parse(destinoelegido.precio_base.ToString());
                decimal porcentajeEmpresa = Decimal.Parse(empresaElegida.porcentaje_extra.ToString());
                decimal porcentajeTransporte = Decimal.Parse(transportelegido.porcentaje_extra.ToString());

                // Calcula el precio total
                decimal precioFinal = precioBase + (precioBase * porcentajeEmpresa / 100) + (precioBase * porcentajeTransporte / 100);

                // Asigna el resultado formateado al label
                lblprecio.Text = precioFinal.ToString();


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null || textBox1.Text == string.Empty) return;
            BLLDestino bLLDestino = new BLLDestino();
            List<BE.Destino> destinos = bLLDestino.leerEntidades();
            BE.Destino destinoelegido = destinos.FirstOrDefault(destino => destino.nombre == textBox1.Text);

            BLLEmpresa bLLEmpresa = new BLLEmpresa();
            var empresas = bLLEmpresa.leerEntidades();
            var empresaElegida = empresas.FirstOrDefault(empresa => empresa.nombre == comboBox1.SelectedItem.ToString());


            BLLTransporte bLLTransporte = new BLLTransporte();
            var transportes = bLLTransporte.leerEntidades();

            var transportelegido = transportes.FirstOrDefault(t => t.nombre == comboBox2.SelectedItem.ToString());


            decimal precioBase = Decimal.Parse(destinoelegido.precio_base.ToString());
            decimal porcentajeEmpresa = Decimal.Parse(empresaElegida.porcentaje_extra.ToString());
            decimal porcentajeTransporte = Decimal.Parse(transportelegido.porcentaje_extra.ToString());

            // Calcula el precio total
            decimal precioFinal = precioBase + (precioBase * porcentajeEmpresa / 100) + (precioBase * porcentajeTransporte / 100);

            // Asigna el resultado formateado al label
            lblprecio.Text = precioFinal.ToString();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || textBox1.Text == string.Empty) return;

            BLLDestino bLLDestino = new BLLDestino();
            List<BE.Destino> destinos = bLLDestino.leerEntidades();
            BE.Destino destinoelegido = destinos.FirstOrDefault(destino => destino.nombre == textBox1.Text);

            BLLEmpresa bLLEmpresa = new BLLEmpresa();
            var empresas = bLLEmpresa.leerEntidades();
            var empresaElegida = empresas.FirstOrDefault(empresa => empresa.nombre == comboBox1.SelectedItem.ToString());


            BLLTransporte bLLTransporte = new BLLTransporte();
            var transportes = bLLTransporte.leerEntidades();

            var transportelegido = transportes.FirstOrDefault(t => t.nombre == comboBox2.SelectedItem.ToString());


            decimal precioBase = Decimal.Parse(destinoelegido.precio_base.ToString());
            decimal porcentajeEmpresa = Decimal.Parse(empresaElegida.porcentaje_extra.ToString());
            decimal porcentajeTransporte = Decimal.Parse(transportelegido.porcentaje_extra.ToString());

            // Calcula el precio total
            decimal precioFinal = precioBase + (precioBase * porcentajeEmpresa / 100) + (precioBase * porcentajeTransporte / 100);

            // Asigna el resultado formateado al label
            lblprecio.Text = precioFinal.ToString();
        }
    }
}
