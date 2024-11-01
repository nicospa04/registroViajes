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
        private static Usuario _user = null;
        private static bool inicióSesion = false;
        private SessionManager() { }

        public static SessionManager ObtenerInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new SessionManager();
            }
            return Instancia;
        }

        public void IniciarSesion(Usuario userNuevo)
        {
            if (Instancia != null)
            {
                _user = userNuevo;
                inicióSesion = true;
                return;
            }

        }

        public static bool verificarInicioSesion (){
            return inicióSesion;
        }


        public void CerrarSesion()
        {
            if (Instancia != null)
            {
                _user = null;
                inicióSesion = false;
            }
            //_user = null;
        }

        public Usuario Usuario
        {
            get { return _user;}
        }

        public static Usuario Obtenerdatosuser()
        {
            return _user;
        }
    }
}
