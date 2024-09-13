using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Destino: IEntidad<BE.Destino>
    {
        DAL.Destino destino = new DAL.Destino();


        public List<BE.Destino> leerEntidades()
        {

            DAL.Destino destino = new DAL.Destino();
            return destino.leerDestino();
        }

        public bool crearEntidad(BE.Destino obj)
        {
            return destino.crearDestino();
        }

        public bool eliminarEntidad(BE.Destino obj)
        {
            throw new NotImplementedException();
        }

        public bool actualizarEntidad(BE.Destino obj)
        {
            throw new NotImplementedException();
        }
    }
}
