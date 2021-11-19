using iText.Kernel.Pdf;
using System.IO;

namespace MergePDFs
{
    public class Program
    {
        /// <summary>
        /// This code merges PDFs with the help of the iText library.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            FileStream fs1 = new FileStream(@"C:\Users\name\source\repos\MergePDFs\file1.pdf", FileMode.Open);
            FileStream fs2 = new FileStream(@"C:\Users\name\source\repos\MergePDFs\file2.pdf", FileMode.Open);

            using (MemoryStream masterStream = new MemoryStream())
            {
                using (MemoryStream temporaryStream = new MemoryStream())
                {
                    // read doc 1
                    using (PdfDocument combinedDocument = new PdfDocument(new PdfReader(fs1), new PdfWriter(temporaryStream)))
                    using (PdfDocument componentDocument = new PdfDocument(new PdfReader(fs2)))
                    {
                        // write doc 2 into doc 1
                        componentDocument.CopyPagesTo(1, componentDocument.GetNumberOfPages(), combinedDocument);
                    }

                    // copy merged pdf into masterStream
                    byte[] temporaryBytes = temporaryStream.ToArray();
                    masterStream.Position = 0;
                    masterStream.Capacity = temporaryBytes.Length;
                    masterStream.Write(temporaryBytes, 0, temporaryBytes.Length);
                    masterStream.Position = 0;
                }

                using (FileStream fs = new FileStream(@"C:\Users\name\source\repos\MergePDFs\output.pdf", FileMode.Create, FileAccess.Write))
                {
                    masterStream.WriteTo(fs);
                }
            }
        }
    }
}
