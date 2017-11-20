using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using M.EventBroker;
using M.Executables;
using M.Executables.Executors.SimpleInjector;
using M.Repository;
using M.Repository.EntityFramework;
using Newtonsoft.Json.Serialization;
using Owin;
using SentencesHost.Events;
using SentencesHost.Infrastructure;
using SentencesHost.ScheduledTasks;
using SentencesHost.WordsProcessing;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace SentencesHost.Startup
{
    public class Bootstrapper
    {
        private Container container;

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

            container.Register<IEventHandler<WordCreated>, WordCreatedLogger>(Lifestyle.Singleton);
            container.RegisterCollection<IEventHandler<WordCreated>>(new Type[] { typeof(IEventHandler<WordCreated>) });

            container.Register<IEventHandler<WordSetCreated>, SentenceCreator>(Lifestyle.Transient);
            container.RegisterCollection<IEventHandler<WordSetCreated>>(new Type[] { typeof(IEventHandler<WordSetCreated>) });

            container.RegisterSingleton<IEventBroker>(() => new EventBroker(2, x => Console.WriteLine(x), t => container.GetAllInstances(typeof(IEventHandler<>).MakeGenericType(t))));

            container.RegisterSingleton<WordsTracker>();

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
