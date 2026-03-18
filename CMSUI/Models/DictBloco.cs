namespace CMSUI.Models;

public partial class DictBloco
{
    public string Tipobloco { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? Icone { get; set; }
    public string SchemaConfig { get; set; } = null!;
}
