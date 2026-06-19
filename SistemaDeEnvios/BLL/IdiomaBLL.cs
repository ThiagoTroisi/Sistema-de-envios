using DAL;
using Servicios.GestionIdiomas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IdiomaBLL
    {
        private IdiomaDAL dal = new IdiomaDAL();
        public List<Idioma> ObtenerIdiomas()
        {
            return dal.ObtenerIdiomas();
        }
    }
}
