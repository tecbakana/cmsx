namespace CMSUI.Models;

public partial class Ciaaerea
{
    public string Ciaaereaid { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? Lotipo { get; set; }
    public string? DescricaoLonga { get; set; }
    public byte? Ativo { get; set; }
    public byte? Tiponac { get; set; }
    public byte? Tipoint { get; set; }
    public string? WebticketStr { get; set; }
}
