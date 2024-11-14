using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLViaje : IEntidad<BE.Viaje>
    {

        DAL.Viaje dal = new DAL.Viaje();


        public List<BE.Viaje> leerEntidades()
        {
            return dal.leerEntidades();
        }

        public Servicios.Resultado<BE.Viaje> crearEntidad(BE.Viaje obj)
        {
            return dal.crearEntidad(obj);
        }

        public Servicios.Resultado<BE.Viaje> eliminarEntidad(BE.Viaje obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public Servicios.Resultado<BE.Viaje> actualizarEntidad(BE.Viaje obj)
        {
            return dal.actualizarEntidad(obj);
        }


        public List<BE.Viaje> ObtenerViajesPorUsuarioId(int id_usuario)
        {
            return dal.ObtenerViajesPorUsuarioId(id_usuario);
        }

        public decimal calcularCostoViaje(BE.Fecha fecha, BE.Empresa empresa, BE.Transporte transporte, BE.Destino destino, decimal descuento_por_paquete = 0)
        {
            return dal.calcularCostoViaje(fecha, empresa, transporte, destino, descuento_por_paquete);
        }


    }
}