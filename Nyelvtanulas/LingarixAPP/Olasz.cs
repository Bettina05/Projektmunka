using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lingarix
{
    internal class Olasz
    {
        //Idegennyelvű szavak
        List<string> csalad_olasz = new List<string>();
        List<string> info_olasz = new List<string>();
        List<string> utazas_olasz = new List<string>();
        List<string> idojaras_olasz = new List<string>();
        List<string> lakohely_olasz = new List<string>();

        //Magyar szavak
        List<string> csalad_magyar = new List<string>();
        List<string> info_magyar = new List<string>();
        List<string> utazas_magyar = new List<string>();
        List<string> idojaras_magyar = new List<string>();
        List<string> lakohely_magyar = new List<string>();

        ///<summary>
        /// A felhasználó felhasználóneve
        /// </summary>
        string username = "";
        /// <summary>
        /// A pontokat itt számoljuk az ABC feladatnál
        /// </summary>
        int pontszamABC;

        /// <summary>
        /// A pontokat itt számoljuk a szópárosításos feladatnál
        /// </summary>
        int pontszamPAROSITAS;

        /// <summary>
        /// A pontokat itt számoljuk az akasztófa feladathoz
        /// </summary>
        int pontszamAKASZTOFA;

        /// <summary>
        /// A pontokat itt számoljuk a szókereső feladathoz
        /// </summary>
        int pontszamSZOKERESO;

        /// <summary>
        /// Itt adják meg a feladat sorszámát
        /// </summary>
        int sorszam;

        /// <summary>
        /// A kitalálandó szavakat tároljuk benne, minden egyes feladatban idegennyelvű
        /// </summary>
        string szo = "";

        /// <summary>
        /// A kitalálandó szavak magyar megfelelőjét, megfejtését tároljuk benne
        /// </summary>
        string megfejtes;

        /// <summary>
        /// A kitalálandó szavakat tároljuk benne, minden egyes feladatban magyar nyelvű
        /// </summary>
        string magyarSzo;

        int index = 0;

        /// <summary>
        /// A szópárosításhoz tartozó szavak betűi abc sorrendbe rendezve
        /// </summary>
        string abc_rendezett_betuk;

        /// <summary>
        /// A szópárosításhoz tartozó megoldás a felhasználó által beírva
        /// </summary>
        string szokereso;

        /// <summary>
        /// Témaköröket tároljuk benne
        /// </summary>
        string temakor = "";

        /// <summary>
        /// Megakadályozza, hogy a megfejtendő szavak ismétlődjenek
        /// </summary>
        static Random szam = new Random();
        //A dokumentumból beolvassuk a szavakat és a témaköröket
        public void Beolvas(string felhasznalonev)
        {
            string[] adatok = File.ReadAllLines("olasz.txt");
            username = felhasznalonev;
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] sor = adatok[i].Split(';');
                if (sor[2] == "család")
                {
                    csalad_olasz.Add(sor[0]);
                    csalad_magyar.Add(sor[1]);
                }
                if (sor[2] == "informatika")
                {
                    info_olasz.Add(sor[0]);
                    info_magyar.Add(sor[1]);
                }
                if (sor[2] == "utazás")
                {
                    utazas_olasz.Add(sor[0]);
                    utazas_magyar.Add(sor[1]);
                }
                if (sor[2] == "időjárás")
                {
                    idojaras_olasz.Add(sor[0]);
                    idojaras_magyar.Add(sor[1]);
                }
                if (sor[2] == "lakóhely")
                {
                    lakohely_olasz.Add(sor[0]);
                    lakohely_magyar.Add(sor[1]);
                }
            }

            Console.WriteLine("--------------------");
        }
        //Első feladatunk: A-B-C lehetőség van a kiírt fordítás helyes megfejtésére
        public void ABC()
        {
            Random szam = new Random();
            HashSet<int> hasznaltIndexek = new HashSet<int>(); // Tárolja a már kiválasztott szavakat

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
                sorszam = Convert.ToInt16(Console.ReadLine());

                if (sorszam == 6) break;

                List<string> temakor_francia = new List<string>();
                List<string> temakor_magyar = new List<string>();
                string temakor = "";

                switch (sorszam)
                {
                    case 1:
                        temakor_francia = csalad_olasz;
                        temakor_magyar = csalad_magyar;
                        temakor = "család";
                        break;

                    case 2:
                        temakor_francia = info_olasz;
                        temakor_magyar = info_magyar;
                        temakor = "informatika";
                        break;

                    case 3:
                        temakor_francia = utazas_olasz;
                        temakor_magyar = utazas_magyar;
                        temakor = "utazás";
                        break;

                    case 4:
                        temakor_francia = idojaras_olasz;
                        temakor_magyar = idojaras_magyar;
                        temakor = "időjárás";
                        break;

                    case 5:
                        temakor_francia = lakohely_olasz;
                        temakor_magyar = lakohely_magyar;
                        temakor = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                hasznaltIndexek.Clear(); // Minden új témánál töröljük a korábbi szavakat

                for (int i = 0; i < 5; i++) // 5 szó bekérése egy témából
                {
                    if (hasznaltIndexek.Count == temakor_francia.Count) // Ha minden szó elfogyott, újra kezdjük
                    {
                        hasznaltIndexek.Clear();
                    }

                    int index;
                    do
                    {
                        index = szam.Next(temakor_francia.Count);
                    } while (hasznaltIndexek.Contains(index)); // Ellenőrizzük, hogy ne ismétlődjön

                    hasznaltIndexek.Add(index); // Elmentjük a felhasznált indexet

                    szo = temakor_francia[index];
                    magyarSzo = temakor_magyar[index];

                    Console.WriteLine("\nA {0} témában válassza ki a megfelelő választ az 'a/b/c' lehetőségek közül!", temakor);
                    Console.WriteLine("A olasz szó: " + szo);

                    List<string> masikMagyarok = temakor_magyar.Where((value, idx) => idx != index).ToList();
                    string masikMagyar1 = masikMagyarok[szam.Next(masikMagyarok.Count)];
                    masikMagyarok.Remove(masikMagyar1);
                    string masikMagyar2 = masikMagyarok[szam.Next(masikMagyarok.Count)];

                    int helyesValaszIndex = szam.Next(3);
                    string[] valaszok = { masikMagyar1, masikMagyar2, magyarSzo };
                    (valaszok[helyesValaszIndex], valaszok[2]) = (valaszok[2], valaszok[helyesValaszIndex]);

                    Console.WriteLine("a) " + valaszok[0]);
                    Console.WriteLine("b) " + valaszok[1]);
                    Console.WriteLine("c) " + valaszok[2]);

                    string megfejtes = Console.ReadLine();
                    if (megfejtes == new[] { "a", "b", "c" }[helyesValaszIndex])
                    {
                        pontszamABC += 1;
                    }
                }

                Console.WriteLine("\nEddigi pontok: " + pontszamABC + " pont\n");
            }
            while (true);

            Console.WriteLine("Köszönjük, hogy velünk tanultál " + username + "!");
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
                sorszam = Convert.ToInt16(Console.ReadLine());

                List<string> olaszLista = new List<string>();
                List<string> magyarLista = new List<string>();
                string temakor = "";

                switch (sorszam)
                {
                    case 1:
                        olaszLista = csalad_olasz;
                        magyarLista = csalad_magyar;
                        temakor = "család";
                        break;
                    case 2:
                        olaszLista = info_olasz;
                        magyarLista = info_magyar;
                        temakor = "informatika";
                        break;
                    case 3:
                        olaszLista = utazas_olasz;
                        magyarLista = utazas_magyar;
                        temakor = "utazás";
                        break;
                    case 4:
                        olaszLista = idojaras_olasz;
                        magyarLista = idojaras_magyar;
                        temakor = "időjárás";
                        break;
                    case 5:
                        olaszLista = lakohely_olasz;
                        magyarLista = lakohely_magyar;
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
                    int index = szam.Next(olaszLista.Count);  // Véletlenszerű index
                    string szo = olaszLista[index];
                    string magyarSzo = magyarLista[index];

                    Console.Write(szo + " -- ");
                    string megfejtes = Console.ReadLine();

                    if (megfejtes.Trim().ToLower() == magyarSzo.ToLower())
                    {
                        pontszamPAROSITAS += 1;
                        Console.WriteLine(" Helyes!");
                    }
                    else
                    {
                        Console.WriteLine($" Hibás! A helyes válasz: {magyarSzo}");
                    }
                }

                Console.WriteLine("\nEddigi pontok: " + pontszamPAROSITAS + " pont");
                Console.WriteLine();

            } while (sorszam != 7);
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

            int maxEletSzam = 6;
            int rajzIndex = maxEletSzam - Math.Max(0, Math.Min(maxEletSzam, eletSzam));
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
                sorszam = Convert.ToInt16(Console.ReadLine());

                if (sorszam == 6) break; // Kilépés esetén kilépünk a ciklusból

                Random r = new Random();
                List<string> szoLista = new List<string>();
                string temaNeve = "";

                switch (sorszam)
                {
                    case 1: szoLista = csalad_olasz; temaNeve = "Család"; break;
                    case 2: szoLista = info_olasz; temaNeve = "Informatika"; break;
                    case 3: szoLista = utazas_olasz; temaNeve = "Utazás"; break;
                    case 4: szoLista = idojaras_olasz; temaNeve = "Időjárás"; break;
                    case 5: szoLista = lakohely_olasz; temaNeve = "Lakóhely"; break;
                    default: Console.WriteLine("Hibás választás!"); continue;
                }

                if (szoLista.Count == 0)
                {
                    Console.WriteLine("Nincs elérhető szó ebben a témakörben!");
                    continue;
                }

                string keresettSzo = szoLista[r.Next(szoLista.Count)];
                int elet = keresettSzo.Length + 5;
                HashSet<char> kiprobaltBetuk = new HashSet<char>();
                HashSet<char> helyesTalalatok = new HashSet<char>();

                Console.WriteLine($"\nAkasztófa játék - Téma: {temaNeve}");
                Console.WriteLine($"A szó {keresettSzo.Length} betűből áll. Kezdésre {elet} életed van.");

                while (elet > 0)
                {
                    int hianyzoBetuk = 0;
                    Console.Write("\nSzó: ");
                    foreach (char kar in keresettSzo)
                    {
                        if (helyesTalalatok.Contains(kar))
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
                        pontszamAKASZTOFA += keresettSzo.Length; // Pontszám növelése
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

                    if (kiprobaltBetuk.Contains(betu))
                    {
                        Console.WriteLine(" Már próbálkoztál ezzel a betűvel!");
                        continue;
                    }

                    kiprobaltBetuk.Add(betu);

                    if (keresettSzo.Contains(betu))
                    {
                        Console.WriteLine($" A(z) '{betu}' betű szerepel a szóban!");
                        helyesTalalatok.Add(betu);
                    }
                    else
                    {
                        elet--;
                        Console.WriteLine($" Sajnos a(z) '{betu}' nincs a szóban! {elet} életed maradt.");
                        RajzolAkasztofa(elet);
                    }
                }

                if (elet == 0)
                {
                    Console.WriteLine($"\n Vesztettél! A keresett szó: {keresettSzo}");
                }

                Console.WriteLine("\n***************************************");
                Console.WriteLine($"Jelenlegi pontszámod: {pontszamAKASZTOFA} pont");
                Console.WriteLine("***************************************\n");

            } while (true);

            Console.WriteLine("Köszönjük, hogy velünk tanultál! ");
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
                sorszam = Convert.ToInt16(Console.ReadLine());

                if (sorszam == 6) break;

                List<string> temakor_francia = new List<string>();
                string temakor = "";

                switch (sorszam)
                {
                    case 1:
                        temakor_francia = csalad_olasz;
                        temakor = "család";
                        break;

                    case 2:
                        temakor_francia = info_olasz;
                        temakor = "informatika";
                        break;

                    case 3:
                        temakor_francia = utazas_olasz;
                        temakor = "utazás";
                        break;

                    case 4:
                        temakor_francia = idojaras_olasz;
                        temakor = "időjárás";
                        break;

                    case 5:
                        temakor_francia = lakohely_olasz;
                        temakor = "lakóhely";
                        break;
                    case 6:
                        Console.WriteLine("Köszönjük, hogy velünk tanultál, {0}!", username);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        continue;
                }

                HashSet<int> hasznaltIndexek = new HashSet<int>();

                Console.WriteLine("\nTémakör: " + temakor);
                Console.WriteLine("A megadott betűk alapján találja ki a szót és írja be a kijelölt helyre!");

                for (int i = 0; i < 5; i++) // 5 különböző szó kiválasztása
                {
                    if (hasznaltIndexek.Count == temakor_francia.Count)
                    {
                        hasznaltIndexek.Clear();
                    }

                    int index;
                    do
                    {
                        index = szam.Next(temakor_francia.Count);
                    } while (hasznaltIndexek.Contains(index));

                    hasznaltIndexek.Add(index);
                    string szo = temakor_francia[index];

                    char[] betuk = szo.ToCharArray();
                    Array.Sort(betuk);
                    string abc_rendezett_betuk = new string(betuk);

                    Console.WriteLine("--------------------");
                    Console.WriteLine(abc_rendezett_betuk);
                    Console.WriteLine("--------------------");

                    string szokereso = Console.ReadLine();

                    if (szokereso == szo)
                    {
                        pontszamSZOKERESO++;
                        Console.WriteLine(" Helyes válasz!");
                    }
                    else
                    {
                        Console.WriteLine("********************");
                        Console.WriteLine("Nem jó :(");
                        Console.WriteLine("A helyes válasz: " + szo);
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
            int pontok = pontszamABC + pontszamAKASZTOFA + pontszamPAROSITAS + pontszamSZOKERESO;
            return pontok;
        }
    }
}
