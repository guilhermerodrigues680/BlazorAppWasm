using BlazorAppWasm.ApplicationCore.Models;

namespace BlazorAppWasm.ApplicationCore.Services;

public class UserManagerFeatureTesterService
{
    private readonly ILogger _logger;

    public UserManagerFeatureTesterService(ILogger<UserManagerFeatureTesterService> logger)
    {
        _logger = logger;
        _logger.LogDebug("UserManagerFeatureTesterService iniciado");
    }

    public Feature Test(string httpMethod, string UriPath)
    {
        throw new Exception("Não implementado");
    }

}