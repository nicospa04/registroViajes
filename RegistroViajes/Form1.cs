﻿using DAL;
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
using BE;

namespace RegistroViajes
{
    public partial class Form1 : Form, IObserver
    {
        private static Form formactivo = null;
        public Form1()
        {
            InitializeComponent();
            BaseDeDatos bd = new BaseDeDatos();
            bd.scriptInicio();
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
            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "EXIT";
            int id_usuario = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha = DateTime.Now;
            int criticidad = 0;

            string actor;
            if (id_usuario == 3)
                actor = "ADMIN";
            else if (id_usuario == 2)
                actor = "EMPLEADO";
            else
                actor = "USUARIO";

            BEBitacora bitacorita = new BEBitacora(id_usuario, operacion, fecha, actor, criticidad);
            bllbita.crearEntidad(bitacorita);
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
            FRMCerrarSesión formCerrarSesion = new FRMCerrarSesión(this);
            AbrirForm(formCerrarSesion);
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
            fechasToolStripMenuItem.Enabled = false;
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
            FRMBitacora frm = new FRMBitacora();
            frm.ShowDialog();
        }

        private void crearFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new CrearFecha());
        }

        private void paquetesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reservarPaqueteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AbrirForm(new HacerReservaConPaquete());
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //VerPerfil formu = new VerPerfil();
            //AbrirForm(formu);
        }

        private void perfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerPerfil formu = new VerPerfil();
            AbrirForm(formu);
        }

        private void backUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackUp formu = new BackUp();
            AbrirForm(formu);
        }

        private void crearPerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CPerfil formu = new CPerfil();
            AbrirForm(formu);
        }
    }
}