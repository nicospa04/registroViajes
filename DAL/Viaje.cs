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

        public Viaje()
        {
            db = new BaseDeDatos();
        }

        public Servicios.Resultado<BE.Viaje> actualizarEntidad(BE.Viaje obj)
        {
            throw new NotImplementedException();
        }

        public Servicios.Resultado<BE.Viaje> crearEntidad(BE.Viaje obj)
        {

            Servicios.Resultado < BE.Viaje > resultado = new Servicios.Resultado<BE.Viaje>();

            string query = "USE SistemaViajes;" +
               "INSERT INTO Viaje (id_usuario, id_empresa, id_fecha, transporte, costo)" +
               "VALUES" +
               $"({obj.id_usuario},{obj.id_empresa}, {obj.id_fecha}, '{obj.transporte}', {obj.costo});";

            try
            {
                bool result = db.ejecutarQuery(query);
                if (!result) throw new Exception("Error al crear destino");
                resultado.resultado = true;
                resultado.mensaje = "Viaje creado con éxito";
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

        public Servicios.Resultado<BE.Viaje> eliminarEntidad(BE.Viaje obj)
        {

            Servicios.Resultado < BE.Viaje > Resultado = new Servicios.Resultado<BE.Viaje>();

            string query = "USE SistemaViajes;" +
                                   $"DELETE FROM Viaje WHERE id_viaje = {obj.id_viaje}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al eliminar destino");
                
                Resultado.resultado = true;
                Resultado.mensaje = "Viaje eliminado con éxito";
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
                            int id_viaje = !lector.IsDBNull(lector.GetOrdinal("id_viaje")) ? lector.GetInt32(lector.GetOrdinal("id_viaje")) : 0;
                            int id_usuario= !lector.IsDBNull(lector.GetOrdinal("id_usuario")) ? lector.GetInt32(lector.GetOrdinal("id_usuario")) : 0;
                            int id_empresa = !lector.IsDBNull(lector.GetOrdinal("id_empresa")) ? lector.GetInt32(lector.GetOrdinal("id_empresa")) : 0;
                            int id_fecha = !lector.IsDBNull(lector.GetOrdinal("id_fecha")) ? lector.GetInt32(lector.GetOrdinal("id_fecha")) : 0;
                            string transporte = !lector.IsDBNull(lector.GetOrdinal("transporte")) ? lector.GetString(lector.GetOrdinal("transporte")) : string.Empty;
                            decimal costo = !lector.IsDBNull(lector.GetOrdinal("costo")) ? lector.GetDecimal(lector.GetOrdinal("costo")) : 0;
                            DateTime fecha_inicio = !lector.IsDBNull(lector.GetOrdinal("fecha_inicio")) ? lector.GetDateTime(lector.GetOrdinal("fecha_inicio")) : DateTime.Now;
                            DateTime fecha_vuelta = !lector.IsDBNull(lector.GetOrdinal("fecha_vuelta")) ? lector.GetDateTime(lector.GetOrdinal("fecha_vuelta")) : DateTime.Now;
                            BE.Viaje viaje = new BE.Viaje(id_viaje, id_usuario, id_empresa, id_fecha, transporte, costo, fecha_inicio, fecha_vuelta);
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

        public List<BE.Viaje> ObtenerViajesPorUsuarioId(int id_usuario)
        {
            List<BE.Viaje> listaViajes = new List<BE.Viaje>();
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();
            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos"); 
                string query = "USE SistemaViajes; SELECT * FROM Viaje WHERE id_usuario = @id_usuario";
                SqlCommand command = new SqlCommand(query, db.Connection);
                command.Parameters.AddWithValue("@id_usuario", id_usuario);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int  id_viaje = Convert.ToInt32(reader["id_viaje"]);
                    int id_empresa = Convert.ToInt32(reader["id_empresa"]);
                    int id_fecha= Convert.ToInt32(reader["id_fecha"]);
                    string transporte = reader["transporte"].ToString();
                    decimal costo = Convert.ToDecimal(reader["costo"]);
                    DateTime fecha_inicio = Convert.ToDateTime(reader["fecha_inicio"]);
                    DateTime fecha_vuelta = Convert.ToDateTime(reader["fecha_vuelta"]);
                    BE.Viaje viaje = new BE.Viaje(id_viaje, id_usuario, id_empresa, id_fecha, transporte, costo, fecha_inicio, fecha_vuelta);
                    listaViajes.Add(viaje);
                }
                return listaViajes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los viajes del usuario: " + ex.Message);
            }
        }

        public decimal calcularCostoViaje(BE.Fecha fecha, BE.Empresa empresa , BE.Transporte transporte, BE.Destino destino ,decimal descuento_por_paquete = 0) 
        {
            decimal destinoPrecioBaseToDecimal = Convert.ToDecimal(destino.precio_base);
            decimal empresaPorcentajeExtraToDecimal = Convert.ToDecimal(empresa.porcentaje_extra);


            decimal transporteCosto =  (destinoPrecioBaseToDecimal * transporte.porcentaje_extra) / 100;

            decimal empresaCosto = (destinoPrecioBaseToDecimal * empresaPorcentajeExtraToDecimal) / 100;


            decimal costoViaje = destinoPrecioBaseToDecimal + transporteCosto + empresaCosto - descuento_por_paquete;

            return costoViaje;
        }
    }
}
