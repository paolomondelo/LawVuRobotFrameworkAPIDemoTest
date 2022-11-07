using AppLogic.DependencyInjection;
using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DependencyInjection
{
    public class WebApiServiceRegistry : ServiceRegistry
    {
        public WebApiServiceRegistry()
        {
            IncludeRegistry<AppLogicServiceRegistry>();
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions(ServiceLifetime.Scoped);
            });
        }
    }
}
