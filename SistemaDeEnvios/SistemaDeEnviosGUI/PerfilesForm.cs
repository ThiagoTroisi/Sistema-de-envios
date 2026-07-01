using BLL;
using DAL;
using Servicios.Perfiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI
{
    public partial class PerfilesForm : Form
    {
        PerfilBLL perfilBLL = new PerfilBLL();
        FamiliaBLL familiaBLL = new FamiliaBLL();
        PermisoBLL permisoBLL = new PermisoBLL();
        public PerfilesForm()
        {
            InitializeComponent();
            radioButtonPerfiles.Checked = true;
            CargarModo();
        }
        private void CargarModo()
        {
            if (radioButtonPerfiles.Checked)
            {
                listBoxTipo.DataSource = perfilBLL.ObtenerPerfiles();
                listBoxTipo.DisplayMember = "nombre";
                listBoxTipo.ValueMember = "id_perfil";
            }
            else
            {
                listBoxTipo.DataSource = familiaBLL.ObtenerFamilias();
                listBoxTipo.DisplayMember = "nombre";
                listBoxTipo.ValueMember = "id_familia";
            }

            checkedListBoxPermisos.DataSource = permisoBLL.ObtenerPermisos();
            checkedListBoxPermisos.DisplayMember = "nombre";
            checkedListBoxPermisos.ValueMember = "id_permiso";

            checkedListBoxFamilias.DataSource = familiaBLL.ObtenerFamilias();
            checkedListBoxFamilias.DisplayMember = "nombre";
            checkedListBoxFamilias.ValueMember = "id_familia";

            textBoxNombre.Clear();
            treeViewOrganizacion.Nodes.Clear();
            listBoxTipo_SelectedIndexChanged(null, null);
        }

        private void radioButtonPerfiles_CheckedChanged(object sender, EventArgs e)
        {
            CargarModo();
        }

        private void radioButtonFamilias_CheckedChanged(object sender, EventArgs e)
        {
            CargarModo();
        }

        private void LimpiarChecks()
        {
            for (int i = 0; i < checkedListBoxPermisos.Items.Count; i++)
            {
                checkedListBoxPermisos.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBoxFamilias.Items.Count; i++)
            {
                checkedListBoxFamilias.SetItemChecked(i, false);
            }
        }

        private void CargarPerfilSeleccionado()
        {
            DataRowView fila = (DataRowView)listBoxTipo.SelectedItem;
            int idPerfil = Convert.ToInt32(fila["id_perfil"]);
            textBoxNombre.Text = fila["nombre"].ToString();
            MarcarPermisosPerfil(idPerfil);
            MarcarFamiliasPerfil(idPerfil);
            CargarTreePerfil(idPerfil);
        }

        private void MarcarPermisosPerfil(int idPerfil)
        {
            List<int> permisos = perfilBLL.ObtenerPermisosPerfil(idPerfil);
            for (int i = 0; i < checkedListBoxPermisos.Items.Count; i++)
            {
                DataRowView fila = (DataRowView)checkedListBoxPermisos.Items[i];
                int id = Convert.ToInt32(fila["id_permiso"]);
                checkedListBoxPermisos.SetItemChecked(i, permisos.Contains(id));
            }
        }

        private void MarcarFamiliasPerfil(int idPerfil)
        {
            List<int> familias = perfilBLL.ObtenerFamiliasPerfil(idPerfil);
            for (int i = 0; i < checkedListBoxFamilias.Items.Count; i++)
            {
                DataRowView fila = (DataRowView)checkedListBoxFamilias.Items[i];

                int id = Convert.ToInt32(fila["id_familia"]);

                checkedListBoxFamilias.SetItemChecked(i, familias.Contains(id));
            }
        }

        private void CargarFamiliaSeleccionada()
        {
            DataRowView fila = (DataRowView)listBoxTipo.SelectedItem;
            int idFamilia = Convert.ToInt32(fila["id_familia"]);
            textBoxNombre.Text = fila["nombre"].ToString();
            MarcarPermisosFamilia(idFamilia);
            MarcarSubfamilias(idFamilia);
            CargarTreeFamilia(idFamilia);
        }

        private void MarcarPermisosFamilia(int idFamilia)
        {
            List<int> permisos = familiaBLL.ObtenerPermisosFamilia(idFamilia);
            for (int i = 0; i < checkedListBoxPermisos.Items.Count; i++)
            {
                DataRowView fila = (DataRowView)checkedListBoxPermisos.Items[i];
                int id = Convert.ToInt32(fila["id_permiso"]);
                checkedListBoxPermisos.SetItemChecked(i, permisos.Contains(id));
            }
        }

        private void MarcarSubfamilias(int idFamilia)
        {
            List<int> familias = familiaBLL.ObtenerSubfamilias(idFamilia);
            for (int i = 0; i < checkedListBoxFamilias.Items.Count; i++)
            {
                DataRowView fila = (DataRowView)checkedListBoxFamilias.Items[i];

                int id = Convert.ToInt32(fila["id_familia"]);

                checkedListBoxFamilias.SetItemChecked(i, familias.Contains(id));
            }
        }

        private void listBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTipo.SelectedItem == null)
                return;

            LimpiarChecks();

            if (radioButtonPerfiles.Checked)
            {
                CargarPerfilSeleccionado();
            }
            else
            {
                CargarFamiliaSeleccionada();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> permisos = ObtenerPermisosSeleccionados();
                if (radioButtonPerfiles.Checked)
                {
                    perfilBLL.CrearPerfil(textBoxNombre.Text, permisos);
                }
                else
                {
                    familiaBLL.CrearFamilia(textBoxNombre.Text, permisos);
                }
                textBoxNombre.Clear();
                CargarModo();
                MessageBox.Show("Alta realizada");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxTipo.SelectedIndex == -1) throw new Exception("Seleccione el registro a eliminar");
                DialogResult r = MessageBox.Show("¿Seguro/a que desea eliminar el registro seleccionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r != DialogResult.Yes) return;

                int id = Convert.ToInt32(listBoxTipo.SelectedValue);
                if (radioButtonPerfiles.Checked)
                {
                    perfilBLL.EliminarPerfil(id);
                }
                else
                {
                    familiaBLL.EliminarFamilia(id);
                }
                textBoxNombre.Clear();
                treeViewOrganizacion.Nodes.Clear();
                CargarModo();
                MessageBox.Show("Eliminación realizada");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxTipo.SelectedItem == null) throw new Exception("Seleccione un elemento");

                List<int> permisos = ObtenerPermisosSeleccionados();
                List<int> familias = ObtenerFamiliasSeleccionadas();

                if (radioButtonFamilias.Checked)
                {
                    DataRowView fila = (DataRowView)listBoxTipo.SelectedItem;

                    Familia familia = new Familia();

                    familia.IdFamilia = Convert.ToInt32(fila["id_familia"]);
                    familia.Nombre = textBoxNombre.Text;

                    familiaBLL.ActualizarFamilia(familia, permisos, familias);
                }
                else
                {
                    DataRowView fila = (DataRowView)listBoxTipo.SelectedItem;

                    Perfil perfil = new Perfil();

                    perfil.IdPerfil = Convert.ToInt32(fila["id_perfil"]);
                    perfil.Nombre = textBoxNombre.Text.Trim();

                    perfilBLL.ActualizarPerfil(perfil, permisos, familias);
                }

                CargarModo();

                MessageBox.Show("Actualización realizada");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<int> ObtenerPermisosSeleccionados()
        {
            List<int> permisos = new List<int>();

            foreach (DataRowView item in checkedListBoxPermisos.CheckedItems)
            {
                permisos.Add(Convert.ToInt32(item["id_permiso"]));
            }

            return permisos;
        }

        private List<int> ObtenerFamiliasSeleccionadas()
        {
            List<int> familias = new List<int>();

            foreach (DataRowView item in checkedListBoxFamilias.CheckedItems)
            {
                familias.Add(Convert.ToInt32(item["id_familia"]));
            }

            return familias;
        }

        // COMPOSITE PARA CARGAR TREEVIEW

        private void CargarTreePerfil(int idPerfil)
        {
            treeViewOrganizacion.Nodes.Clear();

            Perfil perfil = perfilBLL.ObtenerComposite(idPerfil);

            TreeNode raiz = new TreeNode(perfil.Nombre);
            raiz.Tag = perfil;

            treeViewOrganizacion.Nodes.Add(raiz);

            foreach (Componente componente in perfil.Componentes)
            {
                AgregarNodo(raiz, componente);
            }

            raiz.ExpandAll();
        }

        private void CargarTreeFamilia(int idFamilia)
        {
            treeViewOrganizacion.Nodes.Clear();

            Familia familia = familiaBLL.ObtenerComposite(idFamilia);

            TreeNode raiz = new TreeNode(familia.Nombre);
            raiz.Tag = familia;

            treeViewOrganizacion.Nodes.Add(raiz);

            foreach (Componente componente in familia.ObtenerHijos())
            {
                AgregarNodo(raiz, componente);
            }

            raiz.ExpandAll();
        }

        private void AgregarNodo(TreeNode padre, Componente componente)
        {
            TreeNode nodo = new TreeNode(componente.Nombre);
            nodo.Tag = componente;

            if (componente is Permiso)
            {
                nodo.ForeColor = Color.Blue;
            }
            else if (componente is Familia familia)
            {
                nodo.ForeColor = Color.DarkOrange;

                foreach (Componente hijo in familia.ObtenerHijos())
                {
                    AgregarNodo(nodo, hijo);
                }
            }

            padre.Nodes.Add(nodo);
        }






        /*
        private void CargarTreePerfil(int idPerfil)
        {
            treeViewOrganizacion.Nodes.Clear();

            Perfil perfil = perfilBLL.ObtenerPorId(idPerfil);

            TreeNode nodoPerfil = new TreeNode(perfil.Nombre);
            nodoPerfil.Tag = perfil;

            treeViewOrganizacion.Nodes.Add(nodoPerfil);

            CargarPermisosPerfil(nodoPerfil, idPerfil);
            CargarFamiliasPerfil(nodoPerfil, idPerfil);

            nodoPerfil.Expand();
        }

        private void CargarPermisosPerfil(TreeNode nodoPerfil, int idPerfil)
        {
            List<int> permisos = perfilBLL.ObtenerPermisosPerfil(idPerfil);

            foreach (int idPermiso in permisos)
            {
                Permiso p = permisoBLL.ObtenerPorId(idPermiso);

                TreeNode nodo = new TreeNode(p.Nombre);
                nodo.Tag = p;

                nodo.ForeColor = Color.Blue;

                nodoPerfil.Nodes.Add(nodo);
            }
        }

        private void CargarFamiliasPerfil(TreeNode nodoPerfil, int idPerfil)
        {
            List<int> familias = perfilBLL.ObtenerFamiliasPerfil(idPerfil);

            foreach (int idFamilia in familias)
            {
                Familia f = familiaBLL.ObtenerPorId(idFamilia);

                if (f == null) continue;
                TreeNode nodoFam = new TreeNode(f.Nombre);
                nodoFam.Tag = f;

                nodoFam.ForeColor = Color.DarkOrange;

                CargarFamiliaRecursiva(nodoFam, f.IdFamilia);

                nodoPerfil.Nodes.Add(nodoFam);
            }
        }

        private void CargarFamiliaRecursiva(TreeNode nodoPadre, int idFamilia)
        {
            List<int> permisos = familiaBLL.ObtenerPermisosFamilia(idFamilia);
            foreach (int idPermiso in permisos)
            {
                Permiso p = permisoBLL.ObtenerPorId(idPermiso);

                TreeNode nodoPermiso = new TreeNode(p.Nombre);
                nodoPermiso.Tag = p;

                nodoPermiso.ForeColor = Color.Blue;

                nodoPadre.Nodes.Add(nodoPermiso);
            }

            List<int> subfamilias = familiaBLL.ObtenerSubfamilias(idFamilia);
            foreach (int idSub in subfamilias)
            {
                Familia sub = familiaBLL.ObtenerPorId(idSub);

                if (sub == null) break;

                TreeNode nodoSub = new TreeNode(sub.Nombre);
                nodoSub.Tag = sub;

                nodoSub.ForeColor = Color.DarkOrange;

                CargarFamiliaRecursiva(nodoSub, sub.IdFamilia);

                nodoPadre.Nodes.Add(nodoSub);
            }
        }

        private void CargarTreeFamilia(int idFamilia)
        {
            treeViewOrganizacion.Nodes.Clear();

            Familia familia = familiaBLL.ObtenerPorId(idFamilia);

            TreeNode nodoFamilia = new TreeNode(familia.Nombre);
            nodoFamilia.Tag = familia;

            treeViewOrganizacion.Nodes.Add(nodoFamilia);

            CargarFamiliaRecursiva(nodoFamilia, idFamilia);

            nodoFamilia.Expand();
        }
        */
    }
}
