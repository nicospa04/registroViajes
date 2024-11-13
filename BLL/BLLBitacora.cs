using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;
using Servicios;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLBitacora : IEntidad<BEBitacora>
    {
        DALBitacora dal = new DALBitacora();
        public List<BEBitacora> leerEntidades()
        {
            return dal.leerEntidades();
        }
        public Servicios.Resultado<BEBitacora> crearEntidad(BEBitacora obj)
        {
            return dal.crearEntidad(obj);
        }
        public Servicios.Resultado<BEBitacora> eliminarEntidad(BEBitacora obj)
        {
            return dal.eliminarEntidad(obj);
        }
        public Servicios.Resultado<BEBitacora> actualizarEntidad(BEBitacora obj)
        {
            return dal.actualizarEntidad(obj);
        }
    }
}
