using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lingarix
{
    internal class Angol
    {
        //Idegennyelvű szavak
        List<string> family_english = new List<string>();
        List<string> it_english = new List<string>();
        List<string> travek_english = new List<string>();
        List<string> weather_english = new List<string>();
        List<string> home_english = new List<string>();

        //Magyar szavak
        List<string> family_hun = new List<string>();
        List<string> it_hun = new List<string>();
        List<string> travel_hun = new List<string>();
        List<string> weather_hun = new List<string>();
        List<string> home_hun = new List<string>();

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
        string right_answear;

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
        Random r = new Random();
        //A dokumentumból beolvassuk a szavakat és a témaköröket
        public void Beolvas(string felhasznalonev)
        {
            string[] datas = File.ReadAllLines("angol.txt");
            username = felhasznalonev;
            for (int i = 0; i < datas.Length; i++)
            {
                string[] sor = datas[i].Split(';');
                if (sor[2] == "család")
                {
                    family_english.Add(sor[0]);
                    family_hun.Add(sor[1]);
                }
                if (sor[2] == "informatika")
                {
                    it_english.Add(sor[0]);
                    it_hun.Add(sor[1]);
                }
                if (sor[2] == "utazás")
                {
                    travek_english.Add(sor[0]);
                    travel_hun.Add(sor[1]);
                }
                if (sor[2] == "időjárás")
                {
                    weather_english.Add(sor[0]);
                    weather_hun.Add(sor[1]);
                }
                if (sor[2] == "lakóhely")
                {
                    home_english.Add(sor[0]);
                    home_hun.Add(sor[1]);
                }
            }

            Console.WriteLine("--------------------");
        }
        //Első feladatunk: A-B-C lehetőség van a kiírt fordítás helyes megfejtésére
        public void ABC()
        {
            Random rnd = new Random();
            HashSet<int> used_index = new HashSet<int>(); // Tárolja a már kiválasztott szavakat

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

                List<string> topic_english = new List<string>();
                List<string> topic_hun = new List<string>();
                string temakor = "";

                switch (serial_number)
                {
                    case 1:
                        topic_english = family_english;
                        topic_hun = family_hun;
                        temakor = "család";
                        break;

                    case 2:
                        topic_english = it_english;
                        topic_hun = it_hun;
                        temakor = "informatika";
                        break;

                    case 3:
                        topic_english = travek_english;
                        topic_hun = travel_hun;
                        temakor = "utazás";
                        break;

                    case 4:
                        topic_english = weather_english;
                        topic_hun = weather_hun;
                        temakor = "időjárás";
                        break;

                    case 5:
                        topic_english = home_english;
                        topic_hun = home_hun;
                        temakor = "lakóhely";
                        break;
                }

                used_index.Clear(); // Minden új témánál töröljük a korábbi szavakat

                for (int i = 0; i < 5; i++) // 5 szó bekérése egy témából
                {
                    if (used_index.Count == topic_english.Count) // Ha minden szó elfogyott, újra kezdjük
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = rnd.Next(topic_english.Count);
                    } while (used_index.Contains(index)); // Ellenőrizzük, hogy ne ismétlődjön

                    used_index.Add(index); // Elmentjük a felhasznált indexet

                    word = topic_english[index];
                    hun_word = topic_hun[index];

                    Console.WriteLine("\nA {0} témában válassza ki a megfelelő választ az 'a/b/c' lehetőségek közül!", temakor);
                    Console.WriteLine("A angol szó: " + word);

                    List<string> another_HUN = topic_hun.Where((value, idx) => idx != index).ToList();
                    string another_HUN2 = another_HUN[rnd.Next(another_HUN.Count)];
                    another_HUN.Remove(another_HUN2);
                    string another_HUN3 = another_HUN[rnd.Next(another_HUN.Count)];

                    int helyesValaszIndex = rnd.Next(3);
                    string[] valaszok = { another_HUN2, another_HUN3, hun_word };
                    (valaszok[helyesValaszIndex], valaszok[2]) = (valaszok[2], valaszok[helyesValaszIndex]);

                    Console.WriteLine("a) " + valaszok[0]);
                    Console.WriteLine("b) " + valaszok[1]);
                    Console.WriteLine("c) " + valaszok[2]);

                    string answer = Console.ReadLine();
                    if (answer == new[] { "a", "b", "c" }[helyesValaszIndex])
                    {
                        scoreABC += 1;
                    }
                }

                Console.WriteLine("\nEddigi pontok: " + scoreABC + " pont\n");
            }
            while (true);

            Console.WriteLine("Köszönjük, hogy velünk tanultál " + username + "!");
        }

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

            int maxHP = 6;
            int rajzIndex = maxHP - Math.Max(0, Math.Min(maxHP, eletSzam));
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

                Random r = new Random();
                List<string> word_LIST = new List<string>();
                string temaNeve = "";

                switch (serial_number)
                {
                    case 1: word_LIST = family_english; temaNeve = "Család"; break;
                    case 2: word_LIST = it_english; temaNeve = "Informatika"; break;
                    case 3: word_LIST = travek_english; temaNeve = "Utazás"; break;
                    case 4: word_LIST = weather_english; temaNeve = "Időjárás"; break;
                    case 5: word_LIST = home_english; temaNeve = "Lakóhely"; break;
                    default: Console.WriteLine("Hibás választás!"); continue;
                }

                if (word_LIST.Count == 0)
                {
                    Console.WriteLine("Nincs elérhető szó ebben a témakörben!");
                    continue;
                }

                string WORD_finder = word_LIST[r.Next(word_LIST.Count)];
                int user_HP = WORD_finder.Length + 5;
                HashSet<char> kiprobaltBetuk = new HashSet<char>();
                HashSet<char> correct_aswers = new HashSet<char>();

                Console.WriteLine($"\nAkasztófa játék - Téma: {temaNeve}");
                Console.WriteLine($"A szó {WORD_finder.Length} betűből áll. Kezdésre {user_HP} életed van.");

                while (user_HP > 0)
                {
                    int missing_CHAR = 0;
                    Console.Write("\nSzó: ");
                    foreach (char kar in WORD_finder)
                    {
                        if (correct_aswers.Contains(kar))
                        {
                            Console.Write(kar);
                        }
                        else
                        {
                            Console.Write("_");
                            missing_CHAR++;
                        }
                    }
                    Console.WriteLine();

                    if (missing_CHAR == 0)
                    {
                        Console.WriteLine("\n Gratulálok! Megnyerted a játékot! 🎉");
                        scoreAKASZTOFA += WORD_finder.Length; // Pontszám növelése
                        break;
                    }

                    Console.Write("\nÍrj be egy betűt: ");
                    string guess = Console.ReadLine().ToLower();

                    if (string.IsNullOrWhiteSpace(guess) || guess.Length != 1 || !char.IsLetter(guess[0]))
                    {
                        Console.WriteLine(" Csak egy érvényes betűt adj meg!");
                        continue;
                    }

                    char guessed_char = guess[0];

                    if (kiprobaltBetuk.Contains(guessed_char))
                    {
                        Console.WriteLine(" Már próbálkoztál ezzel a betűvel!");
                        continue;
                    }

                    kiprobaltBetuk.Add(guessed_char);

                    if (WORD_finder.Contains(guessed_char))
                    {
                        Console.WriteLine($" A(z) '{guessed_char}' betű szerepel a szóban!");
                        correct_aswers.Add(guessed_char);
                    }
                    else
                    {
                        user_HP--;
                        Console.WriteLine($" Sajnos a(z) '{guessed_char}' nincs a szóban! {user_HP} életed maradt.");
                        RajzolAkasztofa(user_HP);
                    }
                }

                if (user_HP == 0)
                {
                    Console.WriteLine($"\n Vesztettél! A keresett szó: {WORD_finder}");
                }

                Console.WriteLine("\n***************************************");
                Console.WriteLine($"Jelenlegi pontszámod: {scoreAKASZTOFA} pont");
                Console.WriteLine("***************************************\n");

            } while (true);

            Console.WriteLine("Köszönjük, hogy velünk tanultál! ");
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

                List<string> English_List = new List<string>();
                List<string> Hun_list = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        English_List = family_english;
                        Hun_list = family_hun;
                        topic = "család";
                        break;
                    case 2:
                        English_List = it_english;
                        Hun_list = it_hun;
                        topic = "informatika";
                        break;
                    case 3:
                        English_List = travek_english;
                        Hun_list = travel_hun;
                        topic = "utazás";
                        break;
                    case 4:
                        English_List = weather_english;
                        Hun_list = weather_hun;
                        topic = "időjárás";
                        break;
                    case 5:
                        English_List = home_english;
                        Hun_list = home_hun;
                        topic = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                Console.WriteLine($"Adja meg az angol szó magyar megfelelőjét a {topic} témához kapcsolódva!");

                for (int i = 0; i < 5; i++)  // 5 szót kérünk le
                {
                    int index = rnd.Next(English_List.Count);  // Véletlenszerű index
                    string word = English_List[index];
                    string word_hun = Hun_list[index];

                    Console.Write(word + " -- ");
                    string answear = Console.ReadLine();

                    if (answear.Trim().ToLower() == word_hun.ToLower())
                    {
                        scorePAROSITAS += 1;
                        Console.WriteLine("  Helyes!");
                    }
                    else
                    {
                        Console.WriteLine($" Hibás! A helyes válasz: {word_hun}");
                    }
                }

                Console.WriteLine("\nEddigi pontok: " + scorePAROSITAS + " pont");
                Console.WriteLine();

            } while (serial_number != 7);
        }
        //Negyedik feladatunk: A felhasználó kiválasztotta a témakört és a szót megkapja ABC sorrendbe állítva, majd ezután kell helyes srrendben állítania őket, hogy megkaphassa a helyes megfejtést
        public void SzoKereso()
        {
            Random szam = new Random();

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

                List<string> topic_english = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        topic_english = family_english;
                        topic = "család";
                        break;

                    case 2:
                        topic_english = it_english;
                        topic = "informatika";
                        break;

                    case 3:
                        topic_english = travek_english;
                        topic = "utazás";
                        break;

                    case 4:
                        topic_english = weather_english;
                        topic = "időjárás";
                        break;

                    case 5:
                        topic_english = home_english;
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
                    if (used_index.Count == topic_english.Count)
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = szam.Next(topic_english.Count);
                    } while (used_index.Contains(index));

                    used_index.Add(index);
                    string word = topic_english[index];

                    char[] betuk = word.ToCharArray();
                    Array.Sort(betuk);
                    string abc_rendezett_betuk = new string(betuk);

                    Console.WriteLine("--------------------");
                    Console.WriteLine(abc_rendezett_betuk);
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