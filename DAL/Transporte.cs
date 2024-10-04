using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Transporte : ICrud<BE.Transporte>
    {
        public Resultado<BE.Transporte> actualizarEntidad(BE.Transporte obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<BE.Transporte> crearEntidad(BE.Transporte obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<BE.Transporte> eliminarEntidad(BE.Transporte obj)
        {
            throw new NotImplementedException();
        }

        public List<BE.Transporte> leerEntidades()
        {
            List<BE.Transporte> list = new List<BE.Transporte>();

            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Transporte";

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
                            string nombre = !lector.IsDBNull(lector.GetOrdinal("nombre")) ? lector.GetString(lector.GetOrdinal("nombre")) : string.Empty;
                            decimal porcentaje_extra = !lector.IsDBNull(lector.GetOrdinal("porcentaje_extra")) ? lector.GetDecimal(lector.GetOrdinal("porcentaje_extra")): 1;
                            BE.Transporte objeto = new BE.Transporte(id, nombre, porcentaje_extra);

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
