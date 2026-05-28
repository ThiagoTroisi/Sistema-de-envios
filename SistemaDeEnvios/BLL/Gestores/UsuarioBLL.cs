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

            dal.AltaUsuario(new Usuario(dni, nombre, apellido, email, contraseña, rol, false, true, 0));
            return new ResultadoOperacion(true, "Alta exitosa");
        }
        public ResultadoOperacion ModificarUsuario(int dni, string nombre, string apellido, string email, int rol)
        {

        }
        public ResultadoOperacion ModificarUsuario(int dni, string nombre, string apellido, string email, int rol)
        {
            Usuario existente = dal.ConsultaPorDNI(dni);
            Usuario consulta = dal.ConsultaPorMail(email);

            if (consulta != null && consulta.DNI != dni) return new ResultadoOperacion(false, "El email ya está en uso");

            existente.Nombre = nombre;
            existente.Apellido = apellido;
            existente.Email = email;
            existente.IdRol = rol;

            dal.ModificarUsuario(existente);

            return new ResultadoOperacion(true, "Usuario modificado correctamente");
        }
        public ResultadoOperacion CambiarEstadoUsuario(int dni, bool estado)
        {
            Usuario u = dal.ConsultaPorDNI(dni);

            dal.ActivarDesactivarUsuario(dni, estado);

            string mensaje = estado ? "El usuario fue activado" : "El usuario fue desactivado";

            return new ResultadoOperacion(true, mensaje);
        }
        public ResultadoOperacion DesbloquearUsuario(int dni)
        {
            Usuario u = dal.ConsultaPorDNI(dni);

            if (!u.Bloqueado)
                return new ResultadoOperacion(false, "El usuario no se encuentra bloqueado");

            dal.DesbloquearUsuario(dni);

            return new ResultadoOperacion(true, "Usuario desbloqueado correctamente");
        }
    }
}
