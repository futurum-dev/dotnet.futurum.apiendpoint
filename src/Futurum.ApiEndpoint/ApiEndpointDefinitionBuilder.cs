using Futurum.ApiEndpoint.DebugLogger;
using Futurum.Core.Result;

namespace Futurum.ApiEndpoint;

public class ApiEndpointDefinitionBuilder
{
    private readonly List<IApiEndpointDefinitionBuilder> _apiEndpointDefinitions = new();

    public Result<IEnumerable<ApiEndpointDefinition>> Get() =>
        _apiEndpointDefinitions.FlatMap(x => x.Build())
                               .FlatMap(x => x.SelectMany(keyValuePair => keyValuePair.Value.Select(metadataDefinition => new ApiEndpointDefinition(keyValuePair.Key, metadataDefinition))));

    public IEnumerable<ApiEndpointDebugNode> Debug() =>
        _apiEndpointDefinitions.Select(x => x.Debug());

    public void Add(IApiEndpointDefinitionBuilder apiEndpointDefinitionBuilder)
    {
        _apiEndpointDefinitions.Add(apiEndpointDefinitionBuilder);
    }
}

public record ApiEndpointDefinition(Type ApiEndpointType, IMetadataDefinition MetadataDefinition);