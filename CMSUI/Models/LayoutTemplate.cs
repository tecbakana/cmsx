namespace CMSUI.Models;

public class LayoutTemplate
{
    public string Templateid { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? Tipo { get; set; }       // "home", "landing", etc.
    public string? Layout { get; set; }     // JSON dos blocos
    public bool Padrao { get; set; }        // template padrão ao criar tenant
    public DateTime Datainclusao { get; set; }
}
