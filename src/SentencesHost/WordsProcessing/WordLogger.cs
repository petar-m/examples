using M.EventBroker;
using SentencesHost.Events;
using static System.Console;

namespace SentencesHost.WordsProcessing
{
    public class WordCreatedLogger : IEventHandler<WordCreated>
    {
        public void Handle(WordCreated @event) => WriteLine($"Word generated: {@event.Word.Value}");

        public bool ShouldHandle(WordCreated @event) => true;
    }
}
