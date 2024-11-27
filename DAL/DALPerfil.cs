using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPerfil
    {
        BaseDeDatos db { get; }
        public DALPerfil()
        {
            db = new BaseDeDatos();
        }
        public Servicios.Resultado<BEPerfil> aggPerfil(BEPerfil obj)
        {
            db.Connection.Open();
            Servicios.Resultado<BEPerfil> resultado = new Servicios.Resultado<BEPerfil>();
            var trans = db.Connection.BeginTransaction();
            string queryToCreateUser = "USE SistemaViajes;" +
                "INSERT INTO PermisosComp (nombre, nombreformulario, isperfil)" +
                "VALUES" +
                $"('{obj.nombre}', '{obj.permiso}', '{obj.is_perfil}');";
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
        public List<BEPerfil> cargarCBPerfil()
        {
            db.Connection.Open();
            List<BEPerfil> listaNombres = new List<BEPerfil>(); // Mantener la lista de objetos BEPerfil
            string query = "USE SistemaViajes; SELECT * FROM PermisosComp WHERE isperfil = 1";

            try
            {
                SqlCommand command = new SqlCommand(query, db.Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Crear objeto BEPerfil usando el constructor existente
                    BEPerfil perfil = new BEPerfil(
                        reader["nombre"]?.ToString(),
                        reader["nombreformulario"]?.ToString(),
                        reader["isperfil"] != DBNull.Value && Convert.ToBoolean(reader["isperfil"])
                    );

                    // Agregar el objeto BEPerfil a la lista
                    listaNombres.Add(perfil);
                }

                reader.Close();
                return listaNombres; // Retorna la lista de objetos BEPerfil
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar perfiles desde la base de datos.", ex);
            }
            finally
            {
                db.Desconectar();
            }
        }
        public List<BEPerfil> cargarCBPermisos()
        {
            db.Connection.Open();
            List<BEPerfil> listaNombres = new List<BEPerfil>(); // Mantener la lista de objetos BEPerfil
            string query = "USE SistemaViajes; SELECT * FROM PermisosComp WHERE isperfil = 0";

            try
            {
                SqlCommand command = new SqlCommand(query, db.Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Crear objeto BEPerfil usando el constructor existente
                    BEPerfil perfil = new BEPerfil(
                        reader["nombre"]?.ToString(),
                        reader["nombreformulario"]?.ToString(),
                        reader["isperfil"] != DBNull.Value && Convert.ToBoolean(reader["isperfil"])
                    );

                    // Agregar el objeto BEPerfil a la lista
                    listaNombres.Add(perfil);
                }

                reader.Close();
                return listaNombres; // Retorna la lista de objetos BEPerfil
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar perfiles desde la base de datos.", ex);
            }
            finally
            {
                db.Desconectar();
            }
        }
    }
}
