﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lingarix
{
    internal class Francia
    {
        //Idegennyelvű szavak
        List<string> family_french = new List<string>();
        List<string> it_french = new List<string>();
        List<string> tracell_french = new List<string>();
        List<string> weather_french = new List<string>();
        List<string> home_french = new List<string>();

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
        /// A pontokat itt számoljuk a tükörfordításos feladathoz
        /// </summary>
        int scoreMONDATRENDEZES;

        /// <summary>
        /// A pontokat itt számoljuk a mondat rendezéses feladathoz
        /// </summary>
        int scoreHELYESFORDITAS;

        /// <summary>
        /// Itt adják meg a feladat sorszámát
        /// </summary>
        int serial_number;

        /// <summary>
        /// A kitalálandó szavakat tároljuk benne, minden egyes feladatban
        /// </summary>
        string word;

        /// <summary>
        /// A felhasználó válasza, minden egyes feladatban magyar nyelvű
        /// </summary>
        string hun_word;

        //A dokumentumból beolvassuk a szavakat és a témaköröket
        public void Beolvas()
        {
            string[] datas = File.ReadAllLines("francia.txt");
            for (int i = 0; i < datas.Length; i++)
            {
                string[] sor = datas[i].Split(';');
                if (sor[2] == "család")
                {
                    family_french.Add(sor[0]);
                    family_hun.Add(sor[1]);
                }
                if (sor[2] == "informatika")
                {
                    it_french.Add(sor[0]);
                    it_hun.Add(sor[1]);
                }
                if (sor[2] == "utazás")
                {
                    tracell_french.Add(sor[0]);
                    travel_hun.Add(sor[1]);
                }
                if (sor[2] == "időjárás")
                {
                    weather_french.Add(sor[0]);
                    weather_hun.Add(sor[1]);
                }
                if (sor[2] == "lakóhely")
                {
                    home_french.Add(sor[0]);
                    home_hun.Add(sor[1]);
                }
            }

            Console.WriteLine("--------------------");
        }
        
        //1. feladatunk: A-B-C lehetőség van a kiírt fordítás helyes megfejtésére
        public void ABC()
        {
            Console.WriteLine("A - B - C feladat: A felhasználónak három lehetőség közül kell kiválasztania a helyes fordítást a megadott szóra.");
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

                List<string> topic_french = new List<string>();
                List<string> topic_hun = new List<string>();
                string temakor = "";

                switch (serial_number)
                {
                    case 1:
                        topic_french = family_french;
                        topic_hun = family_hun;
                        temakor = "család";
                        break;

                    case 2:
                        topic_french = it_french;
                        topic_hun = it_hun;
                        temakor = "informatika";
                        break;

                    case 3:
                        topic_french = tracell_french;
                        topic_hun = travel_hun;
                        temakor = "utazás";
                        break;

                    case 4:
                        topic_french = weather_french;
                        topic_hun = weather_hun;
                        temakor = "időjárás";
                        break;

                    case 5:
                        topic_french = home_french;
                        topic_hun = home_hun;
                        temakor = "lakóhely";
                        break;
                }

                used_index.Clear(); // Minden új témánál töröljük a korábbi szavakat

                for (int i = 0; i < 5; i++) // 5 szó bekérése egy témából
                {
                    if (used_index.Count == topic_french.Count) // Ha minden szó elfogyott, újra kezdjük
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = rnd.Next(topic_french.Count);
                    } while (used_index.Contains(index)); // Ellenőrizzük, hogy ne ismétlődjön

                    used_index.Add(index); // Elmentjük a felhasznált indexet

                    word = topic_french[index];
                    hun_word = topic_hun[index];

                    Console.WriteLine("\nA {0} témában válassza ki a megfelelő választ az 'a/b/c' lehetőségek közül!", temakor);
                    Console.WriteLine("A francia szó: " + word);

                    List<string> another_HUN = topic_hun.Where((value, idx) => idx != index).ToList();
                    string another_HUN2 = another_HUN[rnd.Next(another_HUN.Count)];
                    another_HUN.Remove(another_HUN2);
                    string another_HUN3 = another_HUN[rnd.Next(another_HUN.Count)];

                    int correct_answer_indec = rnd.Next(3);
                    string[] answears = { another_HUN2, another_HUN3, hun_word };
                    (answears[correct_answer_indec], answears[2]) = (answears[2], answears[correct_answer_indec]);

                    Console.WriteLine("a) " + answears[0]);
                    Console.WriteLine("b) " + answears[1]);
                    Console.WriteLine("c) " + answears[2]);

                    string megfejtes = Console.ReadLine();
                    if (megfejtes == new[] { "a", "b", "c" }[correct_answer_indec])
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

            int maxEletSzam = 6;
            int rajzIndex = maxEletSzam - Math.Max(0, Math.Min(maxEletSzam, eletSzam));
            Console.WriteLine(akasztofaRajz[rajzIndex]);
        }
        
        //2. feladatunk: Akasztófa játék, a felhasználónak kell kitalálnia a szót
        public void Akasztofa()
        {
            Console.WriteLine("Akasztófa játék: A felhasználónak ki kell találnia a keresett szót betűnként tippelve, akárcsak a klasszikus akasztófajátékban.");
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
                List<string> word_list = new List<string>();
                string temaNeve = "";

                switch (serial_number)
                {
                    case 1: word_list = family_french; temaNeve = "Család"; break;
                    case 2: word_list = it_french; temaNeve = "Informatika"; break;
                    case 3: word_list = tracell_french; temaNeve = "Utazás"; break;
                    case 4: word_list = weather_french; temaNeve = "Időjárás"; break;
                    case 5: word_list = home_french; temaNeve = "Lakóhely"; break;
                    default: Console.WriteLine("Hibás választás!"); continue;
                }

                if (word_list.Count == 0)
                {
                    Console.WriteLine("Nincs elérhető szó ebben a témakörben!");
                    continue;
                }

                string missing_word = word_list[r.Next(word_list.Count)];
                int user_HP = missing_word.Length + 5;
                HashSet<char> used_chars = new HashSet<char>();
                HashSet<char> correct_answears = new HashSet<char>();

                Console.WriteLine($"\nAkasztófa játék - Téma: {temaNeve}");
                Console.WriteLine($"A szó {missing_word.Length} betűből áll. Kezdésre {user_HP} életed van.");

                while (user_HP > 0)
                {
                    int pice_of_missing_chars = 0;
                    Console.Write("\nSzó: ");
                    foreach (char kar in missing_word)
                    {
                        if (correct_answears.Contains(kar))
                        {
                            Console.Write(kar);
                        }
                        else
                        {
                            Console.Write("_");
                            pice_of_missing_chars++;
                        }
                    }
                    Console.WriteLine();

                    if (pice_of_missing_chars == 0)
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
                        correct_answears.Add(betu);
                    }
                    else
                    {
                        user_HP--;
                        Console.WriteLine($" Sajnos a(z) '{betu}' nincs a szóban! {user_HP} életed maradt.");
                        RajzolAkasztofa(user_HP);
                    }
                }

                if (user_HP == 0)
                {
                    Console.WriteLine($"\n Vesztettél! A keresett szó: {missing_word}");
                }

                Console.WriteLine("\n***************************************");
                Console.WriteLine($"Jelenlegi pontszámod: {scoreAKASZTOFA} pont");
                Console.WriteLine("***************************************\n");

            } while (true);

            Console.WriteLine("Köszönjük, hogy velünk tanultál! ");
        }

        //3. feladatunk: Az idegennyelvű szót megjelenítjük a felhasználónak, majd a magyar megfelelőjét kell begépelnie
        public void SzoParositas()
        {
            Console.WriteLine("Szópárosítás: A program megjeleníti az idegen nyelvű szót, és a felhasználónak be kell írnia a helyes magyar megfelelőjét.");
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

                List<string> list_french = new List<string>();
                List<string> list_hun = new List<string>();
                string temakor = "";

                switch (serial_number)
                {
                    case 1:
                        list_french = family_french;
                        list_hun = family_hun;
                        temakor = "család";
                        break;
                    case 2:
                        list_french = it_french;
                        list_hun = it_hun;
                        temakor = "informatika";
                        break;
                    case 3:
                        list_french = tracell_french;
                        list_hun = travel_hun;
                        temakor = "utazás";
                        break;
                    case 4:
                        list_french = weather_french;
                        list_hun = weather_hun;
                        temakor = "időjárás";
                        break;
                    case 5:
                        list_french = home_french;
                        list_hun = home_hun;
                        temakor = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                Console.WriteLine($"Adja meg az francia szó magyar megfelelőjét a {temakor} témához kapcsolódva!");

                for (int i = 0; i < 5; i++)  // 5 szót kérünk le
                {
                    int index = rnd.Next(list_french.Count);  // Véletlenszerű index
                    string word = list_french[index];
                    string hun_word = list_hun[index];

                    Console.Write(word + " -- ");
                    string answer = Console.ReadLine();

                    if (answer.Trim().ToLower() == hun_word.ToLower())
                    {
                        scorePAROSITAS += 1;
                        Console.WriteLine("  Helyes!");
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

        //4. feladatunk: A felhasználó kiválasztotta a témakört és a szót megkapja ABC sorrendbe állítva, majd ezután kell helyes serrendbe állítania őket, hogy megkaphassa a helyes megfejtést
        public void SzoKereso()
        {
            Console.WriteLine("Szókereső: A megadott témakörből kapott szavakat a program összekeveri ABC sorrendben, a felhasználónak pedig helyes sorrendbe kell állítania őket.");
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

                List<string> topic_french = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        topic_french = family_french;
                        topic = "család";
                        break;

                    case 2:
                        topic_french = it_french;
                        topic = "informatika";
                        break;

                    case 3:
                        topic_french = tracell_french;
                        topic = "utazás";
                        break;

                    case 4:
                        topic_french = weather_french;
                        topic = "időjárás";
                        break;

                    case 5:
                        topic_french = home_french;
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
                    if (used_index.Count == topic_french.Count)
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = rnd.Next(topic_french.Count);
                    } while (used_index.Contains(index));

                    used_index.Add(index);
                    string word = topic_french[index];

                    char[] betuk = word.ToCharArray();
                    Array.Sort(betuk);
                    string abc_rendezett_betuk = new string(betuk);

                    Console.WriteLine("--------------------");
                    Console.WriteLine(abc_rendezett_betuk);
                    Console.WriteLine("--------------------");

                    string szokereso = Console.ReadLine();

                    if (szokereso == word)
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
            Console.WriteLine("Mondatrendezés: Egy összekevert mondatrészletekből álló mondatot kell a felhasználónak helyesen visszaállítania az eredeti formájába.");
            List<string> Rendezes_magyar = new List<string>();
            List<string> Rendezes_francia = new List<string>();
            string[] adatok = File.ReadAllLines("mondatok_francia.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                Rendezes_francia.Add(sor[0]);
                Rendezes_magyar.Add(sor[1]);
            }
            string felhasznalotippje;
            int elet = 3;

            Console.WriteLine("Ha nem tudja kitalálni a mondatot akkor írjon be egy 'x' karaktert");
            Console.WriteLine("------------------------");
            Console.WriteLine("Kevert mondat:");

            Random rnd = new Random(); // Random példány egyszeri létrehozása

            do
            {
                int randomkivalasztott_mondat_indexe = rnd.Next(0, Rendezes_francia.Count());
                string magyar_helyes_mondat = Rendezes_magyar[randomkivalasztott_mondat_indexe];
                string francia_keverni_kivant_mondat = Rendezes_francia[randomkivalasztott_mondat_indexe];
                Console.WriteLine(francia_keverni_kivant_mondat);
                // Spliteljük a kiválasztott idegennyelvű mondatot
                string[] splitteltszo = francia_keverni_kivant_mondat.Split(' ');
                // Megkeverjük a mondatot 
                string[] franciaul_kevert_szavak = splitteltszo.OrderBy(x => rnd.Next()).ToArray();
                // Egy stringbe fűzzük a kevert mondatot
                string francia_osszekevert_mondat = string.Join(" ", franciaul_kevert_szavak);
                Console.WriteLine("Élet:" + elet);
                Console.WriteLine(francia_osszekevert_mondat);
                felhasznalotippje = Console.ReadLine();

                // A válaszok szótárának ellenőrzése (figyelmen kívül hagyva a szavak sorrendjét)
                if (felhasznalotippje.Split(' ').OrderBy(s => s).SequenceEqual(francia_osszekevert_mondat.Split(' ').OrderBy(s => s)))
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
            if (felhasznalotippje == "x") // Kilépés az 'x' gomb megnyomásával
            {
                Console.WriteLine("Köszönjük a játékot!");
                Console.WriteLine("Maradt élet:" + elet);
                Console.WriteLine("-------------------");
            }
            if (elet == 0) // Kilépés az 'x' gomb megnyomásával
            {
                Console.WriteLine("Köszönjük a játékot!");
                Console.WriteLine("Maradt élet:" + elet);
                Console.WriteLine("-------------------");
            }
        }

        //6. feladat: Egy tömbbe 5 mondatot bele teszünk majd a programba meg kerverjük a mondat el rendezését és a felhasználónak be kell írnia a helyesen leírt mondatot
        public void Helyesforditas()
        {
            Console.WriteLine("Helyes fordítás: A magyar mondat után két lehetőség közül kell kiválasztani a helyes fordítást.");
            List<string> forditas_magyar = new List<string>();
            List<string> forditas_francia = new List<string>();

            string[] adatok = File.ReadAllLines("mondatok_francia.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                forditas_francia.Add(sor[0]);
                forditas_magyar.Add(sor[1]);
            }

            int rounds = 5;

            Random rnd = new Random();
            List<int> usedIndices = new List<int>();

            Console.WriteLine($"Francia-magyar mondat kvíz ({rounds} kör)");
            Console.WriteLine("------------------------------\n");

            for (int round = 1; round <= rounds; round++)
            {
                Console.WriteLine($"Kör {round}/{rounds}");

                int currentIndex;
                do
                {
                    currentIndex = rnd.Next(0, forditas_francia.Count);
                }
                while (usedIndices.Contains(currentIndex));
                usedIndices.Add(currentIndex);

                int wrongIndex;
                do
                {
                    wrongIndex = rnd.Next(0, forditas_francia.Count);
                }
                while (wrongIndex == currentIndex || usedIndices.Contains(wrongIndex));

                string magyar_kivalasztott_mondat = forditas_magyar[currentIndex];
                string helyes_francia_valasz = forditas_francia[currentIndex];

                bool helyes_elso = rnd.Next(2) == 0;
                string elso_opcio = helyes_elso ? forditas_francia[currentIndex] : forditas_francia[wrongIndex];
                string masodik_opcio = helyes_elso ? forditas_francia[wrongIndex] : forditas_francia[currentIndex];

                Console.WriteLine("\nA magyar mondat: " + magyar_kivalasztott_mondat);
                Console.WriteLine("\nLehetőségek:");
                Console.WriteLine("A) " + elso_opcio);
                Console.WriteLine("B) " + masodik_opcio);
                Console.Write("\nAdd meg a válaszod betűjelét (A/B): ");

                string valasz = Console.ReadLine().ToUpper();

                if ((valasz == "A" && helyes_elso) || (valasz == "B" && !helyes_elso))
                {
                    Console.WriteLine(" Helyes válasz!");
                    scoreHELYESFORDITAS++;
                }
                else
                {
                    Console.WriteLine($" Helytelen válasz! A helyes válasz: {helyes_francia_valasz}");
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