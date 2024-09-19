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

        bool crearEntidad(T obj);

        bool eliminarEntidad(T obj);

        bool actualizarEntidad(T obj);

    }
}
