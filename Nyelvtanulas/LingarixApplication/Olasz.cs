using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lingarix
{
    internal class Olasz
    {
        //Idegennyelvű szavak
        List<string> family_italy = new List<string>();
        List<string> it_italy = new List<string>();
        List<string> travel_italy = new List<string>();
        List<string> weathe_italy = new List<string>();
        List<string> home_italy = new List<string>();

        //Magyar szavak
        List<string> family_hun = new List<string>();
        List<string> it_hun = new List<string>();
        List<string> travel_hun = new List<string>();
        List<string> weather_hun = new List<string>();
        List<string> home_travel = new List<string>();

        ///<summary>
        /// A felhasználó felhasználóneve
        /// </summary>
        string username = "";
        /// <summary>
        /// A pontokat itt számoljuk az ABC feladatnál
        /// </summary>
        int scoreABC;

        /// <summary>
        /// A pontokat itt számoljuk a szópárosításos feladatnál
        /// </summary>
        int scorePAROSITAS;

        /// <summary>
        /// A pontokat itt számoljuk az akasztófa feladathoz
        /// </summary>
        int scoreAKASZTOFA;

        /// <summary>
        /// A pontokat itt számoljuk a szókereső feladathoz
        /// </summary>
        int scoreSZOKERESO;

        /// <summary>
        /// Itt adják meg a feladat sorszámát
        /// </summary>
        int serial_number;

        /// <summary>
        /// A kitalálandó szavakat tároljuk benne, minden egyes feladatban idegennyelvű
        /// </summary>
        string word = "";

        /// <summary>
        /// A kitalálandó szavak magyar megfelelőjét, megfejtését tároljuk benne
        /// </summary>
        string answear;

        /// <summary>
        /// A kitalálandó szavakat tároljuk benne, minden egyes feladatban magyar nyelvű
        /// </summary>
        string hun_word;

        int index = 0;

        /// <summary>
        /// A szópárosításhoz tartozó szavak betűi abc sorrendbe rendezve
        /// </summary>
        string abc_sorted_char;

        /// <summary>
        /// A szópárosításhoz tartozó megoldás a felhasználó által beírva
        /// </summary>
        string word_finder;

        /// <summary>
        /// Témaköröket tároljuk benne
        /// </summary>
        string topic = "";

        /// <summary>
        /// Megakadályozza, hogy a megfejtendő szavak ismétlődjenek
        /// </summary>
        static Random rnd = new Random();
        //A dokumentumból beolvassuk a szavakat és a témaköröket
        public void Beolvas(string felhasznalonev)
        {
            string[] datas = File.ReadAllLines("olasz.txt");
            username = felhasznalonev;
            for (int i = 0; i < datas.Length; i++)
            {
                string[] sor = datas[i].Split(';');
                if (sor[2] == "család")
                {
                    family_italy.Add(sor[0]);
                    family_hun.Add(sor[1]);
                }
                if (sor[2] == "informatika")
                {
                    it_italy.Add(sor[0]);
                    it_hun.Add(sor[1]);
                }
                if (sor[2] == "utazás")
                {
                    travel_italy.Add(sor[0]);
                    travel_hun.Add(sor[1]);
                }
                if (sor[2] == "időjárás")
                {
                    weathe_italy.Add(sor[0]);
                    weather_hun.Add(sor[1]);
                }
                if (sor[2] == "lakóhely")
                {
                    home_italy.Add(sor[0]);
                    home_travel.Add(sor[1]);
                }
            }

            Console.WriteLine("--------------------");
        }
        //Első feladatunk: A-B-C lehetőség van a kiírt fordítás helyes megfejtésére
        public void ABC()
        {
            Random rnd = new Random();
            HashSet<int> usedn_index = new HashSet<int>(); // Tárolja a már kiválasztott szavakat
            do
            {
                Console.WriteLine("  Témák:  ");
                Console.WriteLine("**********");
                Console.WriteLine("1.  Család       (Könnyű)");
                Console.WriteLine("2.  Info         (Könnyű)");
                Console.WriteLine("3.  Utazás       (Közepes)");
                Console.WriteLine("4.  Időjárás     (Közepes)");
                Console.WriteLine("5.  Lakóhely     (Nehéz)");
                Console.WriteLine("6.  Kilépés      ");
                Console.WriteLine("--------------------");
                Console.Write(" -- Téma sorszáma: ");
                serial_number = Convert.ToInt16(Console.ReadLine());

                if (serial_number == 6) break;

                List<string> topic_italy = new List<string>();
                List<string> topic_hun = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        topic_italy = family_italy;
                        topic_hun = family_hun;
                        topic = "család";
                        break;

                    case 2:
                        topic_italy = it_italy;
                        topic_hun = it_hun;
                        topic = "informatika";
                        break;

                    case 3:
                        topic_italy = travel_italy;
                        topic_hun = travel_hun;
                        topic = "utazás";
                        break;

                    case 4:
                        topic_italy = weathe_italy;
                        topic_hun = weather_hun;
                        topic = "időjárás";
                        break;

                    case 5:
                        topic_italy = home_italy;
                        topic_hun = home_travel;
                        topic = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                usedn_index.Clear(); // Minden új témánál töröljük a korábbi szavakat

                for (int i = 0; i < 5; i++) // 5 szó bekérése egy témából
                {
                    if (usedn_index.Count == topic_italy.Count) // Ha minden szó elfogyott, újra kezdjük
                    {
                        usedn_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = rnd.Next(topic_italy.Count);
                    } while (usedn_index.Contains(index)); // Ellenőrizzük, hogy ne ismétlődjön

                    usedn_index.Add(index); // Elmentjük a felhasznált indexet

                    word = topic_italy[index];
                    hun_word = topic_hun[index];

                    Console.WriteLine("\nA {0} témában válassza ki a megfelelő választ az 'a/b/c' lehetőségek közül!", topic);
                    Console.WriteLine("A olasz szó: " + word);

                    List<string> another_HUN = topic_hun.Where((value, idx) => idx != index).ToList();
                    string another_HUN2 = another_HUN[rnd.Next(another_HUN.Count)];
                    another_HUN.Remove(another_HUN2);
                    string antoher_HUN3 = another_HUN[rnd.Next(another_HUN.Count)];

                    int correct_index = rnd.Next(3);
                    string[] answears = { another_HUN2, antoher_HUN3, hun_word };
                    (answears[correct_index], answears[2]) = (answears[2], answears[correct_index]);

                    Console.WriteLine("a) " + answears[0]);
                    Console.WriteLine("b) " + answears[1]);
                    Console.WriteLine("c) " + answears[2]);

                    string answear = Console.ReadLine();
                    if (answear == new[] { "a", "b", "c" }[correct_index])
                    {
                        scoreABC += 1;
                    }
                }

                Console.WriteLine("\nEddigi pontok: " + scoreABC + " pont\n");
            }
            while (true);

            Console.WriteLine("Köszönjük, hogy velünk tanultál " + username + "!");
        }

        //Második feladatunk: Az idegennyelvű szót megjelenítjük a felhasználónak, majd a magyar megfelelőjét kell begépelnie
        public void SzoParositas()
        {
            Random rnd = new Random();

            do
            {
                Console.WriteLine("  Témák:  ");
                Console.WriteLine("**********");
                Console.WriteLine("1.  Család       (Könnyű)");
                Console.WriteLine("2.  Info         (Könnyű)");
                Console.WriteLine("3.  Utazás       (Közepes)");
                Console.WriteLine("4.  Időjárás     (Közepes)");
                Console.WriteLine("5.  Lakóhely     (Nehéz)");
                Console.WriteLine("6.  Kilépés      ");
                Console.WriteLine("--------------------");
                Console.Write(" -- Téma sorszáma: ");
                serial_number = Convert.ToInt16(Console.ReadLine());

                List<string> itali_list = new List<string>();
                List<string> hun_list = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        itali_list = family_italy;
                        hun_list = family_hun;
                        topic = "család";
                        break;
                    case 2:
                        itali_list = it_italy;
                        hun_list = it_hun;
                        topic = "informatika";
                        break;
                    case 3:
                        itali_list = travel_italy;
                        hun_list = travel_hun;
                        topic = "utazás";
                        break;
                    case 4:
                        itali_list = weathe_italy;
                        hun_list = weather_hun;
                        topic = "időjárás";
                        break;
                    case 5:
                        itali_list = home_italy;
                        hun_list = home_travel;
                        topic = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                Console.WriteLine($"Adja meg az olasz szó magyar megfelelőjét a {topic} témához kapcsolódva!");

                for (int i = 0; i < 5; i++)  // 5 szót kérünk le
                {
                    int index = rnd.Next(itali_list.Count);  // Véletlenszerű index
                    string word = itali_list[index];
                    string hun_word = hun_list[index];

                    Console.Write(word + " -- ");
                    string answear = Console.ReadLine();

                    if (answear.Trim().ToLower() == hun_word.ToLower())
                    {
                        scorePAROSITAS += 1;
                        Console.WriteLine(" Helyes!");
                    }
                    else
                    {
                        Console.WriteLine($" Hibás! A helyes válasz: {hun_word}");
                    }
                }

                Console.WriteLine("\nEddigi pontok: " + scorePAROSITAS + " pont");
                Console.WriteLine();

            } while (serial_number != 7);
        }
        //Harmadik feladatunk: A felhasználó kiválasztotta a témakört és csak a szó hosszát látja a kijelzőn, - jelekkel helyettesítve az egyes betűket
        public void RajzolAkasztofa(int eletSzam)
        {
            string[] akasztofaRajz = {
        "  +---+  \n  |   |  \n      |  \n      |  \n      |  \n      |  \n=========",
        "  +---+  \n  |   |  \n  O   |  \n      |  \n      |  \n      |  \n=========",
        "  +---+  \n  |   |  \n  O   |  \n  |   |  \n      |  \n      |  \n=========",
        "  +---+  \n  |   |  \n  O   |  \n /|   |  \n      |  \n      |  \n=========",
        "  +---+  \n  |   |  \n  O   |  \n /|\\  |  \n      |  \n      |  \n=========",
        "  +---+  \n  |   |  \n  O   |  \n /|\\  |  \n /    |  \n      |  \n=========",
        "  +---+  \n  |   |  \n  O   |  \n /|\\  |  \n / \\  |  \n      |  \n========="
    };

            int max_HP = 6;
            int rajzIndex = max_HP - Math.Max(0, Math.Min(max_HP, eletSzam));
            Console.WriteLine(akasztofaRajz[rajzIndex]);
        }
        public void Akasztofa()
        {
            do
            {
                Console.WriteLine("  Témák:  ");
                Console.WriteLine("**********");
                Console.WriteLine("1.  Család       (Könnyű)");
                Console.WriteLine("2.  Info         (Könnyű)");
                Console.WriteLine("3.  Utazás       (Közepes)");
                Console.WriteLine("4.  Időjárás     (Közepes)");
                Console.WriteLine("5.  Lakóhely     (Nehéz)");
                Console.WriteLine("6.  Kilépés      ");
                Console.WriteLine("--------------------");
                Console.Write(" -- Téma sorszáma: ");
                serial_number = Convert.ToInt16(Console.ReadLine());

                if (serial_number == 6) break; // Kilépés esetén kilépünk a ciklusból

                Random rnd = new Random();
                List<string> word_list = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1: word_list = family_italy; topic = "Család"; break;
                    case 2: word_list = it_italy; topic = "Informatika"; break;
                    case 3: word_list = travel_italy; topic = "Utazás"; break;
                    case 4: word_list = weathe_italy; topic = "Időjárás"; break;
                    case 5: word_list = home_italy; topic = "Lakóhely"; break;
                    default: Console.WriteLine("Hibás választás!"); continue;
                }

                if (word_list.Count == 0)
                {
                    Console.WriteLine("Nincs elérhető szó ebben a témakörben!");
                    continue;
                }

                string missing_word = word_list[rnd.Next(word_list.Count)];
                int HP = missing_word.Length + 5;
                HashSet<char> used_chars = new HashSet<char>();
                HashSet<char> correct_chars = new HashSet<char>();

                Console.WriteLine($"\nAkasztófa játék - Téma: {topic}");
                Console.WriteLine($"A szó {missing_word.Length} betűből áll. Kezdésre {HP} életed van.");

                while (HP > 0)
                {
                    int missing_char_index = 0;
                    Console.Write("\nSzó: ");
                    foreach (char kar in missing_word)
                    {
                        if (correct_chars.Contains(kar))
                        {
                            Console.Write(kar);
                        }
                        else
                        {
                            Console.Write("_");
                            missing_char_index++;
                        }
                    }
                    Console.WriteLine();

                    if (missing_char_index == 0)
                    {
                        Console.WriteLine("\n Gratulálok! Megnyerted a játékot! ");
                        scoreAKASZTOFA += missing_word.Length; // Pontszám növelése
                        break;
                    }

                    Console.Write("\nÍrj be egy betűt: ");
                    string hint = Console.ReadLine().ToLower();

                    if (string.IsNullOrWhiteSpace(hint) || hint.Length != 1 || !char.IsLetter(hint[0]))
                    {
                        Console.WriteLine(" Csak egy érvényes betűt adj meg!");
                        continue;
                    }

                    char betu = hint[0];

                    if (used_chars.Contains(betu))
                    {
                        Console.WriteLine(" Már próbálkoztál ezzel a betűvel!");
                        continue;
                    }

                    used_chars.Add(betu);

                    if (missing_word.Contains(betu))
                    {
                        Console.WriteLine($" A(z) '{betu}' betű szerepel a szóban!");
                        correct_chars.Add(betu);
                    }
                    else
                    {
                        HP--;
                        Console.WriteLine($" Sajnos a(z) '{betu}' nincs a szóban! {HP} életed maradt.");
                        RajzolAkasztofa(HP);
                    }
                }

                if (HP == 0)
                {
                    Console.WriteLine($"\n Vesztettél! A keresett szó: {missing_word}");
                }

                Console.WriteLine("\n***************************************");
                Console.WriteLine($"Jelenlegi pontszámod: {scoreAKASZTOFA} pont");
                Console.WriteLine("***************************************\n");

            } while (true);

            Console.WriteLine("Köszönjük, hogy velünk tanultál! ");
        }
        //Negyedik feladatunk: A felhasználó kiválasztotta a témakört és a szót megkapja ABC sorrendbe állítva, majd ezután kell helyes srrendben állítania őket, hogy megkaphassa a helyes megfejtést
        public void SzoKereso()
        {
            Random rnd = new Random();

            do
            {
                Console.WriteLine("  Témák:  ");
                Console.WriteLine("**********");
                Console.WriteLine("1.  Család       (Könnyű)");
                Console.WriteLine("2.  Info         (Könnyű)");
                Console.WriteLine("3.  Utazás       (Közepes)");
                Console.WriteLine("4.  Időjárás     (Közepes)");
                Console.WriteLine("5.  Lakóhely     (Nehéz)");
                Console.WriteLine("6.  Kilépés      ");
                Console.WriteLine("--------------------");
                Console.Write(" -- Téma sorszáma: ");
                serial_number = Convert.ToInt16(Console.ReadLine());

                if (serial_number == 6) break;

                List<string> topic_italy = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        topic_italy = family_italy;
                        topic = "család";
                        break;

                    case 2:
                        topic_italy = it_italy;
                        topic = "informatika";
                        break;

                    case 3:
                        topic_italy = travel_italy;
                        topic = "utazás";
                        break;

                    case 4:
                        topic_italy = weathe_italy;
                        topic = "időjárás";
                        break;

                    case 5:
                        topic_italy = home_italy;
                        topic = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                HashSet<int> used_index = new HashSet<int>();

                Console.WriteLine("\nTémakör: " + topic);
                Console.WriteLine("A megadott betűk alapján találja ki a szót és írja be a kijelölt helyre!");

                for (int i = 0; i < 5; i++) // 5 különböző szó kiválasztása
                {
                    if (used_index.Count == topic_italy.Count)
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = rnd.Next(topic_italy.Count);
                    } while (used_index.Contains(index));

                    used_index.Add(index);
                    string word = topic_italy[index];

                    char[] betuk = word.ToCharArray();
                    Array.Sort(betuk);
                    string abc_sorted_chars = new string(betuk);

                    Console.WriteLine("--------------------");
                    Console.WriteLine(abc_sorted_chars);
                    Console.WriteLine("--------------------");

                    string word_finder = Console.ReadLine();

                    if (word_finder == word)
                    {
                        scoreSZOKERESO++;
                        Console.WriteLine(" Helyes válasz!");
                    }
                    else
                    {
                        Console.WriteLine("********************");
                        Console.WriteLine("Nem jó :(");
                        Console.WriteLine("A helyes válasz: " + word);
                        Console.WriteLine("********************");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Eddigi pontok: " + scoreSZOKERESO + " pont");
                Console.WriteLine();

            } while (true);

            Console.WriteLine($"Köszönjük {username}, hogy a Lingarixel tanultál!");
            Console.WriteLine($"{username} összesen {Pontok()} pontot gyűjtött! Gratulálunk :)");
        }
        //Összeszámolja, hogy összesen hány pontot gyűjtött a felhasználó a gyakorlással
        public int Pontok()
        {
            int pontok = scoreABC + scoreAKASZTOFA + scorePAROSITAS + scoreSZOKERESO;
            return pontok;
        }
    }
}