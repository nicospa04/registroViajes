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
            db = new BaseDeDatos();
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
                            string salt = !lector.IsDBNull(9) ? lector.GetString(9) : string.Empty;
                            string idioma = !lector.IsDBNull(8) ? lector.GetString(8) : string.Empty;
                            int id_familia = !lector.IsDBNull(10) ? lector.GetInt32(10) : 0;
                            BE.Usuario usuario = new BE.Usuario(id_usuario, dni, nombre, apellido, telefono, email, contraseña, fecha_nacimiento, id_familia, salt, idioma);

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

        public Servicios.Resultado<BE.Usuario> crearEntidad(BE.Usuario obj)
        {

            Servicios.Resultado<BE.Usuario> resultado = new Servicios.Resultado<BE.Usuario>();




            string salt;
            string hashedPassword = hasher.HashPassword(obj.contraseña, out salt);

            string queryToCreateUser = "USE SistemaViajes;" +
                "INSERT INTO Usuario (dni, nombre, contraseña, apellido, telefono, email, fecha_nacimiento, id_familia, salt, idioma)" +
                "VALUES" +
                $"('{obj.dni}','{obj.nombre}', '{hashedPassword}', '{obj.apellido}', '{obj.telefono}', '{obj.mail}', '{obj.fechaNacimiento.ToString("yyyy-MM-dd")}', '{obj.id_familia}', '{salt}', '{obj.idioma}');";

            string queryToSearchUser = $"USE SistemasViajes; SELECT * FROM Usuario WHERE email = '{obj.mail}'";

            try
            {
                SqlCommand cmd = new SqlCommand(queryToSearchUser, db.Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close(); 
                    throw new Exception("Ya existe un usuario con ese mail");
                }
                reader.Close(); 

            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
            }



            try
            {
                bool result = db.ejecutarQuery(queryToCreateUser);
                if (!result) throw new Exception("Error al usuario");

                resultado.resultado = true;
                resultado.mensaje = "Usuario creado exitosamente";
                resultado.entidad = obj;
                
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
            }


            return resultado;
        }

        public Servicios.Resultado<BE.Usuario> eliminarEntidad(BE.Usuario obj)
        {

            Servicios.Resultado < BE.Usuario > Resultado = new Servicios.Resultado<BE.Usuario>();


            string query = "USE SistemaViajes;" +
                                   $"DELETE FROM Usuario WHERE id_usuario = {obj.id_usuario}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al eliminar destino");
                Resultado.resultado = true;
                Resultado.mensaje = "Usuario eliminado con éxito";
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

        public Servicios.Resultado<BE.Usuario> actualizarEntidad(BE.Usuario obj)
        {


            Servicios.Resultado < BE.Usuario > Resultado = new Servicios.Resultado<BE.Usuario>();


            string query = "USE SistemaViajes;" +
                           
                           "UPDATE Usuario" +
           $"SET nombre = '{obj.nombre}', contraseña = '{obj.contraseña}', id_familia = {obj.id_familia}" +
           $"WHERE id_usuario = {obj.id_usuario}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al actualizar destino");
                Resultado.resultado = true;
                Resultado.mensaje = "Usuario actualizado con éxito";
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

        public string recuperarIdioma(string id_usuario)
        {
            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Consulta SQL solo para obtener el salt y el hash
            string sqlQuery =$"USE SistemaViajes; SELECT idioma FROM Usuario WHERE id_usuario = {id_usuario}";

            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");

                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    // Uso de parámetros para prevenir inyecciones SQL
             

                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        if (lector.Read()) // Si encontramos un usuario con el email
                        {
                            // Manejar posibles valores nulos
                            string idioma = !lector.IsDBNull(0) ? lector.GetString(0) : "ES";
                           
                            return idioma;
                        }
                    }
                }

                db.Desconectar();
                return "ES"; // Si no se encontró ningún usuario o la contraseña es incorrecta, devuelve null
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return null; // Devuelve null en caso de error
            }
        }
        public bool modificarIdioma(BE.Usuario user, string idiomaNuevo)
        {

            string idioma = idiomaNuevo == "Español" ? "ES" : "EN";


            string query = "USE SistemaViajes; " +
                           "UPDATE Usuario " +
           $"SET idioma = '{idioma}' "+
           $"WHERE id_usuario = {user.id_usuario}";

            try
            {
                bool resultado = db.ejecutarQuery(query);
                if (!resultado) throw new Exception("Error al actualizar idioma");
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return false;
            }
        }


        public Servicios.Resultado<BE.Usuario> recuperarUsuario(string email, string contraseña)
        {

            Servicios.Resultado<BE.Usuario> resultado = new Servicios.Resultado<BE.Usuario>();

                 

            // Consulta SQL solo para obtener el salt y el hash
            string sqlQuery = "USE SistemaViajes; SELECT * FROM Usuario WHERE email = @Email";

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
                        if (!lector.HasRows)
                        {
                            throw new Exception("No se encontró un usuario con ese mail");
                        }



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
                            int id_familia = !lector.IsDBNull(8) ? lector.GetInt32(8) : 0;
                            string idioma = !lector.IsDBNull(10) ? lector.GetString(10) : string.Empty;
                            // Recuperar el hash y el salt almacenados
                            string saltAlmacenado = !lector.IsDBNull(9) ? lector.GetString(9) : "";

                            // Verificar la contraseña ingresada
                            bool esContraseñaValida = hasher.VerifyPassword(contraseña, saltAlmacenado, hashAlmacenado);

                            if (!esContraseñaValida)
                            {
                               
                                throw new Exception("Contraseña incorrecta");
                               
                            }



                            if (esContraseñaValida)
                            {
                                
                              BE.Usuario usuario = new BE.Usuario(id_usuario, dni, nombre, apellido, telefono, email_db, contraseña, fecha_nacimiento, id_familia, saltAlmacenado, idioma);
                             
                                resultado.resultado = true;
                                resultado.entidad = usuario;
                                resultado.mensaje = "Inicio de sesión correcto";

                            }
                        }
                    }
                }

                db.Desconectar();
                return resultado; // Si no se encontró ningún usuario o la contraseña es incorrecta, devuelve null
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
                return resultado;
            }
        }

    }
}
