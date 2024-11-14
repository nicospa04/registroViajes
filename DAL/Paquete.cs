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
                            int id_paquete = !lector.IsDBNull(lector.GetOrdinal("id_paquete")) ? lector.GetInt32(lector.GetOrdinal("id_paquete")) : 0;
                            string nombre = !lector.IsDBNull(lector.GetOrdinal("nombre")) ? lector.GetString(lector.GetOrdinal("nombre")) : string.Empty;
                            decimal precio_base = !lector.IsDBNull(lector.GetOrdinal("precio")) ? lector.GetDecimal(lector.GetOrdinal("precio")) : 0;
                            string descripcion = !lector.IsDBNull(lector.GetOrdinal("descripcion")) ? lector.GetString(lector.GetOrdinal("descripcion")) : string.Empty;
                            int cant_max_asientos = !lector.IsDBNull(lector.GetOrdinal("cant_max_asientos ")) ? lector.GetInt32(lector.GetOrdinal("cant_max_asientos ")) : 0;
                            int id_fecha = !lector.IsDBNull(lector.GetOrdinal("id_fecha")) ? lector.GetInt32(lector.GetOrdinal("id_fecha")) : 0;
                            
                            BE.Paquete paquete = new BE.Paquete(id_paquete, precio_base, nombre, descripcion, cant_max_asientos, id_fecha);


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
