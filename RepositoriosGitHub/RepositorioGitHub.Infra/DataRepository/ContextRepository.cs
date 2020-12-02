using Microsoft.Data.Sqlite;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Infra.Context;
using RepositorioGitHub.Infra.Contract;
using System.Collections.Generic;
using System.Linq;

namespace RepositorioGitHub.Infra.Repository
{
    public class ContextRepository: IContextRepository
    {
        private readonly ContextData _context;

        public ContextRepository(ContextData context)
        {
            _context = context;
        }

        public bool Insert( Favorite favorite)
        {
            try
            {
                _context.Favoritos.Add(favorite);
                _context.SaveChanges();
                return true;
                
            }
            catch (System.Exception  )
            {
                return false;
            }
            
        }

        public List<Favorite> GetAll()
        {
            return _context.Favoritos.ToList();
        }

        public bool ExistsByCheckAlready(Favorite favorite)
        {
            var verify = _context.Favoritos.FirstOrDefault(p => p.Name == favorite.Name && p.Language == favorite.Language && p.Owner == favorite.Owner);
            return verify == null ? false : true;          
        }
    }
}
