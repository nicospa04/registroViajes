using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEPerfil
    {
        public int id_permiso { get; set; }
        public string nombre { get; set; }
        public string permiso { get; set; }
        public bool is_perfil { get; set; }

        public BEPerfil(int id_permiso, string nombre, string permiso, bool is_perfil)
        {
            this.id_permiso = id_permiso;
            this.nombre = nombre;
            this.permiso = permiso;
            this.is_perfil = is_perfil;
        }
        public BEPerfil(string nombre, string permiso, bool is_perfil)
        {
            this.nombre = nombre;
            this.permiso = permiso;
            this.is_perfil = is_perfil;
        }
    }
}
