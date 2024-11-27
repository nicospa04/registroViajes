using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroViajes
{
    public partial class CPerfil : Form,IObserver
    {
        private Servicios.Perfil INICIO;
        public CPerfil()
        {
            InitializeComponent();
            cargarcb();
            INICIO = new Servicios.Perfil();
            CargarTreeView();
        }

        BLLPerfil bllp = new BLLPerfil();
        public void ActualizarIdioma()
        {
            Lenguaje.ObtenerInstancia().CambiarIdiomaControles(this);
        }
        private void CargarTreeView()
        {
            TreeNode rootNode = CrearNodo(INICIO, "BASE"); //Nodo BASE.
            treeView1.Nodes.Add(rootNode); //agg como nodo lo creado de base arriba (ALMACEN).
        }
        private void CargarTreeView2()
        {
            TreeNode rootNode = CrearNodo2(INICIO, "BASE"); //Nodo BASE.
            treeView1.Nodes.Add(rootNode); //agg como nodo lo creado de base arriba (ALMACEN).
        }
        private void CargarTreeView3()
        {
            TreeNode rootNode = CrearNodo3(INICIO, "BASE");
            treeView1.Nodes.Add(rootNode);
            treeView1.ExpandAll();
        }
        private TreeNode CrearNodo(Componente componente, string nombre)
        {
            TreeNode node = new TreeNode(nombre) //crea nodo (componente).
            {
                Tag = componente
            };
            if (componente.ObtenerHijos() != null) //se fija si hay hijos dentro de un componente.
            {
                foreach (var hijo in componente.ObtenerHijos()) //recorre.
                {
                    string nombreHijo = hijo is Servicios.Perfil ? "Perfil" : $"Permiso";
                    node.Nodes.Add(CrearNodo(hijo, nombreHijo)); //llama recursivamente al método para agg un hijo
                }                                                   // dentro del componente.
            }
            return node;
        }
        private TreeNode CrearNodo2(Componente componente, string nombre)
        {
            string nombreperf = comboBox3.SelectedItem.ToString();
            TreeNode node = new TreeNode(nombre) //crea nodo (componente).
            {
                Tag = componente
            };
            if (componente.ObtenerHijos() != null) //se fija si hay hijos dentro de un componente.
            {
                foreach (var hijo in componente.ObtenerHijos()) //recorre.
                {
                    string nombreHijo = hijo is Servicios.Perfil ? nombreperf : "Permiso";
                    node.Nodes.Add(CrearNodo2(hijo, nombreHijo)); //llama recursivamente al método para agg un hijo
                }                                                   // dentro del componente.
            }
            return node;
        }
        private TreeNode CrearNodo3(Componente componente, string nombre)
        {
            // Obtener los valores seleccionados de los ComboBox
            string nombrePerfil = comboBox3.SelectedItem?.ToString(); // Obtiene el perfil seleccionado
            string nombrePermiso = comboBox1.SelectedItem?.ToString(); // Obtiene el permiso seleccionado

            // Crea el nodo base con el nombre especificado y asocia el componente como su "Tag"
            TreeNode node = new TreeNode(nombre)
            {
                Tag = componente
            };

            // Verifica si el componente tiene hijos
            if (componente.ObtenerHijos() != null)
            {
                // Recorre cada hijo del componente y los agrega al nodo actual
                foreach (var hijo in componente.ObtenerHijos())
                {
                    // Define el nombre del hijo basado en el tipo de componente
                    string nombreHijo = hijo is Servicios.Perfil ? nombrePerfil : nombrePermiso;
                    node.Nodes.Add(CrearNodo3(hijo, nombreHijo)); // Llama recursivamente al método para agregar los hijos
                }
            }

            return node; // Retorna el nodo creado
        }
        private void CPerfil_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
        }

        private void cargarcb()
        {
            textBox1.Clear();
            comboBox3.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            foreach (string L in bllp.cargarCBPerfil())
            {
                comboBox3.Items.Add(L);
            }
            foreach (string L in bllp.cargarCBPermisos())
            {
                comboBox1.Items.Add(L);
            }
            //foreach (string L in BLLM.CargarCMBIDHijo())
            //{
            //    CmbIDHijo.Items.Add(L);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string permiso = "iconMenuItem2";
            bool is_perfil = true;
            BEPerfil permisAZO = new BEPerfil(nombre,permiso,is_perfil);
            Servicios.Resultado<BEPerfil> result = bllp.aggPerfil(permisAZO);
            cargarcb();
            MessageBox.Show("Se ha Agregado un PERFIL");
            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "Crear Perfil";
            int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha1 = DateTime.Now;
            int criticidad = 16;

            string actor;
            if (id_usuario1 == 3)
                actor = "ADMIN";
            else if (id_usuario1 == 2)
                actor = "EMPLEADO";
            else
                actor = "USUARIO";

            BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
            bllbita.crearEntidad(bitacorita);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode?.Tag is Componente componenteSeleccionado) // Verifica si el nodo seleccionado es un Componente
            {
                // Obtener el permiso seleccionado desde ComboBox1
                string permisoSeleccionado = comboBox1.SelectedItem?.ToString();

                // Crear una nueva instancia de Permiso para evitar que se modifiquen permisos existentes
                Servicios.Permisos nuevoPermiso = new Servicios.Permisos(permisoSeleccionado); // Crear un nuevo permiso con el nombre seleccionado

                // Verifica si el nodo seleccionado tiene hijos
                if (componenteSeleccionado.ObtenerHijos() != null && componenteSeleccionado.ObtenerHijos().Count > 0)
                {
                    // Agrega el nuevo permiso sin modificar los permisos anteriores
                    componenteSeleccionado.AgregarHijo(nuevoPermiso); // Se crea un nuevo hijo para el nodo seleccionado
                }
                else
                {
                    // Si el nodo no tiene hijos, simplemente agrega el permiso
                    componenteSeleccionado.AgregarHijo(nuevoPermiso);
                }

                // Reconstruye el árbol para reflejar los cambios
                treeView1.Nodes.Clear(); // Limpia los nodos actuales
                CargarTreeView3(); // Vuelve a cargar el árbol con los cambios realizados
            }
            else
            {
                MessageBox.Show("Seleccione un nodo para agregar el Permiso.");
            }

            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "Agregar Permiso";
            int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha1 = DateTime.Now;
            int criticidad = 17;

            string actor;
            if (id_usuario1 == 3)
                actor = "ADMIN";
            else if (id_usuario1 == 2)
                actor = "EMPLEADO";
            else
                actor = "USUARIO";

            BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
            bllbita.crearEntidad(bitacorita);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "Eliminar Perfil";
            int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
            DateTime fecha1 = DateTime.Now;
            int criticidad = 18;

            string actor;
            if (id_usuario1 == 3)
                actor = "ADMIN";
            else if (id_usuario1 == 2)
                actor = "EMPLEADO";
            else
                actor = "USUARIO";

            BEBitacora bitacorita = new BEBitacora(id_usuario1, operacion, fecha1, actor, criticidad);
            bllbita.crearEntidad(bitacorita);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //verif();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode?.Tag is Componente componenteSeleccionado) //selec nodo y mira que tipo de componente es,
            {                                                                   //si es un producto o caja.
                Servicios.Perfil newperfil = new Servicios.Perfil();
                componenteSeleccionado.AgregarHijo(newperfil); //agg caja a componente (Caja).
                treeView1.Nodes.Clear();
                CargarTreeView2();
            }
            else
            {
                MessageBox.Show("Seleccione un nodo para agregar el Perfil.");
            }
        }
    }
}
