using M.EventBroker;
using M.Logging;
using SentencesHost.Events;

namespace SentencesHost.WordsProcessing
{
    public class WordLogger : IEventHandler<WordCreated>
    {
        public void Handle(WordCreated @event) => Log.For(this).Info($"Word generated: {@event.Word.Value}");

        public bool ShouldHandle(WordCreated @event) => true;
    }
}
