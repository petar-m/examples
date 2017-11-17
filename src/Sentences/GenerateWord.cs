using M.Executables;

namespace Sentences
{
    public class GenerateWord : IExecutable<Word>
    {
        private readonly WordGenerator generator;

        public GenerateWord(WordGenerator generator)
        {
            this.generator = generator;
        }

        public Word Execute()
        {
            return generator.Generate();
        }
    }
}
