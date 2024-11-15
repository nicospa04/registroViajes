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
using DAL;

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
            Servicios.Resultado<BE.Usuario> resultado = BLLUser.recuperarUsuario(txtusuario.Text.Trim(), txtcontraseña.Text.Trim());
            if (!resultado.resultado)
            {
                MessageBox.Show(resultado.mensaje);
                return;
            }
            MessageBox.Show("Sesion Iniciada Correctamente");
            SessionManager.ObtenerInstancia().IniciarSesion(resultado.entidad);
            Lenguaje.ObtenerInstancia().IdiomaActual = BLLUser.recuperarIdioma(resultado.entidad);
            inicioSesionCorrecto.Invoke(this, new EventArgs());
            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "Inicio de Sesión";
            int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha1 = DateTime.Now;
            int criticidad = 1;

            string actor;
            if (id_usuario1 == 3)
                actor = "ADMIN";
            else if (id_usuario1 == 2)
                actor = "EMPLEADO";
            else
                actor = "USUARIO";

            BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
            bllbita.crearEntidad(bitacorita);
            //if (!resultadobita.resultado)
            //{
            //    MessageBox.Show(resultadobita.mensaje);
            //    return;
            //}
            //MessageBox.Show(resultadobita.mensaje);
            Close();
        }
        public event EventHandler inicioSesionCorrecto;
        private void btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void FRMIniciarSesion_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();

            controlUsuario1.txt1 = txtusuario;
            controlUsuario1.txt2 = txtcontraseña;
        }

        private void controlUsuario1_Load(object sender, EventArgs e)
        {

        }
    }
}
