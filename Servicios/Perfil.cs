using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Perfil: Componente
    {
        List<Componente> Hijos = new List<Componente>();
        public override void AgregarHijo(Componente c)
        {
            Hijos.Add(c);
        }
        public override List<Componente> ObtenerHijos()
        {
            return Hijos;
        }
        public override void QuitarHijo(Componente c)
        {
            Hijos.Remove(c);
        }
    }
}
