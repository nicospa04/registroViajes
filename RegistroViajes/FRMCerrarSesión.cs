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
    public partial class FRMCerrarSesión : Form
    {
        public FRMCerrarSesión()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(SessionManager.ObtenerInstancia().Usuario == null)
            {
                MessageBox.Show("No se ha iniciado sesion anteriormente");
            }
            SessionManager.ObtenerInstancia().CerrarSesion();
            MessageBox.Show("Se ha cerrado la Sesion Correctamente");
            this.Hide();
            //CUANDO SE CIERRA SESION, LOS PERMISOS DEL FORMULARIO PRINCIPAL QUEDAN CON LOS PERMISOS DEL ÚLTIMO QUE INICIÓ SESIÓN
        }
        private void FRMCerrarSesion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
