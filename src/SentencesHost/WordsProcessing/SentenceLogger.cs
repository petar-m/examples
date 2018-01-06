using M.EventBroker;
using M.Logging;
using SentencesHost.Events;

namespace SentencesHost.WordsProcessing
{
    public class SentenceLogger : IEventHandler<SentenceCreated>
    {
        public void Handle(SentenceCreated @event) => Log.For(this).Info($"Sentence created: {@event.Sentence.Value}");

        public bool ShouldHandle(SentenceCreated @event) => true;
    }
}
