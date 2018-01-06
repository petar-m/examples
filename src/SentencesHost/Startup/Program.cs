using System;
using M.Logging;
using M.Logging.NLog;
using Microsoft.Owin.Hosting;
using SentencesHost.ScheduledTasks;

namespace SentencesHost.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Initialize(new NLogLoggerFactory());

            var bootstrapper = new Bootstrapper();
            using (var webApp = WebApp.Start($"http://+:{8989}/", x => bootstrapper.Run(x)))
            {
                foreach (var task in TaskBuilder.Build())
                {
                    task.Start();
                }

                Console.Read();
            }
        }
    }
}
