using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BLL
{
    public class BLLPermisos
    {
        DALPermisos dal;
        public BLLPermisos()
        {
            dal = new DALPermisos();
        }
        public Servicios.Resultado<BEPermisos> aggPermisos(BEPermisos obj)
        {
            return dal.aggPermisos(obj);
        }

    }
}
