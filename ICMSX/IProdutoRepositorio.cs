using CMSXData.Models;

namespace ICMSX
{
    public interface IProdutoRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Produto> ListaProduto();
        List<Produto> ListaProdutoXId();
        List<Produto> ListaProdutoXCategoria();
        List<Produto> ListaProdutoXTipo();
        void CriaProduto(Produto prod);
        void EditaProduto(Produto prod);
        void InativaProduto(Produto prod);
    }
}