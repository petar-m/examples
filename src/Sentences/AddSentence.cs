using M.Executables;
using M.Repository;

namespace Sentences
{
    public class AddSentence : IExecutableVoid<Sentence>
    {
        private readonly IRepository<Sentence> sentenceRepository;
        private readonly IRepository<SentenceMetadata> sentenceMetadataRepository;

        public AddSentence(
            IRepository<Sentence> sentenceRepository,
            IRepository<SentenceMetadata> sentenceMetadataRepository)
        {
            this.sentenceRepository = sentenceRepository;
            this.sentenceMetadataRepository = sentenceMetadataRepository;
        }

        public void Execute(Sentence sentence)
        {
            sentenceRepository.Add(sentence);
            var metadata = new SentenceMetadata(sentence);
            sentenceMetadataRepository.Add(metadata);
        }
    }
}
