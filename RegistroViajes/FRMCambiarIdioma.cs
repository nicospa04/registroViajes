using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

//using iTextSharp.text.xml;
using Servicios;

namespace RegistroViajes
{
    public partial class FRMCambiarIdioma : Form, IObserver
    {
        public FRMCambiarIdioma()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(box.Text == "")
            {
                return;
            }
            
                Lenguaje.ObtenerInstancia().IdiomaActual = box.Text;

            if (SessionManager.verificarInicioSesion())
            {
                BE.Usuario user = SessionManager.Obtenerdatosuser();
                BLLUsuario bll = new BLLUsuario();
                bll.modificarIdioma(user, box.Text);
            }


            MessageBox.Show("Se cambió el idioma correctamente a: " + box.Text);

            this.Hide();
        }

        private void FRMCambiarIdioma_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
               e.Cancel = true;
                Hide();
            }
        }

        private void FRMCambiarIdioma_Load(object sender, EventArgs e)
        {

        }
    }
}
