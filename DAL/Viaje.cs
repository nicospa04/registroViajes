using System;
using System.Collections.Generic;
using System.Data;
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

            string query = "USE SistemaViajes; " +
               "INSERT INTO Viaje (id_usuario, id_empresa, id_fecha, transporte, costo, num_asiento) " +
               "VALUES (@id_usuario, @id_empresa, @id_fecha, @transporte, @costo, @num_asiento);";


            try
            {

                db.Connection.Open();


                using (SqlCommand command = new SqlCommand(query, db.Connection))
                {
                    command.Parameters.AddWithValue("@id_usuario", obj.id_usuario);
                    command.Parameters.AddWithValue("@id_empresa", obj.id_empresa);
                    command.Parameters.AddWithValue("@id_fecha", obj.id_fecha);
                    command.Parameters.AddWithValue("@transporte", obj.transporte);
                    command.Parameters.AddWithValue("@costo", obj.costo);
                    command.Parameters.AddWithValue("@num_asiento", obj.num_asiento);

                    bool result = command.ExecuteNonQuery() > 0;
                    resultado.resultado = result;
                    resultado.mensaje = result ? "Viaje creado con éxito" : "No se pudo crear el viaje";
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                db.Desconectar();
                return resultado;
            }
            finally
            {
                db.Connection.Close();
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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id_usuario = Convert.ToInt32(reader["id_usuario"]);
                            int id_viaje = Convert.ToInt32(reader["id_viaje"]);
                            int id_empresa = Convert.ToInt32(reader["id_empresa"]);
                            int id_fecha = Convert.ToInt32(reader["id_fecha"]);
                            string transporte = reader["transporte"].ToString();
                            decimal costo = Convert.ToDecimal(reader["costo"]);
                            int num_asiento = Convert.ToInt32(reader["num_asiento"]);
                            BE.Viaje viaje = new BE.Viaje(id_viaje, id_usuario, id_empresa, id_fecha, transporte, costo, num_asiento);
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

            string connectionString = "Server=DESKTOP-Q714KGU\\SQLEXPRESS;Database=SistemaViajes;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ObtenerViajesPorUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@id_usuario", id_usuario));

                    try
                    {
                        // Abrir la conexión
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Leer los resultados del procedimiento almacenado
                        while (reader.Read())
                        {
                            int id_viaje = Convert.ToInt32(reader["id_viaje"]);
                            int id_empresa = Convert.ToInt32(reader["id_empresa"]);
                            int id_fecha = Convert.ToInt32(reader["id_fecha"]);
                            string transporte = reader["transporte"].ToString();
                            decimal costo = Convert.ToDecimal(reader["costo"]);
                            int num_asiento = Convert.ToInt32(reader["num_asiento"]);

                            // Crear un objeto Viaje con los datos obtenidos
                            BE.Viaje viaje = new BE.Viaje(id_viaje, id_usuario, id_empresa, id_fecha, transporte, costo, num_asiento);

                            // Agregar el viaje a la lista
                            listaViajes.Add(viaje);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar los errores que puedan ocurrir
                        throw new Exception("Error al obtener los viajes del usuario: " + ex.Message);
                    }
                }
            }

            // Retornar la lista de viajes
            return listaViajes;
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
