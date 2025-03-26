using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nyelvtanulas.Models;

namespace Nyelvtanulas.Controllers
{
    public class AppController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        public AppController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        public IActionResult LaunchConsoleApp()
        {
            // Bejelentkezett felhasználó neve
            string username = authenticationService.UserName; 

            // A konzolos alkalmazás relatív elérési útja
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string consoleAppPath = Path.Combine(basePath, "LingarixApplication.exe");

            if (!System.IO.File.Exists(consoleAppPath))
            {
                return BadRequest("A konzolos alkalmazás nem található.");
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k \" {consoleAppPath} {username}\"",
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            };

            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                process.WaitForExit();  
            }
            // Visszairányítás a statisztika oldalára
            return RedirectToAction("Stat", "Statistics");
        }
    }
}
