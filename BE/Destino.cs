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

        public Destino(int id_destino, string nombre, string descripcion)
        {
            this.id_destino = id_destino;
            this.nombre = nombre;
            this.descripcion = descripcion;
        }
        public Destino(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

    }
}
