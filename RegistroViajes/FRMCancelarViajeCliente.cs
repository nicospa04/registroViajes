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
    public partial class FRMCancelarViajeCliente : Form
    {
        public FRMCancelarViajeCliente()
        {
            InitializeComponent();


            BE.Usuario user = SessionManager.Obtenerdatosuser();


            List<BE.Viaje> viajes = new List<BE.Viaje>();

            DAL.Viaje dal = new DAL.Viaje();

            viajes = dal.leerEntidades();


            viajes = viajes.Where(viaje => viaje.id_usuario == user.id_usuario).ToList();


            dataGridView1.DataSource = viajes;



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BLLViaje bllviaje = new BLLViaje();

            List<BE.Viaje> viajes = new List<BE.Viaje>();
            viajes = bllviaje.leerEntidades();


            BE.Viaje viaje = viajes.FirstOrDefault(x => x.id_viaje == id_viajeee);



            bllviaje.eliminarEntidad(viaje);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void FRMCancelarViajeCliente_Load(object sender, EventArgs e)
        {

        }
        int id_viajeee;

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            BLLEmpresa bllEmpresa = new BLLEmpresa();

            // Verificar si el clic fue en una celda válida
            if (e.RowIndex >= 0)
            {
                // Obtener la fila que fue clickeada
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

                id_viajeee = int.Parse(fila.Cells["id_viaje"].Value.ToString());


                // Acceder a los valores de las celdas de esa fila
                string titular = fila.Cells["Titular"].Value.ToString();
                string empresa = "";
                if (fila.Cells["id_empresa"] != null && fila.Cells["id_empresa"].Value != null)
                {
                    string idEmpresa = fila.Cells["id_empresa"].Value.ToString();
                    empresa = bllEmpresa.devolverNombrePorId(idEmpresa);
                }
                string destino = fila.Cells["id_destino"].Value.ToString();
                string transporte = fila.Cells["transporte"].Value.ToString();
                int cant_niños = int.Parse(fila.Cells["cant_niños"].Value.ToString());
                int cant_adultos = int.Parse(fila.Cells["cant_adulto"].Value.ToString());
                DateTime fecha_inicio = DateTime.Parse(fila.Cells["fecha_inicio"].Value.ToString());
                DateTime fecha_fin = DateTime.Parse(fila.Cells["fecha_vuelta"].Value.ToString());


                textBox1.Text = titular;
                textBox2.Text = empresa;
                textBox3.Text = destino;
                textBox4.Text = transporte;
                numericUpDown2.Value = cant_niños;
                numericUpDown1.Value = cant_adultos;
                dateTimePicker2.Value = fecha_inicio;
                dateTimePicker1.Value = fecha_fin;


            }
        }
    }
}
