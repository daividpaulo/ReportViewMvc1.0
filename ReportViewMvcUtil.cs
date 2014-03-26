using System.IO;
using System.Web;

namespace Relatorios.Util
{
        // Create by Daivid Paulo V. F. de Sousa
	// daividpaulo.infor@gmail.com
	// 26/03/2014
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