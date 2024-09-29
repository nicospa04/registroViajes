using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Resultado<T>
    {
        public bool resultado { get; set; }
        public T entidad { get; set; }
        public string mensaje { get; set; }
    }
}
