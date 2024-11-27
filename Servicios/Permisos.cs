using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Permisos: Componente
    {
        public Permisos(string permisoSeleccionado)
        {
            PermisoSeleccionado = permisoSeleccionado;
        }

        public string PermisoSeleccionado { get; }

        public override void AgregarHijo(Componente c) { }
        public override List<Componente> ObtenerHijos() { return null; }
        public override void QuitarHijo(Componente c) { }
    }
}
