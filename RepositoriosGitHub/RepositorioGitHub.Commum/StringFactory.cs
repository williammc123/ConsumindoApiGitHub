using System.Text;

namespace RepositorioGitHub.Commum
{
    public class StringFactory
    {
        public static string BuildUrl(string url,string usuario, string acao)
        {
            var build = new StringBuilder();

            build.Append(url);
            build.Append(usuario + "/");
            build.Append(acao);

            return build.ToString();

        }

        public static string BuildUrl(string url2,string nameRepo)
        {
            
            var build = new StringBuilder();

            build.Append(url2);
            build.Append(nameRepo);
            return build.ToString();

        }

        //public static string BuildUrl(string url, string owner, string action, string name )
        //{
        //    //ComponenteTef
        //    var build = new StringBuilder();

        //    build.Append(url2);
        //    build.Append(nameRepo);
        //    return build.ToString();

        //}


    }
}
