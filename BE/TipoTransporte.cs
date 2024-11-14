using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoTransporte
    {
        public int id_tipo_transporte { get; set; }
        public string nombre { get; set; }

        public TipoTransporte()
        {

        }

        public TipoTransporte(int id, string nom)
        {
            this.id_tipo_transporte = id;
            this.nombre = nom;
        }
    }
}
