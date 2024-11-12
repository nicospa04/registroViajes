using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Viaje
    {
        public int id_viaje {  get; set; }
        public int id_usuario { get; set; }
        public int id_empresa { get; set; }
        public int id_fecha { get; set; }
        public string transporte { get; set; }
        public decimal costo { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_vuelta { get; set; }

        public Viaje(int id_viaje, int id_usuario, int id_empresa, int id_fecha, string transporte, decimal costo, DateTime fecha_inicio, DateTime fecha_vuelta)
        {
            this.id_viaje = id_viaje;
            this.id_usuario = id_usuario;
            this.id_empresa = id_empresa;
            this.id_fecha = id_fecha;
            this.transporte = transporte;
            this.costo = costo;
            this.fecha_inicio = fecha_inicio;
            this.fecha_vuelta = fecha_vuelta;
        }

        public Viaje(int id_usuario, int id_empresa, int id_fecha, string transporte, decimal costo, DateTime fecha_inicio, DateTime fecha_vuelta)
        {
            this.id_usuario = id_usuario;
            this.id_empresa = id_empresa;
            this.id_fecha = id_fecha;
            this.transporte = transporte;
            this.costo = costo;
            this.fecha_inicio = fecha_inicio;
            this.fecha_vuelta = fecha_vuelta;
        }

    }
}
