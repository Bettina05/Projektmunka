using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Lingarix_Database;

namespace Lingarix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string username = "";
            if (args.Length > 0)
            {
                username = args[0];
                Console.WriteLine($"               Üdvözlünk {username} a Lingarixben!            ");
                Console.WriteLine("                 _______________________");
                Console.WriteLine("                 |                     |");
                Console.WriteLine("                 | Használati utasítás |");
                Console.WriteLine("                 |_____________________|");
                Console.WriteLine("Írja be a nyelvet amelyet tanulni szeretne,  \n majd azt a számot amilyen témában tanulni szeretne! \n A választások után kiválaszthatja, hogy milyen formában szeretné.");
                Console.WriteLine("Lehetősége van 6 játékmód közül választani.\nAz egyes feladatokról rövid leírást találhat a feladat elindításánál.");
                Console.WriteLine("Köszönjük, hogy minket választott!");
                Console.ResetColor();
            }
            ///<summary>
            /// Osztályok példányosítása
            /// </summary>

            Angol English = new Angol();
            Olasz Italy = new Olasz();
            Francia French = new Francia();
            Spanyol Spain = new Spanyol();
            Nemet Deutsch = new Nemet();

            Dictionary<string, string> used = new Dictionary<string, string>();

            string language;
            Stopwatch stopwatch = Stopwatch.StartNew();

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
                language = Console.ReadLine().ToLower();
                switch (language)
                {
                    case "angol":
                        English.Beolvas(username);
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine(" 5. Tükörfordítás");
                        Console.WriteLine(" 6. Mondat rendezése");
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
                        if (intENGLISH == 5)
                        {
                            used.Add("Angol_TukorForditas", "igen");
                            English.TukorForditas();
                        }
                        if (intENGLISH == 6)
                        {
                            used.Add("Angol_MondatRendezes", "igen");
                            English.MondatRendezes ();
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
                        Console.WriteLine(" 5. Tükörfordítás");
                        Console.WriteLine(" 6. Mondat rendezése");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intGERMAN = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intGERMAN == 1)
                        {
                            used.Add("Nemet_ABC", "igen");
                            Deutsch.ABC();
                        }
                        if (intGERMAN == 2)
                        {
                            used.Add("Nemet_Akasztofa", "igen");
                            Deutsch.Akasztofa();
                        }
                        if (intGERMAN == 3)
                        {
                            used.Add("Nemet_SzoParositas", "igen");
                            Deutsch.SzoParositas();
                        }
                        if (intGERMAN == 4)
                        {
                            used.Add("Nemet_SzoKereso", "igen");
                            Deutsch.SzoKereso();
                        }
                        if (intGERMAN == 5)
                        {
                            used.Add("Angol_TukorForditas", "igen");
                            Deutsch.TukorForditas();
                        }
                        if (intGERMAN == 6)
                        {
                            used.Add("Angol_MondatRendezes", "igen");
                            Deutsch.MondatRendezes();
                        }
                        Deutsch.Pontok();
                        break;

                    case "spanyol":
                        Spain.Beolvas(username);
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine(" 5. Tükörfordítás");
                        Console.WriteLine(" 6. Mondat rendezése");
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
                        if (intSPAIN == 5)
                        {
                            used.Add("Angol_TukorForditas", "igen");
                            Spain.TukorForditas();
                        }
                        if (intSPAIN == 6)
                        {
                            used.Add("Angol_MondatRendezes", "igen");
                            Spain.MondatRendezes();
                        }
                        Spain.Pontok();
                        break;

                    case "olasz":
                        Italy.Beolvas(username);
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine(" 5. Tükörfordítás");
                        Console.WriteLine(" 6. Mondat rendezése");
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
                        if (intITALY == 5)
                        {
                            used.Add("Angol_TukorForditas", "igen");
                            Italy.TukorForditas();
                        }
                        if (intITALY == 6)
                        {
                            used.Add("Angol_MondatRendezes", "igen");
                            Italy.MondatRendezes();
                        }
                        Italy.Pontok();
                        break;

                    case "francia":
                        French.Beolvas();
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("  Választható feladatok:");
                        Console.WriteLine(" 1. ABC választási lehetőség");
                        Console.WriteLine(" 2. Akasztófa");
                        Console.WriteLine(" 3. Szópárosítás");
                        Console.WriteLine(" 4. Szó kereső");
                        Console.WriteLine(" 5. Tükörfordítás");
                        Console.WriteLine(" 6. Mondat rendezése");
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
                        if (intFRENCH == 5)
                        {
                            used.Add("Angol_TukorForditas", "igen");
                            French.TukorForditas();
                        }
                        if (intFRENCH == 6)
                        {
                            used.Add("Angol_MondatRendezes", "igen");
                            French.MondatRendezes();
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
                using (StreamWriter writer = new StreamWriter("Hasznaltnyelv_es_feladatok.txt"))
                {
                    foreach (var pair in used)
                    {
                        writer.WriteLine($"{pair.Key}: {pair.Value}");
                    }
                }
            }
            Console.WriteLine("\nNyomj meg egy gombot a kilépéshez...");
            Console.ReadKey();
        }
        // Adatbázis kontextus osztály
        //public class UserDbContext : DbContext
        //{
        //    public DbSet<User> Users { get; set; }

        //    protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    {
        //        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsersDatabase;Trusted_Connection=True;");
        //    }
        //}

        //// Felhasználó osztály
        //public class User
        //{
        //    public int Id { get; set; }
        //    public string Username { get; set; }
        //    public bool IsLoggedIn { get; set; } // Ez az oszlop jelzi, ki van bejelentkezve
        //}

    }
}

