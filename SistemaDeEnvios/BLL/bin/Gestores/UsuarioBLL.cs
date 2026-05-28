using BE.Entidades;
using BLL.Otros;
using DAL.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Gestores
{
    public class UsuarioBLL
    {
        private UsuarioDAL dal = new UsuarioDAL();
        public DataTable ObtenerUsuarios(bool todos)
        {
            return dal.ObtenerUsuarios(todos);
        }
        public ResultadoOperacion AltaUsuario(int dni, string nombre, string apellido, string email, int rol)
        {
            if (dal.ConsultaPorDNI(dni) != null)
            {
                return new ResultadoOperacion(false, "El DNI ya se encuentra registrado en el sistema");
            }
            if (dal.ConsultaPorMail(email) != null)
            {
                return new ResultadoOperacion(false, "El mail ya se encuentra registrado en el sistema");
            }
            string contraseñadefault = dni.ToString() + apellido;

            string contraseña = BCrypt.Net.BCrypt.HashPassword(contraseñadefault);

            dal.AltaUsuario(new Usuario(dni, nombre, apellido, email, contraseña, rol, false, true));
            return new ResultadoOperacion(true, "Alta exitosa");
        }
        public ResultadoOperacion ModificarUsuario(int dni, string nombre, string apellido, string email, int rol)
        {

        }
    }
}
