namespace CMSXData.Models;

public partial class ProdutoMaoDeObra
{
    public Guid Id { get; set; }
    public string Produtoid { get; set; } = null!;
    public string Tipo { get; set; } = null!;        // "capacidade_dia" | "milheiro"
    public string Descricao { get; set; } = null!;
    public int? CapacidadeDia { get; set; }
    public decimal? ValorDia { get; set; }
    public decimal? ValorMilheiro { get; set; }

    public virtual Produto? Produto { get; set; }
}
