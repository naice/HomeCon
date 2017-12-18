using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core
{
    public class App : RestServer.IRestServerServiceDependencyResolver
    {
        #region SINGLETON
        private static App _Instance;
        public static App Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new App();
                return _Instance;
            }
        }
        App()
        {

        }
        #endregion

        private Container _container;
        public void Run()
        {
            _container = CreateIoCContainer();
        }


        private Container CreateIoCContainer()
        {
            Container container = new Container();
            container.RegisterSingleton<Configuration>();
            container.RegisterSingleton<RestServer.ILogger>(Log.DefaultLogger);
            container.RegisterSingleton<RestServer.IRestServerServiceDependencyResolver>(this);
            container.Register<RestServer.IConverter, JsonConvert>(Lifestyle.Singleton);
            container.RegisterSingleton<ApiServer>();

            container.RegisterSingleton<Hardware.I2CBridge>();

            container.Verify();

            return container;
        }

        #region RestServer.IRestServerServiceDependencyResolver
        public object[] GetDependecys(Type[] dependencyTypes)
        {
            return dependencyTypes.Select(type => GetDependency(type)).ToArray();
        }
        public object GetDependency(Type dependencyType)
        {
            return _container.GetInstance(dependencyType);
        }
        #endregion
    }
}
