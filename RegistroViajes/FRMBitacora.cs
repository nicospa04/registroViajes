using BE;
using BLL;
using DAL;
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
    public partial class FRMBitacora : Form,IObserver
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //ALT "complete los campos".
        }
        private void FRMBitacora_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
    }
}
