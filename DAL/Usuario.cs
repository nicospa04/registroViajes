﻿using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Usuario : ICrud<BE.Usuario>
    {

        BaseDeDatos db { get; }
        PasswordHasher hasher { get; set; }

        public Usuario()
        {
            db = new BaseDeDatos();
            hasher = new PasswordHasher();
        }

        public List<Permiso> ObtenerPermisosUsuario(int idUsuario)
        {
            List<Permiso> permisos = new List<Permiso>();
            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");

                // Consulta para obtener los permisos directos del usuario
                string query = @"USE SistemaViajes;
                SELECT P.nombre, P.nombreformulario, P.isperfil, P.id_permiso
                FROM UsuarioPermiso UP
                INNER JOIN PermisosComp P ON UP.id_permiso = P.id_permiso
                WHERE UP.id_usuario = @idUsuario";

                SqlCommand cmd = new SqlCommand(query, db.Connection);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                SqlDataReader reader = cmd.ExecuteReader();

                // Crear una lista temporal para los perfiles que necesitan obtener permisos hijos
                List<int> permisosPerfilIds = new List<int>();

                while (reader.Read())
                {
                    string nombrePermiso = reader["nombre"].ToString();
                    string nombreFormulario = reader["nombreformulario"].ToString();
                    bool isPerfil = Convert.ToBoolean(reader["isperfil"]);
                    int idPermiso = Convert.ToInt32(reader["id_permiso"]);

                    Permiso permiso = new Permiso(nombrePermiso, nombreFormulario, isPerfil);
                    permisos.Add(permiso);  // Agregar el permiso directo a la lista

                    // Si el permiso es un perfil, guardamos su ID para obtener los permisos hijos después
                    if (isPerfil)
                    {
                        permisosPerfilIds.Add(idPermiso);
                    }
                }
                reader.Close();  // Cerramos el lector antes de ejecutar la siguiente consulta

                // Obtener los permisos hijos para cada perfil
                foreach (int idPermisoPadre in permisosPerfilIds)
                {
                    List<Permiso> permisosHijos = ObtenerPermisosHijos(idPermisoPadre);
                    permisos.AddRange(permisosHijos);  // Agregar los permisos hijos
                }

                bool result2 = db.Desconectar();
                if (!result2) throw new Exception("Error al desconectarse de la base de datos");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los permisos del usuario: " + ex.Message);
            }
            return permisos;
        }



        public int encontrarIdUsuarioPorUsuario(BE.Usuario user)
        {
            string query = $"USE SistemaViajes; SELECT id_usuario FROM Usuario WHERE nombre = '{user.nombre}' AND email = '{user.mail}'";

            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Consulta SQL solo para obtener el salt y el hash
            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");

                using (SqlCommand command = new SqlCommand(query, db.Connection))
                {
                    // Uso de parámetros para prevenir inyecciones SQL


                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        if (lector.Read()) // Si encontramos un usuario con el email
                        {
                            // Manejar posibles valores nulos
                            int id = !lector.IsDBNull(lector.GetOrdinal("id_usuario")) ? lector.GetInt32(lector.GetOrdinal("id_usuario")) : 0;

                            return id;
                        }
                    }
                }

                db.Desconectar();
                return 0; // Si no se encontró ningún usuario o la contraseña es incorrecta, devuelve null
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return 0; // Devuelve null en caso de error
            }

        }




        private List<Permiso> ObtenerPermisosHijos(int idPermisoPadre)
        {
            List<Permiso> permisosHijos = new List<Permiso>();

            try
            {
                // Consulta para obtener los permisos hijos de un permiso padre
                string query = @"USE SistemaViajes;
                    SELECT P.nombre, P.nombreformulario
                    FROM PermisoPermiso PP
                    INNER JOIN PermisosComp P ON PP.id_permisohijo = P.id_permiso
                    WHERE PP.id_permisopadre = @idPermisoPadre";



                //bool result = db.Conectar();
                //if (!result) throw new Exception("Error al conectarse a la base de datos");

                SqlCommand cmd = new SqlCommand(query, db.Connection);
                cmd.Parameters.AddWithValue("@idPermisoPadre", idPermisoPadre);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nombrePermisoHijo = reader["nombre"].ToString();
                    string formPermisoHijo = reader["nombreformulario"].ToString();
                    
                    Permiso permiso = new Permiso(nombrePermisoHijo, formPermisoHijo);
                    permisosHijos.Add(permiso);  // Agregar el nombre del permiso hijo a la lista
                }
                reader.Close();


                //bool result2 = db.Desconectar();
                //if (!result2) throw new Exception("Error al desconectarse de la base de datos");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los permisos hijos: " + ex.Message);
            }
            return permisosHijos;
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
            bool resulta = db.Conectar();
            if (!resulta) throw new Exception("Error al conectarse a la base de datos");

            Servicios.Resultado<BE.Usuario> resultado = new Servicios.Resultado<BE.Usuario>();

            string salt;
            string hashedPassword = hasher.HashPassword(obj.contraseña, out salt);

            string queryToCreateUser = "USE SistemaViajes;" +
                "INSERT INTO Usuario (dni, nombre, contraseña, apellido, telefono, email, fecha_nacimiento, id_familia, salt, idioma)" +
                "VALUES" +
                $"('{obj.dni}','{obj.nombre}', '{hashedPassword}', '{obj.apellido}', '{obj.telefono}', '{obj.mail}', '{obj.fechaNacimiento.ToString("yyyy-MM-dd")}', '{obj.id_familia}', '{salt}', '{obj.idioma}');";

            string queryToSearchUser = $"USE SistemaViajes; SELECT * FROM Usuario WHERE email = '{obj.mail}'";

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
                return resultado;
            }

            try
            {
                bool result = db.ejecutarQuery(queryToCreateUser);
                if (!result) throw new Exception("Error al registrar usuario");

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
            finally
            {
                // Asegúrate de cerrar la conexión de forma segura
                try
                {
                    if (db.Connection != null && db.Connection.State == ConnectionState.Open)
                    {
                        bool resulta2 = db.Desconectar();
                        if (!resulta2) throw new Exception("Error al desconectarse de la base de datos");
                    }
                }
                catch (Exception ex)
                {
                    resultado.resultado = false;
                    resultado.mensaje = "Error al desconectar la base de datos: " + ex.Message;
                }
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

            


            string query = "USE SistemaViajes; " +
                           "UPDATE Usuario " +
           $"SET idioma = '{idiomaNuevo}' "+
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


        public string encontrarNombreUsuarioPorID(string id)
        {
            string query = $"USE SistemaViajes; SELECT nombre FROM Usuario WHERE id_usuario = {id}";

            // Crear e inicializar la instancia de la base de datos
            DAL.BaseDeDatos db = new DAL.BaseDeDatos();

            // Consulta SQL solo para obtener el salt y el hash
            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");

                using (SqlCommand command = new SqlCommand(query, db.Connection))
                {
                    // Uso de parámetros para prevenir inyecciones SQL


                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        if (lector.Read()) // Si encontramos un usuario con el email
                        {
                            // Manejar posibles valores nulos
                            string nombre = !lector.IsDBNull(0) ? lector.GetString(0) : "not found";

                            return nombre;
                        }
                    }
                }

                db.Desconectar();
                return "not found"; // Si no se encontró ningún usuario o la contraseña es incorrecta, devuelve null
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return null; // Devuelve null en caso de error
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
