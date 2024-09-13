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

            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Destino";

            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");

                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            // Manejar posibles valores nulos
                            int id = !lector.IsDBNull(0) ? lector.GetInt32(0) : 0;
                            string nombre = !lector.IsDBNull(1) ? lector.GetString(1) : string.Empty;
                            string descripcion = !lector.IsDBNull(2) ? lector.GetString(2) : string.Empty;

                            BE.Destino objeto = new BE.Destino(id, nombre, descripcion);

                            list.Add(objeto);
                        }
                    }
                }

                bool result2 = db.Desconectar();
                if (!result2) throw new Exception("Error al desconectarse de la base de datos");

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Asegurarse de que la conexión se cierre en caso de error
                db.Desconectar();
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

            try
            {
                db.ejecutarQuery(query);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

       public bool crearDestino(BE.Destino destino)
        {
            string query = "USE SistemaViajes;" +
                "INSERT INTO Destino (nombre, descripcion)" +
                "VALUES" +
                $"('{destino.nombre}','{destino.descripcion}');";

            try
            {
                bool result = db.ejecutarQuery(query);
                if (!result) throw new Exception("Error al crear destino");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return false;
            }
        }

        void eliminarDestino(int id)
        {
            string query = "USE SistemaViajes; GO" +
                $"DELETE FROM Destino WHERE id_destino = {id}";

            try
            {
                db.ejecutarQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
