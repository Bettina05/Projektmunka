using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lingarix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///<summary>
            /// Osztályok példányosítása
            /// </summary>

            Angol english = new Angol();
            //Angol english = new Angol();
            //Olasz olasz = new Olasz();
            //Francia francia = new Francia();
            //Spanyol spanyol = new Spanyol();
            //Nemet nemet = new Nemet();

            Dictionary<string, string> used = new Dictionary<string, string>();

            string username = "Márk";
            string language;
            Stopwatch stopwatch = Stopwatch.StartNew();


            Console.WriteLine("               Üdvözlünk " + username + " a Lingarixben            ");
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
                    case "english":
                        english.Beolvas(username);
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
                            english.ABC();
                        }
                        if (intENGLISH == 2)
                        {
                            used.Add("Angol_Akasztofa", "igen");
                            english.Akasztofa();
                        }
                        if (intENGLISH == 3)
                        {
                            used.Add("Angol", "SzoParositas");
                            english.SzoParositas();
                        }
                        if (intENGLISH == 4)
                        {
                            used.Add("Angol_SzoKereso", "igen");
                            english.SzoKereso();
                        }
                        english.Pontok();
                        break;

                    case "német":
                        nemet.Beolvas(username);
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
                            nemet.ABC();
                        }
                        if (IntGERMAN == 2)
                        {
                            used.Add("Nemet_Akasztofa", "igen");
                            nemet.Akasztofa();
                        }
                        if (IntGERMAN == 3)
                        {
                            used.Add("Nemet_SzoParositas", "igen");
                            nemet.SzoParositas();
                        }
                        if (IntGERMAN == 4)
                        {
                            used.Add("Nemet_SzoKereso", "igen");
                            nemet.SzoKereso();
                        }
                        nemet.Pontok();
                        break;

                    case "spanyol":
                        spanyol.Beolvas(username);
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
                            spanyol.ABC();
                        }
                        if (intSPAIN == 2)
                        {
                            used.Add("Spanyol_Akasztofa", "igen");
                            spanyol.Akasztofa();
                        }
                        if (intSPAIN == 3)
                        {
                            used.Add("Spanyol_SzoParositas", "igen");
                            spanyol.SzoParositas();
                        }
                        if (intSPAIN == 4)
                        {
                            used.Add("Spanyol_SzoKereso", "igen");
                            spanyol.SzoKereso();
                        }
                        spanyol.Pontok();
                        break;

                    case "olasz":
                        olasz.Beolvas(username);
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
                            olasz.ABC();
                        }
                        if (intITALY == 2)
                        {
                            used.Add("Olasz_Akasztofa", "igen");
                            olasz.Akasztofa();
                        }
                        if (intITALY == 3)
                        {
                            used.Add("Olasz_SzoParositas", "igen");
                            olasz.SzoParositas();
                        }
                        if (intITALY == 4)
                        {
                            used.Add("Olasz_SzoKereso", "igen");
                            olasz.SzoKereso();
                        }
                        olasz.Pontok();
                        break;

                    case "francia":
                        francia.Beolvas();
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
                            francia.ABC();
                        }
                        if (intFRENCH == 2)
                        {
                            used.Add("Francia_ABC", "igen");
                            francia.Akasztofa();
                        }
                        if (intFRENCH == 3)
                        {
                            used.Add("Francia_ABC", "igen");
                            francia.SzoParositas();
                        }
                        if (intFRENCH == 4)
                        {
                            used.Add("Francia_ABC", "igen");
                            francia.SzoKereso();
                        }
                        francia.Pontok();
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
                int Score = english.Pontok() + olasz.Pontok() + francia.Pontok() + nemet.Pontok() + spanyol.Pontok();
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
    }
}

