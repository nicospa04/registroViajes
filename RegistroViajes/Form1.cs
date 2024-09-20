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
 

namespace RegistroViajes
{
    public partial class Form1 : Form
    {
        
        private static Form formactivo = null;


        public Form1()
        {
            InitializeComponent();
            BaseDeDatos bd = new BaseDeDatos();
            bd.scriptInicio();
            
            
        }

        private void AbrirForm(Form formu)
        {

            if(formactivo!=null)
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
            AbrirForm(new FRMIniciarSesion());
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
    }
}
