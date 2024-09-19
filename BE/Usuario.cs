using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string mail { get; set; }
        public string contraseña {  get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string rol {  get; set; }

        public Usuario(int id, string dni, string nombre, string apellido, string telefono, string mail, string contraseña, DateTime fechaNacimiento, string rol)
        {
            this.id_usuario = id;
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.mail = mail;
            this.contraseña = contraseña;
            this.fechaNacimiento = fechaNacimiento;
            this.rol = rol;
        }

        public Usuario(string dni, string nombre, string apellido, string telefono, string mail, string contraseña, DateTime fechaNacimiento, string rol)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.mail = mail;
            this.contraseña = contraseña;
            this.fechaNacimiento = fechaNacimiento;
            this.rol = rol;
        }


    }
}
