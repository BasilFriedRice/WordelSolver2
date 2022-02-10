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
    class Solver
    {
        public static RandomWrapper Rand = new RandomWrapper(DateTime.Now.Millisecond, true);
        public List<string> AllWords;
        List<string> BadLetters = new List<string>();
        List<string> SolvedLetters = new List<string>() { "", "", "", "", "", };
        MultiMap<string, string> LetterCounts = new MultiMap<string, string>();
        MultiMap<int, string> BadLettersAtPosition = new MultiMap<int, string>();


        public void SolvePuzzle()
        {
            AllWords = File.ReadAllLines("dictionary10.txt").ToList();




            while (true)
            {
                // clear the per-guess state
                LetterCounts = new MultiMap<string, string>();

                // record the new entry & prune the word list
                string userText = System.Console.ReadLine();
                List<string> userEntries = StringUtil.Split(userText, " ", true).ToList();
                for (int i = 0; i < userEntries.Count; i++)
                {
                    string userEntry = userEntries[i];
                    string letter = userEntry[0].ToString();

                    if (userEntry.Contains("clear"))
                    {
                        // exit the solver & let user start over
                        System.Console.WriteLine("restarting");
                        System.Console.WriteLine("");
                        System.Console.WriteLine("");
                        return;
                    }
                    else if (userEntry.Contains("-"))
                    {
                        PruneBadLetter(letter);
                    }
                    else if (userEntry.Contains("+"))
                    {
                        PruneMisplacedLetter(letter, i);
                    }
                    else
                    {
                        PruneGoodLetter(letter, i);
                    }
                }

                // give user feedback
                System.Console.WriteLine(AllWords.Count.ToString() + " words remaining");
                if (AllWords.Count == 0)
                {
                    System.Console.WriteLine("No words remaining in the dictionary.  Did we fail somehow?");
                }
                else
                {
                    // suggest potential next words
                    int numToPrint = Math.Min(10, AllWords.Count);

                    System.Console.WriteLine("Some potential words:");
                    List<string> potentialWords = RandomUtil.GetRandomPicks(numToPrint, AllWords, Rand);
                    for (int i = 0; i < numToPrint; i++)
                    {
                        System.Console.WriteLine(potentialWords[i]);
                    }
                    System.Console.WriteLine(Environment.NewLine);
                }

            }
        }



        /// <summary>
        /// Remove entries which contain the non-existent letter
        /// </summary>
        private void PruneBadLetter(string badLetter)
        {
            // weed out/check the input data
            if (BadLetters.Contains(badLetter))
            {
                return;
            }

            // update our state
            BadLetters.Add(badLetter);
            foreach (string word in AllWords.ToList())
            {
                if (word.Contains(badLetter))
                {
                    AllWords.Remove(word);
                }
            }
        }


        /// <summary>
        /// Remove entries which do not have the good letter at the verified position
        /// </summary>
        private void PruneGoodLetter(string goodLetter, int pos)
        {
            // check for multiples of a letter
            LetterCounts.Add(goodLetter, goodLetter);
            if (LetterCounts[goodLetter].Count > 1)
            {
                PruneMultiLetter(goodLetter, LetterCounts[goodLetter].Count);
            }

            // weed out/check the input data
            if (SolvedLetters[pos] == goodLetter)
            {
                return;
            }
            else if ((SolvedLetters[pos] != goodLetter) && (!string.IsNullOrEmpty(SolvedLetters[pos])))
            {
                Console.WriteLine("you entered a different good letter in spot " + (pos + 1) + " than you entered earlier.");
                return;
            }

            // update our state
            SolvedLetters[pos] = goodLetter;



            // prune words that do not have the given letter at the given position
            foreach (string word in AllWords.ToList())
            {
                if (word[pos] != goodLetter[0])
                {
                    AllWords.Remove(word);
                }
            }
        }


        /// <summary>
        /// Remove entries which do not conform to the maybe letter
        /// </summary>
        private void PruneMisplacedLetter(string maybeLetter, int pos)
        {
            // check for multiples of a letter
            LetterCounts.Add(maybeLetter, maybeLetter);
            if (LetterCounts[maybeLetter].Count > 1)
            {
                PruneMultiLetter(maybeLetter, LetterCounts[maybeLetter].Count);
            }


            if (BadLettersAtPosition[pos].Contains(maybeLetter))
            {
                return;
            }
            BadLettersAtPosition.Add(pos, maybeLetter);

            // the answer cannot have the maybe letter at the given position
            foreach (string word in AllWords.ToList())
            {
                if (word[pos] == maybeLetter[0])
                {
                    AllWords.Remove(word);
                }
            }

            // the answer must contain the maybe letter
            foreach (string word in AllWords.ToList())
            {
                if (!word.Contains(maybeLetter))
                {
                    AllWords.Remove(word);
                }
            }


            // the answer cannot have the maybe letter at any of the already verified places 
            // - (unless doubles? really, we would need to find the *limit* for this letter before we could go from:
            // verified letter ->
            // kind of a niche case, not sure worth bothering with
            // (would also need to be updated as new letters are verified)
        }

        /// <summary>
        /// 
        /// </summary>
        public void PruneMultiLetter(string letter, int desiredNumOfTargetLetter)
        {
            // remove words that do not have the needed number of the given letter.
            foreach (string word in AllWords.ToList())
            {
                int numNonMatchingLetters = StringUtil.RemoveStringsFromText(word, letter).Length;
                int actualNumOfTargetLetter = 5 - numNonMatchingLetters;

                if (actualNumOfTargetLetter < desiredNumOfTargetLetter)
                {
                    AllWords.Remove(word);
                }
            }
        }
    }
}
