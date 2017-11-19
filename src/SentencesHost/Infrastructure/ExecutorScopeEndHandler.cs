using M.Executables.Executors.SimpleInjector;
using M.Repository;
using SimpleInjector;

namespace SentencesHost.Infrastructure
{
    public class ExecutorScopeEndHandler : IScopeEndHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public ExecutorScopeEndHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Handle(Scope scope)
        {
            object hasErrorInScope = scope.GetItem("HasError");
            var hasError = hasErrorInScope == null ? false : (bool)hasErrorInScope;
            if (hasError)
            {
                unitOfWork.Rollback();
            }
            else
            {
                unitOfWork.Commit();
            }
        }
    }
}
