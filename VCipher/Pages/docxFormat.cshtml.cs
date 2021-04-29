using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.IO;
namespace VCipher
{
    public class DocXFormatModel : PageModel
    {
        private IWebHostEnvironment webHostEnvironment;
        public string FilePath { get; set; }
        public string FormattedTextDe { get; set; }
        public string FormattedTextEn { get; set; }

        public DocXFormatModel(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public ActionResult OnPostDownloadFormattedText(string fileName)
        {
            var folder = Path.Combine(webHostEnvironment.WebRootPath, "files");
            FilePath = Path.Combine(folder, GetIpValue() + "_" + "formattedText.docx");
            byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
            return File(fileBytes, "application/octet-stream", fileName + ".docx");
        }
        public void OnPostAddFile(IFormFile fileDOCX)
        {
            var folder = Path.Combine(webHostEnvironment.WebRootPath, "files");
            FilePath = Path.Combine(folder, GetIpValue() + "_" + "text.docx");
            using (var fileStream = new FileStream(FilePath, FileMode.Create))
            {
                fileDOCX.CopyTo(fileStream);
            }
        }
        public void OnPostGetEncryptedText(string key)
        {
            var folder = Path.Combine(webHostEnvironment.WebRootPath, "files");
            Document inputDoc = new Document();
            FilePath = Path.Combine(folder, GetIpValue() + "_" + "text.docx");
            inputDoc.LoadFromFile(FilePath);
            string text = inputDoc.GetText();
            VigenereCipherHandler encoder = new VigenereCipherHandler(text, key);
            FormattedTextEn = encoder.EncodeText();
            Document exportDoc = new Document();
            Section section = exportDoc.AddSection();
            Paragraph Para = section.AddParagraph();
            Para.AppendText(FormattedTextEn);
            FilePath = Path.Combine(folder, GetIpValue() + "_" + "formattedText.docx");
            exportDoc.SaveToFile(FilePath, FileFormat.Docx);
        }
        private string GetIpValue()
        {
            string remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (remoteIpAddress.ToString() == "::1")
            {
                remoteIpAddress = "127.0.0.1";
            }
            return remoteIpAddress;
        }
    }
}
