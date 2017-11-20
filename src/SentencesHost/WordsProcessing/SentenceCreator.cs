using System.Collections.Generic;
using M.EventBroker;
using M.Executables.Executors.SimpleInjector;
using Sentences;
using SentencesHost.Events;

namespace SentencesHost.WordsProcessing
{
    public class SentenceCreator : IEventHandler<WordSetCreated>
    {
        private readonly IExecutorScope scope;

        public SentenceCreator(IExecutorScope scope)
        {
            this.scope = scope;
        }

        public void Handle(WordSetCreated @event)
        {
            using (scope)
            {
                var sentence = scope.Executor.Execute<GenerateSentence, IEnumerable<Word>, Sentence>(@event.Words);
                scope.Executor.Execute<AddSentence, Sentence>(sentence);
            }
        }

        public bool ShouldHandle(WordSetCreated @event)
        {
            return @event.Words.Length >= 10;
        }
    }
}
