using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using System.Net;
using System.Text.Json;

namespace Nyelvtanulas.Controllers
{
    public class CaptchaController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SubmitForm(string captchaResponse)
        {
            // Ha nem kaptunk választ a reCAPTCHA-tól, akkor nem engedjük a form elküldését
            if (string.IsNullOrEmpty(captchaResponse))
            {
                ModelState.AddModelError(string.Empty, "Captcha validation failed.");
                return View();
            }

            // Ellenőrzés a Google reCAPTCHA API-n keresztül
            var secretKey = "6LeTpc4qAAAAAHrJ7ODeujPihfPlIVSVDyKK0eF8";
            var verificationUrl = "https://www.google.com/recaptcha/api/siteverify";

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("secret", secretKey),
                new KeyValuePair<string, string>("response", captchaResponse)
            });

                var response = await client.PostAsync(verificationUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Deszerializáljuk a választ JSON formátumban
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseString);

                // Ellenőrizzük, hogy a sikeres válasz mellett a score eléri-e a küszöbértéket
                var success = jsonResponse.GetProperty("success").GetBoolean();
                var score = jsonResponse.GetProperty("score").GetSingle();

                if (success && score >= 0.5f)
                {
                    // Ha sikeres és a pontszám elfogadható, folytathatjuk a kérést
                    return RedirectToAction("Success");
                }
                else
                {
                    // Ha a validálás nem sikerült
                    ModelState.AddModelError(string.Empty, "Captcha validation failed.");
                    return View();
                }
            }
        }
    } 
}
