using System.Web.Http;
using M.Executables;
using Sentences;

namespace SentencesHost.Web
{
    public class SentenceController : ApiController
    {
        private readonly IExecutor executor;

        public SentenceController(IExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        [Route("sentence")]
        public IHttpActionResult Foo()
        {
            executor.Execute<AddSentence, Sentence>(new Sentence(new[] { new Word("Hello"), new Word("world!") }));
            return Ok();
        }
    }
}
