namespace BE
{
    public class DestinoBE
    {
        public int IdDestino { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }
        public int DniDestinatario { get; set; }
        public string NombreDestinatario { get; set; }
        public string ApellidoDestinatario { get; set; }
        public string TelefonoDestinatario { get; set; }
        public string EmailDestinatario { get; set; }

        public DestinoBE(int idDestino, string direccion, string ciudad, string codigoPostal, string provincia, int dniDestinatario, string nombreDestinatario, string apellidoDestinatario, string telefonoDestinatario, string emailDestinatario)
        {
            IdDestino = idDestino;
            Direccion = direccion;
            Ciudad = ciudad;
            CodigoPostal = codigoPostal;
            Provincia = provincia;
            DniDestinatario = dniDestinatario;
            NombreDestinatario = nombreDestinatario;
            ApellidoDestinatario = apellidoDestinatario;
            TelefonoDestinatario = telefonoDestinatario;
            EmailDestinatario = emailDestinatario;
        }

        public DestinoBE() { }
    }
}