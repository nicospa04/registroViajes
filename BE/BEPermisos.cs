using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEPermisos
    {
        public int id_permisopadre { get; set; }
        public int id_permisohijo { get; set; }

        public BEPermisos(int idp, int idh)
        {
            this.id_permisopadre = idp;
            this.id_permisohijo = idh;
        }
    }
}
