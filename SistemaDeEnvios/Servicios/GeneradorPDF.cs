using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Servicios.GestionIdiomas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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

        public static void GenerarFactura(FacturaBE factura, EnvioBE envio, DestinoBE destino, bool abrir = true)
        {
            if (factura == null)
                throw new Exception("Los datos de la factura son obligatorios.");

            if (envio == null)
                throw new Exception("Los datos del envío son obligatorios.");

            if (destino == null)
                throw new Exception("Los datos del destino son obligatorios.");

            string carpeta = Path.Combine(AppContext.BaseDirectory, "Facturas");
            Directory.CreateDirectory(carpeta);

            string numeroFactura = $"FAC-{factura.IdFactura:D6}";
            string archivo = Path.Combine(carpeta, numeroFactura + ".pdf");

            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream(archivo, FileMode.Create));

            doc.Open();

            Font titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            Font subtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Font normal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            Paragraph encabezado = new Paragraph("SISTEMA DE GESTIÓN DE ENVÍOS", titulo);
            encabezado.Alignment = Element.ALIGN_CENTER;
            doc.Add(encabezado);

            doc.Add(new Paragraph(" "));

            Paragraph tituloFactura = new Paragraph("FACTURA", subtitulo);
            tituloFactura.Alignment = Element.ALIGN_CENTER;
            doc.Add(tituloFactura);

            Paragraph numero = new Paragraph(numeroFactura, subtitulo);
            numero.Alignment = Element.ALIGN_CENTER;
            doc.Add(numero);

            doc.Add(new Paragraph(" "));

            doc.Add(new Paragraph($"Fecha de emisión: {factura.Fecha:dd/MM/yyyy HH:mm:ss}", normal));
            doc.Add(new Paragraph($"DNI del cliente: {factura.DniCliente}", normal));
            doc.Add(new Paragraph($"Código de seguimiento: {envio.CodigoSeguimiento}", normal));

            doc.Add(new Paragraph(" "));

            PdfPTable tablaDatos = new PdfPTable(2);
            tablaDatos.WidthPercentage = 100;

            tablaDatos.AddCell("Origen");
            tablaDatos.AddCell(envio.Origen);

            tablaDatos.AddCell("Destino");
            tablaDatos.AddCell($"{destino.Direccion}, {destino.Ciudad}, {destino.Provincia}");

            tablaDatos.AddCell("Código postal");
            tablaDatos.AddCell(destino.CodigoPostal);

            tablaDatos.AddCell("Medio de pago");
            tablaDatos.AddCell("Tarjeta");

            doc.Add(tablaDatos);

            doc.Add(new Paragraph(" "));

            PdfPTable tablaImporte = new PdfPTable(2);
            tablaImporte.WidthPercentage = 100;

            tablaImporte.AddCell("Concepto");
            tablaImporte.AddCell("Importe");

            tablaImporte.AddCell("Servicio de envío");
            tablaImporte.AddCell(factura.Importe.ToString("C2"));

            doc.Add(tablaImporte);

            doc.Add(new Paragraph(" "));

            Paragraph total = new Paragraph($"TOTAL: {factura.Importe:C2}", subtitulo);
            total.Alignment = Element.ALIGN_RIGHT;
            doc.Add(total);

            doc.Add(new Paragraph(" "));

            Paragraph pie = new Paragraph("Comprobante generado por el Sistema de Gestión de Envíos.", normal);
            pie.Alignment = Element.ALIGN_CENTER;
            doc.Add(pie);

            doc.Close();

            if (abrir)
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = archivo,
                    UseShellExecute = true
                });
            }
        }
    }
}
