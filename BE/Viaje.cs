using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Viaje
    {
        int id_viaje {  get; set; }
        int id_usuario { get; set; }
        int id_empresa { get; set; }
        int id_destino { get; set; }
        int id_fecha { get; set; }
        string transporte { get; set; }
        int cant_adulto { get; set; }
        int cant_niños { get; set; }
        float costo { get; set; }

        public Viaje(int id_viaje, int id_usuario, int id_empresa, int id_destino, int id_fecha, string transporte, int cant_adulto, int cant_niños, float costo)
        {
            this.id_viaje = id_viaje;
            this.id_usuario = id_usuario;
            this.id_empresa = id_empresa;
            this.id_destino = id_destino;
            this.id_fecha = id_fecha;
            this.transporte = transporte;
            this.cant_adulto = cant_adulto;
            this.cant_niños = cant_niños;
            this.costo = costo;
        }

        public Viaje(int id_usuario, int id_empresa, int id_destino, int id_fecha, string transporte, int cant_adulto, int cant_niños, float costo)
        {
            this.id_usuario = id_usuario;
            this.id_empresa = id_empresa;
            this.id_destino = id_destino;
            this.id_fecha = id_fecha;
            this.transporte = transporte;
            this.cant_adulto = cant_adulto;
            this.cant_niños = cant_niños;
            this.costo = costo;
        }

    }
}
