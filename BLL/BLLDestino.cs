using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLDestino: IEntidad<BE.Destino>
    {
        DAL.Destino destino = new DAL.Destino();


        public List<BE.Destino> leerEntidades()
        {

            DAL.Destino destino = new DAL.Destino();
            return destino.leerEntidades();
        }

        public Servicios.Resultado<BE.Destino> crearEntidad(BE.Destino obj)
        {
            return destino.crearEntidad(obj);
        }

        public Servicios.Resultado<BE.Destino>  eliminarEntidad(BE.Destino obj)
        {
            return destino.eliminarEntidad(obj);
        }

        public Servicios.Resultado<BE.Destino>  actualizarEntidad(BE.Destino obj)
        {
           return destino.actualizarEntidad(obj);
        }
    }
}
