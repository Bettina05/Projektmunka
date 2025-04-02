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
        /// A pontokat itt számoljuk a tükörfordításos feladathoz
        /// </summary>
        int scoreTUKORFORDITAS;

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
            HashSet<int> used_index = new HashSet<int>(); // Tárolja a már kiválasztott szavakat
            Console.WriteLine("A - B - C feladat: A felhasználónak három lehetőség közül kell kiválasztania a helyes fordítást a megadott szóra.");
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
        
        //4. feladatunk: A felhasználó kiválasztotta a témakört és a szót megkapja ABC sorrendbe állítva, majd ezután kell helyes sorrendbe állítania őket, hogy megkaphassa a helyes megfejtést
        public void SzoKereso()
        {
            Console.WriteLine("Szókereső: A megadott témakörből kapott szavakat a program összekeveri ABC sorrendben, a felhasználónak pedig helyes sorrendbe kell állítania őket.");
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
        
        //5. feladat: Egy tömbbe bele teszünk 5 a témával kapcsolatos mondatot adott nyelven és tükörfordítással a magyar megfelelőjét 
        public void TukorForditas()
        {
            Console.WriteLine("Tükörfordítás: Egy adott nyelvű mondatot és annak szó szerinti magyar megfelelőjét kapja meg a felhasználó, így gyakorolva a nyelvi szerkezeteket.");
            List<string> Tukorforditas_magyar = new List<string>();
            List<string> Tukorforditas_angol = new List<string>();
            string[] adatok = File.ReadAllLines("mondatok_angol.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                Tukorforditas_angol.Add(sor[0]);
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
                int randomkivalasztott_mondat_indexe = rnd.Next(0, Tukorforditas_angol.Count());
                string magyar_helyes_mondat = Tukorforditas_magyar[randomkivalasztott_mondat_indexe];
                string angol_keverni_kivant_mondat = Tukorforditas_angol[randomkivalasztott_mondat_indexe];
                Console.WriteLine(angol_keverni_kivant_mondat);
                // Spliteljük a kiválasztott idegennyelvű mondatot
                string[] splitteltszo = angol_keverni_kivant_mondat.Split(' ');
                // Megkeverjük a mondatot 
                string[] angolul_kevert_szavak = splitteltszo.OrderBy(x => rnd.Next()).ToArray();
                // Egy stringbe fűzzük a kevert mondatot
                string angol_osszekevert_mondat = string.Join(" ", angolul_kevert_szavak);
                Console.WriteLine("Élet:" + elet);
                Console.WriteLine(angol_osszekevert_mondat);
                felhasznalotippje = Console.ReadLine();

                // A válaszok szótárának ellenőrzése (figyelmen kívül hagyva a szavak sorrendjét)
                if (felhasznalotippje.Split(' ').OrderBy(s => s).SequenceEqual(angol_osszekevert_mondat.Split(' ').OrderBy(s => s)))
                {
                    Console.WriteLine("Helyes válasz!");
                    //elet++;
                    //Console.WriteLine("Élet: " + elet);
                    scoreTUKORFORDITAS += 2;
                    Console.WriteLine("Pontok: " + scoreTUKORFORDITAS);
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
        public void MondatRendezes()
        {
            Console.WriteLine("Mondatrendezés: Egy összekevert mondatrészletekből álló mondatot kell a felhasználónak helyesen visszaállítania az eredeti formájába.");
            List<string> mondatRendezes_magyar = new List<string>();
            List<string> mondatRendezes_angol = new List<string>();

            string[] adatok = File.ReadAllLines("mondatok_angol.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                if (sor[2] == "angol")
                {
                    mondatRendezes_angol.Add(sor[0]);
                    mondatRendezes_magyar.Add(sor[1]);
                }
            }

            int rounds = 5;

            Random rnd = new Random();
            List<int> usedIndices = new List<int>();

            Console.WriteLine($"Angol-magyar mondat kvíz ({rounds} kör)");
            Console.WriteLine("------------------------------\n");

            for (int round = 1; round <= rounds; round++)
            {
                Console.WriteLine($"Kör {round}/{rounds}");

                int currentIndex;
                do
                {
                    currentIndex = rnd.Next(0, mondatRendezes_angol.Count);
                } 
                while (usedIndices.Contains(currentIndex));
                usedIndices.Add(currentIndex);

                int wrongIndex;
                do
                {
                    wrongIndex = rnd.Next(0, mondatRendezes_angol.Count);
                } 
                while (wrongIndex == currentIndex || usedIndices.Contains(wrongIndex));

                string magyar_kivalasztott_mondat = mondatRendezes_magyar[currentIndex];
                string helyes_angol_valasz = mondatRendezes_angol[currentIndex];

                bool helyes_elso = rnd.Next(2) == 0;
                string elso_opcio = helyes_elso ? mondatRendezes_angol[currentIndex] : mondatRendezes_angol[wrongIndex];
                string masodik_opcio = helyes_elso ? mondatRendezes_angol[wrongIndex] : mondatRendezes_angol[currentIndex];

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
                    Console.WriteLine($" Helytelen válasz! A helyes válasz: {helyes_angol_valasz}");
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
            int pontok = scoreABC + scoreAKASZTOFA + scorePAROSITAS + scoreSZOKERESO + scoreTUKORFORDITAS + scoreMONDATRENDEZES;
            return pontok;
        }

    }
}