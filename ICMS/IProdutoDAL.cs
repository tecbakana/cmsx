using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMS
{
    public interface IProdutoDAL
    {
        int NumParms { get; set; }
        void CriaProduto();
        void MakeConnection(dynamic prop);
        IEnumerable<object> ListaProduto();
        DataTable ListaProdutoPorId();
        DataTable ListaProdutoRelacionado();
        void EditaProduto();
        void InativaProduto();
    }
}
