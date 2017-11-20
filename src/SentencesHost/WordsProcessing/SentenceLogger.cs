using M.EventBroker;
using SentencesHost.Events;
using static System.Console;

namespace SentencesHost.WordsProcessing
{
    public class SentenceLogger : IEventHandler<SentenceCreated>
    {
        public void Handle(SentenceCreated @event) => WriteLine($"Sentence created: {@event.Sentence.Value}");

        public bool ShouldHandle(SentenceCreated @event) => true;
    }
}
