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

        bool crearEntidad(T obj);

        bool eliminarEntidad(T obj);

        bool actualizarEntidad(T obj);


    }
}
