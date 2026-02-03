using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Encryption.Controllers
{
    public class CaesarEncryptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
      

[HttpPost]
[HttpGet]
    public IActionResult Encrypt(string text, int key)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            ViewBag.EncryptedText = "Please enter text to encrypt.";
            return View("Encrypt");
        }

        string pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "caesar_encrypt.py");

        var psi = new ProcessStartInfo
        {
            FileName = "python",
            Arguments = $"\"{pythonScriptPath}\" \"{text.Replace("\"", "\\\"")}\" {key}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        string output = "";
        string error = "";

        try
        {
            using (var process = Process.Start(psi))
            {
                output = process.StandardOutput.ReadToEnd().Trim();
                error = process.StandardError.ReadToEnd().Trim();
                process.WaitForExit();
            }

            ViewBag.EncryptedText = !string.IsNullOrEmpty(output) ? output : error;
        }
        catch (Exception ex)
        {
            ViewBag.EncryptedText = "Error running encryption script: " + ex.Message;
        }

        return View("Encrypt");
    }

        [HttpPost]
        [HttpGet]
        public IActionResult Decrypt(string text, int key)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ViewBag.DecryptedText = "Please enter text to decrypt.";
                return View("Decrypt");
            }

            string pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "caesar_decrypt.py");

            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{pythonScriptPath}\" \"{text.Replace("\"", "\\\"")}\" {key}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string output = "";
            string error = "";

            try
            {
                using (var process = Process.Start(psi))
                {
                    output = process.StandardOutput.ReadToEnd().Trim();
                    error = process.StandardError.ReadToEnd().Trim();
                    process.WaitForExit();
                }

                ViewBag.DecryptedText = !string.IsNullOrEmpty(output) ? output : error;
            }
            catch (Exception ex)
            {
                ViewBag.DecryptedText = "Error running decryption script: " + ex.Message;
            }

            return View("Decrypt");
        }


    }
}
