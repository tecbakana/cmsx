using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace CMSXBLL.Repositorio
{
    public class Relacao
    {
        public Guid RelacaoId { get; set; }
        public Guid PaiId { get; set; }
        public Guid FilhoId { get; set; }

        public static Relacao ObterNovaRelacao()
        {
            return new Relacao();
        }
    }
}
