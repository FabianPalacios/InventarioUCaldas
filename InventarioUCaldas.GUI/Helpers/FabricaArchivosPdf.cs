using InventarioUCaldas.GUI.Models.Parametros;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Helpers
{
    public class FabricaArchivosPdf
    {

        public bool CrearListadoMarcasEnPdf(String pdfpath, String titulo, IEnumerable<ModeloMarcaGUI> listaDatos)
        {
            try
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfpath, FileMode.Create));
                doc.Open();
                doc.Add(new Paragraph(new Phrase(titulo)));
                doc.Add(new Paragraph(""));
                doc.AddAuthor("Yorman Valencia");
                doc.AddCreationDate();


                PdfPTable tabla = new PdfPTable(2);
                float anchoColumna = (PageSize.A4.Width - (doc.LeftMargin + doc.RightMargin)) / 2;


                foreach (var registro in listaDatos)
                {
                    PdfPCell celda = new PdfPCell(new Paragraph(new Phrase(registro.Id)));
                    tabla.AddCell(celda);
                    celda = new PdfPCell(new Paragraph(new Phrase(registro.Nombre)));
                    tabla.AddCell(celda);

                }

                doc.Add(tabla);
                doc.Close();
                writer.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}