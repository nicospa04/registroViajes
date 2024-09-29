using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICrud<T>
    {
        List<T> leerEntidades();

        Servicios.Resultado<T> crearEntidad(T obj);

        Servicios.Resultado<T> eliminarEntidad(T obj);

        Servicios.Resultado<T> actualizarEntidad(T obj);

    }
}
