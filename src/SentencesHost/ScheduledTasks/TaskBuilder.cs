using System;
using System.Collections.Generic;
using M.EventBroker;
using M.Executables;
using M.Executables.Executors.SimpleInjector;
using M.Logging;
using M.ScheduledAction;
using M.ScheduledAction.Schedules;
using Sentences;
using SentencesHost.Events;
using SimpleInjector;

namespace SentencesHost.ScheduledTasks
{
    public static class TaskBuilder
    {
        private static Container container;

        public static void Initialize(Container container)
        {
            TaskBuilder.container = container;
        }

        public static IEnumerable<IScheduledAction> Build()
        {
            Action generateWordTask = () =>
            {
                var scope = container.GetInstance<IScopedContext>();
                scope.Execute<IExecutor, IEventBroker>(
                    (executor, eventBroker) =>
                    {
                        var word = executor.Execute<GenerateWord, Word>();
                        eventBroker.Publish(new WordCreated(word));
                    });
            };

            var options = new Options
            {
                ExecuteOnStart = true,
                OnError = (_, error) => Log.For<ScheduledAction>().Error($"Error generating word : {error}"),
                OnReschedule = (_, interval) => Log.For<ScheduledAction>().Trace($"Next word will be generated at: {DateTime.Now.Add(interval)}")
            };

            return new[] { new ScheduledAction(generateWordTask, new Fixed(TimeSpan.FromSeconds(10)), options) };
        }
    }
}
