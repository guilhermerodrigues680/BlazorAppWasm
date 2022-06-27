namespace BlazorAppWasm.ApplicationCore.Models;

public class FeatureMatchParam
{
    public string Name { get; set; }
    public string Value { get; set; }

    public FeatureMatchParam(string name, string value)
    {
        Name = name;
        Value = value;
    }
}