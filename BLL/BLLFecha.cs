using BE;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLFecha : IEntidad<BE.Fecha>
    {
        DAL.DALFecha dal = new DAL.DALFecha();

        public Resultado<Fecha> actualizarEntidad(Fecha obj)
        {
            return dal.actualizarEntidad(obj);
        }

        public Resultado<Fecha> crearEntidad(Fecha obj)
        {
            return dal.crearEntidad(obj);
        }

        public Resultado<Fecha> eliminarEntidad(Fecha obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public List<Fecha> leerEntidades()
        {
            return dal.leerEntidades();
        }
    }
}
