using System;
using M.Executables.Executors.SimpleInjector;
using SimpleInjector;

namespace SentencesHost.Infrastructure
{
    public class ExecutorErrorHandler : IErrorHandler
    {
        public void Handle(Exception exception, Scope scope)
        {
            Console.WriteLine(exception);
            scope.SetItem("HasError", true);
            scope.Dispose();
        }
    }
}
