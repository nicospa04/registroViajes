using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Asiento
    {
        public int id_asiento {  get; set; }
        public int id_fecha { get; set; }
        public int num_asiento { get; set; }
        public bool esta_disponible { get; set; }

        public Asiento(int id_asiento, int id_fecha, int num_asiento, bool esta_disponible)
        {
            this.id_asiento = id_asiento;
            this.id_fecha = id_fecha;
            this.num_asiento = num_asiento;
            this.esta_disponible = esta_disponible;
        }
    }
}
