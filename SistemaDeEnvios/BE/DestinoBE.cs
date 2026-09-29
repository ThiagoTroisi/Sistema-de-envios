namespace BE
{
    public class DestinoBE
    {
        public int IdDestino { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }

        public DestinoBE(int idDestino, string direccion, string ciudad, string codigoPostal, string provincia)
        {
            IdDestino = idDestino;
            Direccion = direccion;
            Ciudad = ciudad;
            CodigoPostal = codigoPostal;
            Provincia = provincia;
        }

        public DestinoBE() { }
    }
}