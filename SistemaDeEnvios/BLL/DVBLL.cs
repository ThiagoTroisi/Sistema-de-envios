using DAL;

namespace BLL
{
    public class DVBLL
    {
        DVDAL dal = new DVDAL();

        public string CalcularHash(string texto)
        {
            return dal.CalcularHash(texto);
        }
        public string ObtenerCadenaRegistro(string tabla, string columnaPK, object valorPK)
        {
            return dal.ObtenerCadenaRegistro(tabla, columnaPK, valorPK);
        }
        public void ActualizarDVH(string tabla, string columnaPK, object valorPK)
        {
            dal.ActualizarDVH(tabla, columnaPK, valorPK);
        }
        public void ActualizarDVV(string tabla)
        {
            dal.ActualizarDVV(tabla);
        }
    }
}