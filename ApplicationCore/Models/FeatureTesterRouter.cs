namespace BlazorAppWasm.ApplicationCore.Models;

public class FeatureTesterRouter
{
    private class MuxEntry
    {
        public Dictionary<string, MuxEntry> SubRoutes { get; set; }
        public Feature? Feature { get; set; }
        public string? ParamName { get; set; }

        public MuxEntry()
        {
            SubRoutes = new Dictionary<string, MuxEntry>();
        }
    }

    // private readonly ILogger _logger;
    private MuxEntry _RootEntry { get; set; }

    // ILogger<FeatureTesterRouter> logger
    public FeatureTesterRouter(List<Feature> features)
    {
        BuildTrie(features);
    }

    private void BuildTrie(List<Feature> features)
    {
        if (_RootEntry is null)
            _RootEntry = new MuxEntry();

        var curEntry = _RootEntry;
        foreach (var feature in features)
        {
            Console.WriteLine($"{feature.Name}");

            var path = feature.UriPath.Trim().ToLowerInvariant();

            if (!path.StartsWith("/"))
                // path = path.Remove(0, 1);
                path = "/" + path;

            if (path.EndsWith("/"))
                path = path.Remove(path.Length - 1);

            Console.WriteLine($"path={path}");
            var pathParts = path.Split('/');
            pathParts.Prepend(feature.HttpMethod);

            foreach (var pathPart in pathParts)
            {
                var pathPartIsPathParam = pathPart.StartsWith("{") && pathPart.EndsWith("}");
                Console.WriteLine($"p={pathPart}, {pathPartIsPathParam}");

                if (!curEntry.SubRoutes.ContainsKey(pathPart))
                {
                    // Se a arvore não possui a entrada, cria e adentra na arvore
                    if (pathPartIsPathParam)
                        curEntry.SubRoutes["{}"] = new MuxEntry();
                    else
                        curEntry.SubRoutes[pathPart] = new MuxEntry();
                }

                // TODO: Acho que pode ser um único if
                // Se a arvore já possui a entrada, adentra na arvore
                if (pathPartIsPathParam)
                {
                    curEntry = curEntry.SubRoutes["{}"];
                    curEntry.ParamName = pathPart;
                }
                else
                {
                    curEntry = curEntry.SubRoutes[pathPart];
                }
            }

            curEntry.Feature = feature;
            curEntry = _RootEntry;
        }

        // TODO: Debugar estrutura gerada
    }
}