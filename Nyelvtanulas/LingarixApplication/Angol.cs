namespace Lingarix
{
    internal class Angol
    {
        List<string> family_english = new List<string>();
        List<string> it_english = new List<string>();
        List<string> travel_english = new List<string>();
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
        public void Beolvas(string usernameFromMVC)
        {
            string[] datas = File.ReadAllLines("angol.txt");
            username = usernameFromMVC;
            for (int i = 0; i < datas.Length; i++)
            {
                string[] line = datas[i].Split(';');
                if (line[2] == "család")
                {
                    family_english.Add(line[0]);
                    family_hun.Add(line[1]);
                }
                if (line[2] == "informatika")
                {
                    it_english.Add(line[0]);
                    it_hun.Add(line[1]);
                }
                if (line[2] == "utazás")
                {
                    travel_english.Add(line[0]);
                    travel_hun.Add(line[1]);
                }
                if (line[2] == "időjárás")
                {
                    weather_english.Add(line[0]);
                    weather_hun.Add(line[1]);
                }
                if (line[2] == "lakóhely")
                {
                    home_english.Add(line[0]);
                    home_hun.Add(line[1]);
                }
            }
            Console.WriteLine("--------------------");
        }
        
        //1. feladatunk: A-B-C lehetőség van a kiírt fordítás helyes megfejtésére
        public void ABC()
        {
            Random random = new Random();
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
                string topic = "";

                switch (serial_number)
                {
                    case 1: topic_english = family_english;     topic_hun = family_hun;     topic = "család";         break;
                    case 2: topic_english = it_english;         topic_hun = it_hun;         topic = "informatika";    break;
                    case 3: topic_english = travel_english;     topic_hun = travel_hun;     topic = "utazás";         break;
                    case 4: topic_english = weather_english;    topic_hun = weather_hun;    topic = "időjárás";       break;
                    case 5: topic_english = home_english;       topic_hun = home_hun;       topic = "lakóhely";       break;
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
                        index = random.Next(topic_english.Count);
                    } 
                    while (used_index.Contains(index));

                    used_index.Add(index);

                    word = topic_english[index];
                    hun_word = topic_hun[index];
                    Console.WriteLine("A - B - C feladat: A három lehetőség közül kell kiválasztania a helyes fordítást a megadott szóra.");
                    Console.WriteLine("\nA {0} témát választotta.", topic);
                    Console.WriteLine("Az angol szó: " + word);

                    List<string> another_HUN = topic_hun.Where((value, idx) => idx != index).ToList();
                    string another_HUN2 = another_HUN[random.Next(another_HUN.Count)];
                    another_HUN.Remove(another_HUN2);
                    string another_HUN3 = another_HUN[random.Next(another_HUN.Count)];

                    int correct_answer_index = random.Next(3);
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
                Console.WriteLine("\nEddigi Score: " + scoreABC + " pont\n");
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
                Random r = new Random();
                List<string> word_LIST = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1: word_LIST = family_english;     topic = "Család";        break;
                    case 2: word_LIST = it_english;         topic = "Informatika";   break;
                    case 3: word_LIST = travel_english;     topic = "Utazás";        break;
                    case 4: word_LIST = weather_english;    topic = "Időjárás";      break;
                    case 5: word_LIST = home_english;       topic = "Lakóhely";      break;
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

                string WORD_Hangman = word_LIST[r.Next(word_LIST.Count)];
                int user_HP = WORD_Hangman.Length + 5;
                HashSet<char> triedChar = new HashSet<char>();
                HashSet<char> correct_answers = new HashSet<char>();
                Console.WriteLine("Akasztófa játék: A felhasználónak ki kell találnia a keresett szót betűnként tippelve,\n akárcsak a klasszikus akasztófajátékban.");
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
                        Console.WriteLine("\n Gratulálok! Megnyerted a játékot!");
                        scoreAKASZTOFA += WORD_Hangman.Length;
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

                List<string> English_list = new List<string>();
                List<string> Hun_list = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1: English_list = family_english;  Hun_list = family_hun;  topic = "család";       break;
                    case 2: English_list = it_english;      Hun_list = it_hun;      topic = "informatika";  break;
                    case 3: English_list = travel_english;  Hun_list = travel_hun;  topic = "utazás";       break;
                    case 4: English_list = weather_english; Hun_list = weather_hun; topic = "időjárás";     break;
                    case 5: English_list = home_english;    Hun_list = home_hun;    topic = "lakóhely";     break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }
                Console.WriteLine("Szópárosítás: A program megjeleníti az idegen nyelvű szót, és a felhasználónak be kell írnia a helyes magyar megfelelőjét.");
                Console.WriteLine($"A {topic} témát választotta.");

                for (int i = 0; i < 5; i++)
                {
                    int index = rnd.Next(English_list.Count);
                    string word = English_list[index];
                    string word_hun = Hun_list[index];

                    Console.Write(word + " -- ");
                    string answer = Console.ReadLine();

                    if (answer.Trim().ToLower() == word_hun.ToLower())
                    {
                        scorePAROSITAS += 1;
                        Console.WriteLine("  Helyes!");
                    }
                    else
                    {
                        Console.WriteLine($" Hibás! A helyes válasz: {word_hun}");
                    }
                }
                Console.WriteLine("\nEddigi Score: " + scorePAROSITAS + " pont");
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
                List<string> topic_english = new List<string>();
                string topic = "";

                switch (serial_number)
                {
                    case 1: topic_english = family_english;     topic = "család";       break;
                    case 2: topic_english = it_english;         topic = "informatika";  break;
                    case 3: topic_english = travel_english;     topic = "utazás";       break;
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
                        index = rnd.Next(topic_english.Count);
                    } 
                    while (used_index.Contains(index));

                    used_index.Add(index);
                    string word = topic_english[index];
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
                Console.WriteLine("Eddigi Score: " + scoreSZOKERESO + " pont");
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
            List<string> order_english = new List<string>();
            string[] datas = File.ReadAllLines("mondatok_angol.txt");
            for (int i = 0; i < datas.Length; i++)
            {
                string[] line = datas[i].Split(';');
                order_english.Add(line[0]);
                order_hungarian.Add(line[1]);
            }
            string UserTip;
            int Life = 3;
            Console.WriteLine("Mondatrendezés: Egy összekevert mondatrészletekből álló mondatot kell a felhasználónak\n helyesen visszaállítania az eredeti formájába.");
            Console.WriteLine("Ha nem tudja kitalálni a mondatot akkor írjon be egy 'x' karaktert");
            Console.WriteLine("------------------------");
            Console.WriteLine("Kevert mondat:");

            Random rnd = new Random(); 
            do
            {
                int randomSelectedSentence_English = rnd.Next(0, order_english.Count());
                string correctSentence_Hungarian = order_hungarian[randomSelectedSentence_English];
                string English_ToBeMixed_sentence = order_english[randomSelectedSentence_English];
                string[] broken_Word = English_ToBeMixed_sentence.Split(' ');
                string[] English_mixed_words = broken_Word.OrderBy(x => rnd.Next()).ToArray();
                string English_mixed_sentences = string.Join(" ", English_mixed_words);
                Console.WriteLine("Élet:" + Life);
                Console.WriteLine(English_mixed_sentences);
                UserTip = Console.ReadLine();

                if (UserTip.Split(' ').OrderBy(s => s).SequenceEqual(English_mixed_sentences.Split(' ').OrderBy(s => s)))
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
            if (UserTip == "x" )
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
            List<string> sentenceOrdering_English = new List<string>();

            string[] datas = File.ReadAllLines("mondatok_angol.txt");
            for (int i = 0; i < datas.Length; i++)
            {
                string[] line = datas[i].Split(';');
                sentenceOrdering_English.Add(line[0]);
                sentenceOrdering_Hungarian.Add(line[1]);
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

                string hungarian_choosen_sentence = sentenceOrdering_Hungarian[currentIndex];
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
            int Score = scoreABC + scoreAKASZTOFA + scorePAROSITAS + scoreSZOKERESO + scoreMONDATRENDEZES + scoreHELYESFORDITAS;
            return Score;
        }
    }
}