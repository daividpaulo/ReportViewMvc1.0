using System.IO;
using System.Web;

namespace Relatorios.Util
{
    public class ReportViewMvcUtil
    {

        public string RetornaPathDllDeRelatorio(string nomeDllRelatorio)
        {
            string caminhoPath = "";
            caminhoPath = HttpContext.Current.Server.MapPath("/");
            caminhoPath = Path.GetDirectoryName(caminhoPath);
            caminhoPath = Path.GetDirectoryName(caminhoPath);
            return caminhoPath + "\\" + nomeDllRelatorio;
        }




    }
}