namespace Lingarix
{
    internal class Spanyol
    {
        //Idegennyelvű szavak
        List<string> family_spain = new List<string>();
        List<string> it_spain = new List<string>();
        List<string> travel_spain = new List<string>();
        List<string> weather_spain = new List<string>();
        List<string> home_spain = new List<string>();

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
        int scoreHELYESFORDITAS;

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
            string[] datas = File.ReadAllLines("spanyol.txt");
            username = felhasznalonev;
            for (int i = 0; i < datas.Length; i++)
            {
                string[] sor = datas[i].Split(';');
                if (sor[2] == "család")
                {
                    family_spain.Add(sor[0]);
                    family_hun.Add(sor[1]);
                }
                if (sor[2] == "informatika")
                {
                    it_spain.Add(sor[0]);
                    it_hun.Add(sor[1]);
                }
                if (sor[2] == "utazás")
                {
                    travel_spain.Add(sor[0]);
                    travel_hun.Add(sor[1]);
                }
                if (sor[2] == "időjárás")
                {
                    weather_spain.Add(sor[0]);
                    weather_hun.Add(sor[1]);
                }
                if (sor[2] == "lakóhely")
                {
                    home_spain.Add(sor[0]);
                    home_hun.Add(sor[1]);
                }
            }

            Console.WriteLine("--------------------");
        }

        //1. feladatunk: A-B-C lehetőség van a kiírt fordítás helyes megfejtésére
        public void ABC()
        {
            Console.WriteLine("A - B - C feladat: A felhasználónak három lehetőség közül kell kiválasztania a helyes fordítást a megadott szóra.");
            Random random = new Random();
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

                List<string> topic_spain = new List<string>();
                List<string> topic_hun = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        topic_spain = family_spain;
                        topic_hun = family_hun;
                        topic = "család";
                        break;

                    case 2:
                        topic_spain = it_spain;
                        topic_hun = it_hun;
                        topic = "informatika";
                        break;

                    case 3:
                        topic_spain = travel_spain;
                        topic_hun = travel_hun;
                        topic = "utazás";
                        break;

                    case 4:
                        topic_spain = weather_spain;
                        topic_hun = weather_hun;
                        topic = "időjárás";
                        break;

                    case 5:
                        topic_spain = home_spain;
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
                    if (used_index.Count == topic_spain.Count) // Ha minden szó elfogyott, újra kezdjük
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = random.Next(topic_spain.Count);
                    } while (used_index.Contains(index)); // Ellenőrizzük, hogy ne ismétlődjön

                    used_index.Add(index); // Elmentjük a felhasznált indexet

                    word = topic_spain[index];
                    hun_word = topic_hun[index];

                    Console.WriteLine("\nA {0} témában válassza ki a megfelelő választ az 'a/b/c' lehetőségek közül!", topic);
                    Console.WriteLine("A spanyol szó: " + word);

                    List<string> another_Hun = topic_hun.Where((value, idx) => idx != index).ToList();
                    string another_Hun2 = another_Hun[random.Next(another_Hun.Count)];
                    another_Hun.Remove(another_Hun2);
                    string masikMagyar2 = another_Hun[random.Next(another_Hun.Count)];

                    int right_answer_index = random.Next(3);
                    string[] answers = { another_Hun2, masikMagyar2, hun_word };
                    (answers[right_answer_index], answers[2]) = (answers[2], answers[right_answer_index]);

                    Console.WriteLine("a) " + answers[0]);
                    Console.WriteLine("b) " + answers[1]);
                    Console.WriteLine("c) " + answers[2]);

                    string deciphering = Console.ReadLine(); //megfejtés
                    if (deciphering == new[] { "a", "b", "c" }[right_answer_index])
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

            int maxLifeNumber = 6;
            int rajzIndex = maxLifeNumber - Math.Max(0, Math.Min(maxLifeNumber, eletSzam));
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
                List<string> wordList = new List<string>();
                string topicName = "";

                switch (serial_number)
                {
                    case 1: wordList = family_spain; topicName = "Család"; break;
                    case 2: wordList = it_spain; topicName = "Informatika"; break;
                    case 3: wordList = travel_spain; topicName = "Utazás"; break;
                    case 4: wordList = weather_spain; topicName = "Időjárás"; break;
                    case 5: wordList = home_spain; topicName = "Lakóhely"; break;
                    default: Console.WriteLine("Hibás választás!"); continue;
                }

