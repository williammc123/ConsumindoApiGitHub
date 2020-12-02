using RepositorioGitHub.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGitHub.Infra.Context
{
    public class ContextData : DbContext
    {
        

        public ContextData() : base("ContextData")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public System.Data.Entity.DbSet<Favorite> Favoritos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }
    }
}
