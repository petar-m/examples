using System.Collections.Generic;
using System.Linq;
using M.Repository;

namespace Sentences
{
    public class Sentence : Entity<int>
    {
        protected Sentence()
        {
        }

        public Sentence(IEnumerable<Word> words)
        {
            Value = string.Join(" ", words.Select(x => x.Value));
        }

        public string Value { get; private set; }
    }
}
