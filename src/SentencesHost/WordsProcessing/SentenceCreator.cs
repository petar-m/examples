using System.Collections.Generic;
using M.EventBroker;
using M.Executables;
using M.Executables.Executors.SimpleInjector;
using Sentences;
using SentencesHost.Events;

namespace SentencesHost.WordsProcessing
{
    public class SentenceCreator : IEventHandler<WordSetCreated>
    {
        private readonly IScopedContext scope;

        public SentenceCreator(IScopedContext scope)
        {
            this.scope = scope;
        }

        public void Handle(WordSetCreated @event)
        {
            scope.Execute<IExecutor>(GenerateAndAddSentence);

            void GenerateAndAddSentence(IExecutor executor)
            {
                var sentence = executor.Execute<GenerateSentence, IEnumerable<Word>, Sentence>(@event.Words);
                executor.Execute<AddSentence, Sentence>(sentence);
            }
        }

        public bool ShouldHandle(WordSetCreated @event)
        {
            return @event.Words.Length >= 10;
        }
    }
}
