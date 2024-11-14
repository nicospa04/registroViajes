using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TipoTransporte : ICrud<BE.TipoTransporte>
    {
        public Resultado<BE.TipoTransporte> actualizarEntidad(BE.TipoTransporte obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<BE.TipoTransporte> crearEntidad(BE.TipoTransporte obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<BE.TipoTransporte> eliminarEntidad(BE.TipoTransporte obj)
        {
            throw new NotImplementedException();
        }

        public List<BE.TipoTransporte> leerEntidades()
        {
            List<BE.TipoTransporte> list = new List<BE.TipoTransporte>();

            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM tipo_transporte";

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
                            int id_tipo_transporte = !lector.IsDBNull(lector.GetOrdinal("id_tipo_transporte")) ? lector.GetInt32(lector.GetOrdinal("id_tipo_transporte")) : 0;
                            string nombre = !lector.IsDBNull(lector.GetOrdinal("nombre")) ? lector.GetString(lector.GetOrdinal("nombre")) : string.Empty;
                            
                            BE.TipoTransporte objeto = new BE.TipoTransporte(id_tipo_transporte, nombre);

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
