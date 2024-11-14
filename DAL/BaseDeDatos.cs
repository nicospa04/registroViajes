using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
    {
        public class BaseDeDatos
        {
            public static string dataSource = "compuReLoca";
            public static string dbName = "SistemaViajes";
            public static string conexionMaster = $"Data source={dataSource};Initial Catalog=master;Integrated Security=True;";
            public SqlConnection Connection = new SqlConnection(conexionMaster);
            public SqlCommand Command = new SqlCommand();
            public bool Conectar()
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                    return true;
                }
                return false;
            }
            public bool Desconectar()
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                    return true;
                }
                return false;
            }
            public bool ejecutarQuery(string query)
            {
            try
            {
                Conectar();
                Command = new SqlCommand(query, Connection);
                Command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Desconectar();
            }
            }
            public void scriptInicio()
            {
                // Crear la base de datos
                bool bdCreada = ejecutarQuery("CREATE DATABASE SistemaViajes;");
                // Crear las tablas dentro de la base de datos SistemaViajes
                if (bdCreada)
                {
                    ejecutarQuery("USE SistemaViajes; CREATE TABLE Usuario (" +
                        "id_usuario INT PRIMARY KEY IDENTITY(1,1)," +
                        "dni VARCHAR(20) NOT NULL," +
                        "nombre VARCHAR(50) NOT NULL," +
                        "contraseña VARCHAR(50) NOT NULL," +
                        "apellido VARCHAR(50) NOT NULL," +
                        "telefono VARCHAR(20)," +
                        "email VARCHAR(100) NOT NULL," +
                        "fecha_nacimiento DATE," +
                        "id_familia INT," +
                        "salt VARCHAR(50)," +
                        "idioma VARCHAR(50)" +
                         ");");

                    ejecutarQuery("USE SistemaViajes; CREATE TABLE Empresa (" +
                        "id_empresa INT PRIMARY KEY IDENTITY(1,1)," +
                        "nombre VARCHAR(100) NOT NULL," +
                        "descripcion TEXT," +
                        "porcentaje_extra FLOAT" +
                        ");");

                    ejecutarQuery("USE SistemaViajes; CREATE TABLE Destino (" +
                        "id_destino INT PRIMARY KEY IDENTITY(1,1)," +
                        "nombre VARCHAR(100) NOT NULL," +
                        "descripcion TEXT," +
                        "precio_base FLOAT" +
                        ");");

                    ejecutarQuery("USE SistemaViajes; CREATE TABLE PermisosComp(" +
                        "id_permiso INT PRIMARY KEY IDENTITY(1,1)," +
                        "nombre NVARCHAR(100) NOT NULL," +
                        "nombreformulario NVARCHAR(100) NULL," +
                        "isperfil BIT NOT NULL DEFAULT 0" +
                        ");");

                    ejecutarQuery("USE SistemaViajes; CREATE TABLE PermisoPermiso(" +
                        "id_permisopadre INT NOT NULL," +
                        "id_permisohijo INT NOT NULL," +
                        "FOREIGN KEY(id_permisopadre) REFERENCES PermisosComp(id_permiso)," +
                        "FOREIGN KEY(id_permisohijo) REFERENCES PermisosComp(id_permiso)" +
                        ");");

                    ejecutarQuery("USE SistemaViajes; CREATE TABLE UsuarioPermiso(" +
                        "id_usuario INT NOT NULL, " +
                        "id_permiso INT NOT NULL, " +
                        "FOREIGN KEY(id_usuario) REFERENCES Usuario(id_usuario)," +
                        "FOREIGN KEY(id_permiso) REFERENCES PermisosComp(id_permiso)," +
                        "PRIMARY KEY(id_usuario, id_permiso)" +
                        ");");

                    ejecutarQuery("USE SistemaViajes; CREATE TABLE tipo_transporte(" +
                        "id_tipo_transporte INT PRIMARY KEY IDENTITY (1,1)," +
                        "nombre VARCHAR(100));");


                    ejecutarQuery("USE SistemaViajes; CREATE TABLE Transporte" +
                        "(id_transporte INT PRIMARY KEY IDENTITY(1,1)," +
                        "porcentaje_extra DECIMAL NOT NULL, " +
                        "modelo VARCHAR(100), " +
                        "id_tipo_transporte INT, " +
                        "id_empresa INT NOT NULL, " +
                        "FOREIGN KEY (id_empresa) REFERENCES Empresa(id_empresa)," +
                        "FOREIGN KEY (id_tipo_transporte) REFERENCES tipo_transporte(id_tipo_transporte));");

                    ejecutarQuery("USE SistemaViajes; CREATE TABLE Bitacora (" +
                        "id_bitacora INT PRIMARY KEY IDENTITY(1,1)," +
                        "id_usuario INT NOT NULL," +
                        "operacion VARCHAR(100)," +
                        "fecha DATETIME," +
                        "actor VARCHAR(100)," +
                        "criticidad INT NOT NULL," +
                        "FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)" +
                        ");");

                    ejecutarQuery(
                         "USE SistemaViajes; CREATE TABLE Fecha(" +
                         "id_fecha INT PRIMARY KEY IDENTITY(1,1)," +
                         "id_empresa INT NOT NULL," +
                         "id_lugar_origen INT NOT NULL," +
                         "id_lugar_destino INT NOT NULL," +
                         "id_transporte INT NOT NULL," +
                         "fecha_ida DATETIME," +
                         "fecha_vuelta DATETIME," +
                         "categoria_tipo VARCHAR(100)," +
                         "FOREIGN KEY (id_empresa) REFERENCES Empresa(id_empresa)," +
                         "FOREIGN KEY (id_lugar_origen) REFERENCES Destino(id_destino)," +
                         "FOREIGN KEY (id_lugar_destino) REFERENCES Destino(id_destino)," +
                         "FOREIGN KEY (id_transporte) REFERENCES Transporte(id_transporte));"
                        );

                    ejecutarQuery("USE SistemaViajes; CREATE TABLE Asiento(" +
                        "id_asiento INT PRIMARY KEY IDENTITY(1,1)," +
                        "id_fecha INT NOT NULL," +
                        "num_asiento INT," +
                        "esta_disponible BIT DEFAULT 0," +
                        "FOREIGN KEY (id_fecha) REFERENCES Fecha(id_fecha));");

                    ejecutarQuery("USE SistemaViajes; CREATE TABLE Viaje(" +
                        "id_viaje INT PRIMARY KEY IDENTITY(1,1)," +
                        "id_usuario INT," +
                        "id_empresa INT," +
                        "id_fecha INT," +
                        "transporte VARCHAR(100)," +
                        "costo DECIMAL(18, 2)," +
                        "num_asiento INT," +
                        "FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)," +
                        "FOREIGN KEY (id_empresa) REFERENCES Empresa(id_empresa)," +
                        "FOREIGN KEY (id_fecha) REFERENCES Fecha(id_fecha)" +
                        ");");

                ejecutarQuery("USE SistemaViajes; CREATE TABLE Paquete(" +
                                "id_paquete INT PRIMARY KEY IDENTITY(1,1)," +
                                "nombre VARCHAR(100)," +
                                "precio DECIMAL," +
                                "descripcion TEXT," +
                                "cantidad_max_asientos INT NOT NULL," +
                                "id_fecha INT NOT NULL," +
                                "FOREIGN KEY (id_fecha) REFERENCES Fecha(id_fecha)" +
                                ");");

                ejecutarQuery("USE SistemaViajes; CREATE TABLE PaqueteAsiento(" +
                    "id_paquete_asiento INT PRIMARY KEY IDENTITY(1,1)," +
                    "id_paquete INT," +
                    "id_asiento INT," +
                    "FOREIGN KEY (id_paquete) REFERENCES Paquete(id_paquete)," +
                    "FOREIGN KEY (id_asiento) REFERENCES Asiento(id_asiento)" +
                    ");");
                scriptDatos();
            }
        }

        void scriptDatos()
        {
            PasswordHasher hasher = new PasswordHasher();
            // Generar salt y hash para el usuario Juan
            string saltJuan;
            string hashJuan = hasher.HashPassword("abc1", out saltJuan);
            // Generar salt y hash para el usuario María
            string saltMaria;
            string hashMaria = hasher.HashPassword("abc2", out saltMaria);
            // Generar salt y hash para el usuario Carlos
            string saltCarlos;
            string hashCarlos = hasher.HashPassword("abc3", out saltCarlos);
            // Insertar datos en la tabla Usuario con salt y contraseña hasheada
            ejecutarQuery($"USE SistemaViajes; INSERT INTO Usuario (dni, nombre, apellido, contraseña, telefono, email, fecha_nacimiento, id_familia, salt, idioma) " +
                "VALUES " +
                $"('12345678', 'Juan', 'Pérez', '{hashJuan}', '555-1234', 'juan.perez@example.com', '1985-04-23', 1, '{saltJuan}', 'ES')," +
                $"('87654321', 'María', 'González', '{hashMaria}', '555-5678', 'maria.gonzalez@example.com', '1990-09-12', 1, '{saltMaria}', 'ES')," +
                $"('13579111', 'Carlos', 'Martínez', '{hashCarlos}', '555-6789', 'carlos.martinez@example.com', '1978-02-15', 3, '{saltCarlos}', 'EN');");

            // Insertar datos en la tabla Empresa
            ejecutarQuery("USE SistemaViajes; INSERT INTO Empresa (nombre, descripcion, porcentaje_extra) " +
                "VALUES " +
                "('Viajes Excelsior', 'Agencia de viajes especializada en tours internacionales.', 1.25)," +
                "('Turismo Global', 'Empresa líder en turismo local y regional.', 1.47);");

            // Insertar datos en la tabla Destino
            ejecutarQuery("USE SistemaViajes; INSERT INTO Destino (nombre, descripcion, precio_base) " +
                "VALUES " +
                "('París', 'La ciudad del amor, conocida por su arte, cultura y la Torre Eiffel.', 1000.00)," +
                "('Nueva York', 'La ciudad que nunca duerme, llena de rascacielos y vida urbana.', 1200.00)," +
                "('Bariloche', 'Destino turístico en la Patagonia argentina, famoso por sus paisajes naturales.', 800.00);");

            // Insertar datos en la tabla tipo_transporte
            ejecutarQuery("USE SistemaViajes; INSERT INTO tipo_transporte(nombre) " +
                "VALUES ('avion'),('bus'),('barco');");

            // Insertar datos en la tabla Transporte
            ejecutarQuery("USE SistemaViajes; INSERT INTO Transporte (porcentaje_extra, modelo, id_tipo_transporte, id_empresa) " +
                "VALUES " +
                "(1.15, 'Boeing 747', 1, 1)," +    // 1 = Avión
                "(1.25, 'Crucero Marítimo', 3, 2)," + // 3 = Barco
                "(1.10, 'Colectivo de Turismo', 2, 1);"); // 2 = Bus

            // Insertar datos en la tabla Fecha (trayectos específicos)
            //ejecutarQuery("USE SistemaViajes; INSERT INTO Fecha (id_empresa, id_lugar_origen, id_lugar_destino, id_transporte, fecha_ida, fecha_vuelta, categoria_tipo) " +
            //    "VALUES " +
            //    "(1, 1, 2, 1, '2024-09-01 10:00:00', '2024-09-10 18:00:00', 'Avión')," +
            //    "(2, 3, 2, 2, '2024-10-05 08:00:00', '2024-10-12 20:00:00', 'Barco')," +
            //    "(1, 2, 3, 3, '2024-12-20 12:00:00', '2024-12-30 16:00:00', 'Bus');");

            // Insertar datos en la tabla Asiento
            //ejecutarQuery("USE SistemaViajes; INSERT INTO Asiento (id_fecha, num_asiento, esta_disponible) " +
            //    "VALUES " +
            //    "(1, 1, 0)," +
            //    "(1, 2, 1)," +
            //    "(1, 3, 0)," +
            //    "(2, 1, 0)," +
            //    "(2, 2, 1)," +
            //    "(3, 1, 0)," +
            //    "(3, 2, 1);");

            // Insertar datos en la tabla Viaje
            ejecutarQuery("USE SistemaViajes; INSERT INTO Viaje (id_usuario, id_empresa, id_fecha, transporte, costo) " +
                "VALUES " +
                "(1, 1, 1, 'Avión', 3000.00)," +
                "(2, 2, 2, 'Barco', 4000.00)," +
                "(1, 2, 3, 'Bus', 2400.00);");

            // Insertar datos en la tabla Paquete
            //ejecutarQuery("USE SistemaViajes; INSERT INTO Paquete (id_fecha, precio, cantidad_max_asientos, nombre, descripcion) " +
            //    "VALUES " +
            //    "(1, 1500.00, 2, 'Romance en París', 'Paquete especial para parejas, incluye visitas guiadas y cenas románticas.')," +
            //    "(2, 2000.00, 4, 'Nueva York Express', 'Paquete para conocer lo esencial de Nueva York en 5 días.')," +
            //    "(3, 1200.00, 5, 'Aventura en Bariloche', 'Paquete de aventura que incluye excursiones, caminatas y deportes de invierno.');");

            //// Insertar datos en la tabla PaqueteAsiento
            //ejecutarQuery("USE SistemaViajes; INSERT INTO PaqueteAsiento (id_paquete, id_asiento) " +
            //    "VALUES " +
            //    "(1, 1)," +
            //    "(1, 2)," +
            //    "(2, 4)," +
            //    "(2, 5)," +
            //    "(3, 6)," +
            //    "(3, 7);");

            // Insertar datos en la tabla PermisosComp
            ejecutarQuery("USE SistemaViajes; INSERT INTO PermisosComp (nombre, nombreformulario, isperfil) " +
                "VALUES" +
                "('Cliente', 'iconMenuItem2', 1), " +
                "('Empleado', 'iconMenuItem2', 1), " +
                "('Admin', 'iconMenuItem2', 1), " +
                "('Viaje', 'toolStripMenuItem1', 0), " +
                "('Cancelar', 'cancelarToolStripMenuItem', 0), " +
                "('Paquetes', 'iconMenuItem3', 0)," +
                "('ModificarViaje', 'toolStripMenuItem2', 0)," +
                "('VerViajes', 'verViajesRealizadosToolStripMenuItem', 0)," +
                "('Reg', 'registrosToolStripMenuItem', 0)," +
                "('RegUser', 'usuariosToolStripMenuItem', 0)," +
                "('RegEmp', 'empresasToolStripMenuItem', 0)," +
                "('RegDest', 'destinosToolStripMenuItem', 0)," +
                "('RegPaque', 'paquetesToolStripMenuItem', 0)," +
                "('Sesion', 'iconMenuItem1', 0)," +
                "('IniSes', 'iconMenuItem7', 0)," +
                "('CerSes', 'cerrarSesionToolStripMenuItem', 0)," +
                "('CamIdio', 'iconMenuItem5', 0)," +
                "('Exit', 'iconMenuItem6', 0)," +
                "('Config', 'iconMenuItem4', 0)," +
                "('Bitacora', 'bitacoraToolStripMenuItem', 0);");

            // Insertar datos en la tabla PermisoPermiso
            ejecutarQuery("USE SistemaViajes; INSERT INTO PermisoPermiso (id_permisopadre, id_permisohijo) " +
                "VALUES" +
                "(3, 1), " +
                "(3, 2)," +
                "(1, 1)," +
                "(1, 4)," +
                "(1, 5)," +
                "(1, 6)," +
                "(2, 7)," +
                "(2, 8)," +
                "(2, 2)," +
                "(2, 9)," +
                "(2, 10)," +
                "(2, 11)," +
                "(2, 12)," +
                "(2, 13)," +
                "(1, 14)," +
                "(1, 15)," +
                "(1, 16)," +
                "(1, 17)," +
                "(1, 18)," +
                "(3, 3)," +
                "(3, 4)," +
                "(3, 5)," +
                "(3, 6)," +
                "(3, 7)," +
                "(3, 8)," +
                "(3, 9)," +
                "(3, 10)," +
                "(3, 11)," +
                "(3, 12)," +
                "(3, 13)," +
                "(3, 14)," +
                "(3, 15)," +
                "(3, 16)," +
                "(3, 17)," +
                "(3, 18)," +
                "(3, 19)," +
                "(3, 20)," +
                "(2, 14)," +
                "(2, 15)," +
                "(2, 16)," +
                "(2, 18)," +
                "(2, 17);");

            // Insertar datos en la tabla UsuarioPermiso
            ejecutarQuery("USE SistemaViajes; INSERT INTO UsuarioPermiso (id_usuario, id_permiso)" +
                "VALUES " +
                "(3, 3), " +
                "(2, 2), " +
                "(1, 1);");
        }

    }
}

    
