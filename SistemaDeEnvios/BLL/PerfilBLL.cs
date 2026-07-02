using DAL;
using Servicios.Perfiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PerfilBLL
    {
        PerfilDAL perfildal = new PerfilDAL();
        FamiliaBLL familiaBLL = new FamiliaBLL();
        PermisoBLL permisoBLL = new PermisoBLL();
        DVBLL dvBLL = new DVBLL();

        public DataTable ObtenerPerfiles()
        {
            return perfildal.ObtenerPerfiles();
        }
        public Perfil ObtenerPorNombre(string nombre)
        {
            return perfildal.ObtenerPorNombre(nombre);
        }
        public Perfil ObtenerPorId(int idPerfil)
        {
            return perfildal.ObtenerPorId(idPerfil);
        }
        public List<int> ObtenerPermisosPerfil(int idPerfil)
        {
            return perfildal.ObtenerPermisosPerfil(idPerfil);
        }
        public List<int> ObtenerFamiliasPerfil(int idPerfil)
        {
            return perfildal.ObtenerFamiliasPerfil(idPerfil);
        }
        public void CrearPerfil(string nombre, List<int> permisos, List<int> familias)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("Debe ingresar un nombre");
            if (ObtenerPorNombre(nombre) != null) throw new Exception("Ya existe un perfil con el nombre ingresado");
            if (permisos.Count == 0 && familias.Count == 0) throw new Exception("El perfil debe tener al menos un permiso o familia");
            perfildal.CrearPerfil(nombre);

            Perfil perfil = ObtenerPorNombre(nombre);
            foreach (int idPermiso in permisos)
            {
                perfildal.AgregarPermiso(perfil.IdPerfil, idPermiso);
            }
            foreach (int idFamilia in familias)
            {
                perfildal.AgregarFamilia(perfil.IdPerfil, idFamilia);
            }
            dvBLL.ActualizarDVH("Perfil", "id_perfil", perfil.IdPerfil);
            dvBLL.ActualizarDVV("Perfil");

            dvBLL.ActualizarTodosLosDVH("Perfil_Permiso");

            dvBLL.ActualizarTodosLosDVH("Perfil_Familia");
        }
        public void EliminarPerfil(int idPerfil)
        {
            if (perfildal.ExisteUsuariosConPerfil(idPerfil)) throw new Exception("No se puede eliminar el perfil porque está asignado a usuarios");
            perfildal.EliminarPerfil(idPerfil);
            dvBLL.ActualizarDVH("Perfil", "id_perfil", idPerfil);
            dvBLL.ActualizarDVV("Perfil");
        }
        public void ActualizarPerfil(Perfil perfil, List<int> permisos, List<int> familias)
        {
            if (permisos.Count == 0 && familias.Count == 0) throw new Exception("El perfil debe tener al menos un permiso o familia");
            if (string.IsNullOrWhiteSpace(perfil.Nombre)) throw new Exception("Debe ingresar un nombre");

            Perfil existente = ObtenerPorNombre(perfil.Nombre);

            if (existente != null && existente.IdPerfil != perfil.IdPerfil) throw new Exception("Ya existe un perfil activo con ese nombre.");

            perfildal.ActualizarNombre(perfil.IdPerfil, perfil.Nombre);

            perfildal.EliminarPermisos(perfil.IdPerfil);

            foreach (int idPermiso in permisos)
            {
                perfildal.AgregarPermiso(perfil.IdPerfil, idPermiso);
            }

            perfildal.EliminarFamilias(perfil.IdPerfil);

            foreach (int idFamilia in familias)
            {
                perfildal.AgregarFamilia(perfil.IdPerfil, idFamilia);
            }
            
            dvBLL.ActualizarDVH("Perfil", "id_perfil", perfil.IdPerfil);
            dvBLL.ActualizarDVV("Perfil");

            dvBLL.ActualizarTodosLosDVH("Perfil_Permiso");

            dvBLL.ActualizarTodosLosDVH("Perfil_Familia");
        }

        public Perfil ObtenerComposite(int idPerfil)
        {
            Perfil perfil = ObtenerPorId(idPerfil);

            if (perfil == null) return null;

            foreach (int idPermiso in ObtenerPermisosPerfil(idPerfil))
            {
                Permiso permiso = permisoBLL.ObtenerPorId(idPermiso);

                if (permiso != null) perfil.Componentes.Add(permiso);
            }

            foreach (int idFamilia in ObtenerFamiliasPerfil(idPerfil))
            {
                Familia familia = familiaBLL.ObtenerComposite(idFamilia);

                if (familia != null) perfil.Componentes.Add(familia);
            }

            return perfil;
        }

        public HashSet<string> ObtenerPermisosEfectivos(int idPerfil)
        {
            Perfil perfil = ObtenerComposite(idPerfil);

            HashSet<string> permisos = new HashSet<string>();

            foreach (Componente c in perfil.Componentes)
            {
                AgregarPermisos(c, permisos);
            }

            return permisos;
        }

        private void AgregarPermisos(Componente componente, HashSet<string> permisos)
        {
            if (componente is Permiso permiso)
            {
                permisos.Add(permiso.Nombre);
                return;
            }

            if (componente is Familia familia)
            {
                foreach (Componente hijo in familia.ObtenerHijos())
                {
                    AgregarPermisos(hijo, permisos);
                }
            }
        }
    }
}
