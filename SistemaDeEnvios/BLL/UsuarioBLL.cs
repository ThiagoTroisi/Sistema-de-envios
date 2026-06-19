using BE.Entidades;
using BLL.Otros;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioBLL
    {
        private UsuarioDAL dal = new UsuarioDAL();
        private EventoBLL eventobll = new EventoBLL();
        public DataTable ObtenerUsuarios(bool todos)
        {
            return dal.ObtenerUsuarios(todos);
        }
        public DataTable ObtenerRoles()
        {
            return dal.ObtenerRoles();
        }
        public ResultadoOperacion CambiarContraseña(int dni, string contraseñaactual, string contraseñanueva, string contraseñanueva2)
        {
            Usuario u = dal.ConsultaPorDNI(dni);
            bool verificacion = Encriptador.VerificarContraseña(contraseñaactual, u.Contraseña);

            if (!verificacion) return new ResultadoOperacion(false, "La contraseña actual es incorrecta");

            if (string.IsNullOrWhiteSpace(contraseñanueva)) return new ResultadoOperacion(false, "La contraseña nueva no puede estar vacía");

            if (contraseñanueva == contraseñaactual) return new ResultadoOperacion(false, "La contraseña nueva coincide con la actual");

            if (contraseñanueva != contraseñanueva2) return new ResultadoOperacion(false, "Las contraseñas no coinciden");

            string hash = Encriptador.Hash(contraseñanueva);

            dal.CambiarContraseña(dni, hash);

            return new ResultadoOperacion(true, "Contraseña actualizada correctamente");
        }
        public ResultadoOperacion AltaUsuario(int dni, string nombre, string apellido, string email, int rol)
        {
            try
            {
                if (dni < 0 || dni.ToString().Length < 7 || dni.ToString().Length > 8) throw new Exception("No se ingresó un DNI válido");
                if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("No se ingresó un nombre");
                if (string.IsNullOrWhiteSpace(apellido)) throw new Exception("No se ingresó un apellido");
                if (string.IsNullOrWhiteSpace(email)) throw new Exception("No se ingresó un email");
                if (!FormatoMail(email)) throw new Exception("El formato del email no es válido");
                if (dal.ConsultaPorDNI(dni) != null) return new ResultadoOperacion(false, "El DNI ya se encuentra registrado en el sistema");
                if (dal.ConsultaPorMail(email) != null) return new ResultadoOperacion(false, "El mail ya se encuentra registrado en el sistema");

                string contraseñadefault = dni.ToString() + apellido;
                string contraseña = Encriptador.Hash(contraseñadefault);

                dal.AltaUsuario(new Usuario(dni, nombre, apellido, email, contraseña, rol, false, true, 0));
                eventobll.RegistrarEvento("Usuarios", "Alta de usuario", 2);
                return new ResultadoOperacion(true, "Alta exitosa");
            }
            catch (Exception ex) { return new ResultadoOperacion(false, ex.Message); }
        }
        
        private bool FormatoMail(string email)
        {
            string formato = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool valido = Regex.IsMatch(email, formato);
            return valido;
        }

        public ResultadoOperacion ModificarUsuario(int dni, string nombre, string apellido, string email, int rol)
        {
            try
            {
                Usuario existente = dal.ConsultaPorDNI(dni);

                if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("No se ingresó un nombre");
                if (string.IsNullOrWhiteSpace(apellido)) throw new Exception("No se ingresó un apellido");
                if (string.IsNullOrWhiteSpace(email)) throw new Exception("No se ingresó un email");
                if (!FormatoMail(email)) throw new Exception("El formato del email no es válido");

                Usuario consulta = dal.ConsultaPorMail(email);

                if (consulta != null && consulta.DNI != dni) return new ResultadoOperacion(false, "El email ya está en uso");

                existente.Nombre = nombre;
                existente.Apellido = apellido;
                existente.Email = email;
                existente.IdRol = rol;

                dal.ModificarUsuario(existente);
                eventobll.RegistrarEvento("Usuarios", "Modificación de usuario", 3);
                return new ResultadoOperacion(true, "Usuario modificado correctamente");
            }
            catch (Exception ex) { return new ResultadoOperacion(false, ex.Message); }
        }
        public ResultadoOperacion ActivarUsuario(int dni)
        {
            Usuario u = dal.ConsultaPorDNI(dni);
            if (u.Estado == true) return new ResultadoOperacion(false, "El usuario ya se encuentra activado");
            dal.ActivarUsuario(dni);
            eventobll.RegistrarEvento("Usuarios", "Activación de usuario", 2);
            return new ResultadoOperacion(true, "El usuario fue activado");
        }
        public ResultadoOperacion DesactivarUsuario(int dni)
        {
            Usuario u = dal.ConsultaPorDNI(dni);
            if (u.Estado == false) return new ResultadoOperacion(false, "El usuario ya se encuentra desactivado");
            dal.DesactivarUsuario(dni);
            eventobll.RegistrarEvento("Usuarios", "Desactivación de usuario", 2);
            return new ResultadoOperacion(true, "El usuario fue desactivado");
        }
        public ResultadoOperacion DesbloquearUsuario(int dni)
        {
            Usuario u = dal.ConsultaPorDNI(dni);

            if (!u.Bloqueado) return new ResultadoOperacion(false, "El usuario no se encuentra bloqueado");

            dal.DesbloquearUsuario(dni);
            eventobll.RegistrarEvento("Usuarios", "Desbloqueo de usuario", 2);
            return new ResultadoOperacion(true, "Usuario desbloqueado correctamente");
        }
    }
}
