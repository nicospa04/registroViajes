using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Empresa
    {
        public int id_empresa {  get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public float porcentaje_extra {  get; set; }

        public Empresa (int id_empresa, string nombre, string descripcion, float porcentaje_extra)
        {
            this.id_empresa = id_empresa;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.porcentaje_extra = porcentaje_extra;
        }

        public Empresa(string nombre, string descripcion, float porcentaje_extra)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.porcentaje_extra = porcentaje_extra;
        }



    }
}
