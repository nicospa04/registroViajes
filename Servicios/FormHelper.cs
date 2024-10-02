        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Reflection;
        using System.Windows.Forms;

        public class FormHelper
        {
            // Método para obtener todos los formularios de la aplicación
            public static List<Type> ObtenerTodosLosFormularios()
            {
        // Obtener todos los tipos en el ensamblado que hereden de Form
        var assembly = Assembly.Load("RegistroViajes");  // Reemplaza con el nombre correcto del ensamblado que contiene los formularios

        var formularios = assembly.GetTypes()
                                  .Where(t => t.IsSubclassOf(typeof(Form)) && !t.IsAbstract)
                                  .ToList();


        return formularios;
            }
        }
