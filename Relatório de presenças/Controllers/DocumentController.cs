using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;

public class DocumentController : Controller
{
    public async Task<IActionResult> GenerateDocuments()
    {
        try
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Modelo.docx");
            string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "Content", "dados.csv");
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Output");

            Directory.CreateDirectory(outputPath);

            await GeneratePdfFromCsvAsync(templatePath, csvPath, outputPath);

            ViewBag.Message = "PDFs gerados com sucesso.";
        }
        catch (Exception ex)
        {
            ViewBag.Message = $"Erro ao gerar PDFs: {ex.Message}";
        }

        return View();
    }

    private async Task GeneratePdfFromCsvAsync(string templatePath, string csvPath, string outputPath)
    {
        DataTable dataTable = await ReadCsvAsync(csvPath);

        using (FileStream fileStreamPath = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (WordDocument templateDocument = new WordDocument(fileStreamPath, FormatType.Docx))
        {
            foreach (DataRow row in dataTable.Rows)
            {
                using (WordDocument document = templateDocument.Clone())
                {
                    document.MailMerge.Execute(row);

                    WSection section = document.Sections[0];
                    IWParagraph headerParagraph = section.HeadersFooters.Header.AddParagraph();
                    string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Content", "logo_quizzone.png");
                    headerParagraph.AppendPicture(await File.ReadAllBytesAsync(logoPath));

                    IWParagraph footerParagraph = section.HeadersFooters.Footer.AddParagraph();
                    footerParagraph.AppendText($"Nome: {row["ContactName"]}\nEmail: {row["Email"]}\nTelefone: {row["Phone"]}\nEmpresa: QuizZone");

                    using (DocIORenderer render = new DocIORenderer())
                    using (PdfDocument pdfDocument = render.ConvertToPDF(document))
                    {
                        string pdfPath = Path.Combine(outputPath, $"{row["ContactName"]}.pdf");
                        await Task.Run(() => pdfDocument.Save(pdfPath));
                    }
                }
            }
        }
    }

    private async Task<DataTable> ReadCsvAsync(string path)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(path))
        {
            string headerLine = await reader.ReadLineAsync();
            if (headerLine != null)
            {
                foreach (var header in headerLine.Split(','))
                {
                    dt.Columns.Add(header.Trim());
                }

                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
                    if (dataLine != null)
                    {
                        dt.Rows.Add(dataLine.Split(','));
                    }
                }
            }
        }
        return dt;
    }
}