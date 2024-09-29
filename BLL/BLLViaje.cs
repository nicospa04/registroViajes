using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLViaje : IEntidad<BE.Viaje>
    {

        DAL.Viaje dal = new DAL.Viaje();


        public List<BE.Viaje> leerEntidades()
        {
            return dal.leerEntidades();
        }

        public Servicios.Resultado<BE.Viaje>  crearEntidad(BE.Viaje obj)
        {
            return dal.crearEntidad(obj);
        }

        public Servicios.Resultado<BE.Viaje>  eliminarEntidad(BE.Viaje obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public Servicios.Resultado<BE.Viaje>  actualizarEntidad(BE.Viaje obj)
        {
            return dal.actualizarEntidad(obj);
        }
    }

}