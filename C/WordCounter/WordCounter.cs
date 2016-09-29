namespace WordCounter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class WordCounter
    {
        public const string WordSeparators = " ,.:[]()<>";

        public static int Count(StreamReader text, string word)
        {
            int count = 0;
            while (!text.EndOfStream)
            {
                count +=
                    text.ReadLine()?
                        .Split(WordSeparators.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        .Count(x => x.Equals(word, StringComparison.InvariantCultureIgnoreCase)) ?? 0;
            }

            return count;
        }

        public static int AllWordsCount(StreamReader text)
        {
            int count = 0;

            while (!text.EndOfStream)
            {
                count += text.ReadLine()?.Split(WordSeparators.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length ?? 0;
            }

            return count;
        }

        public static void AnalyzeFile(StreamReader text, Dictionary<string, uint> allWords)
        {
            while (!text.EndOfStream)
            {
                var words = text.ReadLine()?
                    .Split(WordSeparators.ToCharArray(), StringSplitOptions.RemoveEmptyEntries) ?? new string[] { };

                foreach (var word in words)
                {
                    if (allWords.ContainsKey(word))
                    {
                        allWords[word]++;
                    }
                    else
                    {
                        allWords.Add(word, 1);
                    }
                }
            }
        }
    }
}
