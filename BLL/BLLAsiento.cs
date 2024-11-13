using BE;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLAsiento : IEntidad<BE.Asiento>
    {
        DAL.Asiento Dal = new DAL.Asiento();

        public Resultado<Asiento> actualizarEntidad(Asiento obj)
        {
            return Dal.actualizarEntidad(obj);
        }

        public Resultado<Asiento> crearEntidad(Asiento obj)
        {
            return Dal.crearEntidad(obj);
        }

        public Resultado<Asiento> eliminarEntidad(Asiento obj)
        {
            return Dal.eliminarEntidad(obj);
        }

        public List<Asiento> leerEntidades()
        {
           return Dal.leerEntidades();
        }
    }
}
