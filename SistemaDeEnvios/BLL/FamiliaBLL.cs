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
    public class FamiliaBLL
    {
        private FamiliaDAL dal = new FamiliaDAL();
        private PermisoBLL permisoBLL = new PermisoBLL();

        public DataTable ObtenerFamilias()
        {
            return dal.ObtenerFamilias();
        }
        public Familia ObtenerPorNombre(string nombre)
        {
            return dal.ObtenerPorNombre(nombre);
        }
        public Familia ObtenerPorId(int idFamilia)
        {
            return dal.ObtenerPorId(idFamilia);
        }
        public List<int> ObtenerPermisosFamilia(int idFamilia)
        {
            return dal.ObtenerPermisosFamilia(idFamilia);
        }

        public List<int> ObtenerSubfamilias(int idFamilia)
        {
            return dal.ObtenerSubfamilias(idFamilia);
        }
        public void CrearFamilia(string nombre, List<int> permisos)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("Debe ingresar un nombre");
            if (ObtenerPorNombre(nombre) != null) throw new Exception("Ya existe una familia con el nombre ingresado");
            if (permisos.Count == 0) throw new Exception("La familia debe tener al menos un permiso");
            dal.CrearFamilia(nombre);

            Familia familia = ObtenerPorNombre(nombre);
            foreach (int idPermiso in permisos)
            {
                dal.AgregarPermiso(familia.IdFamilia, idPermiso);
            }
        }

        public void EliminarFamilia(int id)
        {
            if (dal.TienePadre(id)) throw new Exception("No se puede eliminar la familia porque está contenida dentro de otra");
            dal.EliminarFamilia(id);
        }
        public void ActualizarFamilia(Familia familia, List<int> permisos, List<int> subfamilias)
        {
            if (permisos.Count == 0) throw new Exception("La familia debe tener al menos un permiso");
            if (string.IsNullOrWhiteSpace(familia.Nombre)) throw new Exception("Debe ingresar un nombre");
            
            Familia existente = ObtenerPorNombre(familia.Nombre);
            if (existente != null && existente.IdFamilia != familia.IdFamilia) throw new Exception("Ya existe una familia activa con ese nombre");

            if (subfamilias.Contains(familia.IdFamilia)) throw new Exception("Una familia no puede contenerse a sí misma");

            dal.ActualizarNombre(familia.IdFamilia, familia.Nombre);

            dal.EliminarPermisos(familia.IdFamilia);
            foreach (int idPermiso in permisos)
            {
                dal.AgregarPermiso(familia.IdFamilia, idPermiso);
            }

            dal.EliminarSubfamilias(familia.IdFamilia);
            foreach (int idFamilia in subfamilias)
            {
                dal.AgregarSubfamilia(familia.IdFamilia, idFamilia);
            }
        }

        public Familia ObtenerComposite(int idFamilia)
        {
            Familia familia = ObtenerPorId(idFamilia);

            if (familia == null)
                return null;

            // Agrego permisos
            foreach (int idPermiso in ObtenerPermisosFamilia(idFamilia))
            {
                Permiso permiso = permisoBLL.ObtenerPorId(idPermiso);

                if (permiso != null) familia.Agregar(permiso);
            }

            // Agrego subfamilias
            foreach (int idSubfamilia in ObtenerSubfamilias(idFamilia))
            {
                Familia sub = ObtenerComposite(idSubfamilia);

                if (sub != null) familia.Agregar(sub);
            }

            return familia;
        }
    }
}
