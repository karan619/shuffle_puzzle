using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
namespace Shuffle_2
{
    public class infos //this needs to be public because serialization cannot be on protected classes
    {
        public string names { get; set; }
        public int scores { get; set; }
    }
    class Program
    {
        StreamReader r = new StreamReader("info.xml");
        infos info = new infos();
        static string highname;
        static int highscore;
        static string currentuser;
        static int moves = 0;
        static bool loop = true;
        static int[] numbers = new int[9];
        public static void rules()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("The game is, You get a shuffled 3x3 grid\n\n5\t7\t8\n2\t0\t6\n4\t3\t1\n");
            //Thread.Sleep(3000);
            Console.WriteLine("Swap the shuffled numbers with 0\n");
            //Thread.Sleep(4000);
            Console.WriteLine("\n\n1\t2\t3\n4\t5\t6\n7\t8\t0\n\n");
            //Thread.Sleep(5000);
            Console.WriteLine("Use the arrow keys, Make it asscending..Pressing d will finish the game\n\n");
            //Thread.Sleep(3000);
            Console.WriteLine("Making lesser key presses will save your name in the highest scorer..");
            //Thread.Sleep(3000);
            Console.ReadKey();
            Console.Clear();
        }

        public static void print()
        {
            infos inf = new infos();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();
            Console.WriteLine("HighSCorer: " + highname); Console.WriteLine("\nKeyPresses: " + highscore);
            Console.Write("\n\n\n\t\t\t|{0}|\t\t|{1}|\t\t|{2}|\t\t\t\n\n\n\t\t\t|{3}|\t\t|{4}|\t\t|{5}|\t\t\t\n\n\n\t\t\t|{6}|\t\t|{7}|\t\t|{8}|\t\t\t\n", numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5], numbers[6], numbers[7], numbers[8]);
            Console.WriteLine("\n\nNow playing: " + currentuser + "\n\n" + "Your KeyPresses: " + moves);
            Console.Write("\n\n\nSwap 0 using arrow keys\t\tPress 'D' to Submit");
        }
        public void Reader()
        {
            infos inf = new infos();
            XmlSerializer xmlsr = new XmlSerializer(inf.GetType());
            File.Create("info.xml");
            object disp = xmlsr.Deserialize(r);
            inf = (infos)disp;
            Console.WriteLine("Reading..");
            highname = inf.names;
            highscore = inf.scores;
            r.Close();
        }
        public void writer()
        {
            r.Close();
            infos inf = new infos();
            Console.Clear();
            Console.WriteLine("Error in reading records:\nEnter your name new user");
            inf.names = Console.ReadLine();
            inf.scores = 0;
            XmlSerializer xmlsr = new XmlSerializer(inf.GetType());
            StreamWriter w = new StreamWriter("info.xml");
            xmlsr.Serialize(w, inf);
            Console.WriteLine("Writing complete");
            w.Flush();
            w.Close();
        }
        public static void checker()
        {
            if (
                numbers[0] == 1 &&
                numbers[1] == 2 &&
                numbers[2] == 3 &&
                numbers[3] == 4 &&
                numbers[4] == 5 &&
                numbers[5] == 6 &&
                numbers[6] == 7 &&
                numbers[7] == 8 &&
                numbers[8] == 0
                )
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nCongo!!!\nYou Completed in {0} moves", moves);
                if (moves != 0 && moves < highscore)
                {
                    Console.WriteLine(currentuser + "You made a High Score");
                    Program p = new Program();
                    p.writer();
                }
            }
            else
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\t\tDid not match!!!\t\tTry Again.\n Press any key to continue");
                Console.ReadKey();
                print();
            }
        }
        public static void Auto_arrange()
        {
            numbers[0] = 1;
            numbers[1] = 2;
            numbers[2] = 3;
            numbers[3] = 4;
            numbers[4] = 5;
            numbers[5] = 6;
            numbers[6] = 7;
            numbers[7] = 8;
            numbers[8] = 0;
            print();
        }
        static void Main(string[] args)
        {
            Console.Title = "Karan Valecha";
            Console.CursorVisible = false;
            Program p = new Program();
            Console.WriteLine("Preparing rules..");
            Thread.Sleep(2000);
            rules();
            Console.Clear();
            Console.WriteLine("Enter a Name:");
            currentuser = Console.ReadLine();
            if (File.Exists("info.xml") == true)
            {
                Console.Write("Previous Records Found:\n");
                Thread.Sleep(2000);
                try
                {
                    p.Reader();
                }
                catch
                {
                    p.writer();
                }
            }
            else
            {
                Console.Write("Fille Does Not Exists");
                Thread.Sleep(2000);
                p.writer();
            }
            
            //th.Abort();
            //Beginning of shuffling random 10 numbers
            Random r = new Random();
            int seed = r.Next(10); int increament = 2;
            int n = 9;
            int x = seed;
            for (int i = 0; i < 9; i++)
            {
                x = (x + increament) % n;
                numbers[i] = x;
                Console.Clear();
            }//shuffling ended
            print();
            ConsoleKeyInfo k;
            while (loop == true)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (numbers[i] == 0)
                    {
                        k = Console.ReadKey();
                        {
                            if (k.Key == ConsoleKey.UpArrow && numbers[i] != numbers[0] && numbers[i] != numbers[1] && numbers[i] != numbers[2])
                            {
                                numbers[i] = numbers[i - 3];
                                numbers[i - 3] = 0;
                                moves++;
                                Console.Clear();
                                print();
                            }
                            if (k.Key == ConsoleKey.DownArrow && numbers[i] != numbers[6] && numbers[i] != numbers[7] && numbers[i] != numbers[8])
                            {
                                numbers[i] = numbers[i + 3];
                                numbers[i + 3] = 0;
                                moves++;
                                Console.Clear();
                                print();
                            }
                            if (k.Key == ConsoleKey.LeftArrow && numbers[i] != numbers[3] && numbers[i] != numbers[6] && numbers[i] != numbers[0])
                            {
                                numbers[i] = numbers[i - 1];
                                numbers[i - 1] = 0;
                                moves++;
                                Console.Clear();
                                print();
                            }
                            if (k.Key == ConsoleKey.RightArrow && numbers[i] != numbers[2] && numbers[i] != numbers[5])
                            {
                                numbers[i] = numbers[i + 1];
                                numbers[i + 1] = 0;
                                moves++;
                                Console.Clear();
                                print();
                            }
                            if (k.Key == ConsoleKey.D)
                            {
                                checker();
                            }
                            if (k.Key == ConsoleKey.Escape)
                            {
                                loop = false;
                            }
                            if (k.Key == ConsoleKey.X)
                            {
                                Auto_arrange();
                            }
                        }
                    }
                }
            }
        }
    }
}
