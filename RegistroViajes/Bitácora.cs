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

namespace RegistroViajes
{
    public partial class FRMBitacora : Form, IObserver
    {
        public FRMBitacora()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
            List<BEBitacora> bitacora = new List<BEBitacora>();
            BLLBitacora bllbita = new BLLBitacora();
            bitacora = bllbita.leerEntidades();
            dataGridView1.DataSource = bitacora; 
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
    }
}
