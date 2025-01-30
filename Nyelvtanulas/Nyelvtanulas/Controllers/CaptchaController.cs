using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace NyelvtanuloMVC.Controllers
{
    public class CaptchaController : Controller
    {
        public IActionResult GenerateCaptcha()
        {
            string captchaText = GenerateRandomText(5);
            HttpContext.Session.SetString("Captcha", captchaText);

            using (var bitmap = new Bitmap(150, 50))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.LightGray);
                g.DrawString(captchaText, new Font("Arial", 24), Brushes.Black, new PointF(20, 10));

                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    return File(stream.ToArray(), "image/png");
                }
            }
        }

        private string GenerateRandomText(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] captcha = new char[length];
            for (int i = 0; i < length; i++)
            {
                captcha[i] = chars[random.Next(chars.Length)];
            }
            return new string(captcha);
        }
    }
}
