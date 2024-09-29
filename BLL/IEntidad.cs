using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IEntidad<T>
    {
         List<T> leerEntidades();

        Servicios.Resultado<T> crearEntidad(T obj);

        Servicios.Resultado<T> eliminarEntidad(T obj);

        Servicios.Resultado<T> actualizarEntidad(T obj);


    }
}
