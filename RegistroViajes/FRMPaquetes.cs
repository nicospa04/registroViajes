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
            agregarColumnaNombreDestino(paquetes); // Agregamos la columna directamente al DataGridView
            dataGridView1.DataSource = paquetes; //hay que agregar una columna con el nombre del destino en funcion del id_destino en id_fecha

        }

        void agregarColumnaNombreDestino(List<BE.Paquete> paquetes)
        {
            // Crear una nueva columna para el nombre del destino
            DataGridViewTextBoxColumn destinoColumn = new DataGridViewTextBoxColumn
            {
                Name = "NombreDestino",
                HeaderText = "Nombre del Destino",
                ReadOnly = true // Para que no sea editable
            };

            // Agregar la columna al DataGridView
            dataGridView1.Columns.Add(destinoColumn);

            // Llenar la columna con los nombres de destino
            for (int i = 0; i < paquetes.Count; i++)
            {
                //var destino = new BLLDestino().leerEntidades().FirstOrDefault(d => d.id_destino == paquetes[i].id_destino);
             //   dataGridView1.Rows[i].Cells["NombreDestino"].Value = destino?.nombre ?? "Desconocido"; // Asignar el nombre o un valor por defecto si no existe
            }
        }


        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }



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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
