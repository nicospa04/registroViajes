using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Empresa
    {
        int id_empresa {  get; set; }
        string nombre { get; set; }
        string descripcion { get; set; }


        public Empresa (int id_empresa, string nombre, string descripcion)
        {
            this.id_empresa = id_empresa;
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public Empresa(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }



    }
}
