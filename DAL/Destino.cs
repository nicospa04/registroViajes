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


        public List<BE.Destino> leerDestino()
        {
            List<BE.Destino> list = new List<BE.Destino>();

            DAL.BaseDeDatos bd;

            string sqlQuery = "select * FROM Destino";

            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");

                SqlCommand command = new SqlCommand(sqlQuery, db.Connection);


                SqlDataReader lector = command.ExecuteReader();

                while (lector.Read())
                {
                    BE.Destino objeto = new BE.Destino(lector.GetInt32(0), lector.GetString(1), lector.GetString(2);

                    list.Add(objeto);
                }

                lector.Close();

                bool result2 = db.Desconectar();

                if (!result2) throw new Exception("Error al desconectarse de la base de datos");


                return list;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;

            }

        }
        
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
