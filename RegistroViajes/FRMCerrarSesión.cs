using BE;
using BLL;
using FontAwesome.Sharp;
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
            if (SessionManager.ObtenerInstancia().Usuario != null)
            {
                BLLBitacora bllbita = new BLLBitacora();
                string operacion = "Cierre de Sesión";
                int id_usuario = SessionManager.ObtenerInstancia().IdUsuarioActual;
                DateTime fecha = DateTime.Now;
                int criticidad = 2;
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
                SessionManager.ObtenerInstancia().CerrarSesion();
                MessageBox.Show("Se ha cerrado la Sesion Correctamente");
            }
            else
            {
                MessageBox.Show("No se ha iniciado sesion anteriormente");
            }
            this.Hide();
            //CUANDO SE CIERRA SESION, LOS PERMISOS DEL FORMULARIO PRINCIPAL QUEDAN CON LOS PERMISOS DEL ÚLTIMO QUE INICIÓ SESIÓN
        }
        //private void FRMCerrarSesion_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (e.CloseReason == CloseReason.UserClosing)
        //    {
        //        e.Cancel = true;
        //        Hide();
        //    }
        //}
        private void FRMCerrarSesión_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
