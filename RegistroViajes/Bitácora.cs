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
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.IO;
using Newtonsoft.Json;

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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener la fila que fue clickeada
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
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
                comboBox3.Text = user.ToString();
                comboBox4.Text = actor;
                comboBox5.Text = criticidad.ToString();
                dateTimePicker1.Value = fecha;
            }
        }
        private void limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            List<BEBitacora> bitacora = new List<BEBitacora>();
            BLLBitacora bllbita = new BLLBitacora();
            bitacora = bllbita.leerEntidades();
            dataGridView1.DataSource = bitacora;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
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
                    BLLBitacora bllbita = new BLLBitacora();
                    string operacion = "Imprimir";
                    int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
                    DateTime fecha1 = DateTime.Now;
                    int criticidad = 21;

                    string actor;
                    if (id_usuario1 == 3)
                        actor = "ADMIN";
                    else if (id_usuario1 == 2)
                        actor = "EMPLEADO";
                    else
                        actor = "USUARIO";

                    BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
                    bllbita.crearEntidad(bitacorita);
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los ComboBox
            int? idUsuario = string.IsNullOrEmpty(comboBox3.Text) ? (int?)null : int.Parse(comboBox3.Text);
            string operacion = string.IsNullOrEmpty(comboBox1.Text) ? null : comboBox1.Text;
            string actor = string.IsNullOrEmpty(comboBox4.Text) ? null : comboBox4.Text;
            int? criticidad = string.IsNullOrEmpty(comboBox5.Text) ? (int?)null : int.Parse(comboBox5.Text);

            // Filtrar los datos según los criterios seleccionados
            List<BEBitacora> bitacoraFiltrada = ObtenerListaBitacora()
                .Where(b =>
                    (!idUsuario.HasValue || b.id_usuario == idUsuario) &&
                    (string.IsNullOrEmpty(operacion) || b.operacion == operacion) &&
                    (string.IsNullOrEmpty(actor) || b.actor == actor) &&
                    (!criticidad.HasValue || b.criticidad == criticidad)
                )
                .ToList();
            // Actualizar el DataGridView con los datos filtrados
            dataGridView1.DataSource = bitacoraFiltrada;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                BEBitacora persona = new BEBitacora
                {
                    id_usuario = int.Parse(comboBox3.Text),
                    operacion = comboBox1.Text,
                    actor = comboBox4.Text,
                    fecha = dateTimePicker1.Value,
                    criticidad = int.Parse(comboBox5.Text),
                };

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON Files|*.json";
                    //saveFileDialog.Title = "Guardar Bitácora en JSON";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        SerializarJSON(persona, saveFileDialog.FileName);
                        MostrarArchivoSerializado(saveFileDialog.FileName);
                        //MessageBox.Show("Bitácora serializada en formato JSON con éxito.");
                        BLLBitacora bllbita = new BLLBitacora();
                        string operacion = "Serializado";
                        int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
                        DateTime fecha1 = DateTime.Now;
                        int criticidad = 19;

                        string actor;
                        if (id_usuario1 == 3)
                            actor = "ADMIN";
                        else if (id_usuario1 == 2)
                            actor = "EMPLEADO";
                        else
                            actor = "USUARIO";

                        BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
                        bllbita.crearEntidad(bitacorita);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al serializar: " + ex.Message);
            }
        }
        private void SerializarJSON(BEBitacora persona, string path)
        {
            string jsonString = JsonConvert.SerializeObject(persona, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(path, jsonString);
        }
        private void MostrarArchivoSerializado(string path)
        {
            listBox1.Items.Clear();

            string[] lineas = File.ReadAllLines(path);

            foreach (string linea in lineas)
            {
                listBox1.Items.Add(linea);
            }
        }

        private BEBitacora DeserializarJSON(string path)
        {
            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<BEBitacora>(jsonString);
        }
        private void MostrarDatosDeserializados(BEBitacora persona)
        {
            listBox2.Items.Clear();

            listBox2.Items.Add($"ID USUARIO: {persona.id_usuario}");
            listBox2.Items.Add($"EVENTO: {persona.operacion}");
            listBox2.Items.Add($"ACTOR: {persona.actor}");
            listBox2.Items.Add($"FECHA: {persona.fecha}");
            listBox2.Items.Add($"CRITICIDAD: {persona.criticidad}");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "JSON Files|*.json";
                    //openFileDialog.Title = "Abrir archivo JSON de Bitácora";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        listBox2.Items.Clear();
                        BEBitacora persona = DeserializarJSON(openFileDialog.FileName);
                        MostrarDatosDeserializados(persona);
                        BLLBitacora bllbita = new BLLBitacora();
                        string operacion = "Deserializado";
                        int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
                        DateTime fecha1 = DateTime.Now;
                        int criticidad = 20;

                        string actor;
                        if (id_usuario1 == 3)
                            actor = "ADMIN";
                        else if (id_usuario1 == 2)
                            actor = "EMPLEADO";
                        else
                            actor = "USUARIO";

                        BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
                        bllbita.crearEntidad(bitacorita);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al deserializar: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
