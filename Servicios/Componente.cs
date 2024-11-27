using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public abstract class Componente
    {
        public abstract void AgregarHijo(Componente c);
        public abstract void QuitarHijo(Componente c);
        public abstract List<Componente> ObtenerHijos();
    }
}
