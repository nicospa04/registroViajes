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
    public partial class CrearFecha : Form
    {
        public CrearFecha()
        {
            InitializeComponent();

            cargarComboBoxs();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void CrearFecha_Load(object sender, EventArgs e)
        {

        }

        void cargarComboBoxs()
        {
            List<BE.Destino> destinos = new BLLDestino().leerEntidades();
            List<BE.Transporte> transportes = new BLLTransporte().leerEntidades();
            List<BE.Empresa> empresas = new BLLEmpresa().leerEntidades();


            foreach(var empresa in empresas)
            {
                comboBox1.Items.Add(empresa.nombre);
            }

            foreach (var destino in destinos)
            {
                comboBox2.Items.Add(destino.nombre);
            }

            foreach (var destino in destinos)
            {
                comboBox3.Items.Add(destino.nombre);
            }

            foreach (var transporte in transportes)
            {
                comboBox4.Items.Add(transporte.modelo);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fechaIda = dateTimePicker1.Value;
            DateTime fechaVuelta = dateTimePicker2.Value;


            if(fechaIda <= DateTime.Now.AddDays(-1))
            {
                MessageBox.Show("La fecha de ida no puede ser menor a la fecha actual");
                return;
            }

            if (fechaIda > fechaVuelta)
            {
                MessageBox.Show("La fecha de ida no puede ser mayor a la fecha de vuelta");
                return;
            }

            BE.Empresa empresa = new BLLEmpresa().leerEntidades().FirstOrDefault(emp => emp.nombre == comboBox1.Text);
            BE.Destino destinoIda = new BLLDestino().leerEntidades().FirstOrDefault(dest => dest.nombre == comboBox2.Text);
            BE.Destino destinoVuelta = new BLLDestino().leerEntidades().FirstOrDefault(dest => dest.nombre == comboBox3.Text);
            BE.Transporte transporte = new BLLTransporte().leerEntidades().FirstOrDefault(BE => BE.modelo == comboBox4.Text);
            BE.TipoTransporte tipoTransporte = new BLL.TipoTransporte().leerEntidades().FirstOrDefault(BE => BE.id_tipo_transporte == transporte.id_tipo_transporte);
            BE.Fecha fecha = new BE.Fecha()
            {
                fecha_ida = fechaIda,
                fecha_vuelta = fechaVuelta,
                id_empresa = empresa.id_empresa,
                id_lugar_origen = destinoIda.id_destino,
                id_lugar_destino = destinoVuelta.id_destino,
                id_transporte = transporte.id_transporte,
                categoria_tipo = tipoTransporte.nombre
            };

            BLLFecha bllFecha = new BLLFecha();
            Servicios.Resultado<BE.Fecha> resultado = bllFecha.crearEntidad(fecha);
            BLLAsiento bllAsiento = new BLLAsiento();

            var fechas = bllFecha.leerEntidades();
            var fechaCreada = fechas.LastOrDefault(f => f.fecha_ida.Date == fechaIda.Date && f.fecha_vuelta.Date == fechaVuelta.Date);


            bllAsiento.crearAsientosParaFecha(fechaCreada.id_fecha);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var empresa = new BLLEmpresa().leerEntidades().FirstOrDefault(emp => emp.nombre == comboBox1.Text);

            var transportesDeEmpresa = new BLLTransporte().leerEntidades().Select(transporte => transporte).Where(transporte => transporte.id_empresa == empresa.id_empresa);

            comboBox4.Items.Clear();
            foreach (var transporte in transportesDeEmpresa)
            {
                comboBox4.Items.Add(transporte.modelo);
            }

            comboBox4.SelectedIndex = 0;
        }
    }
}
