using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Empresa: IEntidad<BE.Empresa>
    {
       

        DAL.Empresa dal = new DAL.Empresa();

        public List<BE.Empresa> leerEntidades()
        {
            return dal.leerEntidades();
        }

        public bool crearEntidad(BE.Empresa obj)
        {
            return dal.crearEntidad(obj);
        }

        public bool eliminarEntidad(BE.Empresa obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public bool actualizarEntidad(BE.Empresa obj)
        {
            return dal.actualizarEntidad(obj);
        }
    }
}
