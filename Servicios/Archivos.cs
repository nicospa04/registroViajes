using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using BE;

namespace Servicios
{
    public class Archivo
    {

        public string DestinoFactura { get; set; }
        public string DestinoBitacora { get; set; }
        public string Conn { get; set; }


        public Archivo()
        {
            string rutaArchivoConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "destino.txt");
            Console.WriteLine("Ruta del archivo de configuración: " + rutaArchivoConfig);
            try
            {
                if (File.Exists(rutaArchivoConfig))
                {
                    using (FileStream fs = new FileStream(rutaArchivoConfig, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            string[] vregistro;
                            string registroo;
                            while (!(sr.Peek() == -1))
                            {
                                registroo = sr.ReadLine();
                                vregistro = registroo.Split('|');
                                DestinoFactura = vregistro[0];
                                Conn = vregistro[1];
                                DestinoBitacora = vregistro[2];
                                //DestinoBitacora = "C:/Documentos/Eventos/";
                            }
                        }
                    }
                    Console.WriteLine($"DestinoFactura: {DestinoFactura}");
                    Console.WriteLine($"Conn: {Conn}");
                    Console.WriteLine($"DestinoBitacora: {DestinoBitacora}");
                    if (string.IsNullOrEmpty(DestinoBitacora))
                    {
                        throw new Exception("La ruta destino de la bitácora (DestinoBitacora) no se ha especificado en destino.txt.");
                    }
                }
                else
                {
                    throw new FileNotFoundException("No se encontró el archivo de configuración 'destino.txt'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo de configuración: {ex.Message}");
            }
        }

        public bool GenerarArchivo(List<BEBitacora> bitacoras, string idioma)
        {
            try
            {
                //generar nombre del archivo
                string fileName = Path.Combine(DestinoBitacora, $"{Generarnombre()}.pdf");
                //crear doc pdf
                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));
                doc.Open();
                switch (idioma)
                {
                    case "Inglés":
                    case "English":
                    case "EN":
                        doc.Add(new Paragraph("Event Report"));
                        break;

                    case "Español":
                    case "Spanish":
                    case "ES":
                        doc.Add(new Paragraph("Informe de eventos"));
                        break;

                    default:
                        doc.Add(new Paragraph("Informe de eventos"));
                        break;
                }
                PdfPTable tabla = new PdfPTable(6);
                if (idioma == "Inglés" || idioma == "English" || idioma == "EN")
                {
                    tabla.AddCell("ID Binnacle");
                    tabla.AddCell("ID User");
                    tabla.AddCell("Operation");
                    tabla.AddCell("Date");
                    tabla.AddCell("Module");
                    tabla.AddCell("Criticality");
                }
                else
                {
                    tabla.AddCell("ID bitacora");
                    tabla.AddCell("ID usuario");
                    tabla.AddCell("Operacion");
                    tabla.AddCell("Fecha");
                    tabla.AddCell("Modulo");
                    tabla.AddCell("Criticidad");
                }

                foreach (BEBitacora b in bitacoras)
                {
                    if (b != null)
                    {
                        tabla.AddCell(b.id_bitacora.ToString());
                        tabla.AddCell(b.id_usuario.ToString());
                        tabla.AddCell(b.operacion);
                        tabla.AddCell(b.fecha.ToString("yyyy-MM-dd HH-mm-ss"));
                        tabla.AddCell(b.actor);
                        tabla.AddCell(b.criticidad.ToString());
                    }
                }
                //agregar tabla al doc
                doc.Add(tabla);
                //cerrar el doc
                doc.Close();
                //abrir el archivo pdf generado
                Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
                return true;
            }
            catch (DocumentException dex)
            {
                throw new Exception("Error al generar el archivo pdf: " + dex.Message);
            }
            catch (IOException ioex)
            {
                throw new Exception("Error de IO al generar el archivo pdf: " + ioex.Message);
            }
        }

        public string Generarnombre()
        {
            Random random = new Random();
            string nombre;
            int numero = random.Next(0, 1001);
            char letra = (char)random.Next('A', 'Z' + 1);
            nombre = "BITÁCORAGORDO" + numero.ToString() + letra;
            string FileName = Path.Combine(DestinoBitacora, $"{nombre}.pdf");
            if (File.Exists(FileName))
            {
                return Generarnombre();
            }
            return nombre;
        }
    }
}
