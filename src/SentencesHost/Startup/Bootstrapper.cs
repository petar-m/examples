using System.Data.Entity;
using System.Web.Http;
using M.Executables;
using M.Executables.Executors.SimpleInjector;
using M.Repository;
using M.Repository.EntityFramework;
using Newtonsoft.Json.Serialization;
using Owin;
using SentencesHost.Infrastructure;
using SentencesHost.Tasks;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace SentencesHost.Startup
{
    public class Bootstrapper
    {
        private Container container;

        public Bootstrapper()
        {

        }

        public void Run(IAppBuilder appBuilder)
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<DbContext, SentencesDbContext>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);

            container.Register<IExecutor, SimpleInjectorExecutor>(Lifestyle.Scoped);
            container.Register<IExecutorAsync, SimpleInjectorExecutor>(Lifestyle.Scoped);
            container.Register<IScopeEndHandler, ExecutorScopeEndHandler>(Lifestyle.Scoped);
            container.Register<IErrorHandler, ExecutorErrorHandler>(Lifestyle.Singleton);
            container.Register<IExecutorScope, ExecutorScope>(Lifestyle.Transient);

            appBuilder.Use(async (context, next) =>
            {
                using (var scope = AsyncScopedLifestyle.BeginScope(container))
                {
                    scope.WhenScopeEnds(() => scope.Container.GetInstance<IScopeEndHandler>().Handle(scope));
                    await next();
                }
            });

            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            config.MapHttpAttributeRoutes();
            //config.Services.Replace(typeof(IExceptionHandler), new Web.ControllerExceptionHandler());
            appBuilder.UseWebApi(config);

            TaskBuilder.Initialize(container);

            Verify();
        }

        public void Verify()
        {
            container.GetRegistration(typeof(IExecutorScope))
                     .Registration
                     .SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "IExecutorScope is intended to be disposed manually by caller.");

            container.Verify(VerificationOption.VerifyAndDiagnose);
        }
    }
}
