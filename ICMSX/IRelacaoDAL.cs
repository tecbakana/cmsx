using System;
using System.Collections.Generic;
using System.Data;

namespace ICMSX
{
    public interface IRelacaoDAL
    {
        void MakeConnection(dynamic prop);
        void CriaRelacaoAplicacao();
        void CriaRelacaoModulo();
        DataTable ListaRelacao();
    }
}
