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
        public FRMCancelarViaje()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);

            List<BE.Viaje> viajes = new List<BE.Viaje>();
            BLL.BLLViaje bll = new BLL.BLLViaje();
            viajes = bll.leerEntidades();
            dataGridView1.DataSource = viajes;

            DataGridViewTextBoxColumn nuevaColumna = new DataGridViewTextBoxColumn();
            nuevaColumna.HeaderText = "Titular del viaje";
            nuevaColumna.Name = "Titular";
            dataGridView1.Columns.Add(nuevaColumna);

            DAL.Usuario dal = new DAL.Usuario();


            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.DataBoundItem != null)
                {
                    // Obtenemos el objeto asociado a la fila
                    BE.Paquete paquete = (BE.Paquete)fila.DataBoundItem;

                    // Calculamos un valor basado en otras propiedades
                    fila.Cells["columnaTotal"].Value = dal.encontrarNombreUsuarioPorID(fila.Cells["id_usuario"].ToString());
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
            // Verificar si el clic fue en una celda válida
            if (e.RowIndex >= 0)
            {
                // Obtener la fila que fue clickeada
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

                // Acceder a los valores de las celdas de esa fila
                string titular = fila.Cells["Titular"].ToString(); 
                string empresa = fila.Cells["empresa"].ToString();
                string destino = fila.Cells["destino"].ToString();
                string transporte = fila.Cells["transporte"].ToString();
                int cant_niños = int.Parse(fila.Cells["cant_niños"].ToString());
                int cant_adultos = int.Parse(fila.Cells["cant_adultos"].ToString());
                DateTime fecha_inicio = DateTime.Parse(fila.Cells["fecha_inicio"].ToString());
                DateTime fecha_fin = DateTime.Parse(fila.Cells["fecha_fin"].ToString());
            }
        }
    }
}
