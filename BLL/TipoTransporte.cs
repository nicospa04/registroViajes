using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TipoTransporte : IEntidad<BE.TipoTransporte>
    {

        DAL.TipoTransporte dal = new DAL.TipoTransporte();

        public Resultado<BE.TipoTransporte> actualizarEntidad(BE.TipoTransporte obj)
        {
            return dal.actualizarEntidad(obj);
        }

        public Resultado<BE.TipoTransporte> crearEntidad(BE.TipoTransporte obj)
        {
            return dal.crearEntidad(obj);
        }

        public Resultado<BE.TipoTransporte> eliminarEntidad(BE.TipoTransporte obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public List<BE.TipoTransporte> leerEntidades()
        {
            return dal.leerEntidades();
        }
    }
}
