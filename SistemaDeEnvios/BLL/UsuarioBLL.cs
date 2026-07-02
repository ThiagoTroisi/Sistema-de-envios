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
        DVBLL dvBLL = new DVBLL();
        public DataTable ObtenerUsuarios(bool todos)
        {
            return dal.ObtenerUsuarios(todos);
        }
        public Usuario ConsultaPorMail(string mail)
        {
            return dal.ConsultaPorMail(mail);
        }
        public void IncrementarIntentos(int dni)
        {
            dal.IncrementarIntentos(dni);
            dvBLL.ActualizarDVH("Usuario", "dni", dni);
            dvBLL.ActualizarDVV("Usuario");
        }
        public void BloquearUsuario(int dni)
        {
            dal.BloquearUsuario(dni);
            dvBLL.ActualizarDVH("Usuario", "dni", dni);
            dvBLL.ActualizarDVV("Usuario");
        }
        public void ReiniciarIntentos(int dni)
        {
            dal.ReiniciarIntentos(dni);
            dvBLL.ActualizarDVH("Usuario", "dni", dni);
            dvBLL.ActualizarDVV("Usuario");
        }
        public void ActualizarIdioma(int dni, int idIdioma)
        {
            dal.ActualizarIdioma(dni, idIdioma);
            dvBLL.ActualizarDVH("Usuario", "dni", dni);
            dvBLL.ActualizarDVV("Usuario");
        }
        public ResultadoOperacion CambiarContraseña(int dni, string contraseñaactual, string contraseñanueva, string contraseñanueva2)
        {
            Usuario u = dal.ConsultaPorDNI(dni);
            bool verificacion = Encriptador.Verificar(contraseñaactual, u.Contraseña);

            if (!verificacion) return new ResultadoOperacion(false, "contraseña_actual_incorrecta");

            if (string.IsNullOrWhiteSpace(contraseñanueva)) return new ResultadoOperacion(false, "contraseña_vacia");

            if (contraseñanueva == contraseñaactual) return new ResultadoOperacion(false, "contraseña_igual");

            if (contraseñanueva != contraseñanueva2) return new ResultadoOperacion(false, "contraseñas_no_coinciden");

            string hash = Encriptador.Hash(contraseñanueva);

            dal.CambiarContraseña(dni, hash);
            dvBLL.ActualizarDVH("Usuario", "dni", dni);
            dvBLL.ActualizarDVV("Usuario");

            return new ResultadoOperacion(true, "contraseña_actualizada");
        }
        public ResultadoOperacion AltaUsuario(int dni, string nombre, string apellido, string email, int perfil)
        {
            try
            {
                if (dni < 0 || dni.ToString().Length < 7 || dni.ToString().Length > 8) throw new Exception("dni_invalido");
                if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("nombre_vacio");
                if (string.IsNullOrWhiteSpace(apellido)) throw new Exception("apellido_vacio");
                if (string.IsNullOrWhiteSpace(email)) throw new Exception("email_vacio");
                if (!FormatoMail(email)) throw new Exception("email_formato_invalido");
                if (dal.ConsultaPorDNI(dni) != null) throw new Exception ("dni_registrado");
                if (dal.ConsultaPorMail(email) != null) throw new Exception("email_registrado");

                string contraseñadefault = dni.ToString() + apellido;
                string contraseña = Encriptador.Hash(contraseñadefault);

                dal.AltaUsuario(new Usuario(dni, nombre, apellido, email, contraseña, perfil, false, true, 0, 1));
                dvBLL.ActualizarDVH("Usuario", "dni", dni);
                dvBLL.ActualizarDVV("Usuario");
                eventobll.RegistrarEvento("mod_usuarios", "ev_alta_usuario", 2);
                return new ResultadoOperacion(true, "alta_exitosa");
            }
            catch (Exception ex) { return new ResultadoOperacion(false, ex.Message); }
        }
        
        private bool FormatoMail(string email)
        {
            string formato = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool valido = Regex.IsMatch(email, formato);
            return valido;
        }

        public ResultadoOperacion ModificarUsuario(int dni, string nombre, string apellido, string email, int perfil)
        {
            try
            {
                Usuario existente = dal.ConsultaPorDNI(dni);

                if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("nombre_vacio");
                if (string.IsNullOrWhiteSpace(apellido)) throw new Exception("apellido_vacio");
                if (string.IsNullOrWhiteSpace(email)) throw new Exception("email_vacio");
                if (!FormatoMail(email)) throw new Exception("email_formato_invalido");

                Usuario consulta = dal.ConsultaPorMail(email);

                if (consulta != null && consulta.DNI != dni) throw new Exception("email_en_uso");

                existente.Nombre = nombre;
                existente.Apellido = apellido;
                existente.Email = email;
                existente.IdPerfil = perfil;

                dal.ModificarUsuario(existente);
                dvBLL.ActualizarDVH("Usuario", "dni", dni);
                dvBLL.ActualizarDVV("Usuario");
                eventobll.RegistrarEvento("mod_usuarios", "ev_modificacion_usuario", 3);
                return new ResultadoOperacion(true, "usuario_modificado");
            }
            catch (Exception ex) { return new ResultadoOperacion(false, ex.Message); }
        }
        public ResultadoOperacion ActivarUsuario(int dni)
        {
            Usuario u = dal.ConsultaPorDNI(dni);
            if (u.Estado == true) return new ResultadoOperacion(false, "usuario_ya_activado");
            dal.ActivarUsuario(dni);
            dvBLL.ActualizarDVH("Usuario", "dni", dni);
            dvBLL.ActualizarDVV("Usuario");
            eventobll.RegistrarEvento("mod_usuarios", "ev_activacion_usuario", 2);
            return new ResultadoOperacion(true, "usuario_activado");
        }
        public ResultadoOperacion DesactivarUsuario(int dni)
        {
            Usuario u = dal.ConsultaPorDNI(dni);
            if (u.Estado == false) return new ResultadoOperacion(false, "usuario_ya_desactivado");
            dal.DesactivarUsuario(dni);
            dvBLL.ActualizarDVH("Usuario", "dni", dni);
            dvBLL.ActualizarDVV("Usuario");
            eventobll.RegistrarEvento("mod_usuarios", "ev_desactivacion_usuario", 2);
            return new ResultadoOperacion(true, "usuario_desactivado");
        }
        public ResultadoOperacion DesbloquearUsuario(int dni)
        {
            Usuario u = dal.ConsultaPorDNI(dni);

            if (!u.Bloqueado) return new ResultadoOperacion(false, "usuario_no_bloqueado");

            dal.DesbloquearUsuario(dni);
            dvBLL.ActualizarDVH("Usuario", "dni", dni);
            dvBLL.ActualizarDVV("Usuario");
            eventobll.RegistrarEvento("mod_usuarios", "ev_desbloqueo_usuario", 2);
            return new ResultadoOperacion(true, "usuario_desbloqueado");
        }
    }
}
