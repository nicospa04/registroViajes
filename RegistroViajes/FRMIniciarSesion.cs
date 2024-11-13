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
            int id_usuario = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha = DateTime.Now;
            int criticidad = 1;
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
        }
    }
}
