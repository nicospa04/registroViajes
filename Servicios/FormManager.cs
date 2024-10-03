using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public class FormManager
{
    // Método para habilitar formularios según los permisos
    public void HabilitarMenusPorPermisos(ToolStripMenuItem menuItem, List<Permiso> permisos)
    {

        var permisoFormulario = permisos.FirstOrDefault(p => p.NombreFormulario == menuItem.Name);

        if(permisoFormulario == null)
        {
            menuItem.Enabled = false;
            
        }
        else
        {
            menuItem.Enabled = true;
        }


        // Iterar sobre los submenús de forma recursiva
        foreach (ToolStripMenuItem subMenuItem in menuItem.DropDownItems)
        {
            // Verificar si el submenú tiene permisos
            var permisoFormulario2 = permisos.FirstOrDefault(p => p.NombreFormulario == subMenuItem.Name);

            if (permisoFormulario2 != null)
            {
                // Si tiene permiso, habilitarlo
                subMenuItem.Enabled = true;
            }
            else
            {
                // Si no tiene permiso, desabilitarlo
                subMenuItem.Enabled = false;
            }

            // Llamar recursivamente si tiene submenús
            //if (subMenuItem.HasDropDownItems)
            //{
            //    HabilitarMenusPorPermisos(subMenuItem, permisos);
            //}
        }
    }

    //public void HabilitarFormulariosRecursivos(Form form, List<Permiso> permisos)
    //{
    //    // Iterar sobre los controles dentro del MDI Parent
    //    foreach (Control control in form.Controls)
    //    {
    //        if (control is MdiClient) // Solo MDI contenedor
    //        {
    //            foreach (Form mdiChild in form.MdiChildren)
    //            {
    //                var permisoFormulario = permisos.FirstOrDefault(p => p.NombreFormulario == mdiChild.GetType().Name);

    //                if (permisoFormulario != null)
    //                {
    //                    // Si tiene permiso, mostrar el formulario
    //                    mdiChild.Visible = true;
    //                }
    //                else
    //                {
    //                    // Si no tiene permiso, ocultar el formulario
    //                    mdiChild.Visible = false;
    //                }

    //                // Recursión para formularios anidados
    //                HabilitarFormulariosRecursivos(mdiChild, permisos);
    //            }
    //        }
    //    }
    //}

}
