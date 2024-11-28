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
            INICIO = new Servicios.Perfil("BASE");
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
        private TreeNode CrearNodo(Componente componente, string nombre)
        {
            var node = new TreeNode(nombre) { Tag = componente };
            foreach (var hijo in componente.ObtenerHijos() ?? Enumerable.Empty<Componente>())
            {
                string nombreHijo = hijo is Servicios.Perfil ? "Perfil" : "Permiso";
                node.Nodes.Add(CrearNodo(hijo, nombreHijo));
            }
            return node;
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
            //perfiles
            List<BEPerfil> listaPerfiles = bllp.cargarCBPerfil();
            List<string> nombres = listaPerfiles.Select(p => p.nombre).ToList();
            List<Servicios.Perfil> listaDePerfiles = new List<Servicios.Perfil>();
            foreach (string nombre in nombres)
            {
                listaDePerfiles.Add(new Servicios.Perfil(nombre));
            }
            foreach (var perfil in listaDePerfiles)
            {
                comboBox3.Items.Add(perfil);
            }
            comboBox3.DisplayMember = "Nombre";
            //permisos
            List<BEPerfil> listaPERFILES = bllp.cargarCBPermisos();
            List<string> nombresperm = listaPERFILES.Select(p => p.nombre).ToList();
            List<Servicios.Perfil> listaDePermisos = new List<Servicios.Perfil>();
            foreach (string permiso in nombresperm)
            {
                listaDePermisos.Add(new Servicios.Perfil(permiso));
            }
            foreach (var permisos in listaDePermisos)
            {
                comboBox1.Items.Add(permisos);
            }
            comboBox1.DisplayMember = "Nombre";
        }

        private void button1_Click(object sender, EventArgs e) //agregar perfil
        {
            string nombre = textBox1.Text;
            string permiso = "iconMenuItem2";
            bool is_perfil = true;
            BEPerfil permisAZO = new BEPerfil(nombre,permiso,is_perfil);
            Servicios.Resultado<BEPerfil> result = bllp.aggPerfil(permisAZO);
            cargarcb();
            MessageBox.Show("Se ha Agregado un PERFIL");
            BLLBitacora bllbita = new BLLBitacora();
            string operacion = "Agregado Nuevo Perfil";
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

        private void button3_Click(object sender, EventArgs e)  //agregar Permisos
        {
            if (treeView1.SelectedNode?.Tag is Servicios.Perfil perfilSeleccionado)
            {
                if (treeView1.SelectedNode != treeView1.Nodes[0]) 
                {
                    if (comboBox1.SelectedItem is Servicios.Perfil permisoSeleccionado)
                    {
                        string permisoNombre = permisoSeleccionado.Nombre; 
                        Servicios.Permisos nuevoPermiso = new Servicios.Permisos(permisoNombre);
                        perfilSeleccionado.AgregarHijo(nuevoPermiso);
                        TreeNode nodoPermiso = CrearNodo(nuevoPermiso, nuevoPermiso.Nombre);
                        treeView1.SelectedNode.Nodes.Add(nodoPermiso);
                        treeView1.SelectedNode.Expand();
                        MessageBox.Show($"Se ha agregado el permiso: {permisoSeleccionado.Nombre} al perfil: {perfilSeleccionado.Nombre}.");



                        BLLBitacora bllbita = new BLLBitacora();
                        string operacion = "Agregado Permiso a Perfil";
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
                    else
                    {
                        MessageBox.Show("Seleccione un permiso válido para agregar.", "Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("No se puede agregar un permiso al nodo raíz.", "Error", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un perfil para agregar el permiso.", "Error", MessageBoxButtons.OK);
            }
        }


        private void button2_Click(object sender, EventArgs e) //eliminar
        {
            TreeNode nodoSeleccionado = treeView1.SelectedNode;
            if (nodoSeleccionado != null)
            {
                if (nodoSeleccionado.Tag is Servicios.Perfil)
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este perfil y toda su rama?",
                                                         "Confirmar eliminación",
                                                         MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        nodoSeleccionado.Remove();
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
                }
                else if (nodoSeleccionado == treeView1.Nodes[0]) // Verificar si es el nodo raíz
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar TODO y dejar solo el nodo raíz?",
                                                         "Confirmar eliminación",
                                                         MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        treeView1.Nodes.Clear();
                        treeView1.Nodes.Add(CrearNodo(INICIO, "BASE"));
                        nodoSeleccionado.Remove();
                        BLLBitacora bllbita = new BLLBitacora();
                        string operacion = "Eliminar PERFILES";
                        int id_usuario1 = SessionManager.ObtenerInstancia().IdUsuarioActual;
                        DateTime fecha1 = DateTime.Now;
                        int criticidad = 22;

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
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un nodo para eliminar.");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e) //seleciconar perfil
        {
            if (comboBox3.SelectedItem is Servicios.Perfil perfilSeleccionado)
            {
                TreeNode nodoSeleccionado = treeView1.SelectedNode;
                if (nodoSeleccionado != null && nodoSeleccionado.Tag is Servicios.Perfil perfilNodo)
                {
                    if (perfilNodo is Servicios.Perfil)
                    {
                        TreeNode nodoPerfil = CrearNodo(perfilSeleccionado, perfilSeleccionado.Nombre);
                        nodoSeleccionado.Nodes.Add(nodoPerfil);
                        nodoSeleccionado.Expand();
                        perfilNodo.AgregarHijo(perfilSeleccionado);
                        MessageBox.Show($"Se ha agregado el perfil: {perfilSeleccionado.Nombre} bajo el nodo {perfilNodo.Nombre}.");
                    }
                    else
                    {
                        MessageBox.Show("No puede agregar un perfil debajo de un nodo de tipo Permiso.");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un nodo de tipo perfil para agregar el nuevo perfil.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un perfil válido.");
            }
        }
    }
}
