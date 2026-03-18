using System;
using System.Collections.Generic;
using System.Data;

namespace ICMS
{
    public interface IRelacaoDAL
    {
        void MakeConnection(dynamic prop);
        void CriaRelacaoAplicacao();
        void CriaRelacaoModulo();
        DataTable ListaRelacao();
    }
}
