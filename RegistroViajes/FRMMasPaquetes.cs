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
    public partial class FRMMasPaquetes : Form
    {
        public FRMMasPaquetes()
        {
            InitializeComponent();
            List<BE.Paquete> paquetes = new List<BE.Paquete>();
            BLL.BLLPaquete bll = new BLL.BLLPaquete();
            paquetes = bll.leerEntidades();
            dataGridView1.DataSource = paquetes;

            //dataGridView1.Columns["PropiedadNoDeseada1"].Visible = false;
            //dataGridView1.Columns["PropiedadNoDeseada2"].Visible = false;
        }

        private void FRMMasPaquetes_Load(object sender, EventArgs e)
        {

        }
    }
}
