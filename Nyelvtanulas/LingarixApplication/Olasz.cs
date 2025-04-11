 namespace Lingarix
{
    internal class Olasz
    {
        List<string> family_italy = new List<string>();
        List<string> it_italy = new List<string>();
        List<string> travel_italy = new List<string>();
        List<string> weathe_italy = new List<string>();
        List<string> home_italy = new List<string>();

        List<string> family_hun = new List<string>();
        List<string> it_hun = new List<string>();
        List<string> travel_hun = new List<string>();
        List<string> weather_hun = new List<string>();
        List<string> home_travel = new List<string>();

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
        public void Beolvas(string usernameFromMVC)
        {
            string[] datas = File.ReadAllLines("olasz.txt");
            username = usernameFromMVC;
            for (int i = 0; i < datas.Length; i++)
            {
                string[] line = datas[i].Split(';');
                if (line[2] == "család")
                {
                    family_italy.Add(line[0]);
                    family_hun.Add(line[1]);
                }
                if (line[2] == "informatika")
                {
                    it_italy.Add(line[0]);
                    it_hun.Add(line[1]);
                }
                if (line[2] == "utazás")
                {
                    travel_italy.Add(line[0]);
                    travel_hun.Add(line[1]);
                }
                if (line[2] == "időjárás")
                {
                    weathe_italy.Add(line[0]);
                    weather_hun.Add(line[1]);
                }
                if (line[2] == "lakóhely")
                {
                    home_italy.Add(line[0]);
                    home_travel.Add(line[1]);
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
                List<string> topic_italy = new List<string>();
                List<string> topic_hun = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1: topic_italy = family_italy;     topic_hun = family_hun;     topic = "család";       break;
                    case 2: topic_italy = it_italy;         topic_hun = it_hun;         topic = "informatika";  break;
                    case 3: topic_italy = travel_italy;     topic_hun = travel_hun;     topic = "utazás";       break;
                    case 4: topic_italy = weathe_italy;     topic_hun = weather_hun;    topic = "időjárás";     break;
                    case 5: topic_italy = home_italy;       topic_hun = home_travel;    topic = "lakóhely";     break;
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
                    if (used_index.Count == topic_italy.Count)
                    {
                        used_index.Clear();
                    }

                    int index;
                    do
                    {
                        index = rnd.Next(topic_italy.Count);
                    } 
                    while (used_index.Contains(index));

                    used_index.Add(index);

                    word = topic_italy[index];
                    hun_word = topic_hun[index];
                    Console.WriteLine("A - B - C feladat: A felhasználónak három lehetőség közül kell kiválasztania a helyes fordítást a megadott szóra.");
                    Console.WriteLine("\nA {0} témát választotta", topic);
                    Console.WriteLine("Az olasz szó: " + word);

                    List<string> another_HUN = topic_hun.Where((value, idx) => idx != index).ToList();
                    string another_HUN2 = another_HUN[rnd.Next(another_HUN.Count)];
                    another_HUN.Remove(another_HUN2);
                    string antoher_HUN3 = another_HUN[rnd.Next(another_HUN.Count)];

                    int correct_answer_index = rnd.Next(3);
                    string[] answers = { another_HUN2, antoher_HUN3, hun_word };
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
            string[] hangmanDrawing = 
            {
                "  +---+  \n  |   |  \n      |  \n      |  \n      |  \n      |  \n=========",
                "  +---+  \n  |   |  \n  O   |  \n      |  \n      |  \n      |  \n=========",
                "  +---+  \n  |   |  \n  O   |  \n  |   |  \n      |  \n      |  \n=========",
                "  +---+  \n  |   |  \n  O   |  \n /|   |  \n      |  \n      |  \n=========",
                "  +---+  \n  |   |  \n  O   |  \n /|\\  |  \n      |  \n      |  \n=========",
                "  +---+  \n  |   |  \n  O   |  \n /|\\  |  \n /    |  \n      |  \n=========",
                "  +---+  \n  |   |  \n  O   |  \n /|\\  |  \n / \\  |  \n      |  \n========="
            };

            int maxHP = 6;
            int drawingIndex = maxHP - Math.Max(0, Math.Min(maxHP, eletSzam));
            Console.WriteLine(hangmanDrawing[drawingIndex]);
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
                Random rnd = new Random();
                List<string> word_LIST = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1: word_LIST = family_italy;   topic = "Család";       break;
                    case 2: word_LIST = it_italy;       topic = "Informatika";  break;
                    case 3: word_LIST = travel_italy;   topic = "Utazás";       break;
                    case 4: word_LIST = weathe_italy;   topic = "Időjárás";     break;
                    case 5: word_LIST = home_italy;     topic = "Lakóhely";     break;
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

                string WORD_Hangman = word_LIST[rnd.Next(word_LIST.Count)];
                int user_HP = WORD_Hangman.Length + 5;
                HashSet<char> triedChar = new HashSet<char>();
                HashSet<char> correct_answers = new HashSet<char>();
                Console.WriteLine("Akasztófa játék: A felhasználónak ki kell találnia a keresett szót betűnként tippelve, akárcsak a klasszikus akasztófajátékban.");
                Console.WriteLine($"\nTéma: {topic}");
                Console.WriteLine($"A szó {WORD_Hangman.Length} betűből áll. Kezdésre {user_HP} életed van.");

                while (user_HP > 0)
                {
                    int missing_CHAR = 0;
                    Console.Write("\nSzó: ");
                    foreach (char character in WORD_Hangman)
                    {
                        if (correct_answers.Contains(character))
                        {
                            Console.Write(character);
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
                        Console.WriteLine("\n Gratulálok! Megnyerted a játékot! ");
                        scoreAKASZTOFA += WORD_Hangman.Length; // Pontszám növelése
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

                    if (triedChar.Contains(guessed_char))
                    {
                        Console.WriteLine(" Már próbálkoztál ezzel a betűvel!");
                        continue;
                    }

                    triedChar.Add(guessed_char);

                    if (WORD_Hangman.Contains(guessed_char))
                    {
                        Console.WriteLine($" A(z) '{guessed_char}' betű szerepel a szóban!");
                        correct_answers.Add(guessed_char);
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
                    Console.WriteLine($"\n Vesztettél! A keresett szó: {WORD_Hangman}");
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

                List<string> Italy_list = new List<string>();
                List<string> Hun_list = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1: Italy_list = family_italy;  Hun_list = family_hun;  topic = "család";       break;
                    case 2: Italy_list = it_italy;      Hun_list = it_hun;      topic = "informatika";  break;
                    case 3: Italy_list = travel_italy;  Hun_list = travel_hun;  topic = "utazás";       break;
                    case 4: Italy_list = weathe_italy;  Hun_list = weather_hun; topic = "időjárás";     break;
                    case 5: Italy_list = home_italy;    Hun_list = home_travel; topic = "lakóhely";     break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }
                Console.WriteLine("Szópárosítás: A program megjeleníti az idegen nyelvű szót, és a felhasználónak be kell írnia a helyes magyar megfelelőjét.");
                Console.WriteLine($"Adja meg az olasz szó magyar megfelelőjét a {topic} témához kapcsolódva!");

                for (int i = 0; i < 5; i++)
                {
                    int index = rnd.Next(Italy_list.Count);
                    string word = Italy_list[index];
                    string word_hun = Hun_list[index];

                    Console.Write(word + " -- ");
                    string answer = Console.ReadLine();

                    if (answer.Trim().ToLower() == word_hun.ToLower())
                    {
                        scorePAROSITAS += 1;
                        Console.WriteLine(" Helyes!");
                    }
                    else
                    {
                        Console.WriteLine($" Hibás! A helyes válasz: {word_hun}");
                    }
                }
                Console.WriteLine("\nEddigi pontok: " + scorePAROSITAS + " pont");
                Console.WriteLine();
            } 
            while (serial_number != 7);
        }

        //4. feladatunk: A felhasználó kiválasztotta a témakört és a szót megkapja ABC sorrendbe állítva, majd ezután kell helyes sorrendbe állítania őket, hogy megkaphassa a helyes megfejtést
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
                    case 1: topic_italy = family_italy; topic = "család";       break;
                    case 2: topic_italy = it_italy;     topic = "informatika";  break;
                    case 3: topic_italy = travel_italy; topic = "utazás";       break;
                    case 4: topic_italy = weathe_italy; topic = "időjárás";     break;
                    case 5: topic_italy = home_italy;   topic = "lakóhely";     break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                HashSet<int> used_index = new HashSet<int>();
                Console.WriteLine("Szókereső: A megadott témakörből kapott szavakat a program összekeveri ABC sorrendben, a felhasználónak pedig helyes sorrendbe kell állítania őket.");
                Console.WriteLine("\nTémakör: " + topic);

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
                    } 
                    while (used_index.Contains(index));

                    used_index.Add(index);
                    string word = topic_italy[index];
                    char[] Words_Char = word.ToCharArray();
                    Array.Sort(Words_Char);
                    string abc_ordered_chars = new string(Words_Char);

                    Console.WriteLine("--------------------");
                    Console.WriteLine(abc_ordered_chars);
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
            } 
            while (true);

            Console.WriteLine($"Köszönjük {username}, hogy a Lingarixel tanultál!");
            Console.WriteLine($"{username} összesen {Pontok()} pontot gyűjtött! Gratulálunk :)");
        }

        //5. feladat: Egy tömbbe bele teszünk 5 a témával kapcsolatos mondatot adott nyelven és tükörfordítással a magyar megfelelőjét 
        public void MondatRendezes()
        {
            List<string> order_hungarian = new List<string>();
            List<string> order_italian = new List<string>();
            string[] datas = File.ReadAllLines("mondatok_olasz.txt");
            for (int i = 0; i < datas.Length; i++)
            {
                string[] line = datas[i].Split(';');
                order_italian.Add(line[0]);
                order_hungarian.Add(line[1]);
            }
            string UserTip;
            int Life = 3;
            Console.WriteLine("Mondatrendezés: Egy összekevert mondatrészletekből álló mondatot kell a felhasználónak helyesen visszaállítania az eredeti formájába.");
            Console.WriteLine("Ha nem tudja kitalálni a mondatot akkor írjon be egy 'x' karaktert");
            Console.WriteLine("------------------------");
            Console.WriteLine("Kevert mondat:");

            Random rnd = new Random();
            do
            {
                int randomSelectedSentence_Italy = rnd.Next(0, order_italian.Count());
                string correctSentence_Hungarian = order_hungarian[randomSelectedSentence_Italy];
                string Italy_ToBeMixed_sentences = order_italian[randomSelectedSentence_Italy];
                string[] broken_Word = Italy_ToBeMixed_sentences.Split(' ');
                string[] Italy_mixed_words = broken_Word.OrderBy(x => rnd.Next()).ToArray();
                string Italy_mixed_sentences = string.Join(" ", Italy_mixed_words);
                Console.WriteLine("Élet:" + Life);
                Console.WriteLine(Italy_mixed_sentences);
                UserTip = Console.ReadLine();

                if (UserTip.Split(' ').OrderBy(s => s).SequenceEqual(Italy_mixed_sentences.Split(' ').OrderBy(s => s)))
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
                    Life--;
                    Console.WriteLine("Élet: " + Life);
                    Console.WriteLine("-------------------");
                    Console.WriteLine();
                }
            }
            while (Life > 0 || UserTip != "x");
            if (UserTip == "x")
            {
                Console.WriteLine("Köszönjük a játékot!");
                Console.WriteLine("Maradt élet:" + Life);
                Console.WriteLine("-------------------");
            }
            if (Life == 0)
            {
                Console.WriteLine("Köszönjük a játékot!");
                Console.WriteLine("Maradt élet:" + Life);
                Console.WriteLine("-------------------");
            }
        }

        //6. feladat: Egy tömbbe 5 mondatot bele teszünk majd a programba meg kerverjük a mondat el rendezését és a felhasználónak be kell írnia a helyesen leírt mondatot
        public void Helyesforditas()
        {
            List<string> sentenceOrdering_Hungarian = new List<string>();
            List<string> sentenceOrdering_Italian = new List<string>();

            string[] datas = File.ReadAllLines("mondatok_olasz.txt");
            for (int i = 0; i < datas.Length; i++)
            {
                string[] line = datas[i].Split(';');
                sentenceOrdering_Italian.Add(line[0]);
                sentenceOrdering_Hungarian.Add(line[1]);
            }

            int rounds = 5;
            Random rnd = new Random();
            List<int> usedIndices = new List<int>();
            Console.WriteLine("Helyes fordítás: A magyar mondat után két lehetőség közül kell kiválasztani a helyes fordítást.");
            Console.WriteLine($"Olasz-magyar mondat kvíz ({rounds} kör)");
            Console.WriteLine("------------------------------\n");

            for (int round = 1; round <= rounds; round++)
            {
                Console.WriteLine($"Kör {round}/{rounds}");

                int currentIndex;
                do
                {
                    currentIndex = rnd.Next(0, sentenceOrdering_Italian.Count);
                }
                while (usedIndices.Contains(currentIndex));
                usedIndices.Add(currentIndex);

                int wrongIndex;
                do
                {
                    wrongIndex = rnd.Next(0, sentenceOrdering_Italian.Count);
                }
                while (wrongIndex == currentIndex || usedIndices.Contains(wrongIndex));

                string hungarian_choosen_sentence = sentenceOrdering_Hungarian[currentIndex];
                string correct_italian_sentence = sentenceOrdering_Italian[currentIndex];
                bool correct_one = rnd.Next(2) == 0;
                string option1 = correct_one ? sentenceOrdering_Italian[currentIndex] : sentenceOrdering_Italian[wrongIndex];
                string option2 = correct_one ? sentenceOrdering_Italian[wrongIndex] : sentenceOrdering_Italian[currentIndex];

                Console.WriteLine("\nA magyar mondat: " + hungarian_choosen_sentence);
                Console.WriteLine("\nLehetőségek:");
                Console.WriteLine("A) " + option1);
                Console.WriteLine("B) " + option2);
                Console.Write("\nAdd meg a válaszod betűjelét (A/B): ");

                string valasz = Console.ReadLine().ToUpper();

                if ((valasz == "A" && correct_one) || (valasz == "B" && !correct_one))
                {
                    Console.WriteLine(" Helyes válasz!");
                    scoreHELYESFORDITAS++;
                }
                else
                {
                    Console.WriteLine($" Helytelen válasz! A helyes válasz: {correct_italian_sentence}");
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
            int pontok = scoreABC + scoreAKASZTOFA + scorePAROSITAS + scoreSZOKERESO + scoreHELYESFORDITAS + scoreMONDATRENDEZES;
            return pontok;
        }
    }
}