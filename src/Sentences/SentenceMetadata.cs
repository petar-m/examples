using System;
using System.Linq;
using M.Repository;

namespace Sentences
{
    public class SentenceMetadata : Entity<int>
    {
        protected SentenceMetadata()
        {
        }

        public SentenceMetadata(Sentence sentence)
        {
            Sentence = sentence;
            SentenceId = sentence.Id;
            WordCount = sentence.Value.Split(' ').Length;
            LetterCount = sentence.Value.Count(x => x != ' ');
            CreatedAt = DateTime.Now;
        }

        public Sentence Sentence { get; private set; }

        public int SentenceId { get; private set; }

        public int WordCount { get; private set; }

        public int LetterCount { get; private set; }

        public DateTime CreatedAt { get; private set; }
    }
}
