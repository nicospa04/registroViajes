using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class DALEventos
    {
        BaseDeDatos db { get; }


        //public DataTable RegistrarEvento(Evento evento)
        //{
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand($"INSERT INTO Evento VALUES @NombreUsuario, @Fecha, @Modulo, @Criticidad");
        //    SqlDataAdapter adap = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    adap.Fill(dt);
        //    con.Close();
        //    return dt;

        //}
    }
}
