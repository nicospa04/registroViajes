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

        static string dataSource = "090L3PC16-80598";
        static string dbName = "SistemaViajes";
        static string conexionMaster = $"Data source={dataSource};Initial Catalog=master;Integrated Security=True;";

        public SqlConnection Connection = new SqlConnection(conexionMaster);
        public SqlCommand Command = new SqlCommand();

        public void Conectar()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        public void Desconectar()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public bool ejecutarQuery(string query)
        {
            try
            {
                Conectar();
                Command = new SqlCommand(query, Connection);
                Command.ExecuteNonQuery();
                Desconectar();
                return true;
            }
            catch
            {
                return false;
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
                    "rol VARCHAR(50));");

                ejecutarQuery("USE SistemaViajes; CREATE TABLE Empresa (" +
                    "id_empresa INT PRIMARY KEY IDENTITY(1,1)," +
                    "nombre VARCHAR(100) NOT NULL," +
                    "descripcion TEXT);");

                ejecutarQuery("USE SistemaViajes; CREATE TABLE Destino (" +
                    "id_destino INT PRIMARY KEY IDENTITY(1,1)," +
                    "nombre VARCHAR(100) NOT NULL," +
                    "descripcion TEXT);");

                ejecutarQuery("USE SistemaViajes; CREATE TABLE Paquete (" +
                    "id_paquete INT PRIMARY KEY IDENTITY(1,1)," +
                    "id_destino INT," +
                    "precio_base DECIMAL(18, 2)," +
                    "cupo_personas INT," +
                    "nombre VARCHAR(100) NOT NULL," +
                    "descripcion TEXT," +
                    "FOREIGN KEY (id_destino) REFERENCES Destino(id_destino));");

                ejecutarQuery("USE SistemaViajes; CREATE TABLE FechasDisponibles (" +
                    "id_fecha INT PRIMARY KEY IDENTITY(1,1)," +
                    "id_paquete INT," +
                    "fecha_inicio DATE NOT NULL," +
                    "fecha_vuelta DATE NOT NULL," +
                    "FOREIGN KEY (id_paquete) REFERENCES Paquete(id_paquete));");

                ejecutarQuery("USE SistemaViajes; CREATE TABLE Viaje (" +
                    "id_viaje INT PRIMARY KEY IDENTITY(1,1)," +
                    "id_usuario INT," +
                    "id_empresa INT," +
                    "id_destino INT," +
                    "id_fecha INT," +
                    "transporte VARCHAR(100)," +
                    "cant_adulto INT," +
                    "cant_niños INT," +
                    "costo DECIMAL(18, 2)," +
                    "FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)," +
                    "FOREIGN KEY (id_empresa) REFERENCES Empresa(id_empresa)," +
                    "FOREIGN KEY (id_destino) REFERENCES Destino(id_destino)," +
                    "FOREIGN KEY (id_fecha) REFERENCES FechasDisponibles(id_fecha));");

                ejecutarQuery("USE SistemaViajes; CREATE TABLE cargado (" +
                    "id_cargado INT PRIMARY KEY IDENTITY(1,1)," +
                    "cargado BIT);");


                scriptDatos();
            }
        }

        void scriptDatos()
        {
            // Cambiar a la base de datos 'SistemaViajes'
            
            // Insertar datos en la tabla Usuario
            ejecutarQuery("USE SistemaViajes; INSERT INTO Usuario (dni, nombre, apellido, contraseña, telefono, email, fecha_nacimiento, rol) " +
                "VALUES " +
                "('12345678', 'Juan', 'Pérez', 'abc1', '555-1234', 'juan.perez@example.com', '1985-04-23', 'cliente')," +
                "('87654321', 'María', 'González', 'abc2', '555-5678', 'maria.gonzalez@example.com', '1990-09-12', 'cliente')," +
                "('13579111', 'Carlos', 'Martínez', 'abc3', '555-6789', 'carlos.martinez@example.com', '1978-02-15', 'administrador');");

            // Insertar datos en la tabla Empresa
            ejecutarQuery("USE SistemaViajes; INSERT INTO Empresa (nombre, descripcion) " +
                "VALUES " +
                "('Viajes Excelsior', 'Agencia de viajes especializada en tours internacionales.')," +
                "('Turismo Global', 'Empresa líder en turismo local y regional.');");

            // Insertar datos en la tabla Destino
            ejecutarQuery("USE SistemaViajes; INSERT INTO Destino (nombre, descripcion) " +
                "VALUES " +
                "('París', 'La ciudad del amor, conocida por su arte, cultura y la Torre Eiffel.')," +
                "('Nueva York', 'La ciudad que nunca duerme, llena de rascacielos y vida urbana.')," +
                "('Bariloche', 'Destino turístico en la Patagonia argentina, famoso por sus paisajes naturales.');");

            // Insertar datos en la tabla Paquete
            ejecutarQuery("USE SistemaViajes; INSERT INTO Paquete (id_destino, precio_base, cupo_personas, nombre, descripcion) " +
                "VALUES " +
                "(1, 1500.00, 20, 'Romance en París', 'Paquete especial para parejas, incluye visitas guiadas y cenas románticas.')," +
                "(2, 2000.00, 15, 'Nueva York Express', 'Paquete para conocer lo esencial de Nueva York en 5 días.')," +
                "(3, 1200.00, 25, 'Aventura en Bariloche', 'Paquete de aventura que incluye excursiones, caminatas y deportes de invierno.');");

            // Insertar datos en la tabla Fechas Disponibles
            ejecutarQuery("USE SistemaViajes; INSERT INTO FechasDisponibles (id_paquete, fecha_inicio, fecha_vuelta) " +
                "VALUES " +
                "(1, '2024-09-01', '2024-09-10')," +
                "(2, '2024-10-05', '2024-10-12')," +
                "(3, '2024-12-20', '2024-12-30');");

            // Insertar datos en la tabla Viaje
            ejecutarQuery("USE SistemaViajes; INSERT INTO Viaje (id_usuario, id_empresa, id_destino, id_fecha, transporte, cant_adulto, cant_niños, costo) " +
                "VALUES " +
                "(1, 1, 1, 1, 'Avión', 2, 0, 3000.00)," +
                "(2, 2, 2, 2, 'Avión', 1, 1, 4000.00)," +
                "(1, 2, 3, 3, 'Bus', 2, 2, 2400.00);");
        }





    }
}
