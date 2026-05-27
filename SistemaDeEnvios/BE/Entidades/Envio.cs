using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class Envio
    {
        public int IdEnvio { get; set; }
        public string CodigoSeguimiento { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public decimal Peso { get; set; }
        public string Dimensiones { get; set; }
        public decimal Costo { get; set; }
        public string Estado { get; set; }
        public Envio(int idEnvio, string codigoSeguimiento, string direccionOrigen, string direccionDestino, decimal peso, string dimensiones, decimal costo, string estado)
        {
            IdEnvio = idEnvio;
            CodigoSeguimiento = codigoSeguimiento;
            DireccionOrigen = direccionOrigen;
            DireccionDestino = direccionDestino;
            Peso = peso;
            Dimensiones = dimensiones;
            Costo = costo;
            Estado = estado;
        }
    }
}
