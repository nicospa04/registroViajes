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
    public partial class FRMModificarViaje : Form, IObserver
    {
        public FRMModificarViaje()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);

            BLL.BLLEmpresa bllEmpresa = new BLLEmpresa();



            List<BE.Viaje> viajes = new List<BE.Viaje>();
            BLL.BLLViaje bll = new BLL.BLLViaje();
            viajes = bll.leerEntidades();

            //BE.Viaje viaje = viajes.FirstOrDefault(viaje => id == viaje.id_usuario);


            DataGridViewTextBoxColumn nuevaColumna = new DataGridViewTextBoxColumn();
            nuevaColumna.HeaderText = "Titular";
            nuevaColumna.Name = "Titular";
            dataGridView1.Columns.Add(nuevaColumna);
 
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
                    fila.Cells["Titular"].Value = dal.encontrarNombreUsuarioPorID(id_usuario);
                }
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            BLL.BLLEmpresa bllEmpresa = new BLLEmpresa();



            // Verificar si el clic fue en una celda válida
            if (e.RowIndex >= 0)
            {
                // Obtener la fila que fue clickeada
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

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
                comboBox1.Text = empresa;
                comboBox4.Text = destino;
                comboBox2.Text = transporte;
                numericUpDown2.Value = cant_niños;
                numericUpDown1.Value = cant_adultos;
                dateTimePicker2.Value = fecha_inicio;
                dateTimePicker1.Value = fecha_fin;


            }
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
