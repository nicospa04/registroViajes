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
        
        public decimal precio_base { get; set; }
        
        public string nombre { get; set; }
        public string descripcion { get; set; }
         
        public int cant_max_personas { get; set; }

        public int id_fecha { get; set; }

        public Paquete(int id_paquete, decimal precio_base, string nombre, string descripcion, int cant_max_personas, int id_fecha)
        {
            this.id_paquete = id_paquete;
            this.precio_base = precio_base;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.cant_max_personas = cant_max_personas;
            this.id_fecha = id_fecha;
        }

        public Paquete(decimal precio_base, string nombre, string descripcion, int cant_max_personas, int id_fecha)
        {
            this.precio_base = precio_base;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.cant_max_personas = cant_max_personas;
            this.id_fecha = id_fecha;
        }




        }
}
