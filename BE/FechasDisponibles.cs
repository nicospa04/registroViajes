using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class FechasDisponibles
    {
        int id_fecha {  get; set; }
        int id_paquete { get; set; }
        DateTime fecha_inicio { get; set; }
        DateTime fecha_vuelta { get; set; }

        public FechasDisponibles(int id_fecha, int id_paquete, DateTime fecha_inicio, DateTime fecha_vuelta)
        {
            this.id_fecha = id_fecha;
            this.id_paquete = id_paquete;
            this.fecha_inicio = fecha_inicio;
            this.fecha_vuelta = fecha_vuelta;
        }

        public FechasDisponibles(int id_paquete, DateTime fecha_inicio, DateTime fecha_vuelta)
        {
            this.id_paquete = id_paquete;
            this.fecha_inicio = fecha_inicio;
            this.fecha_vuelta = fecha_vuelta;
        }



    }
}
