using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace RegistroViajes
{
    public partial class FRMVerViajesRealizados : Form
    {
        List<BE.Viaje> viajes = new List<BE.Viaje>();
        public FRMVerViajesRealizados()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            BLLViaje bllviaje = new BLLViaje();
            BLLUsuario bllusuario = new BLLUsuario();
            DAL.Usuario dal = new DAL.Usuario();
            
            BLL.BLLViaje bll = new BLL.BLLViaje();

           
            int idUsuario;
            bool user = int.TryParse(txtDni.Text, out idUsuario);

            if (user)
            {
                // Buscar los viajes asociados al id_usuario
                List<BE.Viaje> viajes = bllviaje.ObtenerViajesPorUsuarioId(idUsuario);

                if (viajes != null && viajes.Count > 0)
                {
                    // Mostrar los viajes en el DataGridView
                    dataGridView1.DataSource = viajes.Select(v => new
                    {
                        v.id_viaje,
                        v.id_empresa,
                        v.id_destino,
                        v.transporte,
                        v.cant_adulto,
                        v.cant_niños,
                        v.costo,
                        v.fecha_inicio,
                        v.fecha_vuelta
                    }).ToList();
                }
                else
                {
                    // Si no se encuentran viajes, mostrar un mensaje
                    MessageBox.Show("No se encontraron viajes para este usuario.");
                }
            }
            else
            {
                // Si el id_usuario ingresado no es válido
                MessageBox.Show("Por favor, ingrese un ID de usuario válido.");
            }

        }
    }
}
