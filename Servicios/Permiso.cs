using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios
{
    public class Permiso
    {
        public string Nombre { get; set; }
        public string NombreFormulario { get; set; }
        public bool IsPerfil { get; set; }  // Indica si el permiso es un perfil compuesto

        public List<Permiso> PermisosHijos { get; set; }  // Lista de permisos hijos

        public Permiso(string nombre, string formulario, bool isPerfil = false)
        {
            Nombre = nombre;
            NombreFormulario = formulario;
            IsPerfil = isPerfil;
            PermisosHijos = new List<Permiso>();  // Inicializamos la lista de permisos hijos
        }

        // Método para verificar si este permiso incluye un permiso hijo
        public bool IncluyePermiso(string permisoRequerido)
        {
            if (Nombre == permisoRequerido)
            {
                return true;  // Si este permiso es el requerido
            }

            // Recorrer los permisos hijos
            foreach (var permisoHijo in PermisosHijos)
            {
                if (permisoHijo.IncluyePermiso(permisoRequerido))
                {
                    return true;  // Si alguno de los hijos incluye el permiso requerido
                }
            }

            return false;  // Si no se encuentra el permiso en este perfil ni en sus hijos
        }

    }
}
