using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Paquete
    {
        int id_paquete {  get; set; }
        int id_destino { get; set; }
        float precio_base { get; set; }
        int cupo_personas { get; set; }
        string nombre { get; set; }
        string descripcion { get; set; }

        public Paquete(int id_paquete ,int id_destino , float precio_base ,int cupo_personas ,string nombre ,string descripcion)
        {
            this.id_paquete = id_paquete;
            this.id_destino = id_destino;
            this.precio_base = precio_base;
            this.cupo_personas = cupo_personas;
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public Paquete(int id_destino, float precio_base, int cupo_personas, string nombre, string descripcion)
        {
            this.id_destino = id_destino;
            this.precio_base = precio_base;
            this.cupo_personas = cupo_personas;
            this.nombre = nombre;
            this.descripcion = descripcion;
        }




    }
}
