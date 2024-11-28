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
