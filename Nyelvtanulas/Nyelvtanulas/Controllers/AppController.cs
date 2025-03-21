using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Lingarix_Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Nyelvtanulas.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        public IActionResult LaunchConsoleApp()
        {
            // A konzolos alkalmazás relatív elérési útja
            string consoleAppPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "LingarixAPP", "bin", "Debug", "net6.0", "LingarixAPP.exe");

            if (System.IO.File.Exists(consoleAppPath))
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = consoleAppPath,
                    UseShellExecute = true
                };

                Process consoleProcess = Process.Start(processStartInfo);

                if (consoleProcess != null)
                {
                    // Várakozás, amíg a konzolos app befejeződik
                    consoleProcess.WaitForExit();
                }

                // Konzol bezárása után irányítás a statisztikai oldalra
                return RedirectToAction("Stat", "Statistics");
            }
            else
            {
                return BadRequest("A konzolos alkalmazás nem található.");
            }
        }
    }
}
