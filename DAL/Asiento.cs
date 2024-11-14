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
        DAL.BaseDeDatos db = new DAL.BaseDeDatos();

        public Resultado<BE.Asiento> actualizarEntidad(BE.Asiento obj)
        {
            throw new NotImplementedException();
        }

        public Resultado<BE.Asiento> crearEntidad(BE.Asiento obj)
        {
            int disponible = obj.esta_disponible ? 1 : 0;
            string query = $"USE SistemaViajes; INSERT INTO Asiento (id_fecha, num_asiento, esta_disponible) VALUES (${obj.id_fecha},{obj.num_asiento}, {disponible})";
            try
            {
                using (SqlCommand con = new SqlCommand(query, db.Connection))
                {
                    con.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Resultado<BE.Asiento> eliminarEntidad(BE.Asiento obj)
        {
            throw new NotImplementedException();
        }
        public Resultado<BE.Asiento> marcarAsientoOcupado(BE.Asiento obj)
        {

            int booleano = obj.esta_disponible ? 1 : 0;

            Servicios.Resultado<BE.Asiento> resultado = new Servicios.Resultado<BE.Asiento>();
            string query = $"USE SistemaViajes; UPDATE Asiento SET esta_disponible = {booleano} WHERE id_asiento = ${obj.id_asiento}";
            try
            {
                using(SqlCommand con = new SqlCommand(query, db.Connection))
                {
                    con.ExecuteReader();
                    resultado.resultado = true;
                    resultado.mensaje = "Asiento marcado como ocupado";
                }
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
            }
            return resultado;


        }

        public List<BE.Asiento> leerEntidades()
        {
            List<BE.Asiento> list = new List<BE.Asiento>();

            // Crear e inicializar la instancia de la base de datos

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

 

        public Resultado<List<BE.Asiento>> crearAsientosParaFecha(int id_fecha)
        {
            List<BE.Fecha> fechas = new List<BE.Fecha>();
            List<BE.Asiento> asientosCreados = new List<BE.Asiento>();

            fechas = new DAL.DALFecha().leerEntidades();
            BE.Fecha fecha = fechas.Find(x => x.id_fecha == id_fecha);

            BE.Transporte transporte = new DAL.Transporte().leerEntidades().Find(x => x.id_transporte == fecha.id_transporte);

            string nombre_transporte = transporte.modelo;

            int columnas = 0;
            int filas = 0;

            switch (nombre_transporte) 
            {
                case "Bus":
                    columnas = 4;
                    filas = 8;
                    break;
                case "Avion":
                    columnas = 6;
                    filas = 10;
                    break;
                case "Barco":
                    columnas = 6;
                    filas = 5;
                    break;
            }

            for (int fila = 0; fila < filas; fila++)
            {
                for (int columna = 0; columna < columnas; columna++)
                {
                    int numeroAsiento = fila * columnas + columna + 1;
                    BE.Asiento asiento = new BE.Asiento(0, id_fecha, numeroAsiento, true);
                    asientosCreados.Add(asiento);
                    crearEntidad(asiento);
                }
            }

            return new Resultado<List<BE.Asiento>> { resultado = true, mensaje = "Asientos creados", entidad = asientosCreados };
        }
    }
}
