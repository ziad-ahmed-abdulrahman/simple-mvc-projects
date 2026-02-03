using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class AESEncryptionController : Controller
{
    public IActionResult Index()
    {
       return View("~/Views/AESEncryption/Index.cshtml");

    }

    [HttpPost]
    [HttpGet]
    public IActionResult Encrypt(string text, string key)
    {
        if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(key))
        {
            ViewBag.EncryptedText = "Please enter both text and key.";
            return View("Encrypt");
        }

        
        string pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "aes_encrypt.py");

        
        string safeText = text.Replace("\"", "\\\"");
        string safeKey = key.Replace("\"", "\\\"");

        var psi = new ProcessStartInfo
        {
            FileName = "python",
            Arguments = $"\"{pythonScriptPath}\" \"{safeText}\" \"{safeKey}\"",
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






    // ---------------------------------------------------------------

    [HttpPost]
    [HttpGet]
    public IActionResult Decrypt(string text, string key)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            ViewBag.DecryptedText = "Please enter text to decrypt.";
            return View("Decrypt");
        }

        if (string.IsNullOrWhiteSpace(key) || (key.Length != 16 && key.Length != 24 && key.Length != 32))
        {
            ViewBag.DecryptedText = "AES key must be 16, 24, or 32 characters long.";
            return View("Decrypt");
        }

        string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "aes_decrypt.py");

        var psi = new ProcessStartInfo
        {
            FileName = "python",
            Arguments = $"\"{scriptPath}\" \"{text.Replace("\"", "\\\"")}\" \"{key.Replace("\"", "\\\"")}\"",
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
