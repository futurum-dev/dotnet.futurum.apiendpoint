namespace Futurum.ApiEndpoint.DebugLogger;

public class ApiEndpointDebugNode
{
    public string Name { get; init; }
    public List<ApiEndpointDebugNode> Children { get; set; } = new();
}