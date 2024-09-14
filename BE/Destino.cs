using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Destino
    {
       public int id_destino {  get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public float precio_base { get; set; }

        public Destino(int id_destino, string nombre, string descripcion, float precio_base)
        {
            this.id_destino = id_destino;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio_base = precio_base;
        }
        public Destino(string nombre, string descripcion, float precio_base)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio_base = precio_base;
        }

    }
}