                if (wordList.Count == 0)
                {
                    Console.WriteLine("Nincs elérhető szó ebben a témakörben!");
                    continue;
                }

                string findable_word = wordList[r.Next(wordList.Count)];
                int HitPoints = findable_word.Length + 5;
                HashSet<char> Used_Chars = new HashSet<char>();
                HashSet<char> Correct_Results = new HashSet<char>();

                Console.WriteLine($"\nAkasztófa játék - Téma: {topicName}");
                Console.WriteLine($"A szó {findable_word.Length} betűből áll. Kezdésre {HitPoints} életed van.");

                while (HitPoints > 0)
                {
                    int hianyzoBetuk = 0;
                    Console.Write("\nSzó: ");
                    foreach (char kar in findable_word)
                    {
                        if (Correct_Results.Contains(kar))
                        {
                            Console.Write(kar);
                        }
                        else
                        {
                            Console.Write("_");
                            hianyzoBetuk++;
                        }
                    }
                    Console.WriteLine();

                    if (hianyzoBetuk == 0)
                    {
                        Console.WriteLine("\n Gratulálok! Megnyerted a játékot! ");
                        scoreAKASZTOFA += findable_word.Length; // Pontszám növelése
                        break;
                    }

                    Console.Write("\nÍrj be egy betűt: ");
                    string tipp = Console.ReadLine().ToLower();

                    if (string.IsNullOrWhiteSpace(tipp) || tipp.Length != 1 || !char.IsLetter(tipp[0]))
                    {
                        Console.WriteLine(" Csak egy érvényes betűt adj meg!");
                        continue;
                    }

                    char betu = tipp[0];

                    if (Used_Chars.Contains(betu))
                    {
                        Console.WriteLine(" Már próbálkoztál ezzel a betűvel!");
                        continue;
                    }

                    Used_Chars.Add(betu);

                    if (findable_word.Contains(betu))
                    {
                        Console.WriteLine($" A(z) '{betu}' betű szerepel a szóban!");
                        Correct_Results.Add(betu);
                    }
                    else
                    {
                        HitPoints--;
                        Console.WriteLine($" Sajnos a(z) '{betu}' nincs a szóban! {HitPoints} életed maradt.");
                        RajzolAkasztofa(HitPoints);
                    }
                }

                if (HitPoints == 0)
                {
                    Console.WriteLine($"\n Vesztettél! A keresett szó: {findable_word}");
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

                List<string> list_spain = new List<string>();
                List<string> list_hun = new List<string>();
                string temakor = "";

                switch (serial_number)
                {
                    case 1:
                        list_spain = family_spain;
                        list_hun = family_hun;
                        temakor = "család";
                        break;
                    case 2:
                        list_spain = it_spain;
                        list_hun = it_hun;
                        temakor = "informatika";
                        break;
                    case 3:
                        list_spain = travel_spain;
                        list_hun = travel_hun;
                        temakor = "utazás";
                        break;
                    case 4:
                        list_spain = weather_spain;
                        list_hun = weather_hun;
                        temakor = "időjárás";
                        break;
                    case 5:
                        list_spain = home_spain;
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

                Console.WriteLine($"Adja meg az olasz szó magyar megfelelőjét a {temakor} témához kapcsolódva!");

                for (int i = 0; i < 5; i++)  // 5 szót kérünk le
                {
                    int index = szam.Next(list_spain.Count);  // Véletlenszerű index
                    string word = list_spain[index];
                    string hun_Word = list_hun[index];

                    Console.Write(word + " -- ");
                    string answer = Console.ReadLine();

                    if (answer.Trim().ToLower() == hun_Word.ToLower())
                    {
                        scorePAROSITAS += 1;
                        Console.WriteLine("  Helyes!");
                    }
                    else
                    {
                        Console.WriteLine($" Hibás! A helyes válasz: {hun_Word}");
                    }
                }

                Console.WriteLine("\nEddigi pontok: " + scorePAROSITAS + " pont");
                Console.WriteLine();

            } while (serial_number != 7);
        }

