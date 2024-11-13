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
        BLLBitacora bllbita = new BLLBitacora();
        private void button1_Click(object sender, EventArgs e)
        {
            if (box.Text == "")
            {
                MessageBox.Show("Complete el Campo");
                return;
            }
            string idioma = box.Text == "Español" ? "ES" : "EN";
            Lenguaje.ObtenerInstancia().IdiomaActual = idioma;
            if (SessionManager.verificarInicioSesion())
            {
                BE.Usuario user = SessionManager.Obtenerdatosuser();
                BLLUsuario bll = new BLLUsuario();
                bll.modificarIdioma(user, idioma);
            }
            MessageBox.Show("Se cambió el idioma correctamente a: " + box.Text);

            string operacion = "Cambio de Idioma";
            int id_usuario = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha = DateTime.Now;
            int criticidad = 4;
            if (id_usuario == 3)
            {
                string actor3 = "ADMIN";
                BEBitacora bitacorita = new BEBitacora(id_usuario, operacion, fecha, actor3, criticidad);
                Servicios.Resultado<BEBitacora> resultadobita3 = bllbita.crearEntidad(bitacorita);
            }
            else if (id_usuario == 2)
            {
                string actor2 = "EMPLEADO";
                BEBitacora bitacorita = new BEBitacora(id_usuario, operacion, fecha, actor2, criticidad);
                Servicios.Resultado<BEBitacora> resultadobita2 = bllbita.crearEntidad(bitacorita);
            }
            else
            {
                string actor1 = "USUARIO";
                BEBitacora bitacorita = new BEBitacora(id_usuario, operacion, fecha, actor1, criticidad);
                Servicios.Resultado<BEBitacora> resultadobita1 = bllbita.crearEntidad(bitacorita);
            }
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
            ActualizarIdioma();
        }
    }
}
