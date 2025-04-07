using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lingarix
{
    internal class Angol
    {
        List<string> family_english = new List<string>();
        List<string> it_english = new List<string>();
        List<string> travek_english = new List<string>();
        List<string> weather_english = new List<string>();
        List<string> home_english = new List<string>();

        List<string> family_hun = new List<string>();
        List<string> it_hun = new List<string>();
        List<string> travel_hun = new List<string>();
        List<string> weather_hun = new List<string>();
        List<string> home_hun = new List<string>();

        string username = "";
        string word = "";
        string hun_word;
        int scoreABC;
        int scorePAROSITAS;
        int scoreAKASZTOFA;
        int scoreSZOKERESO;
        int scoreMONDATRENDEZES;
        int scoreHELYESFORDITAS;
        int serial_number;

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
        
        //1. feladatunk: A-B-C lehetőség van a kiírt fordítás helyes megfejtésére
        public void ABC()
        {
            Random rnd = new Random();
            HashSet<int> used_index = new HashSet<int>();
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
                    case 1: topic_english = family_english;     topic_hun = family_hun;     temakor = "család";         break;
                    case 2: topic_english = it_english;         topic_hun = it_hun;         temakor = "informatika";    break;
                    case 3: topic_english = travek_english;     topic_hun = travel_hun;     temakor = "utazás";         break;
                    case 4: topic_english = weather_english;    topic_hun = weather_hun;    temakor = "időjárás";       break;
                    case 5: topic_english = home_english;       topic_hun = home_hun;       temakor = "lakóhely";       break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }
                used_index.Clear();

                for (int i = 0; i < 5; i++)
                {
                    if (used_index.Count == topic_english.Count)
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = rnd.Next(topic_english.Count);
                    } while (used_index.Contains(index));

                    used_index.Add(index);

                    word = topic_english[index];
                    hun_word = topic_hun[index];
                    Console.WriteLine("A - B - C feladat: A három lehetőség közül kell kiválasztania a helyes fordítást a megadott szóra.");
                    Console.WriteLine("\nA {0} témát választotta.", temakor);
                    Console.WriteLine("Az angol szó: " + word);

                    List<string> another_HUN = topic_hun.Where((value, idx) => idx != index).ToList();
                    string another_HUN2 = another_HUN[rnd.Next(another_HUN.Count)];
                    another_HUN.Remove(another_HUN2);
                    string another_HUN3 = another_HUN[rnd.Next(another_HUN.Count)];

                    int correct_answer_index = rnd.Next(3);
                    string[] answers = { another_HUN2, another_HUN3, hun_word };
                    (answers[correct_answer_index], answers[2]) = (answers[2], answers[correct_answer_index]);

                    Console.WriteLine("a) " + answers[0]);
                    Console.WriteLine("b) " + answers[1]);
                    Console.WriteLine("c) " + answers[2]);

                    string answer = Console.ReadLine();
                    if (answer == new[] { "a", "b", "c" }[correct_answer_index])
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
        
        //2. feladatunk: Akasztófa játék, a felhasználónak kell kitalálnia a szót
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

                if (serial_number == 6) break;

                Random r = new Random();
                List<string> word_LIST = new List<string>();
                string temaNeve = "";

                switch (serial_number)
                {
                    case 1: word_LIST = family_english;     temaNeve = "Család";        break;
                    case 2: word_LIST = it_english;         temaNeve = "Informatika";   break;
                    case 3: word_LIST = travek_english;     temaNeve = "Utazás";        break;
                    case 4: word_LIST = weather_english;    temaNeve = "Időjárás";      break;
                    case 5: word_LIST = home_english;       temaNeve = "Lakóhely";      break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
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
                Console.WriteLine("Akasztófa játék: A felhasználónak ki kell találnia a keresett szót betűnként tippelve,\n akárcsak a klasszikus akasztófajátékban.");
                Console.WriteLine($"\nTéma: {temaNeve}");
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
                        Console.WriteLine("\n Gratulálok! Megnyerted a játékot!");
                        scoreAKASZTOFA += WORD_finder.Length;
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

            } 
            while (true);

            Console.WriteLine("Köszönjük, hogy velünk tanultál! ");
        }

        //3. feladatunk: Az idegennyelvű szót megjelenítjük a felhasználónak, majd a magyar megfelelőjét kell begépelnie
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
                    case 1: English_List = family_english;  Hun_list = family_hun;  topic = "család";       break;
                    case 2: English_List = it_english;      Hun_list = it_hun;      topic = "informatika";  break;
                    case 3: English_List = travek_english;  Hun_list = travel_hun;  topic = "utazás";       break;
                    case 4: English_List = weather_english; Hun_list = weather_hun; topic = "időjárás";     break;
                    case 5: English_List = home_english;    Hun_list = home_hun;    topic = "lakóhely";     break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }
                Console.WriteLine("Szópárosítás: A program megjeleníti az idegen nyelvű szót, és a felhasználónak be kell írnia a helyes magyar megfelelőjét.");
                Console.WriteLine($"A {topic} témát választotta!");

                for (int i = 0; i < 5; i++)
                {
                    int index = rnd.Next(English_List.Count);
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
        
        //4. feladatunk: A felhasználó kiválasztotta a témakört és a szót megkapja ABC sorrendbe állítva, majd ezután kell helyes sorrendbe állítania őket, hogy megkaphassa a helyes megfejtést
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
                    case 1: topic_english = family_english;     topic = "család";       break;
                    case 2: topic_english = it_english;         topic = "informatika";  break;
                    case 3: topic_english = travek_english;     topic = "utazás";       break;
                    case 4: topic_english = weather_english;    topic = "időjárás";     break;
                    case 5: topic_english = home_english;       topic = "lakóhely";     break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                HashSet<int> used_index = new HashSet<int>();
                Console.WriteLine("Szókereső: A megadott témakörből kapott szavakat a program összekeveri ABC sorrendben,\n a felhasználónak pedig helyes sorrendbe kell állítania őket.");
                Console.WriteLine("\nTémakör: " + topic);

                for (int i = 0; i < 5; i++)
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
        
        //5. feladat: Egy tömbbe bele teszünk 5 a témával kapcsolatos mondatot adott nyelven és tükörfordítással a magyar megfelelőjét 
        public void MondatRendezes()
        {
            List<string> Rendezes_magyar = new List<string>();
            List<string> Rendezes_angol = new List<string>();
            string[] adatok = File.ReadAllLines("mondatok_angol.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                Rendezes_angol.Add(sor[0]);
                Rendezes_magyar.Add(sor[1]);
            }
            string felhasznalotippje;
            int elet = 3;
            Console.WriteLine("Mondatrendezés: Egy összekevert mondatrészletekből álló mondatot kell a felhasználónak\n helyesen visszaállítania az eredeti formájába.");
            Console.WriteLine("Ha nem tudja kitalálni a mondatot akkor írjon be egy 'x' karaktert");
            Console.WriteLine("------------------------");
            Console.WriteLine("Kevert mondat:");

            Random rnd = new Random(); 

            do
            {
                int randomkivalasztott_mondat_indexe = rnd.Next(0, Rendezes_angol.Count());
                string magyar_helyes_mondat = Rendezes_magyar[randomkivalasztott_mondat_indexe];
                string angol_keverni_kivant_mondat = Rendezes_angol[randomkivalasztott_mondat_indexe];
                Console.WriteLine(angol_keverni_kivant_mondat);
                string[] splitteltszo = angol_keverni_kivant_mondat.Split(' ');
                string[] angolul_kevert_szavak = splitteltszo.OrderBy(x => rnd.Next()).ToArray();
                string angol_osszekevert_mondat = string.Join(" ", angolul_kevert_szavak);
                Console.WriteLine("Élet:" + elet);
                Console.WriteLine(angol_osszekevert_mondat);
                felhasznalotippje = Console.ReadLine();

                if (felhasznalotippje.Split(' ').OrderBy(s => s).SequenceEqual(angol_osszekevert_mondat.Split(' ').OrderBy(s => s)))
                {
                    Console.WriteLine("Helyes válasz!");
                    scoreMONDATRENDEZES += 2;
                    Console.WriteLine("Pontok: " + scoreMONDATRENDEZES);
                    Console.WriteLine("-------------------");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Rossz válasz");
                    elet--;
                    Console.WriteLine("Élet: " + elet);
                    Console.WriteLine("-------------------");
                    Console.WriteLine();
                }
            }
            while (elet > 0 || felhasznalotippje != "x");
            if (felhasznalotippje == "x" )
            {
                Console.WriteLine("Köszönjük a játékot!");
                Console.WriteLine("Maradt élet:" + elet);
                Console.WriteLine("-------------------");
            }
            if (elet == 0)
            {
                Console.WriteLine("Köszönjük a játékot!");
                Console.WriteLine("Maradt élet:" + elet);
                Console.WriteLine("-------------------");
            }
        }
        
        //6. feladat: Egy tömbbe 5 mondatot bele teszünk majd a programba meg kerverjük a mondat el rendezését és a felhasználónak be kell írnia a helyesen leírt mondatot
        public void Helyesforditas()
        {
            List<string> mondatRendezes_magyar = new List<string>();
            List<string> sentenceOrdering_English = new List<string>();

            string[] adatok = File.ReadAllLines("mondatok_angol.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                sentenceOrdering_English.Add(sor[0]);
                mondatRendezes_magyar.Add(sor[1]);
            }

            int rounds = 5;
            Random rnd = new Random();
            List<int> usedIndices = new List<int>();
            Console.WriteLine("Helyes fordítás: A magyar mondat után két lehetőség közül kell kiválasztani a helyes fordítást.");
            Console.WriteLine($"Angol-magyar mondat kvíz ({rounds} kör)");
            Console.WriteLine("------------------------------\n");

            for (int round = 1; round <= rounds; round++)
            {
                Console.WriteLine($"Kör {round}/{rounds}");

                int currentIndex;
                do
                {
                    currentIndex = rnd.Next(0, sentenceOrdering_English.Count);
                } 
                while (usedIndices.Contains(currentIndex));
                usedIndices.Add(currentIndex);

                int wrongIndex;
                do
                {
                    wrongIndex = rnd.Next(0, sentenceOrdering_English.Count);
                } 
                while (wrongIndex == currentIndex || usedIndices.Contains(wrongIndex));

                string hungarian_choosen_sentence = mondatRendezes_magyar[currentIndex];
                string correct_english_sentence = sentenceOrdering_English[currentIndex];

                bool correct_one = rnd.Next(2) == 0;
                string option1 = correct_one ? sentenceOrdering_English[currentIndex] : sentenceOrdering_English[wrongIndex];
                string option2 = correct_one ? sentenceOrdering_English[wrongIndex] : sentenceOrdering_English[currentIndex];

                Console.WriteLine("\nA magyar mondat: " + hungarian_choosen_sentence);
                Console.WriteLine("\nLehetőségek:");
                Console.WriteLine("A) " + option1);
                Console.WriteLine("B) " + option2);
                Console.Write("\nAdd meg a válaszod betűjelét (A/B): ");

                string AorB = Console.ReadLine().ToUpper();

                if ((AorB == "A" && correct_one) || (AorB == "B" && !correct_one))
                {
                    Console.WriteLine(" Helyes válasz!");
                    scoreHELYESFORDITAS++;
                }
                else
                {
                    Console.WriteLine($" Helytelen válasz! A helyes válasz: {correct_english_sentence}");
                }

                Console.WriteLine($"Pontszám: {scoreHELYESFORDITAS}/{round}\n");
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("\nJáték vége!");
            Console.WriteLine($"Végső pontszám: {scoreHELYESFORDITAS}/{rounds}");
            Console.WriteLine($"Sikerráta: {scoreHELYESFORDITAS * 100 / rounds}%");

            Console.WriteLine("\nNyomjon meg egy billentyűt a kilépéshez...");
        }

        //Összeszámolja, hogy összesen hány pontot gyűjtött a felhasználó a gyakorlással
        public int Pontok()
        {
            int pontok = scoreABC + scoreAKASZTOFA + scorePAROSITAS + scoreSZOKERESO + scoreMONDATRENDEZES + scoreHELYESFORDITAS;
            return pontok;
        }

    }
}