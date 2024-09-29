using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class Destino : ICrud<BE.Destino>
    {
        public BaseDeDatos db { get; }


        public List<BE.Destino> leerEntidades()
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
                            float precio_base = !lector.IsDBNull(3) ? lector.GetFloat(3) : 1;
                            BE.Destino objeto = new BE.Destino(id, nombre, descripcion, precio_base);

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

        public Servicios.Resultado<BE.Destino> crearEntidad(BE.Destino obj)
        {

            Servicios.Resultado < BE.Destino > resultado = new Servicios.Resultado<BE.Destino>();


            string query = "USE SistemaViajes;" +
                 "INSERT INTO Destino (nombre, descripcion)" +
                 "VALUES" +
                 $"('{obj.nombre}','{obj.descripcion}');";

            try
            {
                bool result = db.ejecutarQuery(query);
                if (!result) throw new Exception("Error al crear destino");
               
                resultado.resultado = true;
                resultado.mensaje = "Destino creado con exito";
                return resultado;



            }
            catch (Exception ex)
            {
                db.Desconectar();
               

                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                return resultado;

            }
        }

        public Servicios.Resultado<BE.Destino> eliminarEntidad(BE.Destino obj)
        {

            Servicios.Resultado < BE.Destino > Resultado = new Servicios.Resultado<BE.Destino>();


            string query = "USE SistemaViajes;" +
               $"DELETE FROM Destino WHERE id_destino = {obj.id_destino}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al eliminar destino");
                Resultado.resultado = true;
                Resultado.mensaje = "Destino eliminado con éxito";
                return Resultado;
            }
            catch (Exception ex)
            {   
                db.Desconectar();
             
                Resultado.resultado = false;
                Resultado.mensaje = ex.Message;
                return Resultado;
            
            }

        }

        public Servicios.Resultado<BE.Destino> actualizarEntidad(BE.Destino obj)
        {

            Servicios.Resultado < BE.Destino > Resultado = new Servicios.Resultado<BE.Destino>();


            string query = "USE SistemaViajes;" +
                             
                            "UPDATE Destino" +
            $"SET nombre = '{obj.nombre}', descripcion = '{obj.descripcion}', precio_base = {obj.precio_base}" +
            $"WHERE id_destino = {obj.id_destino}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al actualizar destino");
                
                Resultado.resultado = true;
                Resultado.mensaje = "Destino actualizado con éxito";
                return Resultado;

            }
            catch (Exception ex)
            {
                db.Desconectar();

                Resultado.resultado = false;
                Resultado.mensaje = ex.Message;
                return Resultado;

            }
        }
    }
}
