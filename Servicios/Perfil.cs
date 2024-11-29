using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Servicios
{
    public class Perfil : Componente
    {
        public string Nombre { get; set; } 
        public string Permisos { get; set; }
        private List<Componente> Hijos = new List<Componente>();
        public Perfil(string nombre)
        {
            Nombre = nombre;
        }

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
