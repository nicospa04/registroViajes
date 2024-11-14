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
    public partial class FRMCancelarViaje : Form, IObserver
    {
        int id_viajeee;


        BLL.BLLEmpresa bllEmpresa = new BLLEmpresa();
        public FRMCancelarViaje()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
            List<BE.Viaje> viajes = new List<BE.Viaje>();
            BLL.BLLViaje bll = new BLL.BLLViaje();
            viajes = bll.leerEntidades();
            viajes = viajes.Where(viaje => viaje.id_usuario == SessionManager.Obtenerdatosuser().id_usuario).ToList();
            DataGridViewTextBoxColumn nuevaColumna = new DataGridViewTextBoxColumn();
            nuevaColumna.HeaderText = "Titular";
            nuevaColumna.Name = "Titular";
            dataGridView1.Columns.Add(nuevaColumna);
            BLLEmpresa bllEmpresa = new BLL.BLLEmpresa();

            dataGridView1.DataSource = viajes;

            DAL.Usuario dal = new DAL.Usuario();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.DataBoundItem != null)
                {
                    // Obtenemos el objeto asociado a la fila
                    BE.Viaje viaje = (BE.Viaje)fila.DataBoundItem;

                    string id_usuario = viaje.id_usuario.ToString();


                    // Calculamos un valor basado en otras propiedades
                    //fila.Cells["Titular"].Value = dal.encontrarNombreUsuarioPorID(id_usuario);
                }
            }
        }

        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMCancelarViaje_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
                string fecha = fila.Cells["id_fecha"].Value.ToString();
                string transporte = fila.Cells["transporte"].Value.ToString();
                DateTime fecha_inicio = DateTime.Parse(fila.Cells["fecha_inicio"].Value.ToString());
                DateTime fecha_fin = DateTime.Parse(fila.Cells["fecha_vuelta"].Value.ToString());
                textBox1.Text = titular;
                textBox2.Text = empresa;
                textBox3.Text = fecha;
                textBox4.Text = transporte;
                dateTimePicker2.Value = fecha_inicio;
                dateTimePicker1.Value = fecha_fin;
            }
        }

        private void label11_Click(object sender, EventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DAL.Usuario dal = new DAL.Usuario();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.DataBoundItem != null)
                {
                    BE.Viaje viaje = (BE.Viaje)fila.DataBoundItem;
                    string id_usuario = viaje.id_usuario.ToString();

                    // Asignar el nombre del usuario a la columna "Titular"
                    fila.Cells["Titular"].Value = dal.encontrarNombreUsuarioPorID(id_usuario);
                }
            }

            // Forzar actualización del control
            dataGridView1.Refresh();
        }
    }
}
