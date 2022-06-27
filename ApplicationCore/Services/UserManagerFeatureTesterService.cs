using BlazorAppWasm.ApplicationCore.Models;

namespace BlazorAppWasm.ApplicationCore.Services;

public class UserManagerFeatureTesterService
{
    private readonly ILogger _logger;
    private FeatureTesterRouter _FeatureTesterRouter;
    private readonly List<Feature> _features_mock = new List<Feature>{
        new Feature(){
            Name= "Get Root",
            HttpMethod= "GET",
            UriPath= "/",
        },
        new Feature(){
            Name= "Get ABCD",
            HttpMethod= "GET",
            UriPath= "/a/b/{c}/{d}",
        },
        // TODO: Gerenciar conflitos
        // new Feature(){
        //     Name= "Get Root",
        //     HttpMethod= "GET",
        //     UriPath= "/{conflito}",
        // },
        new Feature(){
            Name= "Ler todos alunos",
            HttpMethod= "GET",
            UriPath= "/alunos",
        },
        new Feature(){
            Name= "Ler alunos individualmente",
            HttpMethod= "GET",
            UriPath= "/alunos/{id}",
        },
        new Feature(){
            Name= "Alterar alunos individualmente",
            HttpMethod= "PUT",
            UriPath= "/alunos/{id}",
        },
        new Feature(){
            Name= "Alterar foto de alunos individualmente",
            HttpMethod= "PATCH",
            UriPath= "/alunos/{id}/photo",
        },
        new Feature(){
            Name= "Cadastrar aluno",
            HttpMethod= "POST",
            UriPath= "/alunos/",
        },
    };

    public UserManagerFeatureTesterService(ILogger<UserManagerFeatureTesterService> logger)
    {
        _logger = logger;
        _FeatureTesterRouter = new FeatureTesterRouter(_features_mock);
        _logger.LogDebug("UserManagerFeatureTesterService iniciado");
    }

    public Feature Test(string httpMethod, string UriPath) =>
        _FeatureTesterRouter.Test(httpMethod, UriPath);
}