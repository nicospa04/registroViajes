using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Viaje: ICrud<BE.Viaje>
    {
        BaseDeDatos db { get; }

        public bool actualizarEntidad(BE.Viaje obj)
        {
            throw new NotImplementedException();
        }

        public bool crearEntidad(BE.Viaje obj)
        {
            string query = "USE SistemaViajes;" +
               "INSERT INTO Viaje (id_usuario, id_empresa, id_destino, transporte, cant_adulto, cant_niños, costo, fecha_inicio, fecha_vuelta)" +
               "VALUES" +
               $"({obj.id_usuario},{obj.id_empresa}, {obj.id_destino}, '{obj.transporte}', {obj.cant_adulto}, {obj.cant_niños}, {obj.costo}, '{obj.fecha_inicio.ToString()}','{obj.fecha_vuelta.ToString()}');";

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

        public bool eliminarEntidad(BE.Viaje obj)
        {
            string query = "USE SistemaViajes;" +
                                   $"DELETE FROM Viaje WHERE id_viaje = {obj.id_viaje}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al eliminar destino");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return false;
            }
        }

        public List<BE.Viaje> leerEntidades()
        {
            List<BE.Viaje> list = new List<BE.Viaje>();

            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Viaje";

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
                            int id_viaje = !lector.IsDBNull(0) ? lector.GetInt32(0) : 0;
                            int id_usuario= !lector.IsDBNull(1) ? lector.GetInt32(1) : 0;
                            int id_empresa = !lector.IsDBNull(2) ? lector.GetInt32(2) : 0;
                            int id_destino = !lector.IsDBNull(3) ? lector.GetInt32(3) : 0;
                            string transporte = !lector.IsDBNull(4) ? lector.GetString(4) : string.Empty;
                            int cant_adulto = !lector.IsDBNull(5) ? lector.GetInt32(5) : 0;
                            int cant_niño = !lector.IsDBNull(6) ? lector.GetInt32(6) : 0;
                            float costo = !lector.IsDBNull(7) ? lector.GetFloat(7) : 0;
                            DateTime fecha_inicio = !lector.IsDBNull(8) ? lector.GetDateTime(8) : DateTime.Now;
                            DateTime fecha_vuelta = !lector.IsDBNull(9) ? lector.GetDateTime(9) : DateTime.Now;

                            BE.Viaje viaje = new BE.Viaje(id_viaje, id_usuario, id_empresa, id_destino, transporte, cant_adulto, cant_niño, costo, fecha_inicio, fecha_vuelta);

                            list.Add(viaje);
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
    }
}
