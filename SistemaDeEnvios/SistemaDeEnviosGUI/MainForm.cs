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
            

            treeView1.Nodes.Add(admin);


            // ENVÍO
            TreeNode envio = new TreeNode();
            envio.Text = Traducciones.Traducir("node_envio");
            envio.Tag = "envio";

            envio.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_solicitar_envio"), Tag = "SolicitarEnvio" });
            envio.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_registrar_envio"), Tag = "RegistrarEnvio" });
            envio.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_consultar_estado"), Tag = "ConsultarEstado" });
            envio.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_modificar_destino"), Tag = "ModificarDestino" });
            envio.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_cancelar_envio"), Tag = "CancelarEnvio" });

            treeView1.Nodes.Add(envio);


            // PAGO
            TreeNode pago = new TreeNode();
            pago.Text = Traducciones.Traducir("node_pago");
            pago.Tag = "pago";

            pago.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_registrar_pago"), Tag = "RegistrarPago" });
            pago.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_consultar_comprobantes"), Tag = "ConsultarComprobantes" });

            treeView1.Nodes.Add(pago);


            // LOGÍSTICA
            TreeNode logistica = new TreeNode();
            logistica.Text = Traducciones.Traducir("node_logistica");
            logistica.Tag = "logistica";

            logistica.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_generar_rutas"), Tag = "GenerarRutas" });
            logistica.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_asignar_rutas"), Tag = "AsignarRutas" });
            logistica.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_consultar_rutas"), Tag = "ConsultarRutas" });

            treeView1.Nodes.Add(logistica);


            // ENTREGA
            TreeNode entrega = new TreeNode();
            entrega.Text = Traducciones.Traducir("node_entrega");
            entrega.Tag = "entrega";

            entrega.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_registrar_entrega"), Tag = "RegistrarEntrega" });
            entrega.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_solicitar_devolucion"), Tag = "SolicitarDevolucion" });
            entrega.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_calificar_repartidor"), Tag = "CalificarRepartidor" });

            treeView1.Nodes.Add(entrega);


            // INCONVENIENTES
            TreeNode inc = new TreeNode();
            inc.Text = Traducciones.Traducir("node_inconveniente");
            inc.Tag = "inconveniente";

            inc.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_reportar_inconveniente"), Tag = "ReportarInconveniente" });

            treeView1.Nodes.Add(inc);


            // REPORTES
            TreeNode rep = new TreeNode();
            rep.Text = Traducciones.Traducir("node_reporte");
            rep.Tag = "reporte";

            rep.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_reporte_envios"), Tag = "ReporteEnvios" });
            rep.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_reporte_entregas"), Tag = "ReporteEntregas" });
            rep.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_reporte_inconvenientes"), Tag = "ReporteInconvenientes" });
            rep.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_reporte_repartidores"), Tag = "ReporteRepartidores" });
            rep.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_reporte_pagos"), Tag = "ReportePagos" });
            rep.Nodes.Add(new TreeNode() { Text = Traducciones.Traducir("node_reporte_analiticas"), Tag = "ReporteAnaliticas" });

            treeView1.Nodes.Add(rep);

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
                case "SolicitarEnvio": return "Solicitar envío";
                case "RegistrarEnvio": return "Registrar envío";
                case "ConsultarEstado": return "Consultar estado";
                case "ModificarDestino": return "Modificar destino";
                case "CancelarEnvio": return "Cancelar envío";
                case "RegistrarPago": return "Registrar pago";
                case "ConsultarComprobantes": return "Consultar comprobantes";
                case "GenerarRutas": return "Generar rutas";
                case "AsignarRutas": return "Asignar rutas";
                case "ConsultarRutas": return "Consultar rutas";
                case "RegistrarEntrega": return "Registrar entrega";
                case "SolicitarDevolucion": return "Solicitar devolución";
                case "CalificarRepartidor": return "Calificar repartidor";
                case "ReportarInconveniente": return "Reportar inconveniente";
                case "ReporteEnvios": return "Reporte de envíos";
                case "ReporteEntregas": return "Reporte de entregas";
                case "ReporteInconvenientes": return "Reporte de inconvenientes";
                case "ReporteRepartidores": return "Reporte de repartidores";
                case "ReportePagos": return "Reporte de pagos";
                case "ReporteAnaliticas": return "Reporte de analíticas operativas";
                default: return null;
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Beige;
        }

        // ACÁ HAY QUE PONER LO DE LOS PERFILES!
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
    }
}
