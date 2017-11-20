using System.Collections.Generic;
using M.EventBroker;
using Sentences;
using SentencesHost.Events;

namespace SentencesHost.WordsProcessing
{
    public class WordsTracker
    {
        private object locker = new object();
        private List<Word> words = new List<Word>();
        private readonly IEventBroker eventBroker;

        public WordsTracker(IEventBroker eventBroker)
        {
            eventBroker.Subscribe<WordCreated>(Handle);
            this.eventBroker = eventBroker;
        }

        private void Handle(WordCreated @event)
        {
            lock (locker)
            {
                words.Add(@event.Word);
                if(words.Count < 10)
                {
                    return;
                }

                eventBroker.Publish(new WordSetCreated(words.ToArray()));
                words.Clear();
            }
        }
    }
}
