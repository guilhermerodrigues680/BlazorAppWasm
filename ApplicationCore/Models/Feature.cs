namespace BlazorAppWasm.ApplicationCore.Models;

public class Feature
{
    // public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string HttpMethod { get; set; } = string.Empty;
    public string UriPath { get; set; } = string.Empty;

    public override string ToString() =>
        $"{base.ToString()}: {Name} - {HttpMethod} {UriPath}";
}
