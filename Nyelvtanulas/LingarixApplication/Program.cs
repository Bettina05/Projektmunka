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
            // Adatbáziskapcsolat beállítása az Entity Framework számára
            var optionsBuilder = new DbContextOptionsBuilder<LingarixDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsersDatabase;Trusted_Connection=True;");
            var context = new LingarixDbContext(optionsBuilder.Options);

            // Felhasználónév inicializálása (parancssori argumentumból)
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

            // Nyelvi osztályok példányosítása
            Angol English = new Angol();
            Olasz Italy = new Olasz();
            Francia French = new Francia();
            Spanyol Spain = new Spanyol();
            Nemet Deutsch = new Nemet();

            // Statisztikai és naplózási adatok inicializálása
            Dictionary<string, string> used = new Dictionary<string, string>();
            int totalExercises = 0;
            int totalPoints = 0;
            List<string> languagesUsed = new List<string>();
            List<string> exerciseTypesCompleted = new List<string>();
            HashSet<string> taskTypes = new HashSet<string>();
            string language;
            Stopwatch stopwatch = Stopwatch.StartNew();

            // A fő programciklus - nyelvválasztás és feladattípusok kezelése
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
                language = Console.ReadLine().ToLower().Trim();
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
                            taskTypes.Add("ABC");
                            totalExercises++;
                            totalPoints += English.Pontok();
                            languagesUsed.Add("Angol");
                            exerciseTypesCompleted.Add("ABC");
                        }
                        if (intENGLISH == 2)
                        {
                            used.Add("Angol: Akasztófa", "igen");
                            English.Akasztofa();
                            taskTypes.Add("Akasztófa");
                            totalExercises++;
                            totalPoints += English.Pontok();
                            languagesUsed.Add("Angol");
                            exerciseTypesCompleted.Add("Akasztófa");
                        }
                        if (intENGLISH == 3)
                        {
                            used.Add("Angol: Szópárosítás", "igen");
                            English.SzoParositas();
                            taskTypes.Add("Szópárosítás");
                            totalExercises++;
                            totalPoints += English.Pontok();
                            languagesUsed.Add("Angol");
                            exerciseTypesCompleted.Add("Szópárosítás");
                        }
                        if (intENGLISH == 4)
                        {
                            used.Add("Angol: Szókereső", "igen");
                            English.SzoKereso();
                            taskTypes.Add("Szókereső");
                            totalExercises++;
                            totalPoints += English.Pontok();
                            languagesUsed.Add("Angol");
                            exerciseTypesCompleted.Add("Szókereső");
                        }
                        if (intENGLISH == 5)
                        {
                            used.Add("Angol: Mondat rendezés", "igen");
                            English.MondatRendezes();
                            taskTypes.Add("Mondat rendezés");
                            totalExercises++;
                            totalPoints += English.Pontok();
                            languagesUsed.Add("Angol");
                            exerciseTypesCompleted.Add("Mondat rendezés");
                        }
                        if (intENGLISH == 6)
                        {
                            used.Add("Angol: Helyes fordítás", "igen");
                            English.Helyesforditas();
                            taskTypes.Add("Helyes fordítás");
                            totalExercises++;
                            totalPoints += English.Pontok();
                            languagesUsed.Add("Angol");
                            exerciseTypesCompleted.Add("Helyes fordítás");
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
                            taskTypes.Add("ABC");
                            totalExercises++;
                            totalPoints += Deutsch.Pontok();
                            languagesUsed.Add("Német");
                            exerciseTypesCompleted.Add("ABC");
                        }
                        if (intGERMAN == 2)
                        {
                            used.Add("Német: Akasztófa", "igen");
                            Deutsch.Akasztofa();
                            taskTypes.Add("Akasztófa");
                            totalExercises++;
                            totalPoints += Deutsch.Pontok();
                            languagesUsed.Add("Német");
                            exerciseTypesCompleted.Add("Akasztófa");
                        }
                        if (intGERMAN == 3)
                        {
                            used.Add("Német: Szópárosítás", "igen");
                            Deutsch.SzoParositas();
                            taskTypes.Add("Szópárosítás");
                            totalExercises++;
                            totalPoints += Deutsch.Pontok();
                            languagesUsed.Add("Német");
                            exerciseTypesCompleted.Add("Szópárosítás");
                        }
                        if (intGERMAN == 4)
                        {
                            used.Add("Német: Szókereső", "igen");
                            Deutsch.SzoKereso();
                            taskTypes.Add("Szókereső");
                            totalExercises++;
                            totalPoints += Deutsch.Pontok();
                            languagesUsed.Add("Német");
                            exerciseTypesCompleted.Add("Szókereső");
                        }
                        if (intGERMAN == 5)
                        {
                            used.Add("Német: Mondat rendezés", "igen");
                            Deutsch.MondatRendezes();
                            taskTypes.Add("Mondat rendezés");
                            totalExercises++;
                            totalPoints += Deutsch.Pontok();
                            languagesUsed.Add("Német");
                            exerciseTypesCompleted.Add("Mondat rendezés");
                        }
                        if (intGERMAN == 6)
                        {
                            used.Add("Német: Helyes fordítás", "igen");
                            Deutsch.Helyesforditas();
                            taskTypes.Add("Helyes fordítás");
                            totalExercises++;
                            totalPoints += Deutsch.Pontok();
                            languagesUsed.Add("Német");
                            exerciseTypesCompleted.Add("Helyes fordítás");
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
                            taskTypes.Add("ABC");
                            totalExercises++;
                            totalPoints += Spain.Pontok();
                            languagesUsed.Add("Spanyol");
                            exerciseTypesCompleted.Add("ABC");
                        }
                        if (intSPAIN == 2)
                        {
                            used.Add("Spanyol: Akasztófa", "igen");
                            Spain.Akasztofa();
                            taskTypes.Add("Akasztófa");
                            totalExercises++;
                            totalPoints += Spain.Pontok();
                            languagesUsed.Add("Spanyol");
                            exerciseTypesCompleted.Add("Akasztófa");
                        }
                        if (intSPAIN == 3)
                        {
                            used.Add("Spanyol: Szópárosítás", "igen");
                            Spain.SzoParositas();
                            taskTypes.Add("Szópárosítás");
                            totalExercises++;
                            totalPoints += Spain.Pontok();
                            languagesUsed.Add("Spanyol");
                            exerciseTypesCompleted.Add("Szópárosítás");
                        }
                        if (intSPAIN == 4)
                        {
                            used.Add("Spanyol: Szókereső", "igen");
                            Spain.SzoKereso();
                            taskTypes.Add("Szókereső");
                            totalExercises++;
                            totalPoints += Spain.Pontok();
                            languagesUsed.Add("Spanyol");
                            exerciseTypesCompleted.Add("Szókereső");
                        }
                        if (intSPAIN == 5)
                        {
                            used.Add("Spanyol: Mondat rendezés", "igen");
                            Spain.MondatRendezes();
                            taskTypes.Add("Mondat rendezés");
                            totalExercises++;
                            totalPoints += Spain.Pontok();
                            languagesUsed.Add("Spanyol");
                            exerciseTypesCompleted.Add("Mondat rendezés");
                        }
                        if (intSPAIN == 6)
                        {
                            used.Add("Spanyol: Helyes fordítás", "igen");
                            Spain.Helyesforditas();
                            taskTypes.Add("Helyes fordítás");
                            totalExercises++;
                            totalPoints += Spain.Pontok();
                            languagesUsed.Add("Spanyol");
                            exerciseTypesCompleted.Add("Helyes fordítás");
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
                            taskTypes.Add("ABC");
                            totalExercises++;
                            totalPoints += Italy.Pontok();
                            languagesUsed.Add("Olasz");
                            exerciseTypesCompleted.Add("ABC");
                        }
                        if (intITALY == 2)
                        {
                            used.Add("Olasz: Akasztófa", "igen");
                            Italy.Akasztofa();
                            taskTypes.Add("Akasztófa");
                            totalExercises++;
                            totalPoints += Italy.Pontok();
                            languagesUsed.Add("Olasz");
                            exerciseTypesCompleted.Add("Akasztófa");
                        }
                        if (intITALY == 3)
                        {
                            used.Add("Olasz: Szópárosítás", "igen");
                            Italy.SzoParositas();
                            taskTypes.Add("Szópárosítás");
                            totalExercises++;
                            totalPoints += Italy.Pontok();
                            languagesUsed.Add("Olasz");
                            exerciseTypesCompleted.Add("Szópárosítás");
                        }
                        if (intITALY == 4)
                        {
                            used.Add("Olasz: Szókereső", "igen");
                            Italy.SzoKereso();
                            taskTypes.Add("Szókereső");
                            totalExercises++;
                            totalPoints += Italy.Pontok();
                            languagesUsed.Add("Olasz");
                            exerciseTypesCompleted.Add("Szókereső");
                        }
                        if (intITALY == 5)
                        {
                            used.Add("Olasz: Mondat rendezés", "igen");
                            Italy.MondatRendezes();
                            taskTypes.Add("Mondat rendezés");
                            totalExercises++;
                            totalPoints += Italy.Pontok();
                            languagesUsed.Add("Olasz");
                            exerciseTypesCompleted.Add("Mondat rendezés");
                        }
                        if (intITALY == 6)
                        {
                            used.Add("Olasz: Helyes fordítás", "igen");
                            Italy.Helyesforditas();
                            taskTypes.Add("Helyes fordítás");
                            totalExercises++;
                            totalPoints += Italy.Pontok();
                            languagesUsed.Add("Olasz");
                            exerciseTypesCompleted.Add("Helyes fordítás");
                        }
                        Italy.Pontok();
                        break;

                    case "francia":
                        French.Beolvas(username);
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
                            taskTypes.Add("ABC");
                            totalExercises++;
                            totalPoints += French.Pontok();
                            languagesUsed.Add("Francia");
                            exerciseTypesCompleted.Add("ABC");
                        }
                        if (intFRENCH == 2)
                        {
                            used.Add("Francia: Akasztófa", "igen");
                            French.Akasztofa();
                            taskTypes.Add("Akasztófa");
                            totalExercises++;
                            totalPoints += French.Pontok();
                            languagesUsed.Add("Francia");
                            exerciseTypesCompleted.Add("Akasztófa");
                        }
                        if (intFRENCH == 3)
                        {
                            used.Add("Francia_: Szópárosítás", "igen");
                            French.SzoParositas();
                            taskTypes.Add("Szópárosítás");
                            totalExercises++;
                            totalPoints += French.Pontok();
                            languagesUsed.Add("Francia");
                            exerciseTypesCompleted.Add("Szópárosítás");
                        }
                        if (intFRENCH == 4)
                        {
                            used.Add("Francia: Szókereső", "igen");
                            French.SzoKereso();
                            taskTypes.Add("Szókereső");
                            totalExercises++;
                            totalPoints += French.Pontok();
                            languagesUsed.Add("Francia");
                            exerciseTypesCompleted.Add("Szókereső");
                        }
                        if (intFRENCH == 5)
                        {
                            used.Add("Francia: Mondat rendezés", "igen");
                            French.MondatRendezes();
                            taskTypes.Add("Mondat rendezés");
                            totalExercises++;
                            totalPoints += French.Pontok();
                            languagesUsed.Add("Francia");
                            exerciseTypesCompleted.Add("Mondat rendezés");
                        }
                        if (intFRENCH == 6)
                        {
                            used.Add("Francia: Helyes fordítás", "igen");
                            French.Helyesforditas();
                            taskTypes.Add("Helyes fordítás");
                            totalExercises++;
                            totalPoints += French.Pontok();
                            languagesUsed.Add("Francia");
                            exerciseTypesCompleted.Add("Helyes fordítás");
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

                // Statisztikák beszúrása az adatbázisba
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

                // Achievementek kezelése
                if (totalExercises >= 1)
                    AddAchievement(username, "Megoldott 1 feladatot");
                if (totalExercises >= 10)
                    AddAchievement(username, "Megoldott 10 feladatot");
                if (languagesUsed.Distinct().Count() >= 3)
                    AddAchievement(username, "Gyakorolt több nyelven");
                if (taskTypes.Distinct().Count() >= 3)
                    AddAchievement(username, "A változatosság mestere");
                if (totalPoints >= 10)
                    AddAchievement(username, "Szerzett 10 pontot");
                if (totalPoints >= 100)
                    AddAchievement(username, "Szerzett 100 pontot");
                if (totalPoints >= 500)
                    AddAchievement(username, "Szerzett 500 pontot");

                // Visszajelzés a felhasználónak
                Console.WriteLine("Ranglista frissítve!");
                Console.WriteLine("Eredmény hozzáadva!");
                Console.WriteLine("Adatok sikeresen beszúrva az adatbázisba!");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("A gyakorlásod statisztikája:");
                Console.WriteLine($"Mai dátum: {today.ToString("yyyy-MM-dd")}");

                Console.WriteLine($"A program futásának ideje: {Math.Round(elapsedHours,4)} perc");

                Console.WriteLine("Elért pontok:" + Score);
                Console.WriteLine("Elvégzett feladataid: ");
                foreach (var item in used)
                {
                    Console.Write($"{item.Key}: {item.Value}\n");
                }

            }
            Console.WriteLine("\nNyomj meg egy gombot a kilépéshez...");
            Console.ReadKey(true);
        }
        static void AddAchievement(string username, string achievementName)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=UsersDatabase;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Ellenőrzés: a felhasználó már megszerezte ezt az achievementet?
                string checkQuery = "SELECT COUNT(*) FROM Achievements WHERE Username = @Username AND AchievementName = @AchievementName";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Username", username);
                    checkCmd.Parameters.AddWithValue("@AchievementName", achievementName);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                        return; // Ha már létezik, nem adjuk hozzá újra
                }

                // Új achievement beszúrása
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

