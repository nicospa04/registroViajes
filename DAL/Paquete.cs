using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Paquete: ICrud<BE.Paquete>
    {
        BaseDeDatos db { get; }

        public Servicios.Resultado<BE.Paquete> actualizarEntidad(BE.Paquete obj)
        {

            Servicios.Resultado < BE.Paquete > Resultado = new Servicios.Resultado<BE.Paquete>();


            string query = "USE SistemaViajes;" +

                                       "UPDATE Paquete" +
                       $"SET nombre = '{obj.nombre}', descripcion = '{obj.descripcion}', precio_base = {obj.precio_base}" +
                       $"WHERE id_paquete = {obj.id_paquete}";
            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al actualizar destino");
                Resultado.resultado = true;
                Resultado.mensaje = "Paquete actualizado con éxito";
                return Resultado;
            }
            catch (Exception ex)
            {
                Resultado.resultado = false;
                Resultado.mensaje = ex.Message; 
                
                db.Desconectar();
                return Resultado;
            }
        }

        public Servicios.Resultado<BE.Paquete> crearEntidad(BE.Paquete obj)
        {

            Servicios.Resultado < BE.Paquete > resultado = new Servicios.Resultado<BE.Paquete>();


            string query = "USE SistemaViajes;" +
                "INSERT INTO Paquete (nombre, descripcion)" +
                "VALUES" +
                $"('{obj.nombre}','{obj.descripcion}');";

            try
            {
                bool result = db.ejecutarQuery(query);
                if (!result) throw new Exception("Error al crear destino");
                resultado.resultado = true;
                resultado.mensaje = "Paquete creado con éxito";
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                
                db.Desconectar();
                return resultado;
            }
        }

        public Servicios.Resultado<BE.Paquete> eliminarEntidad(BE.Paquete obj)
        {

            Servicios.Resultado < BE.Paquete > Resultado = new Servicios.Resultado<BE.Paquete>();


            string query = "USE SistemaViajes;" +
                         $"DELETE FROM Paquete WHERE id_paquete = {obj.id_paquete}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al eliminar destino");
                Resultado.resultado = true;
                Resultado.mensaje = "Paquete eliminado con éxito";
                return Resultado;
            }
            catch (Exception ex)
            {
                Resultado.resultado = false;
                Resultado.mensaje = ex.Message;
                
                db.Desconectar();
                return Resultado;
            }
        }

        public List<BE.Paquete> leerEntidades()
        {
            List<BE.Paquete> list = new List<BE.Paquete>();

            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Paquete";

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
                            int id_paquete = !lector.IsDBNull(0) ? lector.GetInt32(0) : 0;
                            int id_destino = !lector.IsDBNull(1) ? lector.GetInt32(1) : 0;
                            float precio_base = !lector.IsDBNull(3) ? lector.GetFloat(3) : 0.0f;
                            int cupo_personas = !lector.IsDBNull(3) ? lector.GetInt32(3) : 0;
                            string nombre = !lector.IsDBNull(4) ? lector.GetString(4) : string.Empty;
                            string descripcion = !lector.IsDBNull(5) ? lector.GetString(5) : string.Empty;
                            DateTime fecha_inicio = !lector.IsDBNull(6) ? lector.GetDateTime(6) : DateTime.Now;
                            DateTime fecha_vuelta = !lector.IsDBNull(7) ? lector.GetDateTime(6) : DateTime.Now;


                            BE.Paquete paquete = new BE.Paquete(id_paquete, id_destino, precio_base, cupo_personas, nombre, descripcion, fecha_inicio, fecha_vuelta);


                            list.Add(paquete);
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
