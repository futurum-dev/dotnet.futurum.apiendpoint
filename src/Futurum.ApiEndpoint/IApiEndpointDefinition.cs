namespace Futurum.ApiEndpoint;

public interface IApiEndpointDefinition
{
    void Configure(ApiEndpointDefinitionBuilder definitionBuilder);
}