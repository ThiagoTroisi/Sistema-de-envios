using BE.Entidades;
using BLL;
using Servicios.GestionIdiomas;
using Servicios.Sesión;
using SistemaDeEnviosGUI.Formularios.Administrador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI.Formularios
{
    public partial class MainForm : Form, IObserverIdioma
    {
        PerfilBLL perfilBLL = new PerfilBLL();
        public MainForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
            ActualizarIdioma();
        }
        private void ActualizarTreeView()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            // USUARIO
            TreeNode usuario = new TreeNode();
            usuario.Text = Traducciones.Traducir("node_usuario");
            usuario.Tag = "usuario";

            usuario.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_cambiar_password"), Tag = "CambiarPassword" });
            usuario.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_cambiar_idioma"), Tag = "CambiarIdioma" });

            treeView1.Nodes.Add(usuario);


            // ADMINISTRACIÓN
            TreeNode admin = new TreeNode();
            admin.Text = Traducciones.Traducir("node_administracion");
            admin.Tag = "administracion";

            admin.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_gestion_usuarios"), Tag = "GestionUsuarios" });
            admin.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_bitacora"), Tag = "Bitacora" });
            admin.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_gestion_perfiles"), Tag = "GestionPerfiles" });
            admin.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_gestion_backup"), Tag = "GestionBackup" });
            admin.Nodes.Add(new TreeNode() { Text = "Bitacora de cambios", Tag = "Bitacora de cambios" });
            admin.Nodes.Add(new TreeNode() { Text = "Reporte inteligente", Tag = "Reporte inteligente" });


            treeView1.Nodes.Add(admin);

            // RESPONSABILIDADES DE CADA USUARIO
            TreeNode resp = new TreeNode();
            resp.Text = "Responsabilidades";
            resp.Tag = "Responsabilidades";

            resp.Nodes.Add(new TreeNode() { Text = "Registro de clientes", Tag = "Registro de clientes" });
            resp.Nodes.Add(new TreeNode() { Text = "Registro de envíos", Tag = "Registro de envíos" });
            resp.Nodes.Add(new TreeNode() { Text = "Cobro de envíos", Tag = "Cobro de envíos" });
            resp.Nodes.Add(new TreeNode() { Text = "Gestión de facturas", Tag = "Gestión de facturas" });
            resp.Nodes.Add(new TreeNode() { Text = "Creación de rutas", Tag = "Creación de rutas" });
            resp.Nodes.Add(new TreeNode() { Text = "Asignación de rutas", Tag = "Asignación de rutas" });
            resp.Nodes.Add(new TreeNode() { Text = "Administración de rutas", Tag = "Administración de rutas" });


            treeView1.Nodes.Add(resp);

            treeView1.EndUpdate();

            AplicarPermisos();
        }
        private void AplicarPermisos()
        {
            Usuario usuario = SesionUsuario.GetInstancia().UsuarioActual;

            HashSet<string> permisos = perfilBLL.ObtenerPermisosEfectivos(usuario.IdPerfil);

            FiltrarNodos(treeView1.Nodes, permisos);
        }
        private void FiltrarNodos(TreeNodeCollection nodos, HashSet<string> permisos)
        {
            for (int i = nodos.Count - 1; i >= 0; i--)
            {
                TreeNode nodo = nodos[i];

                if (nodo.Nodes.Count > 0)
                {
                    FiltrarNodos(nodo.Nodes, permisos);

                    if (nodo.Nodes.Count == 0)
                    {
                        nodos.RemoveAt(i);
                    }
                }
                else
                {
                    string permiso = ObtenerNombrePermiso(nodo.Tag?.ToString());

                    if (permiso != null && !permisos.Contains(permiso))
                    {
                        nodos.RemoveAt(i);
                    }
                }
            }
        }
        private string ObtenerNombrePermiso(string tag)
        {
            switch (tag)
            {
                case "CambiarPassword": return "Cambiar contraseña";
                case "CambiarIdioma": return "Cambiar idioma";
                case "GestionUsuarios": return "Gestión de usuarios";
                case "Bitacora": return "Bitácora de eventos";
                case "GestionPerfiles": return "Gestión de perfiles";
                case "GestionBackup": return "Gestión de backup";
                case "Registro de clientes": return "Registro de clientes";
                case "Registro de envíos": return "Registro de envíos";
                case "Cobro de envíos": return "Cobro de envíos";
                case "Gestión de facturas": return "Gestión de facturas";
                case "Creación de rutas": return "Creación de rutas";
                case "Asignación de rutas": return "Asignación de rutas";
                case "Administración de rutas": return "Administración de rutas";
                case "Bitacora de cambios": return "Bitacora de cambios";
                case "Reporte inteligente": return "Reporte inteligente";
                default: return null;
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Beige;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null) return;
            switch (e.Node.Tag?.ToString())
            {
                case "CambiarPassword":
                    new CambioContraseñaForm().ShowDialog();
                    break;

                case "CambiarIdioma":
                    new CambiarIdiomaForm().ShowDialog();
                    break;

                case "GestionUsuarios":
                    new GestionUsuariosForm().ShowDialog();
                    break;

                case "Bitacora":
                    new BitacoraDeEventosForm().ShowDialog();
                    break;

                case "GestionPerfiles":
                    new PerfilesForm().ShowDialog();
                    ActualizarTreeView();
                    break;

                case "GestionBackup":
                    new BackupForm().ShowDialog();
                    break;

                case "Registro de clientes":
                    new GestionClientesForm().ShowDialog();
                    break;

                case "Registro de envíos":
                    new GestionEnviosForm().ShowDialog();
                    break;

                case "Cobro de envíos":
                    new CobroEnviosForm().ShowDialog();
                    break;

                case "Gestión de facturas":
                    new GestionFacturasForm().ShowDialog();
                    break;

                case "Creación de rutas":
                    new CreacionRutasForm().ShowDialog();
                    break;

                case "Asignación de rutas":
                    new AsignacionRutaForm().ShowDialog();
                    break;

                case "Administración de rutas":
                    new RepartidorForm().ShowDialog();
                    break;

                case "Bitacora de cambios":
                    new BitacoraCambiosForm().ShowDialog();
                    break;

                case "Reporte inteligente":
                    new ReporteInteligenteForm().ShowDialog();
                    break;
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelogin_Click(object sender, EventArgs e)
        {
            new LoginForm(true).ShowDialog();
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("MenuMain");
            btnCerrarSesion.Text = Traducciones.Traducir("CerrarSesion");
            btnRelogin.Text = Traducciones.Traducir("Relogin");
            ActualizarTreeView();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            MessageBox.Show(Traducciones.Traducir("logout_exitoso"));
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            try
            {
                new AyudaForm("Ayuda - Menú principal", "MainForm.txt").ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir la ayuda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
