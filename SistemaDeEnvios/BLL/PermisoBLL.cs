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
    public class PermisoBLL
    {
        private PermisoDAL dal = new PermisoDAL();

        public DataTable ObtenerPermisos()
        {
            return dal.ObtenerPermisos();
        }
        public Permiso ObtenerPorId(int idPermiso)
        {
            return dal.ObtenerPorId(idPermiso);
        }
    }
}
