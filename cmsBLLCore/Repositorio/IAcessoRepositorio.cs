using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace CMSXBLL.Repositorio
{
    public interface IAcessoRepositorio
    {
        dynamic ValidaAcesso();
        void MakeConnection(dynamic prop);
    }
}
