using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Asiento : ICrud<BE.Asiento>
    {
        public Resultado<BE.Asiento> actualizarEntidad(BE.Asiento obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<BE.Asiento> crearEntidad(BE.Asiento obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<BE.Asiento> eliminarEntidad(BE.Asiento obj)
        {
            throw new NotImplementedException();
        }

        public List<BE.Asiento> leerEntidades()
        {
            List<BE.Asiento> list = new List<BE.Asiento>();

            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Asiento";

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
                            int id_asiento = !lector.IsDBNull(0) ? lector.GetInt32(0) : 0;
                            int id_fecha = !lector.IsDBNull(lector.GetOrdinal("id_fecha ")) ? lector.GetInt32(lector.GetOrdinal("id_fecha ")) : 0;
                            int num_asiento = !lector.IsDBNull(lector.GetOrdinal("num_asiento ")) ? lector.GetInt32(lector.GetOrdinal("num_asiento ")) : 0;
                            var resultado_booleano  = !lector.IsDBNull(lector.GetOrdinal("esta_disponible")) ? lector.GetInt32(lector.GetOrdinal("esta_disponible")) : 0;
                            bool resultado = resultado_booleano == 1 ? true : false;
                            BE.Asiento objeto = new BE.Asiento(id_asiento, id_fecha, num_asiento, resultado);

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
    }
}
