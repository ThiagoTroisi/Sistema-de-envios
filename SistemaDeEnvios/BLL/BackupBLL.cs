using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BackupBLL
    {
        BackupDAL dal = new BackupDAL();

        public void HacerBackup(string path)
        {
            dal.CrearBackup(path);
        }

        public void Restaurar(string path)
        {
            dal.RestaurarBackup(path);
        }
    }
}
