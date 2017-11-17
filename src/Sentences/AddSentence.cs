using M.Executables;
using M.Repository;

namespace Sentences
{
    public class AddSentence : IExecutableVoid<Sentence>
    {
        private readonly IRepository<Sentence> sentenceRepository;

        public AddSentence(IRepository<Sentence> sentenceRepository)
        {
            this.sentenceRepository = sentenceRepository;
        }

        public void Execute(Sentence sentence)
        {
            sentenceRepository.Add(sentence);
        }
    }
}
