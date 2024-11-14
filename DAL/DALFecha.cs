using BE;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALFecha : ICrud<BE.Fecha>
    {
        BaseDeDatos db { get; }

        public DALFecha()
        {
            db = new BaseDeDatos();
        }

        public Resultado<Fecha> actualizarEntidad(Fecha obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<Fecha> crearEntidad(Fecha obj)
        {
            Servicios.Resultado<BE.Fecha> resultado = new Servicios.Resultado<BE.Fecha>();


            string query = "USE SistemaViajes;" +
                 "INSERT INTO Fecha (id_empresa, id_lugar_origen, id_lugar_destino, id_transporte, fecha_ida, fecha_vuelta, categoria_tipo)" +
                 "VALUES" +
                 $"({obj.id_empresa}, {obj.id_lugar_origen}, {obj.id_lugar_destino}, {obj.id_transporte}, '{obj.fecha_ida.ToString("yyyy-MM-dd")}', '{obj.fecha_vuelta.ToString("yyyy-MM-dd")}', '{obj.categoria_tipo}');";

            try
            {
                bool result = db.ejecutarQuery(query);
                if (!result) throw new Exception("Error al crear destino");

                resultado.entidad = obj;
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

        public Resultado<Fecha> eliminarEntidad(Fecha obj)
        {
            Servicios.Resultado<BE.Fecha> Resultado = new Servicios.Resultado<BE.Fecha>();


            string query = "USE SistemaViajes;" +
               $"DELETE FROM Fecha WHERE id_fecha = {obj.id_fecha}";

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

        public List<Fecha> leerEntidades()
        {
            List<BE.Fecha> list = new List<BE.Fecha>();

            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Fecha;";

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
                            int id_fecha = !lector.IsDBNull(0) ? lector.GetInt32(0) : 0;
                            int id_empresa = !lector.IsDBNull(lector.GetOrdinal("id_empresa")) ? lector.GetInt32(lector.GetOrdinal("id_empresa")) : 0;
                            int id_lugar_origen = !lector.IsDBNull(lector.GetOrdinal("id_lugar_origen")) ? lector.GetInt32(lector.GetOrdinal("id_lugar_origen")) : 0;
                            int id_lugar_destino = !lector.IsDBNull(lector.GetOrdinal("id_lugar_destino")) ? lector.GetInt32(lector.GetOrdinal("id_lugar_destino")) : 0;
                            DateTime fecha_ida = !lector.IsDBNull(lector.GetOrdinal("fecha_ida")) ? lector.GetDateTime(lector.GetOrdinal("fecha_ida")) : DateTime.Now;
                            DateTime fecha_vuelta = !lector.IsDBNull(lector.GetOrdinal("fecha_vuelta")) ? lector.GetDateTime(lector.GetOrdinal("fecha_vuelta")) : DateTime.Now;
                            string categoria_tipo = !lector.IsDBNull(lector.GetOrdinal("categoria_tipo")) ? lector.GetString(lector.GetOrdinal("categoria_tipo")) : string.Empty;
                            int id_transporte = !lector.IsDBNull(lector.GetOrdinal("id_transporte")) ? lector.GetInt32(lector.GetOrdinal("id_transporte")) : 0; 
                            BE.Fecha objeto = new BE.Fecha(id_fecha, id_empresa, id_lugar_origen, id_lugar_destino, id_transporte ,fecha_ida, fecha_vuelta, categoria_tipo);
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
