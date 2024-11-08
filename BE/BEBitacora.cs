using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEBitacora
    {
        public int id_bitacora { get; set; }
        public int id_usuario { get; set; }
        public string operacion { get; set; }
        public DateTime fecha { get; set; }
        public string actor { get; set; }
        public int criticidad { get; set; }

        public BEBitacora(int id_bitacora, int id_usuario, string operacion, DateTime fecha, string actor, int criticidad)
        {
            this.id_bitacora = id_bitacora;
            this.id_usuario = id_usuario;
            this.operacion = operacion;
            this.fecha = fecha;
            this.actor = actor;
            this.criticidad = criticidad;
        }
        public BEBitacora(int id_usuario, string operacion, DateTime fecha, string actor, int criticidad)
        {
            this.id_usuario = id_usuario;
            this.operacion = operacion;
            this.fecha = fecha;
            this.actor = actor;
            this.criticidad = criticidad;
        }
    }
}
