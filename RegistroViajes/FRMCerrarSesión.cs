using BE;
using BLL;
using Servicios;
using System.Windows.Forms;
using System;

namespace RegistroViajes
{
    public partial class FRMCerrarSesión : Form,IObserver
    {
        private Form1 formularioPrincipal;

        private void FRMCerrarSesion_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        public FRMCerrarSesión(Form1 formPrincipal)
        {
            InitializeComponent();
            formularioPrincipal = formPrincipal; // Asigna el formulario principal
            Lenguaje.ObtenerInstancia().Agregar(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SessionManager.ObtenerInstancia().Usuario != null)
            {
                BLLBitacora bllbita = new BLLBitacora();
                string operacion = "Cierre de Sesión";
                int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
                DateTime fecha = DateTime.Now;
                int criticidad = 2;
                BLLUsuario BLLUser = new BLLUsuario();
                int idpermi = BLLUser.DevolverIdPermisoPorId(id_usuario1);
                string actor = BLLUser.obtenernamepermisoporID(idpermi.ToString());

                BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha, actor, criticidad);
                bllbita.crearEntidad(bitacorita);

                // Cierra la sesión y modifica los permisos del menú en el formulario principal
                SessionManager.ObtenerInstancia().CerrarSesion();
                formularioPrincipal.modifmenu(); // Llama a modifmenu en el formulario principal

                MessageBox.Show("Se ha cerrado la Sesión Correctamente");
            }
            else
            {
                MessageBox.Show("No se ha iniciado sesión anteriormente");
            }
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

