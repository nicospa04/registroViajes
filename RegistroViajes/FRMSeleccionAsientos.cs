using BE;
using BLL;
using DAL;
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
    public partial class FRMSeleccionAsientos : Form 
    {
        int id_fecha;
        private List<BE.Asiento> asientosSeleccionados = new List<BE.Asiento>(); // Lista temporal de asientos seleccionados
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        public FRMSeleccionAsientos(int id_fecha)
        {
            InitializeComponent();
            this.id_fecha = id_fecha;
            var fecha = encontrarFecha();
            var transporte = encontrarTransporte(fecha.id_transporte);

            var destino = new BLLDestino().leerEntidades().FirstOrDefault(d => d.id_destino == fecha.id_lugar_destino);
            var empresa = new BLLEmpresa().leerEntidades().FirstOrDefault(e => e.id_empresa == fecha.id_empresa);
            label1.Text = $"Reserva {destino.nombre} - {transporte.modelo}";


            decimal costo = new BLLViaje().calcularCostoViaje(fecha,empresa , transporte , destino);

            label2.Text = $"Costo: ${costo}";

            var tipoTransporte = new BLL.TipoTransporte().leerEntidades().FirstOrDefault(t => t.id_tipo_transporte == transporte.id_tipo_transporte);



            switch (tipoTransporte.nombre.ToLower().Trim())
            {
                case "avion":
                    CrearDisposicionAsientos(10, 6); // Ejemplo de disposición para avión
                    break;
                case "bus":
                    CrearDisposicionAsientos(8, 4); // Ejemplo de disposición para bus
                    break;
                case "barco":
                    CrearDisposicionAsientos(5, 6); // Ejemplo de disposición para barco
                    break;
            }

        }

        void CrearDisposicionAsientos(int filas, int columnas)
        {
            // Obtener la lista de asientos para la fecha específica
            BLLAsiento bllAsiento = new BLLAsiento();
            List<BE.Asiento> asientos = bllAsiento.leerEntidades().Where(a => a.id_fecha == id_fecha).ToList();

            for (int fila = 0; fila < filas; fila++)
            {
                for (int columna = 0; columna < columnas; columna++)
                {
                    int numeroAsiento = fila * columnas + columna + 1;
                    BE.Asiento asiento = asientos.FirstOrDefault(a => a.num_asiento == numeroAsiento);

                    // Crear un botón para el asiento
                    Button btnAsiento = new Button();
                    btnAsiento.Width = 30;
                    btnAsiento.Height = 30;
                    btnAsiento.Left = columna * 35;
                    btnAsiento.Top = fila * 35;
                    btnAsiento.Text = numeroAsiento.ToString();
                    btnAsiento.Tag = asiento; // Asocia el asiento a este botón

                    // Establecer el color según la disponibilidad
                    if (asiento != null && !asiento.esta_disponible) // 0 = Ocupado
                    {
                        btnAsiento.BackColor = Color.Red;
                    }
                    else
                    {
                        btnAsiento.BackColor = Color.Green;
                    }

                    // Asigna el evento click para reservar asiento
                    btnAsiento.Click += BtnAsiento_Click;

                    // Agregar el botón al panel de asientos en el formulario
                    panelAsientos.Controls.Add(btnAsiento);
                }
            }
        }

        private void BtnAsiento_Click(object sender, EventArgs e)
        {
            Button asientoButton = (Button)sender;
            BE.Asiento asiento = (BE.Asiento)asientoButton.Tag;

            // Si el asiento ya está ocupado, no se puede seleccionar
            if (asiento != null && !asiento.esta_disponible)
            {
                MessageBox.Show("Este asiento está ocupado.");
                return;
            }

            // Si el asiento está seleccionado (verde), lo deselecciona
            if (asientosSeleccionados.Contains(asiento))
            {
                asientosSeleccionados.Remove(asiento);
                asientoButton.BackColor = Color.Green; // Cambiar de vuelta a color verde (disponible)
                var fecha = encontrarFecha();
                var transporte = encontrarTransporte(fecha.id_transporte);

                var destino = new BLLDestino().leerEntidades().FirstOrDefault(d => d.id_destino == fecha.id_lugar_destino);
                var empresa = new BLLEmpresa().leerEntidades().FirstOrDefault(ee => ee.id_empresa == fecha.id_empresa);


                decimal costo = new BLLViaje().calcularCostoViaje(fecha, empresa, transporte, destino);

                label2.Text = $"Costo: ${costo * asientosSeleccionados.Count}";
            }
            else
            {
                // Si el asiento no está en la lista de seleccionados, lo selecciona
                asientosSeleccionados.Add(asiento);
                asientoButton.BackColor = Color.Yellow; // Cambiar a color amarillo para indicar selección

                var fecha = encontrarFecha();
                var transporte = encontrarTransporte(fecha.id_transporte);

                var destino = new BLLDestino().leerEntidades().FirstOrDefault(d => d.id_destino == fecha.id_lugar_destino);
                var empresa = new BLLEmpresa().leerEntidades().FirstOrDefault(ee => ee.id_empresa == fecha.id_empresa);
 

                decimal costo = new BLLViaje().calcularCostoViaje(fecha, empresa, transporte, destino);

                label2.Text = $"Costo: ${costo*asientosSeleccionados.Count}";

            }
        }
        

        BE.Fecha encontrarFecha()
        {
            BLLFecha bllFecha = new BLLFecha();
            List<BE.Fecha> fechas = bllFecha.leerEntidades();
            return fechas.FirstOrDefault(f => f.id_fecha == id_fecha);
        }
        BE.Transporte encontrarTransporte(int id_transporte)
        {
            BLLTransporte bllTransporte = new BLLTransporte();
            List<BE.Transporte> transportes = bllTransporte.leerEntidades();
            return transportes.FirstOrDefault(t => t.id_transporte == id_transporte);

        }

        private void FRMSeleccionAsientos_Load(object sender, EventArgs e)
        {
            //ActualizarIdioma();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BLLAsiento bllAsiento = new BLLAsiento();

            if(asientosSeleccionados.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un asiento");
                return;
            }


            foreach (var asiento in asientosSeleccionados)
            {
                asiento.esta_disponible = false; // Marcar el asiento como ocupado en la entidad
                Servicios.Resultado<BE.Asiento> resultado = bllAsiento.marcarAsientoOcupado(asiento);

                if (resultado.resultado)
                {


                    int id_usuario = SessionManager.Obtenerdatosuser().id_usuario ;
                    var fecha = encontrarFecha();
                    var transporte = encontrarTransporte(fecha.id_transporte);

                    var destino = new BLLDestino().leerEntidades().FirstOrDefault(d => d.id_destino == fecha.id_lugar_destino);
                    var empresa = new BLLEmpresa().leerEntidades().FirstOrDefault(ee => ee.id_empresa == fecha.id_empresa);

                    var tipo_transporte = new BLL.TipoTransporte().leerEntidades().FirstOrDefault(t => t.id_tipo_transporte == transporte.id_tipo_transporte);

                    decimal costo = new BLLViaje().calcularCostoViaje(fecha, empresa, transporte, destino);


                    //int id_usuario, int id_empresa, int id_fecha, string transporte, decimal costo, int num_asiento
                    var viaje = new BLLViaje().crearEntidad(new BE.Viaje(id_usuario, empresa.id_empresa, fecha.id_fecha,tipo_transporte.nombre, costo, asiento.num_asiento));
                    BLLBitacora bllbita = new BLLBitacora();
                    string operacion = "Registro de Viaje";
                    int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
                    DateTime fecha1 = DateTime.Now;
                    int criticidad = 3;
                    string actor;
                    if (id_usuario == 3)
                        actor = "ADMIN";
                    else if (id_usuario == 2)
                        actor = "EMPLEADO";
                    else
                        actor = "USUARIO";

                    BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
                    bllbita.crearEntidad(bitacorita);

                    // Cambiar el color del botón del asiento a rojo en la interfaz
                    foreach (Control control in panelAsientos.Controls)
                    {
                        if (control is Button && ((BE.Asiento)control.Tag).id_asiento == asiento.id_asiento)
                        {
                            control.BackColor = Color.Red;
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Error al reservar el asiento {asiento.num_asiento}.");
                }
            }

            asientosSeleccionados.Clear();
            MessageBox.Show("Asientos reservados con éxito.");
            MessageBox.Show("SE HA RESERVADO UN VIAJE");
        }
    }
}