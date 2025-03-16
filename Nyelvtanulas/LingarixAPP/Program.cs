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
            
            Angol angol = new Angol();
            //Angol angol = new Angol();
            //Olasz olasz = new Olasz();
            //Francia francia = new Francia();
            //Spanyol spanyol = new Spanyol();
            //Nemet nemet = new Nemet();

            Dictionary<string, string> hasznaltak = new Dictionary<string, string>();

            string username = "Márk";
            string nyelv;
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
                nyelv = Console.ReadLine();
                switch (nyelv)
                {
                    case "angol":
                        angol.Beolvas(username);
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int szamANGOL = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (szamANGOL == 1)
                        {
                            hasznaltak.Add("Angol_ABC", "igen");
                            angol.ABC();
                        }
                        if (szamANGOL == 2)
                        {
                            hasznaltak.Add("Angol_Akasztofa", "igen");
                            angol.Akasztofa();
                        }
                        if (szamANGOL == 3)
                        {
                            hasznaltak.Add("Angol", "SzoParositas");
                            angol.SzoParositas();
                        }
                        if (szamANGOL == 4)
                        {
                            hasznaltak.Add("Angol_SzoKereso", "igen");
                            angol.SzoKereso();
                        }
                        angol.Pontok();
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
                        int szamNEMET = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (szamNEMET == 1)
                        {
                            hasznaltak.Add("Nemet_ABC", "igen");
                            nemet.ABC();
                        }
                        if (szamNEMET == 2)
                        {
                            hasznaltak.Add("Nemet_Akasztofa", "igen");
                            nemet.Akasztofa();
                        }
                        if (szamNEMET == 3)
                        {
                            hasznaltak.Add("Nemet_SzoParositas", "igen");
                            nemet.SzoParositas();
                        }
                        if (szamNEMET == 4)
                        {
                            hasznaltak.Add("Nemet_SzoKereso", "igen");
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
                        int szamSPANYOL = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (szamSPANYOL == 1)
                        {
                            hasznaltak.Add("Spanyol_ABC", "igen");
                            spanyol.ABC();
                        }
                        if (szamSPANYOL == 2)
                        {
                            hasznaltak.Add("Spanyol_Akasztofa", "igen");
                            spanyol.Akasztofa();
                        }
                        if (szamSPANYOL == 3)
                        {
                            hasznaltak.Add("Spanyol_SzoParositas", "igen");
                            spanyol.SzoParositas();
                        }
                        if (szamSPANYOL == 4)
                        {
                            hasznaltak.Add("Spanyol_SzoKereso", "igen");
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
                        int szamOLASZ = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (szamOLASZ == 1)
                        {
                            hasznaltak.Add("Olasz_ABC", "igen");
                            olasz.ABC();
                        }
                        if (szamOLASZ == 2)
                        {
                            hasznaltak.Add("Olasz_Akasztofa", "igen");
                            olasz.Akasztofa();
                        }
                        if (szamOLASZ == 3)
                        {
                            hasznaltak.Add("Olasz_SzoParositas", "igen");
                            olasz.SzoParositas();
                        }
                        if (szamOLASZ == 4)
                        {
                            hasznaltak.Add("Olasz_SzoKereso", "igen");
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
                        int szamFRANCIA = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (szamFRANCIA == 1)
                        {
                            hasznaltak.Add("Francia_ABC", "igen");
                            francia.ABC();
                        }
                        if (szamFRANCIA == 2)
                        {
                            hasznaltak.Add("Francia_ABC", "igen");
                            francia.Akasztofa();
                        }
                        if (szamFRANCIA == 3)
                        {
                            hasznaltak.Add("Francia_ABC", "igen");
                            francia.SzoParositas();
                        }
                        if (szamFRANCIA == 4)
                        {
                            hasznaltak.Add("Francia_ABC", "igen");
                            francia.SzoKereso();
                        }
                        francia.Pontok();
                        break;
                }
            }
            while (nyelv != "x");
            {
                Console.WriteLine("Köszönjük " + username + ", hogy a Lingarixet választotta!");
                Console.WriteLine("Készítők: Gunics Bettina Virág, Páll Márk Hunor");
                DateTime today = DateTime.Now;
                Console.WriteLine($"Mai dátum: {today.ToString("yyyy-MM-dd")}");
                double elapsedHours = stopwatch.Elapsed.TotalMinutes;
                Console.WriteLine($"A program futásának ideje: {Math.Round(elapsedHours),4} perc");
                DateTime Date = DateTime.Now;
                int Score = angol.Pontok() + olasz.Pontok() + francia.Pontok() + nemet.Pontok() + spanyol.Pontok();
                Console.WriteLine("Elért pontok:" + Score);
                foreach (var item in hasznaltak)
                {
                    Console.Write($"{item.Key}: {item.Value}\n");
                }
                using (StreamWriter writer = new StreamWriter("Hasznaltnyelv_es_feladatok"))
                {
                    foreach (var pair in hasznaltak)
                    {
                        writer.WriteLine($"{pair.Key}: {pair.Value}");
                    }
                }
            }
            Console.ReadKey();
        }
    }

