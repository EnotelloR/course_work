using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace VCipher.Pages
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment webHostEnvironment;
        public string FilePath { get; set; }
        public string FormattedTextDe { get; set; }
        public string FormattedTextEn { get; set; }

        public IndexModel(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public ActionResult OnPostDownloadFormattedText(string fileName)
        {
            var folder = Path.Combine(webHostEnvironment.WebRootPath, "files");
            FilePath = Path.Combine(folder, GetIpValue() + "_" + "formattedText.txt");
            byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
            return File(fileBytes, "application/octet-stream", fileName + ".txt");
        }
        public void OnPostAddFile(IFormFile fileTXT)
        {
            var folder = Path.Combine(webHostEnvironment.WebRootPath, "files");
            FilePath = Path.Combine(folder, GetIpValue() + "_" + "text.txt");
            using (var fileStream = new FileStream(FilePath, FileMode.Create))
            {
                fileTXT.CopyTo(fileStream);
            }
        }
        public void OnPostGetDecryptedText(string key)
        {
            var folder = Path.Combine(webHostEnvironment.WebRootPath, "files");
            FilePath = Path.Combine(folder, GetIpValue() + "_" + "text.txt");
            var text = System.IO.File.ReadAllText(FilePath);
            VigenereCipherHandler decoder = new VigenereCipherHandler(text, key);
            FormattedTextDe = decoder.DecodeText();
            FilePath = Path.Combine(folder, GetIpValue() + "_" + "formattedText.txt");
            System.IO.File.WriteAllText(FilePath, FormattedTextDe);
        }
        public void OnPostGetEncryptedText(string text, string key)
        {
            var folder = Path.Combine(webHostEnvironment.WebRootPath, "files");
            VigenereCipherHandler encoder = new VigenereCipherHandler(text, key);
            FormattedTextEn = encoder.EncodeText();
            FilePath = Path.Combine(folder, GetIpValue() + "_" + "formattedText.txt");
            System.IO.File.WriteAllText(FilePath, FormattedTextEn);
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
