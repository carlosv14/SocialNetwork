[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SocialNetwork.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SocialNetwork.Web.App_Start.NinjectWebCommon), "Stop")]

namespace SocialNetwork.Web.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataProtection;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using SocialNetwork.Database.Contexts;
    using SocialNetwork.Database.Models;
    using SocialNetwork.Database.Repositories;
    using SocialNetwork.Web.Utils;

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
            kernel.Bind<SocialNetworkContext>().ToSelf().InRequestScope();
            kernel.Load<AuthorizationModule>();
            kernel.Load<RepositoryModule>();
        }

        private class AuthorizationModule : NinjectModule
        {
            public override void Load()
            {
                this.Kernel.Bind<DbContext>().To<SocialNetworkContext>().InRequestScope();
                this.Kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().InRequestScope();
                this.Kernel.Bind<IDataProtectionProvider>().To<MachineKeyDataProtectionProvider>().InRequestScope();
                this.Kernel.Bind<IDataProtector>().ToMethod((k) => k.Kernel.Get<IDataProtectionProvider>().Create("ASP.NET Identity")).InRequestScope();
                this.Kernel.Bind<UserManager<ApplicationUser>>().To<ApplicationUserManager>().InRequestScope();
                this.Kernel.Bind<IAuthenticationManager>().ToMethod((k) => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            }
        }

        private class RepositoryModule : NinjectModule
        {
            public override void Load()
            {
                this.Kernel.Bind<IRepository<User>>().To<SocialNetworkUserRepository>().InRequestScope();
                this.Kernel.Bind<IRepository<Post>>().To<PostRepository>().InRequestScope();
                this.Kernel.Bind<IRepository<Friendship>>().To<FriendshipRepository>().InRequestScope();
            }
        }
    }
}