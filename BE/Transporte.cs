using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Transporte
    {
        public int id_transporte {  get; set; }
        public string nombre { get; set; }
        public decimal porcentaje_extra { get; set; }


        public Transporte(int id_transporte, string nombre, decimal porcentaje_extra)
        {
            this.id_transporte = id_transporte;
            this.nombre = nombre;
            this.porcentaje_extra = porcentaje_extra;
        }
    }
}
