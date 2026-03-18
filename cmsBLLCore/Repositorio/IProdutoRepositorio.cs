using System;
using System.Collections.Generic;
using System.Data;

namespace CMSXBLL.Repositorio
{
    public interface IProdutoRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Produto> Helper(DataTable moddata);
        List<Produto> ListaProduto();
        List<Produto> ListaProdutoXId();
        List<Produto> ListaProdutoXCategoria();
        List<Produto> ListaProdutoXTipo();
        void CriaProduto(Produto prod);
        void EditaProduto(Produto prod);
        void InativaProduto(Produto prod);
    }
}
