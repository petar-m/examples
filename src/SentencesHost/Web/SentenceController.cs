using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using M.Executables;
using Sentences;

namespace SentencesHost.Web
{
    public class SentenceController : ApiController
    {
        private readonly IExecutor executor;
        private readonly IExecutorAsync executorAsync;

        public SentenceController(IExecutor executor, IExecutorAsync executorAsync)
        {
            this.executor = executor;
            this.executorAsync = executorAsync;
        }

        [HttpPost]
        [Route("sentence")]
        public IHttpActionResult Generate([FromBody]string[] input)
        {
            var words = input.Select(x => new Word(x));

            var sentence = executor.Execute<GenerateSentence, IEnumerable<Word>, Sentence>(words);
            executor.Execute<AddSentence, Sentence>(sentence);

            return Ok(sentence.Value);
        }

        [HttpGet]
        [Route("sentence")]
        public async Task<IHttpActionResult> Get([FromUri]int count, [FromUri]bool asc = true)
        {
            var specification = new SentencesSpecification { Count = count, Ascending = asc };

            var sentences = await executorAsync.ExecuteAsync<GetSentences, SentencesSpecification, Sentence[]>(specification);

            return Ok(sentences);
        }

        [HttpGet]
        [Route("sentence/{sentenceId}/metadata")]
        public async Task<IHttpActionResult> Get(int sentenceId)
        {
            var metadata = await executorAsync.ExecuteAsync<GetSentencesMetadata, int, SentenceMetadata>(sentenceId);

            return Ok(metadata);
        }
    }
}
