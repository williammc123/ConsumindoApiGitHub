using RepositorioGitHub.Business.Contract;
using RepositorioGitHub.Commum;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Infra.ApiGitHub;
using RepositorioGitHub.Infra.Contract;
using RepositorioGitHub.Infra.Repository;
using RepositorioGitHubViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositorioGitHub.Business
{
    public class GitHubApiBusiness: IGitHubApiBusiness
    {
        private readonly IContextRepository _context;
        public GitHubApiBusiness(IContextRepository context)
        {
            _context = context;
        }
        public ActionResult<GitHubRepositoryViewModel> Get()
        {
            ActionResult<GitHubRepositoryViewModel> model = new ActionResult<GitHubRepositoryViewModel>();
            List<GitHubRepositoryViewModel> repositories = new List<GitHubRepositoryViewModel>();

            GitHubApi gitHubApi = new GitHubApi();
            var result = gitHubApi.GetMyRepositories();

            foreach (var item in result.Results)
            {
                GitHubRepositoryViewModel repository = new GitHubRepositoryViewModel()
                {
                    Id = item.Id,
                    Description = item.Description,
                    FullName = item.FullName,
                    Homepage = item.Homepage,
                    Language = item.Language,
                    Name = item.Name,
                    Owner = item.Owner,
                    UpdatedAt = item.UpdatedAt,
                    Url = item.Url
                };
                repositories.Add(repository);
            }

            model.Results = repositories;
            model.Message = result.Message;
            model.IsValid = result.IsValid;

            return model;
        }


        public ActionResult<RepositoryViewModel> GetByName(string name)
        {
            ActionResult<RepositoryViewModel> model = new ActionResult<RepositoryViewModel>();

            if (string.IsNullOrEmpty(name))
            {
                model.Message = "Por favor verificar o campo nome do repositório";
                model.IsValid = false;
                return model;
            }

            GitHubApi gitHubApi = new GitHubApi();
            var result = gitHubApi.GetRepositoryByName(name);

            RepositoryViewModel repository = new RepositoryViewModel()
            {
                Repositories = result.Result.Repositories,
                TotalCount = result.Result.TotalCount
            };

            model.Result = repository;
            model.Message = result.Message;
            model.IsValid = result.IsValid;

            return model;
        }

        public ActionResult<GitHubRepositoryViewModel> GetById(long id)
        {
            ActionResult<GitHubRepositoryViewModel> model = new ActionResult<GitHubRepositoryViewModel>();
            var response = Get();
            if (response.IsValid)
            {
                var result = response.Results.Where(p => p.Id.Equals(id)).FirstOrDefault();
                model.Result = result;
                model.Message = response.Message;
                model.IsValid = response.IsValid;

                return model;
            }
            else
            {
                return model;
            }

        }

        public ActionResult<GitHubRepositoryViewModel> GetRepository(string owner, long id)
        {
            ActionResult<GitHubRepositoryViewModel> model = new ActionResult<GitHubRepositoryViewModel>();
            GitHubApi gitHubApi = new GitHubApi();
            var response = gitHubApi.GetRepository(owner);

            if (response.IsValid)
            {
                var result = response.Results.Where(p => p.Id.Equals(id)).FirstOrDefault();
                if (result == null)
                {
                    model.IsValid = false;
                    model.Message = "Este repositório não é válido";
                    return model;
                }

                GitHubRepositoryViewModel view = new GitHubRepositoryViewModel()
                {
                    Id = result.Id,
                    Description = result.Description,
                    FullName = result.FullName,
                    Homepage = result.Homepage,
                    Language = result.Language,
                    Name = result.Name,
                    Owner = result.Owner,
                    UpdatedAt = result.UpdatedAt,
                    Url = result.Url
                };

                model.Result = view;
                model.Message = response.Message;
                model.IsValid = response.IsValid;
            }

            return model;
        }

        public ActionResult<FavoriteViewModel> GetFavoriteRepository()
        {
            ActionResult<FavoriteViewModel> model = new ActionResult<FavoriteViewModel>();
            List<FavoriteViewModel> favoriteViewModel = new List<FavoriteViewModel>();
            
            var response = _context.GetAll();

            if (response.Count > 0)
            {
                foreach (var item in response)
                {
                    FavoriteViewModel viewModel = new FavoriteViewModel()
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Language = item.Language,
                        Owner = item.Owner,
                        UpdateLast = item.UpdateLast,
                        Name = item.Name

                    };

                    favoriteViewModel.Add(viewModel);
                }

                model.IsValid = true;
                model.Message = "Sucesso";
                model.Results = favoriteViewModel;
            }

            return model;
        }

        public ActionResult<FavoriteViewModel> SaveFavoriteRepository(FavoriteViewModel view)
        {
            ActionResult<FavoriteViewModel> model = new ActionResult<FavoriteViewModel>();
            if (view != null)
            {
                Favorite favorite = new Favorite()
                {
                    Description = view.Description,
                    Language = view.Language,
                    Owner = view.Owner,
                    UpdateLast = view.UpdateLast,
                    Name = view.Name
                };

                var check = _context.ExistsByCheckAlready(favorite);
                if (check)
                {
                    model.IsValid = false;
                    model.Message = "Este Repositório já esta marcado como Favorito";
                    return model;
                }

                var response = _context.Insert(favorite);
                if (response)
                {
                    model.IsValid = true;
                    model.Message = "Salvo com Sucesso";
                }
                else
                {
                    model.Message = "Não foi possivel realizar a operação";
                    model.IsValid = false;
                }

            }
            else
            {
                model.Message = "Não foi possivel realizar a operação";
                model.IsValid = false;
            }

            return model;
        }

    }
}
