using Sentences;

namespace SentencesHost.Events
{
    public class WordCreated
    {
        public WordCreated(Word word)
        {
            Word = word;
        }

        public Word Word { get; }
    }
}
