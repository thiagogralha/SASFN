[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SASF.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SASF.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace SASF.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using SASF.Application.Interface;
    using SASF.Application;
    using SASF.Domain.Interfaces.Services;
    using SASF.Domain.Services;
    using SASF.Domain.Interfaces.Repositories;
    using SASF.Infra.Data.Repositories;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind<IFuncionarioAppService>().To<FuncionarioAppService>();
            kernel.Bind<IDependenteAppService>().To<DependenteAppService>();
            kernel.Bind<IPlanoAppService>().To<PlanoAppService>();
            kernel.Bind<IFichaAppService>().To<FichaAppService>();
            kernel.Bind<ITituloAppService>().To<TituloAppService>();

            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<IFuncionarioService>().To<FuncionarioService>();
            kernel.Bind<IDependenteService>().To<DependenteService>();
            kernel.Bind<IFichaService>().To<FichaService>();
            kernel.Bind<IPlanoService>().To<PlanoService>();
            kernel.Bind<ITituloService>().To<TituloService>();

            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IFuncionarioRepository>().To<FuncionarioRepository>();
            kernel.Bind<IDependenteRepository>().To<DependenteRepository>();
            kernel.Bind<IFichaRepository>().To<FichaRepository>();
            kernel.Bind<IPlanoRepository>().To<PlanoRepository>();
            kernel.Bind<ITituloRepository>().To<TituloRepository>();


        }        
    }
}
