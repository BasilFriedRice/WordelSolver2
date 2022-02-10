using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Utilities;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace WordelSolver
{
    class Program
    {


        static void Main(string[] args)
        {

            // try1 
            // HashSet<string> resultWords = new HashSet<string>();
            //List<string> wordLines = File.ReadAllLines("dictionary1.txt").ToList();
            //foreach (string wordLine in wordLines)
            //{
            //    string[] chunks = StringUtil.Split(wordLine, "\"", true);
            //    string chunk = chunks[0];
            //    if ((chunk.Length == 5) && (!chunk.Contains(" ")) && (!chunk.Contains("-")) && (!chunk.Contains("'")))
            //    {
            //        resultWords.Add(chunks[0]);
            //    }
            //}


            // try 2 - working
            // HashSet<string> resultWords = new HashSet<string>();
            //List<string> wordLines = File.ReadAllLines("dictionary3.txt").ToList();
            //foreach (string wordLine in wordLines)
            //{
            //    string[] chunks = StringUtil.Split(wordLine, "/", true);
            //    string chunk = chunks[0];
            //    if ((chunk.Length == 5) && (!chunk.Contains(" ")) && (!chunk.Contains("-")) && (!chunk.Contains("'")))
            //    {
            //        resultWords.Add(chunks[0]);
            //    }
            //}
            //int blah = 2;
            //File.WriteAllLines("dictionary5.txt", resultWords.ToArray<string>());



            // try 3 - working
            // HashSet<string> resultWords = new HashSet<string>();
            //List<string> wordLines = File.ReadAllLines("dic6.txt").ToList();
            //foreach (string wordLine in wordLines)
            //{
            //    string[] chunks = StringUtil.Split(wordLine, "/", true);
            //    string chunk = chunks[0];
            //    if ((chunk.Length == 5) && (!chunk.Contains(" ")) && (!chunk.Contains("-")) && (!chunk.Contains("'")))
            //    {
            //        resultWords.Add(chunks[0]);
            //    }
            //}
            //int blah = 2;
            //File.WriteAllLines("dictionary10.txt", resultWords.ToArray<string>());
            //return;

            
            Console.WriteLine("usage: $+ for letters that are present but not in right slot");
            Console.WriteLine("usage: $- for letters that are not present.");
            Console.WriteLine("usage: $ for correct letters");
            Console.WriteLine("");
            Console.WriteLine("Example:");
            Console.WriteLine("g- r- e- a+ t-");
            Console.WriteLine("f- o- a+ m- y-");
            Console.WriteLine("p- a n+ i+ c-");
            Console.WriteLine("n a i l s");
            Console.WriteLine("");
            Console.WriteLine("");
            



            while (true)
            {
                Solver solver = new Solver();
                solver.SolvePuzzle();
            }
        }

    }
}
