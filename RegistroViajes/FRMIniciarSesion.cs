using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios;
using BLL;
using BE;

namespace RegistroViajes
{
    public partial class FRMIniciarSesion : Form, IObserver
    {
        public FRMIniciarSesion()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
        }
        BLLUsuario BLLUser = new BLLUsuario();
        private static Form formactivo = null;
        private void AbrirForm(Form formu)
        {

            if (formactivo != null)
            {
                formactivo.Close();

            }
            formactivo = formu;
            formu.TopLevel = false;
            formu.FormBorderStyle = FormBorderStyle.None;
            formu.Dock = DockStyle.Fill;

            this.Controls.Add(formu);
            formu.Show();

        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        private void btniniciar_Click(object sender, EventArgs e)
        
        {
            if (txtusuario.Text == string.Empty || txtcontraseña.Text == string.Empty)
            {
                MessageBox.Show("Complete los campos");
                return;
            }

            if (SessionManager.ObtenerInstancia().Usuario != null)
            {
                MessageBox.Show("Ya hay una sesión activa.");
                return;
            }

            Servicios.Resultado<BE.Usuario> resultado = BLLUser.recuperarUsuario(txtusuario.Text.Trim(),txtcontraseña.Text.Trim());
            
            if(!resultado.resultado)
            {
                MessageBox.Show(resultado.mensaje);
                return;
            }
      
                MessageBox.Show("Sesion Iniciada Correctamente");
                SessionManager.ObtenerInstancia().IniciarSesion(resultado.entidad);
                BLLUsuario bLLUsuario = new BLLUsuario();
                Lenguaje.ObtenerInstancia().IdiomaActual = BLLUser.recuperarIdioma(resultado.entidad);

                
          
                inicioSesionCorrecto.Invoke(this, new EventArgs());

            Close();
            
        }


        public event EventHandler inicioSesionCorrecto;

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            FRMRegistrarCliente fRMRegistrarCliente = new FRMRegistrarCliente();
            fRMRegistrarCliente.ShowDialog();
        }

      

        private void FRMIniciarSesion_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
    }
}
