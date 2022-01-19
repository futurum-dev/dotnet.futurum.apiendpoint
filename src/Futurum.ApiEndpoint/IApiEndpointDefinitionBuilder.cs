using Futurum.ApiEndpoint.DebugLogger;
using Futurum.Core.Result;

namespace Futurum.ApiEndpoint;

public interface IApiEndpointDefinitionBuilder
{
    Result<Dictionary<Type, List<IMetadataDefinition>>> Build();

    ApiEndpointDebugNode Debug();
}