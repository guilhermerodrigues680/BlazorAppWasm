using BlazorAppWasm.ApplicationCore.Exceptions;

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
        _RootEntry = new MuxEntry();
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

            var method = feature.HttpMethod.Trim().ToLowerInvariant();
            var path = feature.UriPath.Trim().ToLowerInvariant();

            // if (!path.StartsWith("/"))
            //     path = "/" + path;
            if (path.StartsWith("/"))
                path = path.Remove(0, 1);

            if (path.EndsWith("/"))
                path = path.Remove(path.Length - 1);

            Console.WriteLine($"path={path}");
            var pathParts = new List<string>(path.Split('/'));
            pathParts.Insert(0, method);

            foreach (var p in pathParts)
                Console.Write("{0} , ", p);
            Console.WriteLine();

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

    public Feature Test(string httpMethod, string UriPath)
    {
        Console.WriteLine("{0} {1}", httpMethod, UriPath);
        var method = httpMethod.Trim().ToLowerInvariant();
        var path = UriPath.Trim().ToLowerInvariant();
        // if (!path.StartsWith("/"))
        //     path = "/" + path;
        if (path.StartsWith("/"))
            path = path.Remove(0, 1);
        if (path.EndsWith("/"))
            path = path.Remove(path.Length - 1);

        Console.WriteLine($"Test httpMethod={httpMethod} path={path}");
        var pathParts = new List<string>(path.Split('/'));
        pathParts.Insert(0, method);

        foreach (var p in pathParts)
            Console.Write("{0} , ", p);
        Console.WriteLine();

        var matchParams = new List<FeatureMatchParam>();
        Feature? matchFeature = null;
        var curEntry = _RootEntry.SubRoutes;
        for (int idx = 0; idx < pathParts.Count; idx++)
        {
            var pathPart = pathParts[idx];
            // Rota com parametro no Path?
            var pIsPathParam = curEntry.ContainsKey("{}");

            Console.WriteLine($"idx={idx} pathPart={pathPart}, pIsPathParam={pIsPathParam}");

            // Se não tem o path, não é um match. Encerra o loop.
            if (!pIsPathParam && !curEntry.ContainsKey(pathPart))
                break;

            MuxEntry pathPartMuxEntry;
            if (pIsPathParam)
            {
                pathPartMuxEntry = curEntry["{}"];
                matchParams.Add(new FeatureMatchParam(pathPartMuxEntry.ParamName ?? "{}", pathPart));
            }
            else
                pathPartMuxEntry = curEntry[pathPart];

            if (idx == pathParts.Count - 1 && pathPartMuxEntry.Feature is not null)
                // Se for o último elemento e a feature existir é um match
                matchFeature = pathPartMuxEntry.Feature;
            else
                // Se não for o último elemento continua navegando
                curEntry = pathPartMuxEntry.SubRoutes;
        }

        if (matchFeature is null)
            // retorna null ou lança uma exceção
            // return null;
            throw new NotFoundException($"{httpMethod} {UriPath} - Not Found");

        var featureMatch = new FeatureMatch(matchFeature, matchParams);
        Console.WriteLine(featureMatch);
        return matchFeature;
    }
}