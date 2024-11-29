using BE;
using BLL;
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
    public partial class HacerReservaConPaquete : Form, IObserver
    {
        int id_paquete;
        private List<BE.Asiento> asientosSeleccionados = new List<BE.Asiento>(); // Lista temporal de asientos seleccionados

        public HacerReservaConPaquete(int id_paquete)
        {
            InitializeComponent();
            this.id_paquete = id_paquete;
        }
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        void cargarPaquete()
        {
            var paquete = new BLLPaquete().leerEntidades().FirstOrDefault(p => p.id_paquete == id_paquete);
            
            var fecha = new BLLFecha().leerEntidades().FirstOrDefault(f => f.id_fecha == paquete.id_fecha);

            var transporte = new BLLTransporte().leerEntidades().FirstOrDefault(t => t.id_transporte
            == fecha.id_transporte);

            switch (transporte.modelo)
            {
                case "Avión":
                    CrearDisposicionAsientos(10, 6); // Ejemplo de disposición para avión
                    break;
                case "Bus":
                    CrearDisposicionAsientos(8, 4); // Ejemplo de disposición para bus
                    break;
                case "Barco":
                    CrearDisposicionAsientos(5, 6); // Ejemplo de disposición para barco
                    break;
            }

            label1.Text = $"Paquete: {paquete.nombre}, costo: ${paquete.precio_base}";

        }

        private void BtnAsiento_Click(object sender, EventArgs e)
        {

            var paquete = new BLLPaquete().leerEntidades().FirstOrDefault(p => p.id_paquete == id_paquete);

            if(asientosSeleccionados.Count > paquete.cant_max_personas)
            {
                MessageBox.Show("No puede seleccionar más asientos de los que tiene el paquete.");
                return;
            }

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
            }
            else
            {
                // Si el asiento no está en la lista de seleccionados, lo selecciona
                asientosSeleccionados.Add(asiento);
                asientoButton.BackColor = Color.Yellow; // Cambiar a color amarillo para indicar selección
            }
        }

        void CrearDisposicionAsientos(int filas, int columnas)
        {
            var paquete = new BLLPaquete().leerEntidades().FirstOrDefault(p => p.id_paquete == id_paquete);

            var fecha = new BLLFecha().leerEntidades().FirstOrDefault(f => f.id_fecha == paquete.id_fecha);

            // Obtener la lista de asientos para la fecha específica
            BLLAsiento bllAsiento = new BLLAsiento();
            List<BE.Asiento> asientos = bllAsiento.leerEntidades().Where(a => a.id_fecha == fecha.id_fecha).ToList();

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

        private void HacerReservaConPaquete_Load(object sender, EventArgs e)
        {
            //ActualizarIdioma();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            if(asientosSeleccionados.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un asiento.");
                return;
            }

            var paquete = new BLLPaquete().leerEntidades().FirstOrDefault(p => p.id_paquete == id_paquete);
            
            if(asientosSeleccionados.Count > paquete.cant_max_personas)
            {
                MessageBox.Show("No puede seleccionar mas cantidad de asientos de las que ofrece el paquete");
                return;
            }


            foreach(var a in asientosSeleccionados)
            {
                BLLAsiento bllAsiento = new BLLAsiento();
                bllAsiento.marcarAsientoOcupado(a);
                BLLBitacora bllbita = new BLLBitacora();
                string operacion = "Reserva Asiento Paquete";
                int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
                DateTime fecha1 = DateTime.Now;
                int criticidad = 10;
                BLLUsuario BLLUser = new BLLUsuario();
                int idpermi = BLLUser.DevolverIdPermisoPorId(id_usuario1);
                string actor = BLLUser.obtenernamepermisoporID(idpermi.ToString());

                BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
                bllbita.crearEntidad(bitacorita);

                BLL.BLLViaje bllViaje = new BLL.BLLViaje();

                var user = Servicios.SessionManager.Obtenerdatosuser();

                var fecha = new BLLFecha().leerEntidades().FirstOrDefault(f => f.id_fecha == paquete.id_fecha);
                var empresa = new BLL.BLLEmpresa().leerEntidades().FirstOrDefault(emp => emp.id_empresa == fecha.id_empresa);


                decimal costo = paquete.precio_base;

                var transporte = new BLLTransporte().leerEntidades().FirstOrDefault(t => t.id_transporte == fecha.id_transporte);

                 BE.Viaje viaje = new BE.Viaje(0,user.id_usuario, empresa.id_empresa, paquete.id_fecha, transporte.modelo, costo, a.num_asiento);
                   bllViaje.crearEntidad(viaje);
            }

        }
    }
}
