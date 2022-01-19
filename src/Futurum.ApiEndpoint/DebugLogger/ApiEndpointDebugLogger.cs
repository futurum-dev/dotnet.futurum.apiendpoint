using System.Text;

namespace Futurum.ApiEndpoint.DebugLogger;

public interface IApiEndpointDebugLogger
{
    void Execute(List<ApiEndpointDebugNode> nodes);
}

public class ApiEndpointDebugLogger : IApiEndpointDebugLogger
{
    private readonly IApiEndpointLogger _logger;

    // Taken from https://andrewlock.net/creating-an-ascii-art-tree-in-csharp/
    // Constants for drawing lines and spaces
    private const string Cross = " ├─";
    private const string Corner = " └─";
    private const string Vertical = " │ ";
    private const string Space = "   ";

    public ApiEndpointDebugLogger(IApiEndpointLogger logger)
    {
        _logger = logger;
    }
    
    public void Execute(List<ApiEndpointDebugNode> nodes)
    {
        var stringBuilder = new StringBuilder();

        foreach (var node in nodes)
        {
            PrintNode(stringBuilder, node, indent: "");
        }

        var log = stringBuilder.ToString();
        
        _logger.ApiEndpointDebugLog(log);
    }

    private static void PrintNode(StringBuilder stringBuilder, ApiEndpointDebugNode node, string indent)
    {
        stringBuilder.AppendLine(node.Name);

        // Loop through the children recursively, passing in the
        // indent, and the isLast parameter
        var numberOfChildren = node.Children.Count();
        for (var i = 0; i < numberOfChildren; i++)
        {
            var child = node.Children[i];
            var isLast = i == numberOfChildren - 1;
            PrintChildNode(stringBuilder, child, indent, isLast);
        }
    }

    private static void PrintChildNode(StringBuilder stringBuilder, ApiEndpointDebugNode node, string indent, bool isLast)
    {
        // Print the provided pipes/spaces indent
        stringBuilder.Append(indent);

        // Depending if this node is a last child, print the
        // corner or cross, and calculate the indent that will
        // be passed to its children
        if (isLast)
        {
            stringBuilder.Append(Corner);
            indent += Space;
        }
        else
        {
            stringBuilder.Append(Cross);
            indent += Vertical;
        }

        PrintNode(stringBuilder, node, indent);
    }
}