using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Lingarix
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Add meg a felhasználóneved: ");
            string username = Console.ReadLine();

            using (var client = new HttpClient())
            {
                string url = $"http://localhost:5130/api/users/{username}"; // Az MVC alkalmazásod URL-je
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseData);
                    string fetchedUsername = json["username"]?.ToString();

                    Console.WriteLine($"Üdvözlünk {fetchedUsername} a Lingarixben!");
                }
                else
                {
                    Console.WriteLine("Hiba: A felhasználó nem található.");
                }
            }
        }
        static void Main(string[] args)
        {
            ///<summary>
            /// Osztályok példányosítása
            /// </summary>

            Angol English = new Angol();
            Olasz Italy = new Olasz();
            Francia French = new Francia();
            Spanyol Spain = new Spanyol();
            Nemet Deutsch = new Nemet();

            Dictionary<string, string> used = new Dictionary<string, string>();

            string username = args.Length > 0 ? args[0] : "Ismeretlen felhasználó";
            StartConsoleApp(username);
            string language;
            Stopwatch stopwatch = Stopwatch.StartNew();


            Console.WriteLine($"               Üdvözlünk {username} a Lingarixben!            ");
            Console.WriteLine("                 _______________________");
            Console.WriteLine("                 |                     |");
            Console.WriteLine("                 | Használati utasítás |");
            Console.WriteLine("                 |_____________________|");
            Console.WriteLine("Írja be a nyelvet amiben szeretne tanulni \n majd azt a számot amilyen témában tanulni szeretne!");
            Console.ResetColor();

            do
            {
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("  Választható nyelvek:");
                Console.WriteLine(" * Angol");
                Console.WriteLine(" * Német");
                Console.WriteLine(" * Spanyol");
                Console.WriteLine(" * Olasz");
                Console.WriteLine(" * Francia");
                Console.WriteLine("-----------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" -- Kilépéshez írjon be egy 'x' karaktert");
                Console.ResetColor();
                Console.Write(" -- Nyelv: ");
                language = Console.ReadLine();
                switch (language)
                {
                    case "English":
                        English.Beolvas(username);
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intENGLISH = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intENGLISH == 1)
                        {
                            used.Add("Angol_ABC", "igen");
                            English.ABC();
                        }
                        if (intENGLISH == 2)
                        {
                            used.Add("Angol_Akasztofa", "igen");
                            English.Akasztofa();
                        }
                        if (intENGLISH == 3)
                        {
                            used.Add("Angol", "SzoParositas");
                            English.SzoParositas();
                        }
                        if (intENGLISH == 4)
                        {
                            used.Add("Angol_SzoKereso", "igen");
                            English.SzoKereso();
                        }
                        English.Pontok();
                        break;

                    case "német":
                        Deutsch.Beolvas(username);
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int IntGERMAN = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (IntGERMAN == 1)
                        {
                            used.Add("Nemet_ABC", "igen");
                            Deutsch.ABC();
                        }
                        if (IntGERMAN == 2)
                        {
                            used.Add("Nemet_Akasztofa", "igen");
                            Deutsch.Akasztofa();
                        }
                        if (IntGERMAN == 3)
                        {
                            used.Add("Nemet_SzoParositas", "igen");
                            Deutsch.SzoParositas();
                        }
                        if (IntGERMAN == 4)
                        {
                            used.Add("Nemet_SzoKereso", "igen");
                            Deutsch.SzoKereso();
                        }
                        Deutsch.Pontok();
                        break;

                    case "Spain":
                        Spain.Beolvas(username);
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intSPAIN = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intSPAIN == 1)
                        {
                            used.Add("Spanyol_ABC", "igen");
                            Spain.ABC();
                        }
                        if (intSPAIN == 2)
                        {
                            used.Add("Spanyol_Akasztofa", "igen");
                            Spain.Akasztofa();
                        }
                        if (intSPAIN == 3)
                        {
                            used.Add("Spanyol_SzoParositas", "igen");
                            Spain.SzoParositas();
                        }
                        if (intSPAIN == 4)
                        {
                            used.Add("Spanyol_SzoKereso", "igen");
                            Spain.SzoKereso();
                        }
                        Spain.Pontok();
                        break;

                    case "Italy":
                        Italy.Beolvas(username);
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intITALY = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intITALY == 1)
                        {
                            used.Add("Olasz_ABC", "igen");
                            Italy.ABC();
                        }
                        if (intITALY == 2)
                        {
                            used.Add("Olasz_Akasztofa", "igen");
                            Italy.Akasztofa();
                        }
                        if (intITALY == 3)
                        {
                            used.Add("Olasz_SzoParositas", "igen");
                            Italy.SzoParositas();
                        }
                        if (intITALY == 4)
                        {
                            used.Add("Olasz_SzoKereso", "igen");
                            Italy.SzoKereso();
                        }
                        Italy.Pontok();
                        break;

                    case "French":
                        French.Beolvas();
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intFRENCH = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intFRENCH == 1)
                        {
                            used.Add("Francia_ABC", "igen");
                            French.ABC();
                        }
                        if (intFRENCH == 2)
                        {
                            used.Add("Francia_ABC", "igen");
                            French.Akasztofa();
                        }
                        if (intFRENCH == 3)
                        {
                            used.Add("Francia_ABC", "igen");
                            French.SzoParositas();
                        }
                        if (intFRENCH == 4)
                        {
                            used.Add("Francia_ABC", "igen");
                            French.SzoKereso();
                        }
                        French.Pontok();
                        break;
                }
            }
            while (language != "x");
            {
                Console.WriteLine("Köszönjük " + username + ", hogy a Lingarixet választotta!");
                Console.WriteLine("Készítők: Gunics Bettina Virág, Páll Márk Hunor");
                DateTime today = DateTime.Now;
                Console.WriteLine($"Mai dátum: {today.ToString("yyyy-MM-dd")}");
                double elapsedHours = stopwatch.Elapsed.TotalMinutes;
                Console.WriteLine($"A program futásának ideje: {Math.Round(elapsedHours),4} perc");
                DateTime Date = DateTime.Now;
                int Score = English.Pontok() + Italy.Pontok() + French.Pontok() + Deutsch.Pontok() + Spain.Pontok();
                Console.WriteLine("Elért pontok:" + Score);
                foreach (var item in used)
                {
                    Console.Write($"{item.Key}: {item.Value}\n");
                }
                using (StreamWriter writer = new StreamWriter("Hasznaltnyelv_es_feladatok"))
                {
                    foreach (var pair in used)
                    {
                        writer.WriteLine($"{pair.Key}: {pair.Value}");
                    }
                }
            }
            Console.ReadKey();
        }
        public static void StartConsoleApp (string username)
        {
            Console.WriteLine($"               Üdvözlünk {username} a Lingarixben!            ");
            Console.WriteLine("                 _______________________");
            Console.WriteLine("                 |                     |");
            Console.WriteLine("                 | Használati utasítás |");
            Console.WriteLine("                 |_____________________|");
            Console.WriteLine("Írja be a nyelvet amiben szeretne tanulni \n majd azt a számot amilyen témában tanulni szeretne!");
            Console.ResetColor();

        }
    }
}

