using Ninject.Modules;
using TestLoginMyViettel.HttpClientServices;
using TestLoginMyViettel.Services;

namespace TestLoginMyViettel.IoC
{
    public class ConfigModules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IHttpClientFactory>().To<HttpClientFactory>().InSingletonScope();
            Bind<IMyViettelServices>().To<MyViettelServices>().InSingletonScope();
        }

        #endregion
    }
}