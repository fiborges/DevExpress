using Microsoft.AspNetCore.Mvc;
using YourNamespace.Services;
using YourNamespace.Models;
using System.IO;
using System.Linq;

namespace YourNamespace.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DocumentService _documentService;

        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "Content", "dados.csv");
            var records = _documentService.ReadCsv(csvPath);
            var names = records.Select(r => r.StudentName).ToList();
            ViewBag.Names = names;
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public IActionResult GenerateDocuments(string selectedName)
        {
            string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "Content", "dados.csv");
            string wordTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "Content", "modelo.docx");
            string pdfOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Attendance Declaration.pdf");

            var record = _documentService.ReadCsv(csvPath).FirstOrDefault(r => r.StudentName == selectedName);
            if (record != null)
            {
                _documentService.ProcessDocument(record, wordTemplatePath, pdfOutputPath);
                return File(System.IO.File.ReadAllBytes(pdfOutputPath), "application/pdf", "Attendance Declaration.pdf");
            }

            return RedirectToAction("Index");
        }
    }
}
