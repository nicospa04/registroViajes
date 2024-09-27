using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BLLUsuario: IEntidad<BE.Usuario>
    {

        DAL.Usuario dal = new DAL.Usuario();    


        public List<BE.Usuario> leerEntidades()
        {
            return dal.leerEntidades();
        }
        public BE.Usuario recuperarUsuario(string email, string contraseña)
        {
            return dal.recuperarUsuario(email, contraseña);
        }

        public bool crearEntidad(BE.Usuario obj)
        {
            return dal.crearEntidad(obj);
        }

        public bool eliminarEntidad(BE.Usuario obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public bool actualizarEntidad(BE.Usuario obj)
        {
            return dal.actualizarEntidad(obj);
        }

        public string recuperarIdioma(BE.Usuario obj)
        {
            return dal.recuperarIdioma(obj.id_usuario.ToString());
        }

        public bool modificarIdioma(BE.Usuario obj, string nuevoIdioma)
        {
            bool result = dal.modificarIdioma(obj, nuevoIdioma);
            return result;
        }

    }
}
