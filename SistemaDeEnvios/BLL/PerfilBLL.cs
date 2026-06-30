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
        private PerfilDAL perfildal = new PerfilDAL();

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
        public void CrearPerfil(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("Debe ingresar un nombre");
            if (ObtenerPorNombre(nombre) != null) throw new Exception("Ya existe un perfil con el nombre ingresado");
            perfildal.CrearPerfil(nombre);
        }

        public void EliminarPerfil(int idPerfil)
        {
            perfildal.EliminarPerfil(idPerfil);
        }
        public void ActualizarPerfil(Perfil perfil, List<int> permisos, List<int> familias)
        {
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
        }
    }
}
