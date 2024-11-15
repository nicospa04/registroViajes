using BE;
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


            cargarCombos();
            cargarDataGridView();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false; // Opcional: si deseas permitir solo una fila seleccionada

        }

        void cargarDataGridView()
        {
            BLLFecha bllFecha = new BLLFecha();
            List<BE.Fecha> fechas = bllFecha.leerEntidades();
            dataGridView1.DataSource = fechas;
        }

        void filtrarPorComboBox()
        {
            List<BE.Fecha> fechas = new BLLFecha().leerEntidades();
            int id_destino = new BLLDestino().leerEntidades().FirstOrDefault(destino => destino.nombre == comboBox3.SelectedItem.ToString()).id_destino;
            fechas = fechas.Where(fecha => fecha.id_lugar_destino == id_destino).ToList();
            int id_empresa = new BLLEmpresa().leerEntidades().FirstOrDefault(empresa => empresa.nombre == comboBox1.SelectedItem.ToString()).id_empresa;
            fechas = fechas.Where(fecha => fecha.id_empresa == id_empresa).ToList();
            int id_transporte = new BLLTransporte().leerEntidades().FirstOrDefault(transporte => transporte.modelo == comboBox2.SelectedItem.ToString()).id_transporte;
            fechas = fechas.Where(fecha => fecha.id_transporte == id_transporte).ToList();

            dataGridView1.DataSource = fechas;
        }



        void cargarCombos()
        {
            // Desactivar temporalmente los eventos de los ComboBox
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged -= comboBox3_SelectedIndexChanged;

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            comboBox1.Items.Add("Todos");
            comboBox2.Items.Add("Todos");
            comboBox3.Items.Add("Todos");

            var destinos = new BLLDestino().leerEntidades();
            foreach (var destino in destinos.Select(d => d.nombre).Distinct())
            {
                comboBox3.Items.Add(destino);
            }

            var empresas = new BLLEmpresa().leerEntidades();
            foreach (var empresa in empresas)
            {
                comboBox1.Items.Add(empresa.nombre);
            }

            var transportes = new BLL.TipoTransporte().leerEntidades();
            foreach (var transporte in transportes)
            {
                comboBox2.Items.Add($"{transporte.nombre}");
            }

            // Seleccionar el primer elemento de cada ComboBox
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            // Reactivar los eventos de los ComboBox
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
        }


        void aplicarComboBoxs()
        {
            string empresa = comboBox1.SelectedItem.ToString();
            string transporte = comboBox2.SelectedItem.ToString();
            string destino = comboBox3.SelectedItem.ToString();

            BLLFecha bllFecha = new BLLFecha();
            List<BE.Fecha> fechas = bllFecha.leerEntidades();

            if (empresa != "Todos")
            {
                int id_empresa = new BLLEmpresa().leerEntidades().FirstOrDefault(emp => emp.nombre == empresa).id_empresa;
                fechas = fechas.Where(fecha => fecha.id_empresa == id_empresa).ToList();
            }

            if (transporte != "Todos")
            {
                int id_tipo_transporte = new BLL.TipoTransporte().leerEntidades().FirstOrDefault(tipo => tipo.nombre == transporte).id_tipo_transporte;
                int id_transporte = new BLLTransporte().leerEntidades().FirstOrDefault(t => t.id_tipo_transporte == id_tipo_transporte).id_transporte;
                fechas = fechas.Where(f => f.id_transporte == id_transporte).ToList();

            }

            if (destino != "Todos")
            {
                int id_destino = new BLLDestino().leerEntidades().FirstOrDefault(dest => dest.nombre == destino).id_destino;
                fechas = fechas.Where(fecha => fecha.id_lugar_destino == id_destino).ToList();
            }

            dataGridView1.DataSource = fechas;


        }



        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int idFecha = Convert.ToInt32(selectedRow.Cells["id_fecha"].Value);
                int idEmpresa = Convert.ToInt32(selectedRow.Cells["id_empresa"].Value);
                int idLugarOrigen = Convert.ToInt32(selectedRow.Cells["id_lugar_origen"].Value);
                int idLugarDestino = Convert.ToInt32(selectedRow.Cells["id_lugar_destino"].Value);
                int idTransporte = Convert.ToInt32(selectedRow.Cells["id_transporte"].Value);
                DateTime fechaIda = Convert.ToDateTime(selectedRow.Cells["fecha_ida"].Value);

                FRMSeleccionAsientos frmSeleccionAsientos = new FRMSeleccionAsientos(idFecha);
                frmSeleccionAsientos.Show();

            }
            else
            {

            }


        
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
                
                var fecha_ida = fila.Cells["fecha_ida"].Value.ToString();
                var fecha_vuelta = fila.Cells["fecha_vuelta"].Value.ToString();

                dateTimePicker2.Value = DateTime.Parse(fecha_ida);
                dateTimePicker1.Value = DateTime.Parse(fecha_vuelta);

               
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            aplicarComboBoxs();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            aplicarComboBoxs();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            aplicarComboBoxs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cargarCombos();
        }
    }
}
