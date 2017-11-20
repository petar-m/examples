using Sentences;

namespace SentencesHost.Events
{
    public class SentenceCreated
    {
        public SentenceCreated(Sentence sentence)
        {
            Sentence = sentence;
        }

        public Sentence Sentence { get; }
    }
}
