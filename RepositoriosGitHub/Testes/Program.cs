using Newtonsoft.Json;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Infra.ApiGitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes
{
    class Program
    {
        static void Main(string[] args)
        {
            GitHubApi git = new GitHubApi();
            string a = "teste";
            string b = "teste";
            //var teste = git.GetMyRepositories();


            var outroTeste = git.GetRepositoryByName("ComponenteTef");
        }
    }
}
