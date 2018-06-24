using M.EventBroker;
using M.Logging;
using SentencesHost.Events;
using System;

namespace SentencesHost.WordsProcessing
{
    public class WordLogger : IEventHandler<WordCreated>
    {
        public void Handle(WordCreated @event) => Log.For(this).Info($"Word generated: {@event.Word.Value}");

        public void OnError(Exception exception, WordCreated @event) => Log.For(this).Error(exception);

        public bool ShouldHandle(WordCreated @event) => true;
    }
}
