using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMS
{
    public interface IFornecedorDAL
    {
        void MakeConnection(dynamic prop);
        void CriaFornecedor();
        DataTable ListaFornecedor();
        DataTable ListaFornecedorPorId();
        DataTable ListaFornecedorPorNome();
        DataTable ListaFornecedorPorAplicacaoId();
        void AtualizaFornecedor();
    }
}
