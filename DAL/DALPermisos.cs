using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPermisos
    {
        BaseDeDatos db { get; }
        public DALPermisos()
        {
            db = new BaseDeDatos();
        }

        public List<BEPermisos> listapermisos()
        {
            db.Connection.Open();
            List<BEPermisos> lista = new List<BEPermisos>();
            string query = "USE SistemaViajes; SELECT * FROM PermisoPermiso";
            try
            {
                SqlCommand command = new SqlCommand(query, db.Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BEPermisos perfil = new BEPermisos(
                        Convert.ToInt32(reader["id_permisopadre"]),
                        Convert.ToInt32(reader["id_permisohijo"])
                    );
                    lista.Add(perfil);
                }

                reader.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            finally
            {
                db.Desconectar();
            }
        }

        public Servicios.Resultado<BEPermisos> aggPermisos(BEPermisos obj)
        {
            db.Connection.Open();
            Servicios.Resultado<BEPermisos> resultado = new Servicios.Resultado<BEPermisos>();
            var trans = db.Connection.BeginTransaction();
            string queryToCreateUser = "USE SistemaViajes;" +
                "INSERT INTO PermisoPermiso (id_permisopadre, id_permisohijo)" +
                "VALUES" +
                $"('{obj.id_permisopadre}', '{obj.id_permisohijo}');";
            try
            {
                SqlCommand cmd2 = new SqlCommand(queryToCreateUser, db.Connection);
                cmd2.Transaction = trans;
                SqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Close();
                trans.Commit();
                resultado.entidad = null;
                resultado.resultado = true;
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
                trans.Rollback();
            }
            finally
            {
                db.Connection.Close();
            }
            return resultado;
        }
    }
}
