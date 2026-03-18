using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CMSXData;
using CMSXData.Models;

namespace ICMSX
{
    public interface IProdutoDAL
    {
        int NumParms { get; set; }
        void CriaProduto();
        void MakeConnection(dynamic prop);
        IEnumerable<Produto> ListaProduto();
        DataTable ListaProdutoPorId();
        DataTable ListaProdutoRelacionado();
        void EditaProduto();
        void InativaProduto();
    }
}
