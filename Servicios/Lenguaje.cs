using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Servicios
{
    public class Lenguaje : ISubjetc
    {
        private List<IObserver> ListaForms = new List<IObserver>();
        private Dictionary<string, string> Diccionario;
        private string idiomaActual;
        private static Lenguaje instance;

        private Lenguaje() { }

        public static Lenguaje ObtenerInstancia()
        {
            if (instance == null)
            {
                instance = new Lenguaje();
            }
            return instance;
        }

        public void Agregar(IObserver observer)
        {
            ListaForms.Add(observer);
        }
        public void Quitar(IObserver observer)
        {
            ListaForms.Remove(observer);
        }
        public void Notificar()
        {
            foreach (IObserver observer in ListaForms)
            {
                observer.ActualizarIdioma();
            }
        }

        public string IdiomaActual
        {
            get
            {
                return idiomaActual;
            }
            set
            {
                idiomaActual = value;
                CargarIdioma();
                Notificar();
            }
        }

        public void CargarIdioma()
        {
            string idioma = idiomaActual == "ES" ? "Español" : "Inglés";


            var NombreArchivo = $"{idioma}.json";
            var jsonString = File.ReadAllText(NombreArchivo);
            Diccionario = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
        }

        public string ObtenerTexto(string key)
        {
            return Diccionario.ContainsKey(key) ? Diccionario[key] : key;
        }
        public void CambiarIdiomaControles(Control frm)
        {
            try
            {
                frm.Text = Lenguaje.ObtenerInstancia().ObtenerTexto(frm.Name + ".Text");

                foreach (Control c in frm.Controls)
                {
                    if (c is Button || c is Label)
                    {
                        c.Text = ObtenerInstancia().ObtenerTexto(frm.Name + "." + c.Name);
                    }

                    if (c is MenuStrip m)
                    {
                        foreach (ToolStripMenuItem item in m.Items)
                        {
                            if (item is ToolStripMenuItem toolStripMenuItem)
                            {
                                item.Text = ObtenerInstancia().ObtenerTexto(frm.Name + "." + item.Name);
                                CambiarIdiomaMenuStrip(toolStripMenuItem.DropDownItems, frm);
                            }
                        }
                    }

                    if (c.Controls.Count > 0)
                    {
                        CambiarIdiomaControles(c);
                    }
                }
            }
            catch (Exception) { };
        }

        private void CambiarIdiomaMenuStrip(ToolStripItemCollection items, Control frm)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem item1)
                {
                    item.Text = ObtenerInstancia().ObtenerTexto(frm.Name + "." + item.Name);

                    CambiarIdiomaMenuStrip(item1.DropDownItems, frm);
                }
            }
        }
        public static string ObtenerEtiqueta(string NombreControl)
        {
            return Lenguaje.ObtenerInstancia().ObtenerTexto(NombreControl);
        }

        public void CambiarIdioma()
        {

        }










    }
}
