using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nyelvtanulas.Models;
using Lingarix_Database;

namespace Nyelvtanulas.Controllers
{
    public class AppController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly Lingarix_Database.LingarixDbContext DBcontext;
        public AppController(IAuthenticationService authenticationService, LingarixDbContext DBcontext)
        {
            this.authenticationService = authenticationService;
            this.DBcontext = DBcontext;
        }
        public IActionResult LaunchConsoleApp()
        {
            string username = authenticationService.UserName; 

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string consoleAppPath = Path.Combine(basePath, "LingarixApplication.exe");

            if (!System.IO.File.Exists(consoleAppPath))
            {
                return BadRequest("A konzolos alkalmazás nem található.");
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c \" {consoleAppPath} {username}\"",
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            };

            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                process.WaitForExit();  
            }

            string currentUsername = authenticationService.UserName;
            var userStatistics = DBcontext.UserStatistics
                .Where(x => x.Username == currentUsername)
                .OrderByDescending(x => x.Date)
                .ToList();
            return View(userStatistics);
        }
    }
}
