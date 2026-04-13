using CMSXData.Models;

namespace CMSXBLL
{
    public class Aplicacao : CMSXData.Models.Aplicacao
    {
        public static Aplicacao ObterAplicacao()
        {
            return new Aplicacao();
        }
    }
}
