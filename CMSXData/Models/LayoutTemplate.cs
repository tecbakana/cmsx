namespace CMSXData.Models;

public class LayoutTemplate
{
    public string Templateid { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? Tipo { get; set; }
    public string? Layout { get; set; }
    public bool Padrao { get; set; }
    public DateTime Datainclusao { get; set; }
}
