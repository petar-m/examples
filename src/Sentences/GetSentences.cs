using System;
using System.Linq;
using System.Threading.Tasks;
using M.Executables;
using M.Repository;

namespace Sentences
{
    public class GetSentences : IExecutableAsync<SentencesSpecification, Sentence[]>
    {
        private readonly IRepository<Sentence> sentenceRepository;

        public GetSentences(IRepository<Sentence> sentenceRepository)
        {
            this.sentenceRepository = sentenceRepository;
        }

        public async Task<Sentence[]> ExecuteAsync(SentencesSpecification specification)
        {
            return await sentenceRepository.GetByAsync(new Query(specification));
        }

        private class Query : QueryAsync<Sentence, Sentence[]>
        {
            private readonly SentencesSpecification specification;

            public Query(SentencesSpecification specification)
            {
                this.specification = specification;
                ReadOnly = true;
            }

            public override Task<Sentence[]> ExecuteAgainstAsync(IQueryable<Sentence> items)
            {
                Func<IQueryable<Sentence>, IOrderedQueryable<Sentence>> order =
                    specification.Ascending
                    ? order = x => x.OrderBy(s => s.Id)
                    : order = x => x.OrderByDescending(s => s.Id);

                return order(items).Take(specification.Count).ToArrayAsync();
            }
        }
    }
}
