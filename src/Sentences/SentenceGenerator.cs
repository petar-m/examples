using System;
using System.Collections.Generic;
using System.Linq;

namespace Sentences
{
    public class SentenceGenerator
    {
        private static Random generator = new Random();

        public Sentence Generate(IEnumerable<Word> words)
        {
            var wordsArray = words.ToArray();
            var wordsCount = generator.Next(1, wordsArray.Length);
            var sentence = new Word[wordsCount];
            for (int i = 0; i < sentence.Length; i++)
            {
                sentence[i] = wordsArray[generator.Next(0, wordsArray.Length - 1)];
            }

            return new Sentence(sentence);
        }
    }
}
