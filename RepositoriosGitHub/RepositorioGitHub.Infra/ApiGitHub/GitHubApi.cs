using Newtonsoft.Json;
using RepositorioGitHub.Commum;
using RepositorioGitHub.Dominio;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGitHub.Infra.ApiGitHub
{
    public class GitHubApi
    {
        private const string MEU_REPOSITORIO_NOME = "williammc123";
        private const string PARAMETER_REPOSITORY = "repos";

        string url = $"https://api.github.com/users/";
        string url2 = $"https://api.github.com/search/repositories?q=";


        public ActionResult<GitHubRepository> GetMyRepositories()
        {
            return GetRepository(MEU_REPOSITORIO_NOME);
        }

        public ActionResult<GitHubRepository> GetRepository(string owner)
        {
            var result = new ActionResult<GitHubRepository>();

            var response = Request(owner, PARAMETER_REPOSITORY);
            if (response.IsSuccessful)
            {
                var objectGitHubRepositories = JsonConvert.DeserializeObject<GitHubRepository[]>(response.Content).ToList();
                result.IsValid = true;
                result.Message = " Repositório(s) Carregado Com Sucesso";
                result.Results = objectGitHubRepositories;
                return result;
            }
            else
            {
                result.IsValid = false;
                result.Message = "Erro, não foi possivel completar a operação: " + response.StatusCode;
                return result;
            }

        }

        public ActionResult<RepositoryModel> GetRepositoryByName(string name)
        {
            var result = new ActionResult<RepositoryModel>();
            var listRepo = new List<RepositoryModel>();

            var response = Request(name);
            if (response.IsSuccessful)
            {
                var objectGitHubRepository = JsonConvert.DeserializeObject<RepositoryModel>(response.Content);
                result.IsValid = true;
                result.Message = "Sucesso";
                result.Result = objectGitHubRepository;
                return result;
            }
            else
            {
                result.IsValid = false;
                result.Message = "Erro, não foi possivel completar a operação: " + response.StatusCode;
                return result;
            }

        }


        private IRestResponse Request(string owner, string action)
        {
            var uri = StringFactory.BuildUrl(url, owner, action);
            IRestResponse response = Response(uri);

            return response;
        }

        private IRestResponse Request(string name)
        {
            var uri = StringFactory.BuildUrl(url2, name);
            IRestResponse response = Response(uri);

            return response;
        }

        //private IRestResponse GetResponse(string name, string owner, string action)
        //{
        //    var uri = StringFactory.BuildUrl(url, owner, action,name);
        //    IRestResponse response = Get(uri);

        //    return response;
        //}

        private static IRestResponse Response(string uri)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }

    }
}
