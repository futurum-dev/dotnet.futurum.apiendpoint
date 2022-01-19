using System.Reflection;

using Futurum.ApiEndpoint.DebugLogger;
using Futurum.Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

namespace Futurum.ApiEndpoint;

public class ApiEndpointModule : IModule
{
    private readonly Assembly[] _assemblies;

    public ApiEndpointModule(params Assembly[] assemblies)
    {
        _assemblies = assemblies;
    }

    public void Load(IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(_assemblies)
                                  .AddClasses(classes => classes.Where(type => type.GetInterfaces().Contains(typeof(IApiEndpointDefinition))))
                                  .AsImplementedInterfaces()
                                  .WithSingletonLifetime());
        
        services.AddSingleton<IApiEndpointDebugLogger, ApiEndpointDebugLogger>();
    }
}