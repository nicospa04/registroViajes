using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Usuario: ICrud<BE.Usuario>
    {

        BaseDeDatos db { get; }
        PasswordHasher hasher { get; set; }

       public Usuario()
        {
            hasher = new PasswordHasher();
        }

        public List<BE.Usuario> leerEntidades()
        {
            List<BE.Usuario> list = new List<BE.Usuario>();

            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Especificar las columnas necesarias en lugar de usar *
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Usuario";

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
                            int id_usuario = !lector.IsDBNull(0) ? lector.GetInt32(0) : 0;
                            string dni = !lector.IsDBNull(1) ? lector.GetString(1) : "";
                            string nombre = !lector.IsDBNull(2) ? lector.GetString(2) : "";
                            string contraseña = !lector.IsDBNull(3) ? lector.GetString(3) : "";
                            string apellido = !lector.IsDBNull(4) ? lector.GetString(4) : string.Empty;
                            string telefono = !lector.IsDBNull(5) ? lector.GetString(5) : string.Empty;
                            string email = !lector.IsDBNull(6) ? lector.GetString(6) : "";
                            DateTime fecha_nacimiento = !lector.IsDBNull(7) ? lector.GetDateTime(7) : DateTime.Now;
                            string rol = !lector.IsDBNull(8) ? lector.GetString(8) : string.Empty;
                            string salt = !lector.IsDBNull(9) ? lector.GetString(9) : string.Empty;

                            BE.Usuario usuario = new BE.Usuario(id_usuario, dni, nombre, apellido, telefono, email, contraseña, fecha_nacimiento, rol, salt);

                            list.Add(usuario);
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

        public bool crearEntidad(BE.Usuario obj)
        {
            string salt;
            string hashedPassword = hasher.HashPassword(obj.contraseña, out salt);

            string query = "USE SistemaViajes;" +
                "INSERT INTO Usuario (dni, nombre, contraseña, apellido, telefono, email, fecha_nacimiento, rol, salt)" +
                "VALUES" +
                $"('{obj.dni}','{obj.nombre}', '{hashedPassword}', '{obj.apellido}', '{obj.telefono}', '{obj.mail}', '{obj.fechaNacimiento.ToString()}', '{obj.rol}', '{salt}');";

            try
            {
                bool result = db.ejecutarQuery(query);
                if (!result) throw new Exception("Error al crear destino");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return false;
            }
        }

        public bool eliminarEntidad(BE.Usuario obj)
        {
            string query = "USE SistemaViajes;" +
                                   $"DELETE FROM Usuario WHERE id_usuario = {obj.id_usuario}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al eliminar destino");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return false;
            }
        }

        public bool actualizarEntidad(BE.Usuario obj)
        {
            string query = "USE SistemaViajes;" +
                           
                           "UPDATE Usuario" +
           $"SET nombre = '{obj.nombre}', contraseña = '{obj.contraseña}', rol = {obj.rol}" +
           $"WHERE id_usuario = {obj.id_usuario}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al actualizar destino");
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return false;
            }
        }

        public BE.Usuario recuperarUsuario(string email, string contraseña)
        {
            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Consulta SQL solo para obtener el salt y el hash
            string sqlQuery = "USE SistemaViajes; SELECT id_usuario, dni, nombre, apellido, telefono, email, fecha_nacimiento, rol, contraseña, salt FROM Usuario WHERE email = @Email";

            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");

                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    // Uso de parámetros para prevenir inyecciones SQL
                    command.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        if (lector.Read()) // Si encontramos un usuario con el email
                        {
                            // Manejar posibles valores nulos
                            int id_usuario = !lector.IsDBNull(0) ? lector.GetInt32(0) : 0;
                            string dni = !lector.IsDBNull(1) ? lector.GetString(1) : "";
                            string nombre = !lector.IsDBNull(2) ? lector.GetString(2) : "";
                            string hashAlmacenado = !lector.IsDBNull(3) ? lector.GetString(3) : "";
                            string apellido = !lector.IsDBNull(4) ? lector.GetString(4) : string.Empty;
                            string telefono = !lector.IsDBNull(5) ? lector.GetString(5) : string.Empty;
                            string email_db = !lector.IsDBNull(6) ? lector.GetString(6) : "";
                            DateTime fecha_nacimiento = !lector.IsDBNull(7) ? lector.GetDateTime(7) : DateTime.Now;
                            string rol = !lector.IsDBNull(7) ? lector.GetString(8) : string.Empty;

                            // Recuperar el hash y el salt almacenados
                            string saltAlmacenado = !lector.IsDBNull(9) ? lector.GetString(9) : "";

                            // Verificar la contraseña ingresada
                            bool esContraseñaValida = hasher.VerifyPassword(contraseña, saltAlmacenado, hashAlmacenado);

                            if (esContraseñaValida)
                            {
                                // Crear y retornar el objeto Usuario
                                BE.Usuario usuario = new BE.Usuario(id_usuario, dni, nombre, apellido, telefono, email_db, contraseña, fecha_nacimiento, rol, saltAlmacenado);
                                return usuario; // Retorna el usuario si la contraseña es válida
                            }
                        }
                    }
                }

                db.Desconectar();
                return null; // Si no se encontró ningún usuario o la contraseña es incorrecta, devuelve null
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return null; // Devuelve null en caso de error
            }
        }

    }
}
