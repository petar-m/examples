using System.Collections.Generic;
using M.Executables;

namespace Sentences
{
    public class GenerateSentence : IExecutable<IEnumerable<Word>, Sentence>
    {
        private readonly SentenceGenerator generator;

        public GenerateSentence(SentenceGenerator generator)
        {
            this.generator = generator;
        }

        public Sentence Execute(IEnumerable<Word> words)
        {
            return generator.Generate(words);
        }
    }
}
