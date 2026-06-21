using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Servicios.GestionIdiomas;

namespace Servicios
{
    public class GeneradorPDF
    {
        public static void GenerarBitacora(List<string[]> filas)
        {
            string archivo = Path.Combine(Path.GetTempPath(), $"Bitacora_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());

            PdfWriter.GetInstance(doc, new FileStream(archivo, FileMode.Create));

            doc.Open();

            Paragraph titulo = new Paragraph(Traducciones.Traducir("Bitacora").ToUpper());

            titulo.Alignment = Element.ALIGN_CENTER;

            doc.Add(titulo);

            doc.Add(new Paragraph(" "));

            doc.Add(new Paragraph($"{Traducciones.Traducir("fecha_emision")}: {DateTime.Now:dd/MM/yyyy HH:mm:ss}"));

            doc.Add(new Paragraph(" "));

            PdfPTable tabla = new PdfPTable(6);
            tabla.WidthPercentage = 100;

            tabla.AddCell(Traducciones.Traducir("Email"));
            tabla.AddCell(Traducciones.Traducir("Fecha"));
            tabla.AddCell(Traducciones.Traducir("Hora"));
            tabla.AddCell(Traducciones.Traducir("Modulo"));
            tabla.AddCell(Traducciones.Traducir("Evento"));
            tabla.AddCell(Traducciones.Traducir("Criticidad"));

            foreach (string[] fila in filas)
            {
                foreach (string dato in fila)
                {
                    tabla.AddCell(dato ?? "");
                }
            }

            doc.Add(tabla);

            doc.Close();

            Process.Start(new ProcessStartInfo()
            {
                FileName = archivo,
                UseShellExecute = true
            });
        }
    }
}
