namespace BE
{
    public class PaqueteBE
    {
        public int IdPaquete { get; set; }
        public string Descripcion { get; set; }
        public decimal Peso { get; set; }
        public decimal Alto { get; set; }
        public decimal Ancho { get; set; }
        public decimal Largo { get; set; }

        public PaqueteBE(int idPaquete, string descripcion, decimal peso, decimal alto, decimal ancho, decimal largo)
        {
            IdPaquete = idPaquete;
            Descripcion = descripcion;
            Peso = peso;
            Alto = alto;
            Ancho = ancho;
            Largo = largo;
        }

        public PaqueteBE() { }
    }
}