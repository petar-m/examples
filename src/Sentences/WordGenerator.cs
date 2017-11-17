using System;

namespace Sentences
{
    public class WordGenerator
    {
        private static Random generator = new Random();
        
        public Word Generate()
        {
            var length = generator.Next(1, 12);
            var word = new char[length];
            for(int i = 0; i < length; i++)
            {
                word[i] = Char();
            }

            return new Word(new string(word));
        }

        private char Char()
        {
            const int min = 'a';
            const int max = 'z';

            return (char)generator.Next(min, max);
        }
    }
}
