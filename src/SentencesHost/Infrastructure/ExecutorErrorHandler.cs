using System;
using M.Executables.Executors.SimpleInjector;
using M.Logging;
using SimpleInjector;

namespace SentencesHost.Infrastructure
{
    public class ExecutorErrorHandler : IErrorHandler
    {
        public void Handle(Exception exception, Scope scope)
        {
            Log.For(this).Error(exception);
            scope.SetItem("HasError", true);
            scope.Dispose();
        }
    }
}
