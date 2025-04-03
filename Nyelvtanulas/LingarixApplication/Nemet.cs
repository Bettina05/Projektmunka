using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lingarix
{
    internal class Nemet
    {
        //Idegennyelvű szavak
        List<string> family_german = new List<string>();
        List<string> it_german = new List<string>();
        List<string> travel_german = new List<string>();
        List<string> weather_german = new List<string>();
        List<string> home_german = new List<string>();

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
        int scoreHELYESFORDITAS;

        /// <summary>
        /// A pontokat itt számoljuk a mondat rendezéses feladathoz
        /// </summary>
        int scoreMONDATRENDEZES;

        /// <summary>
        /// Itt adják meg a feladat sorszámát
        /// </summary>
        int serial_number;

        /// <summary>
        /// A kitalálandó szavakat tároljuk benne, minden egyes feladatban idegennyelvű
        /// </summary>
        string word = "";

        /// <summary>
        /// A kitalálandó szavakat tároljuk benne, minden egyes feladatban magyar nyelvű
        /// </summary>
        string hun_word;

        //A dokumentumból beolvassuk a szavakat és a témaköröket
        public void Beolvas(string felhasznalonev)
        {
            string[] datas = File.ReadAllLines("nemet.txt");
            username = felhasznalonev;
            for (int i = 0; i < datas.Length; i++)
            {
                string[] sor = datas[i].Split(';');
                if (sor[2] == "család")
                {
                    family_german.Add(sor[0]);
                    family_hun.Add(sor[1]);
                }
                if (sor[2] == "informatika")
                {
                    it_german.Add(sor[0]);
                    it_hun.Add(sor[1]);
                }
                if (sor[2] == "utazás")
                {
                    travel_german.Add(sor[0]);
                    travel_hun.Add(sor[1]);
                }
                if (sor[2] == "időjárás")
                {
                    weather_german.Add(sor[0]);
                    weather_hun.Add(sor[1]);
                }
                if (sor[2] == "lakóhely")
                {
                    home_german.Add(sor[0]);
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

                List<string> topic_german = new List<string>();
                List<string> topic_hun = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        topic_german = family_german;
                        topic_hun = family_hun;
                        topic = "család";
                        break;

                    case 2:
                        topic_german = it_german;
                        topic_hun = it_hun;
                        topic = "informatika";
                        break;

                    case 3:
                        topic_german = travel_german;
                        topic_hun = travel_hun;
                        topic = "utazás";
                        break;

                    case 4:
                        topic_german = weather_german;
                        topic_hun = weather_hun;
                        topic = "időjárás";
                        break;

                    case 5:
                        topic_german = home_german;
                        topic_hun = home_hun;
                        topic = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }
                used_index.Clear(); // Minden új témánál töröljük a korábbi szavakat

                for (int i = 0; i < 5; i++) // 5 szó bekérése egy témából
                {
                    if (used_index.Count == topic_german.Count) // Ha minden szó elfogyott, újra kezdjük
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = rnd.Next(topic_german.Count);
                    } while (used_index.Contains(index)); // Ellenőrizzük, hogy ne ismétlődjön

                    used_index.Add(index); // Elmentjük a felhasznált indexet

                    word = topic_german[index];
                    hun_word = topic_hun[index];

                    Console.WriteLine("\nA {0} témában válassza ki a megfelelő választ az 'a/b/c' lehetőségek közül!", topic);
                    Console.WriteLine("A német szó: " + word);

                    List<string> antoher_HUN = topic_hun.Where((value, idx) => idx != index).ToList();
                    string another_HUN2 = antoher_HUN[rnd.Next(antoher_HUN.Count)];
                    antoher_HUN.Remove(another_HUN2);
                    string another_HUN3 = antoher_HUN[rnd.Next(antoher_HUN.Count)];

                    int correct_answear_index = rnd.Next(3);
                    string[] answears = { another_HUN2, another_HUN3, hun_word };
                    (answears[correct_answear_index], answears[2]) = (answears[2], answears[correct_answear_index]);

                    Console.WriteLine("a) " + answears[0]);
                    Console.WriteLine("b) " + answears[1]);
                    Console.WriteLine("c) " + answears[2]);

                    string answear = Console.ReadLine();
                    if (answear == new[] { "a", "b", "c" }[correct_answear_index])
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

            int max_HP = 6;
            int rajzIndex = max_HP - Math.Max(0, Math.Min(max_HP, eletSzam));
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

                Random rnd = new Random();
                List<string> word_list = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1: word_list = family_german; topic = "Család"; break;
                    case 2: word_list = it_german; topic = "Informatika"; break;
                    case 3: word_list = travel_german; topic = "Utazás"; break;
                    case 4: word_list = weather_german; topic = "Időjárás"; break;
                    case 5: word_list = home_german; topic = "Lakóhely"; break;
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
                    int missing_char = 0;
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
                            missing_char++;
                        }
                    }
                    Console.WriteLine();

                    if (missing_char == 0)
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

                List<string> german_list = new List<string>();
                List<string> hun_list = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        german_list = family_german;
                        hun_list = family_hun;
                        topic = "család";
                        break;
                    case 2:
                        german_list = it_german;
                        hun_list = it_hun;
                        topic = "informatika";
                        break;
                    case 3:
                        german_list = travel_german;
                        hun_list = travel_hun;
                        topic = "utazás";
                        break;
                    case 4:
                        german_list = weather_german;
                        hun_list = weather_hun;
                        topic = "időjárás";
                        break;
                    case 5:
                        german_list = home_german;
                        hun_list = home_hun;
                        topic = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                Console.WriteLine($"Adja meg az német szó magyar megfelelőjét a {topic} témához kapcsolódva!");

                for (int i = 0; i < 5; i++)  // 5 szót kérünk le
                {
                    int index = rnd.Next(german_list.Count);  // Véletlenszerű index
                    string word = german_list[index];
                    string hun_word = hun_list[index];

                    Console.Write(word + " -- ");
                    string answear = Console.ReadLine();

                    if (answear.Trim().ToLower() == hun_word.ToLower())
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
        //4. feladatunk: A felhasználó kiválasztotta a témakört és a szót megkapja ABC sorrendbe állítva, majd ezután kell helyes sorrendbe állítania őket, hogy megkaphassa a helyes megfejtést
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

                List<string> topic_german = new List<string>();
                string topic = "";
                switch (serial_number)
                {
                    case 1:
                        topic_german = family_german;
                        topic = "család";
                        break;

                    case 2:
                        topic_german = it_german;
                        topic = "informatika";
                        break;

                    case 3:
                        topic_german = travel_german;
                        topic = "utazás";
                        break;

                    case 4:
                        topic_german = weather_german;
                        topic = "időjárás";
                        break;

                    case 5:
                        topic_german = home_german;
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
                    if (used_index.Count == topic_german.Count)
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = rnd.Next(topic_german.Count);
                    } while (used_index.Contains(index));

                    used_index.Add(index);
                    string word = topic_german[index];

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
            List<string> Tukorforditas_magyar = new List<string>();
            List<string> Tukorforditas_olasz = new List<string>();
            string[] adatok = File.ReadAllLines("mondatok_olasz.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                Tukorforditas_olasz.Add(sor[0]);
                Tukorforditas_magyar.Add(sor[1]);
            }
            string felhasznalotippje;
            int elet = 3;
            Console.WriteLine("Ebben a feladatban egy mondatot fog kapni amely össze van keverve.");
            Console.WriteLine("Ezt kell helyes sorrendbe megadni!");
            Console.WriteLine("Ha nem tudja kitalálni a mondatot akkor írjon be egy 'x' karaktert");
            Console.WriteLine("------------------------");
            Console.WriteLine("Kevert mondat:");

            Random rnd = new Random(); // Random példány egyszeri létrehozása

            do
            {
                int randomkivalasztott_mondat_indexe = rnd.Next(0, Tukorforditas_olasz.Count());
                string magyar_helyes_mondat = Tukorforditas_magyar[randomkivalasztott_mondat_indexe];
                string olasz_keverni_kivant_mondat = Tukorforditas_olasz[randomkivalasztott_mondat_indexe];
                Console.WriteLine(olasz_keverni_kivant_mondat);
                // Spliteljük a kiválasztott idegennyelvű mondatot
                string[] splitteltszo = olasz_keverni_kivant_mondat.Split(' ');
                // Megkeverjük a mondatot 
                string[] olasz_kevert_szavak = splitteltszo.OrderBy(x => rnd.Next()).ToArray();
                // Egy stringbe fűzzük a kevert mondatot
                string olasz_osszekevert_mondat = string.Join(" ", olasz_kevert_szavak);
                Console.WriteLine("Élet:" + elet);
                Console.WriteLine(olasz_osszekevert_mondat);
                felhasznalotippje = Console.ReadLine();

                // A válaszok szótárának ellenőrzése (figyelmen kívül hagyva a szavak sorrendjét)
                if (felhasznalotippje.Split(' ').OrderBy(s => s).SequenceEqual(olasz_osszekevert_mondat.Split(' ').OrderBy(s => s)))
                {
                    Console.WriteLine("Helyes válasz!");
                    //elet++;
                    //Console.WriteLine("Élet: " + elet);
                    scoreHELYESFORDITAS += 2;
                    Console.WriteLine("Pontok: " + scoreHELYESFORDITAS);
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
        }

        //6. feladat: Egy tömbbe 5 mondatot bele teszünk majd a programba meg kerverjük a mondat el rendezését és a felhasználónak be kell írnia a helyesen leírt mondatot
        public void Helyesforditas()
        {
            Console.WriteLine("Mondatrendezés: Egy összekevert mondatrészletekből álló mondatot kell a felhasználónak helyesen visszaállítania az eredeti formájába.");
            List<string> mondatRendezes_magyar = new List<string>();
            List<string> mondatRendezes_olasz = new List<string>();

            string[] adatok = File.ReadAllLines("mondatok_olasz.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                mondatRendezes_olasz.Add(sor[0]);
                mondatRendezes_magyar.Add(sor[1]);
            }

            int rounds = 5;

            Random rnd = new Random();
            List<int> usedIndices = new List<int>();

            Console.WriteLine($"Olasz-magyar mondat kvíz ({rounds} kör)");
            Console.WriteLine("------------------------------\n");

            for (int round = 1; round <= rounds; round++)
            {
                Console.WriteLine($"Kör {round}/{rounds}");

                int currentIndex;
                do
                {
                    currentIndex = rnd.Next(0, mondatRendezes_olasz.Count);
                }
                while (usedIndices.Contains(currentIndex));
                usedIndices.Add(currentIndex);

                int wrongIndex;
                do
                {
                    wrongIndex = rnd.Next(0, mondatRendezes_olasz.Count);
                }
                while (wrongIndex == currentIndex || usedIndices.Contains(wrongIndex));

                string magyar_kivalasztott_mondat = mondatRendezes_magyar[currentIndex];
                string helyes_olasz_valasz = mondatRendezes_olasz[currentIndex];

                bool helyes_elso = rnd.Next(2) == 0;
                string elso_opcio = helyes_elso ? mondatRendezes_olasz[currentIndex] : mondatRendezes_olasz[wrongIndex];
                string masodik_opcio = helyes_elso ? mondatRendezes_olasz[wrongIndex] : mondatRendezes_olasz[currentIndex];

                Console.WriteLine("\nA magyar mondat: " + magyar_kivalasztott_mondat);
                Console.WriteLine("\nLehetőségek:");
                Console.WriteLine("A) " + elso_opcio);
                Console.WriteLine("B) " + masodik_opcio);
                Console.Write("\nAdd meg a válaszod betűjelét (A/B): ");

                string valasz = Console.ReadLine().ToUpper();

                if ((valasz == "A" && helyes_elso) || (valasz == "B" && !helyes_elso))
                {
                    Console.WriteLine(" Helyes válasz!");
                    scoreMONDATRENDEZES++;
                }
                else
                {
                    Console.WriteLine($" Helytelen válasz! A helyes válasz: {helyes_olasz_valasz}");
                }

                Console.WriteLine($"Pontszám: {scoreMONDATRENDEZES}/{round}\n");
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("\nJáték vége!");
            Console.WriteLine($"Végső pontszám: {scoreMONDATRENDEZES}/{rounds}");
            Console.WriteLine($"Sikerráta: {scoreMONDATRENDEZES * 100 / rounds}%");

            Console.WriteLine("\nNyomjon meg egy billentyűt a kilépéshez...");
        } 

        //Összeszámolja, hogy összesen hány pontot gyűjtött a felhasználó a gyakorlással
        public int Pontok()
        {
            int pontok = scoreABC + scoreAKASZTOFA + scorePAROSITAS + scoreSZOKERESO + scoreHELYESFORDITAS + scoreMONDATRENDEZES;
            return pontok;
        }
    }
}