using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class UserComp
    {
        public string Nombre { get; set; }
        public List<Permiso> Permisos { get; set; }

        public UserComp(string nombre)
        {
            Nombre = nombre;
            Permisos = new List<Permiso>();
        }

        // Método para verificar si el usuario tiene un permiso o si alguno de sus permisos compuestos lo incluye
        public bool TienePermiso(string permisoRequerido)
        {
            foreach (var permiso in Permisos)
            {
                if (permiso.IncluyePermiso(permisoRequerido))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
