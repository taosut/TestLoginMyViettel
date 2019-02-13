using Ninject;
using TestLoginMyViettel.Services;

namespace TestLoginMyViettel.IoC
{
    public static class IoCContainer
    {
        #region Private methods

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        #endregion

        #region Public properties

        /// <summary>
        ///     The kernel for my IoC container
        /// </summary>
        public static IKernel Kernel { get; set; }

        public static IMyViettelServices MyViettelServices => Get<IMyViettelServices>();

        #endregion
    }
}