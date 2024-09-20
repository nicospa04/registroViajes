using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Lenguaje
    {
        private List<IObserver> ListaObserv = new List<IObserver>();
        //ver bien.
        //ver bien.
        private static Lenguaje instance;

        private Lenguaje() { }

        public static Lenguaje ObtenerInstancia()
        {
            if (instance == null)
            {
                instance = new Lenguaje();
            }
            return instance;
        }

        public void Agregar(IObserver observer)
        {
            ListaObserv.Add(observer);
        }
        public void Quitar(IObserver observer)
        {
            ListaObserv.Remove(observer);
        }
        public void Notificar()
        {
            foreach (IObserver observer in ListaObserv)
            {
                observer.ActualizarIdioma();
            }
        }

        public void CambiarIdioma()
        {

        }










    }
}
