using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLEmpresa: IEntidad<BE.Empresa>
    {
       

        DAL.Empresa dal = new DAL.Empresa();

        public List<BE.Empresa> leerEntidades()
        {
            return dal.leerEntidades();
        }

        public Servicios.Resultado<BE.Empresa> crearEntidad(BE.Empresa obj)
        {
            return dal.crearEntidad(obj);
        }

        public Servicios.Resultado<BE.Empresa>  eliminarEntidad(BE.Empresa obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public Servicios.Resultado<BE.Empresa>  actualizarEntidad(BE.Empresa obj)
        {
            return dal.actualizarEntidad(obj);
        }

        public string devolverNombrePorId(string id)
        {
            return dal.devolverNombrePorId(id);
        }


    }
}
