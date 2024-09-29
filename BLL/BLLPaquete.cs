using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPaquete: IEntidad<BE.Paquete>
    {

        DAL.Paquete dal;

        public BLLPaquete()
        {
            dal = new DAL.Paquete();
        }


        public List<BE.Paquete> leerEntidades()
        {
            return dal.leerEntidades();
        }

        public Servicios.Resultado<BE.Paquete> crearEntidad(BE.Paquete obj)
        {
            return dal.crearEntidad(obj);
        }

        public Servicios.Resultado<BE.Paquete> eliminarEntidad(BE.Paquete obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public Servicios.Resultado<BE.Paquete> actualizarEntidad(BE.Paquete obj)
        {
            return dal.actualizarEntidad(obj);
        }
    }
}
