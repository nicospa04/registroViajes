using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Paquete: IEntidad<BE.Paquete>
    {
         
        DAL.Paquete dal = new DAL.Paquete();


        public List<BE.Paquete> leerEntidades()
        {
            return dal.leerEntidades();
        }

        public bool crearEntidad(BE.Paquete obj)
        {
            return dal.crearEntidad(obj);
        }

        public bool eliminarEntidad(BE.Paquete obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public bool actualizarEntidad(BE.Paquete obj)
        {
            return dal.actualizarEntidad(obj);
        }
    }
}
