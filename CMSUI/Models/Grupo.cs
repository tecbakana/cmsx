namespace CMSUI.Models;

public partial class Grupo
{
    public string Grupoid { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public bool Acessototal { get; set; }
}
