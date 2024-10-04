using BE;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLTransporte : IEntidad<BE.Transporte>
    {


        DAL.Transporte dal;


        public BLLTransporte()
        {
            dal = new DAL.Transporte();
        }


        public Resultado<Transporte> actualizarEntidad(Transporte obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<Transporte> crearEntidad(Transporte obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<Transporte> eliminarEntidad(Transporte obj)
        {
            throw new NotImplementedException();
        }

        public List<Transporte> leerEntidades()
        {
            return dal.leerEntidades();
        }
    }
}
