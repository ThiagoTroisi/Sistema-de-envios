using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Servicios.GestionIdiomas;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static string GenerarHojaRuta(DataTable datosAsignacion, DataTable envios, bool abrir = true)
        {
            if (datosAsignacion == null || datosAsignacion.Rows.Count == 0)
                throw new Exception("No se recibieron los datos de la asignación.");

            if (envios == null || envios.Rows.Count == 0)
                throw new Exception("No se recibieron los envíos de la ruta.");

            DataRow asignacion = datosAsignacion.Rows[0];

            int idAsignacion = Convert.ToInt32(asignacion["id_asignacion"]);
            int idRuta = Convert.ToInt32(asignacion["id_ruta"]);

            string carpeta = Path.Combine(AppContext.BaseDirectory, "HojasDeRuta");

            Directory.CreateDirectory(carpeta);

            string nombreArchivo = $"Ruta_{idRuta:D4}_Asignacion_{idAsignacion:D4}.pdf";
            string archivo = Path.Combine(carpeta, nombreArchivo);

            iTextSharp.text.Document documento = new iTextSharp.text.Document(PageSize.A4, 40, 40, 40, 40);

            using (FileStream stream = new FileStream(archivo, FileMode.Create))
            {
                PdfWriter.GetInstance(documento, stream);

                documento.Open();

                Font titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                Font subtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                Font normal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                Font encabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
                Font contenido = FontFactory.GetFont(FontFactory.HELVETICA, 9);

                Paragraph encabezadoDocumento = new Paragraph("HOJA DE RUTA", titulo);
                encabezadoDocumento.Alignment = Element.ALIGN_CENTER;
                encabezadoDocumento.SpacingAfter = 20;
                documento.Add(encabezadoDocumento);

                documento.Add(new Paragraph($"Ruta N.º: {idRuta}", normal));
                documento.Add(new Paragraph($"Asignación N.º: {idAsignacion}", normal));
                documento.Add(new Paragraph($"Descripción: {asignacion["descripcion"]}", normal));
                documento.Add(new Paragraph($"Repartidor: {asignacion["nombre"]} {asignacion["apellido"]}", normal));
                documento.Add(new Paragraph($"DNI: {asignacion["dni_repartidor"]}", normal));
                documento.Add(new Paragraph($"Fecha de asignación: {Convert.ToDateTime(asignacion["fecha_asignacion"]):dd/MM/yyyy HH:mm}", normal));
                documento.Add(new Paragraph(" "));

                Paragraph tituloEnvios = new Paragraph("ENVÍOS A ENTREGAR", subtitulo);
                tituloEnvios.SpacingAfter = 10;
                documento.Add(tituloEnvios);

                PdfPTable tabla = new PdfPTable(6);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 7, 18, 22, 15, 23, 15 });

                tabla.AddCell(new PdfPCell(new Phrase("Orden", encabezado)));
                tabla.AddCell(new PdfPCell(new Phrase("Seguimiento", encabezado)));
                tabla.AddCell(new PdfPCell(new Phrase("Destinatario", encabezado)));
                tabla.AddCell(new PdfPCell(new Phrase("Teléfono", encabezado)));
                tabla.AddCell(new PdfPCell(new Phrase("Dirección", encabezado)));
                tabla.AddCell(new PdfPCell(new Phrase("Localidad", encabezado)));

                foreach (DataRow envio in envios.Rows)
                {
                    tabla.AddCell(new PdfPCell(new Phrase(envio["orden_entrega"].ToString(), contenido)));
                    tabla.AddCell(new PdfPCell(new Phrase(envio["codigo_seguimiento"].ToString(), contenido)));
                    tabla.AddCell(new PdfPCell(new Phrase(envio["destinatario"].ToString(), contenido)));
                    tabla.AddCell(new PdfPCell(new Phrase(envio["telefono_destinatario"].ToString(), contenido)));
                    tabla.AddCell(new PdfPCell(new Phrase(envio["direccion"].ToString(), contenido)));
                    tabla.AddCell(new PdfPCell(new Phrase(envio["ciudad"].ToString() + ", " + envio["provincia"].ToString(), contenido)));
                }

                documento.Add(tabla);
                documento.Add(new Paragraph(" "));
                documento.Add(new Paragraph("Observaciones:", subtitulo));
                documento.Add(new Paragraph("____________________________________________________________________________________________", normal));
                documento.Add(new Paragraph("____________________________________________________________________________________________", normal));
                documento.Add(new Paragraph("____________________________________________________________________________________________", normal));

                documento.Close();
            }

            if (abrir)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = archivo,
                    UseShellExecute = true
                });
            }

            return archivo;
        }

        public static void RenombrarHojaRutaComoCancelada(int idRuta, int idAsignacion)
        {
            string carpeta = Path.Combine(AppContext.BaseDirectory, "HojasDeRuta");

            string nombreOriginal = $"Ruta_{idRuta:D4}_Asignacion_{idAsignacion:D4}.pdf";
            string nombreCancelado = $"Ruta_{idRuta:D4}_Asignacion_{idAsignacion:D4} - Ruta cancelada.pdf";

            string archivoOriginal = Path.Combine(carpeta, nombreOriginal);
            string archivoCancelado = Path.Combine(carpeta, nombreCancelado);

            if (!File.Exists(archivoOriginal))
                return;

            if (File.Exists(archivoCancelado))
                throw new Exception("Ya existe un archivo de hoja de ruta con el nombre de cancelación.");

            File.Move(archivoOriginal, archivoCancelado);
        }

        public static string GenerarInformeFinalizacionRuta(DataTable datosAsignacion, DataTable resumenEnvios, DataTable intentos, bool abrir = true)
        {
            if (datosAsignacion == null || datosAsignacion.Rows.Count == 0)
                throw new Exception("No se recibieron los datos de la asignación.");

            if (resumenEnvios == null || resumenEnvios.Rows.Count == 0)
                throw new Exception("No se recibieron los resultados de los envíos.");

            if (intentos == null)
                throw new Exception("No se recibieron los datos de los intentos.");

            DataRow asignacion = datosAsignacion.Rows[0];

            int idAsignacion = Convert.ToInt32(asignacion["id_asignacion"]);
            int idRuta = Convert.ToInt32(asignacion["id_ruta"]);

            int totalEnvios = resumenEnvios.Rows.Count;
            int entregados = 0;
            int fallidos = 0;

            foreach (DataRow envio in resumenEnvios.Rows)
            {
                string resultado = envio["ultimo_resultado"].ToString();

                if (resultado == "Entregado")
                    entregados++;
                else if (resultado == "Fallido")
                    fallidos++;
            }

            string carpeta = Path.Combine(AppContext.BaseDirectory, "InformesFinalizacion");
            Directory.CreateDirectory(carpeta);

            string nombreArchivo = $"Resultado_Ruta_{idRuta:D4}_Asignacion_{idAsignacion:D4}.pdf";
            string archivo = Path.Combine(carpeta, nombreArchivo);

            iTextSharp.text.Document documento = new iTextSharp.text.Document(PageSize.A4.Rotate(), 40, 40, 40, 40);

            using (FileStream stream = new FileStream(archivo, FileMode.Create))
            {
                PdfWriter.GetInstance(documento, stream);

                documento.Open();

                Font titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                Font subtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                Font normal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                Font encabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
                Font contenido = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                Paragraph encabezadoDocumento = new Paragraph("INFORME DE FINALIZACIÓN DE RUTA", titulo);
                encabezadoDocumento.Alignment = Element.ALIGN_CENTER;
                encabezadoDocumento.SpacingAfter = 20;
                documento.Add(encabezadoDocumento);

                documento.Add(new Paragraph($"Ruta N.º: {idRuta}", normal));
                documento.Add(new Paragraph($"Asignación N.º: {idAsignacion}", normal));
                documento.Add(new Paragraph($"Descripción: {asignacion["descripcion"]}", normal));
                documento.Add(new Paragraph($"Repartidor: {asignacion["nombre_repartidor"]} {asignacion["apellido_repartidor"]}", normal));
                documento.Add(new Paragraph($"DNI: {asignacion["dni_repartidor"]}", normal));
                documento.Add(new Paragraph($"Fecha de asignación: {Convert.ToDateTime(asignacion["fecha_asignacion"]):dd/MM/yyyy HH:mm}", normal));
                documento.Add(new Paragraph($"Inicio de ruta: {Convert.ToDateTime(asignacion["fecha_inicio"]):dd/MM/yyyy HH:mm}", normal));

                if (asignacion["fecha_fin"] != DBNull.Value)
                    documento.Add(new Paragraph($"Finalización de ruta: {Convert.ToDateTime(asignacion["fecha_fin"]):dd/MM/yyyy HH:mm}", normal));

                documento.Add(new Paragraph(" "));

                Paragraph tituloResumen = new Paragraph("RESUMEN DE RESULTADOS", subtitulo);
                tituloResumen.SpacingAfter = 10;
                documento.Add(tituloResumen);

                PdfPTable tablaResumen = new PdfPTable(3);
                tablaResumen.WidthPercentage = 70;
                tablaResumen.SetWidths(new float[] { 1, 1, 1 });

                tablaResumen.AddCell(new PdfPCell(new Phrase("Total de envíos", encabezado)));
                tablaResumen.AddCell(new PdfPCell(new Phrase("Entregados", encabezado)));
                tablaResumen.AddCell(new PdfPCell(new Phrase("Último intento fallido", encabezado)));

                tablaResumen.AddCell(totalEnvios.ToString());
                tablaResumen.AddCell(entregados.ToString());
                tablaResumen.AddCell(fallidos.ToString());

                documento.Add(tablaResumen);

                documento.Add(new Paragraph(" "));

                Paragraph tituloEnvios = new Paragraph("RESULTADO POR ENVÍO", subtitulo);
                tituloEnvios.SpacingAfter = 10;
                documento.Add(tituloEnvios);

                PdfPTable tablaEnvios = new PdfPTable(5);
                tablaEnvios.WidthPercentage = 100;
                tablaEnvios.SetWidths(new float[] { 7, 18, 25, 15, 12 });

                tablaEnvios.AddCell(new PdfPCell(new Phrase("Orden", encabezado)));
                tablaEnvios.AddCell(new PdfPCell(new Phrase("Seguimiento", encabezado)));
                tablaEnvios.AddCell(new PdfPCell(new Phrase("Destinatario", encabezado)));
                tablaEnvios.AddCell(new PdfPCell(new Phrase("Último resultado", encabezado)));
                tablaEnvios.AddCell(new PdfPCell(new Phrase("Cantidad de intentos", encabezado)));

                foreach (DataRow envio in resumenEnvios.Rows)
                {
                    tablaEnvios.AddCell(new PdfPCell(new Phrase(envio["orden_entrega"].ToString(), contenido)));
                    tablaEnvios.AddCell(new PdfPCell(new Phrase(envio["codigo_seguimiento"].ToString(), contenido)));
                    tablaEnvios.AddCell(new PdfPCell(new Phrase(envio["destinatario"].ToString(), contenido)));
                    tablaEnvios.AddCell(new PdfPCell(new Phrase(envio["ultimo_resultado"].ToString(), contenido)));
                    tablaEnvios.AddCell(new PdfPCell(new Phrase(envio["cantidad_intentos"].ToString(), contenido)));
                }

                documento.Add(tablaEnvios);

                documento.Add(new Paragraph(" "));

                Paragraph tituloIntentos = new Paragraph("HISTORIAL DE INTENTOS DE ENTREGA", subtitulo);
                tituloIntentos.SpacingAfter = 10;
                documento.Add(tituloIntentos);

                PdfPTable tablaIntentos = new PdfPTable(6);
                tablaIntentos.WidthPercentage = 100;
                tablaIntentos.SetWidths(new float[] { 18, 20, 15, 18, 25, 30 });

                tablaIntentos.AddCell(new PdfPCell(new Phrase("Seguimiento", encabezado)));
                tablaIntentos.AddCell(new PdfPCell(new Phrase("Destinatario", encabezado)));
                tablaIntentos.AddCell(new PdfPCell(new Phrase("Fecha y hora", encabezado)));
                tablaIntentos.AddCell(new PdfPCell(new Phrase("Resultado", encabezado)));
                tablaIntentos.AddCell(new PdfPCell(new Phrase("Inconveniente", encabezado)));
                tablaIntentos.AddCell(new PdfPCell(new Phrase("Observaciones", encabezado)));

                foreach (DataRow intento in intentos.Rows)
                {
                    tablaIntentos.AddCell(new PdfPCell(new Phrase(intento["codigo_seguimiento"].ToString(), contenido)));
                    tablaIntentos.AddCell(new PdfPCell(new Phrase(intento["destinatario"].ToString(), contenido)));
                    tablaIntentos.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(intento["fecha_intento"]).ToString("dd/MM/yyyy HH:mm"), contenido)));
                    tablaIntentos.AddCell(new PdfPCell(new Phrase(intento["resultado"].ToString(), contenido)));
                    tablaIntentos.AddCell(new PdfPCell(new Phrase(intento["tipo_inconveniente"] == DBNull.Value ? "-" : intento["tipo_inconveniente"].ToString(), contenido)));
                    tablaIntentos.AddCell(new PdfPCell(new Phrase(intento["observaciones"] == DBNull.Value ? "-" : intento["observaciones"].ToString(), contenido)));
                }

                documento.Add(tablaIntentos);

                documento.Add(new Paragraph(" "));
                Paragraph pie = new Paragraph("Informe generado por el Sistema de Gestión de Envíos.", contenido);
                pie.Alignment = Element.ALIGN_CENTER;
                documento.Add(pie);

                documento.Close();
            }

            if (abrir)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = archivo,
                    UseShellExecute = true
                });
            }

            return archivo;
        }

        public static void AbrirInformeFinalizacionRuta(int idRuta, int idAsignacion)
        {
            if (idRuta <= 0 || idAsignacion <= 0)
                throw new Exception("Los identificadores de ruta y asignación no son válidos.");

            string carpeta = Path.Combine(AppContext.BaseDirectory, "InformesFinalizacion");
            string nombreArchivo = $"Resultado_Ruta_{idRuta:D4}_Asignacion_{idAsignacion:D4}.pdf";
            string archivo = Path.Combine(carpeta, nombreArchivo);

            if (!File.Exists(archivo))
                throw new Exception("No se encontró el informe PDF de esta ruta.");

            Process.Start(new ProcessStartInfo
            {
                FileName = archivo,
                UseShellExecute = true
            });
        }
    }
}
