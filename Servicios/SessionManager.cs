using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Servicios
{
    public sealed class SessionManager
    {
        private static SessionManager Instancia = null;
        private static Usuario _user;
        private SessionManager() { }

        public static SessionManager ObtenerInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new SessionManager();
            }
            return Instancia;
        }

        public static void IniciarSesion(Usuario userNuevo)
        {
            if (Instancia == null)
            {
                Instancia = new SessionManager();
                _user = userNuevo;
            }
            else
            {
                throw new Exception(message: "Sesion ya Iniciada");
            }
        }

        public void CerrarSesion()
        {
            if (Instancia != null)
            {
                _user = null;
            }
            else
            {
                throw new Exception(message: "Sesion no Iniciada");
            }
        }

    }
}
