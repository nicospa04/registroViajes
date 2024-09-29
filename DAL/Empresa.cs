using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Empresa: ICrud<BE.Empresa>
    {
        BaseDeDatos db { get; }

        public List<BE.Empresa> leerEntidades()
        {
            List<BE.Empresa> list = new List<BE.Empresa>();

            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Empresa";

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
                            float porcentaje_extra = !lector.IsDBNull(3) ? lector.GetFloat(3) : 1;
                            BE.Empresa objeto = new BE.Empresa(id, nombre, descripcion, porcentaje_extra);

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

        public Servicios.Resultado<BE.Empresa> crearEntidad(BE.Empresa obj)
        {

            Servicios.Resultado < BE.Empresa > resultado = new Servicios.Resultado<BE.Empresa>();



            string query = "USE SistemaViajes;" +
                 "INSERT INTO Empresa (nombre, descripcion, porcentaje_extra)" +
                 "VALUES" +
                 $"('{obj.nombre}','{obj.descripcion}', {obj.porcentaje_extra});";

            try
            {
                bool result = db.ejecutarQuery(query);
                if (!result) throw new Exception("Error al crear destino");
                resultado.resultado = true;
                resultado.mensaje = "Empresa creada con exito";
                return resultado;
            }
            catch (Exception ex)
            {   db.Desconectar();
             
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                return resultado;


            }
        }

        public Servicios.Resultado<BE.Empresa> eliminarEntidad(BE.Empresa obj)
        {


            Servicios.Resultado < BE.Empresa > Resultado = new Servicios.Resultado<BE.Empresa>();


            string query = "USE SistemaViajes; GO" +
              $"DELETE FROM Empresa WHERE id_empresa = {obj.id_empresa}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al eliminar destino");

                Resultado.resultado = true;
                Resultado.mensaje = "Empresa eliminada con éxito";
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

        public Servicios.Resultado<BE.Empresa> actualizarEntidad(BE.Empresa obj)
        {

            Servicios.Resultado < BE.Empresa > Resultado = new Servicios.Resultado<BE.Empresa>();


            string query = "USE SistemaViajes;" +
                            "GO" +
                            "UPDATE Empresa" +
            $"SET nombre = '{obj.nombre}', descripcion = '{obj.descripcion}', porcentaje_extra = {obj.porcentaje_extra}" +
            $"WHERE id_empresa = {obj.id_empresa}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al actualizar destino");
                
                Resultado.resultado = true;
                Resultado.mensaje = "Empresa actualizada con éxito";
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
