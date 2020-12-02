using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGitHub.Commum
{
    public class ActionResult<TModel> where TModel: class
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public IList<TModel> Results { get; set; }

        public TModel Result { get; set; }
    }
}
