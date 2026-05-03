using CMSXData.Models;
using ICMSX;

namespace CMSXRepo;

public class ProdutoMaoDeObraRepositorio : BaseRepositorio, IProdutoMaoDeObraRepositorio
{
    public ProdutoMaoDeObraRepositorio(CmsxDbContext db) : base(db) { }

    public List<ProdutoMaoDeObra> ListarPorProduto(string produtoid) =>
        _db.ProdutoMaoDeObras
            .Where(m => m.Produtoid == produtoid)
            .OrderBy(m => m.Descricao)
            .ToList();

    public ProdutoMaoDeObra? BuscarPorId(Guid id) =>
        _db.ProdutoMaoDeObras.FirstOrDefault(m => m.Id == id);

    public ProdutoMaoDeObra Criar(ProdutoMaoDeObra mo)
    {
        _db.ProdutoMaoDeObras.Add(mo);
        _db.SaveChanges();
        return mo;
    }

    public ProdutoMaoDeObra Atualizar(ProdutoMaoDeObra mo)
    {
        _db.SaveChanges();
        return mo;
    }

    public void Remover(ProdutoMaoDeObra mo)
    {
        _db.ProdutoMaoDeObras.Remove(mo);
        _db.SaveChanges();
    }
}
