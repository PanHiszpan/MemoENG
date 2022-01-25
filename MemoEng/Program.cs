using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MemoEng
{
    class Program
    {

        // opcja zminay klawisza HELP / wogole dodaj 3. Options w menu
        // ranking punktowy / wynik w trakcie randomWord, w menu 4. Score z wynikiem srednim, najlepszym itd / tryb gry do pierwszego bledu, zeby wykrecic jak najlepszy wynik zanim sie pomylisz

        static void Main(string[] args)
        {
            while (true)
            {
                string dicPath = AppDomain.CurrentDomain.BaseDirectory + "\\dic.txt";
                Word[] allWords = Read(dicPath);
                //---Menu---
                Console.Clear();
                Console.WriteLine("Hello you magnificent bastard!");
                Console.WriteLine("1. Random word");
                Console.WriteLine("2. Open File");
                Console.WriteLine("*  Refresh");

                string menuInput = Console.ReadLine();
                switch (menuInput)
                {
                    case "1":
                        allWords = Read(dicPath);
                        RandomWord(allWords);
                        break;
                    case "2":
                        allWords = Read(dicPath);
                        OpenFile(dicPath);
                        break;
                    default:
                        allWords = Read(dicPath);
                        break;
                }
            }
        }


        static Word[] Read(string _dicPath)
        {
            if (!File.Exists(_dicPath))
            {
                File.Create(_dicPath);
                Console.WriteLine("No dic file, add words");
                OpenFile(_dicPath);
                Console.ReadLine();
            }
            string[] lines = File.ReadAllLines(_dicPath);
            Word[] allWords = new Word[lines.Length];
            int i = 0;
            foreach (string s in lines)//kazda linijka w tablicy jest zapisywana w i
            {
                string[] splitLine = s.Split('\t'); //dzieli na osobne stringi po tabach w formie (char)
                allWords[i] = new Word(splitLine);
                i++;
            }
            return allWords;
        }


        static void RandomWord(Word[] allWords)
        {
            while (true)
            {
                Random rand = new Random();
                Word currentWord = allWords[rand.Next(allWords.Length)];
                int howManyWordsPL = currentWord.pl.Length;

                Console.Clear();
                //---ramka gorna---
                foreach (char c in currentWord.eng)
                {
                    Console.Write("-");
                }
                Console.Write("----------\n"); //dadaje znaki dla tych bokow "||   {0}   ||"
                                               //----------------
                Console.WriteLine("||   {0}   ||", currentWord.eng);
                //---ramka dolna---
                foreach (char c in currentWord.eng)
                {
                    Console.Write("-");
                }
                Console.Write("----------\n");
                //----------------
                //wyswietlanie kontekstu jesli jest zapisany
                if (currentWord.context != null)
                {
                    Console.WriteLine("Press H for HELP\n");
                }
                else
                {
                    Console.WriteLine("No help fot this one :(\n");
                }

                //Input uzytkownika
                string menuRandomWordInput = Console.ReadLine();


                switch (menuRandomWordInput)
                {
                    case ("2"): //wychodzi do menu
                        return;
                    case ("H"): //wypluwa kontekst
                        if (currentWord.context != null)
                        {
                            Console.WriteLine(currentWord.context);
                        }
                        menuRandomWordInput = Console.ReadLine(); //zczytuje jeszcze raz i przekzuje jako opdowiedz uzytkownika
                        checkPlayerAnswer(howManyWordsPL, currentWord.pl, menuRandomWordInput);
                        break;
                    default:
                        //-------- sprawdzanie opdowiedzi uzytkownika ------------
                        //zakladam ze dafault wpisze odpowiedz
                        checkPlayerAnswer(howManyWordsPL, currentWord.pl, menuRandomWordInput);
                        break;
                }
                //powinno sie wyswietlic na koncu /tzn. po wyswietleniu slowa po eng, po pomocy, po zweryfikowaniu odpowiedzi
                Console.Write("\n2 <- | -> Enter\n");
                if (Console.ReadLine() == "2") //wraca do menu
                {
                    return;
                }
            }
        }
        static void checkPlayerAnswer(int _howManyWordsPL, string[] _pl, String playerAnswer)
        {
            //szuka odpowiedzi uzytkownika w liscie pl
            bool rightAnswer = false;
            foreach (string s in _pl)
            {
                if (playerAnswer == s)
                {
                    Console.WriteLine("\nNice!");
                    rightAnswer = true; //jak znalazlo znaczy ze dobrze odpowiedzial
                }
            }
            if (!rightAnswer) //jesli podanego przez uzytkownika slowa nie ma w liscie dobrch opdowiedzi (pl)
            {
                Console.WriteLine("\nYou stupid bastard!\nIt means:\n");
                //---ramka gorna---
                ramka(_howManyWordsPL, _pl);

                Console.Write("||   ");
                if (_howManyWordsPL == 1)
                {
                    Console.Write("{0}", _pl[0]); //wypluwa 1 slowo
                }
                else if (_pl.Length > 1)
                {
                    for (int i = 0; i < _howManyWordsPL; i++)
                    {
                        Console.Write("{0}", _pl[i]);
                        if (i == _howManyWordsPL - 1) //jesli to ostatnie slowo, to nie dawaj seraratora
                        { break; }
                        else { Console.Write(" | "); }
                    }

                }
                Console.Write("   ||\n");
                //---ramka dolna---
                ramka(_howManyWordsPL, _pl);

            }
        }

        static void ramka(int _howManyWordsPL, string[] _pl)
        {
            foreach (string s in _pl) //za kazde slowo
            {
                foreach (char c in s) //za kazda litere w slowie
                {
                    Console.Write("-");
                }
            }
            if (_howManyWordsPL > 1)
            {
                for (int i = 0; i < _howManyWordsPL - 1; i++) //separatory = slowa - 1 / przy 2 slowach chce 1 separator itd
                {
                    Console.Write("---"); //dadaje znaki dla separatorow " | "
                }
            }
            Console.Write("----------\n");//dadaje znaki dla tych bokow "||   {0}   ||"
        }

        static void OpenFile(string _dicPath)
        {
            Process.Start("notepad.exe", _dicPath); //wysupuje sie jak nie znajdzie pliku, stwarza nowy i zglasza ze jest juz w uzyciu
        }
    }
}
