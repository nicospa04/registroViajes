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
        public List<string> cargarCBPerfil()
        {
            db.Connection.Open();
            List<string> Lista = new List<string>();
            DataSet dataSet = new DataSet();
            string query= "USE SistemaViajes; Select * from PermisosComp where isperfil =1";
            try
            {
                SqlDataAdapter Adapter = new SqlDataAdapter(query, db.Connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(Adapter);
                Adapter.Fill(dataSet);
                foreach (DataRow L in dataSet.Tables[0].Rows)
                {
                    Lista.Add(L["nombre"].ToString());
                }
                return Lista;
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
        public List<string> cargarCBPermisos()
        {
            db.Connection.Open();
            List<string> Lista = new List<string>();
            DataSet dataSet = new DataSet();
            string query = "USE SistemaViajes; Select * from PermisosComp where isperfil =0";
            try
            {
                SqlDataAdapter Adapter = new SqlDataAdapter(query, db.Connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(Adapter);
                Adapter.Fill(dataSet);
                foreach (DataRow L in dataSet.Tables[0].Rows)
                {
                    Lista.Add(L["nombre"].ToString());
                }
                return Lista;
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
    }
}
