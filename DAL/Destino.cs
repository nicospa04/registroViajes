using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class Destino
    {
        BaseDeDatos db { get; }


        //List<BE.Destino> leerDestino()
        //{
        //    List<BE.Destino> destinos = new List<BE.Destino>();


        //    string query = "USE SistemaViajes; SELECT * FROM Destino";


        //    using (SqlConnection connection = db.ObtenerConexion())
        //    {
        //        SqlCommand command = new SqlCommand(query, connection);
        //        connection.Open();

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                BE.Destino destino = new BE.Destino
        //                {
        //                    id_destino = reader.GetInt32(0),
        //                    nombre = reader.GetString(1),
        //                    descripcion = reader.GetString(2)
        //                };
        //                destinos.Add(destino);
        //            }
        //        }
        //    }



        //    return new List<BE.Destino>();
        //}

        void actualizarDestino(BE.Destino destino)
        {
            string query = "USE SistemaViajes;" +
                "GO" +
                "UPDATE Destino" +
                $"SET nombre = '{destino.nombre}', descripcion = '{destino.descripcion}'" +
                $"WHERE id_destino = {destino.id_destino}";

            db.ejecutarQuery(query);
        }

       public void crearDestino(BE.Destino destino)
        {
            string query = "USE SistemaViajes;GO" +
                "INSERT INTO Destino (nombre, descripcion)" +
                "VALUES" +
                $"('{destino.nombre}', '{destino.descripcion}');";

            db.ejecutarQuery(query);
        }

        void eliminarDestino(int id)
        {
            string query = "USE SistemaViajes; GO" +
                $"DELETE FROM Destino WHERE id_destino = {id}";

            db.ejecutarQuery(query);


        }
    }
}
