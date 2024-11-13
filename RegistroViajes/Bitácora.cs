using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Servicios;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace RegistroViajes
{
    public partial class FRMBitacora : Form, IObserver
    {
        private Archivo archivoService;
        public FRMBitacora()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
            List<BEBitacora> bitacora = new List<BEBitacora>();
            BLLBitacora bllbita = new BLLBitacora();
            bitacora = bllbita.leerEntidades();
            dataGridView1.DataSource = bitacora;
            archivoService = new Archivo();
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        BLLUsuario blluserr = new BLLUsuario();
        private void Bitácora_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
        int id_bitacora;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener la fila que fue clickeada
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                int id_bitacora = fila.Cells["id_bitacora"].Value != null ? int.Parse(fila.Cells["id_bitacora"].Value.ToString()) : 0;
                int user = fila.Cells["id_usuario"].Value != null ? int.Parse(fila.Cells["id_usuario"].Value.ToString()) : 0;
                string operacion = fila.Cells["operacion"].Value != null ? fila.Cells["operacion"].Value.ToString() : "";
                string actor = fila.Cells["actor"].Value != null ? fila.Cells["actor"].Value.ToString() : "";
                DateTime fecha = fila.Cells["fecha"].Value != null ? DateTime.Parse(fila.Cells["fecha"].Value.ToString()) : DateTime.Now;
                string nombreuser = "";
                string apellidouser = "";
                int criticidad = fila.Cells["criticidad"].Value != null ? int.Parse(fila.Cells["criticidad"].Value.ToString()) : 0;
                if (fila.Cells["id_usuario"] != null && fila.Cells["id_usuario"].Value != null)
                {
                    string iduser = fila.Cells["id_usuario"].Value.ToString();
                    nombreuser = blluserr.devolverNombrePorId(iduser);
                    apellidouser = blluserr.devolverApellidoPorId(iduser);
                }
                textBox1.Text = nombreuser;
                textBox2.Text = apellidouser;
                comboBox1.Text = operacion;
                textBox3.Text = user.ToString();
                textBox4.Text = actor;
                textBox5.Text = criticidad.ToString();
                dateTimePicker1.Value = fecha;
            }
        }

        private void limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(comboBox2.Text == "")
            {
                MessageBox.Show("Seleccione el Idioma a Imprimir la Bitácora");
                return;
            }
            string idioma = comboBox2.Text == "Español" ? "ES" : "EN";
            {
                // Genera la lista de bitácoras que quieres imprimir
                List<BEBitacora> listaBitacora = ObtenerListaBitacora();

                // Llama al método para generar el archivo PDF
                bool exito = archivoService.GenerarArchivo(listaBitacora, idioma);

                if (exito)
                {
                    MessageBox.Show("Archivo PDF generado y abierto correctamente.");
                }
                else
                {
                    MessageBox.Show("Error al generar el archivo PDF.");
                }
            }
        }
        private List<BEBitacora> ObtenerListaBitacora()
        {
            List<BEBitacora> listaBitacora = new List<BEBitacora>();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["id_usuario"].Value != null) // Asegúrate de verificar que la fila no esté vacía
                {
                    BEBitacora bitacora = new BEBitacora
                    {
                        id_usuario = Convert.ToInt32(fila.Cells["id_usuario"].Value),
                        operacion = fila.Cells["operacion"].Value.ToString(),
                        fecha = Convert.ToDateTime(fila.Cells["fecha"].Value),
                        actor = fila.Cells["actor"].Value.ToString(),
                        criticidad = Convert.ToInt32(fila.Cells["criticidad"].Value)
                    };

                    listaBitacora.Add(bitacora);
                }
            }

            return listaBitacora;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }

}
