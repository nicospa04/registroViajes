using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using BLL;
using Servicios;

namespace RegistroViajes
{
    public partial class Form1 : Form, IObserver
    {
        private static Form formactivo = null;
        public Form1()
        {
            InitializeComponent();
            BaseDeDatos bd = new BaseDeDatos();
         //   bd.scriptInicio();
            Lenguaje.ObtenerInstancia().Agregar(this);
            Lenguaje.ObtenerInstancia().IdiomaActual = "Español";
            modifmenu();
        }
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
        FormManager formManager = new FormManager();
        private void AplicarPermisos(object sender, EventArgs e)
        {
            BLLUsuario bll = new BLLUsuario();
            List<Permiso> permisos = bll.obtenerPermisosUsuario(SessionManager.Obtenerdatosuser().id_usuario);
            // Primero, oculta todos los formularios del MDI y menús
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                formManager.HabilitarMenusPorPermisos(item, permisos);
            }
            // Después, recorre todos los formularios MDI y aplica los permisos recursivamente
            //formManager.HabilitarFormulariosRecursivos(this, permisos);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void cerraarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iconMenuItem6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void iconMenuItem7_Click(object sender, EventArgs e)
        {
            FRMIniciarSesion formu = new FRMIniciarSesion();
            formu.TopLevel = false;
            formu.FormBorderStyle = FormBorderStyle.None;
            formu.Dock = DockStyle.Fill;
            this.Controls.Add(formu);
            formu.Show();
            formu.inicioSesionCorrecto += AplicarPermisos;
        }
        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            AbrirForm(new FRMReservarViaje());
        }
        private void iconMenuItem8_Click(object sender, EventArgs e)
        {

        }
        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            AbrirForm(new FRMModificarViaje());
        }
        private void iconMenuItem3_Click(object sender, EventArgs e)
        {
            AbrirForm(new FRMPaquetes());
        }
        private void cancelarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AbrirForm(new FRMCancelarViaje());
        }
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("¿Estas seguro de Cerrar Sesión?", "Confirmar Cierre de Sesión", MessageBoxButtons.YesNo);
            //if(result == DialogResult.Yes)
            //{
            //    SessionManager.ObtenerInstancia().CerrarSesion();
            //    modifmenu();
            //}
            //AbrirForm(new FRMIniciarSesion());
            AbrirForm(new FRMCerrarSesión());
        }
        public void modifmenu()
        {
            iconMenuItem1.Enabled = true;
            iconMenuItem7.Enabled = true;
            cerrarSesionToolStripMenuItem.Enabled = true;
            iconMenuItem2.Enabled = false;
            iconMenuItem3.Enabled = false;
            iconMenuItem4.Enabled = false;
            registrosToolStripMenuItem.Enabled = false;
            iconMenuItem5.Enabled = true;
            iconMenuItem6.Enabled = true;
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        private void iconMenuItem5_Click(object sender, EventArgs e)
        {
            FRMCambiarIdioma fRM = new FRMCambiarIdioma();
            fRM.ShowDialog();
        }
        private void iconMenuItem1_Click(object sender, EventArgs e)
        {

        }
        private void verViajesRealizadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FRMVerViajesRealizados());
        }
        private void registrosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FRMRegistrarCliente());
        }
        private void empresasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FRMBitacora());
        }

        private void crearFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new CrearFecha());
        }
    }
}
