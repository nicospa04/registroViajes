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
    public partial class FRMVerViajesRealizados : Form, IObserver
    {
        List<BE.Viaje> viajes = new List<BE.Viaje>();
        BLLViaje bllviaje = new BLLViaje();
        public FRMVerViajesRealizados()
        {
            InitializeComponent();
            Lenguaje.ObtenerInstancia().Agregar(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        private void button1_Click(object sender, EventArgs e)  //A ESTO LE FALTA EL SCRIPT DE LA BASE DE DATOS
        {
            int idUsuario;
            bool user = int.TryParse(txtDni.Text, out idUsuario);
            if (user)
            {
                // Buscar los viajes asociados al id_usuario
                viajes = bllviaje.ObtenerViajesPorUsuarioId(idUsuario);
                if (viajes != null && viajes.Count > 0)
                {
                    // Mostrar los viajes en el DataGridView
                    dataGridView1.DataSource = viajes.Select(v => new
                    {
                        v.id_viaje,
                        v.id_usuario,
                        v.id_empresa,
                        v.id_fecha,
                        v.transporte,
                        v.costo,
                        v.num_asiento
                    }).ToList();
                    BLLBitacora bllbita = new BLLBitacora();
                    string operacion = "Ver Viajes";
                    int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
                    DateTime fecha1 = DateTime.Now;
                    int criticidad = 9;
                    BLLUsuario BLLUser = new BLLUsuario();
                    int idpermi = BLLUser.DevolverIdPermisoPorId(id_usuario1);
                    string actor = BLLUser.obtenernamepermisoporID(idpermi.ToString());

                    BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
                    bllbita.crearEntidad(bitacorita);
                }
                else
                {
                    MessageBox.Show("No se encontraron viajes para este usuario.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un ID de usuario válido.");
            }
        }

        private void FRMVerViajesRealizados_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }
    }
}
