using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Paquete
    {
        public int id_paquete {  get; set; }
        public int id_destino { get; set; }
        public float precio_base { get; set; }
        public int cupo_personas { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_vuelta { get; set; }

        public Paquete(int id_paquete ,int id_destino , float precio_base ,int cupo_personas ,string nombre ,string descripcion, DateTime fecha_inicio, DateTime fecha_vuelta)
        {
            this.id_paquete = id_paquete;
            this.id_destino = id_destino;
            this.precio_base = precio_base;
            this.cupo_personas = cupo_personas;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.fecha_vuelta = fecha_vuelta;
            this.fecha_inicio = fecha_inicio;
        }

        public Paquete(int id_destino, float precio_base, int cupo_personas, string nombre, string descripcion, DateTime fecha_inicio, DateTime fecha_vuelta)
        {
            this.id_destino = id_destino;
            this.precio_base = precio_base;
            this.cupo_personas = cupo_personas;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.fecha_inicio=fecha_inicio;
            this.fecha_vuelta =fecha_vuelta;
        }




    }
}
