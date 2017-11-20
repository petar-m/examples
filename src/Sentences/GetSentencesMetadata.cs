using System.Threading.Tasks;
using M.Executables;
using M.Repository;

namespace Sentences
{
    public class GetSentencesMetadata : IExecutableAsync<int, SentenceMetadata>
    {
        private readonly IRepository<SentenceMetadata> sentenceMetadata;

        public GetSentencesMetadata(IRepository<SentenceMetadata> sentenceMetadata)
        {
            this.sentenceMetadata = sentenceMetadata;
        }

        public async Task<SentenceMetadata> ExecuteAsync(int sentenceId)
        {
            var query =
                QueryBuilder.ByDelegateAsync<SentenceMetadata, SentenceMetadata>(
                    x => x.SingleOrDefaultAsync(s => s.SentenceId == sentenceId), readOnly: true)
                    .Include(x => x.Sentence);
            return await sentenceMetadata.GetByAsync(query);
        }
    }
}
