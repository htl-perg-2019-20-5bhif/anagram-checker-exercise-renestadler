using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                printUsage();
            }
            else
            {
                switch (args[0])
                {
                    case "check":
                        printCheck(args);
                        break;
                    case "getKnown":
                        printGetKnown(args);
                        break;
                    case "permutation":
                        printPermutation(args);
                        break;
                }
            }
        }

        static void printCheck(string[] args)
        {
            if (args.Length != 3)
            {
                printUsage();
            }
            else
            {
                AnagramChecker anagramChecker = new AnagramChecker();
                if (anagramChecker.CheckWords(args[1], args[2]))
                {
                    Console.WriteLine("\"{0}\" and \"{1}\" are anagrams", args[1], args[2]);
                }
                else
                {
                    Console.WriteLine("\"{0}\" and \"{1}\" are no anagrams", args[1], args[2]);
                }
            }
        }

        static async void printGetKnown(string[] args)
        {
            if (args.Length != 2)
            {
                printUsage();
            }
            else
            {
                AnagramChecker anagramChecker = new AnagramChecker();
                IEnumerable<string> anagrams = await anagramChecker.GetKnownAnagramsAsync(args[1]);
                if (anagrams.Count() > 0)
                {
                    foreach (string anagram in anagrams)
                    {
                        Console.WriteLine("{0}", anagram);
                    }
                }
                else
                {
                    Console.WriteLine("No known anagrams found");
                }
            }
        }
        static async void printPermutation(string[] args)
        {
            if (args.Length != 2)
            {
                printUsage();
            }
            else
            {
                AnagramChecker anagramChecker = new AnagramChecker();
                IEnumerable<string> anagrams = await anagramChecker.GetPermutationsAsync(args[1]);
                foreach (string anagram in anagrams)
                {
                    Console.WriteLine("{0}", anagram);
                }
            }
        }

        static void printUsage()
        {
            Console.WriteLine("Usage:\nAnagramCheckerConsoleClient.exe <command>" +
                "\nCommands:\n check <word1> <word2>\n getKnown <word>\n getPermutations <word>");
        }
    }
}
