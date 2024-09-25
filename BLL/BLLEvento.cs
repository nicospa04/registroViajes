using DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLEvento
    {
        BaseDeDatos db { get; }
        DALEventos eventos = new DALEventos();

        //public void RegistrarEvento(eventos evento)
        //{
        //    data.EjecutarComando("RegistrarEvento", $"'{evento.NombreUsuario}', '{evento.Fecha}', '{evento.Modulo}','{evento.Evento_}' ,'{evento.Criticidad}'");

        //}
    }
}
