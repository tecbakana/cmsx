using CMSXData.Models;

namespace ICMSX;

public interface IProdutoMaoDeObraRepositorio
{
    List<ProdutoMaoDeObra> ListarPorProduto(string produtoid);
    ProdutoMaoDeObra? BuscarPorId(Guid id);
    ProdutoMaoDeObra Criar(ProdutoMaoDeObra mo);
    ProdutoMaoDeObra Atualizar(ProdutoMaoDeObra mo);
    void Remover(ProdutoMaoDeObra mo);
}
