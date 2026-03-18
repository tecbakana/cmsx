using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMSXBLL
{
    public class Unidade
    {
        public Guid unidadeId { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }

        public static Unidade ObterNovaUnidade()
        {
            return new Unidade();
        }
    }
}
