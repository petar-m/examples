using System.Collections.Generic;
using System.Linq;
using Sentences;

namespace SentencesHost.Events
{
    public class WordSetCreated
    {
        public WordSetCreated(IEnumerable<Word> words)
        {
            Words = words.ToArray();
        }

        public Word[] Words { get; }
    }
}
