﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Viaje : IEntidad<BE.Viaje>
    {

        DAL.Viaje dal = new DAL.Viaje();


        public List<BE.Viaje> leerEntidades()
        {
            return dal.leerEntidades();
        }

        public bool crearEntidad(BE.Viaje obj)
        {
            return dal.crearEntidad(obj);
        }

        public bool eliminarEntidad(BE.Viaje obj)
        {
            return dal.eliminarEntidad(obj);
        }

        public bool actualizarEntidad(BE.Viaje obj)
        {
            return dal.actualizarEntidad(obj);
        }
    }

}