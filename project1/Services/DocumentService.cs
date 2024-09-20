using System.Globalization;
using CsvHelper;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;
using YourNamespace.Models;
using System.Collections.Generic;
using System.IO;

namespace YourNamespace.Services
{
    public class DocumentService
    {
        public void ProcessDocument(AttendanceDeclaration record, string wordTemplatePath, string outputPdfPath)
        {
            using (FileStream templateStream = new FileStream(wordTemplatePath, FileMode.Open, FileAccess.Read))
            {
                WordDocument document = new WordDocument(templateStream, FormatType.Docx);

                string[] fieldNames = new string[]
                {
                    "StudentName", "ExamDate", "StartTime", "EndTime",
                    "ExamName", "Location", "StudentEmail", "ContactNumber",
                    "CurrentDate", "TeacherName", "TeacherPosition"
                };

                string[] fieldValues = new string[]
                {
                    record.StudentName,
                    FormatDate(record.ExamDate),
                    record.StartTime,
                    record.EndTime,
                    record.ExamName,
                    record.Location,
                    record.StudentEmail,
                    record.ContactNumber,
                    FormatDate(record.CurrentDate),
                    record.TeacherName,
                    record.TeacherPosition
                };

                document.MailMerge.Execute(fieldNames, fieldValues);

                using (MemoryStream pdfStream = new MemoryStream())
                {
                    DocIORenderer renderer = new DocIORenderer();
                    PdfDocument pdfDocument = renderer.ConvertToPDF(document);
                    using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                    {
                        pdfDocument.Save(outputStream);
                    }
                    pdfDocument.Close(true);
                    renderer.Dispose();
                }

                document.Close();
            }
        }

        public List<AttendanceDeclaration> ReadCsv(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<AttendanceDeclaration>().ToList();
            }
        }

        private string FormatDate(DateTime date)
        {
            CultureInfo cultureInfo = new CultureInfo("pt-PT");
            string formattedDate = date.ToString("d 'de' MMMM 'de' yyyy", cultureInfo);
            int monthStart = formattedDate.IndexOf(' ') + 4; 
            char firstChar = char.ToUpper(formattedDate[monthStart]);
            string formattedMonth = firstChar + formattedDate.Substring(monthStart + 1);
            return formattedDate.Substring(0, monthStart) + formattedMonth;
        }
    }
}
