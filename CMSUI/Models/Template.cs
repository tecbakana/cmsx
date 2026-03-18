namespace CMSUI.Models;

public partial class Template
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public string? Url { get; set; }

    public bool? Ativo { get; set; }
}
