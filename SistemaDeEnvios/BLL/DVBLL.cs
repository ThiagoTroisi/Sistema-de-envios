using DAL;
using Servicios;

namespace BLL
{
    public class DVBLL
    {
        DVDAL dal = new DVDAL();
        private readonly List<string> tablas = new()
        {
            "Usuario",
            "Perfil",
            "Familia",
            "Permiso",
            "Evento",
            "Idioma",
            "Perfil_Permiso",
            "Perfil_Familia",
            "Familia_Permiso",
            "Familia_Familia"
        };
        
        public void ActualizarDVH(string tabla, string columnaPK, object valorPK)
        {
            dal.ActualizarDVH(tabla, columnaPK, valorPK);
        }
        public void ActualizarTodosLosDVH(string tabla)
        {
            dal.ActualizarTodosLosDVH(tabla);
        }
        public void ActualizarDVV(string tabla)
        {
            dal.ActualizarDVV(tabla);
        }

        public List<ErrorTabla> VerificarIntegridadSistema()
        {
            List<ErrorTabla> erroresSistema = new();

            foreach (string tabla in tablas)
            {
                ErrorTabla errorTabla = VerificarIntegridadTabla(tabla);

                if (errorTabla.CantidadErrores > 0)
                {
                    erroresSistema.Add(errorTabla);
                }
            }

            return erroresSistema;
        }

        private ErrorTabla VerificarIntegridadTabla(string tabla)
        {
            ErrorTabla resultado = new ErrorTabla();
            resultado.Tabla = tabla;

            List<Dictionary<string, object>> registros = dal.ObtenerClavesRegistros(tabla);

            foreach (Dictionary<string, object> claves in registros)
            {
                string cadena = dal.ObtenerCadenaRegistro(tabla, claves);

                string dvhCalculado = dal.CalcularHash(cadena);

                string dvhGuardado = dal.ObtenerDVHGuardado(tabla, claves);

                if (dvhCalculado != dvhGuardado)
                {
                    resultado.Errores.Add(new ErrorIntegridad()
                    {
                        Tipo = "DVH",
                        Registro = string.Join(" | ", claves.Values),
                        ValorGuardado = dvhGuardado,
                        ValorCalculado = dvhCalculado
                    });
                }
            }

            if (resultado.Errores.Count == 0)
            {
                string dvvCalculado = dal.CalcularDVV(tabla);

                string dvvGuardado = dal.ObtenerDVVGuardado(tabla);

                if (dvvCalculado != dvvGuardado)
                {
                    resultado.Errores.Add(new ErrorIntegridad()
                    {
                        Tipo = "DVV",
                        Registro = "-",
                        ValorGuardado = dvvGuardado,
                        ValorCalculado = dvvCalculado
                    });
                }
            }

            return resultado;
        }
    }
}