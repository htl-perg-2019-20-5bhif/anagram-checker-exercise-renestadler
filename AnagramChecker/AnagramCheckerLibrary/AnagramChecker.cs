using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramChecker
{
    public class AnagramChecker
    {
        public bool CheckWords(string word1, string word2)
        {
            // Change string to char
            char[] chars1 = word1.ToLower().ToCharArray();
            char[] chars2 = word2.ToLower().ToCharArray();

            //Sort arrays equally
            Array.Sort(chars1);
            Array.Sort(chars2);

            //Compare arrays
            for (int i = 0; i < chars1.Length && i < chars2.Length; i++)
            {
                if (chars1[i] != chars2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<IEnumerable<string>> GetKnownAnagramsAsync(string startWord)
        {
            IEnumerable<WordPair> wordPair = await ListAnagramsFromDictionary();


            List<string> newAnagrams = new List<string>();
            foreach (WordPair anagram in wordPair)
            {
                if (anagram.Word1 == startWord)
                {
                    newAnagrams.Add(anagram.Word2);
                }
                if (anagram.Word2 == startWord)
                {
                    newAnagrams.Add(anagram.Word1);
                }
            }

            List<string> verifiedAnagrams = new List<string>();

            while (newAnagrams.Count != 0)
            {
                var nextNewAnagrams = new List<string>();
                foreach (string word in newAnagrams)
                {
                    foreach (WordPair anagram in wordPair)
                    {
                        if (anagram.Word1 == word)
                        {
                            if (!verifiedAnagrams.Exists(word => (word == anagram.Word2)) &&
                                !newAnagrams.Exists(word => (word == anagram.Word2)) &&
                                anagram.Word2 != startWord)
                            {
                                nextNewAnagrams.Add(anagram.Word2);
                            }
                        }
                        if (anagram.Word2 == word)
                        {
                            if (!verifiedAnagrams.Exists(word => (word == anagram.Word1)) &&
                                !newAnagrams.Exists(word => (word == anagram.Word1)) &&
                                anagram.Word1 != startWord)
                            {
                                nextNewAnagrams.Add(anagram.Word1);
                            }
                        }
                    }
                }
                verifiedAnagrams.AddRange(newAnagrams);
                newAnagrams = nextNewAnagrams;
            }
            return verifiedAnagrams;

        }

        public async Task<IEnumerable<string>> GetPermutationsAsync(string wordToPermute)
        {
            return await Task.FromResult(GeneratePermutations(wordToPermute, 0));
        }

        private async static Task<IEnumerable<WordPair>> ListAnagramsFromDictionary()
        {
            string dictionary = await ReadDictionaryAsync();
            string[] anagramPairs = dictionary.Replace("\r", string.Empty).Split("\n");
            List<WordPair> wordPair = new List<WordPair>();
            foreach (string pair in anagramPairs)
            {
                string[] anagrams = pair.Split(" = ");
                if (anagrams.Length >= 2)
                {
                    wordPair.Add(new WordPair(anagrams[0], anagrams[1]));
                }
            }
            return wordPair;
        }

        private async static Task<string> ReadDictionaryAsync()
        {
            return await System.IO.File.ReadAllTextAsync("Dictionary.txt");
        }

        private static IEnumerable<string> GeneratePermutations(string currentWord, int startIndex)
        {
            List<string> returnList = new List<string>();
            if (startIndex == currentWord.Length - 1)
            {
                returnList.Add(currentWord);
            }
            else
            {
                for (int i = startIndex; i < currentWord.Length; i++)
                {
                    currentWord = SwapChars(currentWord, startIndex, i);
                    returnList.AddRange(GeneratePermutations(currentWord, startIndex + 1));
                    currentWord = SwapChars(currentWord, startIndex, i);
                }
            }

            return returnList;
        }

        public static string SwapChars(string toSwap, int i, int j)
        {
            char[] characters = toSwap.ToCharArray();
            (characters[i], characters[j]) = (characters[j], characters[i]);
            string swappedWord = new string(characters);
            return swappedWord;
        }
    }
}
