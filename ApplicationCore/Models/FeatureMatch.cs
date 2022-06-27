namespace BlazorAppWasm.ApplicationCore.Models;

public class FeatureMatch
{
    public Feature Feature { get; set; }
    public List<FeatureMatchParam>? FeatureMatchParams { get; set; }

    public FeatureMatch(Feature feature, List<FeatureMatchParam> featureMatchParams)
    {
        Feature = feature;
        FeatureMatchParams = featureMatchParams;
    }

    public override string ToString()
    {
        if (FeatureMatchParams.Count == 0)
            return $"{base.ToString()}:\nFeature - {Feature}\nFeatureMatchParams - []";

        var fmpText = string.Join("\n", FeatureMatchParams.Select(fmp => $"{fmp.Name} -> {fmp.Value}").ToList());
        return $"{base.ToString()}:\nFeature - {Feature}\nFeatureMatchParams - [{fmpText}]";
    }
}
