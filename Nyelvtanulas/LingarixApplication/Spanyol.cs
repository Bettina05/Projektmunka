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
        int pontszamSZOKERESO;

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
        string answer;

        /// <summary>
        /// A kitalálandó szavakat tároljuk benne, minden egyes feladatban magyar nyelvű
        /// </summary>
        string hun_word;

        int index = 0;

        /// <summary>
        /// A szópárosításhoz tartozó szavak betűi abc sorrendbe rendezve
        /// </summary>
        string abc_sorted_word;

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
        //Első feladatunk: A-B-C lehetőség van a kiírt fordítás helyes megfejtésére
        public void ABC()
        {
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

        //Második feladatunk: Az idegennyelvű szót megjelenítjük a felhasználónak, majd a magyar megfelelőjét kell begépelnie

        public void SzoParositas()
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
        //Negyedik feladatunk: A felhasználó kiválasztotta a témakört és a szót megkapja ABC sorrendbe állítva, majd ezután kell helyes srrendben állítania őket, hogy megkaphassa a helyes megfejtést
        public void SzoKereso()
        {
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
                        pontszamSZOKERESO++;
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
                Console.WriteLine("Eddigi pontok: " + pontszamSZOKERESO + " pont");
                Console.WriteLine();

            } while (true);

            Console.WriteLine($"Köszönjük {username}, hogy a Lingarixel tanultál!");
            Console.WriteLine($"{username} összesen {Pontok()} pontot gyűjtött! Gratulálunk :)");
        }

        //Összeszámolja, hogy összesen hány pontot gyűjtött a felhasználó a gyakorlással

        public int Pontok()
        {
            int pontok = scoreABC + scoreAKASZTOFA + scorePAROSITAS + pontszamSZOKERESO;
            return pontok;
        }

    }
}