﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Usuario: IEntidad<BE.Usuario>
    {

        DAL.Usuario dal = new DAL.Usuario();    


        public List<BE.Usuario> leerEntidades()
        {

            return dal.leerEntidades();

        }

        public bool crearEntidad(BE.Usuario obj)
        {
            return dal.crearEntidad(obj);
        }

        public bool eliminarEntidad(BE.Usuario obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public bool actualizarEntidad(BE.Usuario obj)
        {
            return dal.actualizarEntidad(obj);
        }
    }
}
