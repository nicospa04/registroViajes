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
    public partial class ControlUsuario : UserControl
    {
        public ControlUsuario()
        {
            InitializeComponent();
        }
        public TextBox txt1 { get; set; }

        public TextBox txt2 { get; set; }

        public void limpiar()
        {
            if(txt1 != null && txt2 != null)
            {
                txt1.Clear();
                txt2.Clear();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
