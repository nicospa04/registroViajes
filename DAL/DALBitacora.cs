using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBitacora : ICrud<BEBitacora>
    {
        BaseDeDatos db { get; }
        public DALBitacora()
        {
            db = new BaseDeDatos();
        }
        public Servicios.Resultado<BEBitacora> actualizarEntidad(BEBitacora obj)
        {
            throw new NotImplementedException();
        }

        public Servicios.Resultado<BEBitacora> crearEntidad(BEBitacora obj)
        {
            throw new NotImplementedException();
        }

        public Servicios.Resultado<BEBitacora> eliminarEntidad(BEBitacora obj)
        {
            throw new NotImplementedException();
        }

        public List<BEBitacora> leerEntidades()
        {
            List<BEBitacora> list = new List<BEBitacora>();
            // Crear e inicializar la instancia de la base de datos
            BaseDeDatos db = new BaseDeDatos();
            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Bitacora";
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
                            int id_bitacora = !lector.IsDBNull(lector.GetOrdinal("id_bitacora")) ? lector.GetInt32(lector.GetOrdinal("id_bitacora")) : 0;
                            int id_usuario = !lector.IsDBNull(lector.GetOrdinal("id_usuario")) ? lector.GetInt32(lector.GetOrdinal("id_usuario")) : 0;
                            string operacion = !lector.IsDBNull(lector.GetOrdinal("operacion")) ? lector.GetString(lector.GetOrdinal("operacion")) : string.Empty;
                            DateTime fecha = !lector.IsDBNull(lector.GetOrdinal("fecha")) ? lector.GetDateTime(lector.GetOrdinal("fecha")) : DateTime.Now;
                            string actor = !lector.IsDBNull(lector.GetOrdinal("actor")) ? lector.GetString(lector.GetOrdinal("actor")) : string.Empty;
                            int criticidad = !lector.IsDBNull(lector.GetOrdinal("criticidad")) ? lector.GetInt32(lector.GetOrdinal("criticidad")) : 0;
                            BEBitacora bitacora = new BEBitacora(id_bitacora, id_usuario, operacion, fecha, actor, criticidad);
                            list.Add(bitacora);
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
