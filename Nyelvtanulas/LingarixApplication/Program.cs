using System.Diagnostics;
using Lingarix_Database;
using Lingarix_Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lingarix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LingarixDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsersDatabase;Trusted_Connection=True;");
            var context = new LingarixDbContext(optionsBuilder.Options);

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
                        Console.WriteLine(" 5. Mondat rendezése");
                        Console.WriteLine(" 6. Helyes fordítás");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intENGLISH = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intENGLISH == 1)
                        {
                            used.Add("Angol: ABC", "igen");
                            English.ABC();
                        }
                        if (intENGLISH == 2)
                        {
                            used.Add("Angol: Akasztófa", "igen");
                            English.Akasztofa();
                        }
                        if (intENGLISH == 3)
                        {
                            used.Add("Angol: Szópárosítás", "igen");
                            English.SzoParositas();
                        }
                        if (intENGLISH == 4)
                        {
                            used.Add("Angol: Szókereső", "igen");
                            English.SzoKereso();
                        }
                        if (intENGLISH == 5)
                        {
                            used.Add("Angol: Mondat rendezés", "igen");
                            English.MondatRendezes();
                        }
                        if (intENGLISH == 6)
                        {
                            used.Add("Angol: Helyes fordítás", "igen");
                            English.Helyesforditas();
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
                        Console.WriteLine(" 5. Mondat rendezése");
                        Console.WriteLine(" 6. Helyes fordítás");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intGERMAN = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intGERMAN == 1)
                        {
                            used.Add("Német: ABC", "igen");
                            Deutsch.ABC();
                        }
                        if (intGERMAN == 2)
                        {
                            used.Add("Német: Akasztófa", "igen");
                            Deutsch.Akasztofa();
                        }
                        if (intGERMAN == 3)
                        {
                            used.Add("Német: Szópárosítás", "igen");
                            Deutsch.SzoParositas();
                        }
                        if (intGERMAN == 4)
                        {
                            used.Add("Német: Szókereső", "igen");
                            Deutsch.SzoKereso();
                        }
                        if (intGERMAN == 5)
                        {
                            used.Add("Német: Mondat rendezés", "igen");
                            Deutsch.MondatRendezes();
                        }
                        if (intGERMAN == 6)
                        {
                            used.Add("Német: Helyes fordítás", "igen");
                            Deutsch.Helyesforditas();
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
                        Console.WriteLine(" 5. Mondat rendezése");
                        Console.WriteLine(" 6. Helyes fordítás");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intSPAIN = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intSPAIN == 1)
                        {
                            used.Add("Spanyol: ABC", "igen");
                            Spain.ABC();
                        }
                        if (intSPAIN == 2)
                        {
                            used.Add("Spanyol: Akasztófa", "igen");
                            Spain.Akasztofa();
                        }
                        if (intSPAIN == 3)
                        {
                            used.Add("Spanyol: Szópárosítás", "igen");
                            Spain.SzoParositas();
                        }
                        if (intSPAIN == 4)
                        {
                            used.Add("Spanyol: Szókereső", "igen");
                            Spain.SzoKereso();
                        }
                        if (intSPAIN == 5)
                        {
                            used.Add("Spanyol: Mondat rendezés", "igen");
                            Spain.MondatRendezes();
                        }
                        if (intSPAIN == 6)
                        {
                            used.Add("Spanyol: Helyes fordítás", "igen");
                            Spain.Helyesforditas();
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
                        Console.WriteLine(" 5. Mondat rendezése");
                        Console.WriteLine(" 6. Helyes fordítás");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intITALY = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intITALY == 1)
                        {
                            used.Add("Olasz: ABC", "igen");
                            Italy.ABC();
                        }
                        if (intITALY == 2)
                        {
                            used.Add("Olasz: Akasztófa", "igen");
                            Italy.Akasztofa();
                        }
                        if (intITALY == 3)
                        {
                            used.Add("Olasz: Szópárosítás", "igen");
                            Italy.SzoParositas();
                        }
                        if (intITALY == 4)
                        {
                            used.Add("Olasz: Szókereső", "igen");
                            Italy.SzoKereso();
                        }
                        if (intITALY == 5)
                        {
                            used.Add("Olasz: Mondat rendezés", "igen");
                            Italy.MondatRendezes();
                        }
                        if (intITALY == 6)
                        {
                            used.Add("Olasz: Helyes fordítás", "igen");
                            Italy.Helyesforditas();
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
                        Console.WriteLine(" 5. Mondat rendezése");
                        Console.WriteLine(" 6. Helyes fordítás");
                        Console.WriteLine("-----------------------------------------");
                        Console.Write(" Adja meg a választott feladat sorszámát: ");
                        int intFRENCH = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("-----------------------------------------");
                        if (intFRENCH == 1)
                        {
                            used.Add("Francia: ABC", "igen");
                            French.ABC();
                        }
                        if (intFRENCH == 2)
                        {
                            used.Add("Francia: Akasztófa", "igen");
                            French.Akasztofa();
                        }
                        if (intFRENCH == 3)
                        {
                            used.Add("Francia_: Szópárosítás", "igen");
                            French.SzoParositas();
                        }
                        if (intFRENCH == 4)
                        {
                            used.Add("Francia: Szókereső", "igen");
                            French.SzoKereso();
                        }
                        if (intFRENCH == 5)
                        {
                            used.Add("Francia: Mondat rendezés", "igen");
                            French.MondatRendezes();
                        }
                        if (intFRENCH == 6)
                        {
                            used.Add("Francia: Helyes fordítás", "igen");
                            French.Helyesforditas();
                        }
                        French.Pontok();
                        break;
                }
            }
            while (language != "x");
            {
                Console.WriteLine("Köszönjük " + username + ", hogy a Lingarixet választotta!");
                Console.WriteLine("Készítők: Gunics Bettina Virág, Páll Márk Hunor");
                
                int Score = English.Pontok() + Italy.Pontok() + French.Pontok() + Deutsch.Pontok() + Spain.Pontok();
                double elapsedHours = stopwatch.Elapsed.TotalMinutes;
                DateTime today = DateTime.Now;
                
                context.UserStatistics.Add(new UserStatistics
                {
                    Username = username,
                    Score = Score,
                    Date = today,
                    Exercises = string.Join(",", used.Keys.ToList()),
                    StudyTime = elapsedHours
                });
                context.SaveChanges();

                var leaderboardEntry = context.UserRangList.FirstOrDefault(l => l.Username == username);
                if (leaderboardEntry != null)
                {
                    leaderboardEntry.Points += Score;
                }
                else
                {
                    context.UserRangList.Add(new UserRangList
                    {
                        Username = username,
                        Points = Score
                    });
                }

                context.SaveChanges();

                AddAchievement(username, "Első 100 pont elérése");
                Console.WriteLine("Ranglista frissítve!");
                Console.WriteLine("Eredmény hozzáadva!");
                Console.WriteLine("Adatok sikeresen beszúrva az adatbázisba!");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("A gyakorlásod statisztikája:");
                Console.WriteLine($"Mai dátum: {today.ToString("yyyy-MM-dd")}");

                Console.WriteLine($"A program futásának ideje: {Math.Round(elapsedHours),4} perc");

                Console.WriteLine("Elért pontok:" + Score);
                Console.WriteLine("Elvégzett feladataid: ");
                foreach (var item in used)
                {
                    Console.Write($"{item.Key}: {item.Value}\n");
                }

            }
            Console.WriteLine("\nNyomj meg egy gombot a kilépéshez...");
            Console.ReadKey();
        }
        static void AddAchievement(string username, string achievementName)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=UsersDatabase;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Achievements (Username, AchievementName, DateEarned) VALUES (@Username, @AchievementName, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@AchievementName", achievementName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

