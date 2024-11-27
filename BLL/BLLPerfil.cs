using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BLL
{
    public class BLLPerfil
    {
        DALPerfil dal;
        public BLLPerfil()
        {
            dal = new DALPerfil();
        }
        public Servicios.Resultado<BEPerfil> aggPerfil(BEPerfil obj)
        {
            return dal.aggPerfil(obj);
        }
        public List<BEPerfil> cargarCBPerfil()
        {
            return dal.cargarCBPerfil();
        }
        public List<BEPerfil> cargarCBPermisos()
        {
            return dal.cargarCBPermisos();
        }
    }
}
