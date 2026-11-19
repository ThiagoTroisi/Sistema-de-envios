using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class SerializadorGrillas
    {
        private static readonly string carpetaSerializaciones = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Serializaciones");

        public static void Serializar(DataTable datos, string nombreFormulario)
        {
            if (datos == null)
                throw new Exception("No hay datos para serializar.");

            if (string.IsNullOrWhiteSpace(nombreFormulario))
                throw new Exception("El nombre del formulario no es válido.");

            if (datos.Columns.Count == 0)
                throw new Exception("La grilla no contiene columnas para serializar.");

            Directory.CreateDirectory(carpetaSerializaciones);

            string rutaArchivo = Path.Combine(carpetaSerializaciones, nombreFormulario + ".xml");

            datos.TableName = nombreFormulario;
            datos.WriteXml(rutaArchivo, XmlWriteMode.WriteSchema);
        }

        public static DataTable Deserializar(string nombreFormulario)
        {
            if (string.IsNullOrWhiteSpace(nombreFormulario))
                throw new Exception("El nombre del formulario no es válido.");

            string rutaArchivo = Path.Combine(carpetaSerializaciones, nombreFormulario + ".xml");

            if (!File.Exists(rutaArchivo))
                throw new Exception("No existe una serialización guardada para este formulario.");

            DataTable datos = new DataTable();

            datos.ReadXml(rutaArchivo);

            return datos;
        }
    }
}