        //4. feladatunk: A felhasználó kiválasztotta a témakört és a szót megkapja ABC sorrendbe állítva, majd ezután kell helyes srrendben állítania őket, hogy megkaphassa a helyes megfejtést
        public void SzoKereso()
        {
            Console.WriteLine("Szókereső: A megadott témakörből kapott szavakat a program összekeveri ABC sorrendben, a felhasználónak pedig helyes sorrendbe kell állítania őket.");
            Random random = new Random();

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

                List<string> topic_spain = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1:
                        topic_spain = family_spain;
                        topic = "család";
                        break;

                    case 2:
                        topic_spain = it_spain;
                        topic = "informatika";
                        break;

                    case 3:
                        topic_spain = travel_spain;
                        topic = "utazás";
                        break;

                    case 4:
                        topic_spain = weather_spain;
                        topic = "időjárás";
                        break;

                    case 5:
                        topic_spain = home_spain;
                        topic = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                HashSet<int> usedIndex = new HashSet<int>();

                Console.WriteLine("\nTémakör: " + topic);
                Console.WriteLine("A megadott betűk alapján találja ki a szót és írja be a kijelölt helyre!");

                for (int i = 0; i < 5; i++) // 5 különböző szó kiválasztása
                {
                    if (usedIndex.Count == topic_spain.Count)
                    {
                        usedIndex.Clear();
                    }

                    int index;
                    do
                    {
                        index = random.Next(topic_spain.Count);
                    } while (usedIndex.Contains(index));

                    usedIndex.Add(index);
                    string word = topic_spain[index];

                    char[] chars = word.ToCharArray();
                    Array.Sort(chars);
                    string abc_sorted_words = new string(chars);

                    Console.WriteLine("--------------------");
                    Console.WriteLine(abc_sorted_words);
                    Console.WriteLine("--------------------");

                    string finding_word = Console.ReadLine();

                    if (finding_word == word)
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
            List<string> Rendezes_spanyol = new List<string>();
            string[] adatok = File.ReadAllLines("mondatok_spanyol.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                Rendezes_spanyol.Add(sor[0]);
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
                int randomkivalasztott_mondat_indexe = rnd.Next(0, Rendezes_spanyol.Count());
                string magyar_helyes_mondat = Rendezes_magyar[randomkivalasztott_mondat_indexe];
                string spanyol_keverni_kivant_mondat  = Rendezes_spanyol[randomkivalasztott_mondat_indexe];
                Console.WriteLine(spanyol_keverni_kivant_mondat);
                // Spliteljük a kiválasztott idegennyelvű mondatot
                string[] splitteltszo = spanyol_keverni_kivant_mondat.Split(' ');
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
        public void Helyesforditas()
        {
            Console.WriteLine("Mondatrendezés: Egy összekevert mondatrészletekből álló mondatot kell a felhasználónak helyesen visszaállítania az eredeti formájába.");
            List<string> mondatRendezes_magyar = new List<string>();
            List<string> mondatRendezes_spanyol = new List<string>();

            string[] adatok = File.ReadAllLines("mondatok_spanyol.txt");
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                mondatRendezes_spanyol.Add(sor[0]);
                mondatRendezes_magyar.Add(sor[1]);
            }

            int rounds = 5;

            Random rnd = new Random();
            List<int> usedIndices = new List<int>();

            Console.WriteLine($"Spanyol-magyar mondat kvíz ({rounds} kör)");
            Console.WriteLine("------------------------------\n");

            for (int round = 1; round <= rounds; round++)
            {
                Console.WriteLine($"Kör {round}/{rounds}");

                int currentIndex;
                do
                {
                    currentIndex = rnd.Next(0, mondatRendezes_spanyol.Count);
                }
                while (usedIndices.Contains(currentIndex));
                usedIndices.Add(currentIndex);

                int wrongIndex;
                do
                {
                    wrongIndex = rnd.Next(0, mondatRendezes_spanyol.Count);
                }
                while (wrongIndex == currentIndex || usedIndices.Contains(wrongIndex));

                string magyar_kivalasztott_mondat = mondatRendezes_magyar[currentIndex];
                string helyes_spanyol_valasz = mondatRendezes_spanyol[currentIndex];

                bool helyes_elso = rnd.Next(2) == 0;
                string elso_opcio = helyes_elso ? mondatRendezes_spanyol[currentIndex] : mondatRendezes_spanyol[wrongIndex];
                string masodik_opcio = helyes_elso ? mondatRendezes_spanyol[wrongIndex] : mondatRendezes_spanyol[currentIndex];

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
                    Console.WriteLine($" Helytelen válasz! A helyes válasz: {helyes_spanyol_valasz}");
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
            int pontok = scoreABC + scoreAKASZTOFA + scorePAROSITAS + scoreSZOKERESO + scoreTUKORFORDITAS + scoreHELYESFORDITAS;
            return pontok;
        }

    }
}