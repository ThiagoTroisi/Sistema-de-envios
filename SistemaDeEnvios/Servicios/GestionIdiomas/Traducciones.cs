using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.GestionIdiomas
{
    public static class Traducciones
    {
        private static Dictionary<string, Dictionary<int, string>> textos = new Dictionary<string, Dictionary<int, string>>();
        public static string Traducir(string clave)
        {
            int idioma = GestorIdiomas.Instancia.IdiomaActual.Id;

            if (textos.ContainsKey(clave))
            {
                return textos[clave][idioma];
            }

            return clave;
        }
        static Traducciones()
        {
            #region LoginForm
            textos.Add("lblEmail", new Dictionary<int, string>
            {
                {1, "Email"},
                {2, "Email"},
                {3, "Email"}
            });
            textos.Add("lblContraseña", new Dictionary<int, string>
            {
                {1, "Contraseña"},
                {2, "Password"},
                {3, "Senha"}
            });
            textos.Add("lblIdioma", new Dictionary<int, string>
            {
                {1, "Idioma"},
                {2, "Language"},
                {3, "Idioma"}
            });
            textos.Add("btnLogin", new Dictionary<int, string>
            {
                {1, "Iniciar sesión"},
                {2, "Login"},
                {3, "Entrar"}
            });
            textos.Add("btnRegistrarse", new Dictionary<int, string>
            {
                {1, "Registrarse"},
                {2, "Register"},
                {3, "Registrar-se"}
            });
            textos.Add("btnSalir", new Dictionary<int, string>
            {
                {1, "Salir"},
                {2, "Exit"},
                {3, "Sair"}
            });
            #endregion

            #region LoginResultadoOperacion
            textos.Add("sesion iniciada", new Dictionary<int, string>
            {
                {1, "ERROR: Ya se encuentra una sesión iniciada."},
                {2, "ERROR: A session is already active."},
                {3, "ERRO: Já existe uma sessão ativa."}
            });
            textos.Add("email no encontrado", new Dictionary<int, string>
            {
                {1, "ERROR: Email no encontrado."},
                {2, "ERROR: Email not found."},
                {3, "ERRO: E-mail não encontrado."}
            });
            textos.Add("usuario inactivo", new Dictionary<int, string>
            {
                {1, "ERROR: Usuario inactivo."},
                {2, "ERROR: Inactive user."},
                {3, "ERRO: Usuário inativo."}
            });
            textos.Add("usuario bloqueado", new Dictionary<int, string>
            {
                {1, "ERROR: Usuario bloqueado."},
                {2, "ERROR: User blocked."},
                {3, "ERRO: Usuário bloqueado."}
            });
            textos.Add("usuario bloqueado por intentos", new Dictionary<int, string>
            {
                {1, "ERROR: Usuario bloqueado por demasiados intentos fallidos."},
                {2, "ERROR: User blocked due to too many failed attempts."},
                {3, "ERRO: Usuário bloqueado devido a muitas tentativas falhas."}
            });
            textos.Add("contraseña incorrecta", new Dictionary<int, string>
            {
                {1, "ERROR: Contraseña incorrecta. Intento {0} de 3"},
                {2, "ERROR: Incorrect password. Attempt {0} of 3"},
                {3, "ERRO: Senha incorreta. Tentativa {0} de 3"}
            });
            textos.Add("login exitoso", new Dictionary<int, string>
            {
                {1, "Inicio de sesión exitoso"},
                {2, "Login successful"},
                {3, "Login bem-sucedido"}
            });
            #endregion

            #region 

            #endregion

            #region 

            #endregion

            #region 

            #endregion

            textos.Add("", new Dictionary<int, string>
            {
                {1, ""},
                {2, ""},
                {3, ""}
            });
        }
    }
}
